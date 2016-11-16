using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace FleetMgtSolution.Report
{
    public partial class TripLogReport : System.Web.UI.Page
    {
        ReportParameter[] rpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string sdate = DateTime.Now.AddDays(-100).ToShortDateString();
                    string edate = DateTime.Now.ToShortDateString();
                    DBAccess db = new DBAccess();
                    db.AddParameter("@fromDate", DateTime.Now.AddDays(-7));
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
        
        protected void btnGenerate_Click(object sender, EventArgs e)
        {

        }
}
}