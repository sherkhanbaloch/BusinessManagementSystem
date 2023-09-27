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
    public partial class frmProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewProducts();
                LoadCategories();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddProduct();
            ClearData();
            ViewProducts();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string productID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            productID = e.CommandArgument.ToString();

            if (e.CommandName == "cmdEdit")
            {
                Label lblName = (Label)e.Item.FindControl("LabelName");
                Label lblBarCode = (Label)e.Item.FindControl("LabelBarCode");
                Label lblCategory = (Label)e.Item.FindControl("LabelCategory");
                Label lblPurchaseRate = (Label)e.Item.FindControl("LabelPurchaseRate");
                Label lblSaleRate = (Label)e.Item.FindControl("LabelSaleRate");
                Label lblOpeningStock = (Label)e.Item.FindControl("LabelOpeningStock");

                nameTxt2.Text = lblName.Text;
                barcodeTxt2.Text = lblBarCode.Text;
                categoryDDL2.SelectedItem.Text = lblCategory.Text;
                purchaseTxt2.Text = lblPurchaseRate.Text;
                saleTxt2.Text = lblSaleRate.Text;
                openingTxt2.Text = lblOpeningStock.Text;

                string script = "<script> showModal('updateModal'); </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            }
            else if (e.CommandName == "cmdDelete")
            {
                DeleteProduct(productID);
                ViewProducts();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct(productID);
            ClearData();
            ViewProducts();
        }

        // Custom Methods
        public void ViewProducts()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewProducts";
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

        public void AddProduct()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_Products values ((select COUNT(productID)+1 from tbl_Products), @name, @barcode, @categoryid, @purchaserate, @salerate, @openingstock, 1)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@barcode", barcodeTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@categoryid", categoryDDL.SelectedValue);
                cmd.Parameters.AddWithValue("@purchaserate", purchaseTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@salerate", saleTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@openingstock", openingTxt.Text.Trim());

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Product', 'Product Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Product', 'Product Could Not Be Added.', 'error')", true);
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

        public void UpdateProduct(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Products set productName = @name, barCode= @barcode, categoryID = @categoryid, purchaseRate = @purchaserate, saleRate = @saleRate, openingStock =  @openingstock where productID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@barcode", barcodeTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@categoryid", categoryDDL2.SelectedValue);
                cmd.Parameters.AddWithValue("@purchaserate", purchaseTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@salerate", saleTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@openingstock", openingTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Product', 'Product Updated Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Product', 'Product Could Not Be Updated.', 'error')", true);
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

        public void DeleteProduct(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Products set productStatus = 0 where productID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Product', 'Product Deleted Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Product', 'Product Could Not Be Deleted.', 'error')", true);
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
            barcodeTxt.Text = barcodeTxt2.Text = string.Empty;
            categoryDDL.ClearSelection();
            categoryDDL2.ClearSelection();
            purchaseTxt.Text = purchaseTxt2.Text = string.Empty;
            saleTxt.Text = saleTxt2.Text = string.Empty;
            openingTxt.Text = openingTxt2.Text = string.Empty;
        }

        public void LoadCategories()
        {
            MainClass.dbConnection.Open();
            string qry = "select ID, Name from vw_ViewCategories";
            SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            categoryDDL.DataSource = categoryDDL2.DataSource = dt;
            categoryDDL.DataTextField = categoryDDL2.DataTextField = "Name";
            categoryDDL.DataValueField = categoryDDL2.DataValueField = "ID";
            categoryDDL.DataBind();
            categoryDDL2.DataBind();

            MainClass.dbConnection.Close();
        }
    }
}