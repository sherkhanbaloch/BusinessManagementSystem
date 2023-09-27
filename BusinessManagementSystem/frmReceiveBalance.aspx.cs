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
    public partial class frmReceiveBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewCustomerPayment();
                LoadCustomers();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddPayment();
            ClearData();
            ViewCustomerPayment();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string customerID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //customerID = e.CommandArgument.ToString();

            //if (e.CommandName == "cmdEdit")
            //{
            //    // Getting Data From Data Table of Repeater
            //    Label lblID = (Label)e.Item.FindControl("LabelID");
            //    Label lblName = (Label)e.Item.FindControl("LabelName");
            //    Label lblPhone = (Label)e.Item.FindControl("LabelPhone");
            //    Label lblAddress = (Label)e.Item.FindControl("LabelAddress");
            //    Label lblOpeningBalance = (Label)e.Item.FindControl("LabelOpeningBalance");
            //    Label lblMaxCredit = (Label)e.Item.FindControl("LabelMaxCredit");

            //    // Setting That Data Into Text Boxes of Update Modal
            //    nameTxt2.Text = lblName.Text;
            //    phoneTxt2.Text = lblPhone.Text;
            //    addressTxt2.Text = lblAddress.Text;
            //    openingBalanceTxt2.Text = lblOpeningBalance.Text;
            //    maxCreditTxt2.Text = lblMaxCredit.Text;

            //    // Showing Update Modal
            //    string script = "<script> showModal('updateModal'); </script>";
            //    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            //}
            //else if (e.CommandName == "cmdDelete")
            //{
            //    DeleteCustomer(customerID);
            //    ViewCustomers();
            //}
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //UpdateCustomer(customerID);
            //ClearData();
            //ViewCustomers();
        }

        // Custom Methods
        public void ViewCustomerPayment()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewCustomerPayment";
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

        public void LoadCustomers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select ID, Name from vw_CustomersRemaining";
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

        public void AddPayment()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_ReceiveBalance values ((select COUNT(receivedID)+1 from tbl_ReceiveBalance), @customerid, @date, @previous, @received, @description, @method)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@customerid", customerDDL.SelectedValue);
                cmd.Parameters.AddWithValue("@date", dateTxt.Text);
                cmd.Parameters.AddWithValue("@previous", previousTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@received", receivedTxt.Text.Trim());
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
            customerDDL.ClearSelection();
            customerDDL2.ClearSelection();
            dateTxt.Text = dateTxt2.Text = string.Empty;
            previousTxt.Text = previousTxt2.Text = string.Empty;
            receivedTxt.Text = receivedTxt2.Text = string.Empty;
            descriptionTxt.Text = descriptionTxt.Text = string.Empty;
        }

        protected void customerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select [Remaining Balance] from vw_CustomersRemaining where ID = @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", customerDDL.SelectedValue);
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
    }
}