using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.BackOffice
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User usr = null;
            if (!IsPostBack)
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
                int totalPendAppr = 0; int totalPendAss = 0; int totalCmpRequest = 0; int totalVehPool = 0; int totalvehEnt = 0;
                totalPendAppr = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(t => t.Status == (int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).Count();
                totalPendAss = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(t => t.Status == (int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment).Count();
                totalCmpRequest = TripBLL.GetTripListByAdmin(usr.LocationID.Value).Where(t => t.Status == (int)Utility.FleetRequestStatus.Completed).Count();
                totalVehPool = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(t => t.Status == (int)Utility.VehicleStatus.Available).Count();
                totalvehEnt = VehicleBLL.GetVehicleList(usr.LocationID.Value).Where(t => t.Status == (int)Utility.VehicleStatus.Enroute).Count();
                //lnkPendAppr.Text = "Total Number of Requests pending Approval ("+totalPendAppr.ToString()+")";
                //lnkPendAppr.NavigateUrl = "ManageFleetRequest.aspx?stat=" + ((int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).ToString();
                lnkPendAppr1.InnerHtml = "Total Number of Requests pending Approval<b> (" + totalPendAppr.ToString() + ")</b>";
                lnkPendAppr1.HRef = "ManageFleetRequest.aspx?stat=" + ((int)Utility.FleetRequestStatus.Pending_FleetManager_Approval).ToString();

                lnkpendAss.InnerHtml = "Total Number of Requests pending Vehicle Assignment <b>(" + totalPendAss.ToString() + ")</b>";
                lnkpendAss.HRef = "ManageFleetRequest.aspx?stat=" + ((int)Utility.FleetRequestStatus.Pending_Vehicle_Assignment).ToString();

                lnkCmpReq.InnerHtml = "Total Number of Completed Request (<b>" + totalCmpRequest.ToString() + ")</b>";
                lnkCmpReq.HRef = "ManageFleetRequest.aspx?stat=" + ((int)Utility.FleetRequestStatus.Completed).ToString();

                lnkVehPool.InnerHtml = "Total Number of Vehicles Available in the Pool <b>("+totalVehPool+")</b>";
                lnkVehPool.HRef = "ManageVehicle.aspx?stat=" + ((int)Utility.VehicleStatus.Available).ToString();

                lnkvehEnt.InnerHtml = "Total Number of Vehicles Enroute <b>(" + totalvehEnt + ")</b>";
                lnkvehEnt.HRef = "ManageVehicle.aspx?stat=" + ((int)Utility.VehicleStatus.Enroute).ToString();
            }
        }
    }
}