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
    public partial class frmCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewCategories();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddCategory();
            ClearData();
            ViewCategories();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string categoryID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            categoryID = e.CommandArgument.ToString();

            if (e.CommandName == "cmdEdit")
            {
                Label lblName = (Label)e.Item.FindControl("LabelName");

                nameTxt2.Text = lblName.Text;

                string script = "<script> showModal('updateModal'); </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            }
            else if (e.CommandName == "cmdDelete")
            {
                DeleteCategory(categoryID);
                ViewCategories();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCategory(categoryID);
            ClearData();
            ViewCategories();
        }

        public void ViewCategories()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewCategories";
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

        public void AddCategory()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "insert into tbl_Categories values ((select COUNT(categoryID)+1 from tbl_Categories), @name, 1)";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Category', 'Category Added Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save Category', 'Category Could Not Be Added.', 'error')", true);
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

        public void UpdateCategory(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Categories set categoryName = @name where categoryID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@name", nameTxt2.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Category', 'Category Updated Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update Category', 'Category Could Not Be Updated.', 'error')", true);
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

        public void DeleteCategory(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Categories set categoryStatus = 0 where categoryID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Category', 'Category Deleted Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete Category', 'Category Could Not Be Deleted.', 'error')", true);
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
        }
    }
}