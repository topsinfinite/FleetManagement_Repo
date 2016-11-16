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

namespace FleetMgtSolution
{
    public partial class _Default : System.Web.UI.Page
    {
        CultureInfo culture = new CultureInfo("en-GB");
        private string DeptInitiatorRole = ConfigurationManager.AppSettings["DeptIniRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindLocation();
                }
            }catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
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
                    if (dHr > aHr&& dHr!=12)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Sorry Departure Time could not be greater than Arrival time";
                        
                        return;
                    }
                    if (dHr == aHr && dMin >= aMin)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button>Sorry Departure Time could not be greater than Arrival time";
                        UpdatePanel1.Update();
                        return;
                        
                    }
                }
                if (dfrmt == "PM" && dHr!=12)
                {
                    dHr += 12;
                }
                if (afrmt == "PM" && aHr!=12)
                {
                    aHr += 12;
                }
                if (dfrmt == "AM" && dHr == 12)
                {
                    //dHr -= 12;
                }
                if (afrmt == "AM" && aHr == 12)
                {
                   // aHr -= 12;
                }
                string dMinPad=dMin.ToString().PadRight(2,'0');
                string aMinPad= aMin.ToString().PadRight(2,'0');
                decimal dpartTime = decimal.Parse(dHr.ToString() + "." + dMinPad);
                decimal arrTime = decimal.Parse(aHr.ToString() + "." +aMin);
                decimal duration=0;
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
                
                Trip trip = new Trip();
                trip.Location = ddlLocation.SelectedItem.Text.Trim();
                trip.LocationID = int.Parse(ddlLocation.SelectedValue);
                trip.PickupLocation = txtLoc.Text;
                trip.Destination = txtDesn.Text;
                trip.Purpose = txtPur.Text;
                trip.TripDate = DateTime.Parse(txtDate.Text, culture);
                trip.DateAdded = DateTime.Now;
                trip.InitiatorID = User.Identity.Name;
                User usr=new User();
                if(Session["user"]!=null){
                   usr =(User)Session["user"];
                    trip.InitiatorName = usr.StaffName;
                    trip.InitiatorEmail = usr.Email;
                }else
                {
                    Response.Redirect("Login.aspx",false);
                    return;
                }
                int userId=int.Parse(ddlDeptApproval.SelectedValue);
                trip.ApproverIDLeve1 = userId;
                User appr=UserBLL.GetByID(userId);
                if (appr != null)
                {
                    trip.DepartmentID = appr.DepartmentID;
                }else
                {
                   error.Visible = true;
                     error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred in retrieving approver's detail. kindly try again";
                    return;
                }
                trip.ExpectedDepartureTime = dTime;
                trip.ExpectedReturnTime = aTime;
                trip.ExpectedDuration = duration;
                trip.priority = int.Parse(ddlPriority.SelectedValue);
                trip.Status = (int)Utility.FleetRequestStatus.Pending_Supervisor_Approval;
               if(TripBLL.AddTrip(trip))
               {
                   //send email to supervisor
                   string body = "";
                   string from = ConfigurationManager.AppSettings["exUser"].ToString();
                   string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                   string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();

                   string subject = "Fleet Request Approval Notification";
                   string FilePath = Server.MapPath("EmailTemplates/");
                   if (File.Exists(FilePath + "ApprovalNotification.htm"))
                   {
                       FileStream f1 = new FileStream(FilePath + "ApprovalNotification.htm", FileMode.Open);
                       StreamReader sr = new StreamReader(f1);
                       body = sr.ReadToEnd();
                       body = body.Replace("@add_appLogo", appLogo);
                       body = body.Replace("@add_siteUrl", siteUrl+"?id="+trip.ID.ToString());
                       //body = body.Replace("@add_fname",appr.StaffName);
                       body = body.Replace("@add_username", usr.StaffName);
                       body = body.Replace("@add_Destination", txtDesn.Text); //Replace the values from DB or any other source to personalize each mail.  
                       body = body.Replace("@add_purpose", txtPur.Text);
                       body = body.Replace("@add_tripDate", txtDate.Text);
                       body = body.Replace("@add_DepartTime", dTime);
                       body = body.Replace("@add_DepartTime", dTime);
                       body = body.Replace("@add_Return", aTime);
                       f1.Close();
                   }
                   string rst = Utility.SendMail(appr.Email, from, "", subject, body);
                   if (rst.Contains("Successful"))
                   {
                       Reset();
                       Response.Redirect("SuccessRequest.aspx", false);
                       return;
                   }
                   else
                   {
                       Reset();
                       Response.Redirect("SuccessRequest.aspx", false);
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
        protected void Reset()
        {
            txtPur.Text = ""; txtDesn.Text = "";
            txtDate.Text = ""; //ddlDeptApproval.SelectedValue = "";
            ddlPriority.SelectedValue = "";
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int loc=int.Parse(ddlLocation.SelectedValue);
                ddlDeptApproval.Items.Clear();
                if (Session["user"] != null)
                {
                    User usr = (User)Session["user"];
                    ddlDeptApproval.DataValueField = "ID";
                    ddlDeptApproval.DataTextField = "StaffName";
                    if (User.IsInRole(DeptInitiatorRole))
                    {
                        ddlDeptApproval.DataSource = Utility.GetUserDeptApprovers(usr.DepartmentName.Trim(), User.Identity.Name,loc);
                        ddlDeptApproval.DataBind();
                    }
                    else
                    {
                        ddlDeptApproval.DataSource = Utility.GetUserDeptApprovers(usr.DepartmentName, usr.Directorate, User.Identity.Name,loc);
                        ddlDeptApproval.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
    }
}
