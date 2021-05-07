using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETOMS
{
    public partial class AddNewContracts : System.Web.UI.Page
    {
        static string connectionstring = @"Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true";
        SqlConnection con = new SqlConnection(connectionstring);
        static string vehicle_name, vehicle_number, partner_name, desc, performance_period;

        static int partner_id, manager_id, response_timeframe;
        static DateTime dateAdded;


        /*DataSet ds1 = null, ds2 = null;
        SqlDataAdapter sda = null;*/


        protected void Page_Load(object sender, EventArgs e)
        {
           /* sda = new SqlDataAdapter("SELECT company_name FROM partners", con);
            ds1 = new DataSet();
            sda.Fill(ds1, "partners");

            sda = new SqlDataAdapter("SELECT Name FROM employee where role='Contract Manager'", con);
            ds2 = new DataSet();
            sda.Fill(ds2, "employee");

            teaming_partner.DataSource = ds1;
            teaming_partner.DataBind();

            contract_manager.DataSource = ds2;
            contract_manager.DataBind();*/
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("\n Inside submit button clicked");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            string insquery = "INSERT INTO contract_vehicle (vehicle_name, RFP_number, partner_id, partner_name, description, date_added, " +
                "performance_period, eoi_response_timeframe, contract_manager) VALUES" +
                "(@vehicle_name, @vehicle_number, @partner_id, @partner_name, @desc, @dateAdded, " +
                "@performance_period, @response_timeframe, @manager_id)";
            SqlCommand inscmd = new SqlCommand(insquery, con);
            vehicle_name = VehicleName.Text.ToString();
            vehicle_number = VehicleNumber.Text.ToString();
            //TextBox vehicle_desc = (TextBox)Page.FindControl("description");
            //desc = vehicle_desc.Text.ToString();
            desc = description.Value;
            System.Diagnostics.Debug.WriteLine("\n Description value:"+desc);
            dateAdded = DateTime.Parse(DateAdded.Text.ToString());
            performance_period = PerformancePeriod.Text.ToString();
            response_timeframe = int.Parse(ResponseTime.Text.ToString());

            foreach (ListItem listItem in contract_manager.Items)
            {
                if (listItem.Selected)
                {
                    string manager = listItem.Value.ToString();
                    String getManagerId = "select id from employee where Name='" + manager + "'";
                    SqlCommand sqlCmd = new SqlCommand(getManagerId, con);
                    SqlDataReader dr;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    dr = sqlCmd.ExecuteReader();
                    if(dr.Read())
                    {
                        manager_id = int.Parse(dr["id"].ToString());
                    }
                    break;
                }
            }

            foreach (ListItem listItem in teaming_partner.Items)
            {
                if (listItem.Selected)
                {
                    partner_name = listItem.Text.ToString();
                    String getPartnerId = "select partner_id from partners where company_name='" + partner_name + "'";
                    SqlCommand sqlCmd = new SqlCommand(getPartnerId, con);
                    SqlDataReader dr;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        partner_id = int.Parse(dr["partner_id"].ToString());
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        //inscmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
                        inscmd.Parameters.AddWithValue("@vehicle_name", vehicle_name);
                        inscmd.Parameters.AddWithValue("@vehicle_number", vehicle_number);
                        inscmd.Parameters.AddWithValue("@partner_id", partner_id);
                        inscmd.Parameters.AddWithValue("@partner_name", partner_name);
                        inscmd.Parameters.AddWithValue("@desc", desc);
                        inscmd.Parameters.AddWithValue("@dateAdded", dateAdded);
                        inscmd.Parameters.AddWithValue("@performance_period", performance_period);
                        if(response_timeframe == 0)
                        {
                            response_timeframe = 24;
                        }
                        inscmd.Parameters.AddWithValue("@response_timeframe", response_timeframe);
                        inscmd.Parameters.AddWithValue("@manager_id", manager_id);
                        inscmd.ExecuteNonQuery();
                        inscmd.Parameters.Clear();
                        System.Diagnostics.Debug.WriteLine("\n Data inserted successfully");
                        con.Close();
                    }
                }
            }
        }
    }
}