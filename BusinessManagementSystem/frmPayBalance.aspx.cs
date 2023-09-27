using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BusinessManagementSystem
{
    public partial class frmPayBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewSuppliersPayment();
                LoadSuppliers();
            }
        }

        // Custom Methods
        public void ViewSuppliersPayment()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewSuppliersPayment";
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

        public void LoadSuppliers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select ID, Name from vw_SuppliersRemaining";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                supplierDDL.DataSource = dt;
                supplierDDL.DataTextField = "Name";
                supplierDDL.DataValueField = "ID";
                supplierDDL.DataBind();
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

        public void AddPayment()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_PayBalance values ((select COUNT(payID)+1 from tbl_PayBalance), @supplierid, @date, @amount, @description, @method)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@supplierid", supplierDDL.SelectedValue);
                cmd.Parameters.AddWithValue("@date", dateTxt.Text);
                cmd.Parameters.AddWithValue("@amount", amountTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@description", descriptionTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@method", methodDDL.Text);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Payment', 'Payment Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Paymentr', 'Payment Could Not Be Added.', 'error')", true);
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

        public void ClearData()
        {
            supplierDDL.ClearSelection();
            supplierDDL.ClearSelection();
            dateTxt.Text = dateTxt2.Text = string.Empty;
            previousTxt.Text = previousTxt2.Text = string.Empty;
            amountTxt.Text = amountTxt2.Text = string.Empty;
            descriptionTxt.Text = descriptionTxt.Text = string.Empty;
        }

        protected void supplierDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select [Remaining Balance] from vw_SuppliersRemaining where ID = @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", supplierDDL.SelectedValue);
                DataTable dt = new DataTable();
                da.Fill(dt);

                previousTxt.Text = dt.Rows[0]["Remaining Balance"].ToString();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddPayment();
            ClearData();
            ViewSuppliersPayment();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
