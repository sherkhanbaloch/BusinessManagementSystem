using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace BusinessManagementSystem
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            userLogIn();
        }

        // Custom Methods
        public void userLogIn()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from tbl_Users where userName = @username and userPassword = @password and userStatus = 1";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@username", usernameTxt.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@password", passwordTxt.Text.Trim());
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Session["username"] = dt.Rows[0]["fullName"].ToString();
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Login Failed.', 'Invalid User Name or Password.', 'error')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Exception', '" + ex.Message + "', 'error')", true);
            }
            finally
            {
                MainClass.dbConnection.Close();
            }
        }
    }
}