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
    
    public partial class VehicleMaker
    {
        public VehicleMaker()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
