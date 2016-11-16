using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Configuration;
using System.IO;

namespace FleetMgtSolution
{
    public partial class RequestApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Trip trip = null;
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int Id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    hid.Value = Id.ToString();
                    BindGrid(Id);
                    trip = TripBLL.GetTripDetailByID(Id);
                    if (trip.Status == (int)Utility.FleetRequestStatus.Pending_Supervisor_Approval)
                    {
                        LinkButton1.Visible = true;
                        LinkButton2.Visible = true;
                    }
                    if (trip != null)
                    {
                        lbheader.Text = trip.ID.ToString();
                        lbInit.Text = trip.InitiatorName.ToUpper();
                    }
                }
                else
                {
                    Response.Redirect("MyFleetRequest.aspx", false);
                }
            }
        }
        private void BindGrid(int id)
        {
            gvheader.DataSource = TripBLL.GetTripListByID(id);
            gvheader.DataBind();
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

        }
        protected void btnbtnAppr_Click(object sender, EventArgs e)
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
                    Response.Redirect("Login.aspx", false);
                    return;
                }
                int requestId = Convert.ToInt32(hid.Value);
                Trip trip = TripBLL.GetTripDetailByID(requestId);
                if (trip != null)
                {
                    trip.Status = (int)Utility.FleetRequestStatus.Pending_FleetManager_Approval;
                    
                    if (TripBLL.UpdateTrip(trip))
                    {
                        LinkButton1.Enabled = false;
                        LinkButton2.Visible = false;
                        BindGrid(requestId);
                        ///send mail to fleet manager...
                        {
                            string body = "";
                            string from = ConfigurationManager.AppSettings["exUser"].ToString();
                           string rolname  = ConfigurationManager.AppSettings["GssAdminRole"].ToString();
                            string to = Utility.GetUsersEmailAdd(rolname, trip.LocationID.Value);
                            string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                            string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                            string subject = "Fleet Request Approval Notification";
                            string FilePath = Server.MapPath("EmailTemplates/");
                            if (File.Exists(FilePath + "FleetManagerNotification.htm"))
                            {
                                FileStream f1 = new FileStream(FilePath + "FleetManagerNotification.htm", FileMode.Open);
                                StreamReader sr = new StreamReader(f1);
                                body = sr.ReadToEnd();
                                body = body.Replace("@add_appLogo", appLogo);
                                body = body.Replace("@add_siteUrl", siteUrl+"?RequestId="+trip.ID.ToString());
                                body = body.Replace("@add_requestor", trip.InitiatorName);
                                body = body.Replace("@priority", Utility.GetPriority(trip.priority));
                                body = body.Replace("@add_Destination", trip.Destination); //Replace the values from DB or any other source to personalize each mail.  
                                body = body.Replace("@add_purpose", trip.Purpose);
                                body = body.Replace("@add_tripDate", trip.TripDate.Value.ToString("dd-MMM-yyyy"));
                                body = body.Replace("@add_DeptTime", trip.ExpectedDepartureTime);
                                body = body.Replace("@add_approver", usr.StaffName);
                                body = body.Replace("@add_comment", txtCommentAppr.Text);
                                f1.Close();
                            } try
                            {
                                User hod = null;
                                if (usr.isHoD == false)
                                {
                                    hod = UserBLL.GetDeptHOD(usr.DepartmentID.Value);
                                }
                                if (hod != null)
                                {
                                    string rst = Utility.SendMail(to, from, hod.Email.Trim(), subject, body);
                                }
                                else
                                {
                                    string rst = Utility.SendMail(to, from, "", subject, body);
                                }
                            }
                            catch { }

                            /////send mail to Requestor...
                            {
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
                                    body1 = body1.Replace("@add_msg", "and it is currently <b>pending FleetManager approver</b>");
                                    body1 = body1.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                                    body1= body1.Replace("@add_comment", txtCommentAppr.Text);
                                    f1.Close();
                                }
                                try
                                {
                                    string rst2 = Utility.SendMail(trip.InitiatorEmail, from1, "", subject1, body1);
                                }
                                catch { }
                            }
                            success.Visible = true;
                            success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Request has been successfully approved. Notifications has been sent to Fleet Manager for further action on the request";
                        }
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
                        LinkButton1.Visible = false;
                        LinkButton2.Enabled = false;
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
                            body = body.Replace("@add_requestor", trip.InitiatorName);
                            body = body.Replace("@add_requestID", trip.ID.ToString());
                            body = body.Replace("@add_comment", txtComment.Text);
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
    }
}