<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA1.aspx.cs" Inherits="TalentAcquisitionPortal.TA1" %>
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


<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style>

        .dropdown-item {
        white-space: pre; /* Preserve white space */
        font-family: monospace; /* Use a monospaced font to align text */
    }

         .invisible-border {
        border: none;
    }

           tr:nth-child(even) {
        background-color: #f2f2f2;
    }
         
 @media (max-width: 767.98px) {
            .responsive-table {
                font-size: 0.875rem; /* Equivalent to table-sm size adjustments */
                padding: .3rem;
            }
        }

    .performance-status label {
        font-size: 18px;
        color: #333;
        margin-right: 10px;
    }

    .option-container {
        display: flex;
        justify-content: space-around;
        margin-top: 10px;
    }

    .option {
        display:flexbox;
        cursor: pointer;
        transition: transform 0.2s ease-in-out;
    }

    .option:hover {
        transform: scale(1.1);
    }



    body {
           font-family: 'Roboto', sans-serif;
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
        th{
            color:white;
            background-color: #343a40;
        }
        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
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

input[type="radio"]:checked + label {
  background-color: #4CAF50;
  color: #fff;
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


        .toast-info {
    background-color: #3498db;
}
        .toast-warning {
    background-color: #e67e22;
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

input[type="radio"]:checked + label {
  background-color: #4CAF50;
  color: #fff;
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

    .center {
  margin-left: auto;
  margin-right: auto;
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
        <div class="col-md-9">
            <asp:DropDownList runat="server" CssClass="form-control" ID="Select_Hiring"  OnSelectedIndexChanged="Hiring_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0" Selected="True" />
                <asp:ListItem Text="New" Value="N" />
                <asp:ListItem Text="Replacement" Value="R" />
            </asp:DropDownList>
        </div>
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
        <!-- Card Body -->

        <div class="card-body p-1"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
                      <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" CommandName="View" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("BUDGET_REF_NO") %>' OnClick="lbl_View_Click" >View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="TOTAT_STR" HeaderText="Total_Str"/>
                                  <asp:BoundField DataField="UT_BUD" HeaderText="Ut_Bud"/>
                                  <asp:BoundField DataField="budget_ref_no" HeaderText="Budget Code" />        
                                  <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                                  <asp:BoundField DataField="VACANT_BUD" HeaderText="VACANT_BUD" />
                                  <asp:BoundField DataField="REG" HeaderText="REG" />
                                  <asp:BoundField DataField="BUD_DIVISION_NAME" HeaderText="BUD_DIVISION_NAME" />
                                  
                                  <asp:BoundField DataField="BUD_UNIT_NAME" HeaderText="BUD_UNIT_NAME" />
                          <asp:BoundField DataField="BUD_DEPARTMENT_NAME" HeaderText="BUD_DEPARTMENT_NAME" />
                                  <asp:BoundField DataField="BUD_SECTION_NAME" HeaderText="BUD_SECTION_NAME" />
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




 <section id="div_EmployeeDetails" runat="server">
    <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
            <h2 class="card-title">Manpower Hierarchial Detail</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" style="color:white;" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
      
        <div class="card-body p-1"> 
             
                  <!-- Table 1 -->        
            <div class="table-responsive">
                               <table class="center table responsive-table" style="width:50%" >
                                  <thead class="thead-dark" >
                            <tr>
                                <td colspan="4" class="text-center" ><strong>Budget Summary (HeadCount)</strong></td>
                            </tr>
                            <tr>
                                <td class="text-center" ><strong>Approved</strong></td>
                                <td class="text-center"><strong>Current</strong></td>
                                <td class="text-center"><strong>Balance</strong></td>
                                <td class="text-center"><strong>Actions</strong></td>
                            </tr>
                            </thead>
                            <tr>
                                <td class="text-center">
                        <asp:TextBox id="approved" class="form-control" type="text"  runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                </td>
                                <td class="text-center">
                        <asp:TextBox id="current" class="form-control" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                </td>
                                <td class="text-center">
                        <asp:TextBox id="balance" class="form-control" type="text" runat="server" CssClass="invisible-border text-center" ReadOnly="true"></asp:TextBox>

                                </td>
                                 <td class="text-center">
                                       <asp:Button  class="btn btn-info badge-pill " Font-Bold="true" ID="Button1" runat="server" Text="View Report"  Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'/>
                                     </td>
                                
                            </tr>
                                  
                              </table>
                                </div>
                      
                       

              


                
              <div class="table-responsive card-body p-0" style="width: 100%; overflow: auto;">
                  <div class="row">
    <table class="table table-sm">         
        
                
                <tr>
                    <td><strong>
                         Region CD: </strong>
                        <asp:TextBox id="region" type="text" class="form-control"  runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                    </td>
                    
                    <td>
                        <strong>Section: </strong>
                        <asp:TextBox id="section" type="text" class="form-control" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                   <td>
                        <strong>Division:</strong>
                        <asp:TextBox id="division" type="text" class="form-control" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                   
                    <td>
                        <strong>Cadre: </strong>
                         <asp:TextBox id="cadre" type="text" class="form-control" runat="server" CssClass="invisible-border" ReadOnly="true" Width="220px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    
                    
                    <td>
                        <strong>Unit: </strong>
                        <asp:TextBox id="unit" type="text" class="form-control" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    <td>
                        <strong>Cadre Subclass:</strong>
                        <asp:TextBox id="cadresubclass" type="text" class="form-control" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    <td>
                        <strong>Department: </strong>
                        <asp:TextBox id="department" type="text" class="form-control" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td> <strong>Designation: </strong>
                    <asp:TextBox id="designation" type="text" class="form-control" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
   
                
                </table>
        </div>

                  <div>

























            
                  
                  


                  
                  
                  
                      
                <div class="card-header" style="background-color:#363940; color:white;">
                    <h2 class="card-title">Job Profile & Detail</h2>
                    </div>

            <div class="table-responsive">
    <table class="table responsive-table">
               <tr>
                    <td>
                        <strong>Reports To</strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownReports" CssClass="form-control dropdown-item" runat="server" AutoPostBack="false">
                                
                                
                                
                            </asp:DropDownList>
                    </td>
                   <td>
                        <strong>Gender</strong>
                       </td>
                    <td>
                        <asp:DropDownList ID="DropDownGender" CssClass="form-control" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="Select Gender" value="0" Selected="True" />
                                
                                <asp:ListItem Text="Male" value="M" />
                                <asp:ListItem Text="Female" value="F" />
                                <asp:ListItem Text="Other" value="O" />
                            </asp:DropDownList>

                    </td>
                   <td>
                        <strong>Minimum Education</strong>
                   </td>
                    <td>
                         <asp:DropDownList ID="DropDownEdu" CssClass="form-control" runat="server" AutoPostBack="false">
                               
                                
                            </asp:DropDownList>
                    </td>
 
                </tr>
                
               
                <tr>
                    <td>
                        <strong>Minimum Experience</strong>

                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownMinExp" CssClass="form-control" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select Years" value="0" Selected="True" />
                            <asp:ListItem Text="01" value="01" />
                            <asp:ListItem Text="02" value="02" />
                            <asp:ListItem Text="03" value="03" />
                            <asp:ListItem Text="04" value="04" />
                            <asp:ListItem Text="05" value="05" />
                            <asp:ListItem Text="06" value="06" />
                            <asp:ListItem Text="07" value="07" />
                            <asp:ListItem Text="08" value="08" />
                            <asp:ListItem Text="09" value="09" />
                            <asp:ListItem Text="10" value="10" />
                            <asp:ListItem Text="11" value="11" />
                            <asp:ListItem Text="12" value="12" />
                            <asp:ListItem Text="13" value="13" />
                            <asp:ListItem Text="14" value="14" />
                            <asp:ListItem Text="15" value="15" />
                            <asp:ListItem Text="16" value="16" />
                            <asp:ListItem Text="17" value="17" />
                            <asp:ListItem Text="18" value="18" />
                            <asp:ListItem Text="19" value="19" />
                            <asp:ListItem Text="20" value="20" />
                            <asp:ListItem Text="21" value="21" />
                            <asp:ListItem Text="22" value="22" />
                            <asp:ListItem Text="23" value="23" />
                            <asp:ListItem Text="24" value="24" />
                            <asp:ListItem Text="25" value="25" />
                                
                                
                            </asp:DropDownList>
                    </td>
                    <td>
                        <strong>Minimum Age</strong>

                    </td>
                    <td>
                        <asp:TextBox id="TextBox14"  type="number" class="form-control" runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <strong>Maximum Age</strong>

                    </td>
                    <td>
                        <asp:TextBox id="TextBox16"  type="number" class="form-control" runat="server"></asp:TextBox>
                    </td>
                   </tr> 
                <tr>
                    <td>
                        <strong>Remarks</strong>
                    </td>
                   <td>
                        <asp:TextBox id="TextBox12" class="form-control" type="text" runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <strong>Job Description</strong>
                    </td>
                   <td colspan="3">
                        <asp:TextBox id="TextBox10" class="form-control" type="text" runat="server"></asp:TextBox>
                        </td>
                </tr>                
                    
                
            </table>
                </div>
                  

         




                <div class="card-header" style="background-color:#363940; color:white;">
                    <h2 class="card-title">Pay &amp; Benefits</h2>
                    </div>
                 <div class="table-responsive">
                <table class="table responsive-table">
                     <tr>
                         <td>
                             <strong>Approved ROP</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox3" type="text" runat="server"  class="form-control" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Production Allow</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox5" class="form-control" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                          <td>
                              <strong>Overtime
                         </strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox7"  class="form-control" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                         <td>
                             <strong>Benefits</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox9"  class="form-control" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                      </tr>
                     <tr>
                         <td>
                             <strong>Total Gross Pay</strong></td>
                         <td>
                        <asp:TextBox id="TextBox17"  class="form-control" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Attendance Allow</strong></td>
                         <td>
                        <asp:TextBox id="TextBox18"  class="form-control" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Festival OT</strong></td>
                         <td colspan="3">
                        <asp:TextBox id="TextBox19"  class="form-control" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                      </tr>
                          </table>
                      </div>









                    <div class="card-header" style="background-color:#363940; color:white;">
                        <h2 class="card-title">Hiring Detail</h2>
                    </div>

                


                  
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="table responsive-table">
                        <tr>
                            <td><strong>Hiring Type</strong></td>
                            <td>
                                <asp:DropDownList ID="hiring_ddl" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Select" Value="0" Selected="True" />
                                    <asp:ListItem Text="New" Value="N" />
                                    <asp:ListItem Text="Replacement" Value="R" />
                                </asp:DropDownList>
                            </td>
                            <td><strong>Notes by HR Department</strong></td>
                            <td>
                                <asp:TextBox ID="TextBox2" Size="10px" Type="text" runat="server" CssClass="invisible-border" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr id="replacementRow" runat="server" visible="false">
                            <td><strong>Ex Employee Code</strong></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownListExEmployee" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlExEmployee_IndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="0" Selected="True" />
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownListExEmployee" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" CssClass="btn btn-success" Font-Bold="true" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="hiring_ddl" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>


                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  
<%--                                  <asp:BoundField DataField="REP_ID" HeaderText="Rep Id"/>--%>
                                  <asp:BoundField DataField="EMP_CD" HeaderText="Emp Code" />
                                  <asp:BoundField DataField="EMP_NAME" HeaderText="Emp Name" />
                                  <asp:BoundField DataField="EMP_STATUS" HeaderText="Emp Status" />
                                  <asp:BoundField DataField="BUDGET_REF_NO" HeaderText="Budget Ref NO" />
                                  <asp:BoundField DataField="LAST_DUTY" HeaderText="Last Duty" />
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
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownListExEmployee" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

                
                 </div>
                </div> 
        </div>

        </div>
   
    <script type="text/javascript">
        function showLoadingSpinner() {
            document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
                  }
</script>

 <asp:Panel ID="Panel1" runat="server" CssClass="loading-panel">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>
     </section>



           </div>
</asp:Content>

