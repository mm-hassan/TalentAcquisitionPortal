<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TA5.aspx.cs" Inherits="TalentAcquisitionPortal.TA5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!DOCTYPE html>


    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>
    <style>

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
    height: 250px;
    border: 1px solid #ccc;
    border-radius: 8px;
    overflow-y: auto;
    padding: 10px;
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
        th{
            color:white;
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

        
        textbox:nth-child(even){
            background-color: #f2f2f2;
        }

        tr:nth-child(even) {
        background-color: #f2f2f2;
    }

        .text-right mt-3{
            padding-bottom: 90px;
            margin-top: 90px;
        }

        .txt{
            padding-left:10% ;
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
         

            <div class="form-title">
                <h1><strong>APPLICANT EVALUATION</strong></h1>
            </div>
        <div class="card-tools" style="padding-bottom:10px;">
    <div class="row">
        <div class="col-md-9"></div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox type="text" id="txtSearch" class="form-control" placeholder="Enter tracking id" runat="server"></asp:TextBox>
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

                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="gv_PendingRequests_RowDataBound" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" type="button" Class="btn btn-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("APPLICANT_ID") %>' OnClick="lbl_View_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="APPLICANT_NAME" HeaderText="Applicant"/>
                                  <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation"/>
                                  <asp:BoundField DataField="TRACKING_ID" HeaderText="Tracking Id"/>
                                  <asp:BoundField DataField="Budget_Ref_No" HeaderText="Budget Ref No"/>
                                  <asp:BoundField DataField="CNIC" HeaderText="CNIC" />
                                  <asp:TemplateField HeaderText="Gender">
                                      <ItemTemplate>
                                       <asp:Label runat="server" ID="lbl"></asp:Label>
                                          </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="APPLICANT_ID" HeaderText="Applicant Id" />      
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
                       <asp:TextBox id="TextBox21" type="text" Size="18px" runat="server" ReadOnly="true" Visible="false" ></asp:TextBox>

                <tr>
                    <td><strong>
                         Evaluation Id</strong>
                    </td>
                    <td>
                       <asp:TextBox id="TextBox1" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                        
                    </td>
                    <td>
                        <strong>Tracking Id</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox2" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Job Applied For</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox3" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Department</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox4" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td>
                        <strong>Applicant Name</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox5" type="text" Size="18px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>

                    <td> <strong>
                        Interview Date</strong></td>
                    <td>
                       <asp:TextBox id="TextBox6" type="text" Size="18px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <strong>Father/ Husband Name</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox7" type="text" Size="18px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Candidate Reference</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox8" type="text" Size="18px" runat="server" CssClass="invisible-border" BackColor="#f2f2f2" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Date Of Birth</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox9" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>

                    <td> <strong>
                        CNIC</strong></td>
                    <td>
                       <asp:TextBox id="TextBox10" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td>
                        <strong>Qualification</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox11" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Last Salary</strong>
                    </td>
                    <td>
                        <asp:TextBox id="TextBox12" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>

                  <asp:ValidationSummary ValidationGroup="sub" ID="ValidationSummary1" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />

                  <div class="card-header" style="background-color:#363940; color:white;">
                    <h2 class="card-title">Observations</h2>
                    </div>

        
                  
              
              <table style="outline:none;">
                      <tr>
                      <td colspan="2">
              
               <div class="card-body p-1"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                  
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                   
                                  <asp:BoundField DataField="detail_id" ItemStyle-Width="10%" HeaderText="Id" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"/>
                                  <asp:BoundField DataField="detail_name" ItemStyle-Width="70%" HeaderText="Parameters" HeaderStyle-CssClass="text-center" />
                                 <asp:TemplateField HeaderText="Score" ItemStyle-Width="16px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                       <ItemTemplate>
                                           <asp:TextBox ID="myTextBox" type="number" min="1" max="4" MaxLength="1" Size="8px" CssClass="text-center" runat="server"></asp:TextBox>
                                           <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="myTextBox" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Score field cannot be empty"  ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                           <asp:RangeValidator ValidationGroup="sub" ID="RangeValidator1" runat="server" ControlToValidate="myTextBox" Display="Dynamic" ErrorMessage="Score should be between 1 to 5" ForeColor="Red" MaximumValue="5" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
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
          
              </div>

                </div>
                 </td>
                      <td colspan="2">
                          <div class="evaluation-card">
                            <h2 class="card-heading">Evaluation Score</h2>
                            <div class="card-body">
                                <ul class="legend-list">
                                    <li><strong>1:</strong> Below Average</li>
                                    <li><strong>2:</strong> Average</li>
                                    <li><strong>3:</strong> Good</li>
                                    <li><strong>4:</strong> Very Good</li>
                                    <!-- Add more as needed -->
                                </ul>
                            </div>
                        </div>

                      </td>
                  </tr>
                  
                      <tr>
                          <td>
                              <strong>Remarks Interviewer 1 (HR)</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox13" type="text" Size="98px" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="TextBox13" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Remarks Interviewer 1 field cannot be empty"  ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                          </td>
                          <td>
                              <strong>Salary Decided</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox16" type="text" Size="8px" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="TextBox16" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Salaryfield cannot be empty"  ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                       <tr>
                           <td>
                              <strong>Remarks Interviewer 2</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox14" type="text" Size="98px" runat="server"></asp:TextBox>
                          </td>
                          <td>
                              <strong>Date of Joining</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox17" type="text" Size="8px" runat="server"></asp:TextBox>
                          </td>
                       </tr>
                       <tr>
                           <td>
                              <strong>Remarks Interviewer 3</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox15" type="text" Size="98px" runat="server"></asp:TextBox>
                          </td>
                           <td>
                              <strong>Shift Details</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox18" type="text" Size="8px" runat="server"></asp:TextBox>
                          </td>
                       </tr>
                       <tr>
                           <td>
                              <strong>Other Benefits/ Comments</strong>
                          </td>
                          <td>
                              <asp:TextBox id="TextBox20" type="text" Size="98px" runat="server"></asp:TextBox>
                          </td>
                           <td>
                              <strong>Overall Recomendation</strong>
                          </td>
                          <td>
                              <div class="option-container">
                              <div class="option">
                              <asp:RadioButton ID="CheckBox1" runat="server" GroupName="satisfactionGroup" Text="No" />
                                </div>
                              <div class="option">
                              <asp:RadioButton ID="CheckBox2" runat="server" GroupName="satisfactionGroup" Text="Yes" />
                                  </div>
                                  </div>
                          </td>
                       </tr>
                  </table>
                   <asp:Button ID="btnSubmit0" runat="server" Text="View CV" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" font-size="16px"  OnClick="btnViewCv_Click"/>
                  

                   <asp:Button ID="btnSubmit1" runat="server" Text="Evaluation Report" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" font-size="16px"  OnClick="btnreport_Click"/>
                  

                   <asp:Button ID="btnSubmit" OnClientClick="showLoadingSpinner()" ValidationGroup="sub" runat="server" Text="Submit" CssClass="btn btn-success" padding="10px 20px" boredr="none" border-radius="4px" font-size="16px"  OnClick="btnSubmit_Click"/>
                  
                   </div>
              
              </div>
                            </div>
                </div>
      
        



      
         </section>
        <script type="text/javascript">
            function showLoadingSpinner() {
                document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
               }
        </script>

  <asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>
        </div>
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
