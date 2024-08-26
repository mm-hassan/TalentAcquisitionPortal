<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HOD_Review.aspx.cs" Inherits="TalentAcquisitionPortal.HOD_Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
      <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>
     <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

     <style>
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
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }

    .loading-panel p {
        text-align: center;
        font-size: 20px;
        color: #555;
        margin-top: 20px;
    }
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
            height: 350px;
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
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">

        <%--<div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0">Dashboard</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard v1</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>--%>



        <div class="row">
            

            <div class="col-lg-3 col-6">
                <asp:Label ID="lbl_HODApprovalsCount" runat="server" Visible="false"></asp:Label>
                <div class="small-box bg-info">
                    <div class="inner">
                        <h3><%= lbl_HODApprovalsCount.Text %></h3>
                        <p>HOD Approvals (TA2)</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-check-circle"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>
            </div>

            <div class="col-lg-3 col-6">
                <asp:Label ID="lbl_ApplicantDetailsCount" runat="server" Visible="false"></asp:Label>
                <div class="small-box bg-success">
                    <div class="inner">
                        <h3><%= lbl_ApplicantDetailsCount.Text %></h3>
                        <p>Applicant Details (TA4)</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-clipboard-list"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>
            </div>

            <div class="col-lg-3 col-6">
                <asp:Label ID="lbl_ApplicanEvaluationCount" runat="server" Visible="false"></asp:Label>
                <div class="small-box bg-warning">
                    <div class="inner">
                        <h3><%= lbl_ApplicanEvaluationCount.Text %></h3>
                        <p>Applicant Evaluation (TA5)</p>
                    </div>
                    <div class="icon">
                        <i class="fas fa-database"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                </div>

            </div>

            <div class="col-lg-3 col-6">

                <div class="small-box">
                    <ol class="breadcrumb ">
                            <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard v1</li>
                        </ol>
                    
                </div>
            </div>

            <%--<div class="col-lg-3 col-6">

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










        <div class="row">
            <div class="col-md-6">
                <section class="content">
                    <!-- Pending Requests Card -->
                    <div class="card">
                        <div class="card-header" style="background-color: #363940; color: white;">
                            <h2 class="card-title"><i class="fas fa-check-circle"></i> HOD Approvals (TA2)</h2>

                            <div class="card-tools">
                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                    <i class="fas fa-minus"></i>
                                </button>
                            </div>

                        </div>

                        <!-- Card Body -->

                        <div class="card-body p-1">
                            <div class="card">
                                <div class="card-body p-0" style="width: 100%; height: 350px; overflow: auto;">
                                    <asp:GridView ID="gv_TA2PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" OnPageIndexChanging="gv_RecentApprovals_PageIndexChanging" Font-Size="12px" AutoGenerateColumns="False">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("JOB_REQ_ID") %>' OnClick="lbl_TA2_View_Click" OnClientClick="return validateAndShowSpinner()">View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JOB_REQ_ID" HeaderText="Request" />
                                            <asp:BoundField DataField="BUDGET_REF_NO" HeaderText="Budget" />
                                            <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />
                                            <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />

                                        </Columns>

                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="card-footer">

                            <div class="row">
                                <div class="col-4">
                                </div>
                                <div class="col-8 text-right">
                                    <asp:Label ID="lbl_TA2GridMsg" runat="server" />
                                </div>
                            </div>


                        </div>


                    </div>
                    <asp:TextBox ID="TA2_Super" Visible="false" runat="server"></asp:TextBox>
                </section>
            </div>




            <div class="col-md-6">

                <section class="content">

                    <div class="card">
                        <div class="card-header" style="background-color: #363940; color: white;">
                            <h2 class="card-title"><i class="fas fa-clipboard-list"></i> Applicant Details (TA4)</h2>
                            <div class="card-tools">
                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                    <i class="fas fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="card-body p-1">
                            <div class="card">
                                <div class="card-body p-0" style="width: 100%; height: 350px; overflow: auto;">

                                    <asp:GridView ID="gv_TA4PendingRequests" runat="server" Font-Size="12px" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("JOB_REQ_ID") %>' OnClick="lbl_TA4_View_Click" OnClientClick="return validateAndShowSpinner()">View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TRACKING_ID" HeaderText="Tracking" />
                                            <asp:BoundField DataField="JOB_REQ_ID" HeaderText="Request" />
                                            <asp:BoundField DataField="BUDGET_CD" HeaderText="Budget" />
                                            <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" />
                                            <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="card-footer">

                            <div class="row">
                                <div class="col-4">
                                </div>
                                <div class="col-8 text-right">
                                    <asp:Label ID="lbl_TA4_GridMsg" runat="server" />
                                </div>
                            </div>


                        </div>
                    </div>
                    <asp:TextBox ID="Super_TA4" Visible="false" runat="server"></asp:TextBox>
                </section>
            </div>
        </div>











        <!-- Bootstrap CSS -->
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
        <!-- jQuery and Bootstrap JS -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>





        <%--        ////////////////////////////////////////////////////                TA2 all details                   ///////////////////////--%>

        <!-- Bootstrap Modal for TA2 Details -->
        <div class="modal fade" id="ta2Modal" tabindex="-1" role="dialog" aria-labelledby="ta2ModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ta2ModalLabel"><i class="fas fa-globe"></i> TA2 - (HOD)</h5>
                        
                        <asp:LinkButton class="btn btn-info badge-pill ml-1" ID="Button1" runat="server" OnClick="ButtonHold_TA2_Click" Visible="true" OnClientClick="return validateAndShowSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'><i class="fas fa-sync-alt"></i> Hold</asp:LinkButton>                                                                                                                                    
                        <asp:LinkButton class="btn btn-danger badge-pill ml-1" ID="Button2" runat="server" OnClick="ButtonReject_TA2_Click" Visible="true" OnClientClick="return validateAndShowSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'><i class="fas fa-trash"></i> Reject</asp:LinkButton>
                         <asp:Button class="btn btn-success badge-pill ml-1" ID="Button7" runat="server" Text="Approve" OnClick="ButtonApprove_TA2_Click" Visible="true" OnClientClick="return validateAndShowSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>' />

                        <a type="button" class="close" href="HOD_Review.aspx" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </a>
                    </div>
                    <div class="modal-body">
                        <!-- The content that will be populated via the code-behind -->
                        <section id="TA2_Section" runat="server">
                            <div id="hiring_info" runat="server">

                                <div class="card">
                                    <div class="card-header" style="background-color: #363940; color: white;">
                                        <h2 class="card-title">Hiring Detail</h2>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table">
                                            <tr>
                                                <td><strong>Hiring Type</strong>
                                                    <asp:TextBox ID="Hir_Type_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                <td><strong>Notes by HOD</strong>
                                                    <asp:TextBox ID="Notes_HOD_TA2" type="text" runat="server" CssClass="form-control"></asp:TextBox></td>

                                                <td>

                                                    <asp:LinkButton class="btn btn-success" ID="Button3" runat="server" OnClick="ButtonReport_TA2_Click"><i class="fas fa-download"></i> Show Report</asp:LinkButton>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>


                                    <div id="hiring_grid" runat="server" class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">

                                        <asp:GridView ID="Hiring_TA2_Grid" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="Hiring_TA2_Grid_RowDataBound" AutoGenerateColumns="false">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>

                                                <asp:BoundField DataField="REP_ID" HeaderText="Rep Id" />
                                                <asp:BoundField DataField="EMP_CD" HeaderText="Emp Code" />
                                                <asp:BoundField DataField="EMP_NAME" HeaderText="Emp Name" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BUDGET_REF_NO" HeaderText="Budget Ref NO" />
                                                <asp:BoundField DataField="Last_Duty" HeaderText="Last Duty" />
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>



                            <div class="row">
                                <div class="col-md">
                                    <section id="div_TA2_EmployeeDetails" runat="server">
                                        <div class="card">
                                            <div class="card-header" style="background-color: #363940; color: white;">
                                                <h2 class="card-title">Manpower Hierarchial Detail</h2>
                                                
                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-card-widget="collapse" style="color: white;" title="Collapse">
                                                        <i class="fas fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>

                                            <div class="card-body p-1">
                                                <div class="card">
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
                                                                            <asp:TextBox ID="Approved_TA2" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                                                        </td>
                                                                        <td class="text-center">
                                                                            <asp:TextBox ID="Current_TA2" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                                                        </td>
                                                                        <td class="text-center">
                                                                            <asp:TextBox ID="Balance_TA2" Size="5px" type="text" runat="server" CssClass="invisible-border text-center" ReadOnly="true"></asp:TextBox>

                                                                        </td>
                                                                        <td class="text-center">
                                                                            

                                                                            
                                                                        </td>

                                                                    </tr>

                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="col-1">
                                                        </div>
                                                    </div>--%>

                                                   <asp:TextBox ID="Approved_TA2" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center" Visible="false"></asp:TextBox>
                                                   <asp:TextBox ID="Current_TA2" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center" Visible="false"></asp:TextBox>
                                                   <asp:TextBox ID="Balance_TA2" Size="5px" type="text" runat="server" CssClass="invisible-border text-center" ReadOnly="true" Visible="false"></asp:TextBox>




                                                    <div class="table-responsive">
                                                        <table class="table table-bordered">
                                                            <tbody>
                                                                <tr>



                                                                    <td><strong>Request</strong><asp:TextBox ID="Req_Id_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Vacancies</strong><asp:TextBox ID="Vac_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Budget</strong><asp:TextBox ID="Budget_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Request Date</strong><asp:TextBox ID="Req_Dat_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>


                                                                </tr>
                                                                <tr>

                                                                    <td><strong>Region</strong>
                                                                        <asp:TextBox ID="Reg_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Section</strong><asp:TextBox ID="Sec_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Division</strong><asp:TextBox ID="Div_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Department</strong><asp:TextBox ID="Dep_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                </tr>
                                                                <tr>

                                                                    <td><strong>Cadre</strong><asp:TextBox ID="Cad_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Cadre Subclass</strong><asp:TextBox ID="Cad_Sub_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Unit</strong><asp:TextBox ID="Unit_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                    <td><strong>Designation</strong><asp:TextBox ID="Des_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>


                                                    <div class="card-header" style="background-color: #363940; color: white;">
                                                        <h2 class="card-title">Job Profile & Detail</h2>
                                                    </div>

                                                    <div class="table-responsive">
                                                        <table class="table">
                                                            <tr>
                                                                <td><strong>Reports To</strong><asp:TextBox ID="Rep_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>


                                                                <td><strong>Gender</strong><asp:TextBox ID="Gen_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Minimum Education</strong><asp:TextBox ID="Min_Edu_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Minimum Experience</strong>
                                                                    <asp:TextBox ID="Min_Exp_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Minimum Age</strong>
                                                                    <asp:TextBox ID="Min_Age_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Maximum Age</strong>
                                                                    <asp:TextBox ID="Max_Age_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Remarks</strong><asp:TextBox ID="Rem_TA2" type="text" size="20px" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Job Description</strong><asp:TextBox ID="Job_Des_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                            </tr>



                                                        </table>
                                                    </div>


                                                    <%--<div class="card-header" style="background-color: #363940; color: white;">
                                                        <h2 class="card-title">Pay &amp; Benefits</h2>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table">
                                                            <tr>
                                                                <td><strong>Approved ROP</strong><asp:TextBox ID="App_ROP_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Production Allow</strong>
                                                                    <asp:TextBox ID="Prod_Allow_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Overtime</strong>
                                                                    <asp:TextBox ID="Over_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Benefits</strong><asp:TextBox ID="Ben_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                            </tr>
                                                            <tr>
                                                                <td><strong>Total Gross Pay</strong><asp:TextBox ID="Gross_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Attendance Allow</strong><asp:TextBox ID="Att_Allow_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                <td><strong>Festival OT</strong><asp:TextBox ID="Fest_OT_TA2" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                            </tr>
                                                        </table>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>



                                    </section>
                                    <asp:TextBox ID="HOD_Approved_TA2" Size="5px" type="text" Visible="false" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>




                        </section>
                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-secondary" href="hod_review.aspx" >Close</a>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function showModal() {
                $('#ta2Modal').modal('show');
            }
        </script>





        <%--        ////////////////////////////////////////////////////                TA4 all details                   ///////////////////////--%>


        <!-- Modal -->
        <div class="modal fade" id="TA4Modal" tabindex="-1" role="dialog" aria-labelledby="TA4ModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="TA4ModalLabel"><i class="fas fa-globe"></i> TA4 - (HOD)</h5>
                        <a type="button" class="close" href="HOD_Review.aspx" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </a>
                    </div>
                    <div class="modal-body">
                        <!-- Place here the content you want to show in the modal -->
                        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-default">
                            <!-- Panel content (Table and other controls) -->
                            <!-- The content that will be populated via the code-behind -->
                            <section id="TA4_Section" runat="server">



                                <section id="div_TA4_EmployeeDetails" runat="server">
                                    <div class="card">
                                        <div class="card-header" style="background-color: #363940; color: white;">
                                            <h2 class="card-title">Job Requisition Tracking</h2>
                                            <div class="card-tools">
                                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                    <i class="fas fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="card-body p-3">
                                            <div class="card">
                                                <div class="card-body p-0" style="width: 100%; height: 100%; overflow: auto;">






                                                    <asp:Panel ID="Panel1_TA4" runat="server" CssClass="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h3 class="panel-title">Requisition Details</h3>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="table-responsive">
                                                                <table class="table ">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td><strong>Tracking Status</strong><asp:TextBox ID="Track_Status_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Request Id</strong><asp:TextBox ID="Req_ID_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Vacancies</strong><asp:TextBox ID="Vac_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Budget CD</strong><asp:TextBox ID="Bud_Cd_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Tracking ID</strong><asp:TextBox ID="Track_Id_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Hiring Type</strong><asp:TextBox ID="Hir_Type_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td><strong>Request Date</strong><asp:TextBox ID="Req_Date_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Reg CD</strong><asp:TextBox ID="Reg_Cd_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Section</strong><asp:TextBox ID="Sec_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Division</strong><asp:TextBox ID="Div_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Department</strong><asp:TextBox ID="Dep_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Cadre</strong><asp:TextBox ID="Cad_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>


                                                                        </tr>

                                                                        <tr>
                                                                            <td><strong>Cadre Subclass</strong><asp:TextBox ID="Cad_Sub_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Unit</strong><asp:TextBox ID="Unit_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Designation</strong><asp:TextBox ID="Des_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Reports To</strong><asp:TextBox ID="Rep_To_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Gender</strong><asp:TextBox ID="Gender_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Minimum Education</strong><asp:TextBox ID="Min_Edu_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Minimum Experience</strong><asp:TextBox ID="Min_Exp_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Minimum Age</strong><asp:TextBox ID="Min_Age_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td><strong>Maximum Age</strong><asp:TextBox ID="Max_Age_TA4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                                            <td colspan="3">
                                                                                <br />
                                                                                <asp:LinkButton ID="Butn4" runat="server"  CssClass="btn btn-success" OnClick="Button1_Click" ><i class="fas fa-download"></i> Requisition Report</asp:LinkButton>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>



                                                    <asp:GridView ID="Applicant_GV_TA4" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="Applicant_GV_TA4_RowDataBound" AutoGenerateColumns="false" Font-Size="13px" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="HOD Selection">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkBox_TA4" runat="server"  OnCheckedChanged="chkBox_CheckedChanged_TA4" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="APPLICANT_ID" HeaderText="Applicant ID" />
                                                            <asp:BoundField DataField="APPLICANT_NAME" HeaderText="Applicant Name" />
                                                            <%--<asp:BoundField DataField="FATHER_NAME" HeaderText="Father Name"/>--%>
                                                            <%--<asp:BoundField DataField="DOB" HeaderText="DOB" />--%>
                                                            <asp:BoundField DataField="CNIC" HeaderText="CNIC" />
                                                            <%--<asp:BoundField DataField="QUALIFICATION" HeaderText="Qualification" />--%>
                                                            <asp:BoundField DataField="LAST_SALARY" HeaderText="Last Salary" />
                                                            <asp:TemplateField HeaderText="Gender">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbl"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Action" HeaderText="Status" />
                                                            <%--<asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlAction" runat="server" OnSelectedIndexChanged="SelectedDDLChanged" Enabled="false" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="CV">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_View_Cv_TA4" Font-Size="Small" Width="60px" type="button" CssClass="btn btn-dark" runat="server" CommandName="" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_CV_TA4">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Evaluation Report">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_ER_TA4" type="button" Width="100px" Font-Size="Smaller" CssClass="btn btn-dark" runat="server" CommandName="" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_Report_TA4">Show Report</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                    <asp:TextBox ID="TextBox7" type="text" size="20px" runat="server" Style="border: none;" Visible="false" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="card-footer">

                                                <div class="row">
                                                    <div class="col-8">
                                                    </div>
                                                    <div class="col-4 text-right">
                                                        <asp:Label ID="Label1_TA4" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </section>

                                <asp:TextBox ID="TextBox2" Visible="false" runat="server"></asp:TextBox>


                            </section>

                        </asp:Panel>
                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-secondary" href="hod_review.aspx" >Close</a>
                    </div>
                </div>
            </div>
        </div>


























        <div class="row">
            <div class="col-md-6">
                <section class="content">

                    <div class="card">
                        <div class="card-header" style="background-color: #363940; color: white;">
                            <h2 class="card-title"><i class="fas fa-database"></i> Applicant Evaluation (TA5)</h2>
                            <div class="card-tools">
                                <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                    <i class="fas fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <div class="card-body p-1">
                            <div class="card">
                                <div class="card-body p-0" style="width: 100%; height: 350px; overflow: auto;">

                                    <asp:GridView ID="gv_TA5PendingRequests" runat="server" Font-Size="12px" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_Click_TA5" OnClientClick="return validateAndShowSpinner()">View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="APPLICANT_ID" HeaderText="Applicant ID" />
                                            <asp:BoundField DataField="TRACKING_ID" HeaderText="Tracking" />
                                            <asp:BoundField DataField="Budget_Ref_No" HeaderText="Budget" />
                                            <asp:BoundField DataField="APPLICANT_NAME" HeaderText="Applicant Name" />
                                            <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                                            <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />
                                            
                                            <asp:BoundField DataField="CNIC" HeaderText="CNIC" />
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="card-footer">

                            <div class="row">
                                <div class="col-4">
                                </div>
                                <div class="col-8 text-right">
                                    <asp:Label ID="lbl_TA5_GridMsg" runat="server" />
                                </div>
                            </div>


                        </div>
                    </div>
                    <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                </section>
            </div>



            <%--<div class="col-md-6  pt-4">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">Chart</h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-tool" data-card-widget="remove">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="chart">
                            <div class="chartjs-size-monitor">
                                <div class="chartjs-size-monitor-expand">
                                    <div class=""></div>
                                </div>
                                <div class="chartjs-size-monitor-shrink">
                                    <div class=""></div>
                                </div>
                            </div>
                            <canvas id="areaChart" style="min-height: 250px; height: 250px; max-height: 250px; max-width: 100%; display: block; width: 452px;" width="904" height="500" class="chartjs-render-monitor"></canvas>
                        </div>
                    </div>
                    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
                </div>
            </div>--%>


        </div>






























        <%--        ////////////////////////////////////////////////////                TA5 all details                   ///////////////////////--%>




        <!-- Modal -->
        <div class="modal fade" id="TA5Modal" tabindex="-1" role="dialog" aria-labelledby="TA4ModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="TA5ModalLabel"><i class="fas fa-globe"></i> TA5 - (HOD)</h5>
                        <a type="button" class="close" href="HOD_Review.aspx" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </a>
                    </div>
                    <div class="modal-body">
                        <!-- Place here the content you want to show in the modal -->
                        <asp:Panel ID="Panel2" runat="server" CssClass="panel panel-default">
                            <!-- Panel content (Table and other controls) -->
                            <!-- The content that will be populated via the code-behind -->
                            <section id="TA5_Section" runat="server">
                                <div class="card">
                                    <div class="card-header" style="background-color: #363940; color: white;">
                                        <h2 class="card-title">Interview Evaluation</h2>
                                        <div class="card-tools">
                                            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                                <i class="fas fa-minus"></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="card-body p-3">
                                        <div class="card">
                                            <div class="card-body p-0" style="width: 100%; height: 100%; overflow: auto;">

                                                <table class="tracking-table">
                                                    <asp:TextBox ID="Applicant_Id_TA5" type="text" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>

                                                    <tr>
                                                        <td><strong>Evaluation</strong><asp:TextBox ID="Eval_Id_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Tracking</strong>
                                                            <asp:TextBox ID="Track_Id_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Job Title</strong><asp:TextBox ID="Job_App_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>



                                                    </tr>
                                                    <tr>
                                                        <td><strong>Applicant</strong><asp:TextBox ID="Applicant_Name_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Interview Date</strong><asp:TextBox ID="Interview_Date_TA5" TextMode="date" runat="server" CssClass="form-control"></asp:TextBox></td>

                                                        <td><strong>Father Name</strong><asp:TextBox ID="Father_Name_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>



                                                    </tr>
                                                    <tr>
                                                        <td><strong>DOB</strong><asp:TextBox ID="DOB_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>CNIC</strong><asp:TextBox ID="CNIC_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Qualification</strong><asp:TextBox ID="Qualification_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>



                                                    </tr>
                                                    <tr>
                                                        <td><strong>Department</strong><asp:TextBox ID="Dep_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Candidate Ref</strong><asp:TextBox ID="Cand_Ref_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                                                        <td><strong>Last Salary</strong><asp:TextBox ID="Last_Sal_TA5" type="text" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                                    </tr>
                                                </table>

                                                <asp:ValidationSummary ValidationGroup="sub" ID="ValidationSummary1" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />

                                                <div class="card-header" style="background-color: #363940; color: white;">
                                                    <h2 class="card-title"><i class="far fa-comments"></i> Observations</h2>
                                                </div>





                                                <div class="card-body p-0" style="width: 100%; height: 100%; overflow: auto;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <div class="card-body p-0" style="width: 100%; height: 400px; overflow: auto;">
                                                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="detail_id" HeaderText="Id" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                                                                <asp:BoundField DataField="detail_name" HeaderText="Parameters" HeaderStyle-CssClass="text-center" />
                                                                                <asp:TemplateField HeaderText="Score" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="myTextBox" onchange="calculateTotal()" type="number" min="1" max="5" MaxLength="1" Size="8px" CssClass="text-center" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="myTextBox" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Score field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                                        <asp:RangeValidator ValidationGroup="sub" ID="RangeValidator1" runat="server" ControlToValidate="myTextBox" Display="Dynamic" ErrorMessage="Score should be between 1 to 5" ForeColor="Red" MaximumValue="5" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="evaluation-card">
                                                                    <h2 class="card-heading"><i class="fas fa-info"></i> Evaluation Score</h2>
                                                                    <div class="card-body">
                                                                        <ul class="legend-list">
                                                                            <li><strong>1:</strong> Below Average</li>
                                                                            <li><strong>2:</strong> Average</li>
                                                                            <li><strong>3:</strong> Good</li>
                                                                            <li><strong>4:</strong> Very Good</li>
                                                                            <li><strong>5:</strong> Excellent</li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="TotalScoreLabel" runat="server" Text="Total Score: 0/65"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>



                                                <table>
                                                    <tr>
                                                        <td><strong>Remarks Interviewer 1 (HR)</strong><asp:TextBox ID="Rem_1_TA5" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="Rem_1_TA5" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Remarks Interviewer 1 field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>


                                                        <td><strong>Salary Decided</strong><asp:TextBox ID="Sal_Decided_TA5" type="number" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="Sal_Decided_TA5" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Salaryfield cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    </tr>

                                                    <tr>
                                                        <td><strong>Remarks Interviewer 2</strong><asp:TextBox ID="Rem_2_TA5" CssClass="form-control" type="text" runat="server"></asp:TextBox></td>

                                                        <td><strong>Date of Joining</strong><asp:TextBox ID="DOJ_TA5" CssClass="form-control" TextMode="date" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="DOJ_TA5" ID="RequiredFieldValidator_DOJ" runat="server" Display="Dynamic" ErrorMessage="Date of Joining field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>


                                                    </tr>

                                                    <tr>
                                                        <td><strong>Remarks Interviewer 3</strong><asp:TextBox ID="Rem_3_TA5" CssClass="form-control" type="text" runat="server"></asp:TextBox></td>

                                                        <td><strong>Shift Details</strong><asp:TextBox ID="Shift_Dtl_TA5" CssClass="form-control" type="text" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>Other Benefits/ Comments</strong><asp:TextBox ID="Ben_TA5" type="text" CssClass="form-control" runat="server"></asp:TextBox></td>

                                                        <td><strong>Overall Recomendation</strong>

                                                            <div class="option-container">
                                                                <div class="option">
                                                                    <asp:RadioButton ID="CheckBox1_TA5" runat="server" GroupName="satisfactionGroup" Text="No" />
                                                                </div>
                                                                <div class="option">
                                                                    <asp:RadioButton ID="CheckBox2_TA5" runat="server" GroupName="satisfactionGroup" Text="Yes" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </table>



                                            </div>


                                        </div>






                                        


                                        <asp:LinkButton ID="btViewCv_TA5" runat="server" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" Font-Size="16px" OnClick="btnViewCv_TA5_Click"><i class="fas fa-eye"></i> View CV</asp:LinkButton>
                                        
                                        <asp:LinkButton ID="btnReport_TA5" runat="server" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" Font-Size="16px" OnClick="btnreport_TA5_Click"><i class="fas fa-download"></i> Evaluation Report</asp:LinkButton>

                                        <asp:Button ID="btnSubmit_TA5" ValidationGroup="sub" runat="server" Text="Submit" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" Font-Size="16px" OnClick="btnSubmit_Click_TA5" />



                                    </div>
                                </div>









                            </section>

                        </asp:Panel>
                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-secondary" href="hod_review.aspx" >Close</a>
                    </div>
                </div>
            </div>
        </div>
    </div>






    <script>

        function calculateTotal() {
            var totalScore = 0;
            var textBoxes = document.querySelectorAll("#<%= GridView1.ClientID %> input[type='number']");
            textBoxes.forEach(function (textBox) {
                totalScore += parseInt(textBox.value) || 0;
            });
            var obtainedScore = totalScore;
            // Update the total score label
            var totalPossibleScore = 65;
            document.getElementById("<%= TotalScoreLabel.ClientID %>").innerText = "Total Score: " + obtainedScore + "/" + totalPossibleScore;
        }


        function showSuccessToast() {
            console.log('showSuccessToast called');
            toastr.success('Request Sucessfully Submitted! ', 'Success');
        }

        function showUpdateToast() {
            console.log('showUpdateToast called');
            toastr.success('Action sucessfully updated!', 'Success');
        }

        function showErrorToast() {
            console.log('showErrorToast called');
            toastr.error('Request not sucessfully updated!', 'Error');
        }

        function showDatabaseErrorToast() {
            console.log('showErrorToast called');
            toastr.error('Database backend error!', 'Error');
        }

        function showInfoToast() {
            console.log('showErrorToast called');
            toastr.info('Warning: Please insert !', 'Info');

        }


        function showWarningToast() {
            console.log('showErrorToast called');
            toastr.warning('Warning: Please select a checkbox!', 'Warning');

        }

        function showRemarksWarningToast() {
            console.log('showErrorToast called');
            toastr.warning('Warning: Please enter remarks!', 'Warning');

        }

        function FailTrackingToast() {
            console.log('showErrorToast called');
            toastr.warning('Failed to insert new tracking ID!', 'Warning');

        }

        function FailApplicantToast() {
            console.log('showErrorToast called');
            toastr.warning('Failed to insert Applicant record!', 'Warning');

        }

        function showSelectionToast() {
            console.log('showSelectionToast called');
            toastr.success('Selected by HOD!', 'Success');
        }

        function showNotSelectionToast() {
            console.log('showNotSelectionToast called');
            toastr.success('Rejected by HOD!', 'Success');
        }
        
    </script>












    <script>
        // JavaScript code to create the chart
        window.onload = function () {
            var ctx = document.getElementById('areaChart').getContext('2d');

            var gradient = ctx.createLinearGradient(0, 0, 0, 250);
            gradient.addColorStop(0, 'rgba(75, 192, 192, 0.2)');
            gradient.addColorStop(1, 'rgba(75, 192, 192, 0)');

            var data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
                datasets: [{
                    label: 'Sample Data',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    backgroundColor: gradient,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1,
                    fill: true
                }]
            };

            var options = {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    x: {
                        beginAtZero: true
                    },
                    y: {
                        beginAtZero: true
                    }
                }
            };

            new Chart(ctx, {
                type: 'line',
                data: data,
                options: options
            });
        };
    </script>



<script type="text/javascript">
    function validateAndShowSpinner() {
        if (Page_ClientValidate()) {
            showLoadingSpinner();
            return true;
        }
        return false;
    }

    function showLoadingSpinner() {
        document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
                 }
</script>

<asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel" style="display: none;">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>

    </div>
</asp:Content>
