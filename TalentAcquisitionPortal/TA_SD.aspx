<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA_SD.aspx.cs" Inherits="TalentAcquisitionPortal.TA_SD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
      <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>
     <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <style>
        body {
            font-family: 'Roboto', sans-serif;
            background-color: #f8f9fa;
        }

        .content-wrapper {
            padding: 10px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        h1 {
            font-family: inherit;
            font-weight: 500;
            line-height: 1.2;
            color: inherit;
        }

        /* Style for textboxes */

        input[type="checkbox"],
        select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
        }


        .evaluation-card {
            width: 100%;
            height: 300px;
            border: 1px solid #ccc;
            border-radius: 8px;
            overflow-y: auto;
            padding: 20px;
            box-sizing: border-box;
            background-color: #f9f9f9;
        }

        .card-heading {
            text-align: center;
            font-size: 1.5em;
            margin-bottom: 15px;
            color: #007bff;
        }

        .legend-list {
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

            .legend-list li {
                margin-bottom: 8px;
            }

                .legend-list li strong {
                    margin-right: 5px;
                }


        .invisible-border {
            border: none;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
            border-radius: 5px;
            overflow: hidden;
            box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
        }

        th {
            color: white;
            background-color: #343a40;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }


        .center-text {
            margin: 0 auto;
            text-align: center;
        }


        textbox:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .text-right mt-3 {
            padding-bottom: 90px;
            margin-top: 90px;
        }

        .txt {
            padding-left: 10%;
            padding-bottom: 0px;
        }

        .btn.btn-dark {
            color: white; /* Set the desired text color */
        }

            .btn.btn-dark:hover {
                color: white; /* Set the same text color to maintain consistency */
            }

        .loading-panel {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.9);
            z-index: 9999;
        }

        .loading-spinner {
            border: 4px solid #f3f3f3; /* Light grey */
            border-top: 4px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
            margin: auto;
            margin-top: 45vh; /* Adjust vertical position */
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .loading-panel p {
            text-align: center;
            font-size: 20px;
            color: #555;
            margin-top: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
            border-radius: 5px;
            overflow: hidden;
            box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
        }

        th {
            color: white;
            background-color: #343a40;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }

        textbox:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper">






       <%-- <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0">Dashboard For SD</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="#">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard v2</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>--%>



        <div class="row">

             <div class="col-lg-4 col-5">
                 <h1 class="m-0">Dashboard (SD)</h1>
                 
                 </div>

           <%-- <div class="col-lg-3 col-6">
                <asp:DropDownList runat="server" CssClass="form-control" ID="Select_Hiring" AutoPostBack="true" OnSelectedIndexChanged="Select_Hiring_SelectedIndexChanged">
                    <asp:ListItem Text="Select Type" Value="0" Selected="True" />
                    <asp:ListItem Text="New" Value="N" />
                    <asp:ListItem Text="Replacement" Value="R" />
                </asp:DropDownList>
            </div>--%>

            <div class="col-lg-3 col-6">
                <asp:DropDownList runat="server" CssClass="form-control" ID="Select_Hiring" AutoPostBack="true" OnSelectedIndexChanged="Select_Hiring_SelectedIndexChanged">
                    <asp:ListItem Text="Select Type" Value="0" Selected="True" />
                    <asp:ListItem Text="New" Value="N" />
                    <asp:ListItem Text="Replacement" Value="R" />
                </asp:DropDownList>
                <%--<div class="small-box bg-success">
                    <div class="inner">
                        <h3>53<sup style="font-size: 20px"></sup></h3>
                        <p>Pending Request</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-clipboard-list"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>--%>
            </div>

            <div class="col-lg-2 col-5">
                </div>

            <div class="col-lg-3 col-6">
                 <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard v2</li>
                        </ol>
                </div>
            

<%--            <div class="col-lg-3 col-6">

                <div class="small-box bg-warning">
                    <div class="inner">
                        <h3>44</h3>
                        <p>Applicant Evaluation</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-database"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>
            </div>



            <div class="col-lg-3 col-6">

                <div class="small-box bg-danger">
                    <div class="inner">
                        <h3>65</h3>
                        <p>Unique Visitors</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-thumbs-up"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>
            </div>--%>

        </div>





        <section id="Ex_Emp" runat="server">
            <div class="card">
                <div class="card-header" style="background-color: #363940; color: white;">
                    <h2 class="card-title">Ex-Employees</h2>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <!-- Card Body -->

                <div class="card-body p-1">
                    <div class="card">
                        <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                            <asp:GridView ID="gv_ExEmp_Requests" runat="server" CssClass="table table-bordered table-striped text-center" Font-Size="14px" GridLines="None" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_View" type="button" CssClass="btn btn-dark btn-view" runat="server" CommandName="View" CommandArgument='<%# Eval("BUDGET_REF_NO") + "|" + Eval("EMP_CD") %>' OnClick="lbl_View_Click_ExEmp">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMP_CD" HeaderText="Emp Code" />
                                    <asp:BoundField DataField="EMP_NAME" HeaderText="Name" />
                                    <asp:BoundField DataField="EMP_STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="BUDGET_REF_NO" HeaderText="Budget" />
                                    <asp:BoundField DataField="LAST_DUTY" HeaderText="Last Duty" />

                                </Columns>


                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="card-footer">

                    <div class="row">
                        <div class="col-8">
                        </div>
                        <div class="col-4 text-right">
                            <asp:Label ID="lbl_EX_GridMsg" runat="server" />
                        </div>
                    </div>


                </div>
            </div>
        </section>



        <section id="New_Pending" runat="server">
            <div class="card">
                <div class="card-header" style="background-color: #363940; color: white;">
                    <h2 class="card-title"><i class="far fa-file-alt"></i> Vacant Request</h2>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                            <i class="fas fa-minus"></i>
                        </button>
                    </div>
                </div>
                <!-- Card Body -->

                <div class="card-body p-1">
                    <div class="card">
                        <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                            <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" Font-Size="14px" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_View" type="button" CssClass="btn btn-dark btn-view" runat="server" CommandName="View" CommandArgument='<%# Bind("BUDGET_REF_NO") %>' OnClick="lbl_View_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="budget_ref_no" HeaderText="Budget" />
                                   <%-- <asp:BoundField DataField="TOTAT_STR" HeaderText="Total_Str" />
                                    <asp:BoundField DataField="UT_BUD" HeaderText="Ut_Bud" />
                                    <asp:BoundField DataField="VACANT_BUD" HeaderText="VACANT_BUD" />--%>
                                    <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                                    <asp:BoundField DataField="REG" HeaderText="Region" />
                                    <asp:BoundField DataField="BUD_DIVISION_NAME" HeaderText="Division" />

                                    <asp:BoundField DataField="BUD_UNIT_NAME" HeaderText="Unit" />
                                    <asp:BoundField DataField="BUD_DEPARTMENT_NAME" HeaderText="Department" />
                                    <asp:BoundField DataField="BUD_SECTION_NAME" HeaderText="Section" />
                                </Columns>

                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="card-footer">

                    <div class="row">
                        <div class="col-8">
                        </div>
                        <div class="col-4 text-right">
                            <asp:Label ID="lbl_GridMsg" runat="server" />
                        </div>
                    </div>


                </div>
            </div>
        </section>









        <!-- Bootstrap Modal for TA2 Details -->
        <div class="modal fade" id="ta2Modal" tabindex="-1" role="dialog" aria-labelledby="ta2ModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ta2ModalLabel"><i class="fas fa-globe"></i> TA1 - (SD)</h5>
                        <a type="button" class="close" href="TA_SD.aspx" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </a>
                    </div>
                    <div class="modal-body">

                        <section id="div" runat="server">

                            <div class="card">
                                <div class="card-header" style="background-color: #363940; color: white;">
                                    <h2 class="card-title">Manpower Hierarchial Detail - (Budget Code: <asp:Label runat="server" ID="bdgtcd"></asp:Label>)</h2>
                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" style="color: white;" title="Collapse">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                </div>

                                <div class="card-body p-1">

                                    <!-- Table 1 -->
                                    <%--<div class="row">
                                                        <div class="col-3">
                                                        </div>
                                                        <div class="col-8">
                                    <div class="table-responsive">
                                        <table class="table-responsive">
                                                                    <thead class="thead-dark">
                                                                        <tr>
                                                                            <td colspan="4" class="text-center"><strong>Budget Summary (HeadCount)</strong></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="text-center"><strong>Approved</strong></td>
                                                                            <td class="text-center"><strong>Current</strong></td>
                                                                            <td class="text-center"><strong>Balance</strong></td>
                                                                            <td class="text-center"><strong>Actions</strong></td>
                                                                        </tr>
                                                                    </thead>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:TextBox ID="approved" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                                </td>
                                                <td class="text-center">
                                                    <asp:TextBox ID="current" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                                </td>
                                                <td class="text-center">

                                                </td>
                                                <td class="text-center">
                                                    <asp:LinkButton class="btn btn-primary btn-sm" ID="Button1" runat="server" Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'><i class="fas fa-download"></i> Report</asp:LinkButton>
                                                </td>
                                                
                                            </tr>

                                        </table>
                                    </div>

                                    </div>
                                        <div class="col-3">
                                                        </div>
                                        </div>--%>

                                        <asp:TextBox ID="approved" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="current" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center" Visible="false"></asp:TextBox>
<asp:TextBox ID="balance" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center" style="display:none;" />



                                    <div class="table-responsive card-body p-0" style="width: 100%; overflow: auto;">
                                        <div class="row">
                                            <table class="table table-sm">


                                                <tr>
                                                    <td><strong>Region CD: </strong>
                                                        <asp:TextBox ID="region" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Section: </strong>
                                                        <asp:TextBox ID="section" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Division:</strong><asp:TextBox ID="division" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Cadre: </strong>
                                                        <asp:TextBox ID="cadre" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                </tr>

                                                <tr>
                                                    <td><strong>Unit: </strong>
                                                        <asp:TextBox ID="unit" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Cadre Subclass:</strong><asp:TextBox ID="cadresubclass" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Department: </strong>
                                                        <asp:TextBox ID="department" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    <td><strong>Designation: </strong>
                                                        <asp:TextBox ID="designation" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                </tr>

                                            </table>
                                        </div>

                                        <div>

                                            <div class="card-header" style="background-color: #363940; color: white;">
                                                <h2 class="card-title">Job Profile & Detail</h2>
                                            </div>

                                            <div class="table-responsive">
                                            <asp:ValidationSummary ValidationGroup="sub" ID="ValidationSummary1" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />

                                                <table class="table responsive-table">
                                                    <tr>
                                                        <td><strong>Reports To</strong><asp:DropDownList ID="DropDownReports" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
<asp:CustomValidator ID="CustomValidatorReports" runat="server" ControlToValidate="DropDownReports" ErrorMessage="Reports To field cannot be empty" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ValidationGroup="sub" ClientValidationFunction="validateReports">*</asp:CustomValidator>                                                        </td>

                                                        <td><strong>Gender</strong>
                                                            <asp:DropDownList ID="DropDownGender" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Select Gender" Value="0"/>

                                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                                <asp:ListItem Value="O">Other</asp:ListItem>
                                                            </asp:DropDownList>
<asp:CustomValidator ID="CustomValidatorGender" runat="server" ControlToValidate="DropDownGender" ErrorMessage="Gender field cannot be empty" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ValidationGroup="sub" ClientValidationFunction="validateGender">*</asp:CustomValidator>                                                        </td>

                                                        <td><strong>Minimum Education</strong><asp:DropDownList ID="DropDownEdu" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
<asp:CustomValidator ID="CustomValidatorEdu" runat="server" ControlToValidate="DropDownEdu" ErrorMessage="Minimum Education field cannot be empty" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ValidationGroup="sub" ClientValidationFunction="validateEdu">*</asp:CustomValidator>                                                        </td>
                                                    </tr>

                                                    

                                                    <tr>
                                                        <td><strong>Minimum Experience</strong>
                                                            <asp:DropDownList ID="DropDownMinExp" CssClass="form-control" runat="server" AutoPostBack="false">
                                                                <asp:ListItem Text="Select Years" Value="0" Selected="True" />
                                                                <asp:ListItem Text="01" Value="01" />
                                                                <asp:ListItem Text="02" Value="02" />
                                                                <asp:ListItem Text="03" Value="03" />
                                                                <asp:ListItem Text="04" Value="04" />
                                                                <asp:ListItem Text="05" Value="05" />
                                                                <asp:ListItem Text="06" Value="06" />
                                                                <asp:ListItem Text="07" Value="07" />
                                                                <asp:ListItem Text="08" Value="08" />
                                                                <asp:ListItem Text="09" Value="09" />
                                                                <asp:ListItem Text="10" Value="10" />
                                                                <asp:ListItem Text="11" Value="11" />
                                                                <asp:ListItem Text="12" Value="12" />
                                                                <asp:ListItem Text="13" Value="13" />
                                                                <asp:ListItem Text="14" Value="14" />
                                                                <asp:ListItem Text="15" Value="15" />
                                                                <asp:ListItem Text="16" Value="16" />
                                                                <asp:ListItem Text="17" Value="17" />
                                                                <asp:ListItem Text="18" Value="18" />
                                                                <asp:ListItem Text="19" Value="19" />
                                                                <asp:ListItem Text="20" Value="20" />
                                                                <asp:ListItem Text="21" Value="21" />
                                                                <asp:ListItem Text="22" Value="22" />
                                                                <asp:ListItem Text="23" Value="23" />
                                                                <asp:ListItem Text="24" Value="24" />
                                                                <asp:ListItem Text="25" Value="25" />


                                                            </asp:DropDownList>
<asp:CustomValidator ID="CustomValidatorMinExp" runat="server" ControlToValidate="DropDownMinExp" ErrorMessage="Minimum Experience field cannot be empty" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ValidationGroup="sub" ClientValidationFunction="validateMinExp">*</asp:CustomValidator>                                                        </td>

                                                        <td><strong>Minimum Age</strong><asp:TextBox ID="TextBox14" type="number" class="form-control" runat="server"></asp:TextBox></td>
                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="TextBox14" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Minimum Age field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>


                                                        <td><strong>Maximum Age</strong><asp:TextBox ID="TextBox16" type="number" class="form-control" runat="server"></asp:TextBox></td>
                                                         <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="TextBox16" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Maximum Age field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                    </tr>

                                                    <tr>
                                                        <td><strong>Remarks</strong><asp:TextBox ID="TextBox12" class="form-control" type="text" runat="server"></asp:TextBox></td>

                                                        <td colspan="1"><strong>Vacancies</strong><asp:TextBox ID="vacancies" class="form-control" type="number" oninput="validateVacancies()" runat="server"></asp:TextBox></td>
                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="vacancies" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Vacancy field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                                        <td colspan="1"><strong>Job Description</strong><asp:TextBox ID="TextBox10" class="form-control" type="text" runat="server"></asp:TextBox></td>


                                                    </tr>


                                                </table>
                                            </div>

                                           





                                            <%--<div class="card-header" style="background-color: #363940; color: white;">
                                                <h2 class="card-title">Pay &amp; Benefits</h2>
                                            </div>
                                            <div class="table-responsive">
                                                <table class="table responsive-table">
                                                    <tr>
                                                        <td><strong>Approved ROP</strong><asp:TextBox ID="TextBox3" type="text" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Production Allow</strong><asp:TextBox ID="TextBox5" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Overtime</strong><asp:TextBox ID="TextBox7" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Benefits</strong><asp:TextBox ID="TextBox9" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <strong>Total Gross Pay</strong><asp:TextBox ID="TextBox17" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Attendance Allow</strong><asp:TextBox ID="TextBox18" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Festival OT</strong><asp:TextBox ID="TextBox19" class="form-control" type="text" runat="server" ReadOnly="true"></asp:TextBox></td>

                                                    </tr>
                                                </table>
                                            </div>--%>

                                        </div>
                                    </div>
                                </div>

                            </div>


                        </section>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton class="btn btn-primary btn-sm" ID="LinkButton1" runat="server" Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'  onclick="ButtonReport_TA1_Click"><i class="fas fa-download"></i> Report</asp:LinkButton>
                        <a type="button" class="btn btn-secondary" href="TA_SD.aspx" >Close</a>
                        <asp:LinkButton ID="submiit"  CommandArgument='<%# Bind("BUDGET_REF_NO") %>' runat="server" type="button" class="btn btn-success float-right" ValidationGroup="sub" OnClick="Submit_TA1"><i class="far fa-credit-card"></i> Submit Requisition</asp:LinkButton>
<%--                         <asp:Button ID="submit"  CommandArgument='<%# Bind("BUDGET_REF_NO") %>' runat="server" type="button" Text="Submit" Class="btn btn-primary badge-pill" OnClick="Submit_TA1" />--%>
                   
                    
                    </div>
                </div>
            </div>
        </div>

        <asp:TextBox runat="server" ID="Rep_EmpCd" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="Rep" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="budget_Code" Visible="false"></asp:TextBox>

        <!-- jQuery -->
        <script src="https://code.jquery.com/jquery-3.5.1.min.js" crossorigin="anonymous"></script>
        <!-- Bootstrap Bundle with Popper -->
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script>
            $(document).ready(function () {
                // Your other JavaScript code here...
            });

            function showNoDataToast() {
                alert("There is no data to show.");
            }

            function showDatabaseErrorToast() {
                alert("An error occurred while accessing the database.");
            }
        </script>


         <script type="text/javascript">
             function validateReports(sender, args) {
                 var dropdown = document.getElementById('<%= DropDownReports.ClientID %>');
                args.IsValid = dropdown.value !== "0";
            }

            function validateGender(sender, args) {
                var dropdown = document.getElementById('<%= DropDownGender.ClientID %>');
                args.IsValid = dropdown.value !== "0";
            }

            function validateEdu(sender, args) {
                var dropdown = document.getElementById('<%= DropDownEdu.ClientID %>');
                args.IsValid = dropdown.value !== "0";
            }

            function validateMinExp(sender, args) {
                var dropdown = document.getElementById('<%= DropDownMinExp.ClientID %>');
                args.IsValid = dropdown.value !== "0";
            }

            function validateVacancies() {
                var balance = parseInt(document.getElementById('<%= balance.ClientID %>').value);
                var vacancies = parseInt(document.getElementById('<%= vacancies.ClientID %>').value);
                if (vacancies > balance) {
                    alert('Vacancies cannot exceed the balance.');
                    document.getElementById('<%= vacancies.ClientID %>').value = '';
                }
            }

             function showSuccessToast() {
                 console.log('showSuccessToast called');
                 toastr.success('New requisition sucessfully inserted! ', 'Success');
             }

             function showUpdateToast() {
                 console.log('showUpdateToast called');
                 toastr.success('Action sucessfully updated!', 'Success');
             }

             function showErrorToast() {
                 console.log('showErrorToast called');
                 toastr.error('Requisition not sucessfully updated!', 'Error');
             }

             function showDatabaseErrorToast() {
                 console.log('showErrorToast called');
                 toastr.error('Database backend error!', 'Error');
             }

             function showInfoToast() {
                 console.log('showErrorToast called');
                 toastr.info('Requisition Rejected !', 'Info');

             }
        </script>

    </div>
</asp:Content>
