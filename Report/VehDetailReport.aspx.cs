using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Data;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.Report
{
     
    public partial class VehDetailReport : System.Web.UI.Page
    {
        private User usr = null;
        ReportParameter[] rpt; CultureInfo culture = new CultureInfo("en-GB");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("../Login.aspx", false);
                }
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
                    BindVehList();
                    BindLocation();
                    DBAccess db = new DBAccess();

                    DataSet ds = new DataSet();
                    ds = db.ExecuteDataSet("FMS_VehicleReport");
                    rpt = new ReportParameter[1];
                    rpt[0] = new ReportParameter("vehId", "1");

                    ReportDataSource datasource = new
                      ReportDataSource("dsFMS",
                      ds.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                   // ReportViewer1.LocalReport.SetParameters(rpt);
                    //ReportViewer1.LocalReport.DataSources.Add(datasource);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // lbmsg.Text = "Sorry, no products under this category!";
                    }

                   // ReportViewer1.LocalReport.Refresh();
                }


            }
            catch (Exception ex)
            {
                lbMsg.Text = "Error Occurred: " + ex.Message;
            }
        }
        private void BindVehList()
        {
            usr = (User)Session["user"];
            ddlVeh.DataTextField = "Name";
            ddlVeh.DataValueField = "ID";
            ddlVeh.DataSource = VehicleBLL.GetVehicleLookUpList(usr.LocationID.Value);
            ddlVeh.DataBind();
        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                
                    DBAccess db = new DBAccess();
                    rpt = new ReportParameter[2];
                    DataSet ds = new DataSet();
                    rpt[0] = new ReportParameter("vehId", "1");
                    rpt[1] = new ReportParameter("locationId", "1");
                    if (ddlVeh.SelectedValue != "")
                    {
                        db.AddParameter("@vehId", ddlVeh.SelectedValue);
                        rpt[0] = new ReportParameter("vehId", ddlVeh.SelectedValue);
                    }
                    if (ddlLocation.SelectedValue != "")
                    {
                        db.AddParameter("@LocationID", ddlLocation.SelectedValue);
                        rpt[1] = new ReportParameter("locationId", ddlLocation.SelectedValue);
                    }
                    ds = db.ExecuteDataSet("FMS_VehicleReport");
                    ReportDataSource datasource = new
                      ReportDataSource("dsFMS",
                      ds.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(rpt);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                         lbMsg.Text = "Sorry, no record found!";
                    }

                    ReportViewer1.LocalReport.Refresh();


            }
            catch (Exception ex)
            {
                lbMsg.Text = "Error Occurred: " + ex.Message;
            }
        }
    }
}