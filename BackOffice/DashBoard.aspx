<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="FleetMgtSolution.BackOffice.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <span class="span12 label label-info pageheader-text">DashBoard</span>
    </div>
<div class="page-body">
  <div class="page-body-wrapper">
   <div class="inner-header">DashBoard</div>
    <div class="dashboard-body row-fluid">
     <div class="span10">
      <ul class="row-fluid">
        <li >
         <%-- <asp:HyperLink ID="lnkPendAppr" runat="server"></asp:HyperLink>--%>
         <a  runat="server" id="lnkPendAppr1" class="btn btn-warning"></a></li>
        <li  > <a runat="server" id="lnkpendAss" class="btn btn-warning"> </a></li>
        <li  > <a runat="server" id="lnkCmpReq" class="btn btn-warning"></a> </li>
        <li  > <a runat="server" id="lnkVehPool" class="btn btn-warning"> </a> </li>
        <li> <a runat="server" id="lnkvehEnt" class="btn btn-warning"> (<asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>)</a> </li>
        <%--<li> <a href="#" class="btn btn-warning">Total Number of Drivers in the Pool(<asp:Label ID="lbAssetCount" runat="server" Text="" Font-Bold="true"></asp:Label>)</a> </li>--%>
        <li> <a href="ReportHome.aspx" class="btn btn-warning">Reports</a> </li>
      </ul>
      <ul class="row-fluid">
       
      </ul>
    </div>
    </div>
    </div>
</div>
</asp:Content>
