using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetMgtSolution.BLL
{
    public class IncidenceBLL
    {
        public static bool AddIncidence(IncidentLog incident)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.IncidentLogs.Add(incident);
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
        public static bool UpdateIncidenceLog(IncidentLog incident)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.IncidentLogs.Attach(incident);
                    db.Entry(incident).State = System.Data.Entity.EntityState.Modified;
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
        public static List<IncidentLog> GetIncidenceLogList(int location)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {

                    return db.IncidentLogs.Include("VehicleIncidenceType").Include("Driver").Include("Vehicle").Where(d=>d.Vehicle.LocationID==location).OrderByDescending(d => d.DateAdded).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IncidentLog GetIncidence(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.IncidentLogs.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}