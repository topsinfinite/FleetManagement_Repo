using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution;
using FleetMgtSolution.BLL;
using System.Configuration;
//using AmconAuctions.BLL;

namespace FleetMgtSolution
{
    public partial class SiteAdmin : System.Web.UI.MasterPage
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("../Login.aspx",false);
                return;
            }
            if (Session["user"] == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    //if (HttpContext.Current.User.IsInRole(AdminRole))
                    //{
                    //    lnkUsrMgt.Enabled = true;
                    //}
                    Label lblfnm = (Label)LoginView1.FindControl("lbFName");
                    Label lblRole = (Label)LoginView1.FindControl("lbRole");
                    User userdata = new User();
                    if (Session["user"] != null)
                    {
                        userdata = (User)Session["user"];
                        // userdata=UserBLL.GetUserByUserName(HttpContext.Current.User.Identity.Name);
                        lblfnm.Text = userdata.StaffName;
                        lblRole.Text = userdata.RoleName;

                        //if (!userdata.HasChangedPwd.Value)
                        //{
                        //    Response.Redirect("../ResetPassword.aspx");
                        //    return;
                        //}
                    }
                    else
                    {
                        Response.Redirect("../Login.aspx", false);
                        return;
                    }
                }
            }
        }
    }
}