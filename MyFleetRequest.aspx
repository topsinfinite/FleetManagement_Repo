<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyFleetRequest.aspx.cs" Inherits="FleetMgtSolution.MyFleetRequest" %>
<%@ Register TagPrefix="es" TagName="EasyStep" Src="~/Controls/easySteps.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="row-fluid home-content">
  <div class="span8">   
    <div class="inner-content">
      <h3>My Fleet Requests</h3>
       <div class="row-fluid">
        <div class="span12 alert alert-error" runat="server" id="error" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
              <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="cat" />
        </div>
        <div class="span12 alert alert-success" runat="server" id="success" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
        </div>
    </div>
      <div id="signup">
       <div class="grid-header">List of Request</div>
           <asp:GridView ID="gvRequest" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
          onselectedindexchanged="gvRequest_SelectedIndexChanged" 
           onpageindexchanging="gvRequest_PageIndexChanging" 
         onrowcommand="gvRequest_RowCommand">
      <Columns>
        <asp:BoundField HeaderText="#" DataField="ID" Visible="false"/>
      <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
       <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
        <asp:BoundField HeaderText="Location" DataField="Location" />
       <%-- <asp:BoundField HeaderText="Pickup Location" DataField="PickupLocation" />--%>
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Purpose" DataField="Purpose" />
        <asp:BoundField HeaderText="Trip Date" DataField="TripDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField HeaderText="Expected Departure Time" DataField="ExpectedDepartureTime" />
        <asp:BoundField HeaderText="Expected Reture Time" DataField="ExpectedReturnTime" />
        <asp:TemplateField HeaderText="Department">
             <ItemTemplate>
             <asp:Label ID="lbMakerID" runat="server" Text='<%#Eval("DepartmentID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbMaker" runat="server" Text='<%#Eval("Department.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
          
         <asp:TemplateField HeaderText="Priority">
              <ItemTemplate>
                 <asp:Label ID="lbpr" runat="server" Visible="false" Text='<%#Eval("priority") %>'></asp:Label>
                  <asp:Label ID="lbpriority" runat="server" ForeColor=Maroon Text='<%#GetPriority(Eval("priority")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
            
            
        
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
     </div>
   </div>

</div>
  <div class="span4" style="margin-top:14px"> 
    <div class="row-fluid">
    <div class="span12 round-edge-new  right-menu">
      <es:EasyStep id="es1" runat="server"></es:EasyStep>
    </div>
    <div class="span12 round-edge-new  right-menu">
     <h3>Fleet Request Status</h3>
      <ul>
       
       <li><i class="icon-circle-arrow-right"></i> Pending Vehicle Assignment:Request has been approved by FleetManager.</li>
       <li><i class="icon-circle-arrow-right"></i> Enroute:Trip in progress </li>
        
       <li><i class="icon-circle-arrow-right"></i> onHold: Currently no pool car available to meet your request, a car will be assigned once its available</li>
        
      </ul>

    </div>
    </div>
  
  </div>
</div>
</asp:Content>
