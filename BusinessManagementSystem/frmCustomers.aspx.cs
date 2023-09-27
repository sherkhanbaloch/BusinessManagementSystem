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
    public partial class frmCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewCustomers();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddCustomer();
            ClearData();
            ViewCustomers();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string customerID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            customerID = e.CommandArgument.ToString();

            if (e.CommandName == "cmdEdit")
            {
                // Getting Data From Data Table of Repeater
                Label lblID = (Label)e.Item.FindControl("LabelID");
                Label lblName = (Label)e.Item.FindControl("LabelName");
                Label lblPhone = (Label)e.Item.FindControl("LabelPhone");
                Label lblAddress = (Label)e.Item.FindControl("LabelAddress");
                Label lblOpeningBalance = (Label)e.Item.FindControl("LabelOpeningBalance");
                Label lblMaxCredit = (Label)e.Item.FindControl("LabelMaxCredit");

                // Setting That Data Into Text Boxes of Update Modal
                nameTxt2.Text = lblName.Text;
                phoneTxt2.Text = lblPhone.Text;
                addressTxt2.Text = lblAddress.Text;
                openingBalanceTxt2.Text = lblOpeningBalance.Text;
                maxCreditTxt2.Text = lblMaxCredit.Text;

                // Showing Update Modal
                string script = "<script> showModal('updateModal'); </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            }
            else if (e.CommandName == "cmdDelete")
            {
                DeleteCustomer(customerID);
                ViewCustomers();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomer(customerID);
            ClearData();
            ViewCustomers();
        }

        // Custom Methods
        public void ViewCustomers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewCustomers";
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

        public void AddCustomer()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_customers values ((select COUNT(customerID)+1 from tbl_Customers), @name, @phone, @address, @openingbalance, @maxcredit, 1)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", phoneTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@address", addressTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@openingbalance", openingTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@maxcredit", maxcreditTxt.Text.Trim());

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Customer', 'Customer Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Customer', 'Customer Could Not Be Added.', 'error')", true);
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

        public void UpdateCustomer(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Customers set customerName = @name, customerPhone = @phone, customerAddress = @address, openingBalance = @openingbalance, maxCredit = @maxcredit where customerID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", phoneTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@address", addressTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@openingbalance", openingBalanceTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@maxcredit", maxCreditTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Customer', 'Customer Updated Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Customer', 'Customer Could Not Be Updated.', 'error')", true);
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

        public void DeleteCustomer(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_customers set customerStatus = 0 where customerID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Customer', 'Customer Deleted Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Customer', 'Customer Could Not Be Deleted.', 'error')", true);
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
            nameTxt.Text = nameTxt2.Text = string.Empty;
            phoneTxt.Text = phoneTxt2.Text = string.Empty;
            addressTxt.Text = addressTxt2.Text = string.Empty;
            openingTxt.Text = openingBalanceTxt2.Text = string.Empty;
            maxcreditTxt.Text = maxCreditTxt2.Text = string.Empty;
        }
    }
}