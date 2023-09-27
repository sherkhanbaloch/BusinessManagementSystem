using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusinessManagementSystem
{
    public partial class frmCashbook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewCashbook();
            }
        }

        public void ViewCashbook()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select ROW_NUMBER() OVER (ORDER BY [Transaction ID]) 'Sr No', * from vw_Cashbook where Debit > 0 or Credit > 0 order by Date asc";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Exception','" + ex.Message + "','error')", true);
            }
            finally
            {
                MainClass.dbConnection.Close();
            }
        }
    }
}