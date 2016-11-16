<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestApproval.aspx.cs" Inherits="FleetMgtSolution.RequestApproval" %>
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
      <h3>Fleet Request Approval Page</h3>
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
    <div class="row-fluid">
     <div class="span12">
     <div id="signup">
       <div class="grid-header"><b><asp:Label ID="lbInit" Font-Bold="true" runat="server" Text=""/></b> Fleet Request #<asp:Label ID="lbheader" Font-Bold="true" runat="server" Text=""/></div>
           <asp:HiddenField ID="hid" runat="server" />
         <asp:GridView ID="gvheader" runat="server" Width="100%" ShowHeader="false" EnableTheming="false" EmptyDataText="No Record found" GridLines="None" 
        AutoGenerateColumns="False"> 
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
                <td class="detail-title">Departure Time <small>expected</small>:</td><td class="nav-header"><%# Eval("ExpectedDepartureTime")%></td>
                 <td class="detail-title">Arrival Time <small>expected</small>:</td><td class="nav-header"><%#Eval("ExpectedReturnTime")%></td>
                </tr>
                  <tr>  <td class="detail-title">Pickup Location:</td><td class="nav-header"><%# Eval("PickupLocation")%></td></tr>
                </table>
               </div>
                <div class="break"></div>
               <div class="detail-main-alt">
                <h4>Request Approvals</h4>
               <table border="0">
                 <tr>
                 <td>Approver (Supervisor):</td><td class="nav-header"><%# Eval("User.StaffName")%></td>
                 <td>Approved By(FleetManager):</td><td class="nav-header"><asp:Label ID="lbAppr" runat="server" Text='<%#Eval("User1.StaffName") != null ? Eval("User1.StaffName").ToString() : "Next Approver"%>' /></td>
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
               
               </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
      
     </div>
       </div>
       <div class="span10">
              <div class="request">
                  <div>
                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="request-approval" Visible="false"
                          CommandName="book" onclick="LinkButton1_Click"   >Approve Request</asp:LinkButton>
                  </div>
                   <div>
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="request-reject" Visible="false" 
                           CommandName="bid" onclick="LinkButton2_Click"  >Reject Request</asp:LinkButton>
                   </div>   
                  </div>
                    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="LinkButton2"
                         CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                           Decline Fleet Request
                        </div>
                        <div class="body">
                         <div id="dvMainBid" runat="server">
                            <div class="form-horizontal">
                                <div class="control-group">
                                    <label class="control-label" for="txtFeature">
                                       Comment:<span>*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" CssClass= ""></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="control-group">
                                    <label class="control-label" for="btnSubmit">
                                    </label>
                                    <div class="controls">
                                        <asp:Button ID="btnDecline" runat="server" Text="Submit" CssClass="btn btn-submit"
                                            OnClick="btnDecline_Click" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-submit" />
                                    </div>
                                </div>
                            </div>
                          </div>
                         <%-- <div id="dvBidMsg" runat="server" class="label-info" style="background:#FBF6E2; color:Maroon" >
                             Log In to Bid:
                                 <p>Please Note that you need to <a href="Login.aspx">Log in</a> to submit your bid . If you have not sign up, kindly create a new account<a href="CreateAccount.aspx">here</a></p></div>--%>
                          
                        </div>
                        <div class="footer" align="right">
                        </div>
                    </asp:Panel>

         <asp:ModalPopupExtender ID="mpeAppr" runat="server" PopupControlID="pnlPopupAppr" TargetControlID="LinkButton1"
                         CancelControlID="btnCloseAppr" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopupAppr" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                           Approve Fleet Request With Comment
                        </div>
                        <div class="body">
                         <div id="Div1" runat="server">
                            <div class="form-horizontal">
                                <div class="control-group">
                                    <label class="control-label" for="txtFeature">
                                        Add Comment:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCommentAppr" runat="server" TextMode="MultiLine" Rows="3" CssClass= ""></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="control-group">
                                    <label class="control-label" for="btnSubmit">
                                    </label>
                                    <div class="controls">
                                        <asp:Button ID="btnAppr" runat="server" Text="Submit" CssClass="btn btn-submit"
                                            OnClick="btnbtnAppr_Click" />
                                        <asp:Button ID="btnCloseAppr" runat="server" Text="Close" CssClass="btn btn-submit" />
                                    </div>
                                </div>
                            </div>
                          </div>
                         <%-- <div id="dvBidMsg" runat="server" class="label-info" style="background:#FBF6E2; color:Maroon" >
                             Log In to Bid:
                                 <p>Please Note that you need to <a href="Login.aspx">Log in</a> to submit your bid . If you have not sign up, kindly create a new account<a href="CreateAccount.aspx">here</a></p></div>--%>
                          
                        </div>
                        <div class="footer" align="right">
                        </div>
                    </asp:Panel>
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
