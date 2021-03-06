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
    
    public partial class Trip
    {
        public int ID { get; set; }
        public string Destination { get; set; }
        public string Purpose { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> TripDate { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string ExpectedDepartureTime { get; set; }
        public string ExpectedReturnTime { get; set; }
        public Nullable<decimal> ExpectedDuration { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string InitiatorID { get; set; }
        public string InitiatorName { get; set; }
        public Nullable<int> ApproverIDLeve1 { get; set; }
        public Nullable<int> ApproverIDlevel2 { get; set; }
        public Nullable<System.DateTime> DateApproved { get; set; }
        public Nullable<int> AssignedBy { get; set; }
        public Nullable<System.DateTime> DateAssigned { get; set; }
        public Nullable<int> AssignedVehicle { get; set; }
        public Nullable<decimal> MileageAtDeparture { get; set; }
        public Nullable<decimal> MileageOnArrival { get; set; }
        public Nullable<int> priority { get; set; }
        public string Note { get; set; }
        public string InitiatorEmail { get; set; }
        public string Location { get; set; }
        public string ActualDepartureTime { get; set; }
        public string ActualRetunTime { get; set; }
        public Nullable<decimal> ActualDuration { get; set; }
        public string PickupLocation { get; set; }
        public string BatchID { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual FleetLocation FleetLocation { get; set; }
    }
}
