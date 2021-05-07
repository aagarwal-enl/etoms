<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contracts.aspx.cs" Inherits="ETOMS.contracts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="addcontract" runat="server" Text="Add New Contracts" /> <br /><br />
    <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grid1_SelectedIndexChanged" >  
     <AlternatingRowStyle BackColor="White" />  
     <columns>  
         <asp:TemplateField HeaderText="VehicleID">  
             <ItemTemplate>  
                 <!--<asp:TextBox ID="VehicleId" runat="server" CssClass="form-control" Text='<%#Bind("vehicle_id") %>'></asp:TextBox>-->
                 <asp:Label ID="LblVehicleId" runat="server" Text='<%#Bind("vehicle_id") %>'></asp:Label>
             </ItemTemplate>  
         </asp:TemplateField>  
         <asp:TemplateField HeaderText="Vehicle Name">  
             <ItemTemplate>  
                 <asp:TextBox ID="VehicleName" runat="server" CssClass="form-control" Text='<%#Bind("vehicle_name") %>'></asp:TextBox>
                 <!--<asp:Label ID="LblVehicleName" runat="server" Text='<%#Bind("vehicle_name") %>'></asp:Label>-->  
             </ItemTemplate>  
         </asp:TemplateField>  
         <asp:TemplateField HeaderText="Vehicle Number">  
             <ItemTemplate>  
                 <asp:TextBox ID="VehicleNumber" runat="server" CssClass="form-control" Text='<%#Bind("vehicle_number") %>'></asp:TextBox>
                <!-- <asp:Label ID="LblVehicleNumber" runat="server" Text='<%#Bind("vehicle_number") %>'></asp:Label>  -->
             </ItemTemplate>  
         </asp:TemplateField>  
         <asp:TemplateField HeaderText="Teaming Partner">  
             <ItemTemplate>  
                 <!--<asp:TextBox ID="TeamingPartner" runat="server" CssClass="form-control" Text='<%#Bind("partner_name") %>'></asp:TextBox>-->
                 <asp:ListBox ID="teaming_partner" runat="server" DataSourceID="SqlDS1" SelectionMode="multiple" DataTextField="name" DataValueField="name" AppendDataBoundItems="true"></asp:ListBox>  
                 <asp:SqlDataSource ID="SqlDS1" runat="server" 
                 ConnectionString="Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true" 
                 SelectCommand="SELECT partner_name FROM partners"></asp:SqlDataSource>
                 <!--<asp:Label ID="LblTeamingPartner" runat="server" Text='<%#Bind("c_mobile") %>'></asp:Label> --> 
             </ItemTemplate>  
         </asp:TemplateField>  
        <asp:TemplateField HeaderText="Description">  
             <ItemTemplate>  
                 <asp:TextBox ID="Description" runat="server" CssClass="form-control" Text='<%#Bind("description") %>'></asp:TextBox>
                 <!--<asp:Label ID="LblTeamingPartner" runat="server" Text='<%#Bind("c_mobile") %>'></asp:Label> --> 
             </ItemTemplate>  
         </asp:TemplateField> 
         <asp:TemplateField HeaderText="Date Added">  
             <ItemTemplate>  
                 <asp:TextBox ID="DateAdded" runat="server" CssClass="form-control" TextMode="Date" Text='<%#Bind("date_added") %>'></asp:TextBox>
                 <!--<asp:Label ID="LblDate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>  -->
             </ItemTemplate>  
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Performance Period">  
             <ItemTemplate>  
                 <asp:TextBox ID="PerformancePeriod" runat="server" CssClass="form-control" Text='<%#Bind("performance_period") %>'></asp:TextBox>
                 <!--<asp:Label ID="LblTeamingPartner" runat="server" Text='<%#Bind("c_mobile") %>'></asp:Label> --> 
             </ItemTemplate>  
         </asp:TemplateField> 
         <asp:TemplateField HeaderText="Response Timeframe">  
             <ItemTemplate>  
                 <asp:TextBox ID="ResponseTimeframe" runat="server" CssClass="form-control" Text='<%#Bind("eoi_response_timeframe") %>'></asp:TextBox>
                 <!--<asp:Label ID="LblTeamingPartner" runat="server" Text='<%#Bind("c_mobile") %>'></asp:Label> --> 
             </ItemTemplate>  
         </asp:TemplateField> 
          <asp:TemplateField HeaderText="Contract Manager">  
             <ItemTemplate>  
                 <!--<asp:TextBox ID="ContractManager" runat="server" CssClass="form-control" Text='<%#Bind("partner_name") %>'></asp:TextBox>-->
                 <asp:ListBox ID="contract_manager" runat="server" DataSourceID="SqlDS2" DataTextField="name" DataValueField="name"></asp:ListBox>  
                 <asp:SqlDataSource ID="SqlDS2" runat="server" 
                 ConnectionString="Data Source=etoms.csdwtoldfxam.us-east-1.rds.amazonaws.com;Initial Catalog = etoms;User ID = admin;Password = password;MultipleActiveResultSets=true" 
                 SelectCommand="SELECT Name FROM employee where role='Contract Manager'"></asp:SqlDataSource>
                 <!--<asp:Label ID="LblTeamingPartner" runat="server" Text='<%#Bind("c_mobile") %>'></asp:Label> --> 
             </ItemTemplate>  
         </asp:TemplateField>
          <asp:TemplateField>  
             <ItemTemplate>  
                  <asp:Button ID="Edit_CV" runat="server" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Edit" CssClass="btn btn-success"/>
             </ItemTemplate>  
         </asp:TemplateField>
     </columns>  
     <EditRowStyle BackColor="#2461BF" />  
     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />  
     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />  
     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />  
     <RowStyle BackColor="#EFF3FB" />  
     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />  
     <SortedAscendingCellStyle BackColor="#F5F7FB" />  
     <SortedAscendingHeaderStyle BackColor="#6D95E1" />  
     <SortedDescendingCellStyle BackColor="#E9EBEF" />  
     <SortedDescendingHeaderStyle BackColor="#4870BE" />  
 </asp:GridView>
</asp:Content>
