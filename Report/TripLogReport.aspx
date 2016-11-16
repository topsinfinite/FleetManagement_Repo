<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripLogReport.aspx.cs" Inherits=" FleetMgtSolution.Report.TripLogReport"  %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>TripLog Report</title>
    <%--<link rel="Stylesheet" type="text/css" href="../css/style.css"/>--%>
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
             <td >From Date(Trip Date):<span class="err">*</span></td>
             <td>
                 <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtvsrt" runat="server" CssClass="txtbox" ValidationGroup="cat"></asp:TextBox>
                   <asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtvsrt" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtvsrt" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="" ControlToValidate="txtvsrt" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
             
                 </td>
            </tr>
            <tr>
             <td>To Date (Trip Date):<span class="err">*</span></td>
             <td>
                 <asp:TextBox ID="txtvstp" runat="server" CssClass="txtbox" ValidationGroup="cat"></asp:TextBox><asp:RequiredFieldValidator ID="Reqvty2" runat="server" ControlToValidate="txtvstp" Display="Dynamic" Text="*" ErrorMessage="LPO validity is required" ValidationGroup="cat"></asp:RequiredFieldValidator>
                   <asp:Image ID="Image2" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtvstp" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ForeColor=Maroon ControlToValidate="txtvstp" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="" ControlToValidate="txtvstp" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
                 </td>
            </tr>
            <tr>
            <td>Filter By Directorate:</td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" Width="310" AutoPostBack="true" 
                    onselectedindexchanged="ddlType_SelectedIndexChanged" >
                 <asp:ListItem Value="-1" Selected="True">...All...</asp:ListItem>
                <%-- <asp:ListItem Value="1">Cmc Order</asp:ListItem>--%>
                 <asp:ListItem Value="2">Branch Order</asp:ListItem>
                  <asp:ListItem Value="3">Department Order</asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
             <td>Filter By Staus:</td>
             <td> <asp:DropDownList ID="ddlstatus" runat="server" Width="310px" 
                     AppendDataBoundItems="True">
                 <asp:ListItem Value="-1" Selected="True">...All...</asp:ListItem>
                  <asp:ListItem Value="8">Awaiting Allocation</asp:ListItem>
                  <asp:ListItem Value="2">Opened</asp:ListItem>
                   <asp:ListItem Value="3">In Process</asp:ListItem>
                   <asp:ListItem Value="4">Available for PickUp</asp:ListItem>
                   <asp:ListItem Value="5">Closed</asp:ListItem>
                   <asp:ListItem Value="6">Cancelled</asp:ListItem>
                   <asp:ListItem Value="7">Enroute to Hub</asp:ListItem>
                   
                </asp:DropDownList>
                </td>
            </tr>
                 <tr>
                 <td >
                     <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" 
                         CssClass="btns btn-submit" ValidationGroup="cat" onclick="btnGenerate_Click"/></td>
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
                      <LocalReport ReportPath="Report\TripReport.rdlc">
                          <DataSources>
                              <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFMS" />
                          </DataSources>
                      </LocalReport>
                  </rsweb:ReportViewer>

                     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                         SelectMethod="GetData" 
                      TypeName="FleetMgtSolution.Report.FleetMgtSysDBDataSetTableAdapters.FMS_TripReportTableAdapter">
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
