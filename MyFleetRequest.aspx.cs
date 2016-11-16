using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution
{
    public partial class MyFleetRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    BindGrid(User.Identity.Name);
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
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
        private void BindGrid(string usrname)
        {
            gvRequest.DataSource = TripBLL.GetFleetRequestByUser(usrname).OrderByDescending(p=>p.DateAdded).ToList();
            gvRequest.DataBind();
        }
        protected void gvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRequest.PageIndex = e.NewPageIndex;
                BindGrid(User.Identity.Name);
            }
            catch
            {
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
                    Response.Redirect(String.Format("RequestDetail.aspx?id={0}", key), false);

                }
            }
            catch
            {
            }
        }

        protected void gvRequest_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}