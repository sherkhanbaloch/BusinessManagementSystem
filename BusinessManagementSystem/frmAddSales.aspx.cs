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
    public partial class frmAddSales : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                InvoiceID();
                LoadCustomers();
                LoadProducts();

                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Purchase Rate", typeof(int));
                dt.Columns.Add("Sale Rate", typeof(int));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Amount", typeof(int));

                ViewState["table"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        // Custom Methods
        public void InvoiceID()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select COUNT(saleID)+1 from tbl_Sales";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                invoiceIDTxt.Text = cmd.ExecuteScalar().ToString();
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
                string qry = "select ID, Name from vw_ViewCustomers";
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

        public void LoadProducts()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select ID, Name from vw_ViewProducts";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                productDDL.DataSource = dt;
                productDDL.DataTextField = "Name";
                productDDL.DataValueField = "ID";
                productDDL.DataBind();
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

        public void SubTotal()
        {
            int sum = 0;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(GridView1.Rows[i].Cells[6].Text);
            }
            subtotalTxt.Text = totalTxt.Text = sum.ToString();
        }

        public void SaveRecord()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_Sales values (@saleid, @customerid, @date, @discount, @netamount, @cash, @description)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@saleid", invoiceIDTxt.Text);
                cmd.Parameters.AddWithValue("@customerid", customerDDL.SelectedValue);
                cmd.Parameters.AddWithValue("@date", dateTxt.Text);
                cmd.Parameters.AddWithValue("@discount", discountTxt.Text);
                cmd.Parameters.AddWithValue("@netamount", totalTxt.Text);
                cmd.Parameters.AddWithValue("@cash", cashTxt.Text);
                cmd.Parameters.AddWithValue("@description", descriptionTxt.Text);

                int a = cmd.ExecuteNonQuery();
                int b = 0;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string qry2 = "insert into tbl_SalesDetails values (@saleid, @productID, @purchase, @sale, @qty)";
                    SqlCommand cmd2 = new SqlCommand(qry2, MainClass.dbConnection);
                    cmd2.Parameters.AddWithValue("@saleid", invoiceIDTxt.Text);
                    cmd2.Parameters.AddWithValue("@productID", GridView1.Rows[i].Cells[0].Text);
                    cmd2.Parameters.AddWithValue("@purchase", GridView1.Rows[i].Cells[3].Text);
                    cmd2.Parameters.AddWithValue("@sale", GridView1.Rows[i].Cells[4].Text);
                    cmd2.Parameters.AddWithValue("@qty", GridView1.Rows[i].Cells[5].Text);

                    b = cmd2.ExecuteNonQuery();
                }

                if (a > 0 && b > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Invoice', 'Invoice Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Invoice', 'Invoice Could Not Be Added.', 'error')", true);
                }
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
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_CustomersRemaining where ID = @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", customerDDL.SelectedValue);
                DataTable dt = new DataTable();
                da.Fill(dt);

                phoneTxt.Text = dt.Rows[0]["Phone"].ToString();
                addressTxt.Text = dt.Rows[0]["Address"].ToString();
                maxcreditTxt.Text = dt.Rows[0]["Max Credit"].ToString();
                balanceTxt.Text = dt.Rows[0]["Remaining Balance"].ToString();

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

        protected void productDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewStock where ID = @id";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                da.SelectCommand.Parameters.AddWithValue("@id", productDDL.SelectedValue);
                DataTable dt = new DataTable();
                da.Fill(dt);

                barcodeTxt.Text = dt.Rows[0]["Bar Code"].ToString();
                categoryTxt.Text = dt.Rows[0]["Category"].ToString();
                prateTxt.Text = dt.Rows[0]["Purchase Rate"].ToString();
                srateTxt.Text = dt.Rows[0]["Sale Rate"].ToString();
                stockTxt.Text = dt.Rows[0]["Available Stock"].ToString();
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

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["table"];

            dt.Rows.Add(productDDL.SelectedValue, productDDL.SelectedItem.Text, categoryTxt.Text, prateTxt.Text, srateTxt.Text, quantityTxt.Text, (Convert.ToInt32(srateTxt.Text) * Convert.ToInt32(quantityTxt.Text)));

            ViewState["table"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            SubTotal();
        }

        protected void discountTxt_TextChanged(object sender, EventArgs e)
        {
            if (discountTxt.Text != "")
            {
                int total = Convert.ToInt32(subtotalTxt.Text) - Convert.ToInt32(discountTxt.Text);
                totalTxt.Text = total.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
    }
}