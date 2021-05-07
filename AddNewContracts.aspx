<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewContracts.aspx.cs" Inherits="ETOMS.AddNewContracts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="form-group">
        <div class="col-md-6 col-md-offset-2 ">
            <h3 style="text-align:center;margin-left:250px;">Add New Contracts</h3>
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 ">
        </div>
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="VehicleName">Vehicle Name</label>
        <div class="col-md-3">
           <!-- <input class="form-control" type="text" data-val="true" data-val-required="The User Name field is required." id="UserName" name="UserName" value="" />-->
            <asp:TextBox ID="VehicleName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvVehicleName" runat="server" ErrorMessage="Please Enter Vehicle Name" ControlToValidate="VehicleName" 
                ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revVehicleName" runat="server" ErrorMessage="Please enter only alphanumeric." ControlToValidate="VehicleName" 
                ValidationExpression="^[a-zA-Z0-9 ]+$"></asp:RegularExpressionValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="UserName" data-valmsg-replace="true"></span>
        </div>-->
    </div>
    <div class="form-group">
        <label class="col-md-2 control-label" for="VehicleNumber">Vehicle Number</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:TextBox ID="VehicleNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvVehicleNumber" runat="server" ErrorMessage="Please Enter Vehicle Number" 
                ControlToValidate="VehicleNumber" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revVehicleNumber" runat="server" ErrorMessage="Please enter only alphanumeric." ControlToValidate="VehicleNumber" 
                ValidationExpression="^[a-zA-Z0-9 ]+$"></asp:RegularExpressionValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

     <div class="form-group">
        <label class="col-md-2 control-label" for="description">Description</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <textarea id="description" name="description" runat="server" cols="20" rows="2" CssClass="form-control"></textarea>
            <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ErrorMessage="Please Enter description" 
                ControlToValidate="description" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revdescription" runat="server" ErrorMessage="Please enter only alphanumeric." ControlToValidate="description" 
                ValidationExpression="^[a-zA-Z0-9 ]+$"></asp:RegularExpressionValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="DateAdded">Date Added</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:TextBox ID="DateAdded" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDateAdded" runat="server" ErrorMessage="Please Enter Valid Date" 
                ControlToValidate="DateAdded" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
           <asp:CompareValidator id="cvDateAdded" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="DateAdded" ErrorMessage="Please enter a valid date.">
            </asp:CompareValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="PerformancePeriod">Performance Period</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:TextBox ID="PerformancePeriod" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPerformancePeriod" runat="server" ErrorMessage="Please Enter Performance Period" 
                ControlToValidate="PerformancePeriod" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPerformancePeriod" runat="server" ErrorMessage="Please enter only alphanumeric." ControlToValidate="PerformancePeriod" 
                ValidationExpression="^[a-zA-Z0-9 ]+$"></asp:RegularExpressionValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="Teaming Partner">Teaming Partners</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:ListBox ID="teaming_partner" runat="server" DataSourceID="SqlDS1" SelectionMode="multiple" DataTextField="company_name" 
                     DataValueField="company_name" AppendDataBoundItems="true"></asp:ListBox>  
             <asp:SqlDataSource ID="SqlDS1" runat="server" 
                 ConnectionString="Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true" 
                 SelectCommand="SELECT company_name FROM partners"></asp:SqlDataSource>
            <asp:RequiredFieldValidator ID="rfvteaming_partner" runat="server" ErrorMessage="Please select at least one teaming partners" 
                ControlToValidate="teaming_partner" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="ResponseTime">Response TimeFrame</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:TextBox ID="ResponseTime" runat="server" CssClass="form-control" TextMode="Number" min="0" default="24"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResponseTime" runat="server" ErrorMessage="Please Enter Response Time" 
                ControlToValidate="ResponseTime" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label" for="Contract Manager">Contract Manager</label>
        <div class="col-md-3">
            <!--
            <input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />-->
            <asp:ListBox ID="contract_manager" runat="server" DataSourceID="SqlDS2" DataTextField="name" 
                     DataValueField="name" AppendDataBoundItems="true"></asp:ListBox>  
             <asp:SqlDataSource ID="SqlDS2" runat="server" 
                 ConnectionString="Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true" 
                 SelectCommand="SELECT Name FROM employee where role='Contract Manager'"></asp:SqlDataSource>
            <asp:RequiredFieldValidator ID="rfvcontract_manager" runat="server" ErrorMessage="Please select contract manager" 
                ControlToValidate="contract_manager" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <!--
        <div class="col-md-offset-2 col-md-10">
            <span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
        </div>-->
    </div>

    
    <div class="form-group">
        <div class="col-md-offset-2 col-md-6">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"/>
        </div>
    </div>
    
   

<input name="__RequestVerificationToken" type="hidden" value="CfDJ8Ok5W1qPL9FAnW8Ykj6aTiEmZDqppJPbOvBklAm3Ipe3yVpSW7zzbg9kNmrthOmS8geaT2TwtgryI_3bV4A2_OffpRYTVoKoFOXA4MCck29Q8bac_p2OImAeR5_4IGzQHiZzMJiPpTGPKlXZxnN5ZI0" />
</asp:Content>