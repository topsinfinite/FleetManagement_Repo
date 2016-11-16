<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageFleetRequest.aspx.cs" Inherits="FleetMgtSolution.BackOffice.ManageFleetRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
      <span class="span12 label label-info pageheader-text">Manage FleetRequest Page</span>
      <div class="alert alert-error" runat="server" id="error" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
      <div class="alert alert-success" runat="server" id="success" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
    </div>
    <div class="row-fluid">
<div class=" span11 page-body-wrapper" style="margin-top:10px;">
<%--<div class="inner-header">Setup Driver</div>--%>
 <div class="nav-header" runat="server" id="dvMsg">Search Criteria :</div>
 <div class="row-fluid">
  <div class="page-body span6">
   <div class="form-horizontal">
        <div class="control-group" runat="server" id="dvID" >
            <label class="control-label" for="txtStaffID">SearchBy RequestID:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtID" runat="server" CssClass=""></asp:TextBox>
            </div>
        </div>
       <div class="control-group">
        <label class="control-label" for="txtStaffID">SearchBy Status<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Request Status..</asp:ListItem>
            </asp:DropDownList>
           
        </div>
        </div>
       
        <div class="control-group">
        <label class="control-label" for="txtStaffID">SearchBy InitiatorID(staffNo):<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtInit" runat="server" CssClass="" ></asp:TextBox> 
            
        </div>
        </div>
        
  </div>
</div>
 <div class="page-body span6">
   <div class="form-horizontal">
     
     <div class="control-group">
        <label class="control-label" for="txtStaffID">TripDate(From):<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="" ></asp:TextBox>&nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/img/cal.gif" /><br />
             <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupButtonID="Image2"></asp:CalendarExtender> 
                 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor=Maroon ControlToValidate="txtFromDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
        </div>
        </div>
        
         <div class="control-group">
        <label class="control-label" for="txtStaffID">TripDate(To):<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="" ></asp:TextBox>&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
             <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupButtonID="Image1"></asp:CalendarExtender> 
                 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor=Maroon ControlToValidate="txtToDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
        </div>
        </div>
       <div class="control-group">
        <label class="control-label" for="btnSubmit"></label>
        <div class="controls">
          <asp:Button ID="btnSubmit" runat="server" Text="Search" 
                CssClass="btn btn-submit" onclick="btnSubmit_Click" />
          <asp:Button ID="btnClr" runat="server" Text="Clear" 
                CssClass="btn btn-submit" onclick="btnClr_Click" />
         </div>
        </div>
        
   </div>
 </div>
 </div>
 </div>
</div>
    
<div class="row-fluid">
<div class="page-body">
 <div class="page-body-control span12">
 <div class="inner-header">List of Fleet Request</div>
    <asp:HiddenField ID="hid" runat="server" />
      <asp:GridView ID="gvRequest" runat="server" GridLines="None" Width="100%" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
          onselectedindexchanged="gvRequest_SelectedIndexChanged" 
           onpageindexchanging="gvRequest_PageIndexChanging" 
         onrowcommand="gvRequest_RowCommand" 
         onrowdatabound="gvRequest_RowDataBound">
      <Columns>
       <asp:TemplateField HeaderText="##">
              <ItemTemplate>
                  <asp:CheckBox ID="Chk" runat="server" Enabled="false" />
                  <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
             <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
              <asp:BoundField HeaderText="#" DataField="ID"/>
          <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
       
        <asp:BoundField HeaderText="Batch" DataField="BatchID"/>
         <asp:BoundField HeaderText="Location" DataField="Location" />
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Purpose Of Trip" DataField="Purpose" />
        <asp:TemplateField HeaderText="Priority">
              <ItemTemplate>
                 <asp:Label ID="lbpr" runat="server" Visible="false" Text='<%#Eval("priority") %>'></asp:Label>
                  <asp:Label ID="lbpriority" runat="server" ForeColor=Maroon Text='<%#GetPriority(Eval("priority")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
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
         
            
             
            
        
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>

      <asp:ModalPopupExtender ID="mpeAssign" runat="server" PopupControlID="pnlPopupAss" TargetControlID="btnBatch"
                         CancelControlID="btnCloseAss" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopupAss" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                          List of Available Vehicles in the Pool
                        </div>
                        <div class="body">
                            <asp:RadioButtonList ID="optVehicle" Font-Size="11px" ForeColor="BurlyWood" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="checkbox" >
                              <asp:ListItem Value="1" Selected="True">Vehicles in the Pool</asp:ListItem>
                              <asp:ListItem Value="2">Vehicles on Route</asp:ListItem>
                            
                            </asp:RadioButtonList>
       <asp:GridView ID="gvVehicle" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="10" EmptyDataText="No Record Found"
          onselectedindexchanged="gvVehicle_SelectedIndexChanged" 
           onpageindexchanging="gvVehicle_PageIndexChanging">
      <Columns>
        <asp:BoundField HeaderText="#" DataField="ID" Visible="false"/>
        <asp:BoundField HeaderText="Vehicle's Name" DataField="Name" />
        <asp:BoundField HeaderText="Plate Number" DataField="PlateNo" />
        <asp:BoundField HeaderText="Capacity" DataField="Capacity"/>
         <asp:TemplateField HeaderText="Associated Driver">
             <ItemTemplate>
              <asp:Label ID="lbMaker" runat="server" Text='<%#Eval("VehicleMaker.Name") %>' Visible="false"></asp:Label>
               <asp:Label ID="lbDrvFone" runat="server" Text='<%#Eval("Driver.MobileNo") %>' Visible="false"></asp:Label>
              <asp:Label ID="lbDrv" runat="server" Text='<%#Eval("Driver.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
             
        <asp:CommandField ButtonType="Image" HeaderText="Assign" ShowSelectButton="true" SelectImageUrl="../img/edit_icon.png"   />
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
                        </div>
                        <div class="footer" align="right">
                          <asp:Button ID="btnCloseAss" runat="server" Text="Close" CssClass="btn btn-submit" />
                        </div>
                    </asp:Panel>
</div>
</div>
</div>
<div class="row-fluid">
<div class="page-body">
<div class="page-body-controlbtn span12">
 <h3>Back Office Batch Options</h3>

      <asp:Button ID="btnBatch" runat="server" Text="Batch Assignment Option" visible="false" ToolTip="use this option when assigning single vehicle for multiple requests"
                CssClass="btn btn-submit" onclick="btnBatch_Click" />
                <div id="dvbatch" runat="server" visible="false">
   <asp:Button ID="btnBatchAppr" runat="server" Text="Batch Approval Option" visible="false" ToolTip="use this option when the head-driver should assign single vehicle for multiple requests"
                CssClass="btn btn-submit" onclick="btnBatchAppr_Click" /><br /><small><b>(USE THIS OPTION WHEN THE HEAD-DRIVER SHOULD ASSIGN SINGLE VEHICLE FOR MULTIPLE REQUESTS)</b></small>
                </div>
 </div>
</div>
</div>


</asp:Content>
