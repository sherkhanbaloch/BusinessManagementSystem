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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ViewCounters();
            }
        }

        // Custom Methods
        public void ViewCounters()
        {
            try
            {
                MainClass.dbConnection.Open();
                string qry = "select * from vw_Counters";
                SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblCustomers.Text = "<h3>" + dt.Rows[0][0].ToString() + "</h3>";
                lblProducts.Text = "<h3>" + dt.Rows[1][0].ToString() + "</h3>";
                lblSuppliers.Text = "<h3>" + dt.Rows[2][0].ToString() + "</h3>";
                lblUsers.Text = "<h3>" + dt.Rows[3][0].ToString() + "</h3>";

                lblSale.Text = lblRevenue.Text = dt.Rows[4][0].ToString();
                lblPurchase.Text = lblCost.Text = dt.Rows[5][0].ToString();
                lblReceived.Text = dt.Rows[6][0].ToString();
                lblPaid.Text = dt.Rows[7][0].ToString();

                if (Convert.ToInt32(lblRevenue.Text) > Convert.ToInt32(lblCost.Text))
                {
                    int profit = Convert.ToInt32(lblRevenue.Text) - Convert.ToInt32(lblCost.Text);
                    lblProfit.Text = profit.ToString();
                    lblLoss.Text = "0";
                }
                else if (Convert.ToInt32(lblCost.Text) > Convert.ToInt32(lblRevenue.Text))
                {
                    int loss = Convert.ToInt32(lblCost.Text) - Convert.ToInt32(lblRevenue.Text);
                    lblLoss.Text = loss.ToString();
                    lblProfit.Text = "0";
                }

                lblCustomerRemaining.Text = dt.Rows[8][0].ToString();
                lblSupplierRemaining.Text = dt.Rows[9][0].ToString();

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