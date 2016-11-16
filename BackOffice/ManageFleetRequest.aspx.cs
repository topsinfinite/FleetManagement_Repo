using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace FleetMgtSolution.BackOffice
{
    public partial class ManageFleetRequest : System.Web.UI.Page
    {
        CultureInfo culture = new CultureInfo("en-GB");
        private string GssAdminRole = ConfigurationManager.AppSettings["GssAdminRole"].ToString();
        private string headDriverRole = ConfigurationManager.AppSettings["HeadDriverRole"].ToString();
        private string smsSender = ConfigurationManager.AppSettings["smsSender"].ToString();
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
                    
                    if (User.IsInRole(GssAdminRole))
                    {
                        btnBatchAppr.Visible = true;
                        dvbatch.Visible = true;
                    }
                    if (User.IsInRole(headDriverRole))
                    {
                        btnBatch.Visible = true;
                        BindVehicleGrid();
                    }
                    if (Request.QueryString["vid"] != null)
                    {
                        int vId = int.Parse(Request.QueryString["vid"].ToString());
                        gvRequest.DataSource = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(t => t.AssignedVehicle == vId).ToList();
                        gvRequest.DataBind();
                    }
                    else if (Request.QueryString["stat"] != null)
                    {
                        int stId = int.Parse(Request.QueryString["stat"].ToString());
                        gvRequest.DataSource = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(t => t.Status == stId).ToList();
                        gvRequest.DataBind();
                    }
                    else
                    {
                        BindGrid();
                    }
                    BindList();
                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        private void BindVehicleGrid()
        {
            gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(p => p.Status == (int)Utility.VehicleStatus.Available).ToList();
            gvVehicle.DataBind();
        }
        private void BindGrid()
        {
            string RequestID = ""; string status = ""; usr = (User)Session["user"];
            string init = ""; string fromDate = ""; string toDate = "";
            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                RequestID = txtID.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtInit.Text))
            {
                init = txtInit.Text;
            }
            if (!string.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
            {
                status = ddlStatus.SelectedValue;
            }
            if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
            {
                fromDate = txtFromDate.Text;
            }

            if (!string.IsNullOrWhiteSpace(txtToDate.Text))
            {
                toDate = txtToDate.Text;
            }
            gvRequest.DataSource = TripBLL.GetTripListByAdmin(usr.LocationID.Value,RequestID, status, init, fromDate, toDate);
            gvRequest.DataBind();
           //gvRequest.DataSource = TripBLL.GetTripListByAdmin();
            //gvRequest.DataBind();
        }
        private void BindList()
        {
            ddlStatus.DataTextField = "DataField";
            ddlStatus.DataValueField = "DataValue";
            ddlStatus.DataSource = Utility.StatusList();
            ddlStatus.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
                //string RequestID = ""; string status = "";
                //string init = ""; string fromDate = ""; string toDate = "";  
                //if (!string.IsNullOrWhiteSpace(txtID.Text))
                //{
                //    RequestID = txtID.Text;
                //}
                //if (!string.IsNullOrWhiteSpace(txtInit.Text))
                //{
                //    init = txtInit.Text;
                //}
                //if (!string.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
                //{
                //    status = ddlStatus.SelectedValue;
                //}
                //if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
                //{
                //    fromDate = txtFromDate.Text;
                //}

                //if (!string.IsNullOrWhiteSpace(txtToDate.Text))
                //{
                //    toDate = txtToDate.Text;
                //}
                //gvRequest.DataSource = TripBLL.GetTripListByAdmin(RequestID, status, init, fromDate, toDate);
                //gvRequest.DataBind();
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        protected void gvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRequest.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch(Exception ex)
            {
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void gvRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    int key = Convert.ToInt32(gvRequest.DataKeys[index].Value.ToString());
                    Label lbst = gvRequest.Rows[index].FindControl("lbStatus") as Label;
                    Response.Redirect(String.Format("FleetRequestDetail.aspx?id={0}&status={1}", key,lbst.Text), false);

                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        protected string GetPriority(object o)
        {
            try
            {
                return Utility.GetPriority(o);
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
                return Utility.GetRequestStatus(o);
            }
            catch
            {
                return "";
            }
        }

        protected void gvRequest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnClr_Click(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Text = ""; txtID.Text = ""; txtInit.Text = "";
                txtToDate.Text = ""; ddlStatus.SelectedValue = "";
                BindGrid();

            }
            catch(Exception ex)
            {
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void btnBatchAppr_Click(object sender, EventArgs e)
        {
            try
            {
                User usr = null;
                if (Session["user"] != null)
                {
                    usr = (User)Session["user"];
                }
                else
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }
                bool isset = false;
                string bat = Utility.GetBatchID();
                foreach (GridViewRow row in gvRequest.Rows)
                {
                    if (((CheckBox)row.FindControl("Chk")).Checked)
                    {
                        Label lbID = row.FindControl("lbID") as Label;
                        int requestID = int.Parse(lbID.Text);
                        Trip trip = TripBLL.GetTripDetailByID(requestID);
                         if (trip != null)
                         {
                             trip.Status = (int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment;
                             trip.ApproverIDlevel2 = usr.ID;
                             trip.DateApproved = DateTime.Now;
                             trip.BatchID = bat;
                             if (TripBLL.UpdateTrip(trip))
                             {
                                // BindGrid(requestId);
                                 isset = true;
                                 //Send mail and sms to HeadDriver
                                 string body = "";
                                 string from = ConfigurationManager.AppSettings["exUser"].ToString();
                                 string rolname = ConfigurationManager.AppSettings["HeadDriverRole"].ToString();
                                 string to = Utility.GetUsersEmailAdd(rolname, trip.LocationID.Value);
                                 string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                                 string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                                 string subject = "Fleet Request Approval Notification";
                                 string FilePath = Server.MapPath("EmailTemplates/");
                                 if (File.Exists(FilePath + "HeadDriverNotification.htm"))
                                 {
                                     FileStream f1 = new FileStream(FilePath + "HeadDriverNotification.htm", FileMode.Open);
                                     StreamReader sr = new StreamReader(f1);
                                     body = sr.ReadToEnd();
                                     body = body.Replace("@add_appLogo", appLogo);
                                     body = body.Replace("@add_siteUrl", siteUrl);
                                     body = body.Replace("@add_requestor", trip.InitiatorName);
                                     body = body.Replace("@priority", Utility.GetPriority(trip.priority));
                                     body = body.Replace("@add_Destination", trip.Destination); //Replace the values from DB or any other source to personalize each mail.  
                                     body = body.Replace("@add_purpose", trip.Purpose);
                                     body = body.Replace("@add_tripDate", trip.TripDate.Value.ToString("dd-MMM-yyyy"));
                                     body = body.Replace("@add_DeptTime", trip.ExpectedDepartureTime);
                                     body = body.Replace("@add_approver", usr.StaffName);
                                     body = body.Replace("@add_batch", bat);
                                     f1.Close();
                                     try
                                     {
                                         string rst = Utility.SendMail(to, from, "", subject, body);
                                     }
                                     catch { }
                                 }
                                 //send a mail to requestor
                                 string body1 = "";
                                 string from1 = ConfigurationManager.AppSettings["exUser"].ToString();
                                 string subject1 = "Fleet Request Notification";
                                 string FilePath1 = Server.MapPath("EmailTemplates/");
                                 if (File.Exists(FilePath1 + "RequestorStatusNotification.htm"))
                                 {
                                     FileStream f1 = new FileStream(FilePath1 + "RequestorStatusNotification.htm", FileMode.Open);
                                     StreamReader sr = new StreamReader(f1);
                                     body1 = sr.ReadToEnd();
                                     body1 = body1.Replace("@add_appLogo", appLogo);
                                     body1 = body1.Replace("@add_siteUrl", siteUrl);
                                     body1 = body1.Replace("@add_requestor", trip.InitiatorName);
                                     body1 = body1.Replace("@add_requestID", trip.ID.ToString());
                                     body1 = body1.Replace("@add_msg", "A vehicle and driver will be assign to you shortly");
                                     body1 = body1.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                                     f1.Close();
                                 }
                                 try
                                 {
                                     string rst2 = Utility.SendMail(trip.InitiatorEmail, from1, "", subject1, body1);
                                 }
                                 catch { }
                                 //success.Visible = true;
                                // success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully approved. Notifications has been sent to HeadDriver for further action on the request";
                             }
                         }
                    }         
                }
                BindGrid();
                if (isset)
                {
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Selected Request(s) has been successfully approved. Notifications has been sent to HeadDriver for further action on the request";
                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void gvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label st = e.Row.FindControl("lbStatus") as Label;
                    if (st.Text == ((int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment).ToString() && User.IsInRole(headDriverRole))
                    {
                        CheckBox ck = e.Row.FindControl("Chk") as CheckBox;
                        ck.Enabled = true;
                    }
                    if (st.Text == ((int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).ToString() && User.IsInRole(GssAdminRole))
                    {
                        CheckBox ck = e.Row.FindControl("Chk") as CheckBox;
                        ck.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                usr = (User)Session["user"];
                if (optVehicle.SelectedValue == "1")
                {
                    gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(p => p.Status == (int)Utility.VehicleStatus.Available).ToList();
                    gvVehicle.DataBind();
                    mpeAssign.Show();
                }
                if (optVehicle.SelectedValue == "2")
                {
                    gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(p => p.Status == (int)Utility.VehicleStatus.Enroute).ToList();
                    gvVehicle.DataBind();
                    mpeAssign.Show();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 User usr = null;
                if (Session["user"] != null)
                {
                    usr = (User)Session["user"];
                }
                else
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }
                bool isset = false;
                String DriverName = ((Label)gvVehicle.SelectedRow.FindControl("lbDrv")).Text;
                String DriverMobile = ((Label)gvVehicle.SelectedRow.FindControl("lbDrvFone")).Text;
                String VehicleMake = ((Label)gvVehicle.SelectedRow.FindControl("lbMaker")).Text;
                int VehicleID = Convert.ToInt32(gvVehicle.SelectedDataKey["ID"].ToString());
                Vehicle veh = VehicleBLL.GetVehicle(VehicleID);
                foreach (GridViewRow row in gvRequest.Rows)
                {
                    if (((CheckBox)row.FindControl("Chk")).Checked)
                    {
                         Label lbID = row.FindControl("lbID") as Label;
                        int requestID = int.Parse(lbID.Text);
                        Trip trip = TripBLL.GetTripDetailByID(requestID);
                        if (trip != null)
                        {
                            trip.Status = (int)Utility.FleetRequestStatus.Enroute;
                        trip.DateAssigned = DateTime.Now;
                        trip.AssignedBy = usr.ID;
                        trip.AssignedVehicle = VehicleID;
                        SmsNotification sms = new SmsNotification();
                        trip.MileageAtDeparture = veh.Mileage.HasValue ? veh.Mileage.Value : 0;
                        Department dept = DepartmentBLL.GetDepartment(trip.DepartmentID.Value);
                        sms.PHONE = DriverMobile;
                        sms.SENDER = smsSender;
                        sms.MESSAGE = "easyFLEET:Trip Assigned:: #Location:" + trip.Location.ToUpper() + " #Destination:" + trip.Destination.ToUpper() + " #Date:" + trip.TripDate.Value.ToString("dd-MMM-yyyy").ToUpper() + " #DepartureTime:" + trip.ExpectedDepartureTime + " #Requestor:" + trip.InitiatorName.ToUpper() + " #Dept:" + dept.Name + " Thanks.";
                        sms.STATUS = "U";
                        if (TripBLL.UpdateTrip(trip))
                        {
                            isset = true;
                            BindGrid();
                            if (veh != null)
                            {
                                veh.Status = (int)Utility.VehicleStatus.Enroute;
                                VehicleBLL.UpdateVehicle(veh);
                            }
                            //Send Sms to drive---to do
                            Utility.AddSmsNotification(sms);

                            //lnkCheckAvailabity.Enabled = false;
                            //lnkHold.Visible = false;
                            //send Email to Requestor and sms to Driver
                            string body = "";
                            string from = ConfigurationManager.AppSettings["exUser"].ToString();

                            string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                            string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                            string subject = "Vehicle Assignment Notification";
                            string FilePath = Server.MapPath("EmailTemplates/");
                            if (File.Exists(FilePath + "VehicleAssignedNotification.htm"))
                            {
                                FileStream f1 = new FileStream(FilePath + "VehicleAssignedNotification.htm", FileMode.Open);
                                StreamReader sr = new StreamReader(f1);
                                body = sr.ReadToEnd();
                                
                                body = body.Replace("@add_appLogo", appLogo);
                                body = body.Replace("@add_siteUrl", siteUrl);
                                //body = body.Replace("@add_Initiator", trip.InitiatorName);
                                body = body.Replace("@dd_RequestID", trip.ID.ToString());
                                body = body.Replace("@add_Plate", veh.PlateNo);
                                body = body.Replace("@add_driver", DriverName);
                                body = body.Replace("@add_Mobile", DriverMobile);
                                body = body.Replace("add_vehicleName", veh.Name + "[" + veh.PlateNo + "]");
                                // body = body.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                                f1.Close();
                            }
                            try
                            {
                                string rst2 = Utility.SendMail(trip.InitiatorEmail, from, "", subject, body);
                            }
                            catch { }
                            success.Visible = true;
                            success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully Treated. Notifications has been sent to Initiator, SMS has been sent to the Assigned Driver";

                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred in one or more of the selected record. pls review your selection. kindly try again!!!";
                        }
                        }
                    }
                }
                if (isset)
                {
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Selected Request(s) has been successfully approved. Sms Notifications has been sent to HeadDriver for further action on the request";
                }
                else
                {
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>No update was done.Check that you select one or more request";
                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        protected void gvVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvVehicle.PageIndex = e.NewPageIndex;
                BindVehicleGrid();
                mpeAssign.Show();
            }
            catch
            {
            }
        }
    }
}