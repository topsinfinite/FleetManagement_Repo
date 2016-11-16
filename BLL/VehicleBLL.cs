using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetMgtSolution.BLL
{
    public class VehicleBLL
    {
        public static bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.Vehicles.Add(vehicle);
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
        public static bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.Vehicles.Attach(vehicle);
                    db.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
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
        public static List<Vehicle> GetVehicleList(int Location,int Id=-1)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    if (Id != -1)
                    {
                        return db.Vehicles.Include("FleetLocation").Include("VehicleMaker").Include("VehicleType").Include("TrackerCompany1").Include("InsuranceCompany1").Include("Driver").Where (p=>p.ID==Id && p.DelFlg=="N" && p.LocationID==Location).OrderBy(d => d.Name).ToList();
                    }
                    else
                    {
                        return db.Vehicles.Include("FleetLocation").Include("VehicleMaker").Include("VehicleType").Include("Driver").Where(k=>k.DelFlg=="N" && k.LocationID==Location).OrderBy(d => d.Name).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<VehicleMap> GetVehicleLookUpList(int Location)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    var q = from v in db.Vehicles.Where(k=>k.DelFlg=="N" && k.LocationID==Location)
                            select new VehicleMap
                            {
                                ID = v.ID,
                                Name = v.Name + "(" + v.PlateNo + ")",
                            };
                    return q.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Vehicle GetVehicle(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.Vehicles.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}