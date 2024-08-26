<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA2.aspx.cs" Inherits="TalentAcquisitionPortal.TA2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />


    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>
    
    <style>
         .invisible-border {
        border: none;
    }

           tr:nth-child(even) {
        background-color: #f2f2f2;
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


            <div class="row">
             <div class="col-md-12">




                 <div class="form-title">
                        <h1><strong>REQUISITION APPROVAL</strong></h1>
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
      <!-- Pending Requests Card -->
      <div class="card">
         <div class="card-header" style="background-color: #363940; color: white;">
                             <h2 class="card-title"><i class="bi bi-list-task"></i> Pending Request</h2>

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
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="75px" CommandName="View" CommandArgument='<%# Bind("JOB_REQ_ID") %>' OnClientClick="showLoadingSpinner()" OnClick="lbl_View_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="JOB_REQ_ID" HeaderText="Request ID"/>
                                  <asp:BoundField DataField="BUDGET_REF_NO" HeaderText="Budget Code" />
                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation Name" />
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
                 </div>
             </div>

        


   
        <asp:TextBox ID="hidden" Visible="false" runat="server"></asp:TextBox>    
            
            
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
            <div class="card">
              
       
                  <!-- Table 1 -->

                      <div class="row">
                          <div class="col-3">

                          </div>
                          <div class="col-8">
                             
                              <div class="table-responsive">
                               <table class="table-responsive">
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
                        <asp:TextBox id="Text32" Size="5px" type="text"  runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                </td>
                                <td class="text-center">
                        <asp:TextBox id="Text33" Size="5px" type="text" runat="server" ReadOnly="true" CssClass="invisible-border text-center"></asp:TextBox>

                                </td>
                                <td class="text-center">
                        <asp:TextBox id="Text34" Size="5px" type="text" runat="server" CssClass="invisible-border text-center" ReadOnly="true"></asp:TextBox>

                                </td>
                                 <td class="text-center">
                                       <asp:Button  class="btn btn-info badge-pill " Font-Bold="true" ID="Button1" runat="server" Text="Hold" OnClick="ButtonHold_Click" Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'/>
                                       <asp:Button  class="btn btn-danger badge-pill" Font-Bold="true" ID="Button2" runat="server" Text="Reject" OnClick="ButtonReject_Click" Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'/>
                                       <asp:Button  class="btn btn-success badge-pill" Font-Bold="true" ID="Button7" runat="server" Text="Approve" OnClick="Button4_Click" Visible="true" OnClientClick="showLoadingSpinner()" CommandArgument='<%# Bind("JOB_REQ_ID") %>'/>
                                   </td>
                                
                            </tr>
                                  
                              </table>
                                  </div>
                          </div>
                          <div class="col-1">
                            
                          </div>
                      </div>
                       

              <script type="text/javascript">
                  function showLoadingSpinner() {
                      document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
                  }
</script>

 <asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>



            <div class="table-responsive">
    <table class="table">
        <tr>
                    <td>
                        <strong>Request Id</strong>
                    </td>
                    <td>
                        <asp:TextBox id="Text29" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Vacancies</strong>
                    </td>
                    <td>
                        <asp:TextBox id="Text30" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Budget CD</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox11" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Request Date</strong>
                    </td>
                    <td>
                        <asp:TextBox id="Text31" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <strong>Reg CD</strong></td>
                    <td>
                        <asp:TextBox id="Text21" size="18px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox></td>

                    <td>
                        <strong>Section</strong></td>
                    <td>
                        <asp:TextBox id="Text46" size="18px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Division</strong></td>
                    <td>
                        <asp:TextBox id="Text43" size="18px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <strong>Department</strong></td>
                    <td>
                        <asp:TextBox id="Text45" size="18px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    
                    <td>
                        <strong>Cadre</strong></td>
                    <td>
                        <asp:TextBox id="Text47" size="18px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Cadre Subclass</strong></td>
                    <td>
                        <asp:TextBox id="Text48" size="18px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Unit</strong></td>
                    <td>
                        <asp:TextBox id="Text44" size="18px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <strong>Designation</strong></td>
                    <td>
                        <asp:TextBox id="Text49" size="18px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
              
            </table>
            </div>
                  
                  


                  
                  
                  
                      
                <div class="card-header" style="background-color:#363940; color:white;">
                    <h2 class="card-title">Job Profile & Detail</h2>
                    </div>

            <div class="table-responsive">
    <table class="table">
               <tr>
                    <td>
                        <strong>Reports To</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox1" size="20px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                   <td>
                        <strong>Gender</strong>
                       </td>
                    <td>
                        <asp:TextBox id="TextBox4" size="20px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>

                    </td>
                   <td>
                        <strong>Minimum Education</strong>
                   </td>
                    <td>
                        <asp:TextBox id="TextBox6" size="20px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
 
                </tr>
                
               
                <tr>
                    <td>
                        <strong>Minimum Experience</strong>

                    </td>
                    <td>
                        <asp:TextBox id="TextBox8" type="text" size="20px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>

                    </td>
                    <td>
                        <strong>Minimum Age</strong>

                    </td>
                    <td>
                        <asp:TextBox id="TextBox14" type="text" size="20px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>

                    </td>
                    <td>
                        <strong>Maximum Age</strong>

                    </td>
                    <td>
                        <asp:TextBox id="TextBox16" type="text" size="20px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                   </tr> 
                <tr>
                    <td>
                        <strong>Remarks</strong>
                    </td>
                   <td>
                        <asp:TextBox id="TextBox12" type="text" size="20px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>

                    </td>
                    <td>
                        <strong>Job Description</strong>
                    </td>
                   <td colspan="4">
                        <asp:TextBox id="TextBox10" type="text" size="100px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                        </td>
                </tr>                
                    
                
            </table>
                </div>
                  

         




                <div class="card-header" style="background-color:#363940; color:white;">
                    <h2 class="card-title">Pay &amp; Benefits</h2>
                    </div>
                 <div class="table-responsive">
                <table class="table">
                     <tr>
                         <td>
                             <strong>Approved ROP</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox3" size="5px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Production Allow</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox5" size="5px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                          <td>
                              <strong>Overtime
                         </strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox7" size="5px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                         <td class="auto-style3">
                             <strong>Benefits
                         </strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox9" size="5px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox></td>
                      </tr>
                     <tr>
                         <td>
                             <strong>Total Gross Pay</strong></td>
                         <td>
                        <asp:TextBox id="TextBox17" size="5px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Attendance Allow</strong></td>
                         <td>
                        <asp:TextBox id="TextBox18" size="5px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                          <td>
                              <strong>Festival OT</strong></td>
                         <td colspan="3">
                        <asp:TextBox id="TextBox19" size="5px" type="text" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                         </td>
                      </tr>
                          </table>
                      </div>









                    <div class="card-header" style="background-color:#363940; color:white;">
                        <h2 class="card-title">Hiring Detail</h2>
                    </div>

                


                  

                <div class="table-responsive">
                         <table class="table">
                       <tr>
                           <td>
                             <strong>Hiring Type</strong>
                         </td>
                         <td>
                        <asp:TextBox id="TextBox20" type="text" size="10px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>

                         </td>
                           <td><strong>Notes by HR Department</strong></td>
                           <td>
                        <asp:TextBox id="TextBox2" size="10px" type="text" runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox></td>
                             <td><asp:Button  class="btn btn-success" ID="Button3" runat="server" Text="Show Report" OnClick="Button3_Click"/>
                           </td>

                       </tr>

                        </table>
                    </div>

              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  
                                  <asp:BoundField DataField="REP_ID" HeaderText="Rep Id"/>
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
        </div>
        
   
    
     </section>
          <asp:TextBox id="TextBox13" Size="5px" type="text" visible="false" runat="server" ReadOnly="true"></asp:TextBox>
    </div>



    <!-- JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>



     <script>

         function showSuccessToast() {
             console.log('showSuccessToast called');
             toastr.success('Request sucessfully updated!', 'Success');
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
             toastr.info('Request on hold !', 'Info');

         }

         function showWarningToast() {
             console.log('showErrorToast called');
             toastr.warning('Request has been successfully rejected !', 'Rejected');

         }

         function showRemarksWarningToast() {
             console.log('showErrorToast called');
             toastr.warning('Warning: Please enter remarks!', 'Warning');

         }

</script>
</asp:Content>
