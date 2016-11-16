using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.BackOffice
{
    public partial class VehicleTypePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
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
            gvVehicleM.DataSource = LookUpBLL.GetVehicleTypeList();
            gvVehicleM.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    VehicleType typ = null; bool rst = false;
                    typ = LookUpBLL.GetVehicleType(Convert.ToInt32(txtID.Text));
                    if (typ != null)
                    {
                        typ.Name = txtDept.Text.ToUpper(); ;
                        rst = LookUpBLL.UpdateVehicleType(typ);
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
                    string typ = "";
                    typ = txtDept.Text;

                    bool result = false;
                    VehicleType type = new VehicleType();
                    type.Name = typ.ToUpper();
                    result = LookUpBLL.AddVehicleType(type);
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
                dvMsg.InnerText = "Update VehicleType :";
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