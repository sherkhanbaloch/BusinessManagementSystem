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
    public partial class frmCustomerLedger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        public void LoadCustomers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select ID, Name from vw_CustomerLedgerBalance";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                customerDDL.DataSource = dt;
                customerDDL.DataTextField = "Name";
                customerDDL.DataValueField = "ID";
                customerDDL.DataBind();
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

        public void FetchLedger()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "exec sp_CustomerLedger @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", customerDDL.SelectedValue);
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

        public void InsertCustomer()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_CustomerLedgerBalance where ID = @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", customerDDL.SelectedValue);
                DataTable dt = new DataTable();
                da.Fill(dt);

                phoneTxt.Text = dt.Rows[0]["Phone"].ToString();
                addressTxt.Text = dt.Rows[0]["Address"].ToString();
                maxCreditTxt.Text = dt.Rows[0]["Max Credit"].ToString();
                balanceTxt.Text = dt.Rows[0]["Balance"].ToString();

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

        protected void customerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertCustomer();
            FetchLedger();
        }
    }
}