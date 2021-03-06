﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResourcesUsed.aspx.cs" Inherits="HMS.ResourcesUsed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 143px;
        }
        .auto-style3 {
            height: 23px;
            width: 143px;
        }
        .auto-style6 {
            width: 25px;
        }
        #one,#two,#three,#four,#five,#six{background-color:#D1D5D5;border-radius:3px;}
        #tbl{border-style:solid;border-radius:10px;border-width:1px;border-spacing:5px;box-shadow:10px 10px 5px #888888;border-color:#888888}
        #tbl tr td:first-child{padding-left:5px;}
    </style>

<body>

    <div style="margin-left:350px">
    
        <div style="font-size:2em"><strong>Resources Used</strong></div>
    
        <table id="tbl" style="width:57%;">
            <tr>
                <td id="one" class="auto-style6">Date:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtDate" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Patient ID:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtPatientID" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnEnter" runat="server" OnClick="btnEnter_Click" Text="Enter" />
                </td>
            </tr>
            <tr>
                <td id="two" class="auto-style6">Patient Name:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtPatientName" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Ward No.:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtWardNo" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td id="three" class="auto-style6">Bed No.:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtBedNo" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Medical Condition:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtMedicalCondition" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td id="four" class="auto-style6">Admission Date:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtAdmissionDate" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Admission No.:</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtAdmissionNo" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td id="five" class="auto-style6">Resources Used:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlResources" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Equipment Used:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlEquipment" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td id="six" class="auto-style6">Medicine:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlMedicine" runat="server" >
                    </asp:DropDownList>
                &nbsp;Quantity:&nbsp;
                    <asp:TextBox ID="txtQty" runat="server" MaxLength="2" Width="16px"></asp:TextBox>
&nbsp;Tablet:
                    <asp:TextBox ID="txtTablet" runat="server" MaxLength="2" Width="16px"></asp:TextBox>
&nbsp; Times:
                    <asp:TextBox ID="txtTimes" runat="server" MaxLength="2" Width="16px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
&nbsp;
                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
                </td>
            </tr>
        </table>
    
    </div>
    <br />
</body>
</html>
</asp:Content>