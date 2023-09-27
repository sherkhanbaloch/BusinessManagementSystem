using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusinessManagementSystem
{
    public partial class frmUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewUsers();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string userID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            userID = e.CommandArgument.ToString();

            if (e.CommandName == "cmdEdit")
            {
                Label lblFullName = (Label)e.Item.FindControl("LabelFullName");
                Label lblUserName = (Label)e.Item.FindControl("LabelUserName");
                Label lblRole = (Label)e.Item.FindControl("LabelRole");
                Label lblPassword = (Label)e.Item.FindControl("LabelPassword");
                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("Image1");

                fullNameTxt2.Text = lblFullName.Text;
                userNameTxt2.Text = lblUserName.Text;
                roleDDL2.SelectedItem.Text = lblRole.Text;
                passwordTxt2.Text = lblPassword.Text;
                ImageBox2.ImageUrl = img.ImageUrl;

                string script = "<script> showModal('updateModal'); </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script);
            }
            else if (e.CommandName == "cmdDelete")
            {
                DeleteUser(userID);
                ViewUsers();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUser(userID);
            ClearData();
            ViewUsers();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddUser();
            ClearData();
            ViewUsers();
        }

        // Custom Methods
        public void ViewUsers()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_ViewUsers";
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

        public void AddUser()
        {
            try
            {
                string filePath = Server.MapPath("Images/Users/");
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string extension = Path.GetExtension(fileName);
                HttpPostedFile postedFile = FileUpload1.PostedFile;
                int size = postedFile.ContentLength;

                if (FileUpload1.HasFile == true)
                {
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        if (size <= 5000000)
                        {
                            //FileUpload1.SaveAs(filePath + fileName);
                            //string imagePath = "Images/Users/" + fileName;

                            // Saving Images With User Name
                            FileUpload1.SaveAs(filePath + userNameTxt.Text + extension);
                            string imagePath = "Images/Users/" + userNameTxt.Text + extension;

                            MainClass.dbConnection.Open();
                            string qry = "insert into tbl_Users values ((select COUNT(userID)+1 from tbl_Users), @name, @username, @role, @password, @imageURL, 1)";
                            SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                            cmd.Parameters.AddWithValue("@name", fullNameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@username", userNameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@role", roleDDL.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@password", passwordTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@imageURL", imagePath);

                            int a = cmd.ExecuteNonQuery();

                            if (a > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save User', 'User Added Successfully.', 'success')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Save User', 'User Could Not Be Added.', 'error')", true);
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Image Should Not Be More Than 5 MB.";
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Image Format Is Not Supported.";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Please Upload Image.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
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

        public void UpdateUser(string id)
        {
            try
            {
                string filePath = Server.MapPath("Images/Users/");
                string fileName = Path.GetFileName(FileUpload2.FileName);
                string extension = Path.GetExtension(fileName);
                HttpPostedFile postedFile = FileUpload2.PostedFile;
                int size = postedFile.ContentLength;

                string updatePath = "Images/Users/";

                if (FileUpload2.HasFile == true)
                {
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        if (size <= 5000000)
                        {
                            updatePath = "Images/Users/" + userNameTxt2.Text + extension;
                            FileUpload2.SaveAs(Server.MapPath(updatePath));

                            MainClass.dbConnection.Open();
                            string qry = "update tbl_Users set fullName = @name, userName = @username, userRole = @role, userPassword = @password, userImageURL = @imageURL where userID = @id";
                            SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                            cmd.Parameters.AddWithValue("@name", fullNameTxt2.Text.Trim());
                            cmd.Parameters.AddWithValue("@username", userNameTxt2.Text.Trim());
                            cmd.Parameters.AddWithValue("@role", roleDDL2.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@password", passwordTxt2.Text.Trim());
                            cmd.Parameters.AddWithValue("@imageURL", updatePath);
                            cmd.Parameters.AddWithValue("@id", id);

                            int a = cmd.ExecuteNonQuery();

                            if (a > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update User', 'User Updated Successfully.', 'success')", true);

                                string DeletePath = Server.MapPath(ImageBox2.ImageUrl.ToString());

                                if (File.Exists(DeletePath) == true)
                                {
                                    File.Delete(DeletePath);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update User', 'User Could Not Be Updated.', 'error')", true);
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Image Should Not Be More Than 5 MB.";
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Image Format Is Not Supported.";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    updatePath = ImageBox2.ImageUrl.ToString();

                    MainClass.dbConnection.Open();
                    string qry = "update tbl_Users set fullName = @name, userName = @username, userRole = @role, userPassword = @password, userImageURL = @imageURL where userID = @id";
                    SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                    cmd.Parameters.AddWithValue("@name", fullNameTxt2.Text.Trim());
                    cmd.Parameters.AddWithValue("@username", userNameTxt2.Text.Trim());
                    cmd.Parameters.AddWithValue("@role", roleDDL2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@password", passwordTxt2.Text.Trim());
                    cmd.Parameters.AddWithValue("@imageURL", updatePath);
                    cmd.Parameters.AddWithValue("@id", id);

                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update User', 'User Updated Successfully.', 'success')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Update User', 'User Could Not Be Updated.', 'error')", true);
                    }
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

        public void DeleteUser(string id)
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "update tbl_Users set userStatus = 0 where userID = @id";
                SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
                cmd.Parameters.AddWithValue("@id", id);

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete User', 'User Deleted Successfully.', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Swal.fire('Delete User', 'User Could Not Be Deleted.', 'error')", true);
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
            fullNameTxt.Text = fullNameTxt2.Text = string.Empty;
            userNameTxt.Text = userNameTxt2.Text = string.Empty;
            roleDDL.ClearSelection();
            roleDDL2.ClearSelection();
            passwordTxt.Text = passwordTxt2.Text = string.Empty;
        }
    }
}