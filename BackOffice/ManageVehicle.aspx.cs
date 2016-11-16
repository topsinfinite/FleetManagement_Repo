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
    public partial class ManageVehicle : System.Web.UI.Page
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
                    BindDrivers();
                    BindInsuranceCoy();
                    BindTrackerCoy();
                    BindVehicleMaker();
                    BindVehicleType();
                    BindLocation();
                    if (Request.QueryString["stat"] != null)
                    {
                       int sta=int.Parse(Request.QueryString["stat"].ToString());
                        gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(v=>v.Status==sta).ToList();
                        gvVehicle.DataBind();
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
            gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value);
            gvVehicle.DataBind();
        }
        private void BindVehicleMaker()
        {
            ddlMaker.DataTextField = "Name";
            ddlMaker.DataValueField = "ID";
            ddlMaker.DataSource = LookUpBLL.GetVehicleMakerList();
            ddlMaker.DataBind();
        }
        private void BindVehicleType()
        {
            ddlVehType.DataTextField = "Name";
            ddlVehType.DataValueField = "ID";
            ddlVehType.DataSource = LookUpBLL.GetVehicleTypeList();
            ddlVehType.DataBind();
        }
        
        private void BindInsuranceCoy()
        {
            ddlInsur.DataTextField = "Name";
            ddlInsur.DataValueField = "ID";
            ddlInsur.DataSource = LookUpBLL.GetInsuranceCoyList();
            ddlInsur.DataBind();
        }
        private void BindTrackerCoy()
        {
            ddlTracker.DataTextField = "Name";
            ddlTracker.DataValueField = "ID";
            ddlTracker.DataSource = LookUpBLL.GetTrackerCoyList();
            ddlTracker.DataBind();
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
                decimal milage = 0; int capacity;
                if(!int.TryParse(txtCapacity.Text, out capacity))
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Wrong input.Capacity must be integer";
                    return;
                }
                if (!decimal.TryParse(txtMilage.Text, out milage))
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Wrong input.Mileage must be decimal";
                    return;
                }
                if (hid.Value == "Update")
                {
                    Vehicle veh = null; bool rst = false;
                    veh = VehicleBLL.GetVehicle(Convert.ToInt32(txtID.Text));
                    if (veh != null)
                    {
                        veh.Name = txtName.Text.ToUpper();
                        veh.AssociatedDriver = Convert.ToInt32(ddlDriver.SelectedValue);
                        veh.Capacity = capacity;
                        veh.InsuranceCompany = Convert.ToInt32(ddlInsur.SelectedValue);
                        veh.InsuranceExprDate = DateTime.Parse(txtIDate.Text, culture);
                        veh.InsuranceNo = txtInsur.Text;
                        veh.LicenseExprDate = DateTime.Parse(txtLDate.Text, culture);
                        veh.LicenseNo = txtLicense.Text;
                        veh.MarkerID = Convert.ToInt32(ddlMaker.SelectedValue);
                        veh.Mileage = milage;
                        veh.PlateNo = txtplate.Text;
                        veh.TrackerCompany = Convert.ToInt32(ddlTracker.SelectedValue);
                        veh.TypeID = Convert.ToInt32(ddlVehType.SelectedValue);
                        veh.DateLastModified = DateTime.Now;
                        if(User.Identity.IsAuthenticated)
                         veh.LastModifiedBy = User.Identity.Name;
                        veh.EngineNo = txtEngNo.Text;
                        veh.ChasisNo = txtChasis.Text;
                        veh.LocationID = int.Parse(ddlLocation.SelectedValue);
                        if (chk.Checked)
                        {
                            veh.DelFlg = "Y";
                        }
                        else
                        {
                            veh.DelFlg = "N";
                        }
                        rst = VehicleBLL.UpdateVehicle(veh);
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
                    Vehicle veh = new Vehicle();
                    veh.Name = txtName.Text.ToUpper();
                    veh.AssociatedDriver = Convert.ToInt32(ddlDriver.SelectedValue);
                    veh.Capacity = capacity;
                    veh.InsuranceCompany = Convert.ToInt32(ddlInsur.SelectedValue);
                    veh.InsuranceExprDate = DateTime.Parse(txtIDate.Text, culture);
                    veh.InsuranceNo = txtInsur.Text;
                    veh.LicenseExprDate = DateTime.Parse(txtLDate.Text, culture);
                    veh.LicenseNo = txtLicense.Text;
                    veh.MarkerID = Convert.ToInt32(ddlMaker.SelectedValue);
                    veh.Mileage = milage;
                    veh.PlateNo = txtplate.Text;
                    veh.TrackerCompany = Convert.ToInt32(ddlTracker.SelectedValue);
                    veh.TypeID = Convert.ToInt32(ddlVehType.SelectedValue);
                    veh.Status = (int)Utility.VehicleStatus.Available;
                    veh.DateAdded = DateTime.Now;
                    veh.EngineNo = txtEngNo.Text;
                    veh.ChasisNo = txtChasis.Text;
                    veh.LocationID = int.Parse(ddlLocation.SelectedValue);
                    veh.DelFlg = "N";
                    if (User.Identity.IsAuthenticated)
                        veh.AddedBy = User.Identity.Name;

                    result = VehicleBLL.AddVehicle(veh);
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

        protected void gvVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                gvVehicle.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            {
            }
        }

        protected void gvVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string  format = "dd/MM/yyyy";
                hid.Value = "Update";
                dvDel.Visible = true;
                dvID.Visible = true;
                dvMsg.InnerText = "Update Vehicle's Information :";
                btnSubmit.Text = "Update";
                int Id = Convert.ToInt32(gvVehicle.SelectedDataKey[0].ToString());
                Vehicle veh = null;
                veh = VehicleBLL.GetVehicle(Id);
                txtID.Text = Id.ToString();
                txtName.Text = veh.Name;
                ddlDriver.SelectedValue = veh.AssociatedDriver.ToString();
                txtCapacity.Text = veh.Capacity.HasValue?veh.Capacity.Value.ToString():"";
                ddlInsur.SelectedValue = veh.InsuranceCompany.HasValue?veh.InsuranceCompany.ToString():"";
                txtLicense.Text = veh.LicenseNo.ToString();
                txtIDate.Text = veh.InsuranceExprDate.HasValue?DateTime.Parse(veh.InsuranceExprDate.Value.ToString()).ToString(format):"";
                txtInsur.Text= veh.InsuranceNo;
                txtLDate.Text=veh.LicenseExprDate.HasValue?DateTime.Parse(veh.LicenseExprDate.Value.ToString()).ToString(format):"";
                txtLicense.Text= veh.LicenseNo ;
                ddlMaker.SelectedValue = veh.MarkerID.HasValue?veh.MarkerID.Value.ToString():"";
                txtMilage.Text = veh.Mileage.HasValue ? veh.Mileage.Value.ToString() : "";
                txtplate.Text = veh.PlateNo;
                ddlTracker.SelectedValue = veh.TrackerCompany.HasValue?veh.TrackerCompany.Value.ToString():"";
                ddlVehType.SelectedValue = veh.TypeID.HasValue? veh.TypeID.Value.ToString():"";
                txtEngNo.Text = veh.EngineNo;
                txtChasis.Text = veh.ChasisNo;
                ddlLocation.SelectedValue = veh.LocationID.HasValue ? veh.LocationID.Value.ToString() : "";
                if (veh.DelFlg != null)
                {
                    if (veh.DelFlg == "Y")
                    {
                        chk.Checked = true;
                    }
                    if (veh.DelFlg == "N")
                    {
                        chk.Checked = false;
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
        private void Reset()
        {
            txtInsur.Text = "";  ddlDriver.SelectedValue="";
            txtLDate.Text = "";  ddlInsur.SelectedValue = "";
            txtName.Text = "";   ddlMaker.SelectedValue = "";
            txtplate.Text = "";  ddlTracker.SelectedValue = "";
            txtMilage.Text = ""; ddlVehType.SelectedValue = "";
            txtCapacity.Text = "";
        }

        protected string GetTripCount(object o)
        {
            try
            {
                int count = 0;
                if (o != null)
                {
                    int vehId = int.Parse(o.ToString());
                    using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                    {
                        var cc = (from p in db.Trips where p.AssignedVehicle == vehId select p).Count();
                        count = int.Parse(cc.ToString());
                    }
                }
                return count.ToString() + " Trip(s)";
            }
            catch
            {
                return "";
            }
        }

        protected string GetStatus(object o)
        {
            try
            {
                return Utility.GetVehicleStatus(o);
            }
            catch
            {
                return "";
            }
        }

        protected void gvVehicle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    int key = Convert.ToInt32(gvVehicle.DataKeys[index].Value.ToString());
                    Response.Redirect(String.Format("VehicleDetail.aspx?id={0}", key), false);

                }
                if (e.CommandName == "trip")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    Response.Redirect(String.Format("ManageFleetRequest.aspx?vid={0}", index), false);
                }
            }
            catch
            {
            }
        }

         
 

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtSea.Value))
                {
                    string srch = txtSea.Value; usr = (User)Session["user"];
                    gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(v => v.PlateNo.ToUpper().Trim() == srch.ToUpper().Trim()).ToList();
                    gvVehicle.DataBind();
                }
            }
            catch
            {
            }
        }

        protected void btnCl_Click(object sender, EventArgs e)
        {
            txtSea.Value = "";
            BindGrid();
        }
    }
}