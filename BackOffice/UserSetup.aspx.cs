using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Web.Security;
using System.Configuration;
using System.IO;

namespace FleetMgtSolution.BackOffice
{
    public partial class UserSetup : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole(AdminRole))
            {
                Response.Redirect("../AccessDenied.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                //if (Utility.LoadDepartments())
                //{
                //    success.Visible = true;
                //    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Departments successfully added";
                //}
                BindDept();
                BindGrid();
                BindLocation();
            }
        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        private void BindDept()
        {
            ddlDept.DataTextField = "Name";
            ddlDept.DataValueField = "ID";
            ddlDept.DataSource = DepartmentBLL.GetDeptList();
            ddlDept.DataBind();
        }
        private void BindGrid()
        {
            gvUsers.DataSource = UserBLL.GetUsersList();
            gvUsers.DataBind();
        }
        private void Reset()
        {
            txtDir.Text = ""; txtEmail.Text = "";
            txtfName.Text = ""; ddlDept.SelectedValue = "";
            ddlRole.SelectedValue = ""; txtStaffID.Text = "";
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string staffID = txtStaffID.Text.Trim();
                MembershipUserCollection usercol = Membership.FindUsersByName(staffID);
                if (usercol.Count != 0)
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> This user already exist, pls add another user";
                    return;
                }
                //List<User> ursList = new List<User>();
                User urs = new FleetMgtSolution.User();
                urs = Utility.GetUser(staffID);
                //if (ursList.Count > 0)
                if (urs!=null)
                {
                    dvDetail.Visible = true;
                    //foreach (var u in ursList)
                    {
                        txtfName.Text = urs.StaffName;
                        txtEmail.Text = urs.Email;
                        txtDir.Text = urs.Directorate;
                        //ddlDept.SelectedItem.Text = u.DepartmentName.Trim();
                        //ddlDept.
                    }
                }
                else
                {
                    Reset();
                    dvDetail.Visible = true;
                    txtfName.Enabled = true;
                    txtEmail.Enabled = true;
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> User was NOT found. However, You can manually add user details!!.";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred "+ex.Message+" . Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string email=txtEmail.Text;
                string usrname=txtStaffID.Text;
                string fname = txtfName.Text; 
                User usr = new User();
                usr.StaffName = fname;
                usr.StaffID = txtStaffID.Text.Trim();
                usr.DepartmentID = Convert.ToInt32(ddlDept.SelectedValue);
                usr.Directorate = txtDir.Text;
                usr.Email = email;
                usr.isHoD = false;
                usr.RoleName=ddlRole.SelectedValue;
                usr.DateAdded = DateTime.Now;
                usr.DepartmentName = ddlDept.SelectedItem.Text;
                if (User.Identity.IsAuthenticated)
                    usr.AddedBy = User.Identity.Name;
                usr.isActive = true;
                usr.LocationID = int.Parse(ddlLocation.SelectedValue);
                MembershipUser user = Membership.CreateUser(usrname, "Pass@word",email);
                if (UserBLL.AddUser(usr))
                {
                    Roles.AddUserToRole(usrname, ddlRole.SelectedValue);
                    try
                    {
                        if (chkHOD.Checked)
                        {
                            int deptID = int.Parse(ddlDept.SelectedValue);
                            using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                            {
                                var q = from p in db.Users where p.DepartmentID == deptID select p;
                                if (q != null)
                                {
                                    if (q.Count() > 0)
                                    {
                                        foreach (User u in q)
                                        {
                                            u.isHoD = false;
                                        }
                                        db.SaveChanges();
                                    }
                                }
                            }
                            User newUsr = UserBLL.GetUserByUserName(usrname);
                            newUsr.isHoD = true;
                            UserBLL.UpdateUser(newUsr);
                        }
                    }
                    catch { }
                    //sending mail
                    string body = "";
                    string from = ConfigurationManager.AppSettings["exUser"].ToString();
                    string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                    string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                    string subject = "User Creation Notification";
                    string FilePath = Server.MapPath("EmailTemplates/");
                    if (File.Exists(FilePath + "NewUserCreated.htm"))
                    {
                        FileStream f1 = new FileStream(FilePath + "NewUserCreated.htm", FileMode.Open);
                        StreamReader sr = new StreamReader(f1);
                        body = sr.ReadToEnd();
                        body = body.Replace("@add_appLogo", appLogo);
                        body = body.Replace("@add_siteUrl", siteUrl);
                        body = body.Replace("@add_fname", fname);
                        body = body.Replace("@add_username", usrname); //Replace the values from DB or any other source to personalize each mail.  
                        f1.Close();
                    }
                    string rst = Utility.SendMail(email, from, "", subject, body);
                    if (rst.Contains("Successful"))
                    {
                        Reset(); BindGrid();
                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> User was successfully created!!. A mail has been sent to the user";
                        return;
                    }
                    else
                    {
                        Reset(); BindGrid();
                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> User was successfully created!!. A mail could NOT be sent to the user at this time";
                        return;
                    }
                }
                else
                {
                    Membership.DeleteUser(usrname);
                    error.Visible = true;
                    error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> User Account could not be created. Kindly try again. If error persist contact Administrator!!.";
                    return;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred " + ex.Message + " . Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvUsers.SelectedDataKey.Value.ToString());
                Response.Redirect(string.Format("UserDetail.aspx?id={0}", id), false);
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!String.IsNullOrWhiteSpace(txtSea.Value))
                {
                    string srch = txtSea.Value;
                    gvUsers.DataSource = UserBLL.GetUserByUserNameLst(srch);
                    gvUsers.DataBind();
                }
            }catch
            {
            }
        }

        protected void btnClr_Click(object sender, EventArgs e)
        {
            try
            {
                txtSea.Value = "";
                BindGrid();
            }
            catch
            {
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
            }catch(Exception ex)
            {
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRole.SelectedValue == "DeptApprover")
                {
                    chkHOD.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}