<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageVehicle.aspx.cs" Inherits="FleetMgtSolution.BackOffice.ManageVehicle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanelContainer" runat="server">
  <ContentTemplate>
    <div class="row-fluid">
      <span class="span12 label label-info pageheader-text">Manage Vehicle Page</span>
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
 <div class="nav-header" runat="server" id="dvMsg">Add New Vehicle :</div>
 <div class="row-fluid">
  <div class="page-body span6">
   <div class="form-horizontal">
        <div class="control-group" runat="server" id="dvID" visible="false">
            <label class="control-label" for="txtStaffID">ID:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtID" runat="server" ReadOnly="true" CssClass=""></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID"> Vehicle Name<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtName" runat="server" CssClass=""></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15"  runat="server" ForeColor=Maroon ControlToValidate="txtName" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Vehicle Maker<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlMaker" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Vehicle Maker..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlMaker" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>

        <div class="control-group">
        <label class="control-label" for="txtStaffID">Vehicle Type<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlVehType" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Vehicle Type..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlVehType" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Plate No:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtplate" runat="server" CssClass="" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ForeColor=Maroon ControlToValidate="txtplate" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">License No:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtLicense" runat="server" CssClass="" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  runat="server" ForeColor=Maroon ControlToValidate="txtLicense" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
         <div class="control-group">
        <label class="control-label" for="txtStaffID">License Expiry Date:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtLDate" runat="server" CssClass="" ></asp:TextBox>&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
             <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtLDate" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtLDate" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor=Maroon ControlToValidate="txtLDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
        </div>
        </div>
         <div class="control-group">
        <label class="control-label" for="txtStaffID">Engine No:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtEngNo" runat="server" CssClass="" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  runat="server" ForeColor=Maroon ControlToValidate="txtEngNo" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group" id="dvDel" runat="server" visible="false">
       <label class="control-label" for="txtStaffID">Mark Deleted:</label>
         <div class="controls">
             <asp:CheckBox ID="chk" runat="server" />
          </div>
       </div>
        <div class="control-group">
           <label class="control-label" for="txtStaffID">Vehicle Location:<span>*</span>:</label>
         <div class="controls">
            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Location..</asp:ListItem>
             
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlLocation" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
       <div class="control-group">
        <label class="control-label" for="btnSubmit"></label>
        <div class="controls">
          <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                CssClass="btn btn-submit" onclick="btnSubmit_Click" />
         </div>
        </div>
  </div>
</div>
 <div class="page-body span6">
   <div class="form-horizontal">
    <div class="control-group">
        <label class="control-label" for="txtStaffID">Insurance No:</label>
        <div class="controls">
            <asp:TextBox ID="txtInsur" runat="server" CssClass="" ></asp:TextBox> 
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" ForeColor=Maroon ControlToValidate="txtLicense" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  --%>
        </div>
        </div>
     <div class="control-group">
        <label class="control-label" for="txtStaffID">Insurance Expiry Date:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtIDate" runat="server" CssClass="" ></asp:TextBox>&nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/img/cal.gif" /><br />
             <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtIDate" PopupButtonID="Image2"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ForeColor=Maroon ControlToValidate="txtIDate" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor=Maroon ControlToValidate="txtIDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
        </div>
        </div>
     <div class="control-group">
        <label class="control-label" for="txtStaffID">Insurance Company<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlInsur" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Insurance Company..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlInsur" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
       <div class="control-group">
        <label class="control-label" for="txtStaffID">Tracker Company:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlTracker" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Tracker Company..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlTracker" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Capacity:<span>*</span>:</label>
        <div class="controls">
             <asp:TextBox ID="txtCapacity" runat="server" CssClass="" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ForeColor=Maroon ControlToValidate="txtCapacity" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>

        <div class="control-group">
        <label class="control-label" for="txtStaffID">StartUp Mileage</label>
        <div class="controls">
             <asp:TextBox ID="txtMilage" runat="server" CssClass="" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" ForeColor=Maroon ControlToValidate="txtMilage" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Driver Assigned<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Driver..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlDriver" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>

         <div class="control-group">
        <label class="control-label" for="txtStaffID">Chasis No:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtChasis" runat="server" CssClass="" ></asp:TextBox> 
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13"  runat="server" ForeColor=Maroon ControlToValidate="txtEngNo" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  --%>
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
 <div class="inner-header">List of Vehicles
    <div class="form-horizontal" style="float:right;margin-top:-5px">
            <div class="controls-group">
            <div class="controls">
                SearchyBy PlateNo: <input type="text" runat="server" id="txtSea" placeholder="Search By plateNo" />
                <asp:Button ID="btnSrch" runat="server" Text="Go" CssClass="btn" CausesValidation="false" 
                    onclick="btnSrch_Click"/>
                <asp:Button ID="btnCl" runat="server" Text="Clr" CssClass="btn" CausesValidation="false"
                    onclick="btnCl_Click" />
                   </div>
              </div>
              
          </div>
 </div>
     
    <asp:HiddenField ID="hid" runat="server" />
    <asp:GridView ID="gvVehicle" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
          onselectedindexchanged="gvVehicle_SelectedIndexChanged" 
           onpageindexchanging="gvVehicle_PageIndexChanging" 
         onrowcommand="gvVehicle_RowCommand">
      <Columns>
        <asp:BoundField HeaderText="#" DataField="ID" Visible="false"/>
        <asp:BoundField HeaderText="Vehicle's Name" DataField="Name" />
        <asp:TemplateField HeaderText="Vehicle Maker">
             <ItemTemplate>
             <asp:Label ID="lbMakerID" runat="server" Text='<%#Eval("MarkerID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbMaker" runat="server" Text='<%#Eval("VehicleMaker.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField HeaderText="Vehicle Type">
             <ItemTemplate>
               <asp:Label ID="lbTypeID" runat="server" Text='<%#Eval("TypeID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbType" runat="server" Text='<%#Eval("VehicleType.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
        <asp:BoundField HeaderText="Plate Number" DataField="PlateNo" />
        <asp:BoundField HeaderText="Capacity" DataField="Capacity"/>
        <asp:BoundField HeaderText="Mileage(km)" DataField="Mileage" DataFormatString="{0:N}"/>
         <asp:TemplateField HeaderText="Associated Driver">
             <ItemTemplate>
              <asp:Label ID="lbEmp" runat="server" Text='<%#Eval("Driver.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Total Trips">
              <ItemTemplate>
                  <asp:LinkButton ID="lnkBid" CssClass="bid-text" CausesValidation="false" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="trip"  Text='<%#GetTripCount(Eval("ID")) %>'></asp:LinkButton>
              </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                 <asp:Label ID="lbStatus" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                  <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
             <asp:ButtonField ButtonType="Image" HeaderText="View" CommandName="view" ImageUrl="~/img/view_icon.png" />
        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../img/edit_icon.png"   />
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
</div>
</div>
</div>
<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelContainer">
                        <ProgressTemplate>
                            <div class="throbber">
                                <img src="../img/loadinfo.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
