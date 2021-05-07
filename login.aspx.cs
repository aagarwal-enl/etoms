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
    public partial class login : System.Web.UI.Page
    {
        static string connectionstring = @"Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true";
        SqlConnection con = new SqlConnection(connectionstring);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String getCred = "select username,password from employee where username='" + UserName.Text +"'";
            SqlCommand sqlCmd = new SqlCommand(getCred, con);
            SqlDataReader dr;

            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }
            dr = sqlCmd.ExecuteReader();

            if(dr.Read())
            {
                if (dr["password"].ToString().Trim() == Password.Text.Trim())
                {
                    System.Diagnostics.Debug.WriteLine("\n Login successful");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("\n Login unsuccessful");
                }
            }
            
        }
    }
}