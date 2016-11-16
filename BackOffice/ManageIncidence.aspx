<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageIncidence.aspx.cs" Inherits="FleetMgtSolution.BackOffice.ManageIncidence" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
      <span class="span12 label label-info pageheader-text">Manage Incidence Page</span>
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
  <div class="inner-header">Vehicle Incidence Log</div>
    <div class="form-horizontal">
    <div class="nav-header" runat="server" id="dvMsg">Add New Incidence :</div>
        <div class="control-group" runat="server" id="dvID" visible="false">
            <label class="control-label" for="txtStaffID">ID:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtID" runat="server" ReadOnly="true" CssClass=""></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Incidence Type<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlIncType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
                onselectedindexchanged="ddlIncType_SelectedIndexChanged">
              <asp:ListItem Value="" Selected="True">..Select Incidence Type..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlIncType" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        <div class="control-group">
        <label class="control-label" for="txtStaffID">Associated Vehicle:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlVeh" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Vehicle..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlVeh" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
       <div id="dvmainTenance" runat="server" visible="false">
           <div class="control-group" runat="server">
            <label class="control-label" for="txtStaffID">Mileage at Service:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtMile" runat="server"   CssClass=""></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ForeColor=Maroon ControlToValidate="txtMile" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
            </div>
        </div>
         <div class="control-group">
               <label class="control-label" for="">Service Date:<span>*</span>:</label>
                <div class="controls">
                     <asp:TextBox ID="txtDate" runat="server"  CssClass=""></asp:TextBox>&nbsp;
                      <asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtDate" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="" ControlToValidate="txtDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
                </div>
           </div>
       </div>
       <div class="control-group">
        <label class="control-label" for="txtStaffID">Driver Associated<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true">
              <asp:ListItem Value="" Selected="True">..Select Driver..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlDriver" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
        </div>
        
        <div class="control-group" runat="server">
            <label class="control-label" for="txtStaffID">Add Description:<span>*</span>:</label>
            <div class="controls">
                <asp:TextBox ID="txtDesc" runat="server"   CssClass="" TextMode="MultiLine" Rows="5"></asp:TextBox>

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
 <div class="inner-header">List of Logged Incidence</div>
    <asp:HiddenField ID="hid" runat="server" />
    <asp:GridView ID="gvIncidence" runat="server" GridLines="None" 
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
           AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found"
           onpageindexchanging="gvIncidence_PageIndexChanging" 
           onrowcommand="gvIncidence_RowCommand" 
           onselectedindexchanged="gvIncidence_SelectedIndexChanged" >
      <Columns>
        <asp:BoundField HeaderText="#" DataField="ID" Visible="false"/>
        <asp:TemplateField HeaderText="Incidence Type">
             <ItemTemplate>
               <asp:Label ID="lbTyID" runat="server" Text='<%#Eval("VehicleIncidenceType.ID") %>' Visible=false></asp:Label>
                 <asp:Label ID="lbType" runat="server" Text='<%#Eval("VehicleIncidenceType.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Associated Driver">
             <ItemTemplate>
              <asp:Label ID="lbDrvID" runat="server" Text='<%#Eval("Driver.ID") %>' Visible=false></asp:Label>
              <asp:Label ID="lbEmp" runat="server" Text='<%#Eval("Driver.Name") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Associated Vehicle">
             <ItemTemplate>
              <asp:Label ID="lbVehID" runat="server" Text='<%#Eval("Vehicle.ID") %>' Visible=false></asp:Label>
                 <asp:LinkButton ID="lnkDriverView" runat="server" CausesValidation="false" CssClass="bid-text" CommandName='Driverview' CommandArgument='<%#Eval("Vehicle.ID") %>' Text='<%#Eval("Vehicle.Name") %>'></asp:LinkButton>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField HeaderText="Date Added" DataField="DateAdded" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
         <asp:BoundField HeaderText="Description" DataField="Note" />

        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../img/edit_icon.png"   />
      </Columns>
       <SelectedRowStyle BackColor="#E0D9BD" />
    </asp:GridView>
</div>
</div>
</div>
</div>
</asp:Content>
