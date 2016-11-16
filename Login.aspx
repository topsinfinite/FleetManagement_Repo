<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FleetMgtSolution.Login" %>

<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8">
    <title>EasyFleet:: Login Page</title>
       <link href="css/login-box.css" rel="stylesheet" type="text/css" />
       <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
     <link href="../css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css"/>
   <style type="text/css">
body {
	background-image: url(img/asdasdasd.jpg);
}
</style>
<script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript"></script>
<script src="../js/action.js" type="text/javascript"></script>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
</head>
<body>
 <form id="form1" runat="server">
    
<div style=" top: 200px; height: 100px;"></div>
<div style=" margin-left:auto;margin-right:auto; width: 30%;  ">

<div id="login-box" >
<%--<asp:Label ID="lbmsg" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
<div class="alert alert-error" runat="server" id="error" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
              <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="cat" />
        </div>
   <div class=" alert alert-success" runat="server" id="success" visible="false">
            <button type="button" class="close" data-dismiss="alert">
                &times;</button>
                </div>
<H2> <img src="img/easyfleetlogo.png" width="212px" height="40px" /> <span style="font-size:16px"></span> </H2> <br />
 Welcome to AMCON Fleet Management Solution, kindly login below to get access to EasyFleet.
<br />
<br />
<div id="login-box-name" style="margin-top:20px;">UserName:</div>
<div id="login-box-field" style="margin-top:20px;"><asp:TextBox ID="txtUsername" runat="server" CssClass="form-login"></asp:TextBox>
    					 
        <asp:RequiredFieldValidator ID="Reqlg" runat="server" ErrorMessage="Required" Display="Dynamic" ControlToValidate="txtUsername" ></asp:RequiredFieldValidator></div>
<div id="login-box-name">Password:</div>
<div id="login-box-field"><asp:TextBox ID="txtPwrd" runat="server" CssClass="form-login" TextMode="Password"></asp:TextBox>
   							 <asp:RequiredFieldValidator ID="Reqpwd1" runat="server" ErrorMessage="Required" Display="Dynamic" ControlToValidate="txtPwrd" ></asp:RequiredFieldValidator></div>
<br />
 <div>
 <asp:ImageButton ID="btnLogn" runat="server" OnClick="btnSubmit_Click" ImageUrl="~/img/login-btn.png" width="103" height="42" style="margin-left:90px;"/><br />
 <span class="login-box-options">
                        Login with your Window's credentials
                        <b></b></span>
                        </div>
   <%-- <asp:Button ID="btnSubmit" runat="server" Text="Login" ValidationGroup="cat" 
            CssClass="btn btn-submit" onclick="btnSubmit_Click" />
            </div>--%>


 <asp:HiddenField ID="hid" runat="server" />   
        <asp:HiddenField ID="hidUser" runat="server" /> 

</div>

</div>
 <%-- <div id="header">
  <div class="container-fluid">
   <div class="row-fluid hidden-phone">
    <div class="span7">
     <a href="index.html"><img src="img/logo.jpg"></a>
    </div>
    <div class="span5 pull-right right action-links">
     <span class="name">
         <asp:Label ID="lbDate" runat="server" Text="Label"></asp:Label></span>
     <ul>
      <li><a href="#"></a></li> 
     </ul>
    </div>
   </div>

    <div class="row-fluid visible-phone">
    <div class="span12">
     <a href="index.html" class="logo-mobile"><img src="img/logo-mobile.png"></a>
     <button type="button" class="mobile-nav" data-toggle="collapse" data-target=".nav-collapse">
     </button>
    </div>
   </div>
  </div>
 </div>
 <div id="site-content">
 <div class="container-fluid">
  <div class="row-fluid home-content" style="margin-top:40px" >
    
   <div class="span7 round-edge-new" >
     <h3>Fleet Management Solution</h3>
     <div class="alert alert-error" runat="server" id="error" visible="false">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
     </div>
    <div class="form-horizontal clear-row-margin">
    <div class="control-group">
    <label class="control-label" for="inputEmail">Staff ID<span class="required">*</span></label>
    <div class="controls">
    <asp:TextBox ID="txtEmail" runat="server"   CssClass="" ></asp:TextBox><small>(e.g 120100)</small>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="cat" runat="server" ForeColor=Maroon ControlToValidate="txtEmail" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
    </div>
  </div>
  <div class="control-group">
    <label class="control-label" for="inputPassword">Password<span class="required">*</span></label>
    <div class="controls">
     <asp:TextBox ID="txtPwd" TextMode="Password" runat="server" CssClass="" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="cat" runat="server" ForeColor=Maroon ControlToValidate="txtPwd" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>  
    </div>
  </div>
      <span class="login">
        <span>Kindly Login with your <strong>Window's Credential</strong></span>
      </span>
  <div class="control-group">
    <div class="controls">
     <asp:Button ID="btnSubmit" runat="server" Text="Login" ValidationGroup="cat" 
            CssClass="btn btn-submit" onclick="btnSubmit_Click" />
     <%-- <button type="submit" class="btn btn-submit">Sign in</button>
    </div>
  </div>
  </div>
    </div>
     
    <div class=" span5 round-edge-new  right-menu" style="margin-left:20px;">
     <h3>Easy Steps to Request Fleet</h3>
      <ul>
       <li><a href="#"><i class="icon-circle-arrow-right"></i> Login with your Window's Credentials </a></li>
        <li><a href="#"><i class="icon-circle-arrow-right"></i> Request for Fleet </a></li>
       <li><a href="#"><i class="icon-circle-arrow-right"></i> Submit request for approval</a></li>
       <li><a href="#"><i class="icon-circle-arrow-right"></i> A vehicle and Driver is Assigned to you on approval </a></li>
      </ul>
    </div>
 
    </div>
 </div>
    </div>--%>
    </form>
</body>
</html>
