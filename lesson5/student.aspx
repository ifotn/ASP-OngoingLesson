<%@ Page Title="" Language="C#" MasterPageFile="~/lesson5.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="lesson5.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Student Details</h1>
    <h5>All fields are required</h5>
    <div class="form-group">
        <label for="txtFirstMidName" class="col-sm-3">First Name:</label>
        <asp:TextBox ID="txtFirstMidName" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Please Enter your first name!"
            CssClass="label label-danger"
            ControlToValidate="txtFirstMidName"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="txtLastName" class="col-sm-3">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="Please Enter your last name!"
            CssClass="label label-danger"
            ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <label for="txtEnrollmentDate" class="col-sm-3">Enrollment Date:</label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" MaxLength="75" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="Please Enter an enrollment date!"
            CssClass="label label-danger"
            ControlToValidate="txtEnrollmentDate"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ErrorMessage="Please enter a date in the following format: YYYY-MM-DD" 
                ControlToValidate="txtEnrollmentDate" ValidationExpression="^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$" CssClass="label label-danger"></asp:RegularExpressionValidator>
    </div>
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <br />
</asp:Content>
