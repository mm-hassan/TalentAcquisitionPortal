<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TalentAcquisitionPortal.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="content-wrapper">
     <h1>Form</h1>
    <asp:TextBox runat="server" ID="text1"></asp:TextBox>
    <asp:Button runat="server" onclick="Unnamed_Click" Text="submit"/>

        </div>
</asp:Content>
