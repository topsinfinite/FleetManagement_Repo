//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FleetMgtSolution
{
    using System;
    using System.Collections.Generic;
    
    public partial class FleetLocation
    {
        public FleetLocation()
        {
            this.Trips = new HashSet<Trip>();
            this.Users = new HashSet<User>();
            this.Vehicles = new HashSet<Vehicle>();
            this.Drivers = new HashSet<Driver>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
