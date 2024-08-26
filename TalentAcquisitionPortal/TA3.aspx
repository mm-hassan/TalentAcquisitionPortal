<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA3.aspx.cs" Inherits="TalentAcquisitionPortal.TA3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html>


    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="Bootstrap/js/jquery-1.11.2.min.js"></script>
    <script src="Bootstrap/js/bootstrap.min.js"></script>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <link href="Assest/bootstrap-4.3.1/bootstrap-4.3.1/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>



    <style>


          .invisible-border {
               border: none;
            }

     body {
            font-family: "Ubuntu", sans-serif;
           background-color: #f8f9fa;
        }
     .content-wrapper {
            padding:10px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        } 

        h1 {
           text-align: center;
            color: #007bff;
            margin-bottom: 30px;
            font-size: 1.5rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-weight: 700;
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
      
        .text-center {
            text-align: center;
        }
       
        input[type="text"], textarea {
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
            margin-bottom: 15px;
            box-sizing: border-box;
        }

        /* CSS for customizing checkboxes */
    input[type="checkbox"] {
      -webkit-appearance: none;
      -moz-appearance: none;
      width: 15px;
      height: 15px;
      border: 1.5px solid #343a40; /* Change border color as needed */
      border-radius: 50%; /* Makes it round */
      outline: none;
      cursor: pointer;
    }

    /* Styles for checked state */
    input[type="checkbox"]:checked {
      background-color: #343a40; /*Change background color for checked state */
    }

    /* Optional: Style the label for checkboxes */
    .checkbox-label {
      font-size: 14px; /* Adjust font size as needed */
      margin-left: 5px; /* Provide some space between checkbox and text */
    }
    .custom-dropdown {
        width: 200px; /* Adjust width as needed */
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
        /* Add any additional styles for appearance */
    }

    .show-button {
        margin-left: 10px; /* Adjust as needed */
        /* Add any specific styles for the 'Show' button */
    }

    .manpower-table {
        /* Add styles for the table if required */
    }

        .manpower-detail {
            margin-top: 20px; /* Adjust as needed */
        }

         

              .btn.btn-dark {
        color: white; /* Set the desired text color */
    }
    .btn.btn-dark:hover {
        color: white; /* Set the same text color to maintain consistency */
    }

    
.performance-status {
            max-width: 805px;
            margin: auto;
            background-color: #fff;
             text-align: center;
            padding: 20px;
            border-radius: 12px;
            margin-bottom: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }

        .option-container {
            display: flexbox;
            justify-content: space-between;
            margin-bottom: 15px;
        }

        .option {
            flex: 1;
            text-align: center;
        }

        .custom-textarea {
            width: 100%;
            resize: none;
        }

        .btn-submit {
            width: 100%;
        }
            
                .btn.btn-dark {
        color: white; /* Set the desired text color */
    }
    .btn.btn-dark:hover {
        color: white; /* Set the same text color to maintain consistency */
    }

.radio-container {
  display: flexbox;
  flex-direction: column;
}



label {
  font-size: 18px;
  cursor: pointer;
  padding: 12px; /* Increase padding for a larger box */
  border-radius: 8px; /* Increase border radius for rounded corners */
  margin-bottom: 10px; /* Adjust spacing between boxes */
  transition: background-color 0.3s, color 0.3s;
}





      .custom-panel {
        border: 1px solid #ccc;
        border-radius: 5px;
        margin-top: 20px;
        padding: 20px;
    }

    .panel-heading {
        background-color: #363940;
        color: white;
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
        padding: 10px 20px;
        margin: -20px -20px 20px -20px;
    }

    .form-group {
        margin-bottom: 20px;
    }

    .input-group-addon {
        background-color: #f5f5f5;
        border: 1px solid #ccc;
        width: 45px; /* Adjusted width */
        text-align: center; /* Center align icon */
    }

    .input-group-addon i {
        line-height: 34px; /* Vertically center icon */
    }

    .form-control {
        border: 1px solid #ccc;
        border-radius: 3px;
    }

    .input-group .form-control {
        width: calc(100% - 45px); /* Adjusted width */
    }

    .btn-submit {
        margin-top: 20px;
        margin-left: 20px;
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
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }

    .loading-panel p {
        text-align: center;
        font-size: 20px;
        color: #555;
        margin-top: 20px;
    }


            </style>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="content-wrapper">



           <script type="text/javascript">
               function showLoadingSpinner() {
                   document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
                  }
</script>

 <asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>

            <div class="form-title">
                <h1><strong>REQUISITION TRACKING</strong></h1>
            </div>
        
          <div class="card-tools" style="padding-bottom:10px;">
    <div class="row">
        <div class="col-md-9"></div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox type="text" id="txtSearch" class="form-control" placeholder="Enter job requisition id" runat="server"></asp:TextBox>
                <div class="input-group-append">
                        <asp:LinkButton class="btn btn-primary" type="button" id="btnSearch" onclick="btnSearch_Click" runat="server">
                        <i class="fas fa-search"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>

             <section class="content">
      
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h2 class="card-title"><i class="far fa-file-alt"></i> Pending Request</h2>
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
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" font-size="14px" GridLines="None">     
                      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                      <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("JOB_REQ_ID") %>' OnClick="lbl_View_Click" >View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Tracking_id" HeaderText="Tracking"/>
                                  <asp:BoundField DataField="job_req_id" HeaderText="Request"/>
                                  <asp:BoundField DataField="budget_ref_no" HeaderText="Budget" />        
                                  <asp:BoundField DataField="Region_Name" HeaderText="Region" />
                                  <asp:BoundField DataField="DIVISION_NAME" HeaderText="Division" />
                                  <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                                  <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />
                                  <asp:BoundField DataField="SECTION_NAME" HeaderText="Section" />
                                 <%-- <asp:BoundField DataField="status" HeaderText="Status" />--%>
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




       




        <section id="div_Details" runat="server">
    <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
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
    
  


            

                  <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">Requisition Details</h3>
    </div>
    <div class="panel-body">
        <div class="table-responsive">
            <table class="table table-bordered">
                <tbody>
                    <tr>
                        <td><strong>Tracking Status</strong><asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Request Id</strong><asp:TextBox ID="Text29" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Vacancies</strong><asp:TextBox ID="Text30" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Budget CD</strong><asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Tracking ID</strong><asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Hiring Type</strong><asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        
                    </tr>

                    <tr>
                        <td><strong>Request Date</strong><asp:TextBox ID="Text31" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Reg CD</strong><asp:TextBox ID="Text21" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Section</strong><asp:TextBox ID="Text46" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Division</strong><asp:TextBox ID="Text43" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Department</strong><asp:TextBox ID="Text45" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Cadre</strong><asp:TextBox ID="Text47" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                        
                    </tr>
                    
                    <tr>
                        <td><strong>Cadre Subclass</strong><asp:TextBox ID="Text48" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Unit</strong><asp:TextBox ID="Text44" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Designation</strong><asp:TextBox ID="Text49" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Reports To</strong><asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Gender</strong><asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Minimum Education</strong><asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td><strong>Minimum Experience</strong><asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Minimum Age</strong><asp:TextBox ID="TextBox14" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td><strong>Maximum Age</strong><asp:TextBox ID="TextBox16" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                        <td colspan="3" ><br /><asp:Button ID="Button1" runat="server" Text="Requisition Report" CssClass="btn btn-success" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
                    </asp:Panel>
                  </div>

           
        </div>
      </div>
        </div>
                </section>




              <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css"/>
          <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="sub" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
           <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="rej" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />


          <div id="cvUpload" class="col-md-8 col-md-offset-4 mx-auto" runat="server">
    <div class="custom-panel">
        <div class="panel-heading">
            <h3><i class="fas fa-paperclip"></i> Insert CV</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="nameField">Applicant Name</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-user"></i></span>
                            <asp:TextBox ID="txtName" type="text" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="txtName" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please enter applicant name" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="addressField">CNIC</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-id-card"></i></span>
                            <asp:TextBox ID="cnic" type="text" placeholder="XXXXX-XXXXXXX-X" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="cnic" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please enter cnic" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="sub" ControlToValidate="cnic" ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="Invalid CNIC format" ForeColor="Red" ValidationExpression="^\d{5}-\d{7}-\d{1}$">*</asp:RegularExpressionValidator>
                            <script type="text/javascript">
                                function formatCNIC(input) {
                                    var value = input.value.replace(/[^0-9]/g, '');
                                    if (value.length > 5) {
                                        value = value.substring(0, 5) + '-' + value.substring(5);
                                    }
                                    if (value.length > 13) {
                                        value = value.substring(0, 13) + '-' + value.substring(13);
                                    }
                                    input.value = value;
                                }
</script>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="emailField">Gender</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-venus-mars"></i></span>
                            <asp:DropDownList ID="DropDownGender" CssClass="form-control" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="Select Gender" value="0" Selected="True" />
                                <asp:ListItem Text="Male" value="M" />
                                <asp:ListItem Text="Female" value="F" />
                                <asp:ListItem Text="Other" value="O" />
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidatorGender" ValidationGroup="sub" ControlToValidate="DropDownGender" runat="server" ErrorMessage="Please select gender" Display="Dynamic" ForeColor="Red" ClientValidationFunction="validateGender">*</asp:CustomValidator>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="emailField">Action</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-tasks"></i></span>
                            <asp:DropDownList ID="DropdownAction" CssClass="form-control" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="Select Action" value="0" Selected="True" />
                                <asp:ListItem Text="Interview in Process" value="I" />
                                <%--<asp:ListItem Text="Interview Re-Schedule" value="R" />--%>
                                <asp:ListItem Text="Shortlisted" value="S" />
                                <asp:ListItem Text="Hold" value="H" />
                                <asp:ListItem Text="Rejected" value="X" />
                                <asp:ListItem Text="Job Offer Placed" value="J" />
                                
                                <%--<asp:ListItem Text="Completed" value="C" />--%>
                                
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidatorAction" ValidationGroup="sub" ControlToValidate="DropdownAction" runat="server" ErrorMessage="Please select an action" Display="Dynamic" ForeColor="Red" ClientValidationFunction="validateAction">*</asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>

             <div class="row">
                

                 <div class="col-md-3">
                    <div class="form-group">
                        <label for="cvField">Last Salary</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-money-bill-wave"></i></span>
                            <asp:TextBox ID="lastSalary" type="number" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="lastSalary" ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please enter last salary" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            

                <div class="col-md-6">
                    <div class="form-group">
                        <label for="cvField">Upload CV</label>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-file-upload"></i></span>
                            <input type="file" id="fileCV" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>

                 <div class="col-md-3">
                    <div class="form-group">
                        <label for="trackingStatus">Tracking Status</label>
                        <div class="input-group">
                            
                            <div class="checkbox-inline">
                                <asp:RadioButton GroupName="chk" ID="chkOpen" runat="server" Text="Open" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" Checked="true"/>
                            </div>
                            <div class="checkbox-inline">
                                <asp:RadioButton GroupName="chk" ID="chkClose" runat="server" Text="Close" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>


           
<div class="row">
    <div class="col-md-12">
        <div class="input-group mb-3">
            <div class="input-group-prepend">
                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="sub" Text="Submit" CssClass="btn btn-success" Font-Bold="true" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReject" runat="server" ValidationGroup="rej" Text="Reject" CssClass="btn btn-danger" Font-Bold="true" OnClick="btnReject_Click" />
            </div>
            <asp:TextBox type="text" ID="HR_REMARKS" class="form-control" placeholder="HR Remarks" aria-label="HR Remarks" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="rej" ControlToValidate="HR_REMARKS" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Remarks field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </div>
    </div>
</div>
   

            <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />

             





        </div>
    </div>
</div>



<br />

          <asp:GridView ID="appgrid" runat="server" DataKeyNames="APPLICANT_ID"  CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
                      <Columns>
                                  <asp:TemplateField HeaderText="ViewCv">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="ViewCv" OnClick="lbl_View_Click1" CommandArgument='<%# Bind("APPLICANT_ID") %>' >View CV</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                 
                                  <asp:BoundField DataField="APPLICANT_NAME" HeaderText="Applicant Name"/>
                                  <asp:BoundField DataField="CNIC" HeaderText="CNIC" />        
                                  <asp:BoundField DataField="GENDER" HeaderText="Gender" />


                                  <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:DropDownList ID="ddlAction" CssClass="form-control" OnSelectedIndexChanged ="SelectedDDLChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem Text="Interview in Process" Value="I"></asp:ListItem>
                    <%--<asp:ListItem Text="Initial Shortlisting" Value="S"></asp:ListItem>--%>
                    <%--<asp:ListItem Text="Interview Re-Schedule" Value="R"></asp:ListItem>--%>
                    <asp:ListItem Text="Shortlisted" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Job Offer Placed" Value="J"></asp:ListItem>
                    <asp:ListItem Text="Hold" Value="H"></asp:ListItem>
                    <%--<asp:ListItem Text="Completed" Value="C"></asp:ListItem>--%>
                    <asp:ListItem Text="Rejected" Value="X"></asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>


                                  <asp:BoundField DataField="ACTION_DATE" HeaderText="Action Date" />
                                  <asp:BoundField DataField="SELECTED_BY_HOD" HeaderText="Finalized by HOD" />

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
          <div class="card-footer">

            <div class="row">
                <div class="col-8">
                    
                </div>
                <div class="col-4 text-right">
                    <asp:Label ID="Label1" runat="server" />
                </div>
            </div>

           
        </div>

         <div class="card" style="max-width: 100%;">
    <div class="card-body">
        <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="val" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
        <h5 class="card-title">Generate Report</h5>
        <div class="row mb-3">
            <div class="col-12 col-md-4 mb-2 mb-md-0">
                <div class="input-group">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm" placeholder="From Date"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy" />
                    <asp:RequiredFieldValidator ValidationGroup="val" ControlToValidate="txtFromDate" ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="From Date field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-12 col-md-4 mb-2 mb-md-0">
                <div class="input-group">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm" placeholder="To Date"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy" />
                    <asp:RequiredFieldValidator ValidationGroup="val" ControlToValidate="txtToDate" ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="To Date field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="d-flex">
                    <asp:Button ID="Button2" ValidationGroup="val" runat="server" Text="TAT Report" CssClass="btn btn-sm btn-info w-50" OnClick="btn_GenerateReport_Click" />
                    <asp:Button ID="Button3" runat="server" Text="Tracking Report" CssClass="btn btn-sm btn-secondary w-50 ms-2 ml-1" OnClick="btn_TrackingReport_Click"  />
                </div>
            </div>
        </div>
    </div>
</div>









 
            <script>

                function showSuccessToast() {
                    console.log('showSuccessToast called');
                    toastr.success('New applicant sucessfully inserted! ', 'Success');
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
                    toastr.info('Requisition Rejected !', 'Info');

                }

                function showNoCvToast() {
                    console.log('showErrorToast called');
                    toastr.info('No CV available !', 'Info');

                }

                function showCvExistToast() {
                    console.log('showErrorToast called');
                    toastr.info('Warning: CV with this name already exixts!', 'Info');

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

                function FailCVDetailsToast() {
                    console.log('showErrorToast called');
                    toastr.warning('Failed to insert CV details!', 'Warning');

                }

</script>

          <script type="text/javascript">
              function validateGender(source, args) {
                  var genderDropDown = document.getElementById('<%=DropDownGender.ClientID%>');
        if (genderDropDown.selectedIndex === 0) {
            args.IsValid = false;
        } else {
            args.IsValid = true;
        }
    }
</script>

          <script type="text/javascript">
              function validateAction(source, args) {
                  var actionDropDown = document.getElementById('<%=DropdownAction.ClientID%>');
        if (actionDropDown.selectedIndex === 0) {
            args.IsValid = false;
        } else {
            args.IsValid = true;
        }
    }
</script>

       


          <asp:TextBox ID="jobReqId" runat="server" Visible="false"></asp:TextBox>


          </div>
    
</asp:Content>
