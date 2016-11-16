<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuccessRequest.aspx.cs" Inherits="FleetMgtSolution.SuccessRequest" %>
<%@ Register TagPrefix="es" TagName="EasyStep" Src="~/Controls/easySteps.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid home-content">
  <div class="span8">
   <div class="label-success">User Fleet Request was successful</div>
<div id="signup">
   <h1>Please note that an email has been sent to the next level approval for your department </h1>
   <p>You can track the status of your request from here</p>
   <p>Thanks for your using easyFleet Solution.</p>
</div>
</div>
  <div class="span4" style="margin-top:14px"> 
    <div class="row-fluid">
    <div class="span12 round-edge-new  right-menu">
    <es:EasyStep id="es1" runat="server"></es:EasyStep>
    </div>
  </div>

  </div>
</div>
</asp:Content>
