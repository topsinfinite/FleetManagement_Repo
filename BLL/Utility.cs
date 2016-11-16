using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Configuration; 
using System.IO;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.OracleClient;
namespace FleetMgtSolution.BLL
{
    public class Directorate
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
    public class RequestStatus
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
    public class VehicleMap : Vehicle
    {
    }
   public class Utility
    {
       private static string DeptApprRoleName = ConfigurationManager.AppSettings["DeptApprverRole"].ToString();
       public enum FleetRequestStatus
       {
           Pending_Supervisor_Approval=1,
           Pending_FleetManager_Approval=2,
           Pending_Vehicle_Assignment=3,
           Enroute=4,
           Completed=5,
           OnHold=6,
           Declined=7,
           Cancelled=8
       }
       public enum VehicleStatus
       {
           Available = 1,
           Enroute = 2
       }

       public enum AccountCategory
       {
           Individual = 1,
           Corporate = 2
       }
        
       public static string GetVehicleStatus(object o)
       {
           try
           {
               string status = "";
               if (o != null)
               {
                   if (o.ToString() == "1")
                       status = "Available";
                   if (o.ToString() == "2")
                       status = "Enroute";
               }
               return status;
           }
           catch
           {
               return "";
           }
       }
       public static string GetPriority(object o)
       {
           try
           {
               string priority = "";
               if (o != null)
               {
                   if (o.ToString() == "1")
                       priority = "Emergency";
                   if (o.ToString() == "2")
                       priority = "High";
                   if (o.ToString() == "3")
                       priority = "Normal";
                   if (o.ToString() == "4")
                       priority = "Low";
               }
               return priority;
           }
           catch
           {
               return "";
           }
       }
       public static string GetRequestStatus(object o)
       {
           try
           {
               string status = "";
               if (o != null)
               {
                   if (o.ToString() == "1")
                       status = "Pending Supervisor Approval";
                   if (o.ToString() == "2")
                       status = "Pending FleetManager Approval";
                   if (o.ToString() == "3")
                       status = "Pending Vehicle Assignment";
                   if (o.ToString() == "4")
                       status = "Enroute";
                   if (o.ToString() == "5")
                       status = "Completed";
                   if (o.ToString() == "6")
                       status = "OnHold";
                   if (o.ToString() == "7")
                       status = "Declined";
                   if (o.ToString() == "8")
                       status = "Cancelled";
               }
               return status;
           }
           catch
           {
               return "";
           }
       }
       public static List<Directorate> DirectorateList()
       {
           List<Directorate> range = new List<Directorate>
               {
                   new Directorate{DataField="Asset Management Directorate",DataValue="Asset Management Directorate"},
                   new Directorate{DataField="Credit Directorate",DataValue="Credit Directorate"},
                   new Directorate{DataField="Managing Directors Directorate",DataValue="Managing Directors Directorate"},
                   new Directorate{DataField="Finance & Corporate Services Directorate",DataValue="Finance & Corporate Services Directorate"}
               };
           return range;
        }
       public static List<RequestStatus> StatusList()
       {
           List<RequestStatus> range = new List<RequestStatus>
               {
                   new RequestStatus{DataField="Pending FleetManager Approval",DataValue="2"},
                   new RequestStatus{DataField="Pending Vehicle Assignment",DataValue="3"},
                   new RequestStatus{DataField="Enroute",DataValue="4"},
                   new RequestStatus{DataField="Completed",DataValue="5"},
                   new RequestStatus{DataField="OnHold",DataValue="6"},
                   new RequestStatus{DataField="Declined",DataValue="7"},
                   new RequestStatus{DataField="Cancelled",DataValue="8"}

               };
           return range;
       }
       public static string ConnectionStr()//all remember to set this to point to live Finacle
       {
           //string connection = ConfigurationManager.ConnectionStrings["finLiveConnectionString"].ConnectionString;
           string connection = ConfigurationManager.ConnectionStrings["OraConnection"].ConnectionString;
           return connection;
       }

       public static bool LoadDepartments()
       {
           try
           {
               string connection = ConnectionStr(); bool rst = false;
               using (OracleConnection con = new OracleConnection(connection))
               {
                   con.Open();
                   string sql = "select distinct unit, directorate from amcon_master_all order by unit";
                   OracleCommand cmd = new OracleCommand(sql);
                   cmd.Connection = con;
                   OracleDataReader reader = cmd.ExecuteReader();
                   List<Department> DeptList = new List<Department>();
                   if (reader.HasRows)
                   {
                       while (reader.Read())
                       {
                           Department dp = new Department();
                           dp.Name = reader["unit"].ToString();
                           dp.Directorate = reader["directorate"].ToString();
                           DepartmentBLL.AddDepartment(dp);
                           rst = true;
                           //DeptList.Add(dp);

                       }
                   }
                   return rst;
                }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static User GetUser(string staffID)
       {
           try
           {
               string connection = ConnectionStr();
               using (OracleConnection con = new OracleConnection(connection))
               {
                   con.Open();
                   string sql = "select staff_no, last_name, first_name, full_name,email_address, unit Department, directorate "+
                                "from amcon_master_all where staff_no='"+staffID.Trim()+"'";
                   OracleCommand cmd = new OracleCommand(sql);
                   cmd.Connection = con;
                   OracleDataReader reader = cmd.ExecuteReader();
                   //List<User> UserList = new List<User>();
                   User urs = new User();
                   if (reader.HasRows)
                   {
                       while (reader.Read())
                       {

                           urs.StaffName = reader["last_name"].ToString() + " " + reader["first_name"].ToString();
                           urs.Email = reader["email_address"].ToString();
                           urs.DepartmentName = reader["Department"].ToString();
                           urs.Directorate = reader["directorate"].ToString();
                           //UserList.Add(urs);
                       }
                   }
                   else
                   {
                       urs = null;
                   }
                   return urs;
               }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static List<User> GetUserDeptApprovers(string deptName, string Directorate, string username,int location)
       {
           try
           {
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   var urs = from u in db.Users
                             where u.DepartmentName.Trim() == deptName.Trim() && u.Directorate.Trim() == Directorate.Trim() && u.RoleName == DeptApprRoleName && u.StaffID!=username && u.LocationID==location
                             select u;

                   return urs.ToList();
               }
                    
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static List<User> GetUserDeptApprovers(string deptName,string username,int location)
       {
           try
           {
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   var urs = from u in db.Users
                             where u.DepartmentName.Trim() == deptName.Trim() && u.RoleName == DeptApprRoleName && u.StaffID != username && u.LocationID == location
                             select u;

                   return urs.ToList();
               }

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static string GetUsersEmailAdd(string RoleName, int Location)
       {
           try
           {
               string to = "";
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   var urs = from u in db.Users
                             where u.RoleName == RoleName && u.LocationID == Location
                             select u;
                   if (urs.Count() > 0)
                   {
                       foreach (User us in urs)
                       {
                           to += us.Email +",";
                       }
                       to = to.TrimEnd(new char[] { ',' });
                   }
               }
               return to;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static bool AddSmsNotification(SmsNotification sms)
       {
           try
           {
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   db.SmsNotifications.Add(sms);
                   db.SaveChanges();
                   return true;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static decimal GetTripDuration(int stHr, int stMin, int rtHr, int rtMin)
       {
           try
           {
               decimal rtTotalSecs = 0; decimal stTotalSecs = 0; decimal totalDurationInSecs = 0;
               rtTotalSecs = (rtHr * 60 * 60) + (rtMin * 60);
               rtTotalSecs = rtTotalSecs / 86400;
               stTotalSecs = (stHr * 60 * 60) + (stMin * 60);
               stTotalSecs = stTotalSecs / 86400;
               totalDurationInSecs = rtTotalSecs - stTotalSecs;
               return totalDurationInSecs * 24;//convert to hours
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static string GetBatchID()
       {
           try
           {
               int bId = 0; string BatchID = "";
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   var b = from p in db.Batches select p;
                   if (b.Count() > 0)
                   {
                       foreach (Batch bb in b)
                       {
                           bId = bb.LastBatchNo.Value;
                           bb.LastBatchNo = bId + 1;
                           break;
                       }
                   }
                   else
                   {
                       Batch bbnew = new Batch();
                       bbnew.LastBatchNo = 1;
                       bId = 1;
                       db.Batches.Add(bbnew);
                   }
                   db.SaveChanges();
               }
              
               BatchID = "Batch " + bId.ToString().PadLeft(5, '0');
               return BatchID;
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
       public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
       {
           System.Drawing.Bitmap bmpOut = null;
           try
           {

               Bitmap loBMP = new Bitmap(lcFilename);
               ImageFormat loFormat = loBMP.RawFormat;
               decimal lnRatio;
               int lnNewWidth = 0;
               int lnNewHeight = 0;

               //*** If the image is smaller than a thumbnail just return it

               if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)

                   return loBMP;

               if (loBMP.Width > loBMP.Height)
               {

                   lnRatio = (decimal)lnWidth / loBMP.Width;
                   lnNewWidth = lnWidth;
                   decimal lnTemp = loBMP.Height * lnRatio;
                   lnNewHeight = (int)lnTemp;

               }
               else
               {
                   lnRatio = (decimal)lnHeight / loBMP.Height;
                   lnNewHeight = lnHeight;
                   decimal lnTemp = loBMP.Width * lnRatio;
                   lnNewWidth = (int)lnTemp;

               }

               // System.Drawing.Image imgOut =
               //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
               //                              null,IntPtr.Zero);
               // *** This code creates cleaner (though bigger) thumbnails and properly
               // *** and handles GIF files better by generating a white background for
               // *** transparent images (as opposed to black)

               bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
               Graphics g = Graphics.FromImage(bmpOut);
               g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
               g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
               g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
               loBMP.Dispose();

           }

           catch
           {
               return null;
           }
           return bmpOut;

       }

       public static string GetUserIPAdress(HttpContext context)
       {
           string UserIPAddress = "";
           UserIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
           if (string.IsNullOrEmpty(UserIPAddress))
           {
               UserIPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
           }

           return UserIPAddress;
       }
       public static string SendMail(string toList, string from, string ccList, string subject, string body)
       {
           MailMessage message = new MailMessage();
           message.Headers.Add("content-type", "text/html;");
           SmtpClient smtpClient = new SmtpClient();
           string msg = string.Empty;
           try
           {
               MailAddress fromAddress = new MailAddress(from);
               message.From = fromAddress;
               ccList = "temitope.fatayo@amcon.com.ng";
               toList = "temitope.fatayo@amcon.com.ng";
               message.To.Add(toList);
               if (ccList != null && ccList != string.Empty)
                   message.CC.Add(ccList);
               message.Subject = subject;
               message.IsBodyHtml = true;
               message.Body = body;
               smtpClient.Host = ConfigurationManager.AppSettings["smtpServer"].ToString();
               // smtpClient.Host = "lac-la1-s024.leadway.com.ng";
               smtpClient.Port = 25;
               smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
               smtpClient.UseDefaultCredentials = false;
               string exUser = ConfigurationManager.AppSettings["exUser"].ToString(); ;
               string exPwd = ConfigurationManager.AppSettings["exPwd"].ToString(); ;
               smtpClient.Credentials = new System.Net.NetworkCredential(exUser, exPwd);

               smtpClient.Send(message);
               msg = "Successful";
           }
           catch (SmtpException ex)
           {
               msg = ex.Message;
               return msg;
           }
           return msg;
       }
       public static void WriteError(string errorMessage)
       {
           try
           {
               string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-MMM-yy") + ".txt";
               if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
               {
                   File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
               }
               using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
               {
                   w.WriteLine("\r\nLog Entry : ");
                   w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                   string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                                 ". Error Message:" + errorMessage;
                   w.WriteLine(err);
                   w.WriteLine("__________________________");
                   w.Flush();
                   w.Close();
               }
           }
           catch (Exception ex)
           {
               WriteError(ex.Message);
           }

       }

       public static void ExporttoExcel(DataTable table, GridView GridView_Result, string RptName)
       {
           HttpContext.Current.Response.Clear();
           HttpContext.Current.Response.ClearContent();
           HttpContext.Current.Response.ClearHeaders();
           HttpContext.Current.Response.Buffer = true;
           HttpContext.Current.Response.ContentType = "application/ms-excel";
           HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
           HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + RptName + ".xls");

           HttpContext.Current.Response.Charset = "utf-8";
           HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
           //sets font
           HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
           HttpContext.Current.Response.Write("<BR><BR><BR>");
           //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
           HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
             "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
             "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
           //am getting my grid's column headers

           //int columnscount = GridView_Result.Columns.Count;
           int columnscount = table.Columns.Count;

           for (int j = 0; j < columnscount; j++)
           {      //write in new column
               HttpContext.Current.Response.Write("<Td>");
               //Get column headers  and make it as bold in excel columns
               HttpContext.Current.Response.Write("<B>");
               //HttpContext.Current.Response.Write(GridView_Result.Columns[j].HeaderText.ToString());
               HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
               HttpContext.Current.Response.Write("</B>");
               HttpContext.Current.Response.Write("</Td>");
           }
           HttpContext.Current.Response.Write("</TR>");
           foreach (DataRow row in table.Rows)
           {//write in new row
               HttpContext.Current.Response.Write("<TR>");
               for (int i = 0; i < table.Columns.Count; i++)
               {
                   HttpContext.Current.Response.Write("<Td>");
                   HttpContext.Current.Response.Write(row[i].ToString());
                   HttpContext.Current.Response.Write("</Td>");
               }

               HttpContext.Current.Response.Write("</TR>");
           }
           HttpContext.Current.Response.Write("</Table>");
           HttpContext.Current.Response.Write("</font>");
           HttpContext.Current.Response.Flush();
           HttpContext.Current.Response.End();
       }


   }
 
 }
    


