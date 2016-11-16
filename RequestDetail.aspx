<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestDetail.aspx.cs" Inherits="FleetMgtSolution.RequestDetail" %>
<%@ Register TagPrefix="es" TagName="EasyStep" Src="~/Controls/easySteps.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="row-fluid home-content">
  <asp:UpdatePanel ID="UpdatePanelContainer" runat="server">
      <ContentTemplate>
  <div class="span8">   
    <div class="inner-content">
      <h3>
          Fleet Request Detail</h3>
       <div class="row-fluid">
        <div class="span12 alert alert-error" runat="server" id="error" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
        </div>
        <div class="span12 alert alert-success" runat="server" id="success" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
        </div>
    </div>
      <div id="signup">
       <div class="grid-header"><b><asp:Label ID="lbInit" Font-Bold="true" runat="server" Text=""/></b> Fleet Request #<asp:Label ID="lbheader" Font-Bold="true" runat="server" Text=""/></div>
           <asp:HiddenField ID="hid" runat="server" />
       <asp:GridView ID="gvheader" runat="server" Width="100%" ShowHeader="false" 
              EnableTheming="false" EmptyDataText="No Record found" GridLines="None" 
        AutoGenerateColumns="False" onrowcommand="gvheader_RowCommand"> 
        <Columns>
             <asp:TemplateField>
              <ItemTemplate>
               <div class="detail-main">
                <h4>Trip Detail</h4>
                <table border="0">
                <tr>
                <td class="detail-title">Location:</td><td class="nav-header"><%# Eval("Location")%></td>
                <td class="detail-title">Destination:</td><td class="nav-header"><%#Eval("Destination")%></td>
                </tr>
                 <tr>
                <td class="detail-title">Trip Date:</td><td class="nav-header"><%#Eval("TripDate","{0:dd-MM-yyyy}")%></td>
                <td class="detail-title">Purpose:</td><td class="nav-header"><%# Eval("Purpose")%></td>
                </tr>
                 <tr>
                 <td class="detail-title">Requestor ID:</td><td class="nav-header"><%#Eval("InitiatorID")%></td>
                <td class="detail-title">Department:</td> <td class="nav-header"><%# Eval("Department.Name")%></td>
                
                </tr>
                <tr>
                <td class="detail-title">Departure Time:</td><td class="nav-header"><%# Eval("ExpectedDepartureTime")%></td>
                 <td class="detail-title">Return Time(expected):</td><td class="nav-header"><%#Eval("ExpectedReturnTime")%></td>
                </tr>
                <tr>  <td class="detail-title">Pickup Location:</td><td class="nav-header"><%# Eval("PickupLocation")%></td>
                       </tr>
                </table>
               </div>
                <div class="break"></div>
               <div class="detail-main-alt">
                <h4>Request Approvals</h4>
               <asp:Label ID="lbInit" runat="server" Text='<%# Eval("ID")%>' Visible="false" />
               <table border="0">
                 <tr>
                <td>Approved By(Supervisor):</td><td class="nav-header"><%# Eval("User.StaffName")%></td>
                 <td>Approved By(FleetManager):</td><td class="nav-header"><asp:Label ID="lbAppr" runat="server" Text='<%#Eval("User1.StaffName") != null ? Eval("User1.StaffName").ToString() : "Pending Action"%>' /></td>
                </tr>
                </table>
                </div>
                <div class="break"></div>
                <div class="detail-main">
                <h4>Request Status</h4>
                <table border="0">
                 <tr>
                  <td>Request Status:</td><td class="nav-header">
                   <asp:Label ID="lbSt" runat="server" ForeColor=Maroon Text='<%#GetStatus(Eval("Status")) %>'></asp:Label></td>
                 <td>Priority:</td><td class="nav-header">
                 <asp:Label ID="lbpriority" runat="server" ForeColor="#B3CC57" Text='<%#GetPriority(Eval("priority")) %>'></asp:Label></td>
                </tr>
                  
               </table>
               </div>
                <div class="break"></div>
                <div class="detail-main-alt">
                <h4>Resource Assigned</h4>
                <table border="0">
                 <tr>
                <td>Vehicle Assigned:</td><td class="nav-header"><asp:Label ID="lbVeh" runat="server" Text='<%# Eval("Vehicle.Name")!=null? Eval("Vehicle.Name"):"Not yet Assigned"%>' /></td>
                  <td class="detail-title">Driver Assigned:</td>
                  <td class="nav-header"style="color:#B3CC57">
                   <asp:LinkButton ID="lnkDrv" CssClass="bid-text" Font-Size="11px" runat="server" CommandArgument='<%#Eval("Vehicle.Driver.ID") %>' CommandName="veh"  Text='<%# Eval("Vehicle.Driver.Name")!=null?Eval("Vehicle.Driver.Name"):"Not yet Assigned"%>'></asp:LinkButton>
                 <%-- <asp:Label ID="Label3" runat="server" Text= '<%# Eval("Vehicle.Driver.Name")!=null?Eval("Vehicle.Driver.Name"):"Not yet Assigned"%>'></asp:Label></td>--%>
                </tr>
                 
               </table>
               </div>
               </ItemTemplate>
             </asp:TemplateField>
        </Columns>

    </asp:GridView>

          <asp:LinkButton ID="lnkDriver" runat="server"></asp:LinkButton>
      <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkDriver"
                         CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                           Assigned Driver's Detail
                        </div>
                        <div class="body">
                            <table border="0" class="table">
                                <tr>
                                <td class="detail-title">Driver's Name</td><td class="nav-header"> 
                                    <asp:Label ID="lbName" runat="server" Text=""></asp:Label></td>
                                </tr><tr>
                                <td class="detail-title">Mobile Number</td><td class="nav-header">
                                  <asp:Label ID="lbMobile" runat="server" Text=""></asp:Label>
                                </td>
                                </tr>
                                 
                                </table>
                        </div>
                        <div class="footer" align="right">
                           <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-submit" />
                        </div>
                    </asp:Panel>
     </div>
   </div>
   <div class="span10">
              <div class="request">
                  <div>
                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="request-reject" Visible="true"
                          CommandName="book" onclick="LinkButton1_Click"   >Cancel Request</asp:LinkButton>
                  </div>
              </div>
   </div>
</div>
         
         <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelContainer">
                        <ProgressTemplate>
                            <div class="throbber">
                                <img src="img/loadinfo.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
         </ContentTemplate>
    </asp:UpdatePanel>
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
