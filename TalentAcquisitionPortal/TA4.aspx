<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA4.aspx.cs" Inherits="TalentAcquisitionPortal.TA4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!DOCTYPE html>


    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>

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
        
        table 
        {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
            border-radius: 5px;
            overflow: hidden;
            box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
        }
        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }
        th {
            color:white;
            background-color: #343a40;
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
         <div class="card-tools" style="padding-bottom:10px;">


            <div class="form-title">
                <h1><strong>APPLICANT DETAILS</strong></h1>
            </div>
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
          <h2 class="card-title">Pending Request</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-1"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">

                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
                      <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("JOB_REQ_ID") %>' OnClick="lbl_View_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="TRACKING_ID" HeaderText="TRACKING ID"/>
                                  <asp:BoundField DataField="JOB_REQ_ID" HeaderText="Request ID"/>
                                  <asp:BoundField DataField="BUDGET_CD" HeaderText="Budget Code" />
                                  <asp:BoundField DataField="DESIGNATION" HeaderText="Designation Name" />
                                  <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department Name" />
                                  
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
                        <td colspan="3" ><br /><asp:Button ID="Button1" runat="server" Text="Requisition Report" CssClass="btn btn-success" OnClick="Button1_Click" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Panel>



                  <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="GridView2_RowDataBound" CellPadding="4" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                   
                                  <asp:TemplateField HeaderText="HOD Selection">
                                      <ItemTemplate>
                                          <asp:CheckBox ID="chkBox"  runat="server" OnCheckedChanged="chkBox_CheckedChanged" AutoPostBack="true"/>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                  <asp:BoundField DataField="APPLICANT_ID" HeaderText="Applicant ID" />
                                  <asp:BoundField DataField="APPLICANT_NAME" HeaderText="Applicant Name"/>
                                  <%--<asp:BoundField DataField="FATHER_NAME" HeaderText="Father Name"/>--%>
                                  <asp:BoundField DataField="DOB" HeaderText="DOB"/>
                                  <asp:BoundField DataField="CNIC" HeaderText="CNIC"/>
                                  <asp:BoundField DataField="QUALIFICATION" HeaderText="Qualification"/>
                                  <asp:BoundField DataField="LAST_SALARY" HeaderText="Last Salary"/>
                                  <asp:TemplateField HeaderText="Gender">
                                      <ItemTemplate>
                                          <asp:Label runat="server" ID="lbl"></asp:Label>
                                          
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Action" HeaderText="Action"/>
                                   <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlAction" runat="server" OnSelectedIndexChanged ="SelectedDDLChanged" Enabled="true"  AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                  <asp:TemplateField HeaderText="CV">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" Font-Size="Small" Width="60px" type="button" CssClass="btn btn-dark" runat="server" CommandName="" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_Click1">View CV</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  
                                   <asp:TemplateField HeaderText="Report">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_ER" type="button" Width="100px" Font-Size="Smaller" CssClass="btn btn-dark" runat="server" CommandName="" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_Click2">Evaluation Report</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>

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
                  
                                   <asp:TextBox id="TextBox7" type="text" size="20px" runat="server" style="border: none;" Visible="false" ReadOnly="true"></asp:TextBox>

                 </div>
                </div>
            <div class="card-footer">

            <div class="row">
                <div class="col-8">
                    
                </div>
                <div class="col-4 text-right">
                    <asp:Label ID="Label1" runat="server" />
                </div>
            </div>
        </div>
        
   </div>
    
     </section>

        <asp:TextBox ID="hidden" Visible="false" runat="server"></asp:TextBox>

</div>


     <script>

         function showSuccessToast() {
             console.log('showSuccessToast called');
             toastr.success('Request sucessfully updated!', 'Success');
         }

         function showSelectionToast() {
             console.log('showSelectionToast called');
             toastr.success('Selected by HOD!', 'Success');
         }

         function showNotSelectionToast() {
             console.log('showNotSelectionToast called');
             toastr.success('Not Selected by HOD!', 'Success');
         }

         function showErrorToast() {
             console.log('showErrorToast called');
             toastr.error('Request not sucessfully updated!', 'Info');
         }

         function showDatabaseErrorToast() {
             console.log('showErrorToast called');
             toastr.error('Database backend error!', 'Error');
         }

         function showInfoToast() {
             console.log('showErrorToast called');
             toastr.info('No CV Attached !', 'Info');

         }

         function showWarningToast() {
             console.log('showErrorToast called');
             toastr.warning('Warning: Please select a checkbox!', 'Warning');

         }

         function showRemarksWarningToast() {
             console.log('showErrorToast called');
             toastr.warning('Warning: Please enter remarks!', 'Warning');

         }

</script>
</asp:Content>
