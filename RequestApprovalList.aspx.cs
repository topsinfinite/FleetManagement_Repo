using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution
{
    public partial class RequestApprovalList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindGrid(); BindGridAppr();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
                return;
            }
        }
        private void BindGrid()
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
            gvRequest.DataSource = TripBLL.GetFleetRequestByApproval(usr.ID,(int)Utility.FleetRequestStatus.Pending_Supervisor_Approval);
            gvRequest.DataBind();
        }
        private void BindGridAppr()
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
            gvRequestAppr.DataSource = TripBLL.GetFleetRequestApprovedByUser(usr.ID, (int)Utility.FleetRequestStatus.Pending_Supervisor_Approval);
            gvRequestAppr.DataBind();
        }
        protected void gvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRequest.PageIndex = e.NewPageIndex;
                BindGrid();
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
                    Response.Redirect(String.Format("RequestApproval.aspx?id={0}", key), false);

                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
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

        protected void gvRequest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRequestAppr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRequestAppr.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            {
            }
        }

        protected void gvRequestAppr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    int key = Convert.ToInt32(gvRequestAppr.DataKeys[index].Value.ToString());
                    Response.Redirect(String.Format("RequestApproval.aspx?id={0}", key), false);

                }
            }
            catch
            {
            }
        }

        protected void gvRequestAppr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(txtSea.Value)){
                    int RequestID = Convert.ToInt32(txtSea.Value);
                    gvRequestAppr.DataSource = TripBLL.GetTripListByID(RequestID).Where(p=>p.Status>(int)Utility.FleetRequestStatus.Pending_Supervisor_Approval).ToList();
                    gvRequestAppr.DataBind();
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        protected void btnClr_Click(object sender, EventArgs e)
        {
            try
            {
                txtSea.Value = "";
                BindGridAppr();
            }
            catch
            {
            }
        }

        protected void lnkViewHistorical_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvHistorical.Visible)
                {
                    dvHistorical.Visible = false;
                    lnkViewHistorical.Text="View Historical Records::";
                }else
                {
                    dvHistorical.Visible = true;
                    lnkViewHistorical.Text = "Hide Historical Records::";
                }
            }
            catch
            {
            }
        }
    }
}