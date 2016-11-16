using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Web.Security;
using System.Configuration;

namespace FleetMgtSolution
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lbDate.Text = DateTime.Now.ToString("dddd d MMMM, yyyy");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string rolname = ""; string ursname = ""; string pwd = "";
                bool a = false; bool b = false; bool c = false; bool d = false;
                string val="";
                ursname = txtUsername.Text.Trim();
                pwd = txtPwrd.Text.Trim(); 
                ADAuth.Service ADSvr = new ADAuth.Service();
               val = ADSvr.ADValidateUser(ursname, pwd); 
                val = "true";
                if (val.ToLower().Contains("true"))
                {
                    User usr=new User();
                    usr= UserBLL.GetUserByUserName(ursname);
                    if (usr != null)
                    {
                        if(!usr.isActive.Value)
                        {
                            error.Visible = true;
                            error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Sorry you profile is NOT active.";
                            return;
                        }
                        Session["user"] = usr;
                    }
                    else
                    {
                        usr = Utility.GetUser(ursname);
                        Session["user"] = usr;
                    }
                    FormsAuthentication.SetAuthCookie(ursname, false);
                    string[] rol = Roles.GetRolesForUser(ursname);
                    if (rol.Length != 0)
                    {
                        rolname = rol[0];

                        if (rolname == ConfigurationManager.AppSettings["adminRole"].ToString())
                        { a = true; }
                        if (rolname == ConfigurationManager.AppSettings["GssAdminRole"].ToString())
                        { b = true; }
                        if (rolname == ConfigurationManager.AppSettings["DeptApprverRole"].ToString())
                        { c = true; }
                        if (rolname == ConfigurationManager.AppSettings["HeadDriverRole"].ToString())
                        { d = true; }

                        if (b || d)
                        {
                            if (Request.QueryString["RequestId"] != null)
                            {
                                string Id = Request.QueryString["RequestId"].ToString();
                                Response.Redirect("BackOffice/FleetRequestDetail.aspx?id=" + Id, false);
                                return;
                            }
                            else
                            {
                                Response.Redirect("BackOffice/DashBoard.aspx", false);
                                return;
                            }
                        }
                       
                        else if (a)
                        {

                            Response.Redirect("BackOffice/DashBoard.aspx", false);
                            return;
                        }
                        else if (c)
                        {
                            if (Request.QueryString["id"] != null)
                            {
                                string Id = Request.QueryString["id"].ToString();
                                Response.Redirect("RequestApproval.aspx?id=" + Id, false);
                                return;
                            }
                            else
                            {
                                Response.Redirect("RequestApprovalList.aspx", false);
                                return;
                            }
                        }
                        else
                        {
                            Response.Redirect("Default.aspx", false);
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
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
    }
}