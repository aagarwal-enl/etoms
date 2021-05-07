<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ETOMS.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="form-group">
        <div class="col-md-6 col-md-offset-2 ">
            <h3>Login</h3>
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 ">
        </div>
    </div>


    <div class="form-group">
        <label class="col-md-2 control-label" for="UserName">User Name</label>
        <div class="col-md-3">
           <!-- <input class="form-control" type="text" data-val="true" data-val-required="The User Name field is required." id="UserName" name="UserName" value="" />-->
            <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Please Enter User Name" ControlToValidate="UserName" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="UserName" data-valmsg-replace="true"></span>
        </div>-->
    </div>
    <div class="form-group">
        <label class="col-md-2 control-label" for="Password">Password</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please Enter Password" ControlToValidate="Password" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-6">
            <!--<button type="submit" class="btn btn-success">Login</button>-->
            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn btn-success" OnClick="btnLogin_Click"/>
        </div>
    </div>

<input name="__RequestVerificationToken" type="hidden" value="CfDJ8Ok5W1qPL9FAnW8Ykj6aTiEmZDqppJPbOvBklAm3Ipe3yVpSW7zzbg9kNmrthOmS8geaT2TwtgryI_3bV4A2_OffpRYTVoKoFOXA4MCck29Q8bac_p2OImAeR5_4IGzQHiZzMJiPpTGPKlXZxnN5ZI0" />
</asp:Content>
