using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class TA4 : System.Web.UI.Page
    {
        string EmployeeCode;
        string EmployeeUCode;
        string rights;
        Database db = new Database();
        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                div_Details.Visible = false;
                loadPendingRequests();
            }
        }

        public void getSession()
        {
            try
            {
                if (Session["EmployeeCode"] != null)
                {
                    EmployeeCode = Session["EmployeeCode"].ToString();
                    DataTable dt = new DataTable();
                    string query = "SELECT * from users where EMP_CD = '" + EmployeeCode + "'";
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Session["EmployeeUserCode"] = dr["USR_CD"].ToString();
                        EmployeeUCode = Session["EmployeeUserCode"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("LockScreen.aspx");
                }
            }
            catch (Exception ex)
            {
                //string script = "alert('Database error');";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", script, true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string JOB_REQ_ID = txtSearch.Text.Trim();
                DataTable dt = new DataTable();
                if (hidden.Text == "true")
                {
                    string query = "SELECT T.TRACKING_ID, r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID)=0 then 'In-Progress' Else 'Job Offer Placed to '||COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION='J') Action, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2','007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T,   HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID=T.JOB_REQ_ID and nvl(r.hod_approved,'N') ='Y' AND r.job_req_id='" + JOB_REQ_ID + "' order by Tracking_Status DESC, TRACKING_ID desc";
                    dt = db.GetData(query);

                    if (dt.Rows.Count > 0)
                    {
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();
                        lbl_GridMsg.Text = "Search results for '" + JOB_REQ_ID + "'";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_GridMsg.Text = "No results found for '" + JOB_REQ_ID + "'";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = null;
                        gv_PendingRequests.DataBind();
                    }
                }
                else if (hidden.Text == "false")
                {
                    string query = "SELECT T.TRACKING_ID,r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID)=0 then 'In-Progress' Else 'Job Offer Placed to '||COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION='J') Action, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2','007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T,   HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID=T.JOB_REQ_ID and nvl(r.hod_approved,'N') ='Y' and r.hrm_cadre_cd  not in ('12','13') AND EXISTS ( SELECT 1 FROM HRM_DEPARTMENT_BUDGET B WHERE B.REG_CD=R.REG_CD AND B.HRM_DIVISION_CD=R.HRM_DIVISION_CD AND B.HRM_UNIT_CD=R.HRM_UNIT_CD AND B.HRM_DEPARTMENT_CD=R.HRM_DEPARTMENT_CD AND (B.EMP_HOD='" + EmployeeCode + "' or  B.nominate_emp_code = '" + EmployeeCode + "') UNION ALL SELECT 1 FROM HRM_DEPARTMENT D WHERE D.REG_CD=R.REG_CD AND D.DIVISION_CD=R.HRM_DIVISION_CD AND D.UNIT_CD=R.HRM_UNIT_CD AND D.DEPARTMENT_CD=R.HRM_DEPARTMENT_CD AND D.EMP_HOD13='" + EmployeeCode + "' UNION ALL SELECT 1 FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD WHERE SD.DETAIL_NAME=TO_CHAR(T.EMP_CD) AND SD.SEQ_NO=153 AND T.EMP_CD = '" + EmployeeCode + "'  AND r.job_req_id='" + JOB_REQ_ID + "' AND T.REG_CD=R.REG_CD AND T.HRM_DIVISION_CD=R.HRM_DIVISION_CD AND T.HRM_UNIT_CD=R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD=R.HRM_DEPARTMENT_CD )";

                    dt = db.GetData(query);

                    if (dt.Rows.Count > 0)
                    {
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();
                        lbl_GridMsg.Text = "Search results for '" + JOB_REQ_ID + "'";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_GridMsg.Text = "No results found for '" + JOB_REQ_ID + "'";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = null;
                        gv_PendingRequests.DataBind();
                    }
                }
                else
                {
                    lbl_GridMsg.Text = "No any pending request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();

                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        public void loadPendingRequests()
        {
            try
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Hrm_Setup_Detl N WHERE N.Detail_Name='" + EmployeeCode + "' and (N.Seq_No=121 or N.Seq_No=141)";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    hidden.Text = "true";
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT T.TRACKING_ID, r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID)=0 then 'In-Progress' Else 'Job Offer Placed to '||COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION='J') Action, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2','007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T,   HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID=T.JOB_REQ_ID and nvl(r.hod_approved,'N') ='Y' order by Tracking_Status DESC, TRACKING_ID desc";
                    dtt = db.GetData(queryy);
                    if (dtt.Rows.Count > 0)
                    {
                        gv_PendingRequests.DataSource = dtt;
                        gv_PendingRequests.DataBind();

                        lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_GridMsg.Text = "No any pending request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = dtt;
                        gv_PendingRequests.DataBind();
                    }
                }
                else
                {
                    hidden.Text = "false";
                    DataTable dttt = new DataTable();
                    string queryyy = "SELECT T.TRACKING_ID,r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID)=0 then 'In-Progress' Else 'Job Offer Placed to '||COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION='J') Action, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2','007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T,   HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID=T.JOB_REQ_ID and nvl(r.hod_approved,'N') ='Y' and r.hrm_cadre_cd  not in ('12','13') AND EXISTS ( SELECT 1 FROM HRM_DEPARTMENT_BUDGET B WHERE B.REG_CD=R.REG_CD AND B.HRM_DIVISION_CD=R.HRM_DIVISION_CD AND B.HRM_UNIT_CD=R.HRM_UNIT_CD AND B.HRM_DEPARTMENT_CD=R.HRM_DEPARTMENT_CD AND (B.EMP_HOD='" + EmployeeCode + "' or  B.nominate_emp_code = '" + EmployeeCode + "') UNION ALL SELECT 1 FROM HRM_DEPARTMENT D WHERE D.REG_CD=R.REG_CD AND D.DIVISION_CD=R.HRM_DIVISION_CD AND D.UNIT_CD=R.HRM_UNIT_CD AND D.DEPARTMENT_CD=R.HRM_DEPARTMENT_CD AND D.EMP_HOD13='" + EmployeeCode + "' UNION ALL SELECT 1 FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD WHERE SD.DETAIL_NAME=TO_CHAR(T.EMP_CD) AND SD.SEQ_NO=153 AND T.EMP_CD = '" + EmployeeCode + "' AND T.REG_CD=R.REG_CD AND T.HRM_DIVISION_CD=R.HRM_DIVISION_CD AND T.HRM_UNIT_CD=R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD=R.HRM_DEPARTMENT_CD )";
                    dttt = db.GetData(queryyy);

                    if (dttt.Rows.Count > 0)
                    {
                        gv_PendingRequests.DataSource = dttt;
                        gv_PendingRequests.DataBind();

                        lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_GridMsg.Text = "No any pending request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = dttt;
                        gv_PendingRequests.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        protected void lbl_View_Click(object sender, EventArgs e)
        {
            try
            {
                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string JOB_REQ_ID = Ids[0].ToString();
                loadInfo(JOB_REQ_ID);
                loadApplicants(JOB_REQ_ID);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }



        public void loadInfo(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.*, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.TRACKING_ID, I.TRACKING_STATUS, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD=G.CADRE_CD INNER JOIN hrm_job_req_tracking I ON A.JOB_REQ_ID=I.JOB_REQ_ID WHERE A.JOB_REQ_ID  = '" + JOB_REQ_ID + "' ";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    div_Details.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {

                        TextBox1.Text = dr["TRACKING_ID"].ToString();
                        Text29.Text = dr["JOB_REQ_ID"].ToString();
                        Text30.Text = dr["VACANCIES"].ToString();
                        Text31.Text = dr["REQ_DATE"].ToString();
                        TextBox11.Text = dr["BUDGET_REF_NO"].ToString();
                        Text21.Text = dr["REG_CD"].ToString();
                        Text43.Text = dr["HRM_DIVISION_CD"].ToString() + " - " + dr["DIVISION_NAME"].ToString();
                        Text44.Text = dr["HRM_UNIT_CD"].ToString() + " - " + dr["UNIT_NAME"].ToString();
                        Text45.Text = dr["HRM_DEPARTMENT_CD"].ToString() + " - " + dr["DEPARTMENT_NAME"].ToString();
                        Text46.Text = dr["HRM_SECTION_CD"].ToString() + " - " + dr["SECTION_NAME"].ToString(); ;
                        Text47.Text = dr["HRM_CADRE_CD"].ToString() + " - " + dr["CADRE_NAME"].ToString();
                        Text48.Text = dr["CADRE_SUBCLASS_CD"].ToString() + " - " + dr["CADRE_SUBCLASS_NAME"].ToString();
                        Text49.Text = dr["HRM_DESIGNATION_CD"].ToString() + " - " + dr["DESIGNATION_NAME"].ToString();
                        TextBox2.Text = dr["REP_TO_EMP_CD"].ToString();
                        TextBox3.Text = dr["MIN_EDU_ID"].ToString();
                        TextBox9.Text = dr["MIN_EXPERIANCE"].ToString();
                        TextBox3.Text = dr["APPROVED_BUDGETED_SALARY"].ToString();
                        TextBox14.Text = dr["MIN_AGE"].ToString();
                        TextBox16.Text = dr["MAX_AGE"].ToString();

                        {
                            if (dr["GENDER"].ToString() == "M")
                            {
                                TextBox4.Text = "Male";
                            }

                            else if (dr["GENDER"].ToString() == "F")
                            {
                                TextBox4.Text = "Female";
                            }

                            else if (dr["GENDER"].ToString() == "O")
                            {
                                TextBox4.Text = "Other";
                            }
                        }



                        {
                            if (dr["HIRING_TYPE"].ToString() == "N")
                            {
                                TextBox5.Text = "New";
                            }

                            else if (dr["HIRING_TYPE"].ToString() == "R")
                            {
                                TextBox5.Text = "Replacement";
                            }


                        }


                        {
                            if (dr["TRACKING_STATUS"].ToString() == "O")
                            {
                                TextBox6.Text = "Open";
                            }

                            else if (dr["TRACKING_STATUS"].ToString() == "C")
                            {
                                TextBox6.Text = "Closed";
                            }
                        }


                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }






        public void loadApplicants(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT B.SELECTED_BY_HOD, B.APPLICANT_ID, B.APPLICANT_NAME, B.FATHER_NAME, B.DOB, B.CNIC, B.QUALIFICATION, B.LAST_SALARY, B.GENDER, B.ACTION, B.TRACKING_ID FROM hrm_job_req_tracking A, hrm_job_req_tracking_applicant B WHERE A.JOB_REQ_ID='" + JOB_REQ_ID + "' and A.tracking_id=B.tracking_id";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();

                    Label1.Text = "Please click on 'view' to process the request.";
                    Label1.ForeColor = System.Drawing.Color.Green;

                }
                else
                {

                    Label1.Text = "No any pending request.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }



        protected void lbl_View_Click1(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            if (btn != null)
            {
                GridViewRow row = btn.NamingContainer as GridViewRow;

                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View") as LinkButton).CommandArgument;
                    string fileName = GetFileNameFromDatabase(applicantId);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string filePath = @"E:\hr_docs\" + fileName;
                        FileInfo fileInfo = new FileInfo(filePath);

                        if (fileInfo.Exists)
                        {
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
                            Response.TransmitFile(filePath);
                            Response.End();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showInfoToast();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showInfoToast();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showInfoToast();", true);
                }
            }
        }




        protected void lbl_View_Click2(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            if (btn != null)
            {
                GridViewRow row = btn.NamingContainer as GridViewRow;

                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View") as LinkButton).CommandArgument;
                    string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0654.rdf&P_APPLICANT_ID=" + applicantId + "&paramform=no";
                    Response.Redirect(reportURL);
                }
            }
        }





        // Method to retrieve file name/path from database based on applicant ID
        public string GetFileNameFromDatabase(string applicantId)
        {
            string fileName = ""; // Default value if not found

            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT FILENAME FROM HRM_JOB_REQ_APPLICANT_ATTACH WHERE APPLICANT_ID = '" + applicantId + "' AND ROWNUM=1";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        fileName = dr["FILENAME"].ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showInfoToast();", true);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or logging here
                // e.g., Log the exception or show an error message
            }

            return fileName;
        }

        protected void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string isChecked = "";
                CheckBox chkBox = sender as CheckBox;
                GridViewRow row = chkBox.NamingContainer as GridViewRow;
                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View") as LinkButton).CommandArgument;
                    if (chkBox.Checked)
                    {
                        isChecked = "Y";
                    }
                    else
                    {
                        isChecked = "";
                    }


                    // Update the database based on the checkbox status
                    UpdateDatabaseWithCheckBoxStatus(applicantId, isChecked);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }
        // Method to update the database based on checkbox status
        private void UpdateDatabaseWithCheckBoxStatus(string applicantId, string isChecked)
        {
            try
            {
                string Query = "UPDATE hrm_job_req_tracking_applicant SET SELECTED_BY_HOD = '" + isChecked + "' WHERE APPLICANT_ID = '" + applicantId + "'";
                string Result = db.PostData(Query);
                if (Result == "Done")
                {
                    if (isChecked == "Y")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSelectionToast", "showSelectionToast();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showNotSelectionToast", "showNotSelectionToast();", true);
                    }
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    GridView2.DataSource = null;
                    loadPendingRequests();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    DropDownList ddlAction = e.Row.FindControl("ddlAction") as DropDownList;
                    if (ddlAction != null)
                    {
                        // Populate the dropdown list with values from your database or define your own options
                        ddlAction.Items.Add(new ListItem("Select an option", ""));
                        ddlAction.Items.Add(new ListItem("Job Offer Placed", "J"));
                        //ddlAction.Items.Add(new ListItem("Initial Shortlisting ", "S"));
                        ddlAction.Items.Add(new ListItem("Interview in Process", "I"));
                        //ddlAction.Items.Add(new ListItem("Interview Re-Schedule", "R"));
                        ddlAction.Items.Add(new ListItem("Shortlisted", "F"));
                        ddlAction.Items.Add(new ListItem("On-Hold", "H"));
                        ddlAction.Items.Add(new ListItem("Completed", "C"));
                        ddlAction.Items.Add(new ListItem("Rejected", "X"));

                        // Set the selected value for each row based on the current value in the database
                        string currentActionValue = dr["ACTION"].ToString();
                        ddlAction.SelectedValue = currentActionValue;
                    }

                    CheckBox chkBox = e.Row.FindControl("chkBox") as CheckBox;
                    if (ddlAction != null)
                    {
                        string selectedByHOD = dr["SELECTED_BY_HOD"].ToString();
                        if (selectedByHOD == "Y")
                        {
                            chkBox.Checked = true; // Check the checkbox
                        }
                        else
                        {
                            chkBox.Checked = false; // Uncheck the checkbox
                        }
                    }

                    Label label = e.Row.FindControl("lbl") as Label;
                    if (label != null)
                    {
                        string gndr = dr["GENDER"].ToString();
                        if (gndr == "M")
                        {
                            label.Text = "Male";
                        }
                        if (gndr == "F")
                        {
                            label.Text = "Female";
                        }
                    }

                }
            }
        }





        protected void SelectedDDLChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewRow row = ddl.NamingContainer as GridViewRow;
            if (row != null)
            {

                string applicantId = (row.FindControl("lbl_View") as LinkButton).CommandArgument;
                string selectedValue = ddl.SelectedValue;


                // Perform the database update here
                UpdateDDLDatabase(selectedValue, applicantId);
            }
        }

        // Method to update the database
        private void UpdateDDLDatabase(string selectedValue, string applicantId)
        {
            try
            {
                // Create a SQL query or use stored procedures to update the database
                string Query = "UPDATE hrm_job_req_tracking_applicant SET ACTION ='" + selectedValue + "' WHERE APPLICANT_ID= '" + applicantId + "'";

                string Result = db.PostData(Query);
                if (Result == "Done")
                {
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    GridView2.DataSource = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    loadPendingRequests();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string jobreqid = Text29.Text;
            string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0643.rdf&P_JOB_REQ_ID=" + jobreqid + "&paramform=no";
            Response.Redirect(reportURL);
        }


    }
}


