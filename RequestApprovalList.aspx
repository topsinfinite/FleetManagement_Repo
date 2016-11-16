<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestApprovalList.aspx.cs" Inherits="FleetMgtSolution.RequestApprovalList" %>
<%@ Register TagPrefix="es" TagName="EasyStep" Src="~/Controls/easySteps.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid home-content">
  <div class="span8">   
    <div class="inner-content">
      <h3>Manage Fleet Requests</h3>
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
       <div class="grid-header"> List of Unapproved Request
        
       </div>
           <asp:GridView ID="gvRequest" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Pending Request"
          onselectedindexchanged="gvRequest_SelectedIndexChanged" 
           onpageindexchanging="gvRequest_PageIndexChanging" 
         onrowcommand="gvRequest_RowCommand">
      <Columns>
       <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
        <asp:BoundField HeaderText="#" DataField="ID"/>
          <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
        <asp:BoundField HeaderText="Location" DataField="Location" />
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Purpose" DataField="Purpose" />
        <asp:BoundField HeaderText="Trip Date" DataField="TripDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField HeaderText="Expected DepartureTime" DataField="ExpectedDepartureTime" />
        <asp:BoundField HeaderText="Expected ReturnTime" DataField="ExpectedReturnTime" />
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
     <div class="content-separator">
         <asp:LinkButton ID="lnkViewHistorical" runat="server" 
             onclick="lnkViewHistorical_Click">View Historical Records::</asp:LinkButton></div>
    <div runat="server" id="dvHistorical" visible="false">
     <div id="signup">
         <div class="grid-header"> List of Approved Request
            <div class="form-horizontal" style="float:right;margin-top:-5px">
            <div class="controls-group">
            <div class="controls">
                <input type="text" runat="server" id="txtSea" placeholder="Search By Request ID" />
                <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn" 
                    onclick="btnSearch_Click"/>  <asp:Button ID="btnClr" runat="server" 
                    Text="Clr" CssClass="btn" onclick="btnClr_Click" 
                    /></div>
              </div>
              
          </div>
         </div>
           <asp:GridView ID="gvRequestAppr" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
          onselectedindexchanged="gvRequestAppr_SelectedIndexChanged" 
           onpageindexchanging="gvRequestAppr_PageIndexChanging" 
         onrowcommand="gvRequestAppr_RowCommand">
      <Columns>
       <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
        <asp:BoundField HeaderText="#" DataField="ID" />
         <asp:BoundField HeaderText="Location" DataField="Location" />
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Purpose" DataField="Purpose" />
        <asp:BoundField HeaderText="Trip Date" DataField="TripDate" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField HeaderText="Departure Time (Expected)" DataField="ExpectedDepartureTime" />
        <asp:BoundField HeaderText="Return Time (Expected)" DataField="ExpectedReturnTime" />
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
             <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
            
        
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
     </div>
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
