using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Configuration;
using System.Globalization;

namespace FleetMgtSolution.BackOffice
{
    public partial class ManageIncidence : System.Web.UI.Page
    {
        private string vehMaintenanceID = ConfigurationManager.AppSettings["VehMaintenance_IncidentID"].ToString();
        CultureInfo culture = new CultureInfo("en-GB");
        private User usr = null;
        protected void Page_Load(object sender, EventArgs e)
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
            try
            {
                dvmainTenance.Visible = false;
                 if (ddlIncType.SelectedValue == vehMaintenanceID)
                 {
                     dvmainTenance.Visible=true;
                 }
                if (!IsPostBack)
                {
                    BindVehicleList();
                    BindDrivers();
                    BindIncidentList();
                    BindGrid();
                }
            }catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        private void BindGrid()
        {
            usr = (User)Session["user"];
            gvIncidence.DataSource = IncidenceBLL.GetIncidenceLogList(usr.LocationID.Value);
            gvIncidence.DataBind();
        }
        private void BindIncidentList()
        {
            ddlIncType.DataTextField = "Name";
            ddlIncType.DataValueField = "ID";
            ddlIncType.DataSource = LookUpBLL.GetIncidenceTypeList();
            ddlIncType.DataBind();
        }
        private void BindVehicleList()
        {
            usr = (User)Session["user"];
            ddlVeh.DataTextField = "Name";
            ddlVeh.DataValueField = "ID";
            ddlVeh.DataSource = VehicleBLL.GetVehicleLookUpList(usr.LocationID.Value);
            ddlVeh.DataBind();
        }
        private void BindDrivers()
        {
            usr = (User)Session["user"];
            ddlDriver.DataTextField = "Name";
            ddlDriver.DataValueField = "ID";
            ddlDriver.DataSource = DriverBLL.GetDriversList(usr.LocationID.Value);
            ddlDriver.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    IncidentLog incident = null; bool rst = false;
                    incident = IncidenceBLL.GetIncidence(Convert.ToInt32(txtID.Text));
                    if (incident != null)
                    {

                        incident.Note = txtDesc.Text;
                        incident.IncidentTypeID = Convert.ToInt32(ddlIncType.SelectedValue);
                        incident.AssociatedDriver = Convert.ToInt32(ddlDriver.SelectedValue);
                        incident.AssociatedVehicle = Convert.ToInt32(ddlVeh.SelectedValue);
                        incident.LastModifiedDate = DateTime.Now;
                        incident.ModifiedBy = User.Identity.Name;
                        if (ddlIncType.SelectedValue == vehMaintenanceID)
                        {
                            incident.VehMaintenanceDate = DateTime.Parse(txtDate.Text, culture);
                             decimal mileAtSer=0;
                             if(!decimal.TryParse(txtMile.Text,out mileAtSer))
                             {
                                 error.Visible=true;
                                 error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Mileage must be numeric";
                               
                                 return;
                             }
                             incident.MileageAtMaintenance = mileAtSer;
                             Vehicle veh = VehicleBLL.GetVehicle(int.Parse(ddlVeh.SelectedValue));
                             if (veh != null)
                             {
                                 veh.LastServiceDate = DateTime.Parse(txtDate.Text, culture);
                                 veh.MileageAtLastService = mileAtSer;
                                 VehicleBLL.UpdateVehicle(veh);
                             }
                        }
                        rst = IncidenceBLL.UpdateIncidenceLog(incident);
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
                    IncidentLog incident = new IncidentLog();
                      if (ddlIncType.SelectedValue == vehMaintenanceID)
                         {
                             decimal mileAtSer=0;
                             if(!decimal.TryParse(txtMile.Text,out mileAtSer))
                             {
                                 error.Visible=true;
                                 error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Mileage must be numeric";
                               
                                 return;
                             }
                             incident.VehMaintenanceDate = DateTime.Parse(txtDate.Text, culture);
                             incident.MileageAtMaintenance = mileAtSer;
                             Vehicle veh = VehicleBLL.GetVehicle(int.Parse(ddlVeh.SelectedValue));
                             if (veh != null)
                                 {
                                    veh.LastServiceDate = DateTime.Parse(txtDate.Text, culture);
                                    veh.MileageAtLastService = mileAtSer;
                                    veh.Mileage = mileAtSer;
                                    VehicleBLL.UpdateVehicle(veh);
                                 }
                         }
                    bool result = false;
                   
                    incident.Note = txtDesc.Text;
                    incident.IncidentTypeID = Convert.ToInt32(ddlIncType.SelectedValue);
                    incident.AssociatedDriver = Convert.ToInt32(ddlDriver.SelectedValue);
                    incident.AssociatedVehicle = Convert.ToInt32(ddlVeh.SelectedValue);
                    incident.DateAdded = DateTime.Now;
                    incident.AddedBy = User.Identity.Name;
                    result = IncidenceBLL.AddIncidence(incident);
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
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void gvIncidence_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvIncidence.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            {
            }
        }

        protected void gvIncidence_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

            }
            catch
            {
            }
        }

        protected void gvIncidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string format = "dd/MM/yyyy";
                hid.Value = "Update";
                dvID.Visible = true;
                dvMsg.InnerText = "Update Incident's Information :";
                btnSubmit.Text = "Update";
                int Id = Convert.ToInt32(gvIncidence.SelectedDataKey[0].ToString());
                GridViewRow row = gvIncidence.SelectedRow;
                txtID.Text = Id.ToString();
                IncidentLog log = IncidenceBLL.GetIncidence(Id);
                if (log != null)
                {
                   // txtDesc.Text = row.Cells[4].Text;
                    ddlVeh.SelectedValue = log.AssociatedVehicle.Value.ToString();
                    ddlIncType.SelectedValue = log.IncidentTypeID.Value.ToString();
                    ddlDriver.SelectedValue = log.AssociatedDriver.Value.ToString();
                    txtDesc.Text = log.Note;
                    if (ddlIncType.SelectedValue == vehMaintenanceID)
                    {
                        dvmainTenance.Visible = true;
                        txtDate.Text = log.VehMaintenanceDate.HasValue ? DateTime.Parse(log.VehMaintenanceDate.Value.ToString()).ToString(format) : "";
                        txtMile.Text = log.MileageAtMaintenance.HasValue ? log.MileageAtMaintenance.Value.ToString() : "";

                    }
                }
            }catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        protected void Reset()
        {
            ddlVeh.SelectedValue = "";
            ddlIncType.SelectedValue = "";
            ddlDriver.SelectedValue = "";
            txtDesc.Text = "";
        }

        protected void ddlIncType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIncType.SelectedValue == vehMaintenanceID)
                {
                    dvmainTenance.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}