using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace FleetMgtSolution.BLL
{
    public class TripBLL
    {
      
        public static bool AddTrip(Trip trip)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.Trips.Add(trip);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static bool UpdateTrip(Trip trip)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.Trips.Attach(trip);
                    db.Entry(trip).State = System.Data.Entity.EntityState.Modified;
                    //db.ObjectStateManager.ChangeObjectState(auction, System.Data.EntityState.Modified);
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static IEnumerable<Trip> GetFleetRequestByUser(string username)
        {
            try
            {
                using(var db = new FleetMgtSysDBEntities())
                {
                    var q = from p in db.Trips.Include("User").Include("Department")
                            where p.InitiatorID == username
                            select p;
                    return q.ToList();

                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static IEnumerable<Trip> GetFleetRequestByApproval(int userId,int status)
        {
            try
            {
                using (var db = new FleetMgtSysDBEntities())
                {
                    var q = from p in db.Trips.Include("User").Include("Department")
                            where p.ApproverIDLeve1 == userId && p.Status==status
                            select p;
                    return q.OrderByDescending(p=>p.DateAdded).ToList();

                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static IEnumerable<Trip> GetFleetRequestApprovedByUser(int userId, int status)
        {
            try
            {
                using (var db = new FleetMgtSysDBEntities())
                {
                    var q = from p in db.Trips.Include("User").Include("Department")
                            where p.ApproverIDLeve1 == userId && p.Status > status
                            select p;
                    return q.OrderByDescending(p=>p.DateAdded).ToList();

                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static List<Trip> GetTripListByID(int Id)
        {
            try
            {
                using (var db = new FleetMgtSysDBEntities())
                {
                    return db.Trips.Include("User").Include("User1").Include("User2").Include("Vehicle.Driver").Include("Vehicle").Include("Department").Where(t => t.ID == Id).OrderByDescending(p=>p.DateAdded).ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static List<Trip> GetTripListByAdmin(int Location,string RequestId = "", string status = "", string Initiator = "", string from = "", string toDate = "")
        {
            try
            {
                CultureInfo culture = new CultureInfo("en-GB");
                using (var db = new FleetMgtSysDBEntities())
                {
                    var result= db.Trips.Include("FleetLocation").Include("User").Include("User1").Include("User2").Include("Vehicle.Driver").Include("Vehicle").Include("Department").Where(t => t.Status>1 && t.LocationID==Location);//greater than request yet to be approved by supervisor
                    if(!String.IsNullOrWhiteSpace(RequestId))
                    {
                        int rID = Convert.ToInt32(RequestId);
                        result = result.Where(p => p.ID == rID);
                    }
                    if (!String.IsNullOrWhiteSpace(status))
                    {
                        int stat = Convert.ToInt32(status);
                        result = result.Where(s => s.Status == stat);
                    }
                    if (!String.IsNullOrWhiteSpace(Initiator))
                    {
                        result = result.Where(i => i.InitiatorID == Initiator);
                    }
                    if (!String.IsNullOrWhiteSpace(from))
                    {
                        DateTime frm = DateTime.Parse(from, culture);
                        result = result.Where(i => i.TripDate.Value>=frm);
                    }
                    if (!String.IsNullOrWhiteSpace(toDate))
                    {
                        DateTime to = DateTime.Parse(toDate, culture);
                        result = result.Where(i => i.TripDate.Value <= to);
                    }
                    return result.OrderByDescending(p => p.DateAdded).ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
 
        public static Trip GetTripDetailByID(int Id)
        {
            try
            {
                using (var db = new FleetMgtSysDBEntities())
                {
                    return db.Trips.Find(Id);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
         
    }
}