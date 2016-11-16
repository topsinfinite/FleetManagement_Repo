<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageDriver.aspx.cs" Inherits="FleetMgtSolution.BackOffice.ManageDriver" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
      <span class="span12 label label-info pageheader-text">Manage Driver Page</span>
      <div class="alert alert-error" runat="server" id="error" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
      <div class="alert alert-success" runat="server" id="success" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
    </div>
<div class="row-fluid">
<div class="page-body span10">
  <div class="page-body-wrapper">
  <div class="inner-header">Setup Driver</div>
    <div class="form-horizontal">
    <div class="nav-header" runat="server" id="dvMsg">Add New Driver :</div>
        <div class="control-group" runat="server" id="dvID" visible="false">
            <label class="control-label" for="txtStaffID">ID:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtID" runat="server" ReadOnly="true" CssClass=""></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID"> Full Name<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtName" runat="server" CssClass=""></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15"  runat="server" ForeColor=Maroon ControlToValidate="txtName" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID"> Address<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtAdd" runat="server" CssClass="" TextMode="MultiLine" Rows="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ForeColor=Maroon ControlToValidate="txtAdd" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Mobile No:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtMobile" runat="server" CssClass="" ></asp:TextBox><small>format(23408020000000)</small>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ForeColor=Maroon ControlToValidate="txtMobile" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
         <div class="control-group">
        <label class="control-label" for="txtStaffID">Date Employed:<span>*</span>:</label>
        <div class="controls">
            <asp:TextBox ID="txtDate" runat="server" CssClass="" ></asp:TextBox><asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" />
             <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtDate" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="" ControlToValidate="txtDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
        </div>
        </div>
       <div class="control-group">
        <label class="control-label" for="txtStaffID">Driver Employer:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlDriverEmployer" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Driver Employer..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlDriverEmployer" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
        <div class="control-group">
           <label class="control-label" for="txtStaffID">Driver Location:<span>*</span>:</label>
         <div class="controls">
            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Location..</asp:ListItem>
             
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlLocation" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
       <%-- <div class="control-group">
        <label class="control-label" for="txtStaffID">Assign Vehicle:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlVeh" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Vehicle..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlVeh" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>--%>
       <div class="control-group" id="dvDel" runat="server" visible="false">
       <label class="control-label" for="txtStaffID">Mark Deleted:</label>
         <div class="controls">
             <asp:CheckBox ID="chk" runat="server" />
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
<div class="row-fluid">
   <div class="page-body-control span12">
 <div class="inner-header">List of Drivers</div>
    <asp:HiddenField ID="hid" runat="server" />
    <asp:GridView ID="gvDriver" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
          onselectedindexchanged="gvDriver_SelectedIndexChanged" 
           onpageindexchanging="gvDriver_PageIndexChanging" 
           onrowcommand="gvDriver_RowCommand">
      <Columns>
        <asp:BoundField HeaderText="#" DataField="ID" Visible="false"/>
        <asp:BoundField HeaderText="Driver's Name" DataField="Name" />
        <asp:BoundField HeaderText="Mobile Number" DataField="MobileNo" />
        <asp:BoundField HeaderText="Address" DataField="Address" />
        <asp:TemplateField HeaderText="Date Employed">
             <ItemTemplate>
                 <asp:Label ID="lbSdate" runat="server" Text='<%#Eval("DateEmployed","{0:dd/MM/yyyy}") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
          <asp:BoundField HeaderText="Address" DataField="EmployerID" Visible="false" />
         <asp:BoundField HeaderText="Address" DataField="VehicleAssociate" Visible="false" />
         <asp:TemplateField HeaderText="Driver's Employer">
             <ItemTemplate>
              <asp:Label ID="lbEmpID" runat="server" Text='<%#Eval("EmployerID") %>' Visible="false"></asp:Label>
              <asp:Label ID="lbEmp" runat="server" Text='<%#Eval("DriverEmployer.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Associated Vehicle">
             <ItemTemplate>
                 <asp:Label ID="lbVehId" runat="server" Text='<%#Eval("VehicleAssociate") %>' Visible="false"></asp:Label>
                 <asp:LinkButton ID="lnkDriverView" runat="server" CausesValidation="false" CssClass="bid-text" CommandName='Driverview' CommandArgument='<%#Eval("Vehicle.ID") %>' Text='<%#Eval("Vehicle.Name") %>'></asp:LinkButton>
             </ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField HeaderText="Location">
             <ItemTemplate>
             <asp:Label ID="lbLoc" runat="server" Text='<%#Eval("LocationID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbLoc2" runat="server" Text='<%#Eval("FleetLocation.Name") %>' Visible="true"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="IsDeleted">
           <ItemTemplate>
              <asp:Label ID="lbDelFlg" runat="server" Text='<%#Eval("DelFlg") %>'></asp:Label>
           </ItemTemplate>
         </asp:TemplateField>
        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../img/edit_icon.png"   />
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
</div>
</div>
</div>
</div>
</asp:Content>
