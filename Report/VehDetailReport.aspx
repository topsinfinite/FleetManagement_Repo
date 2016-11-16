<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehDetailReport.aspx.cs" Inherits="FleetMgtSolution.Report.VehDetailReport" %>
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
             <td>Filter By Location:</td><td>
             <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" Width="310px">
              <asp:ListItem Value="" Selected="True">..Select Location..</asp:ListItem>
            </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>Filter By Vehicle:</td>
            <td>
               <asp:DropDownList Id="ddlVeh" runat="server" CssClass="" 
                                AppendDataBoundItems="true" Enabled="true">
                 <asp:ListItem Value="" Selected="True">..Select Vehicle..</asp:ListItem>
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
                  <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Width="" 
                      Font-Names="Verdana" Font-Size="8pt" Height="100%" 
                      InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                      WaitMessageFont-Size="14pt">
                      <LocalReport ReportPath="Report\VehicleDetailReport.rdlc">
                          <DataSources>
                              <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFMS" />
                          </DataSources>
                      </LocalReport>
                  </rsweb:ReportViewer>

                     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                         SelectMethod="GetData" 
                      
                      
                      TypeName="FleetMgtSolution.Report.FleetMgtSysDBDataSetTableAdapters.FMS_VehicleReportTableAdapter" 
                      OldValuesParameterFormatString="original_{0}">
                         <SelectParameters>
                             <asp:Parameter Name="vehID" Type="Int32" />
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
