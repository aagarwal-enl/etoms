using EAGetMail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ETOMS
{
    public class Global : HttpApplication
    {
        static string taskOrderName, taskOrderState, COR, submitStatus, bidDecision, RFPNumber, requestType, EGOSID, description, governmentCust;
        static DateTime bidDecisionDueDate = System.DateTime.Now;
        static Byte[] bytes = null;

        static string connectionstring = @"Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true";
        static Imap4Folder FindFolder(string folderPath, Imap4Folder[] folders)
        {
            int count = folders.Length;
            for(int i=0;i<count;i++)
            {
                Imap4Folder curr_folder = folders[i];
                if(string.Compare(curr_folder.LocalPath, folderPath, true) == 0)
                {
                    return curr_folder;
                }

                curr_folder = FindFolder(folderPath, curr_folder.SubFolders);
                if(curr_folder != null)
                {
                    return curr_folder;
                }
            }
            return null;
        }

        static string generateFileName(int seq)
        {
            DateTime currentTime = DateTime.Now;
            return string.Format("{0}-{1:000}-{2:000}.eml",
                currentTime.ToString("yyyyMMddHHmmss", new CultureInfo("en-US")),
                currentTime.Millisecond,
                seq);
        }

        static void loadInDB(string bodyText, Attachment[] atts)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            string insquery = "INSERT INTO task_order (E_GOSID, task_order_name, request_type, description, task_order_state, COR, submit_status, bid_proposal_decision, bid_decision_due_date, government_customers, attachments) VALUES" +
                "(@EGOSID, @taskOrderName, @requestType, @description, @taskOrderState, @COR, @submitStatus, @bidDecision, @bidDecisionDueDate, @governmentCust, @attachments)";
            SqlCommand inscmd = new SqlCommand(insquery, con);
            

            //System.Diagnostics.Debug.WriteLine("COR: {0}", COR);
            //System.Diagnostics.Debug.WriteLine("Contents: {0}", bodyText);
            string[] body = bodyText.Split('\n');
            string start = "";

            string tempFolder = "c:\\ETOMStemp";
            if (!System.IO.Directory.Exists(tempFolder))
            {
                System.IO.Directory.CreateDirectory(tempFolder);
            }
                
            int count = atts.Length;
            for(int i=0;i<count;i++)
            {
                Attachment att = atts[i];
                System.Diagnostics.Debug.WriteLine(att.Name);
                string filePath = HttpContext.Current.Server.MapPath(att.Name);
                if (att.Name.Contains(".docx"))
                {
                    string attname = String.Format("{0}\\{1}", tempFolder, att.Name);
                    att.SaveAs(attname, true);
                    FileStream fs = new FileStream(attname, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();
                }
            }

            System.Diagnostics.Debug.WriteLine("Attachments: {0}", bytes);

            for (int i=0;i<body.Length;i++)
            {
                string current = body[i];
                taskOrderState = "Open";
                if (current.Contains("Title:"))
                {
                    start = "Title:";
                    taskOrderName = current.Substring(start.Length + 1);
                }
                else if (current.StartsWith("Request:"))
                {
                    start = "Request:";
                    requestType = current.Substring(start.Length + 1);
                }

                else if (current.StartsWith("Description:"))
                {
                    start = "Description:";
                    description = current.Substring(start.Length + 1);
                }

                else if (current.StartsWith("E-GOS ID:"))
                {
                    start = "E-GOS ID:";
                    EGOSID = current.Substring(start.Length + 1);
                }

                else if (current.StartsWith("Government Customer:"))
                {
                    start = "Government Customer:";
                    governmentCust = current.Substring(start.Length + 1);
                }

                else if (current.StartsWith("COR:"))
                {
                    start = "COR:";
                    COR = current.Substring(start.Length + 1);
                }

                /*else if(current.StartsWith("Submission Status:"))
                {
                    start = "Submission Status:";
                    submitStatus = current.Substring(start.Length + 1);
                }*/

                /*else if(current.StartsWith("Bid-Decision:"))
                {
                    start = "Bid-Decision:";
                    bidDecision = current.Substring(start.Length + 1);
                }*/

                else if (current.Contains("due"))
                {
                    Match match = Regex.Match(current, @"\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}");
                    string date = match.Value;
                    System.Diagnostics.Debug.WriteLine(date);
                    //System.Diagnostics.Debug.WriteLine(current.IndexOf(date));
                    if (!string.IsNullOrEmpty(date))
                    {
                        bidDecisionDueDate = DateTime.Parse(date);
                        //Console.WriteLine(dateTime.ToString());
                    }
                    System.Diagnostics.Debug.WriteLine("Bid decision due date: {0}", bidDecisionDueDate);
                    //bidDecisionDueDate = DateTime.Parse(current.Substring(start.Length + 1));
                }

                else if (current.StartsWith("RFP number:"))
                {
                    start = "RFP number:";
                    RFPNumber = current.Substring(start.Length + 1);
                }
            }
            System.Diagnostics.Debug.WriteLine("Bid decision due date: {0}", bidDecisionDueDate);
            inscmd.Parameters.AddWithValue("@EGOSID", EGOSID ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@taskOrderName", taskOrderName ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@requestType", requestType ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@taskOrderState", taskOrderState ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@COR", COR ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@submitStatus", submitStatus ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@bidDecision", bidDecision ?? (object)DBNull.Value);
            inscmd.Parameters.AddWithValue("@bidDecisionDueDate", bidDecisionDueDate);
            inscmd.Parameters.AddWithValue("@governmentCust", governmentCust ?? (object)DBNull.Value);
            //inscmd.Parameters["@attachments"].Value = bytes ?? null;
            inscmd.Parameters.AddWithValue("@attachments", bytes ?? SqlBinary.Null);
            //inscmd.Parameters.Add("@attachments", bytes);
            //inscmd.Parameters.AddWithValue("@attachments", attachments ?? (object)DBNull.Value);
            inscmd.ExecuteNonQuery();
        }

        static void parseEmail()
        {
            try
            {
                string localInbox = "mails";

                // Create local folder to store email containing task-order details
                if(!Directory.Exists(localInbox))
                {
                    Directory.CreateDirectory(localInbox);
                }

                //MailServer localServer = new MailServer("pop-mail.outlook.com", "AgarwalA@ENLIGHTENED.COM", "Winter2021!", ServerProtocol.Pop3);
                MailServer localServer = new MailServer("imap-mail.outlook.com", "AgarwalA@ENLIGHTENED.COM", "Winter2021!", ServerProtocol.Imap4);


                // Enable SSL/TLS Connection
                localServer.SSLConnection = true;
                localServer.Port = 993;

                MailClient localClient = new MailClient("TryIt");
                localClient.Connect(localServer);

                Imap4Folder folder = FindFolder("NITAAC", localClient.GetFolders());
                if(folder==null)
                {
                    throw new Exception("Folder not found!");
                }

                localClient.SelectFolder(folder);
                System.Diagnostics.Debug.WriteLine("Retrieving Email list...");

                //localClient.GetMailInfosOption = GetMailInfosOptionType.GetCategories;
                //localClient.GetMailInfosParam.Reset();
                //localClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;

                MailInfo[] infos = localClient.GetMailInfos();
                //System.Diagnostics.Debug.WriteLine("\n Total {0} email(s)", infos.Length);

                //Mail firstClient = localClient.GetMail(infos[0]);
                //DateTime firstDate = firstClient.ReceivedDate;
                string bodyText = "";
                SqlConnection con = new SqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                string delquery = "delete from task_order";
                SqlCommand delcmd = new SqlCommand(delquery, con);
                delcmd.ExecuteNonQuery();

                for (int i=0;i<infos.Length;i++)
                {
                    MailInfo info = infos[i];

                    // Recieve Mail from POP3 server
                    Mail localmail = localClient.GetMail(info);
                    System.Diagnostics.Debug.WriteLine("Subject: {0}\r\n", localmail.Subject);
                    bodyText = localmail.TextBody;

                    localmail.DecodeTNEF();

                    Attachment[] atts = localmail.Attachments;

                    /*if (localmail.Subject.Contains("New Task Order") && DateTime.Compare(localmail.ReceivedDate, firstDate) > 0)
                    {
                        //System.Diagnostics.Debug.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",info.Index, info.Size, info.UIDL);
                        //System.Diagnostics.Debug.WriteLine("From: {0}", localmail.From.ToString());
                        //System.Diagnostics.Debug.WriteLine("Subject: {0}", localmail.Subject);
                        bodyText = localmail.TextBody;
                    
                    }*/
                    loadInDB(bodyText, atts);
                    //System.Diagnostics.Debug.WriteLine("From: {0}", localmail.From.ToString());
                    //System.Diagnostics.Debug.WriteLine("Subject: {0}\r\n", localmail.Subject);

                    //string filename = generateFileName(i + 1);
                    //string path = string.Format("{0}\\{1}", localInbox, filename);

                    // Save email to local disk
                    //localmail.SaveAs(path, true);
                }

                //System.Diagnostics.Debug.WriteLine("Contents: {0}", bodyText);

                //System.Diagnostics.Debug.WriteLine("Task Order Name:"+taskOrderName);

                localClient.Quit();
                System.Diagnostics.Debug.WriteLine("Completed!");

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Global.parseEmail();
        }
    }
}
 