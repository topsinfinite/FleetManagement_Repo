using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetMgtSolution.BLL
{
    public class LookUpBLL
    {
        public static bool AddVehicleMaker(VehicleMaker maker)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.VehicleMakers.Add(maker);
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
        public static bool UpdateVehicleMaker(VehicleMaker maker)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.VehicleMakers.Attach(maker);
                    db.Entry(maker).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<VehicleMaker> GetVehicleMakerList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleMakers.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static VehicleMaker GetVehicleMaker(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleMakers.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddVehicleType(VehicleType type)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.VehicleTypes.Add(type);
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
        public static bool UpdateVehicleType(VehicleType type)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.VehicleTypes.Attach(type);
                    db.Entry(type).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<VehicleType> GetVehicleTypeList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleTypes.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static VehicleType GetVehicleType(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleTypes.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddIncidenceType(VehicleIncidenceType type)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.VehicleIncidenceTypes.Add(type);
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
        public static bool UpdateIncidenceType(VehicleIncidenceType type)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.VehicleIncidenceTypes.Attach(type);
                    db.Entry(type).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<VehicleIncidenceType> GetIncidenceTypeList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleIncidenceTypes.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static VehicleIncidenceType GetIncidenceType(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.VehicleIncidenceTypes.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddInsuranceCompany(InsuranceCompany coy)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.InsuranceCompanies.Add(coy);
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
        public static bool UpdateInsuranceCompany(InsuranceCompany coy)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.InsuranceCompanies.Attach(coy);
                    db.Entry(coy).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<InsuranceCompany> GetInsuranceCoyList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.InsuranceCompanies.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static InsuranceCompany GetInsuranceCompany(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.InsuranceCompanies.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddTrackerCompany(TrackerCompany coy)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.TrackerCompanies.Add(coy);
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
        public static bool UpdateTrackerCompany(TrackerCompany coy)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.TrackerCompanies.Attach(coy);
                    db.Entry(coy).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<TrackerCompany> GetTrackerCoyList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.TrackerCompanies.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TrackerCompany GetTrackerCompany(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.TrackerCompanies.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddDriverEmployer(DriverEmployer coy)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.DriverEmployers.Add(coy);
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
        public static bool UpdateDriverEmployer(DriverEmployer coy)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.DriverEmployers.Attach(coy);
                    db.Entry(coy).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<DriverEmployer> GetDriverEmployerList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.DriverEmployers.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DriverEmployer GetDriverEmployer(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.DriverEmployers.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddFleetLocation(FleetLocation loc)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.FleetLocations.Add(loc);
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
        public static bool UpdateFleetLocation(FleetLocation loc)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.FleetLocations.Attach(loc);
                    db.Entry(loc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
        public static IEnumerable<FleetLocation> GetFleetLocationList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.FleetLocations.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FleetLocation GetFleetLocation(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.FleetLocations.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}