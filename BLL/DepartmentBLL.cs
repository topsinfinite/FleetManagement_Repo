using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetMgtSolution.BLL
{
    public class DepartmentMap : Department { }
    public class DepartmentBLL
    {
        public static bool AddDepartment(Department dept)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.Departments.Add(dept);
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
        public static bool Update(Department dept)
        {
            try
            {
                bool rst = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.Departments.Attach(dept);
                    db.Entry(dept).State = System.Data.Entity.EntityState.Modified;
                    //db.ObjectStateManager.ChangeObjectState(auction, System.Data.EntityState.Modified);
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
        public static List<DepartmentMap> GetDeptList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    var d = (from p in db.Departments.OrderBy(k=>k.Name)
                             select new DepartmentMap
                             {
                                 ID=p.ID,
                                 Name=p.Name.ToUpper(),
                                 Directorate=p.Directorate.ToUpper()
                             });
                    return d.ToList();   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Department GetDepartment(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    return db.Departments.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}