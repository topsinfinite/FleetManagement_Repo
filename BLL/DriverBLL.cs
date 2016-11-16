using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetMgtSolution.BLL
{
    public class DriverBLL
    {
        public static bool AddDriver(Driver driver)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.Drivers.Add(driver);
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
        public static bool UpdateDriver(Driver driver)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.Drivers.Attach(driver);
                    db.Entry(driver).State = System.Data.Entity.EntityState.Modified;
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
        public static List<Driver> GetDriversList(int Location)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    //var d = (from p in db.Drivers.OrderBy(k => k.Name)
                    //         select new DepartmentMap
                    //         {
                    //             ID = p.ID,
                    //             Name = p.Name.ToUpper(),
                    //             Directorate = p.Directorate.ToUpper()
                    //         });
                    //return d.ToList();
                    return db.Drivers.Include("FleetLocation").Include("DriverEmployer").Include("Vehicle").Where(k=>k.DelFlg=="N" && k.LocationID==Location).OrderBy(d => d.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Driver GetDriver(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.Drivers.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}