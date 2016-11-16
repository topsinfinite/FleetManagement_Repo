using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Globalization;
namespace FleetMgtSolution.BackOffice
{
    public partial class ManageDriver : System.Web.UI.Page
    {
        CultureInfo culture = new CultureInfo("en-GB");
        private User usr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["user"] != null)
                {
                    usr = (User)Session["user"];
                }
                else
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }
                if (!IsPostBack)
                {
                    BindGrid();
                    BindDriverEmployer();
                    BindLocation();
                    //BindVehicleList();
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        private void BindGrid()
        {
            usr = (User)Session["user"];
            gvDriver.DataSource = DriverBLL.GetDriversList(usr.LocationID.Value);
            gvDriver.DataBind();
        }
        private void BindDriverEmployer()
        {
            ddlDriverEmployer.DataTextField = "Name";
            ddlDriverEmployer.DataValueField = "ID";
            ddlDriverEmployer.DataSource = LookUpBLL.GetDriverEmployerList();
            ddlDriverEmployer.DataBind();
        }
        //private void BindVehicleList()
        //{
        //    ddlVeh.DataTextField = "Name";
        //    ddlVeh.DataValueField = "ID";
        //    ddlVeh.DataSource = VehicleBLL.GetVehicleLookUpList();
        //    ddlVeh.DataBind();
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    Driver driver = null; bool rst = false;
                    driver = DriverBLL.GetDriver(Convert.ToInt32(txtID.Text));
                    if (driver != null)
                    {
                        driver.Name = txtName.Text.ToUpper(); 
                        driver.Address=txtAdd.Text;
                        driver.MobileNo = txtMobile.Text;
                        driver.DateEmployed = DateTime.Parse(txtDate.Text, culture);
                        driver.EmployerID = Convert.ToInt32(ddlDriverEmployer.SelectedValue);
                        //driver.VehicleAssociate = Convert.ToInt32(ddlVeh.SelectedValue);
                        driver.LocationID = int.Parse(ddlLocation.SelectedValue);
                        if (chk.Checked)
                        {
                            driver.DelFlg = "Y";
                        }
                        else
                        {
                            driver.DelFlg = "N";
                        }
                        rst = DriverBLL.UpdateDriver(driver);
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
                    
                    bool result = false;
                    Driver driver = new Driver();
                    driver.Name = txtName.Text.ToUpper();
                    driver.Address = txtAdd.Text;
                    driver.MobileNo = txtMobile.Text;
                    driver.DateEmployed = DateTime.Parse(txtDate.Text, culture);
                    driver.EmployerID = Convert.ToInt32(ddlDriverEmployer.SelectedValue);
                    //driver.VehicleAssociate = Convert.ToInt32(ddlVeh.SelectedValue);
                    driver.LocationID = int.Parse(ddlLocation.SelectedValue);
                    result = DriverBLL.AddDriver(driver);
                    driver.DelFlg = "N";
                    if (result)
                    {
                        BindGrid();
                        Reset();
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

        protected void gvDriver_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDriver.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvID.Visible = true;
                dvDel.Visible = true;
                dvMsg.InnerText = "Update Driver's Information :";
                btnSubmit.Text = "Update";
                int Id = Convert.ToInt32(gvDriver.SelectedDataKey[0].ToString());
                GridViewRow row = gvDriver.SelectedRow;
                txtID.Text = Id.ToString();
                txtName.Text = row.Cells[1].Text.ToUpper();
                txtMobile.Text = row.Cells[2].Text.ToUpper(); 
                txtAdd.Text = row.Cells[3].Text.ToUpper(); 
                txtDate.Text = (row.FindControl("lbSdate") as Label).Text;
               // ddlVeh.SelectedValue = (row.FindControl("lbVehId") as Label).Text; ;
                ddlDriverEmployer.SelectedValue = (row.FindControl("lbEmpID") as Label).Text;
                ddlLocation.SelectedValue = (row.FindControl("lbLoc") as Label).Text;
                if ((row.FindControl("lbDelFlg") as Label).Text == "Y")
                {
                    chk.Checked = true;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void Reset()
        {
            txtMobile.Text = ""; txtName.Text = "";
            txtAdd.Text = ""; txtDate.Text = "";
           // ddlVeh.SelectedValue = ""; ddlDriverEmployer.SelectedValue = "";
        }

        protected void gvDriver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Driverview")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    //int key = Convert.ToInt32(gvAsse.DataKeys[index].Value.ToString());
                    Response.Redirect(String.Format("VehicleDetail.aspx?id={0}", index), false);

                }
                
               
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