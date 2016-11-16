using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using FleetMgtSolution.BLL;
//using AmconAuctions.BLL;

namespace FleetMgtSolution
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string GssAdminRole = ConfigurationManager.AppSettings["GssAdminRole"].ToString();
        private string HeadDriverRole = ConfigurationManager.AppSettings["HeadDriverRole"].ToString();
        private string DeptApprverRole = ConfigurationManager.AppSettings["DeptApprverRole"].ToString();
        private string [] dualRole = ConfigurationManager.AppSettings["dualRole"].ToString().Split(new char[]{','});
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx",false);
                    return;
                }
                 User usr=null;
                 if (Session["user"] != null)
                 {
                     usr = (User)Session["user"];
                 }
                 else
                 {
                     Response.Redirect("Login.aspx", false);
                     return;
                 }

                Label lblfnm = (Label)LoginView1.FindControl("lbFName");
                if (dualRole.Length > 0)//granting users in dual list backoffice access
                {
                    foreach (string uID in dualRole)
                    {
                        if (uID == usr.StaffID)
                        {
                            HyperLink lnkboffice = (HyperLink)LoginView1.FindControl("lnkBackOffice");
                            lnkboffice.Visible = true;
                            lblfnm.Text = usr.StaffName;
                        }
                    }
                }
                if (HttpContext.Current.User.IsInRole(GssAdminRole) || HttpContext.Current.User.IsInRole(AdminRole) || HttpContext.Current.User.IsInRole(HeadDriverRole))
                {
                    HyperLink lnkboffice = (HyperLink)LoginView1.FindControl("lnkBackOffice");
                    lnkboffice.Visible = true;
                    lblfnm.Text = usr.StaffName;
                  
                }else if(HttpContext.Current.User.IsInRole(DeptApprverRole))
                {
                    lblfnm.Text = usr.StaffName;
                    HyperLink lnkbtn = (HyperLink)LoginView1.FindControl("lnkAlert");
                    IEnumerable<Trip> tripLst=TripBLL.GetFleetRequestByApproval(usr.ID,(int)Utility.FleetRequestStatus.Pending_Supervisor_Approval);
                    lnkbtn.Text = tripLst.Count().ToString() + " Pending Request(s)";
                    lnkbtn.NavigateUrl = "RequestApprovalList.aspx";
                    lnkbtn.Visible = true;
                    mgtReq.Visible = true;
                    mgtReqMobile.Visible = true;
                }
                else
                {
                    lblfnm.Text = usr.StaffName;
                }
            //    if (HttpContext.Current.User.IsInRole(MemberRole))
            //    {
            //        HyperLink lnkboffice = (HyperLink)LoginView1.FindControl("lnkBackOffice");
            //        lnkboffice.Visible = false;

            //        Label lblfnm = (Label)LoginView1.FindControl("lbFName");
            //        Label lbbid = (Label)LoginView1.FindControl("lbBidder");
            //        lbbid.Visible = true;
            //        if (Session["user"] != null)
            //        {
            //            lblfnm.Text = Session["user"].ToString();
            //        }
            //        if (Session["bidder"] != null)
            //        {
            //            lbbid.Text = "BidderID: "+Session["bidder"].ToString();
            //        }
                    
            //    }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }

        protected void lnkAlert_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("RequestApprovalList.aspx", false);
            }
            catch(Exception ex)
            {
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
    }
}
