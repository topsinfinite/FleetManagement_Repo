<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="LookUpForms.aspx.cs" Inherits="FleetMgtSolution.BackOffice.LookUpForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <span class="span12 label label-info pageheader-text">Look-Up Forms</span>
    </div>
<div class="page-body">
  <div class="page-body-wrapper">
   <div class="inner-header">Lookup Forms</div>
    <div class="row-fluid">
     <div class="lookup span5">
      <ul>
        <li> <a href="VehicleMaker.aspx" class="btn ">Setup vehicle Maker </a></li>
        <li> <a href="VehicleTypePage.aspx" class="btn">Setup Vehicle Type</a>	 </li>
        <li> <a href="IncidentType.aspx" class="btn"> Setup Incidence Type </a> </li>
        <li > <a href="FleetLocationLookup.aspx" class="btn">Setup Fleet Location</a>  </li>
        <%-- <li > <a href="#" class="btn">	Setup Tracker Company</a>  </li>	
         <li > <a href="#" class="btn">	Setup DriverEmployee</a>  </li>--%>
      </ul>
    </div>
     <div class="lookup span5">
      <ul>
        <%--<li> <a href="#" class="btn ">Setup vehicle Maker </a></li>
        <li> <a href="#" class="btn">Setup Vehicle Type</a>	 </li>
        <li> <a href="#" class="btn"> Setup Vehicle Incidence Type </a> </li>--%>
        <li > <a href="InsuranceCoySetup.aspx" class="btn">	Setup Insurance Company</a>  </li>
         <li > <a href="TrackerCompanySetup.aspx" class="btn">	Setup Tracker Company</a>  </li>	
         <li > <a href="DriversEmployerSetup.aspx" class="btn">	Setup DriversEmployer</a>  </li>
      </ul>
    </div>
    </div>
    </div>
</div>
</asp:Content>
