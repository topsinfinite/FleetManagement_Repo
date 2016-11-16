using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using FleetMgtSolution.BLL;
using System.Globalization;

namespace FleetMgtSolution.Report
{
    public partial class FMSVehUtilizationReport : System.Web.UI.Page
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
                    BindVeh();
                    BindLocation();
                    string sdate = DateTime.Now.AddDays(1).ToShortDateString();
                    string edate = DateTime.Now.ToShortDateString();
                    DBAccess db = new DBAccess();
                    db.AddParameter("@fromDate", DateTime.Now.AddDays(10));
                    db.AddParameter("@toDate", DateTime.Now);
                    //  db.AddParameter("@orderType",DBNull.Value.ToString());
                    // db.AddParameter("@solID", DBNull.Value.ToString());

                    rpt = new ReportParameter[2];

                    rpt[0] = new ReportParameter("paraFrom", sdate);
                    rpt[1] = new ReportParameter("paraTo", edate);


                    DataSet ds = new DataSet();
                    ds = db.ExecuteDataSet("FMS_FleetUtilizationReport");

                    ReportDataSource datasource = new
                      ReportDataSource("dsFMS",
                      ds.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(rpt);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // lbmsg.Text = "Sorry, no products under this category!";
                    }

                    ReportViewer1.LocalReport.Refresh();
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void BindVeh()
        {
            usr = (User)Session["user"];
            ddlDept.DataTextField = "Name";
            ddlDept.DataValueField = "ID";
            ddlDept.DataSource = VehicleBLL.GetVehicleLookUpList(usr.LocationID.Value);
            ddlDept.DataBind();
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
                lbMsg.Text = "";
                string sdate = txtvsrt.Text;
                string edate = txtvstp.Text;
                DateTime sdate1 = DateTime.Parse(sdate, culture);
                DateTime edate1 = DateTime.Parse(edate, culture);
                DBAccess db = new DBAccess();

                rpt = new ReportParameter[4];
                rpt[2] = new ReportParameter("vehId", "1");
                rpt[3] = new ReportParameter("locationId", "1");
                db.AddParameter("@fromDate", sdate1);
                db.AddParameter("@toDate", edate1);
                if (ddlDept.SelectedValue != "")
                {
                    db.AddParameter("@vehId", ddlDept.SelectedValue);
                    rpt[2] = new ReportParameter("vehId", ddlDept.SelectedValue);
                }
                if (ddlLocation.SelectedValue != "")
                {
                    db.AddParameter("@LocationID", ddlLocation.SelectedValue);
                    rpt[3] = new ReportParameter("locationId", ddlLocation.SelectedValue);
                }
                rpt[0] = new ReportParameter("paraFrom", sdate1.ToShortDateString());
                rpt[1] = new ReportParameter("paraTo", edate1.ToShortDateString());

                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet("FMS_FleetUtilizationReport");

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