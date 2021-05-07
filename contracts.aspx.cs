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
    public partial class contracts : System.Web.UI.Page
    {
        static string connectionstring = @"Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true";
        SqlConnection con = new SqlConnection(connectionstring);
        DataSet ds1 = null, ds2=null;
        SqlDataAdapter sda = null;
        ListBox teaming_partner = null, contract_managers = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                sda = new SqlDataAdapter("SELECT company_name FROM partners", con);
                ds1 = new DataSet();
                sda.Fill(ds1, "partners");

                sda = new SqlDataAdapter("SELECT Name FROM employee where role='Contract Manager'", con);
                ds2 = new DataSet();
                sda.Fill(ds2, "employee");

                foreach (GridViewRow row in grid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        teaming_partner = (ListBox)(row.FindControl("teaming_partner"));
                        teaming_partner.DataSource = ds1;
                        teaming_partner.DataBind();

                        contract_managers = (ListBox)(row.FindControl("contract_manager"));
                        contract_managers.DataSource = ds2;
                        contract_managers.DataBind();
                    }
                }
            }
        }

        protected void grid1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grid1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "edit")
            {
                // Retrieve the row index stored in the CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button from the Rows collection.
                GridViewRow row = grid1.Rows[index];

                // Add code here
                int vehicle_id = int.Parse(row.Cells[0].Text.ToString());
                string vehicle_name = row.Cells[1].Text.ToString();
                string vehicle_number = row.Cells[2].Text.ToString();
                string partner_name = "", contract_manager="";
                teaming_partner = (ListBox)row.Cells[3].FindControl("teaming_partner");
                int partner_id;
                string description = row.Cells[4].Text.ToString();
                DateTime date_added = DateTime.Parse(row.Cells[5].Text.ToString());
                string performance_period = row.Cells[6].Text.ToString();
                int response_timeframe = int.Parse(row.Cells[7].Text.ToString());
                foreach (ListItem listItem in contract_managers.Items)
                {
                    if (listItem.Selected)
                    {
                        contract_manager = contract_manager + listItem.Value.ToString();
                        break;
                    }
                }

                foreach (ListItem listItem in teaming_partner.Items)
                {
                    if(listItem.Selected)
                    {
                        partner_name = listItem.Value.ToString();
                        String getPartnerId = "select partner_id from partners where partner_name='" + partner_name + "'";
                        SqlCommand sqlCmd = new SqlCommand(getPartnerId, con);
                        SqlDataReader dr;
                        dr = sqlCmd.ExecuteReader();

                        if (dr.Read())
                        {
                            partner_id = int.Parse(dr["partner_id"].ToString());
                            SqlCommand cmd = new SqlCommand("update contract_vehicle set vehicle_name=@vehicle_name, RFP_number=@vehicle_number, partner_id = @partner_id, " +
                                "partner_name = @partner_name, description = @description, performance_period = @performance_period, eoi_response_timeframe = @response_timeframe, " +
                                "contract_manager = @contract_manager where vehicle_id=@vehicle_id", con);
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
                            cmd.Parameters.AddWithValue("@vehicle_name", vehicle_name);
                            cmd.Parameters.AddWithValue("@vehicle_number", vehicle_number);
                            cmd.Parameters.AddWithValue("@partner_id", partner_id);
                            cmd.Parameters.AddWithValue("@partner_name", partner_name);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@performance_period", performance_period);
                            cmd.Parameters.AddWithValue("@response_timeframe", response_timeframe);
                            cmd.Parameters.AddWithValue("@contract_manager", contract_manager);
                            cmd.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine("\n Data updated successfully");
                            con.Close();
                        }
                    }
                }
                //partner_name.Trim();
                // int partner_id = "select username,password from employee where username='" + UserName.Text + "'";
                /*SqlCommand sqlCmd = new SqlCommand(getCred, con);
                SqlDataReader dr;

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                dr = sqlCmd.ExecuteReader();*/

            }
        }

    }
}