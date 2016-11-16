using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.BackOffice
{
    public partial class VehicleDetail : System.Web.UI.Page
    {
        private User usr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["user"] != null)
                {
                    usr = (User)Session["user"];
                }
                else
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int Id = Convert.ToInt32(Request.QueryString["id"].ToString());
                        hid.Value = Id.ToString();
                        BindGrid(Id); BindTrip();
                    }
                    else
                    {
                        Response.Redirect("ManageVehicle.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        private void BindGrid(int id)
        {
            usr = (User)Session["user"];
            gvheader.DataSource = VehicleBLL.GetVehicleList(usr.LocationID.Value,id);
            gvheader.DataBind();
        }
        private void BindTrip()
        {
            usr = (User)Session["user"];
            int id = int.Parse(hid.Value);
            gvTrip.DataSource = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(p=>p.AssignedVehicle==id).ToList();
            gvTrip.DataBind();
        }
        protected string GetStatus(object o)
        {
            try
            {
                return Utility.GetVehicleStatus(o);
            }
            catch
            {
                return "";
            }
        }
        protected string GetFleetStatus(object o)
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
        protected void gvTrip_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "view")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                int key = Convert.ToInt32(gvTrip.DataKeys[index].Value.ToString());
                Label lbst = gvTrip.Rows[index].FindControl("lbStatus") as Label;
                Response.Redirect(String.Format("FleetRequestDetail.aspx?id={0}&status={1}", key, lbst.Text), false);

            }
        }

        protected void gvTrip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTrip.PageIndex = e.NewPageIndex;
                BindTrip();
            }
            catch
            {
            }
        }
    }
}