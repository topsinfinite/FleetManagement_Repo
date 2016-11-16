using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.BackOffice
{
    public partial class TrackerCompanySetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        private void BindGrid()
        {
            gvVehicleM.DataSource = LookUpBLL.GetTrackerCoyList();
            gvVehicleM.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    TrackerCompany coy = null; bool rst = false;
                    coy = LookUpBLL.GetTrackerCompany(Convert.ToInt32(txtID.Text));
                    if (coy != null)
                    {
                        coy.Name = txtDept.Text.ToUpper(); ;
                        rst = LookUpBLL.UpdateTrackerCompany(coy);
                        if (rst != false)
                        {
                            BindGrid();
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            return;
                        }
                    }

                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>Record could Not updated. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
                else
                {
                    string com = "";
                    com = txtDept.Text;

                    bool result = false;
                    TrackerCompany coy = new TrackerCompany();
                    coy.Name = com.ToUpper();
                    result = LookUpBLL.AddTrackerCompany(coy);
                    if (result)
                    {
                        BindGrid();
                        txtDept.Text = "";

                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record added successfully!!.";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>Record could Not added. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void gvVehicleM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvVehicleM.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            {
            }
        }

        protected void gvVehicleM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvID.Visible = true;
                dvMsg.InnerText = "Update Tracker Company :";
                btnSubmit.Text = "Update";

                GridViewRow row = gvVehicleM.SelectedRow;
                txtID.Text = row.Cells[0].Text;
                txtDept.Text = row.Cells[1].Text.ToUpper();

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
    }
}