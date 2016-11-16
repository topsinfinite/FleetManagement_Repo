using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Globalization;

namespace FleetMgtSolution.Report
{
    public partial class VehicleLogReport : System.Web.UI.Page
    {
        ReportParameter[] rpt; CultureInfo culture = new CultureInfo("en-GB");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("../Login.aspx", false);
            }
            try
            {
                if (!IsPostBack)
                {
                    BindType();
                    BindLocation();
                    string sdate = DateTime.Now.AddDays(10).ToShortDateString();
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
                    ds = db.ExecuteDataSet("FMS_VehicleLogReport");

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
            catch(Exception ex)
            {
                 lbMsg.Text = "An error occurred: "+ ex.Message;
            }
        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        private void BindType()
        {
            ddlType.DataValueField = "ID";
            ddlType.DataTextField = "Name";
            ddlType.DataSource = LookUpBLL.GetIncidenceTypeList();
            ddlType.DataBind();
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
                rpt[2] = new ReportParameter("TypeId", "1");
                rpt[3] = new ReportParameter("locationId", "1");
                db.AddParameter("@fromDate", sdate1);
                db.AddParameter("@toDate", edate1);
                if (ddlType.SelectedValue != "")
                {
                    db.AddParameter("@IncidenceType", ddlType.SelectedValue);
                    rpt[2] = new ReportParameter("TypeId", ddlType.SelectedValue);
                }
                if (ddlLocation.SelectedValue != "")
                {
                    db.AddParameter("@LocationID", ddlLocation.SelectedValue);
                    rpt[1] = new ReportParameter("locationId", ddlLocation.SelectedValue);
                }
                rpt[0] = new ReportParameter("paraFrom", sdate1.ToShortDateString());
                rpt[1] = new ReportParameter("paraTo", edate1.ToShortDateString());

                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet("FMS_VehicleLogReport");

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
                lbMsg.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}