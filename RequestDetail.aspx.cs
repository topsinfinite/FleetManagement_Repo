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
    public partial class RequestDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx",false);
                    return;
                }
                Trip trip = null;
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int Id = Convert.ToInt32(Request.QueryString["id"].ToString());
                        hid.Value = Id.ToString();
                        BindGrid(Id);
                        trip = TripBLL.GetTripDetailByID(Id);
                        if (trip != null)
                        {
                            lbheader.Text = trip.ID.ToString();
                            lbInit.Text = trip.InitiatorName.ToUpper();
                            if (trip.Status == (int)Utility.FleetRequestStatus.Enroute||trip.Status == (int)Utility.FleetRequestStatus.Completed || trip.Status == (int)Utility.FleetRequestStatus.Declined || trip.Status == (int)Utility.FleetRequestStatus.Cancelled)
                            {
                                LinkButton1.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("MyFleetRequest.aspx", false);
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

        protected void gvheader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "veh")
                {
                    if (e.CommandArgument != null)
                    {
                        int drvId = int.Parse(e.CommandArgument.ToString());
                        LinkButton lnkVeh = gvheader.Rows[0].FindControl("lnkDrv") as LinkButton;
                        Driver drv = DriverBLL.GetDriver(drvId);
                        if (drv != null)
                        {
                            lbName.Text = drv.Name.ToUpper();
                            lbMobile.Text = drv.MobileNo;
                        }
                        mpe.Show();
                        //Response.Redirect(string.Format("VehicleDetail.aspx?id={0}", vehId), false);
                    }
                    else
                    {

                    }
                }
            }catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Trip trip = null;
                int id = int.Parse(hid.Value);
                trip = TripBLL.GetTripDetailByID(id);
                if (trip != null)
                {
                   
                    trip.Status = (int)Utility.FleetRequestStatus.Cancelled;
                    if (TripBLL.UpdateTrip(trip))
                    {
                        LinkButton1.Visible = false;

                        BindGrid(id);
                        //send email to GssAdmin and Copy HeadDriver
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
                            body = body.Replace("@add_initiator", trip.InitiatorName);
                            body = body.Replace("@add_requestID", trip.ID.ToString());
                            //body = body.Replace("@add_comment", txtComment.Text);
                            // body = body.Replace("@add_approver", usr.StaffName); //Replace the values from DB or any other source to personalize each mail.  
                            f1.Close();
                        }
                        try
                        {
                            string rst2 = Utility.SendMail(to, from, cc, subject, body);
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
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
    }
}