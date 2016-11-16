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
    public partial class TripLogReport1 : System.Web.UI.Page
    {
        ReportParameter[] rpt; CultureInfo culture = new CultureInfo("en-GB");
        private User usr = null;
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
                    BindDept();
                    BindStatusList();
                    BindVehList();
                    BindLocation();
                    string sdate = DateTime.Now.AddDays(3).ToShortDateString();
                    string edate = DateTime.Now.ToShortDateString();
                    DBAccess db = new DBAccess();
                    db.AddParameter("@fromDate", DateTime.Now.AddDays(3));
                    db.AddParameter("@toDate", DateTime.Now);
                    //  db.AddParameter("@orderType",DBNull.Value.ToString());
                    // db.AddParameter("@solID", DBNull.Value.ToString());

                    rpt = new ReportParameter[2];
                    rpt[0] = new ReportParameter("paraFrom", sdate);
                    rpt[1] = new ReportParameter("paraTo", edate);


                    DataSet ds = new DataSet();
                    ds = db.ExecuteDataSet("FMS_TripReport");

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
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BindLocation()
        {
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = LookUpBLL.GetFleetLocationList();
            ddlLocation.DataBind();
        }
        private void BindStatusList()
        {
            ddlStatus.DataTextField = "DataField";
            ddlStatus.DataValueField = "DataValue";
            ddlStatus.DataSource = Utility.StatusList();
            ddlStatus.DataBind();
        }
        private void BindDept()
        {
            ddlDept.DataTextField = "Name";
            ddlDept.DataValueField = "ID";
            ddlDept.DataSource = DepartmentBLL.GetDeptList();
            ddlDept.DataBind();
        }
        private void BindVehList()
        {
            usr = (User)Session["user"];
            ddlVeh.DataTextField = "Name";
            ddlVeh.DataValueField = "ID";
            ddlVeh.DataSource = VehicleBLL.GetVehicleLookUpList(usr.LocationID.Value);
            ddlVeh.DataBind();
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

                rpt = new ReportParameter[7];
                rpt[2] = new ReportParameter("status", "1");
                rpt[3] = new ReportParameter("deptId", "1");
                rpt[4] = new ReportParameter("vehId", "1");
                rpt[5] = new ReportParameter("staffId", "2");
                rpt[6] = new ReportParameter("locationId", "1");
                db.AddParameter("@fromDate", sdate1);
                db.AddParameter("@toDate", edate1);
                if (ddlStatus.SelectedValue != "")
                {
                    //rpt = new ReportParameter[3];
                    db.AddParameter("@status", ddlStatus.SelectedValue);
                    rpt[2] = new ReportParameter("status", ddlStatus.SelectedValue);
                }
                if (ddlDept.SelectedValue != "")
                {
                    db.AddParameter("@deptID", ddlDept.SelectedValue);
                    rpt[3] = new ReportParameter("deptId", ddlDept.SelectedValue);
                }
                if (ddlVeh.SelectedValue != "")
                {
                    db.AddParameter("@vehID", ddlVeh.SelectedValue);
                    rpt[4] = new ReportParameter("vehId", ddlDept.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    db.AddParameter("@StaffID", txtName.Text);
                    rpt[5] = new ReportParameter("staffId", txtName.Text);
                }
                if (ddlLocation.SelectedValue != "")
                {
                    db.AddParameter("@LocationID", ddlLocation.SelectedValue);
                    rpt[6] = new ReportParameter("locationId", ddlLocation.SelectedValue);
                }
                //  db.AddParameter("@orderType",DBNull.Value.ToString());
                // db.AddParameter("@solID", DBNull.Value.ToString());


                rpt[0] = new ReportParameter("paraFrom", sdate1.ToShortDateString());
                rpt[1] = new ReportParameter("paraTo", edate1.ToShortDateString());


                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet("FMS_TripReport");

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
            catch(Exception ex)
            {
                lbMsg.Text = "Error Occurred: " + ex.Message;
            }
        }
    }
}