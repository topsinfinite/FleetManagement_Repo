﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicleLogReport.aspx.cs" Inherits="FleetMgtSolution.Report.VehicleLogReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="vertical-align: bottom; border-width: 0px; margin-top: 0px; margin-bottom: 0px; width: 100%; height: 99.3%; padding: 0px,0px,0px,0px;" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" >
             <tr>
              <td style="width:80%; background-color:White;">
                   <img src="../img/logo.jpg" alt="Amcon Logo"/> </td>
              <td style="padding:5px;font-family:Arial, Helvetica, sans-serif;color:#5b5b5b;font-size:11px;"><a href="../BackOffice/DashBoard.aspx">Back Office </a> | <a href="../BackOffice/ReportHome.aspx">Report</a></td>
             </tr>
             </table>
          </td>
     </tr>
     <tr>
          <td>
          <div style="border-top: black 1px solid; background-color: #ece9d8; border-bottom-width: 1px; border-bottom-color: #d4d0c8; padding-bottom: 10px; margin-bottom:0px;">
           <table width="100%" style="padding:2px;font-family:Arial, Helvetica, sans-serif;color:#5b5b5b;font-size:11px;">
           
             <tr>
             <td >From Date:<span class="err">*</span></td>
             <td>
                 <asp:TextBox ID="txtvsrt" runat="server" CssClass="txtbox" ValidationGroup="cat"></asp:TextBox>
                   <asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtvsrt" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtvsrt" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="" ControlToValidate="txtvsrt" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
             
                 </td>
            </tr>
            <tr>
             <td>To Date:<span class="err">*</span></td>
             <td>
                 <asp:TextBox ID="txtvstp" runat="server" CssClass="txtbox" ValidationGroup="cat"></asp:TextBox><asp:RequiredFieldValidator ID="Reqvty2" runat="server" ControlToValidate="txtvstp" Display="Dynamic" Text="*" ErrorMessage="LPO validity is required" ValidationGroup="cat"></asp:RequiredFieldValidator>
                   <asp:Image ID="Image2" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtvstp" PopupButtonID="Image2"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ForeColor=Maroon ControlToValidate="txtvstp" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="" ControlToValidate="txtvstp" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
                 </td>
            </tr>
             <tr>
             <td>Filter By Location:</td><td>
             <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" Width="310px">
              <asp:ListItem Value="" Selected="True">..Select Location..</asp:ListItem>
            </asp:DropDownList>
                </td>
            </tr>
              <tr>
             <td>Filter By Incidence Type:</td><td>
             <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" Width="310px">
              <asp:ListItem Value="" Selected="True">..Select Incidence Type..</asp:ListItem>
            </asp:DropDownList>
                </td>
            </tr>
               <tr>
                 <td >
                     <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" 
                         CssClass="btns btn-submit" ValidationGroup="cat" onclick="btnGenerate_Click"/></td>
                            <td>
                        <asp:Label ID="lbMsg" runat="server" Text="" ForeColor="Maroon"></asp:Label></td>
                 </tr>
                 </table>
            </div>
          </td>
      </tr>
      <tr>
        <td>
            <table border="0" width="100%">
             <tr>
              <td colspan="2">
                  <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Width="100%" 
                      Font-Names="Verdana" Font-Size="8pt" Height="100%" 
                      InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                      WaitMessageFont-Size="14pt">
                      <LocalReport ReportPath="Report\VehicleLogReport.rdlc">
                          <DataSources>
                              <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFMS" />
                          </DataSources>
                      </LocalReport>
                  </rsweb:ReportViewer>

                     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                         SelectMethod="GetData" 
                      
                      TypeName="FleetMgtSolution.Report.FleetMgtSysDBDataSetTableAdapters.FMS_VehicleLogReportTableAdapter" 
                      OldValuesParameterFormatString="original_{0}">
                         <SelectParameters>
                             <asp:Parameter Name="fromDate" Type="DateTime" />
                             <asp:Parameter Name="toDate" Type="DateTime" />
                             <asp:Parameter Name="IncidenceType" Type="Int32" />
                         </SelectParameters>
                     </asp:ObjectDataSource>
                 </td>
               </tr>
               </table>
             </td>
         </tr> 
    </table>
    </form>
</body>
</html>
