using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using System.Web.Security;
using System.Configuration;

namespace FleetMgtSolution.BackOffice
{
    public partial class UserDetail : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string deptApprRole = ConfigurationManager.AppSettings["DeptApprverRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole(AdminRole))
            {
                Response.Redirect("../AccessDenied.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int Id = Convert.ToInt32(Request.QueryString["id"].ToString());
                        hid.Value = Id.ToString();
                        BindGrid(Id);
                        BindDept();
                        BindLocation();
                        User usr = UserBLL.GetByID(Id);
                        if (usr != null)
                        {
                            if (usr.RoleName == deptApprRole)
                            {
                                chkHOD.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("UserSetup.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                    Utility.WriteError("Error: " + ex.Message);
                }
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
        private void BindGrid(int Id)
        {
            gvheader.DataSource = UserBLL.GetUserByID(Id);
            gvheader.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Label lbstaID = gvheader.Rows[0].FindControl("lbEbl") as Label;
                bool status = bool.Parse(lbstaID.Text.Trim());
                int id = Convert.ToInt32(hid.Value);
                bool rst = UserBLL.UpdateUserById(id, status);
                if (rst)
                {
                    BindGrid(id);
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Update was successful";
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hid.Value);
                Label lbDeptID = gvheader.Rows[0].FindControl("lbDept") as Label;
                Label lbstaID = gvheader.Rows[0].FindControl("lbInit") as Label;
                string usrname = lbstaID.Text;
                User usr=UserBLL.GetUserByUserName(usrname);
                if (ddlRole.SelectedValue != "")
                {
                    Label lbRole = gvheader.Rows[0].FindControl("lbrole") as Label;
                    Roles.RemoveUserFromRole(usrname, lbRole.Text);
                    Roles.AddUserToRole(usrname, ddlRole.SelectedValue);
                    usr.RoleName = ddlRole.SelectedValue;
                }
                if (chkHOD.Checked)///check if the user is HOD
                {
                    using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                    {
                        int deptID = int.Parse(lbDeptID.Text);
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
                    usr.isHoD = true;
                }
                if (ddlLocation.SelectedValue != "")
                {
                    usr.LocationID = int.Parse(ddlLocation.SelectedValue);
                }
                if (ddlDept.SelectedValue != "")
                {
                    usr.DepartmentID = int.Parse(ddlDept.SelectedValue);
                    usr.DepartmentName = ddlDept.SelectedItem.Text;
                }
                if (UserBLL.UpdateUser(usr))
                {
                    //Roles.AddUserToRole(usrname, ddlRole.SelectedValue);
                    BindGrid(id);
                    success.Visible = true;
                    success.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> Update was successful";
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
    }
}