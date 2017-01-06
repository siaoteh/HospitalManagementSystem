<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PatientCreate.aspx.cs" Inherits="StaffManagement.PatientCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">

    .auto-style2 {
        width: 158px;
        height: 26px;
    }
    .auto-style3 {
            height: 26px;
            width: 160px;
        }
        .auto-style8 {
            width: 158px;
            height: 34px;
        }
        .auto-style9 {
            height: 34px;
            width: 160px;
        }
        
    .auto-style1 {
            width: 158px;
        }
        .auto-style10 {
            width: 160px;
        }
        #pwEmail {
            width: 150px;
        }
        #pwPassword {
            width: 150px;
        }
        #pwConfirm {
            width: 150px;
        }
    .auto-style4 {
        width: 158px;
        height: 30px;
    }
    .auto-style5 {
        height: 30px;
            width: 160px;
        }
        </style>

    <div class="background">
        <div>
            <h1 style="color:darkred">Patient Registration</h1>
        </div>
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style2">Name :</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tbName" runat="server" Width="150px" ToolTip="exp: Hishiko Low Huan Yi"></asp:TextBox>
                    
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbName" ErrorMessage="Please insert Staff Name." ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbName" ErrorMessage="Name must in character." ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8" style="vertical-align:top">IC Number :</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbIC" runat="server" Width="150px" ToolTip="exp: 951213149999"></asp:TextBox>
                        <asp:Label ID="lblIcCheckMesg" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                    </td>
                    <td style="vertical-align:top">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbIC" ErrorMessage="Please insert Staff IC Number." ForeColor="Red" ValidationGroup="IcCheck">*</asp:RequiredFieldValidator>
                    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="tbIC" ErrorMessage="Invalid IC Number. (exp: 951213148888)" ForeColor="Red" ValidationExpression="\d{12}" ValidationGroup="IcCheck">*</asp:RegularExpressionValidator>
                    
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" ValidationGroup="IcCheck" CausesValidation="true"/>
                        </td>
                </tr>
                <tr>
                    <td class="auto-style1">Gender :</td>
                    <td class="auto-style10">
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rblGender" ErrorMessage="Please Select a Gender" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Home Address :</td>
                    <td class="auto-style10">
                        <asp:TextBox ID="tbAddress" runat="server" Width="150px" ToolTip="exp: No.62 Jalan D Taman Batu"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbAddress" ErrorMessage="Please insert Address" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Contact Number :</td>
                    <td class="auto-style10">
                        <asp:TextBox ID="tbContactNum" runat="server" Width="150px" ToolTip="exp: 0168528529"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbContactNum" ErrorMessage="Please insert Contact Number" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbContactNum" ErrorMessage="Invalid Contact Number. (exp: 0168528999)" ForeColor="Red" ValidationExpression="\d{10}" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    
                    </td>

                </tr>
                <tr>
                    <td class="auto-style2">Email Address :</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tbEmail" runat="server" Width="150px" ToolTip="exp: abc@gmail.com"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbEmail" ErrorMessage="Please insert Email" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbEmail" ErrorMessage="Invalid Email Address. (exp: abc@hotmail.com)" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style10"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1">Login Registration</td>
                    <td class="auto-style10">For Making Appointment</td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1">Login ID : </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="tbLoginId" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbLoginId" ErrorMessage="Please insert Login ID" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password :</td>
                    <td class="auto-style3">
                        <input id="pwPassword" type="password" runat="server"/></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="pwPassword" ErrorMessage="Please insert Password for Login" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Confirm Password :</td>
                    <td class="auto-style10">
                        <input id="pwConfirm" type="password" runat="server"/></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="pwConfirm" ErrorMessage="Please insert Confirm Password" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Security Question :</td>
                    <td class="auto-style10">
                        <asp:DropDownList ID="ddlSecurityQuestion" runat="server" Width="160px">
                            <asp:ListItem Value="0">--Please Choose One--</asp:ListItem>
                            <asp:ListItem>What is your pet&#39;s name?</asp:ListItem>
                            <asp:ListItem>Where you born?</asp:ListItem>
                            <asp:ListItem>Who is your idol?</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSecurityQuestion" ErrorMessage="Please Select a Security Question" ForeColor="Red" InitialValue="0" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Security Answer :</td>
                    <td class="auto-style10">
                        <asp:TextBox ID="tbSecurityPw" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="tbSecurityPw" ErrorMessage="Please insert Security Answer." ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td class="auto-style5">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit" CausesValidation="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    
    </div>
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" HeaderText="Please Check Again:" ValidationGroup="Submit" />
        <asp:ValidationSummary ID="ValidationSummary2" ShowMessageBox="true" ShowSummary="false" runat="server" HeaderText="Please Check Again:" ValidationGroup="IcCheck" />
</asp:Content>