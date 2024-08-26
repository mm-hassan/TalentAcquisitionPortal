using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class HOD_Review : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;
        string cadreno_TA2;
        int resultt = 1;


        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();

            
                    

            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                loadTA2PendingRequests();
                loadTA4PendingRequests();
                loadTA5PendingRequests();
                TA2_Section.Visible = false;
                TA4_Section.Visible = false;
                TA5_Section.Visible = false;



                // Check if a success message is stored in session
                if (Session["SuccessMessage"] != null)
                {
                    string successMessage = Session["SuccessMessage"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("SuccessMessage");
                }

                // Check if an update message is stored in session
                if (Session["UpdateMessage"] != null)
                {
                    string updateMessage = Session["UpdateMessage"].ToString();
                    // Display the update message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showUpdateToast", "showUpdateToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("UpdateMessage");
                }

                // Check if an error message is stored in session
                if (Session["ErrorMessage"] != null)
                {
                    string errorMessage = Session["ErrorMessage"].ToString();
                    // Display the error message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("ErrorMessage");
                }

                // Check if a database error message is stored in session
                if (Session["DatabaseErrorMessage"] != null)
                {
                    string databaseErrorMessage = Session["DatabaseErrorMessage"].ToString();
                    // Display the database error message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDatabaseErrorToast", "showDatabaseErrorToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("DatabaseErrorMessage");
                }

                // Check if an info message is stored in session
                if (Session["InfoMessage"] != null)
                {
                    string infoMessage = Session["InfoMessage"].ToString();
                    // Display the info message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showInfoToast", "showInfoToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("InfoMessage");
                }

                // Check if a warning message is stored in session
                if (Session["WarningMessage"] != null)
                {
                    string warningMessage = Session["WarningMessage"].ToString();
                    // Display the warning message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showWarningToast", "showWarningToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("WarningMessage");
                }

                // Check if a remarks warning message is stored in session
                if (Session["RemarksWarningMessage"] != null)
                {
                    string remarksWarningMessage = Session["RemarksWarningMessage"].ToString();
                    // Display the remarks warning message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showRemarksWarningToast", "showRemarksWarningToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("RemarksWarningMessage");
                }

                // Check if a fail tracking message is stored in session
                if (Session["FailTrackingMessage"] != null)
                {
                    string failTrackingMessage = Session["FailTrackingMessage"].ToString();
                    // Display the fail tracking message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "failTrackingToast", "failTrackingToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("FailTrackingMessage");
                }

                // Check if a fail applicant message is stored in session
                if (Session["FailApplicantMessage"] != null)
                {
                    string failApplicantMessage = Session["FailApplicantMessage"].ToString();
                    // Display the fail applicant message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "failApplicantToast", "failApplicantToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("FailApplicantMessage");
                }

                // Check if a selection message is stored in session
                if (Session["SelectionMessage"] != null)
                {
                    string selectionMessage = Session["SelectionMessage"].ToString();
                    // Display the selection message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSelectionToast", "showSelectionToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("SelectionMessage");
                }

                // Check if a not selection message is stored in session
                if (Session["NotSelectionMessage"] != null)
                {
                    string notSelectionMessage = Session["NotSelectionMessage"].ToString();
                    // Display the not selection message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showNotSelectionToast", "showNotSelectionToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("NotSelectionMessage");
                }
            }

        }

        protected void gv_RecentApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_TA2PendingRequests.PageIndex = e.NewPageIndex;
            loadTA2PendingRequests();
        }

        public void loadTA2PendingRequests()
        {
            try
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Hrm_Setup_Detl N WHERE N.Detail_Name='" + EmployeeCode + "' and (N.Seq_No=121 or N.Seq_No=141)";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    TA2_Super.Text = "true";
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT A.JOB_REQ_ID, D.DEPARTMENT_NAME, F.DESIGNATION_NAME, A.* FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD where nvl(A.HOD_APPROVED,'x') !='Y' AND nvl(A.HOD_APPROVED,'x') !='R' AND nvl(A.Status,'x')!= 'C' order by A.Job_Req_Id DESC";
                    dtt = db.GetData(queryy);
                    lbl_HODApprovalsCount.Text = dtt.Rows.Count.ToString(); // Update the label with the count
                    if (dtt.Rows.Count > 0)
                    {
                        gv_TA2PendingRequests.DataSource = dtt;
                        gv_TA2PendingRequests.DataBind();

                        lbl_TA2GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA2GridMsg.ForeColor = System.Drawing.Color.Green;


                    }
                    else
                    {
                        lbl_TA2GridMsg.Text = "No any pending request.";
                        lbl_TA2GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA2PendingRequests.DataSource = dtt;
                        gv_TA2PendingRequests.DataBind();
                    }
                }
                else
                {
                    TA2_Super.Text = "false";
                    DataTable dttt = new DataTable();
                    //string queryyy = "SELECT DISTINCT e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', e.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', e.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', e.HRM_UNIT_CD, e.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', e.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, e.TOTAL_GROSS_SALARY, e.vacancies AS req_strength, e.approve_str, e.utilize_str FROM HRM_JOB_REQUISITION e WHERE NVL(e.hod_approved, 'N') = 'N' AND e.job_req_id NOT IN (SELECT t.job_req_id FROM HRM_JOB_REQ_TRACKING t WHERE t.req_status = 'C') AND ( (e.reg_cd, e.hrm_division_cd, e.hrm_unit_cd, e.hrm_department_cd) IN ( SELECT b.reg_cd, b.division_cd, b.unit_cd, b.department_cd FROM hrm_department b, users u WHERE u.emp_cd = TO_CHAR(b.emp_hod13) AND u.usr_cd =  '" + EmployeeUCode + "' UNION ALL SELECT e.REG_CD, e.HRM_DIVISION_CD, e.HRM_UNIT_CD, e.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE t, HRM_SETUP_DETL sd, users u WHERE sd.DETAIL_NAME = TO_CHAR(t.EMP_CD) AND sd.SEQ_NO = 153 AND u.emp_cd = TO_CHAR(t.EMP_CD) AND t.REG_CD = e.REG_CD AND t.HRM_DIVISION_CD = e.HRM_DIVISION_CD AND t.HRM_UNIT_CD = e.HRM_UNIT_CD AND t.HRM_DEPARTMENT_CD = e.HRM_DEPARTMENT_CD AND u.usr_cd =  '" + EmployeeUCode + "' ) UNION ALL SELECT e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', e.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', e.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', e.HRM_UNIT_CD, e.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', e.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, e.TOTAL_GROSS_SALARY, e.vacancies AS req_strength, e.approve_str, e.utilize_str FROM HRM_JOB_REQUISITION e WHERE NVL(e.hod_approved, 'N') = 'N' AND e.job_req_id NOT IN (SELECT t.job_req_id FROM HRM_JOB_REQ_TRACKING t WHERE t.req_status = 'C') AND (e.reg_cd, e.hrm_division_cd, e.hrm_unit_cd, e.hrm_department_cd) IN ( SELECT b.reg_cd, b.hrm_division_cd, b.hrm_unit_cd, b.hrm_department_cd FROM hrm_department_budget b, users u WHERE u.emp_cd = TO_CHAR(b.emp_hod) AND u.usr_cd = '01707' UNION ALL SELECT b.reg_cd, b.hrm_division_cd, b.hrm_unit_cd, b.hrm_department_cd FROM hrm_department_budget b, users u WHERE u.emp_cd = TO_CHAR(b.Nominate_Emp_Code) AND u.usr_cd = '" + EmployeeUCode + "' ) ) ORDER BY 1 DESC";
                    string queryyy = "SELECT DISTINCT e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies AS req_strength, e.approve_str, e.utilize_str FROM HRM_JOB_REQUISITION e WHERE NVL(e.hod_approved, 'N') = 'N' AND NVL(e.status, 'N') != 'C' AND e.job_req_id NOT IN (SELECT t.job_req_id FROM HRM_JOB_REQ_TRACKING t WHERE t.req_status = 'C') AND ((e.reg_cd, e.hrm_division_cd, e.hrm_unit_cd, e.hrm_department_cd) IN (SELECT b.reg_cd, b.division_cd, b.unit_cd, b.department_cd FROM hrm_department b, users u WHERE u.emp_cd = TO_CHAR(b.emp_hod13) AND u.usr_cd = '" + EmployeeUCode + "' UNION ALL SELECT DISTINCT E.REG_CD, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, users u WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND u.emp_cd = TO_CHAR(T.EMP_CD) AND T.REG_CD = E.REG_CD AND T.HRM_DIVISION_CD = E.HRM_DIVISION_CD AND T.HRM_UNIT_CD = E.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = E.HRM_DEPARTMENT_CD AND u.usr_cd = '" + EmployeeUCode + "')) UNION SELECT DISTINCT e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies AS req_strength, e.approve_str, e.utilize_str FROM HRM_JOB_REQUISITION e WHERE nvl(e.HOD_APPROVED,'x') !='Y' AND nvl(e.HOD_APPROVED,'x') !='R' AND NVL(e.status, 'N') != 'C' AND e.job_req_id NOT IN (SELECT t.job_req_id FROM HRM_JOB_REQ_TRACKING t WHERE t.req_status = 'C') AND (e.reg_cd, e.hrm_division_cd, e.hrm_unit_cd, e.hrm_department_cd) IN (SELECT b.reg_cd, b.hrm_division_cd, b.hrm_unit_cd, b.hrm_department_cd FROM hrm_department_budget b, users u WHERE u.emp_cd = TO_CHAR(b.emp_hod) AND u.usr_cd = '" + EmployeeUCode + "' UNION ALL SELECT b.reg_cd, b.hrm_division_cd, b.hrm_unit_cd, b.hrm_department_cd FROM hrm_department_budget b, users u WHERE u.emp_cd = TO_CHAR(b.Nominate_Emp_Code) AND u.usr_cd = '" + EmployeeUCode + "') ORDER BY 1 DESC";
                    dttt = db.GetData(queryyy);
                    lbl_HODApprovalsCount.Text = dttt.Rows.Count.ToString(); // Update the label with the count
                    if (dttt.Rows.Count > 0)
                    {
                        gv_TA2PendingRequests.DataSource = dttt;
                        gv_TA2PendingRequests.DataBind();

                        lbl_TA2GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA2GridMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lbl_TA2GridMsg.Text = "No any pending request.";
                        lbl_TA2GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA2PendingRequests.DataSource = dttt;
                        gv_TA2PendingRequests.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void loadTA4PendingRequests()
        {
            try
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Hrm_Setup_Detl N WHERE N.Detail_Name='" + EmployeeCode + "' and (N.Seq_No=121 or N.Seq_No=141)";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Super_TA4.Text = "true";
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT T.TRACKING_ID, r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID)=0 then 'In-Progress' Else 'Job Offer Placed to '||COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION='J') Action, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2','007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T,   HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID=T.JOB_REQ_ID and NVL(t.tracking_status, 'N') != 'C' and nvl(r.hod_approved,'N') ='Y' order by TRACKING_ID desc";
                    dtt = db.GetData(queryy);
                    lbl_ApplicantDetailsCount.Text = dtt.Rows.Count.ToString(); // Update the label with the count
                    if (dtt.Rows.Count > 0)
                    {
                        gv_TA4PendingRequests.DataSource = dtt;
                        gv_TA4PendingRequests.DataBind();

                        lbl_TA4_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA4_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_TA4_GridMsg.Text = "No any pending request.";
                        lbl_TA4_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA4PendingRequests.DataSource = dtt;
                        gv_TA4PendingRequests.DataBind();
                    }
                }
                else
                {
                    Super_TA4.Text = "false";
                    DataTable dttt = new DataTable();
                    string queryyy = "SELECT T.TRACKING_ID, r.job_req_id, r.req_date, (SELECT case when COUNT(A.APPLICANT_ID) = 0 then 'In-Progress' Else 'Job Offer Placed to ' || COUNT(A.APPLICANT_ID) end FROM HRM_JOB_REQ_TRACKING_APPLICANT A WHERE A.TRACKING_ID = T.TRACKING_ID AND A.ACTION = 'J') Action, decode(t.Tracking_Status, 'O', 'Open', 'C', 'Closed') Tracking_Status, r.budget_ref_no BUDGET_CD, decode(R.REG_CD, '001', 'AK-1', '002', 'AK-2', '007', 'AK-3', '008', 'Sattar', R.REG_CD) Region_Name, hrm_code_desc('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, R.hrm_cadre_cd CADRE, hrm_code_desc('DIVISION', R.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', R.HRM_UNIT_CD, R.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', R.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME FROM HRM_JOB_REQ_TRACKING T, HRM_JOB_REQUISITION R WHERE R.JOB_REQ_ID = T.JOB_REQ_ID and NVL(T.Tracking_Status, 'N') != 'C' and nvl(r.hod_approved, 'N') = 'Y' and r.hrm_cadre_cd not in ('12', '13') AND EXISTS (SELECT 1 FROM HRM_DEPARTMENT_BUDGET B WHERE B.REG_CD = R.REG_CD AND B.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND B.HRM_UNIT_CD = R.HRM_UNIT_CD AND B.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND (B.EMP_HOD = '" + EmployeeCode + "' or B.nominate_emp_code = '" + EmployeeCode + "') UNION ALL SELECT 1 FROM HRM_DEPARTMENT D WHERE D.REG_CD = R.REG_CD AND D.DIVISION_CD = R.HRM_DIVISION_CD AND D.UNIT_CD = R.HRM_UNIT_CD AND D.DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND D.EMP_HOD13 = '" + EmployeeCode + "' UNION ALL SELECT 1 FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND T.EMP_CD = '" + EmployeeCode + "' AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD) order by TRACKING_ID desc";
                    dttt = db.GetData(queryyy);
                    lbl_ApplicantDetailsCount.Text = dttt.Rows.Count.ToString(); // Update the label with the count
                    if (dttt.Rows.Count > 0)
                    {
                        gv_TA4PendingRequests.DataSource = dttt;
                        gv_TA4PendingRequests.DataBind();

                        lbl_TA4_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA4_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_TA4_GridMsg.Text = "No any pending request.";
                        lbl_TA4_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA4PendingRequests.DataSource = dttt;
                        gv_TA4PendingRequests.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
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


        string getroll()
        {
            string roll = "";
            string finalroll = "";
            try
            {
                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                dt = null;

                string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_TA_RIGHTS_VIEW T WHERE T.EMP_CD= '" + EmployeeCode + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dt.Rows)
                    {
                        roll = drr["ROLL"].ToString();
                        if (roll == "SUPER")
                        {
                            finalroll = roll;
                            break;
                        }

                        else
                        {
                            DataRow firstRow = dt.Rows[0]; // Access the first row directly
                            finalroll = firstRow["ROLL"].ToString();
                        }


                    }
                }
                else
                {
                    string queryy = "SELECT * FROM Hrm_Setup_Detl A WHERE A.Seq_No=121 AND A.Detail_Name='" + EmployeeCode + "' ";
                    dtt = db.GetData(queryy);
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr["Detail_Name"].ToString() != "")
                        {
                            finalroll = "SUPER";
                            break;
                        }
                    }

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
            return finalroll;
        }


        public void loadTA5PendingRequests()
        {
            try
            {
                string roll = getroll();
                if (roll == "SUPER")
                {
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID WHERE E.SELECTED_BY_HOD = 'Y'UNION ALL SELECT NULL,  E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID WHERE E.SELECTED_BY_HOD = 'Y' AND NVL(R.Status, 'X') != 'C' AND NVL(T.Tracking_Status, 'X') != 'C' ORDER BY 1 DESC";

                    dtt = db.GetData(queryy);
                    if (dtt.Rows.Count > 0)
                    {
                        gv_TA5PendingRequests.DataSource = dtt;
                        gv_TA5PendingRequests.DataBind();
                        lbl_ApplicanEvaluationCount.Text = dtt.Rows.Count.ToString(); // Update the label with the count
                        lbl_TA5_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA5_GridMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {

                        lbl_TA5_GridMsg.Text = "No any pending request.";
                        lbl_TA5_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA5PendingRequests.DataSource = dtt;
                        gv_TA5PendingRequests.DataBind();
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    string query = "WITH EVALUATED_APPLICANTS AS ( SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID AS APPLICANT_ID_DUPLICATE, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID WHERE E.SELECTED_BY_HOD = 'Y' AND NVL(T.Tracking_Status, 'X') != 'C' AND ((R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ((SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "'))) ), NON_EVALUATED_APPLICANTS AS ( SELECT NULL AS EVALUATION_ID, E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID AS APPLICANT_ID_DUPLICATE, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID WHERE E.SELECTED_BY_HOD = 'Y' AND NVL(T.Tracking_Status, 'X') != 'C' AND ((R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ((SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "'))) ) SELECT * FROM EVALUATED_APPLICANTS UNION ALL SELECT * FROM NON_EVALUATED_APPLICANTS WHERE APPLICANT_ID_DUPLICATE NOT IN (SELECT APPLICANT_ID_DUPLICATE FROM EVALUATED_APPLICANTS) ORDER BY 1 DESC";

                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        gv_TA5PendingRequests.DataSource = dt;
                        gv_TA5PendingRequests.DataBind();
                        lbl_ApplicanEvaluationCount.Text = dt.Rows.Count.ToString(); // Update the label with the count
                        lbl_TA5_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_TA5_GridMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {

                        lbl_TA5_GridMsg.Text = "No any pending request.";
                        lbl_TA5_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_TA5PendingRequests.DataSource = dt;
                        gv_TA5PendingRequests.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }









        ////////////////////////////////////////               TA2                            //////////////////////////////////////////



        protected void lbl_TA2_View_Click(object sender, EventArgs e)
        {
            try
            {
                TA2_Section.Visible = true;
                TA4_Section.Visible = false;
                TA5_Section.Visible = false;
                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string JOB_REQ_ID = Ids[0].ToString();
                load_TA2_Info(JOB_REQ_ID);

                // Trigger the modal to be shown
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void load_TA2_Info(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT (A.Approve_Str - A.Utilize_Str) AS BALANCE, A.*, A.HOD_APPROVED, (SELECT B.DIVISION_NAME FROM HRM_DIVISION_mst B WHERE A.HRM_DIVISION_CD = B.DIVISION_CD) AS DIVISION_NAME, (SELECT C.UNIT_NAME FROM HRM_UNIT_mst C WHERE A.HRM_UNIT_CD = C.UNIT_CD AND A.HRM_DIVISION_CD = C.DIVISION_CD) AS UNIT_NAME, (SELECT D.DEPARTMENT_NAME FROM HRM_DEPARTMENT_mst D WHERE A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD) AS DEPARTMENT_NAME, (SELECT E.SECTION_NAME FROM HRM_SECTION_MST E WHERE A.HRM_SECTION_CD = E.SECTION_CD) AS SECTION_NAME, (SELECT F.DESIGNATION_NAME FROM HRM_DESIGNATION_mst F WHERE A.HRM_DESIGNATION_CD = F.DESIGNATION_CD) AS DESIGNATION_NAME, (SELECT G.CADRE_NAME FROM hrM_cadre_mst G WHERE A.HRM_CADRE_CD = G.CADRE_CD) AS CADRE_NAME, (SELECT I.REG_NAME FROM REGIONS I WHERE A.REG_CD = I.REG_CD) AS REG_NAME, hrm_code_desc('EMP_NAME', A.rep_to_emp_cd, NULL, NULL, NULL) AS EMP_NAME, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd, NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                //string query = "SELECT (A.Approve_Str-A.Utilize_Str) AS BALANCE, A.*, A.HOD_APPROVED, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.REG_NAME, hrm_code_desc('EMP_NAME', A.rep_to_emp_cd, NULL, NULL, NULL) AS EMP_NAME, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD = G.CADRE_CD INNER JOIN REGIONS I ON A.REG_CD = I.REG_CD WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {

                    div_TA2_EmployeeDetails.Visible = true;
                    hiring_info.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        Approved_TA2.Text = dr["APPROVE_STR"].ToString();
                        Current_TA2.Text = dr["UTILIZE_STR"].ToString();
                        Balance_TA2.Text = dr["BALANCE"].ToString();

                        Req_Id_TA2.Text = dr["JOB_REQ_ID"].ToString();
                        Vac_TA2.Text = dr["VACANCIES"].ToString();
                        Budget_TA2.Text = dr["BUDGET_REF_NO"].ToString();
                        Req_Dat_TA2.Text = dr["REQ_DATE"].ToString();
                        Reg_TA2.Text = dr["REG_CD"].ToString() + " - " + dr["REG_NAME"].ToString();
                        Sec_TA2.Text = dr["HRM_SECTION_CD"].ToString() + " - " + dr["SECTION_NAME"].ToString();
                        Div_TA2.Text = dr["HRM_DIVISION_CD"].ToString() + " - " + dr["DIVISION_NAME"].ToString();
                        Dep_TA2.Text = dr["HRM_DEPARTMENT_CD"].ToString() + " - " + dr["DEPARTMENT_NAME"].ToString();
                        Cad_TA2.Text = dr["HRM_CADRE_CD"].ToString() + " - " + dr["CADRE_NAME"].ToString();
                        Cad_Sub_TA2.Text = dr["CADRE_SUBCLASS_CD"].ToString() + " - " + dr["CADRE_SUBCLASS_NAME"].ToString();
                        Unit_TA2.Text = dr["HRM_UNIT_CD"].ToString() + " - " + dr["UNIT_NAME"].ToString();
                        Des_TA2.Text = dr["HRM_DESIGNATION_CD"].ToString() + " - " + dr["DESIGNATION_NAME"].ToString();

                        Rep_TA2.Text = dr["REP_TO_EMP_CD"].ToString() + " - " + dr["EMP_NAME"].ToString();
                        {
                            if (dr["GENDER"].ToString() == "M")
                            {
                                Gen_TA2.Text = "Male";
                            }

                            else if (dr["GENDER"].ToString() == "F")
                            {
                                Gen_TA2.Text = "Female";
                            }

                            else if (dr["GENDER"].ToString() == "O")
                            {
                                Gen_TA2.Text = "Other";
                            }
                        }
                        string Min_Educ_TA2 = dr["MIN_EDU_ID"].ToString();
                        Min_Exp_TA2.Text = dr["MIN_EXPERIANCE"].ToString();
                        Min_Age_TA2.Text = dr["MIN_AGE"].ToString();
                        Max_Age_TA2.Text = dr["MAX_AGE"].ToString();
                        Rem_TA2.Text = dr["REMARKS"].ToString();
                        Job_Des_TA2.Text = dr["JOB_DESCRIPTION"].ToString();

                        //App_ROP_TA2.Text = dr["APPROVED_BUDGETED_SALARY"].ToString();
                        //Prod_Allow_TA2.Text = dr["PROD_ALLOWANCE"].ToString();
                        //Over_TA2.Text = dr["OVERTIME"].ToString();
                        //Ben_TA2.Text = dr["BENEFITS"].ToString();
                        //Gross_TA2.Text = dr["TOTAL_GROSS_SALARY"].ToString();
                        //Att_Allow_TA2.Text = dr["ATTEND_ALLOWANCE"].ToString();
                        //Fest_OT_TA2.Text = dr["HOLIDAY_OT"].ToString();
                        {
                            if (dr["HIRING_TYPE"].ToString() == "N")
                            {
                                Hir_Type_TA2.Text = "New";
                                hiring_grid.Visible = false;
                            }

                            else if (dr["HIRING_TYPE"].ToString() == "R")
                            {
                                Hir_Type_TA2.Text = "Replacement";
                                bindhiringgrid(JOB_REQ_ID);
                            }
                        }
                        //Note_HR_TA2.Text = dr["NOTES_BY_HR"].ToString();
                        HOD_Approved_TA2.Text = dr["HOD_APPROVED"].ToString();
                        cadreno_TA2 = dr["HRM_CADRE_CD"].ToString();



                DataTable dat = new DataTable();
                string qual_query = "SELECT D.DETAIL_NAME, D.DETAIL_ID FROM HRM_SETUP_DETL D WHERE D.MASTER_CD = 'QUALIFICATION' and D.Detail_Id='" + Min_Educ_TA2 + "' Order By 1";
                //string query = "SELECT (A.Approve_Str-A.Utilize_Str) AS BALANCE, A.*, A.HOD_APPROVED, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.REG_NAME, hrm_code_desc('EMP_NAME', A.rep_to_emp_cd, NULL, NULL, NULL) AS EMP_NAME, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD = G.CADRE_CD INNER JOIN REGIONS I ON A.REG_CD = I.REG_CD WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                dat = db.GetData(qual_query);
                if (dat.Rows.Count > 0)
                {
                    foreach (DataRow drr in dat.Rows)
                    {
                        Min_Edu_TA2.Text = drr["DETAIL_NAME"].ToString();
                    }
                }

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void bindhiringgrid(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = null;
                string query = "SELECT  D.REP_ID, D.JOB_REQ_ID, D.EMP_CD,D.BUDGET_REF_NO, B.EMP_NAME, B.Last_Duty, B.Emp_Status FROM HRM_JOB_REQ_REPLACE_DTL D INNER JOIN Hrm_Employee B on D.EMP_CD=B.EMP_CD INNER JOIN Hrm_Job_Requisition C ON D.JOB_REQ_ID=C.Job_Req_Id WHERE D.JOB_REQ_ID='" + JOB_REQ_ID + "' ";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Hiring_TA2_Grid.DataSource = dt;
                    Hiring_TA2_Grid.DataBind();
                }
                else
                {
                    hiring_grid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void ButtonHold_TA2_Click(object sender, EventArgs e)
        {
            try
            {
                string jobreqid = Req_Id_TA2.Text;
                string hodapp = HOD_Approved_TA2.Text;
                if (HOD_Approved_TA2.Text == "H")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (HOD_Approved_TA2.Text != "H")
                {
                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_COMMENTS = '" + Notes_HOD_TA2.Text + "', HOD_APPROVED = 'H', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        Session["UpdateMessage"] = "Your success message here";
                        Response.Redirect("HOD_Review.aspx");
                        
                    }
                    else
                    {
                        Session["ErrorMessage"] = "Your success message here";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void ButtonReject_TA2_Click(object sender, EventArgs e)
        {
            try
            {
                string jobreqid = Req_Id_TA2.Text;
                string hodapp = HOD_Approved_TA2.Text;

                if (HOD_Approved_TA2.Text == "R")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (HOD_Approved_TA2.Text != "R")
                {

                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_COMMENTS = '" + Notes_HOD_TA2.Text + "',HOD_APPROVED = 'R', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        Session["UpdateMessage"] = "Your success message here";
                        Response.Redirect("HOD_Review.aspx");
                        
                    }
                    else
                    {
                        Session["ErrorMessage"] = "Your success message here";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void ButtonApprove_TA2_Click(object sender, EventArgs e)
        {
            try
            {
                string jobreqid = Req_Id_TA2.Text;
                string hodapp = HOD_Approved_TA2.Text;

                if (HOD_Approved_TA2.Text == "Y")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (HOD_Approved_TA2.Text != "Y")
                {

                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_COMMENTS = '" + Notes_HOD_TA2.Text + "', HOD_APPROVED = 'Y', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Approved !');", true);
                        //GetHrbp_TA2();
                        EmailIntimationHOD_TA2();
                        Session["SuccessMessage"] = "Your success message here";
                        Response.Redirect("HOD_Review.aspx");
                        
                    }
                    else
                    {
                        Session["ErrorMessage"] = "Your success message here";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void ButtonReport_TA2_Click(object sender, EventArgs e)
        {
            string jobreqid = Req_Id_TA2.Text;
            string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0643.rdf&P_JOB_REQ_ID=" + jobreqid + "&paramform=no";
            Response.Redirect(reportURL);
        }

        protected void Hiring_TA2_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    Label label = e.Row.FindControl("lbl") as Label;
                    if (label != null)
                    {
                        string status = dr["Emp_Status"].ToString();
                        if (status == "I")
                        {
                            label.Text = "Inactive";
                        }
                        else if (status == "S")
                        {
                            label.Text = "Temporary Inactive";
                        }
                        else if (status == "A")
                        {
                            label.Text = "Active";
                        }
                    }
                }
            }
        }

        //void GetHrbp_TA2()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        string getHrEmail = "SELECT B.EMP_NAME, B.e_Mail, A.DETAIL_NAME FROM Hrm_Setup_Detl A JOIN HRM_EMPLOYEE B ON TO_CHAR(B.EMP_CD) = A.DETAIL_NAME WHERE A.Seq_No = 121 AND B.EMP_STATUS = 'A'";
        //        dt = db.GetData(getHrEmail);
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                string hrname = dr["EMP_NAME"].ToString();
        //                string hremail = dr["e_Mail"].ToString();
        //                if (hremail != "")
        //                {
        //                    //EmailIntimationHOD_TA2(hremail);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
        //    }
        //}

        public void EmailIntimationHOD_TA2()
        {
            try
            {
                SendResponse res = new SendResponse();

                string sub = "HOD Approved Requisition";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "New requisition request has been submitted for your working, following are the details. " + "<br /><br />";
                eBody += "Budget Code : " + Budget_TA2.Text + "<br />";
                eBody += "Job Req. ID : " + Req_Id_TA2.Text + "<br />";
                eBody += "Request Date : " + Req_Dat_TA2.Text + "<br />";
                eBody += "Hiring Type : " + Hir_Type_TA2.Text + "<br />";
                eBody += "No. of Vacancies : " + Vac_TA2.Text + "<br />";
                eBody += "Designation : " + Des_TA2.Text + "<br />";
                eBody += "Cadre : " + Cad_TA2 + "<br />";
                eBody += "Division : " + Div_TA2.Text + "<br />";
                eBody += "Department : " + Dep_TA2.Text + "<br />";
                eBody += "Section : " + Sec_TA2.Text + "<br />";
                eBody += "Approved Head Count : " + Approved_TA2.Text + "<br />";
                eBody += "Current Head Count : " + Current_TA2.Text + "<br />";
                eBody += "Balance Head Count : " + Balance_TA2.Text + "<br />";
                eBody += "Remarks : " + Rem_TA2.Text + "<br /><br />";
                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmail_HODTAB(eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }





        ////////////////////////////////////////               TA4                            //////////////////////////////////////////


        protected void lbl_TA4_View_Click(object sender, EventArgs e)
        {
            try
            {

                TA4_Section.Visible = true;
                TA2_Section.Visible = false;
                TA5_Section.Visible = false;
                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string JOB_REQ_ID = Ids[0].ToString();
                loadInfo_TA4(JOB_REQ_ID);
                loadApplicants_TA4(JOB_REQ_ID);
                // Trigger the modal using ScriptManager
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#TA4Modal').modal('show');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void loadInfo_TA4(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.*, (SELECT B.DIVISION_NAME FROM HRM_DIVISION_mst B WHERE A.HRM_DIVISION_CD = B.DIVISION_CD) AS DIVISION_NAME, (SELECT C.UNIT_NAME FROM HRM_UNIT_mst C WHERE A.HRM_UNIT_CD = C.UNIT_CD AND A.HRM_DIVISION_CD = C.DIVISION_CD) AS UNIT_NAME, (SELECT D.DEPARTMENT_NAME FROM HRM_DEPARTMENT_mst D WHERE A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD) AS DEPARTMENT_NAME, (SELECT E.SECTION_NAME FROM HRM_SECTION_MST E WHERE A.HRM_SECTION_CD = E.SECTION_CD) AS SECTION_NAME, (SELECT F.DESIGNATION_NAME FROM HRM_DESIGNATION_mst F WHERE A.HRM_DESIGNATION_CD = F.DESIGNATION_CD) AS DESIGNATION_NAME, (SELECT G.CADRE_NAME FROM hrM_cadre_mst G WHERE A.HRM_CADRE_CD = G.CADRE_CD) AS CADRE_NAME, (SELECT I.TRACKING_ID FROM hrm_job_req_tracking I WHERE A.JOB_REQ_ID = I.JOB_REQ_ID) AS TRACKING_ID, (SELECT I.TRACKING_STATUS FROM hrm_job_req_tracking I WHERE A.JOB_REQ_ID = I.JOB_REQ_ID) AS TRACKING_STATUS, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd, NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                //string query = "SELECT A.*, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.TRACKING_ID, I.TRACKING_STATUS, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD=G.CADRE_CD INNER JOIN hrm_job_req_tracking I ON A.JOB_REQ_ID=I.JOB_REQ_ID WHERE A.JOB_REQ_ID  = '" + JOB_REQ_ID + "' ";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    div_TA4_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        {
                            if (dr["TRACKING_STATUS"].ToString() == "O")
                            {
                                Track_Status_TA4.Text = "Open";
                            }

                            else if (dr["TRACKING_STATUS"].ToString() == "C")
                            {
                                Track_Status_TA4.Text = "Closed";
                            }
                        }
                        Req_ID_TA4.Text = dr["JOB_REQ_ID"].ToString();
                        Vac_TA4.Text = dr["VACANCIES"].ToString();
                        Bud_Cd_TA4.Text = dr["BUDGET_REF_NO"].ToString();
                        Track_Id_TA4.Text = dr["TRACKING_ID"].ToString();
                        {
                            if (dr["HIRING_TYPE"].ToString() == "N")
                            {
                                Hir_Type_TA4.Text = "New";
                            }

                            else if (dr["HIRING_TYPE"].ToString() == "R")
                            {
                                Hir_Type_TA4.Text = "Replacement";
                            }
                        }
                        Req_Date_TA4.Text = dr["REQ_DATE"].ToString();
                        Reg_Cd_TA4.Text = dr["REG_CD"].ToString();
                        Sec_TA4.Text = dr["HRM_SECTION_CD"].ToString() + " - " + dr["SECTION_NAME"].ToString(); ;
                        Div_TA4.Text = dr["HRM_DIVISION_CD"].ToString() + " - " + dr["DIVISION_NAME"].ToString();
                        Dep_TA4.Text = dr["HRM_DEPARTMENT_CD"].ToString() + " - " + dr["DEPARTMENT_NAME"].ToString();
                        Cad_TA4.Text = dr["HRM_CADRE_CD"].ToString() + " - " + dr["CADRE_NAME"].ToString();
                        Cad_Sub_TA4.Text = dr["CADRE_SUBCLASS_CD"].ToString() + " - " + dr["CADRE_SUBCLASS_NAME"].ToString();
                        Unit_TA4.Text = dr["HRM_UNIT_CD"].ToString() + " - " + dr["UNIT_NAME"].ToString();
                        Des_TA4.Text = dr["HRM_DESIGNATION_CD"].ToString() + " - " + dr["DESIGNATION_NAME"].ToString();
                        Rep_To_TA4.Text = dr["REP_TO_EMP_CD"].ToString();
                        {
                            if (dr["GENDER"].ToString() == "M")
                            {
                                Gender_TA4.Text = "Male";
                            }

                            else if (dr["GENDER"].ToString() == "F")
                            {
                                Gender_TA4.Text = "Female";
                            }

                            else if (dr["GENDER"].ToString() == "O")
                            {
                                Gender_TA4.Text = "Other";
                            }
                        }
                        Min_Exp_TA4.Text = dr["MIN_EXPERIANCE"].ToString();
                        Min_Age_TA4.Text = dr["MIN_AGE"].ToString();
                        Max_Age_TA4.Text = dr["MAX_AGE"].ToString();
                        string Min_Educ_TA4 = dr["MIN_EDU_ID"].ToString();
                        //TextBox3.Text = dr["APPROVED_BUDGETED_SALARY"].ToString();

                        DataTable dat = new DataTable();
                        string qual_query = "SELECT D.DETAIL_NAME, D.DETAIL_ID FROM HRM_SETUP_DETL D WHERE D.MASTER_CD = 'QUALIFICATION' and D.Detail_Id='" + Min_Educ_TA4 + "' Order By 1";
                        //string query = "SELECT (A.Approve_Str-A.Utilize_Str) AS BALANCE, A.*, A.HOD_APPROVED, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.REG_NAME, hrm_code_desc('EMP_NAME', A.rep_to_emp_cd, NULL, NULL, NULL) AS EMP_NAME, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD = G.CADRE_CD INNER JOIN REGIONS I ON A.REG_CD = I.REG_CD WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                        dat = db.GetData(qual_query);
                        if (dat.Rows.Count > 0)
                        {
                            foreach (DataRow drr in dat.Rows)
                            {
                                Min_Edu_TA4.Text = drr["DETAIL_NAME"].ToString();
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

        public void loadApplicants_TA4(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT B.SELECTED_BY_HOD, B.APPLICANT_ID, B.APPLICANT_NAME, B.FATHER_NAME, B.DOB, B.CNIC, B.QUALIFICATION, B.LAST_SALARY, B.GENDER,  decode (B.ACTION,'I','Interview in Process','S','Initial Shortlisting','R','Interview Re-Schedule','F','Shortlisted','J','Job Offer Placed','H','On-Hold','C','Completed','X','Rejected') Action, B.TRACKING_ID FROM hrm_job_req_tracking A, hrm_job_req_tracking_applicant B WHERE A.JOB_REQ_ID='" + JOB_REQ_ID + "' and A.tracking_id=B.tracking_id";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Applicant_GV_TA4.DataSource = dt;
                    Applicant_GV_TA4.DataBind();
                    Label1_TA4.Text = "Please click on 'view' to process the request.";
                    Label1_TA4.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label1_TA4.Text = "No any pending request.";
                    Label1_TA4.ForeColor = System.Drawing.Color.Red;
                    Applicant_GV_TA4.DataSource = dt;
                    Applicant_GV_TA4.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void lbl_View_CV_TA4(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            if (btn != null)
            {
                GridViewRow row = btn.NamingContainer as GridViewRow;

                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View_Cv_TA4") as LinkButton).CommandArgument;
                    string fileName = GetFileNameFromDatabase_TA4(applicantId);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string filePath = @"D:\CV\" + fileName;
                        //string filePath = @"E:\hr_docs\" + fileName;
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

        protected void lbl_View_Report_TA4(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                GridViewRow row = btn.NamingContainer as GridViewRow;
                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View_Cv_TA4") as LinkButton).CommandArgument;
                    string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0654.rdf&P_APPLICANT_ID=" + applicantId + "&paramform=no";
                    Response.Redirect(reportURL);
                }
            }
        }

        // Method to retrieve file name/path from database based on applicant ID
        public string GetFileNameFromDatabase_TA4(string applicantId)
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

        protected void chkBox_CheckedChanged_TA4(object sender, EventArgs e)
        {
            try
            {
                string isChecked = "";
                CheckBox chkBox = sender as CheckBox;
                GridViewRow row = chkBox.NamingContainer as GridViewRow;
                if (row != null)
                {
                    string applicantId = (row.FindControl("lbl_View_Cv_TA4") as LinkButton).CommandArgument;
                    if (chkBox.Checked)
                    {
                        isChecked = "Y";
                    }
                    else
                    {
                        isChecked = "";
                    }
                    // Update the database based on the checkbox status
                    UpdateDatabaseWithCheckBoxStatus_TA4(applicantId, isChecked);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        


        private void UpdateDatabaseWithCheckBoxStatus_TA4(string applicantId, string isChecked)
        {
            try
            {
                string Query = "UPDATE hrm_job_req_tracking_applicant SET SELECTED_BY_HOD = '" + isChecked + "' WHERE APPLICANT_ID = '" + applicantId + "'";
                string Result = db.PostData(Query);
                if (Result == "Done")
                {
                    if (isChecked == "Y")
                    {
                        EmailIntimationHOD_TA4(applicantId);
                        Session["SelectionMessage"] = "Your success message here";
                        Response.Redirect("HOD_Review.aspx");
                       
                        
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showSelectionToast", "showSelectionToast();", true);
                    }
                    else
                    {
                        EmailIntimationHOD_TA4_NS(applicantId);
                        Session["NotSelectionMessage"] = "Your success message here";
                        Response.Redirect("HOD_Review.aspx");
                        
                        
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showNotSelectionToast", "showNotSelectionToast();", true);
                    }
                    
                }
            }//Session["ErrorMessage"] = "Your success message here";
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void Applicant_GV_TA4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    DropDownList ddlAction = e.Row.FindControl("ddlAction") as DropDownList;
                    if (ddlAction != null)
                    {
                        //// Populate the dropdown list with values from your database or define your own options
                        //ddlAction.Items.Add(new ListItem("Select an option", ""));
                        //ddlAction.Items.Add(new ListItem("Job Offer Placed", "J"));
                        //ddlAction.Items.Add(new ListItem("Initial Shortlisting ", "S"));
                        //ddlAction.Items.Add(new ListItem("Interview", "I"));
                        //ddlAction.Items.Add(new ListItem("Interview Re-Schedule", "R"));
                        //ddlAction.Items.Add(new ListItem("Final Shortlisting", "F"));
                        //ddlAction.Items.Add(new ListItem("On-Hold", "H"));
                        //ddlAction.Items.Add(new ListItem("Completed", "C"));
                        //ddlAction.Items.Add(new ListItem("Canceled", "X"));

                        // Populate the dropdown list with values from your database or define your own options
                        ddlAction.Items.Add(new ListItem("Select an option", ""));
                        ddlAction.Items.Add(new ListItem("Interviewed", "I"));
                        ddlAction.Items.Add(new ListItem("Shortlisted ", "S"));
                        ddlAction.Items.Add(new ListItem("On-Hold", "H"));
                        ddlAction.Items.Add(new ListItem("Rejected", "X"));
                        ddlAction.Items.Add(new ListItem("Job Offer Placed", "J"));


                        // Set the selected value for each row based on the current value in the database
                        string currentActionValue = dr["ACTION"].ToString();
                        ddlAction.SelectedValue = currentActionValue;
                    }

                    CheckBox chkBox = e.Row.FindControl("chkBox_TA4") as CheckBox;
                    
                        string selectedByHOD = dr["SELECTED_BY_HOD"].ToString();
                        if (selectedByHOD == "Y")
                        {
                            chkBox.Checked = true; // Check the checkbox
                        }
                        else
                        {
                            chkBox.Checked = false; // Uncheck the checkbox
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

       
        // Method to update the database
        private void UpdateDDLDatabase_TA4(string selectedValue, string applicantId)
        {
            try
            {
                // Create a SQL query or use stored procedures to update the database
                string Query = "UPDATE hrm_job_req_tracking_applicant SET ACTION ='" + selectedValue + "' WHERE APPLICANT_ID= '" + applicantId + "'";

                string Result = db.PostData(Query);
                if (Result == "Done")
                {
                    gv_TA4PendingRequests.DataSource = null; // Clear the previous data source
                    Applicant_GV_TA4.DataSource = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    loadTA4PendingRequests();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string jobreqid = Req_ID_TA4.Text;
            string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0643.rdf&P_JOB_REQ_ID=" + jobreqid + "&paramform=no";
            Response.Redirect(reportURL);
        }

        public void EmailIntimationHOD_TA4(string applicantId)
        {
            try
            {
                string APPLICANT_NAME = "";
                string CNIC = "";
                string GENDER = "";
                string TRACKING_ID = "";

                DataTable dt = new DataTable();
                string query = "SELECT B.APPLICANT_NAME, B.FATHER_NAME, B.DOB, B.CNIC, B.QUALIFICATION, B.LAST_SALARY, B.GENDER,  decode (B.ACTION,'I','Interview in Process','S','Initial Shortlisting','R','Interview Re-Schedule','F','Shortlisted','J','Job Offer Placed','H','On-Hold','C','Completed','X','Rejected') Action, B.TRACKING_ID FROM hrm_job_req_tracking_applicant B WHERE B.APPLICANT_ID = '" + applicantId + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        APPLICANT_NAME = dr["APPLICANT_NAME"].ToString();
                        CNIC = dr["CNIC"].ToString();
                        GENDER = dr["GENDER"].ToString();
                        TRACKING_ID = dr["TRACKING_ID"].ToString();
                    }
                }
                SendResponse res = new SendResponse();

                string sub = "HOD Selected an Applicant";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "Applicant request has been selected for your working, following are the details. " + "<br /><br />";
                eBody += "Applicant Id : " + applicantId  + "<br />";
                eBody += "Applicant Name : " + APPLICANT_NAME + "<br />";
                eBody += "Applicant CNIC : " + CNIC + "<br />";
                eBody += "Applicant Gender : " + GENDER + "<br />";
                eBody += "Tracking Id : " + TRACKING_ID + "<br />";
                eBody += "Budget Code : " + Bud_Cd_TA4.Text + "<br />";
                eBody += "Job Req. ID : " + Req_ID_TA4.Text + "<br />";
                eBody += "Request Date : " + Req_Date_TA4.Text + "<br />";
                eBody += "Hiring Type : " + Hir_Type_TA4.Text + "<br />";
                eBody += "No. of Vacancies : " + Vac_TA4.Text + "<br />";
                eBody += "Designation : " + Des_TA4.Text + "<br />";
                eBody += "Cadre : " + Cad_TA4.Text + "<br />";
                eBody += "Division : " + Div_TA4.Text + "<br />";
                eBody += "Department : " + Dep_TA4.Text + "<br />";
                eBody += "Section : " + Sec_TA4.Text + "<br /><br />";
                //eBody += "Approved Head Count : " + Approved_TA2.Text + "<br />";
                //eBody += "Current Head Count : " + Current_TA2.Text + "<br />";
                //eBody += "Balance Head Count : " + Balance_TA2.Text + "<br />";
                //eBody += "Remarks : " + Rem_TA2.Text + "<br />";
                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmail_HODTAB(eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public void EmailIntimationHOD_TA4_NS(string applicantId)
        {
            try
            {
                string APPLICANT_NAME = "";
                string CNIC = "";
                string GENDER = "";
                string TRACKING_ID = "";

                DataTable dt = new DataTable();
                string query = "SELECT B.APPLICANT_NAME, B.FATHER_NAME, B.DOB, B.CNIC, B.QUALIFICATION, B.LAST_SALARY, B.GENDER,  decode (B.ACTION,'I','Interview in Process','S','Initial Shortlisting','R','Interview Re-Schedule','F','Shortlisted','J','Job Offer Placed','H','On-Hold','C','Completed','X','Rejected') Action, B.TRACKING_ID FROM hrm_job_req_tracking_applicant B WHERE B.APPLICANT_ID = '" + applicantId + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        APPLICANT_NAME = dr["APPLICANT_NAME"].ToString();
                        CNIC = dr["CNIC"].ToString();
                        GENDER = dr["GENDER"].ToString();
                        TRACKING_ID = dr["TRACKING_ID"].ToString();
                    }
                }
                SendResponse res = new SendResponse();

                string sub = "HOD Rejected an Applicant";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "Applicant request has been rejected, following are the details. " + "<br /><br />";
                eBody += "Applicant Id : " + applicantId + "<br />";
                eBody += "Applicant Name : " + APPLICANT_NAME + "<br />";
                eBody += "Applicant CNIC : " + CNIC + "<br />";
                eBody += "Applicant Gender : " + GENDER + "<br />";
                eBody += "Tracking Id : " + TRACKING_ID + "<br />";
                eBody += "Budget Code : " + Bud_Cd_TA4.Text + "<br />";
                eBody += "Job Req. ID : " + Req_ID_TA4.Text + "<br />";
                eBody += "Request Date : " + Req_Date_TA4.Text + "<br />";
                eBody += "Hiring Type : " + Hir_Type_TA4.Text + "<br />";
                eBody += "No. of Vacancies : " + Vac_TA4.Text + "<br />";
                eBody += "Designation : " + Des_TA4.Text + "<br />";
                eBody += "Cadre : " + Cad_TA4.Text + "<br />";
                eBody += "Division : " + Div_TA4.Text + "<br />";
                eBody += "Department : " + Dep_TA4.Text + "<br />";
                eBody += "Section : " + Sec_TA4.Text + "<br /><br />";
                //eBody += "Approved Head Count : " + Approved_TA2.Text + "<br />";
                //eBody += "Current Head Count : " + Current_TA2.Text + "<br />";
                //eBody += "Balance Head Count : " + Balance_TA2.Text + "<br />";
                //eBody += "Remarks : " + Rem_TA2.Text + "<br />";
                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmail_HODTAB(eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }










        ////////////////////////////////////////               TA5                            //////////////////////////////////////////


        protected void lbl_View_Click_TA5(object sender, EventArgs e)
        {
            try
            {
                TA2_Section.Visible = false;
                TA4_Section.Visible = false;
                TA5_Section.Visible = true;

                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string Applicant_Id = Ids[0].ToString();
                loadInfo_TA5(Applicant_Id);
                DataTable dt = new DataTable();
                string query = "select d.detail_id, d.detail_name from hrm_setup_detl d where d.seq_no=142";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                string evaluationIdQuery = "SELECT EVALUATION_ID FROM HRM_JOB_REQ_EVAL_MST WHERE APPLICANT_ID = '" + Applicant_Id + "'";
                string existingEvaluationId = db.GetSingleValue(evaluationIdQuery);

                if (!string.IsNullOrEmpty(existingEvaluationId))
                {
                    // If evaluation_id exists, populate text boxes
                    PopulateTextBoxesFromDatabase_TA5(Applicant_Id);
                    // Trigger the modal using ScriptManager
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#TA5Modal').modal('show');", true);
                }
                else
                {

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#TA5Modal').modal('show');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void loadInfo_TA5(string Applicant_Id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.*, E.DEPARTMENT_NAME FROM HRM_JOB_REQ_TRACKING_APPLICANT A INNER JOIN HRM_JOB_REQ_TRACKING B ON A.TRACKING_ID= B.TRACKING_ID INNER JOIN HRM_JOB_REQUISITION C ON B.JOB_REQ_ID= C.JOB_REQ_ID INNER JOIN HRM_DEPARTMENT_mst E ON C.HRM_DEPARTMENT_CD = E.DEPARTMENT_CD WHERE A.APPLICANT_ID = '" + Applicant_Id + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    TA5_Section.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        Track_Id_TA5.Text = dr["TRACKING_ID"].ToString();

                        //TextBox1.Text = dr[""].ToString();

                        //TextBox3.Text = dr["JOB_APPLIED_FOR"].ToString();
                        Dep_TA5.Text = dr["DEPARTMENT_NAME"].ToString();
                        Applicant_Name_TA5.Text = dr["APPLICANT_NAME"].ToString();
                        //TextBox6.Text = dr[""].ToString();
                        Father_Name_TA5.Text = dr["FATHER_NAME"].ToString();
                        //TextBox8.Text = dr[""].ToString();
                        DOB_TA5.Text = dr["DOB"].ToString();
                        CNIC_TA5.Text = dr["CNIC"].ToString();
                        Qualification_TA5.Text = dr["QUALIFICATION"].ToString();
                        Last_Sal_TA5.Text = dr["LAST_SALARY"].ToString();
                        Applicant_Id_TA5.Text = dr["APPLICANT_ID"].ToString();
                        Interview_Date_TA5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void btnSubmit_Click_TA5(object sender, EventArgs e)
        {
            try
            {
                string applicantId = Applicant_Id_TA5.Text;
                string evaluationIdQuery = "SELECT EVALUATION_ID FROM HRM_JOB_REQ_EVAL_MST WHERE APPLICANT_ID = '" + applicantId + "'";
                string existingEvaluationId = db.GetSingleValue(evaluationIdQuery);

                if (!string.IsNullOrEmpty(existingEvaluationId))
                {
                    // Update operation since evaluation_id exists for the applicant
                    UpdateEvaluation_TA5(existingEvaluationId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    // Insert operation as evaluation_id doesn't exist for the applicant
                    InsertEvaluation_TA5();
                }
                Response.Redirect("HOD_Review.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void btnViewCv_TA5_Click(object sender, EventArgs e)
        {
            try
            {


                string applicantId = Applicant_Id_TA5.Text;
                string fileName = GetFileNameFromDatabase_TA4(applicantId);

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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void btnreport_TA5_Click(object sender, EventArgs e)
        {
            try
            {
                string applicantId = Applicant_Id_TA5.Text;
                string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0654.rdf&P_APPLICANT_ID=" + applicantId + "&paramform=no";
                Response.Redirect(reportURL);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private void InsertEvaluation_TA5()
        {
            try
            {
                // Generate new evaluation_id
                int newEvaluationId = getevaluationid_TA5();

                // Rest of your code for data retrieval from textboxes
                //PopulateTextBoxesFromDatabase(evaluationId);
                string chk = "";
                string applicantid = Applicant_Id_TA5.Text;
                string trackingid = Track_Id_TA5.Text;

                DateTime interviedatee = Convert.ToDateTime(Interview_Date_TA5.Text);

                string interviedate = interviedatee.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                string applicantname = Applicant_Name_TA5.Text;
                string int1 = Rem_1_TA5.Text;
                string int2 = Rem_2_TA5.Text;
                string int3 = Rem_3_TA5.Text;
                string othbenefits = Ben_TA5.Text;
                string saldecided = Sal_Decided_TA5.Text;
                //string dateojf = DOJ_TA5.Text;
                string shift = Shift_Dtl_TA5.Text;

                DateTime dateojj = Convert.ToDateTime(DOJ_TA5.Text);

                // Convert date to the desired format

                string dateoj = dateojj.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                {

                    if (CheckBox1_TA5.Checked)
                    {
                        chk = "N";
                    }
                    else if (CheckBox2_TA5.Checked)
                    {
                        chk = "Y";
                    }
                    else
                    {
                        chk = "";
                    }

                }

                // Insert into master table
                string insertMasterQuery = "INSERT INTO HRM_JOB_REQ_EVAL_MST (EVALUATION_ID, TRACKING_ID, L$IN_DATE, L$USR_IN, APPLICANT_ID, INTERVIEW_DATE, APPLICANT_NAME, INTERVIEWER_1, INTERVIEWER_2, INTERVIEWER_3,OTHER_BENEFITS, SALARY_DECIDED, DOJ, SHIFT_DETAILS, RECOMMENDATION) VALUES ('" + resultt + "', '" + trackingid + "', TRUNC(SYSDATE), '" + EmployeeUCode + "', '" + applicantid + "', '" + interviedate + "','" + applicantname + "','" + int1 + "','" + int2 + "','" + int3 + "','" + othbenefits + "','" + saldecided + "','" + dateoj + "','" + shift + "', '" + chk + "')";
                string resultMaster = db.PostData(insertMasterQuery);



                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Access each cell in the row and insert data into your table
                    string column1Value = row.Cells[0].Text;
                    string column2Value = row.Cells[1].Text;
                    TextBox textBox = row.Cells[1].FindControl("myTextBox") as TextBox;
                    string column3Value = textBox.Text;

                    // Perform the insertion into your table using SQL commands or your preferred method
                    InsertDataIntoTable_TA5(column1Value, column2Value, column3Value); // Function to insert data
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private void UpdateEvaluation_TA5(string existingEvaluationId)
        {
            try
            {
                // Get data for update
                string trackingid = Track_Id_TA5.Text;
                string applicantname = Applicant_Name_TA5.Text;
                string int1 = Rem_1_TA5.Text;
                string int2 = Rem_2_TA5.Text;
                string int3 = Rem_3_TA5.Text;
                string othbenefits = Ben_TA5.Text;
                string saldecided = Sal_Decided_TA5.Text;

                DateTime dateojj = Convert.ToDateTime(DOJ_TA5.Text);

                // Convert date to the desired format

                string dateoj = dateojj.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                DateTime interviedatee = Convert.ToDateTime(Interview_Date_TA5.Text);

                string interviewdate = interviedatee.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                string shift = Shift_Dtl_TA5.Text;

                string chk = CheckBox1_TA5.Checked ? "N" : (CheckBox2_TA5.Checked ? "Y" : "");

                // Perform the update operation
                string updateQuery = "UPDATE HRM_JOB_REQ_EVAL_MST SET INTERVIEWER_1 = '" + int1 + "', INTERVIEWER_2 = '" + int2 + "', INTERVIEWER_3 = '" + int3 + "', OTHER_BENEFITS = '" + othbenefits + "', SALARY_DECIDED = '" + saldecided + "', DOJ = '" + dateoj + "', SHIFT_DETAILS = '" + shift + "', RECOMMENDATION = '" + chk + "', INTERVIEW_DATE = '" + interviewdate + "' WHERE EVALUATION_ID = '" + existingEvaluationId + "'";

                string updateResult = db.PostData(updateQuery);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Access each cell in the row and insert data into your table
                    string column1Value = row.Cells[0].Text;
                    string column2Value = row.Cells[1].Text;
                    TextBox textBox = row.Cells[1].FindControl("myTextBox") as TextBox;
                    string column3Value = textBox.Text;

                    // Perform the insertion into your table using SQL commands or your preferred method
                    UpdateDataIntoTable_TA5(column1Value, column2Value, column3Value, existingEvaluationId, updateResult); // Function to insert data
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private int getevaluationid_TA5()
        {
            try
            {
                // Generate a new EVALUATION_ID
                int newEvaluationId;
                string getEvaluationIdQuery = "SELECT NVL(MAX(evaluation_id), 0) + 1 FROM HRM_JOB_REQ_EVAL_MST";
                // Assume db is an instance of your database helper class (db.PostData(Query))
                newEvaluationId = int.Parse(db.GetSingleValue(getEvaluationIdQuery));
                resultt = newEvaluationId;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
            return resultt;
        }

        private void UpdateDataIntoTable_TA5(string column1Value, string column2Value, string column3Value, string existingEvaluationId, string updateResult)
        {
            try
            {
                // Assuming existingEvaluationId is already available within this scope
                // Modify this query according to your table structure and requirements
                string updateQuery = "UPDATE HRM_JOB_REQ_EVAL_DTL SET PARAMETER = '" + column2Value + "', SCORE = '" + column3Value + "' WHERE EVALUATION_ID = '" + existingEvaluationId + "' AND PARM_ID = '" + column1Value + "'";

                string updateResultt = db.PostData(updateQuery);

                if (updateResultt == "Done" && updateResult == "Done")
                {
                    EmailIntimationHOD_TA5();
                    Session["SuccessMessage"] = "Your success message here";
                    //GetHrbp_TA5(existingEvaluationId);
                    Response.Redirect("HOD_Review.aspx");
                }
                else
                {
                    Session["ErrorMessage"] = "Your success message here";
                    Response.Redirect("HOD_Review.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private void InsertDataIntoTable_TA5(string column1Value, string column2Value, string column3Value)
        {


            try
            {
                // Insert into detail table with a new DTL_ID for each record linked to EVALUATION_ID
                string insertDetailQuery = "INSERT INTO HRM_JOB_REQ_EVAL_DTL (EVALUATION_ID, DTL_ID, PARM_ID, PARAMETER, SCORE, L$IN_DATE, L$USR_IN) " + " VALUES ('" + resultt + "', (SELECT NVL(MAX(D.DTL_ID),0)+1 FROM HRM_JOB_REQ_EVAL_DTL D) , '" + column1Value + "', '" + column2Value + "' , '" + column3Value + "', TRUNC(SYSDATE), '" + EmployeeUCode + "' )";
                string resultDetail = db.PostData(insertDetailQuery);

                if (resultDetail == "Done")
                {
                    EmailIntimationHOD_TA5_Inserted();
                    Session["SuccessMessage"] = "Your success message here";
                    //GetHrbp_TA5(existingEvaluationId);
                    Response.Redirect("HOD_Review.aspx");
                }
                else
                {
                    Session["ErrorMessage"] = "Your success message here";
                    Response.Redirect("HOD_Review.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void gv_TA5PendingRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    Label label = e.Row.FindControl("lbl") as Label;
                    if (label != null)
                    {
                        string gndr = dr["GENDER"].ToString();
                        if (gndr == "M")
                        {
                            label.Text = "Male";
                        }
                        else if (gndr == "F")
                        {
                            label.Text = "Female";
                        }
                        else
                        {
                            label.Text = "";
                        }
                    }
                }
            }
        }

        private void PopulateTextBoxesFromDatabase_TA5(string Applicant_Id)
        {
            // Construct your SQL query to fetch data based on the EVALUATION_ID
            string selectQuery = "SELECT b.Parm_Id, b.parameter, b.score, a.Evaluation_Id, a.TRACKING_ID, a.APPLICANT_ID, a.INTERVIEW_DATE, a.APPLICANT_NAME, a.INTERVIEWER_1, a.INTERVIEWER_2, a.INTERVIEWER_3, a.OTHER_BENEFITS, a.SALARY_DECIDED, a.DOJ, a.SHIFT_DETAILS, a.RECOMMENDATION FROM HRM_JOB_REQ_EVAL_MST a INNER JOIN HRM_JOB_REQ_EVAL_DTL b ON a.evaluation_id=b.evaluation_id WHERE APPLICANT_ID = '" + Applicant_Id + "'";

            // Execute the query to retrieve data
            DataTable data = db.GetData(selectQuery);

            if (data.Rows.Count > 0)
            {
                Eval_Id_TA5.Text = data.Rows[0]["Evaluation_Id"].ToString();
                Applicant_Id_TA5.Text = data.Rows[0]["APPLICANT_ID"].ToString(); // Populate the Applicant ID textbox
                Track_Id_TA5.Text = data.Rows[0]["TRACKING_ID"].ToString();
                // Set the Interview Date from the database or fallback to current date
                string interviewDate = data.Rows[0]["INTERVIEW_DATE"].ToString();
                if (!string.IsNullOrEmpty(interviewDate))
                {
                    DateTime interviewDateValue;
                    bool isValidDate = DateTime.TryParse(interviewDate, out interviewDateValue);

                    if (isValidDate)
                    {
                        Interview_Date_TA5.Text = interviewDateValue.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // Handle invalid date format by setting current date
                        Interview_Date_TA5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    // Handle case where the date string is empty or null
                    Interview_Date_TA5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                Applicant_Name_TA5.Text = data.Rows[0]["APPLICANT_NAME"].ToString();
                Rem_1_TA5.Text = data.Rows[0]["INTERVIEWER_1"].ToString();
                Rem_2_TA5.Text = data.Rows[0]["INTERVIEWER_2"].ToString();
                Rem_3_TA5.Text = data.Rows[0]["INTERVIEWER_3"].ToString();
                Ben_TA5.Text = data.Rows[0]["OTHER_BENEFITS"].ToString();
                Sal_Decided_TA5.Text = data.Rows[0]["SALARY_DECIDED"].ToString();
               

                // Date of Joining field
                string dateofjoin = data.Rows[0]["DOJ"].ToString();

                if (!string.IsNullOrEmpty(dateofjoin))
                {
                    DateTime dojDate;
                    bool isValidDate = DateTime.TryParse(dateofjoin, out dojDate);

                    if (isValidDate)
                    {
                        DOJ_TA5.Text = dojDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // Handle invalid date format
                        DOJ_TA5.Text = ""; // Or handle appropriately
                    }
                }
                else
                {
                    // Handle case where the date string is empty or null
                    DOJ_TA5.Text = ""; // Or handle appropriately
                }

                Shift_Dtl_TA5.Text = data.Rows[0]["SHIFT_DETAILS"].ToString();

                string recommendation = data.Rows[0]["RECOMMENDATION"].ToString();
                if (recommendation == "N")
                {
                    CheckBox1_TA5.Checked = true;
                }
                else if (recommendation == "Y")
                {
                    CheckBox2_TA5.Checked = true;
                }

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Access each cell in the row
                    string column1Value = row.Cells[0].Text;
                    string column2Value = row.Cells[1].Text;

                    // Find the corresponding row in the fetched data
                    DataRow[] foundRows = data.Select("PARM_ID = '" + column1Value + "'");

                    if (foundRows.Length > 0)
                    {
                        TextBox textBox = row.Cells[1].FindControl("myTextBox") as TextBox;
                        textBox.Text = foundRows[0]["SCORE"].ToString();
                    }
                }

                // Initialize total score
                int totalScore = 0;

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Access each cell in the row
                    string column1Value = row.Cells[0].Text;

                    // Find the corresponding row in the fetched data
                    DataRow[] foundRows = data.Select("Parm_Id = '" + column1Value + "'");

                    if (foundRows.Length > 0)
                    {
                        TextBox textBox = row.Cells[1].FindControl("myTextBox") as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = foundRows[0]["score"].ToString();
                            int score;
                            if (int.TryParse(textBox.Text, out score))
                            {
                                totalScore += score;
                            }
                        }
                    }
                }

                // Update the total score label
                TotalScoreLabel.Text = "Total Score: " + totalScore + "/65";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
            }
        }


        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            int totalScore = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox textBox = row.FindControl("myTextBox") as TextBox;
                if (textBox != null)
                {
                    int score;
                    if (int.TryParse(textBox.Text, out score))
                    {
                        totalScore += score;
                    }
                }
            }

            TotalScoreLabel.Text = "Total Score: " + totalScore + "/65";
        }


        public void EmailIntimationHOD_TA5()
        {
            try
            {

                SendResponse res = new SendResponse();

                string sub = "HOD Updated an Evaluation Id: '" + Eval_Id_TA5.Text + "'";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "Evaluation request has been updated by HOD. Following are the details. " + "<br /><br />";
                //eBody += "Budget Code : " + Qualification_TA5 + "<br />";
                eBody += "Evaluation Id : " + Eval_Id_TA5.Text + "<br />";
                eBody += "Tracking Id : " + Track_Id_TA5.Text + "<br />";
                eBody += "Applicant Id : " + Applicant_Id_TA5.Text + "<br />";
                eBody += "Applicant Name : " + Applicant_Name_TA5.Text + "<br />";
                eBody += "Father Name : " + Father_Name_TA5.Text + "<br />";
                eBody += "Date of Birth : " + DOB_TA5.Text + "<br />";
                eBody += "CNIC : " + CNIC_TA5.Text + "<br />";
                eBody += "Department : " + Dep_TA5.Text + "<br />";
                eBody += "Qualification : " + Qualification_TA5.Text + "<br />";
                eBody += "Interview Date : " + Interview_Date_TA5.Text + "<br /><br />";

                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmail_HODTAB(eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public void EmailIntimationHOD_TA5_Inserted()
        {
            try
            {

                SendResponse res = new SendResponse();

                string sub = "HOD Inserted New Evaluation Id: '" + Eval_Id_TA5.Text + "'";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "New Evaluation request has been inserted by HOD. Following are the details. " + "<br /><br />";
                //eBody += "Budget Code : " + Qualification_TA5 + "<br />";
                eBody += "Evaluation Id : " + Eval_Id_TA5.Text + "<br />";
                eBody += "Tracking Id : " + Track_Id_TA5.Text + "<br />";
                eBody += "Applicant Id : " + Applicant_Id_TA5.Text + "<br />";
                eBody += "Applicant Name : " + Applicant_Name_TA5.Text + "<br />";
                eBody += "Father Name : " + Father_Name_TA5.Text + "<br />";
                eBody += "Date of Birth : " + DOB_TA5.Text + "<br />";
                eBody += "CNIC : " + CNIC_TA5.Text + "<br />";
                eBody += "Department : " + Dep_TA5.Text + "<br />";
                eBody += "Qualification : " + Qualification_TA5.Text + "<br />";
                eBody += "Interview Date : " + Interview_Date_TA5.Text + "<br /><br />";

                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmail_HODTAB(eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }



    }
}