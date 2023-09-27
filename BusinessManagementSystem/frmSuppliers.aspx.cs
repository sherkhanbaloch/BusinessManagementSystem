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
    public partial class frmSuppliers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewSuppliers();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddSupplier();
            ClearData();
            ViewSuppliers();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string supplierID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            supplierID = e.CommandArgument.ToString();

            if (e.CommandName == "cmdEdit")
            {

                Label lblID = (Label)e.Item.FindControl("LabelID");
                Label lblName = (Label)e.Item.FindControl("LabelName");
                Label lblPhone = (Label)e.Item.FindControl("LabelPhone");
                Label lblAddress = (Label)e.Item.FindControl("LabelAddress");
                Label lblOpeningBalance = (Label)e.Item.FindControl("LabelOpeningBalance");
                Label lblNTN = (Label)e.Item.FindControl("LabelNTN");

                // Setting That Data Into Text Boxes of Update Modal
                nameTxt2.Text = lblName.Text;
                phoneTxt2.Text = lblPhone.Text;
                addressTxt2.Text = lblAddress.Text;
                openingBalanceTxt2.Text = lblOpeningBalance.Text;
                ntnTxt2.Text = lblNTN.Text;

                // Showing Update Modal
                string script = "<script> showModal('updateModal'); </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            }
            else if (e.CommandName == "cmdDelete")
            {
                DeleteSupplier(supplierID);
                ViewSuppliers();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateSupplier(supplierID);
            ClearData();
            ViewSuppliers();
        }

        // Custom Methods
        public void ViewSuppliers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewSuppliers";
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

        public void AddSupplier()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_suppliers values ((select COUNT(supplierID)+1 from tbl_Suppliers), @name, @phone, @address, @ntn, @openingbalance, 1)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", phoneTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@address", addressTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@ntn", ntnTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@openingbalance", openingTxt.Text.Trim());

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Supplier', 'Supplier Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Supplier', 'Supplier Could Not Be Added.', 'error')", true);
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

        public void UpdateSupplier(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Suppliers set supplierName = @name, supplierPhone = @phone, supplierAddress = @address, supplierNTN = @ntn, openingBalance = @openingbalance where supplierID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", phoneTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@address", addressTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@ntn", ntnTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@openingbalance", openingBalanceTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Supplier', 'Supplier Updated Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Supplier', 'Supplier Could Not Be Updated.', 'error')", true);
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

        public void DeleteSupplier(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Suppliers set supplierStatus = 0 where supplierID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Supplier', 'Supplier Deleted Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Supplier', 'Supplier Could Not Be Deleted.', 'error')", true);
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
            ntnTxt.Text = ntnTxt2.Text = string.Empty;
        }
    }
}