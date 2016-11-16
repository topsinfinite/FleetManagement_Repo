using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Configuration;
using System.IO;

namespace FleetMgtSolution.BackOffice
{
    public partial class FleetRequestDetail : System.Web.UI.Page
    {
        private string adminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string GssAdminRole = ConfigurationManager.AppSettings["GssAdminRole"].ToString();
        private string headDriverRole = ConfigurationManager.AppSettings["HeadDriverRole"].ToString();
        private string DeptApprverRole = ConfigurationManager.AppSettings["DeptApprverRole"].ToString();
        private string smsSender = ConfigurationManager.AppSettings["smsSender"].ToString();
        private string[] dualRole = ConfigurationManager.AppSettings["dualRole"].ToString().Split(new char[] { ',' });
        private User usr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("../Login.aspx", false);
                }
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
                    Trip trip = null; int Id=0; string stat="";
                    if (Request.QueryString["id"] != null)
                    {
                        Id= Convert.ToInt32(Request.QueryString["id"].ToString());
                        hid.Value = Id.ToString();
                        BindGrid(Id);
                        BindLogGrid();
                        trip = TripBLL.GetTripDetailByID(Id);
                        if (trip != null)
                        {
                            stat = trip.Status.Value.ToString();
                            lbheader.Text = trip.ID.ToString();
                            lbInit.Text = trip.InitiatorName.ToUpper();
                            lbDateAdded.Text = trip.DateAdded.Value.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                            return;
                        }
                        int status = int.Parse(stat);
                        if (status == (int)Utility.FleetRequestStatus.Completed || status == (int)Utility.FleetRequestStatus.Declined || status == (int)Utility.FleetRequestStatus.Cancelled)
                        {
                            dvTripCanCel.Visible = false;
                            //lnkCancel.Visible = false;
                        }
                        if (User.IsInRole(GssAdminRole) && stat == ((int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).ToString())
                        {
                            dvFleetmgr.Visible = true;
                            dvAct.Visible = false;
                            
                        }
                        if (User.IsInRole(GssAdminRole)&& stat == ((int)Utility.FleetRequestStatus.OnHold).ToString())
                        {
                            //dvTripCmpl.Visible = true;
                            //dvHeadDriver.Visible = true;
                            // BindVehicleGrid();
                            dvFleetmgr.Visible = true;
                            dvAct.Visible = false;
                            lnkHold.Visible = false;
                             
                        }
                        if (User.IsInRole(DeptApprverRole) && stat == ((int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).ToString())//enable FleetManager Role for GSS Departmt approval
                        {
                            if (dualRole.Length > 0)//granting users in dual list backoffice access
                            {
                                foreach (string uID in dualRole)
                                {
                                    if (uID == User.Identity.Name)
                                    {
                                        dvFleetmgr.Visible = true;
                                        dvAct.Visible = false;
                                        if (stat == ((int)Utility.FleetRequestStatus.OnHold).ToString())
                                        {
                                            //dvTripCmpl.Visible = true;
                                            //dvHeadDriver.Visible = true;
                                            // BindVehicleGrid();
                                            lnkHold.Visible = false;
                                            dvAct.Visible = false;
                                        }
                                    }
                                }
                            }

                        }
                        if (User.IsInRole(headDriverRole))
                        {
                            if (stat == ((int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment).ToString())
                            {
                                dvHeadDriver.Visible = true;
                                BindVehicleGrid();
                                dvAct.Visible = false;
                            }

                        }
                        if (User.IsInRole(headDriverRole) && stat == ((int)Utility.FleetRequestStatus.Enroute).ToString())
                        {
                            dvTripCmpl.Visible = true;
                            BindVehicleGrid();
                            dvAct.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("ManageFleetRequest.aspx", false);
                        return;
                    }
                
                    
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        private void BindGrid(int id)
        {
            gvheader.DataSource = TripBLL.GetTripListByID(id);
            gvheader.DataBind();
        }
        private void BindVehicleGrid()
        {
            usr = (User)Session["user"];
            gvVehicle.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(p=>p.Status==(int)Utility.VehicleStatus.Available).ToList();
            gvVehicle.DataBind();
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
        protected string GetVehicleDetail(object o)
        {
            try
            {
                if (o != null)
                {
                    Vehicle veh = VehicleBLL.GetVehicle(int.Parse(o.ToString()));
                    if (veh != null)
                    {
                        return veh.Name + "(" + veh.PlateNo + ")";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        protected void lnkAppr_Click(object sender, EventArgs e)
        {
         
             User usr=null;
                if(Session["user"]!=null)
                {
                    usr=(User)Session["user"];
                }else
                {
                    Response.Redirect("../Login.aspx",false);
                    return;
                }
                try
                {
                    int requestId = Convert.ToInt32(hid.Value);
                    Trip trip = TripBLL.GetTripDetailByID(requestId);
                    if (trip != null)
                    {
                        trip.Status = (int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment;
                        trip.ApproverIDlevel2 = usr.ID;
                        trip.DateApproved = DateTime.Now;
                        if (TripBLL.UpdateTrip(trip))
                        {
                            BindGrid(requestId);
                            lnkAppr.Enabled = false;
                            lnkReject.Visible = false;
                            lnkHold.Visible = false;
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
                                body = body.Replace("@add_batch", "N/A");
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
                                //body1 = body1.Replace("@add_requestor", trip.InitiatorName);
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
                            success.Visible = true;
                            success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully approved. Notifications has been sent to HeadDriver for further action on the request";
                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while updating request.. kindly try again!!!";
                            return;
                        }
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while retrieving request.. kindly try again!!!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                    Utility.WriteError("Error: " + ex.InnerException);
                    return;
                }
        }

        protected void lnkReject_Click(object sender, EventArgs e)
        {

        }
        protected void gvVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {  
                User usr=null;
                if(Session["user"]!=null)
                {
                    usr=(User)Session["user"];
                }else
                {
                    Response.Redirect("../Login.aspx",false);
                    return;
                }
                  String DriverName = ((Label)gvVehicle.SelectedRow.FindControl("lbDrv")).Text;
                  String DriverMobile = ((Label)gvVehicle.SelectedRow.FindControl("lbDrvFone")).Text;
                  String VehicleMake = ((Label)gvVehicle.SelectedRow.FindControl("lbMaker")).Text;
                  int VehicleID = Convert.ToInt32(gvVehicle.SelectedDataKey["ID"].ToString());
                  int requestId = Convert.ToInt32(hid.Value);
                  SmsNotification sms = new SmsNotification();
                    Trip trip = TripBLL.GetTripDetailByID(requestId);
                    Vehicle veh = VehicleBLL.GetVehicle(VehicleID);
                    if (trip != null)
                    {
                        trip.Status = (int)Utility.FleetRequestStatus.Enroute;
                        trip.DateAssigned = DateTime.Now;
                        trip.AssignedBy = usr.ID;
                        trip.AssignedVehicle = VehicleID;
                        
                        trip.MileageAtDeparture = veh.Mileage.HasValue ? veh.Mileage.Value : 0;
                        Department dept = DepartmentBLL.GetDepartment(trip.DepartmentID.Value);
                        sms.PHONE = DriverMobile;
                        sms.SENDER = smsSender;
                        sms.MESSAGE = "easyFLEET:Trip Assigned:: #Location:" + trip.Location.ToUpper() + " #Destination:" + trip.Destination.ToUpper() + " #Date:" + trip.TripDate.Value.ToString("dd-MMM-yyyy").ToUpper() + " #DepartureTime:" + trip.ExpectedDepartureTime + " #Requestor:" + trip.InitiatorName.ToUpper() + " #Dept:" + dept.Name + " Thanks.";
                        sms.STATUS = "U";
                        if (TripBLL.UpdateTrip(trip))
                        {
                            BindGrid(requestId);
                           
                            if (veh != null)
                            {
                                veh.Status = (int)Utility.VehicleStatus.Enroute;
                                VehicleBLL.UpdateVehicle(veh);
                            }
                            //Send Sms to drive---to do
                            Utility.AddSmsNotification(sms);

                            lnkCheckAvailabity.Enabled = false;
                            lnkHold.Visible = false;
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
                    }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
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
        protected void lnkHold_Click(object sender, EventArgs e)
        {
            try
            {
                int requestId = Convert.ToInt32(hid.Value);
                Trip trip = TripBLL.GetTripDetailByID(requestId);
                if (trip != null)
                {
                    trip.Status = (int)Utility.FleetRequestStatus.OnHold;
                    if (TripBLL.UpdateTrip(trip))
                    {
                        lnkCheckAvailabity.Visible = false;
                        lnkHold.Enabled = false;
                        BindGrid(requestId);
                        //send email to requestor
                        string body = "";
                        string from = ConfigurationManager.AppSettings["exUser"].ToString();

                        string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                        string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                        string subject = "Fleet Request Notification";
                        string FilePath = Server.MapPath("EmailTemplates/");
                        if (File.Exists(FilePath + "RequestOnHoldNotification.htm"))
                        {
                            FileStream f1 = new FileStream(FilePath + "RequestOnHoldNotification.htm", FileMode.Open);
                            StreamReader sr = new StreamReader(f1);
                            body = sr.ReadToEnd();
                            body = body.Replace("@add_appLogo", appLogo);
                            body = body.Replace("@add_siteUrl", siteUrl);
                            body = body.Replace("@add_comment", txtComment.Text);
                            body = body.Replace("@add_requestID", trip.ID.ToString());
                            // body = body.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                            f1.Close();
                        }
                        try
                        {
                            string rst2 = Utility.SendMail(trip.InitiatorEmail, from, "", subject, body);
                        }
                        catch { }
                        success.Visible = true;
                        success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully Declined. Notifications has been sent to Initiator";

                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while updating status.. kindly try again!!!";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtComment.Text))
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> comment is required";
                    return;
                }
                int requestId = Convert.ToInt32(hid.Value);
                Trip trip = TripBLL.GetTripDetailByID(requestId);
                if (trip != null)
                {
                    trip.Status = (int)Utility.FleetRequestStatus.Declined;
                    if (TripBLL.UpdateTrip(trip))
                    {
                        lnkAppr.Visible = false;
                        lnkReject.Enabled = false;
                        BindGrid(requestId);
                        //send email to requestor
                        string body = "";
                        string from = ConfigurationManager.AppSettings["exUser"].ToString();

                        string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                        string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                        string subject = "Fleet Request Notification";
                        string FilePath = Server.MapPath("EmailTemplates/");
                        if (File.Exists(FilePath + "RequestDeclinedNotification.htm"))
                        {
                            FileStream f1 = new FileStream(FilePath + "RequestDeclinedNotification.htm", FileMode.Open);
                            StreamReader sr = new StreamReader(f1);
                            body = sr.ReadToEnd();
                            body = body.Replace("@add_appLogo", appLogo);
                            body = body.Replace("@add_siteUrl", siteUrl);
                            //body = body.Replace("@add_requestor", trip.InitiatorName);
                            body = body.Replace("@add_requestID", trip.ID.ToString());
                            // body = body.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                            f1.Close();
                        }
                        try
                        {
                            string rst2 = Utility.SendMail(trip.InitiatorEmail, from, "", subject, body);
                        }
                        catch { }
                        success.Visible = true;
                        success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully Declined. Notifications has been sent to Initiator";

                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while updating status.. kindly try again!!!";
                        return;
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while retrieving request.. kindly try again!!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        protected void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                lbmsg.Visible = false;
                string dTime = Request.Form["timepicker1"].ToString();
                string aTime = Request.Form["timepicker2"].ToString();
                string[] dTimeArg = dTime.Split(new char[] { ':', ' ' });
                string[] aTimeArg = aTime.Split(new char[] { ':', ' ' });
                int aHr = int.Parse(aTimeArg[0]);
                int aMin = int.Parse(aTimeArg[1]);
                int dHr = int.Parse(dTimeArg[0]);
                int dMin = int.Parse(dTimeArg[1]);
                string afrmt = aTimeArg[2];
                string dfrmt = dTimeArg[2];
                if (dfrmt == afrmt)
                {
                    if (dHr == aHr && dMin>=aMin)
                    {
                        lbmsg.Visible = true;
                        lbmsg.Text = "Sorry Departure Time could not be greater than or equal to Arrival time";
                        mpeLog.Show();
                        return;
                    }
                    if (dHr > aHr && dHr!=12)
                    {
                        lbmsg.Visible = true;
                        lbmsg.Text = "Sorry Departure Time could not be greater than Arrival time";
                        mpeLog.Show();
                        return;
                    }
                }
                if (dfrmt == "PM" && dHr != 12)
                {
                    dHr += 12;
                }
                if (afrmt == "PM" && aHr != 12)
                {
                    aHr += 12;
                }
                if (dfrmt == "AM" && dHr == 12)
                {
                    //dHr -= 12;
                }
                if (afrmt == "AM" && aHr == 12)
                {
                    //aHr -= 12;
                }
                string dMinPad = dMin.ToString().PadRight(2, '0');
                string aMinPad = aMin.ToString().PadRight(2, '0');
                decimal dpartTime = decimal.Parse(dHr.ToString() + "." + dMinPad);
                decimal arrTime = decimal.Parse(aHr.ToString() + "." + aMin);
                decimal duration = 0;
                duration = Utility.GetTripDuration(dHr, dMin, aHr, aMin);
                //if (dfrmt == afrmt && dHr == 12)
                //{
                //    duration = arrTime - dpartTime;
                //    duration += 12;
                //}
                //else
                //{
                //    duration = arrTime - dpartTime;
                //}
                int requestId = Convert.ToInt32(hid.Value);
                Trip trip = TripBLL.GetTripDetailByID(requestId);
                if (trip == null)
                {

                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                    return;
                }
                trip.ActualDepartureTime = dTime;
                trip.ActualRetunTime = aTime;
                trip.ActualDuration = duration;
                trip.Note = txtNote.Text;
                trip.Status = (int)Utility.FleetRequestStatus.Completed;
                decimal mileage = 0;
                if(!decimal.TryParse(txtMileage.Text,out mileage))
                {
                    lbmsg.Visible = true;
                    lbmsg.Text = "Mileage must be numeric";
                    mpeLog.Show();
                }
                trip.MileageOnArrival = mileage;
                int VehicleID = Convert.ToInt32(((Label)gvheader.Rows[0].FindControl("lbVeh")).Text);
                Vehicle veh = VehicleBLL.GetVehicle(VehicleID);
                if (veh != null)//updating vehicle information on arrival;
                {
                    if (veh.Mileage > mileage)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Current vehicle mileage cannot be greater than logged mileage. Please review your input!!!";
                        return;
                    }
                    veh.Status = (int)Utility.VehicleStatus.Available;
                    veh.Mileage = mileage;
                    VehicleBLL.UpdateVehicle(veh);
                }
                if (TripBLL.UpdateTrip(trip))
                {
                    BindLogGrid(); lnkLog.Enabled = false;
                    BindGrid(requestId);
                    txtNote.Text = "";
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Trip Completion details has been logged successfully";
                }
                else
                {
                    lbmsg.Visible = true;
                    lbmsg.Text = "An error occurred while updating.. kindly try again!!!";
                    mpeLog.Show();
                  
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        protected void lnkCheckAvailabity_Click(object sender, EventArgs e)
        {
            try
            {
                 mpeAssign.Show();

            }catch(Exception ex)
            {}
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

        protected void gvheader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "veh")
                {
                    if (e.CommandArgument != null)
                    {
                        int vehId = int.Parse(e.CommandArgument.ToString());
                        LinkButton lnkVeh = gvheader.Rows[0].FindControl("lnkVeh") as LinkButton;
                    
                        Response.Redirect(string.Format("VehicleDetail.aspx?id={0}", vehId), false);
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        protected void BindLogGrid()
        {
            int id = int.Parse(hid.Value);
            gvLog.DataSource = TripBLL.GetTripListByID(id).Where(t=>t.Status==(int)Utility.FleetRequestStatus.Completed).ToList();
            gvLog.DataBind();
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Trip trip = null;
                int id = int.Parse(hid.Value);
                trip = TripBLL.GetTripDetailByID(id);
                if (trip != null)
                {
                    if (trip.Status == (int)Utility.FleetRequestStatus.Enroute)//unassign the vehicle if this condition is true
                    {
                        if (trip.AssignedVehicle.HasValue)
                        {
                            Vehicle veh = VehicleBLL.GetVehicle(trip.AssignedVehicle.Value);
                            if (veh != null)//updating vehicle information on arrival;
                            {
                                veh.Status = (int)Utility.VehicleStatus.Available;
                                try
                                {
                                    VehicleBLL.UpdateVehicle(veh);
                                }
                                catch { }
                            }
                        }
                    }
                    trip.Status = (int)Utility.FleetRequestStatus.Cancelled;
                    if (TripBLL.UpdateTrip(trip))
                    {
                        lnkCancel.Visible = false;
                        dvFleetmgr.Visible = false;
                        dvHeadDriver.Visible = false;
                        BindGrid(id);
                        //send email to initiator
                        string body = "";
                        string from = ConfigurationManager.AppSettings["exUser"].ToString();
                        string to = ConfigurationManager.AppSettings["GssMailGrp"].ToString();
                        string cc = ConfigurationManager.AppSettings["headDriverMail"].ToString();
                        string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                        string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                        string subject = "Fleet Request Notification";
                        string FilePath = Server.MapPath("EmailTemplates/");
                        if (File.Exists(FilePath + "RequestCancelledNotification.htm"))
                        {
                            FileStream f1 = new FileStream(FilePath + "RequestCancelledNotification.htm", FileMode.Open);
                            StreamReader sr = new StreamReader(f1);
                            body = sr.ReadToEnd();
                            body = body.Replace("@add_appLogo", appLogo);
                            body = body.Replace("@add_siteUrl", siteUrl);
                            //body = body.Replace("@add_initiator", trip.InitiatorName);
                            body = body.Replace("@add_requestID", trip.ID.ToString());
                            //body = body.Replace("@add_comment", txtComment.Text);
                            // body = body.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                            f1.Close();
                        }
                        try
                        {
                             string rst2 = Utility.SendMail(trip.InitiatorEmail, from, "", subject, body);
                        }
                        catch { }
                        success.Visible = true;
                        success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully Cancelled. Notifications has been sent to Fleet Admin";

                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred while retrieving request.. kindly try again!!!";
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}