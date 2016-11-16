<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="FleetRequestDetail.aspx.cs" Inherits="FleetMgtSolution.BackOffice.FleetRequestDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanelContainer" runat="server">
  <ContentTemplate>
    <div class="row-fluid">
      <span class="span12 label label-info pageheader-text">Fleet Request Detail Page</span>
      <div class="alert alert-error" runat="server" id="error" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
      <div class="alert alert-success" runat="server" id="success" visible="false">
      <button type="button" class="close" data-dismiss="alert">&times;</button>
      </div>
    </div>
  <div class="row-fluid home-content">
  
    <div class=" span8" style="margin-top:10px;">
       <div id="signup">
       <div class="grid-header" style="background-color:#9C9432;"><b>
       <asp:Label ID="lbInit" Font-Bold="true" runat="server" Text=""/></b> Fleet Request #
       <asp:Label ID="lbheader" Font-Bold="true" runat="server" Text=""/>
       ::Request Date:<b><asp:Label ID="lbDateAdded" Font-Bold="true" runat="server" Text=""/></b></div>
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
                <td class="detail-title">Departure Time(expected):</td><td class="nav-header"><%# Eval("ExpectedDepartureTime")%></td>
                 <td class="detail-title">Return Time(expected):</td><td class="nav-header"><%#Eval("ExpectedReturnTime")%></td>
                </tr>
                  <tr>  <td class="detail-title">Pickup Location:</td><td class="nav-header"><%# Eval("PickupLocation")%></td>
                         <td class="detail-title">Batch No:</td><td class="nav-header"><%# Eval("BatchID")%></td>  
                  </tr>
                </table>
               </div>
                <div class="break"></div>
               <div class="detail-main-alt">
                <h4>Request Approvals</h4>
               <asp:Label ID="lbInit" runat="server" Text='<%# Eval("ID")%>' Visible="false" />
               <table border="0">
                 <tr>
                <td>Approved By(Supervisor):</td><td class="nav-header"><asp:Label ID="lbspv"  Font-Size="11px"  runat="server" Text='<%# Eval("User.StaffName")%>' /></td>
                 <td>Approved By(FleetManager):</td><td class="nav-header"><asp:Label ID="lbAppr"  Font-Size="11px"  runat="server" Text='<%#Eval("User1.StaffName") != null ? Eval("User1.StaffName").ToString() : "Pending Action"%>' /></td>
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
                <td>Vehicle Assigned:</td>
                  <td class="nav-header">
                   <asp:Label ID="lbVeh" runat="server" Visible="false" Text='<%# Eval("Vehicle.ID")%>' />
                   <asp:LinkButton ID="lnkVeh" CssClass="bid-text" Font-Size="11px" runat="server" CommandArgument='<%#Eval("Vehicle.ID") %>' CommandName="veh"  Text='<%# Eval("Vehicle.Name")!=null? GetVehicleDetail(Eval("Vehicle.ID")):"Not yet Assigned"%>'></asp:LinkButton>
                  </td>
                  <td>Driver Assigned:</td>
                  <td class="nav-header"><asp:Label ID="Label3" Font-Size="11px"  runat="server" Text= '<%# Eval("Vehicle.Driver.Name")!=null?Eval("Vehicle.Driver.Name"):"Not yet Assigned"%>'></asp:Label></td>
                </tr>
                <tr>
                 <td>Date Assigned:</td><td class="nav-header"><asp:Label ID="Label1" runat="server" Font-Size="11px" Text='<%# Eval("DateAssigned")%>' /></td>
                  <td class="detail-title">Assigned By:</td>
                  <td class="nav-header"><asp:Label ID="Label2" runat="server" Font-Size="11px" Text= '<%# Eval("User2.StaffName")%>'></asp:Label></td>
                </tr>
               </table>
               </div>
                <div class="break"></div>
              <div class="detail-main">
                <h4>Mileage Information</h4>
                <table border="0">
                 <tr>
                <td>Mileage at Departure(KM):</td>
                <td class="nav-header"><asp:Label ID="Label4" runat="server" Font-Size="12px" ForeColor="Maroon" Text='<%# Eval("MileageAtDeparture")!=null? Eval("MileageAtDeparture","{0:N}"):"Not Available Yet"%>' /></td>
                <td class="detail-title">Mileage on Arrival(KM):</td>
                <td class="nav-header"><asp:Label ID="Label5" runat="server" Font-Size="12px" ForeColor="Maroon" Text= '<%# Eval("MileageOnArrival")!=null?Eval("MileageOnArrival","{0:N}"):"Not Available Yet"%>'></asp:Label></td>
                </tr>
               </table>
               </div>
               </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
     
      
   </div>
  </div>
    <div class="span4" style="margin-top:12px;" >
     <div class="row-fluid">
     <div class="span12 round-edge-new">
     <h3>Back Office Actions</h3>
              <div id="dvAct" runat="server" visible="true">No Action is Required</div>
            <div id="dvFleetmgr" runat="server" visible="false">
               <div class="request-admin">
                  <div>

                   <asp:LinkButton ID="lnkAppr" runat="server" CssClass="request-approval-admin"  
                            onclick="lnkAppr_Click">Approve Request</asp:LinkButton>
                  </div>
                   <div>
                    <asp:LinkButton ID="lnkReject" runat="server" CssClass="request-reject-admin" 
                             onclick="lnkReject_Click"  >Reject Request</asp:LinkButton>
                   </div>   
                   <br />
                    <div>
                    <asp:LinkButton ID="lnkHold" runat="server" CssClass="request-onhold-admin"
                     onclick="lnkHold_Click">Put Request On HOLD</asp:LinkButton>
                   </div>   
                  </div>
            </div>
         <div id="dvHeadDriver" runat="server" visible="false">
             <div class="request-admin">
                  <div>
                   <asp:LinkButton ID="lnkCheckAvailabity" runat="server" CssClass="request-approval-admin" 
                          onclick="lnkCheckAvailabity_Click" >Assign Vehicle</asp:LinkButton>
                  </div>
                  
                  </div>
         </div>
     
         <div id="dvTripCmpl" runat="server" visible="false">
             <div class="request-admin">
                  <div>
                   <asp:LinkButton ID="lnkLog" runat="server" CssClass="request-approval-admin">Log Trip Completion</asp:LinkButton>
                  </div>
                  </div>
         </div>    <br />
         <div id="dvTripCanCel" runat="server" visible="true">
            <div>
                    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="request-reject-admin" 
                             onclick="lnkCancel_Click"  >Cancel Request</asp:LinkButton>
                   </div>   
         </div>
         <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkReject"
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

      <asp:ModalPopupExtender ID="mpeAssign" runat="server" PopupControlID="pnlPopupAss" TargetControlID="lnkCheckAvailabity"
                         CancelControlID="btnCloseAss" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopupAss" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                          List of Available Vehicles in the Pool
                        </div>
                        <div class="body">
                            <asp:RadioButtonList ID="optVehicle" Font-Size="11px" ForeColor="BurlyWood" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="checkbox" >
                              <asp:ListItem Value="1" Selected="True">Vehicles in the Pool</asp:ListItem>
                              <asp:ListItem Value="2">Vehicles Enroute</asp:ListItem>
                            
                            </asp:RadioButtonList>
                          <asp:GridView ID="gvVehicle" runat="server" GridLines="None" Font-Size="11px"
        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID" 
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

      <asp:ModalPopupExtender ID="mpeLog" runat="server" PopupControlID="pnlPopupLog" TargetControlID="lnkLog"
                         CancelControlID="btnClsLog" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                     <asp:Panel ID="pnlPopupLog" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                           Log Details of Request on Driver's arrival
                        </div>
                        <div class="body">
                            <asp:Label ID="lbmsg" runat="server" Text="" ForeColor="Maroon" Visible=false></asp:Label>
                          <div class="form-horizontal">
                           <div class="control-group">
                               <label class="control-label" for="">Departure Time:<span>*</span><small>(Actual)</small></label>
                                <div class="controls">
                                     <div class="input-append bootstrap-timepicker"><input id="timepicker1" name="timepicker1" type="text" class="input-small" ><span class="add-on"><i class="icon-time"></i></span>
                                 </div>
                                </div>
                           </div>
                           <div class="control-group">
                           <label class="control-label" for="">Return Time:<span>*</span><small>(Actual)</small></label>
                            <div class="controls">
                                 <div class="input-append bootstrap-timepicker"><input id="timepicker2" name="timepicker2" type="text" class="input-small"><span class="add-on"><i class="icon-time"></i></span></div>  
                                    
                            </div>
                        </div>
                         <div class="control-group">
                           <label class="control-label" for="">Mileage On Arrival:<span>*</span></label>
                            <div class="controls">
                                 <asp:TextBox ID="txtMileage" runat="server" CssClass= "" onkeyup = "javascript:this.value=Comma(this.value);"></asp:TextBox>  
                                
                                 <script type="text/javascript" language ="javascript">
                                     function Comma(Num) { //function to add commas to textboxes
                                         Num += '';
                                         Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                         Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                         x = Num.split('.');
                                         x1 = x[0];
                                         x2 = x.length > 1 ? '.' + x[1] : '';
                                         var rgx = /(\d+)(\d{3})/;
                                         while (rgx.test(x1))
                                             x1 = x1.replace(rgx, '$1' + ',' + '$2');
                                         return x1 + x2;
                                     }
                                        </script>
                            </div>
                        </div>
                             <div class="control-group">
                                    <label class="control-label" for="txtFeature"> Additional Note:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="4" CssClass= ""></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="control-group">
                                    <label class="control-label" for="btnSubmit">
                                    </label>
                                    <div class="controls">
                                        <asp:Button ID="btnLog" runat="server" Text="Submit" CssClass="btn btn-submit"
                                            OnClick="btnLog_Click" />
                                        <asp:Button ID="btnClsLog" runat="server" Text="Close" CssClass="btn btn-submit" />
                                    </div>
                                </div>
                            </div>
                         
                        </div>
                        <div class="footer" align="right">
                        
                        </div>
                    </asp:Panel>
      </div>
     <div class=" span12 round-edge-new">
         <h3>Trip Completion Log</h3> 
            
       <asp:GridView ID="gvLog" runat="server" Width="100%" ShowHeader="false" 
               EnableTheming="false" EmptyDataText="No additional information to display" GridLines="None" 
                    AutoGenerateColumns="False">
        <Columns>
             <asp:TemplateField>
              <ItemTemplate>
                <table border="0" class="table">
                <tr>
                <td class="detail-title">Departure Time (Actual)</td><td class="nav-header"><%# Eval("ActualDepartureTime")%></td>
                </tr><tr>
                <td class="detail-title">Arrival Time (Actual)</td><td class="nav-header"><%#Eval("ActualRetunTime")%></td>
                </tr>
                <tr>
                 <td class="detail-title">Additional Note:</td>
                 <td class="nav-header"><%#Eval("Note")%></td>
                </tr>
                </table>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
          </asp:GridView>
  
        </div>
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
