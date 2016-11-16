<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="VehicleDetail.aspx.cs" Inherits="FleetMgtSolution.BackOffice.VehicleDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="row-fluid">
    <span class=" span12 label label-info pageheader-text">Vehicle Details Page</span>
     <div class="alert alert-error" runat="server" id="error" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
     </div>
    <div class="alert alert-success" runat="server" id="success" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
  </div>
</div>
 <div class="row-fluid">
 <div class="page-body span11">
  <div class="page-body-wrapper">
  <div class="inner-header">Vehicle Details</div>
   <asp:HiddenField ID="hid" runat="server" />
       <asp:GridView ID="gvheader" runat="server" Width="100%" ShowHeader="false" EnableTheming="false" EmptyDataText="No Record found" GridLines="None" 
        AutoGenerateColumns="False"> 
        <Columns>
             <asp:TemplateField>
              <ItemTemplate>
                 <div style="padding-bottom: 10px; font-size:12px" class="boxheader">
              <%-- <h3>User Detail</h3>--%>
               <asp:Label ID="lbInit" runat="server" Text='<%# Eval("ID")%>' Visible="false" />
               <table border="0" class="table-detail">
                <tr>
                <td class="detail-title">Vehicle Name:</td><td class="nav-header"><b><%#Eval("Name")%></b></td>
                <td class="detail-title">Vehicle Maker:</td><td class="nav-header"><%# Eval("VehicleMaker.Name")%></td>
                </tr>
                 <tr>
                <td class="detail-title">Vehicle Type:</td> <td class="nav-header"><%# Eval("VehicleType.Name")%></td>
                <td class="detail-title">Capacity:</td><td class="nav-header"><%#Eval("Capacity")%></td
                </tr>
                <tr>
                <td class="detail-title">Mileage(km):</td><td class="nav-header"><%# Eval("Mileage","{0:N}")%></td>
                 <td class="detail-title">Plate Number:</td><td class="nav-header"><%#Eval("PlateNo")%></td>
                </tr>
                 <tr>
                <td class="detail-title">Vehicle License:</td><td class="nav-header"><%# Eval("LicenseNo")%></td>
                 <td class="detail-title">License Expired:</td><td class="nav-header"><%#Eval("LicenseExprDate","{0:dd-MM-yyyy}")%></td>
                </tr>
                 <tr>
                <td class="detail-title">Insurance Company:</td><td class="nav-header"><%# Eval("InsuranceCompany1.Name")%></td>
                 <td class="detail-title">Insurance Expired:</td><td class="nav-header"><%#Eval("InsuranceExprDate","{0:dd-MM-yyyy}")%></td>
                </tr>
                 <tr>
                <td class="detail-title">Tracker Company:</td><td class="nav-header"><%# Eval("TrackerCompany1.Name")%></td>
                 <td class="detail-title">Driver Assigned:</td><td class="nav-header"><%#Eval("Driver.Name")%></td>
                </tr>
                <tr>
                  <td class="detail-title">Vehicle Status:</td><td class="nav-header">
                   <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label></td>
                  <td class="detail-title">Vehicle Location:</td>
                  <td class="nav-header"style="color:Maroon"><asp:Label ID="lbrole" runat="server" Text= '<%# Eval("FleetLocation.Name")%>'></asp:Label></td>
                </tr>
               </table>
               </div>
               </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div class="page-body-control">
       <div class="inner-header">Vehicle Trip History</div>
          <asp:GridView ID="gvTrip" runat="server" GridLines="None" 
              AutoGenerateColumns="false" CssClass="table table-striped" Width="100%" 
              DataKeyNames="ID" SelectedRowStyle-BackColor="#E0D9BD" PageIndex="0" PageSize="15"  PagerStyle-HorizontalAlign="Center" 
              onrowcommand="gvTrip_RowCommand" 
              onselectedindexchanged="gvTrip_SelectedIndexChanged" 
               AllowPaging="true" OnPageIndexChanging="OnPaging" >
           <Columns>
             <asp:BoundField HeaderText="#" DataField="ID" />
              <asp:BoundField HeaderText="Location" DataField="Location" />
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Purpose Of Trip" DataField="Purpose" />
        <asp:BoundField HeaderText="Trip Date" DataField="TripDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField HeaderText="Departure Time (Expected)" DataField="ExpectedDepartureTime" />
        <asp:BoundField HeaderText="Return Time (Expected)" DataField="ExpectedReturnTime" />
        <asp:TemplateField HeaderText="Department">
             <ItemTemplate>
             <asp:Label ID="lbMakerID" runat="server" Text='<%#Eval("DepartmentID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbMaker" runat="server" Text='<%#Eval("Department.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField HeaderText="InitiatedBy">
             <ItemTemplate>
                 <asp:Label ID="lbInit" runat="server" Text='<%#Eval("InitiatorName") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
           <asp:TemplateField HeaderText="ApprovedBy">
             <ItemTemplate>
                 <asp:Label ID="lbusr" runat="server" Text='<%#Eval("User.StaffName") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
       
             <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetFleetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
              <asp:TemplateField HeaderText="Check">
              <ItemTemplate>
                  <asp:CheckBox ID="Chk" runat="server" />
              </ItemTemplate>
             </asp:TemplateField>
             <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
        
           </Columns>
            <SelectedRowStyle BackColor="#E0D9BD" />
          </asp:GridView>
    </div>

 </div>
 </div>

</asp:Content>
