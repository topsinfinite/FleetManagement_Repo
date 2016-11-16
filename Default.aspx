<%@ Page Title="Fleet Request" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FleetMgtSolution._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="es" TagName="EasyStep" Src="~/Controls/easySteps.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <asp:UpdatePanel ID="UpdatePanelContainer" runat="server">
  <ContentTemplate>--%>
 <div class="row-fluid home-content">

  <div class="span8">   
    <div class="inner-content">
      <h3>Make Fleet Request</h3>
       <div class="row-fluid">
        <div class="span12 alert alert-error" runat="server" id="error" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
             <%-- <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="cat" />--%>
        </div>
        <div class="span12 alert alert-success" runat="server" id="success" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
        </div>
    </div>
       <div id="signup">
        
           <div class="form-horizontal">
         <div class="control-group">
           <label class="control-label" for="txtStaffID">Fleet Location:<span>*</span>:</label>
         <div class="controls">
            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" 
                 AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
             <asp:ListItem Selected="True" Value="">..Select Location..</asp:ListItem>
             
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlLocation" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
            <div class="control-group">
               <label class="control-label" for="">Pickup Location:<span>*</span>:</label>
                <div class="controls">
                     <asp:TextBox ID="txtLoc" runat="server"  CssClass=""></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ForeColor=Maroon ControlToValidate="txtLoc" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                     <br /><small>(location where the pool car will pickup the requestor)</small>
                </div>
           </div>
             <div class="control-group">
               <label class="control-label" for="">Destination:<span>*</span>:</label>
                <div class="controls">
                     <asp:TextBox ID="txtDesn" runat="server"  CssClass=""></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ForeColor=Maroon ControlToValidate="txtDesn" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                </div>
           </div>
             <div class="control-group">
               <label class="control-label" for="">Purpose:<span>*</span>:</label>
                <div class="controls">
                     <asp:TextBox ID="txtPur" runat="server"  CssClass="" TextMode="MultiLine" Rows="4"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ForeColor=Maroon ControlToValidate="txtPur" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                   
                </div>
           </div>
              <div class="control-group">
               <label class="control-label" for="">Trip Date:<span>*</span>:</label>
                <div class="controls">
                     <asp:TextBox ID="txtDate" runat="server"  CssClass=""></asp:TextBox>&nbsp;
                      <asp:Image ID="Image1" runat="server" ImageUrl="~/img/cal.gif" /><br />
                      <small>format(dd/MM/yyyy)</small> 
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupButtonID="Image1"></asp:CalendarExtender> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ForeColor=Maroon ControlToValidate="txtDate" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="" ControlToValidate="txtDate" Display="Dynamic" Text="Enter valid date(dd/MM/yyyy)" ValidationExpression="((((0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/])?)?(19|20)\d\d(\s+)?)+"   ></asp:RegularExpressionValidator>
                </div>
           </div>
             <div class="control-group">
               <label class="control-label" for="">Departure Time:<span>*</span>:</label>
                <div class="controls">
                     <div class="input-append bootstrap-timepicker"><input id="timepicker1" name="timepicker1" type="text" class="input-small" ><span class="add-on"><i class="icon-time"></i></span>
               </div>
                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="cat" runat="server" ForeColor=Maroon ControlToValidate="" Display="Dynamic" Text="Required!!"></asp:RequiredFieldValidator>--%>  
                </div>
           </div>
           <div class="control-group">
               <label class="control-label" for="">Expected Return Time:<span>*</span>:</label>
                <div class="controls">
                     <div class="input-append bootstrap-timepicker"><input id="timepicker2" name="timepicker2" type="text" class="input-small"><span class="add-on"><i class="icon-time"></i></span></div>  
                        <br /> 
                   
                </div>
                
         </div> 
       <div class="control-group">
        <label class="control-label" for="txtStaffID">Select Request Priority:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlPriority" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Priority..</asp:ListItem>
             <asp:ListItem Value="1">Emergency</asp:ListItem>
              <asp:ListItem Value="2">High</asp:ListItem>
              <asp:ListItem Value="3">Normal</asp:ListItem>
              <asp:ListItem Value="4">Low</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlPriority" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
       <div class="control-group">
        <label class="control-label" for="txtStaffID">Select Request Approver:<span>*</span>:</label>
        <div class="controls">
            <asp:DropDownList ID="ddlDeptApproval" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Selected="True" Value="">..Select Approver..</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue=""  runat="server" ForeColor=Maroon ControlToValidate="ddlDeptApproval" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
        </div>
       </div>
           <div class="control-group">
        <label class="control-label" for="btnSubmit"></label>
        <div class="controls">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
         <ContentTemplate>
           <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                CssClass="btn btn-submit" onclick="btnSubmit_Click" />

       <asp:UpdateProgress ID="UpProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div class="throbber">
                                <img src="img/loadinfo.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
       </ContentTemplate>
       <Triggers>
         <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
       </Triggers>
       </asp:UpdatePanel>

         </div>
        </div>
      
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
<%--
 <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelContainer">
                        <ProgressTemplate>
                            <div class="throbber">
                                <img src="img/loadinfo.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
