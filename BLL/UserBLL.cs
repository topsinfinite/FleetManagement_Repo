using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FleetMgtSolution;
using System.Data.Objects;
 

namespace FleetMgtSolution.BLL
{
    public class UserBLL
    {
        public static bool AddUser(User usr)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static bool UpdateUser(User usr)
        {
            try
            {
                bool retVal = false;
                using (var db = new FleetMgtSysDBEntities())
                {
                    db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    retVal = true;
                }
                return retVal;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.InnerException);
                throw ex;
            }
        }
       public static User GetUserByUserName(string username)
        {
            try
            {
                using (FleetMgtSysDBEntities db=new FleetMgtSysDBEntities())
                {
                    var user = from usr in db.Users.Include("FleetLocation")
                               where usr.StaffID == username
                           select usr;
                    return user.FirstOrDefault<User>();
                }
                 
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
       public static List<User> GetUserByUserNameLst(string searchTerm)
       {
           try
           {
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   var user = db.Users.Include("FleetLocation").Where(p=>p.StaffName.Contains(searchTerm));
                              
                   if (user != null)
                   {
                       return user.ToList();
                   }
                   else
                   {
                       return null;
                   }
               }

           }
           catch (Exception ex)
           {
               Utility.WriteError("Error Msg: " + ex.Message);
               throw ex;
           }
       }
       public static User GetByID(int Id)
       {
           try
           {
               using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
               {
                   return db.Users.Find(Id);
               }
           }
           catch (Exception ex)
           {
               Utility.WriteError("Error Msg: " + ex.Message);
               throw ex;
           }
       }
        public static List<User> GetUserByID(int Id)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    var user = from usr in db.Users.Include("FleetLocation")
                               where usr.ID == Id
                               select usr;
                    return user.ToList<User>();
                }
                // return null;
                
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static List<User> GetUsersList()
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    var usrList = from p in db.Users.Include("FleetLocation") select p;
                    return usrList.ToList<User>();
                }
            }
            catch(Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static bool UpdateUserById(int Id,bool status)
        {
            try
            {
                bool result = false;
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    var usr =(from p in db.Users 
                              where p.ID==Id 
                              select p).FirstOrDefault();
                    if (status)
                    {
                        usr.isActive= false;
                    }
                    else
                    {
                        usr.isActive = true;
                    }
                    db.SaveChanges();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }

        public static User GetDeptHOD(int deptId)
        {
            try
            {
                using (FleetMgtSysDBEntities db = new FleetMgtSysDBEntities())
                {
                    User usr = (from p in db.Users where p.DepartmentID == deptId  && p.isHoD==true select p).FirstOrDefault();
                    if (usr != null)
                    {
                        return usr;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}