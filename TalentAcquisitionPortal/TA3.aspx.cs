using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text;


namespace TalentAcquisitionPortal
{
    public partial class TA3 : System.Web.UI.Page
    {
        string EmployeeCode;
        string EmployeeUCode;
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {


            getSession();

            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                div_Details.Visible = false;
                cvUpload.Visible = false;
                loadPendingRequests();

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
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        public string getWorkingRights()
        {
            string result;
            DataTable dt = new DataTable();
            string query = "SELECT D.Detail_Name FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 141 and d.detail_name in (select emp_cd from users s where s.usr_cd ='" + EmployeeUCode + "')";
            dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                result = "Y";
            }
            else
            {
                result = "N";
            }
            return result;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string JOB_REQ_ID = txtSearch.Text.Trim();

                DataTable dt = new DataTable();
                string rights = getWorkingRights();
                if (rights == "Y")
                {
                    string query = "select (select t.tracking_id from HRM_JOB_REQ_TRACKING t WHERE t.job_req_id=e.job_req_id and  NVL(t.tracking_status, 'N') !='C') AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HR_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y'  AND JOB_REQ_ID='" + JOB_REQ_ID + "' and NVL(e.STATUS, 'N') != 'C' and e.hrm_cadre_cd  in ('12','13') order by TRACKING_ID desc , e.job_req_id DESC";

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
                else if (rights == "N")
                {
                    string query = "select CASE WHEN (SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id and NVL(t.tracking_status, 'N') !='C') IS NULL THEN 'Pending' ELSE TO_CHAR((SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id)) END AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HR_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y' and NVL(e.STATUS, 'N') != 'C' AND JOB_REQ_ID='" + JOB_REQ_ID + "' and e.hrm_cadre_cd  NOT in ('13') order by TRACKING_ID desc , e.job_req_id DESC";

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

        string getroll()
        {
            string roll = "";
            string finalroll = "";
            try
            {
                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                dt = null;

                string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_CONFIRM_RIGHTS_VIEW T WHERE T.EMP_CD= '" + EmployeeCode + "'";
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


      
        public void loadPendingRequests()
        {
            try
            {
                 string roll = getroll();
                 if (roll == "SUPER")
                 {
                     DataTable dat = new DataTable();
                     string super_query = "select e.STATUS, (select t.tracking_id from HRM_JOB_REQ_TRACKING t WHERE t.job_req_id=e.job_req_id and NVL(t.tracking_status, 'N') !='C') AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HOD_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y' and NVL(e.STATUS, 'N') != 'C' order by TRACKING_ID desc , e.job_req_id DESC";
                     dat = db.GetData(super_query);
                     if (dat.Rows.Count > 0)
                     {
                         gv_PendingRequests.DataSource = dat;
                         gv_PendingRequests.DataBind();
                         lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                         lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                         //EmailIntimationHOD("muhammad.mubbashir@alkaram.com", "Mubbashir", "955", "781");
                     }
                     else
                     {
                         lbl_GridMsg.Text = "No any pending request.";
                         lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                         gv_PendingRequests.DataSource = dat;
                         gv_PendingRequests.DataBind();

                     }
                 }
                 else
                 {
                     string query = "";
                     string rights = getWorkingRights();
                     DataTable dt = new DataTable();
                     if (rights == "Y")
                     {
                         query = "select e.STATUS, (select t.tracking_id from HRM_JOB_REQ_TRACKING t WHERE t.job_req_id=e.job_req_id and NVL(t.tracking_status, 'N') !='C') AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HOD_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y' and NVL(e.STATUS, 'N') != 'C' and e.hrm_cadre_cd  in ('12','13') order by TRACKING_ID desc , e.job_req_id DESC";
                     }
                     else if (rights == "N")
                     {
                         query = "select CASE WHEN (SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id and NVL(t.tracking_status, 'N') !='C') IS NULL THEN 'Pending' ELSE TO_CHAR((SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id)) END AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HOD_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y' and NVL(e.STATUS, 'N') != 'C' and e.hrm_cadre_cd  NOT in ('13') order by TRACKING_ID desc , e.job_req_id DESC";
                     }

                     dt = db.GetData(query);
                     if (dt.Rows.Count > 0)
                     {
                         gv_PendingRequests.DataSource = dt;
                         gv_PendingRequests.DataBind();
                         lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                         lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                         //EmailIntimationHOD("muhammad.mubbashir@alkaram.com", "Mubbashir", "955", "781");
                     }
                     else
                     {
                         lbl_GridMsg.Text = "No any pending request.";
                         lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                         gv_PendingRequests.DataSource = dt;
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
                //loadApplicants(JOB_REQ_ID);
                cvUpload.Visible = true;
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
                jobReqId.Text = JOB_REQ_ID;
                string trackingid = "";
                string trackingstatus = "";
                DataTable dt = new DataTable();
                string query = "SELECT * FROM HRM_JOB_REQ_TRACKING T WHERE T.Job_Req_Id='" + JOB_REQ_ID + "' ";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dt.Rows)
                    {
                        trackingid = drr["Tracking_id"].ToString();
                        trackingstatus = drr["Tracking_status"].ToString();
                    }

                    if (trackingstatus == "C")
                    {
                        chkClose.Checked = true;
                    }

                    else if (trackingstatus == "O")
                    {
                        chkOpen.Checked = true;
                    }

                    DataTable dtt = new DataTable();
                    string queryy = " SELECT a.Tracking_Id, A.APPLICANT_ID, A.APPLICANT_NAME, A.CNIC,  decode(A.GENDER,'M','Male','F','Female','O','Other') GENDER, decode(A.Action,'I','Interview in Process','S','Initial Shortlisted','R','Interview Re-Schedule','F','Shortlisted','J','Job Offer Placed','H','Hold','C','Completed','X','Rejected') Action, A.ACTION_DATE, DECODE(A.Selected_By_Hod,'N','No','Y','Yes') Selected_By_Hod FROM Hrm_Job_Req_Tracking_Applicant A WHERE A.TRACKING_ID='" + trackingid + "' ";
                    dtt = db.GetData(queryy);
                    if (dtt.Rows.Count > 0)
                    {
                        appgrid.DataSource = dtt;
                        appgrid.DataBind();

                        Label1.Text = "Please click on 'view' to process the request.";
                        Label1.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    chkOpen.Checked = true;
                    chkClose.Checked = false;
                    Label1.Text = "No any pending request.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    appgrid.DataSource = dt;
                    appgrid.DataBind();
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }
        
        protected void lbl_View_Click1(object sender, EventArgs e)
        {
            try
            {
                TA4 ta = new TA4();

                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string Applicant_Id = Ids[0].ToString();

                string fileName = ta.GetFileNameFromDatabase(Applicant_Id);

                if (!string.IsNullOrEmpty(fileName))
                {
                    string filePath = @"D:\CV\" + fileName;

                    FileInfo fileInfo = new FileInfo(filePath);

                    if (fileInfo.Exists)
                    {
                        string fileExtension = fileInfo.Extension.ToLower();
                        string contentType = GetContentType(fileExtension);

                        if (!string.IsNullOrEmpty(contentType))
                        {
                            Response.Clear();
                            Response.ContentType = contentType;
                            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
                            Response.TransmitFile(filePath);
                            Response.End();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showUnsupportedFileToast", "showUnsupportedFileToast();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showNoCvToast", "showNoCvToast();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showNoCvToast", "showNoCvToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private string GetContentType(string fileExtension)
{
    switch (fileExtension)
    {
        case ".pdf":
            return "application/pdf";
        case ".doc":
            return "application/msword";
        case ".docx":
            return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        case ".jpg":
        case ".jpeg":
            return "image/jpeg";
        case ".png":
            return "image/png";
        case ".gif":
            return "image/gif";
        case ".txt":
            return "text/plain";
        // Add other file types here as needed
        default:
            return string.Empty;
    }
}


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the DropDownList control in the row.
                DropDownList ddlAction = (DropDownList)e.Row.FindControl("ddlAction");

                // Get the data item for the row.
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (rowView != null)
                {
                    // Get the value of the "ACTION" column from the data item.
                    string actionValue = rowView["ACTION"].ToString();

                    // Select the corresponding item in the dropdown list.
                    ListItem selectedItem = ddlAction.Items.FindByText(actionValue);
                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
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
                UpdateActionInDatabase(selectedValue, applicantId);
            }
        }



        private void UpdateActionInDatabase(string newActionValue, string APPLICANT_ID)
        {
            try
            {
                string dateColumn = "";

                switch (newActionValue)
                {
                    case "I":
                        dateColumn = "INTERVIEW_PROCESS_DATE";
                        break;
                    case "F":
                        dateColumn = "SHORT_LIST_DATE";
                        break;
                    case "J":
                        dateColumn = "JOBOFFER_DATE";
                        break;
                    case "H":
                        dateColumn = "HOLD_DATE";
                        break;
                    case "X":
                        dateColumn = "REJECT_DATE";
                        break;
                }

                string query = "UPDATE Hrm_Job_Req_Tracking_Applicant SET ACTION = '" + newActionValue + "', L$USR_UP= '" + EmployeeUCode + "', L$UP_DATE= Trunc(SYSDATE), ACTION_DATE= Trunc(SYSDATE), "+ dateColumn +" = TRUNC(SYSDATE) WHERE APPLICANT_ID = '" + APPLICANT_ID + "' ";
                string updateAction = db.PostData(query);
                if (updateAction == "Done")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showUpdateToast", "showUpdateToast();", true);
                }

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message).
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void GetHod(string JOB_REQ_ID, string Applicantid)
        {
            try
            {
                DataTable dt = new DataTable();
                string getHrEmail = "select B.Emp_Hod, e.e_mail, e.emp_name from Hrm_Department_Budget B, Hrm_Job_Requisition R, Hrm_Employee E WHERE b.reg_cd=r.reg_cd AND b.hrm_division_cd=r.hrm_division_cd AND b.hrm_unit_cd=r.hrm_unit_cd AND b.hrm_department_cd=r.hrm_department_cd AND r.job_req_id='" + JOB_REQ_ID + "' AND e.emp_cd=b.emp_hod";

                dt = db.GetData(getHrEmail);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string hodname = dr["emp_name"].ToString();
                        string hodemail = dr["e_mail"].ToString();
                        if (hodemail != "")
                        {
                            EmailIntimationHOD(hodemail, hodname, JOB_REQ_ID, Applicantid);
                        }
                    }
                }
            }

            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public DataTable GetData(string job_req_id, string applicant_id)
        {
            DataTable dt = new DataTable();
            string getData = "SELECT T.Tracking_Id, Trunc(T.L$IN_DATE) REQ_DATE, decode(t.Tracking_Status,'O','Open','C','Closed') Tracking_Status, a.applicant_id, a.applicant_name, decode (A.Action,'I','Interview','S','Initial Shortlisting','R','Interview Re-Schedule','F','Final Shortlisting','J','Job Offer Placed','H','On-Hold','C','Completed','X','Canceled') Action, A.Action_Date, hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.BUDGET_REF_NO, E.Reg_Cd, E.Hrm_Cadre_Cd, E.Hrm_Department_Cd, E.Hrm_Designation_Cd, e.hrm_division_cd, e.hrm_section_cd, e.hrm_unit_cd, e.hiring_type, e.vacancies, e.remarks FROM HRM_JOB_REQUISITION E, HRM_JOB_REQ_TRACKING T, Hrm_Job_Req_Tracking_Applicant A WHERE e.job_req_id=t.job_req_id AND a.tracking_id=t.tracking_id AND a.applicant_id=" + applicant_id + " and E.Job_Req_Id=" + job_req_id + "";
            dt = db.GetData(getData);
            return dt;
        }


        public void EmailIntimationHOD(string hodemail, string hodname, string JOB_REQ_ID, string applicant_id)
        {
            try
            {
                DataTable data = GetData(JOB_REQ_ID, applicant_id);

                // Check if data is available
                if (data.Rows.Count > 0)
                {
                    // Loop through each row in the DataTable
                    foreach (DataRow dr in data.Rows)
                    {
                        // Extract data from the DataRow
                        string BUDGET_REF_NO = dr["BUDGET_REF_NO"].ToString();
                        string CADRE_NAME = dr["CADRE_NAME"].ToString();
                        string DESIGNATION_NAME = dr["DESIGNATION_NAME"].ToString();
                        string DIVISION_NAME = dr["DIVISION_NAME"].ToString();
                        string UNIT_NAME = dr["UNIT_NAME"].ToString();
                        string SECTION_NAME = dr["SECTION_NAME"].ToString();
                        string DEPARTMENT_NAME = dr["DEPARTMENT_NAME"].ToString();
                        string HIRING_TYPE = dr["HIRING_TYPE"].ToString();
                        string REMARKS = dr["REMARKS"].ToString();
                        string HEAD_COUNT = dr["VACANCIES"].ToString();
                        string Tracking_Id = dr["Tracking_Id"].ToString();
                        string Action = dr["Action"].ToString();
                        string Action_Date = dr["Action_Date"].ToString();
                        string Tracking_Status = dr["Tracking_Status"].ToString();
                        string REQ_DATE = dr["REQ_DATE"].ToString();

                        string HRM_CADRE_CD = dr["HRM_CADRE_CD"].ToString();
                        string HRM_DESIGNATION_CD = dr["HRM_DESIGNATION_CD"].ToString();
                        string HRM_DIVISION_CD = dr["HRM_DIVISION_CD"].ToString();
                        string HRM_UNIT_CD = dr["HRM_UNIT_CD"].ToString();
                        string HRM_SECTION_CD = dr["HRM_SECTION_CD"].ToString();
                        string HRM_DEPARTMENT_CD = dr["HRM_DEPARTMENT_CD"].ToString();

                        SendResponse res = new SendResponse();
                        string sub = "HR Completed Tracking Against Requisition";
                        string eBody = string.Empty;

                        // Email Body
                        eBody += "Dear " + ToTitleCase(hodname) + ",<br><br>";
                        eBody += "Tracking has been completed by HR Department. Now you can check tracking and can perform online Evaluation using Form No.HRM0653. <br>";
                        eBody += "Following are the details:<br><br>";
                        eBody += "Budget Ref #           : " + BUDGET_REF_NO + "<br>";
                        eBody += "Requisition ID        : " + JOB_REQ_ID + "<br>";
                        eBody += "Tracking ID           : " + Tracking_Id + "<br>";
                        eBody += "Designation           : " + HRM_DESIGNATION_CD + " - " + DESIGNATION_NAME + "<br>";
                        eBody += "Cadre                   : " + HRM_CADRE_CD + " - " + CADRE_NAME + "<br>";
                        eBody += "Division              : " + HRM_DIVISION_CD + " - " + DIVISION_NAME + "<br>";
                        eBody += "Location               : " + HRM_UNIT_CD + " - " + UNIT_NAME + "<br>";
                        eBody += "Depatrment             : " + HRM_DEPARTMENT_CD + " - " + DEPARTMENT_NAME + "<br>";
                        eBody += "Section                 : " + HRM_SECTION_CD + " - " + SECTION_NAME + "<br>";
                        eBody += "Head Count             : " + HEAD_COUNT + "<br>";
                        eBody += "Hiring Type            : " + HIRING_TYPE + "<br>";
                        eBody += "Request Status         : " + Tracking_Status + "<br>";
                        eBody += "Remarks                 : " + REMARKS + "<br>";
                        eBody += "Start Date  : " + REQ_DATE + "<br><br>";
                        eBody += "Action  : " + Action + ", Action Date  : " + Action_Date + "<br><br>";

                        eBody += "This is a system generated e-mail, please do not reply.";

                        // Send Email
                        res.SendEmailTA2(hodemail, eBody, sub);
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public static string ToTitleCase(string str)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }

       
        private void CheckVisitorPermissions(string visitorRegisterNicNo)
        {
            // Define queries
            string blacklistQuery = " SELECT z.EMP_CD AS BLACKLIST_EMP_CODE FROM HRM_EMPLOYEE z WHERE CNIC_NO = '" + visitorRegisterNicNo + "' AND BLACK_LIST = 'Y' AND NVL(VISIT_ALLOW, 'N') = 'N'";
    
            string bandQuery = "SELECT EMP_CD AS BAND_EMP_CODE FROM HRM_EMPLOYEE WHERE CNIC_NO = '" + visitorRegisterNicNo + "' AND IS_BAND = 'Y' AND NVL(VISIT_ALLOW, 'N') = 'N'";

            string govBlacklistQuery = "SELECT CNIC_NO AS GOV_BLACKLIST FROM hr_emp_gov_blacklist WHERE cnic_no = '" + visitorRegisterNicNo + "' AND NVL(VISIT_ALLOW, 'N') = 'N'";

            //string empAllowByQuery = "SELECT ALLOW_BY AS EMP_ALLOW_BY FROM HRM_EMPLOYEE WHERE CNIC_NO = '" + visitorRegisterNicNo + "' AND NVL(VISIT_ALLOW, 'N') = 'Y' AND ROWNUM = 1";

            //string govAllowByQuery = "SELECT ALLOW_BY AS GOV_ALLOW_BY FROM hrm_emp_gov_blacklist WHERE cnic_no = '" + visitorRegisterNicNo + "' AND NVL(VISIT_ALLOW, 'N') = 'Y' AND ROWNUM = 1";

            //string empDetailsQuery = "SELECT E.EMP_CD AS EMP_DETAILS, DECODE(E.EMP_STATUS, 'A', 'ACTIVE', 'I', 'INACTIVE', 'S', 'TEMP.INACTIVE', 'R', 'SURPLUS') AS INFO_EMP_STATUS_NAME FROM HRM_EMPLOYEE E WHERE E.CNIC_NO = '" + visitorRegisterNicNo + "' AND E.EMP_CD = (SELECT MAX(M.EMP_CD) FROM HRM_EMPLOYEE M WHERE E.CNIC_NO = M.CNIC_NO)";

            // Execute queries and handle results
            CheckBlacklist(blacklistQuery);
            CheckBandStatus(bandQuery);
            CheckGovBlacklist(govBlacklistQuery);
            //CheckAllowBy(empAllowByQuery, govAllowByQuery);
            //SetEmployeeDetails(empDetailsQuery);
        }

        private void CheckBlacklist(string query)
        {
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0 && !DBNull.Value.Equals(dt.Rows[0]["BLACKLIST_EMP_CODE"]))
            {
                var a = dt.Rows[0]["BLACKLIST_EMP_CODE"];
                throw new Exception("Person is banned. Please coordinate with HR department for further inquiry.");
            }
        }

        private void CheckBandStatus(string query)
        {
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0 && !DBNull.Value.Equals(dt.Rows[0]["BAND_EMP_CODE"]))
            {
                 throw new Exception("Person is banned. Please coordinate with HR department for further inquiry.");
            }
        }

        private void CheckGovBlacklist(string query)
        {
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0 && !DBNull.Value.Equals(dt.Rows[0]["GOV_BLACKLIST"]))
            {
                throw new Exception("This person was banned by the government. Please coordinate with HR department for further inquiry.");
            }
        }

        private void CheckAllowBy(string empQuery, string govQuery)
        {
            string BAND_ALLOW_BY;
            DataTable dtEmp = db.GetData(empQuery);
            if (dtEmp.Rows.Count > 0 && !DBNull.Value.Equals(dtEmp.Rows[0]["EMP_ALLOW_BY"]))
            {
                BAND_ALLOW_BY = dtEmp.Rows[0]["EMP_ALLOW_BY"].ToString();
            }
            else
            {
                DataTable dtGov = db.GetData(govQuery);
                if (dtGov.Rows.Count > 0 && !DBNull.Value.Equals(dtGov.Rows[0]["GOV_ALLOW_BY"]))
                {
                    BAND_ALLOW_BY = dtGov.Rows[0]["GOV_ALLOW_BY"].ToString();
                }
            }
        }

        private void SetEmployeeDetails(string query)
        {
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                string F_EMP_CD, STATUS_NAME;
                DataRow dr = dt.Rows[0];
                if (!DBNull.Value.Equals(dr["EMP_DETAILS"]))
                {
                    F_EMP_CD = dr["EMP_DETAILS"].ToString();
                    STATUS_NAME = dr["INFO_EMP_STATUS_NAME"].ToString();
                }
            }
            else
            {
                throw new Exception("No data found for the provided CNIC.");
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string JOB_REQ_ID = jobReqId.Text;

            try
    {
            CheckVisitorPermissions(cnic.Text);

            // Check if a file is uploaded
            if (fileCV.PostedFile != null && fileCV.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileCV.PostedFile.FileName);
                string fileType = Path.GetExtension(fileCV.PostedFile.FileName);
                string filePath = @"D:\CV\" + fileName;
                //string filePath = @"E:\hr_docs\" + fileName;


                // Check if the file already exists on the server
                if (File.Exists(filePath))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showCvExistToast", "showCvExistToast();", true);
                    return;
                }
                else
                {


                    // Get tracking id
                    string trackingid = GetTrackingId(JOB_REQ_ID);
                    if (string.IsNullOrEmpty(trackingid))
                    {
                        // If no tracking id found, insert a new one
                        string insertInTracking = "INSERT INTO HRM_JOB_REQ_TRACKING (TRACKING_ID, JOB_REQ_ID, L$USR_IN, L$IN_DATE) SELECT (SELECT nvl(max(T.TRACKING_ID),0)+1 FROM HRM_JOB_REQ_TRACKING T), " + JOB_REQ_ID + ", " + EmployeeUCode + ", trunc(sysdate) FROM HRM_JOB_REQUISITION R WHERE R.Job_Req_Id=" + JOB_REQ_ID + " ";
                        string insertTracking = db.PostData(insertInTracking);
                        if (insertTracking != "Done")
                        {
                            // Handle insertion failure
                            ScriptManager.RegisterStartupScript(this, GetType(), "FailTrackingToast", "FailTrackingToast();", true);
                            return;
                        }
                        
                       
                        // Retrieve the newly inserted tracking ID
                        trackingid = GetTrackingId(JOB_REQ_ID);
                    }


                    // Insert new applicant record
                    string Applicantid = InsertApplicant(trackingid);
                    if (string.IsNullOrEmpty(Applicantid))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "FailApplicantToast", "FailApplicantToast();", true);
                        return;
                    }

                    // Insert CV details
                    string result = InsertCVDetails(Applicantid, fileName, fileType);
                    if (result != "Done")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "FailCVDetailsToast", "FailCVDetailsToast();", true);
                        return;
                    }

                    // Save the file to the server
                    fileCV.PostedFile.SaveAs(filePath);

                    //For Email
                    GetHod(JOB_REQ_ID, Applicantid);
                    loadInfo(JOB_REQ_ID);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showInfoToast", "showInfoToast();", true);
            }

            txtName.Text = string.Empty;
            cnic.Text = string.Empty;
            DropdownAction.SelectedValue = "0";
            DropDownGender.SelectedValue = "0";

        }
             catch (Exception ex)
            {
                // Escape single quotes for JavaScript
                string message = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showBlacklistAlert", "alert('"+message+"');", true);
            }
        }



        // Function to get tracking ID
        private string GetTrackingId(string JOB_REQ_ID)
        {
            string query = "SELECT Tracking_id FROM HRM_JOB_REQ_TRACKING WHERE Job_Req_Id='" + JOB_REQ_ID + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Tracking_id"].ToString();
            }
            return null;
        }

        // Function to insert new applicant
        private string InsertApplicant(string trackingid)
        {
            string query = "SELECT NVL(MAX(APPLICANT_ID), 0) + 1 AS APPLICANT_ID FROM HRM_JOB_REQ_TRACKING_APPLICANT";
            string Applicantid = db.GetSingleValue(query);
            string insertQuery = "INSERT INTO HRM_JOB_REQ_TRACKING_APPLICANT (TRACKING_ID, APPLICANT_ID, APPLICANT_NAME, CNIC, L$USR_IN, L$IN_DATE, ACTION, ACTION_DATE, GENDER, LAST_SALARY, REMARKS) VALUES (" + trackingid + ", '" + Applicantid + "',  '" + txtName.Text + "', '" + cnic.Text + "' , '" + EmployeeUCode + "' , TRUNC(SYSDATE), '" + DropdownAction.SelectedItem.Value + "' , TRUNC(SYSDATE), '" + DropDownGender.SelectedItem.Value + "', '" + lastSalary.Text + "', '" + HR_REMARKS.Text + "')";
            string result = db.PostData(insertQuery);
            return result == "Done" ? Applicantid : null;
        }

        // Function to insert CV details
        private string InsertCVDetails(string Applicantid, string fileName, string fileType)
        {
            string query = "INSERT INTO HRM_JOB_REQ_APPLICANT_ATTACH (APPLICANT_ID, FILENAME, Filetype, ATTACHMENTID) VALUES ('" + Applicantid + "', '" + fileName + "', '" + fileType + "', (SELECT nvl(max(T.ATTACHMENTID),0)+1 AS ATTACHMENTID FROM HRM_JOB_REQ_APPLICANT_ATTACH T ))";
            return db.PostData(query);

        }





        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string trackingStatus = "";

                // Determine which radio button was checked
                if (chkOpen.Checked)
                {
                    trackingStatus = "O"; // Open
                }
                else if (chkClose.Checked)
                {
                    trackingStatus = "C"; // Close
                }

                string selectquery = "SELECT * FROM HRM_JOB_REQ_TRACKING WHERE Job_Req_Id = '" + jobReqId.Text + "'";
                dt = db.GetData(selectquery);

                if (dt.Rows.Count > 0)
                {
                    // Update the database with the new tracking status
                    string query = "UPDATE HRM_JOB_REQ_TRACKING SET Tracking_status = '" + trackingStatus + "', L$USR_UP ='" + EmployeeUCode + "', L$UP_DATE= Trunc(SYSDATE), CLOSING_DATE= Trunc(SYSDATE) WHERE Job_Req_Id = '" + jobReqId.Text + "'";
                    string updateAction = db.PostData(query);
                    if (updateAction == "Done")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showUpdateToast", "showUpdateToast();", true);

                        string script = "showUpdateToast();window.location = 'TA3.aspx';";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showToastAndRedirect", script, true);
                    }
                }
                else
                {
                    string script = "alert('You need to add CV first');setTimeout(function() {window.location = 'TA3.aspx';}, 0);";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertAndRedirect", script, true);
                    return;  // Ensure no further code execution
                }
            }
            catch (ThreadAbortException)
            {
                // Do nothing, this is expected due to Response.Redirect
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                var status = 'C';
                string insertQuery = "UPDATE HRM_JOB_REQUISITION R SET R.STATUS ='"+status+"', R.CLOSING_DATE= TRUNC(SYSDATE), R.HR_COMMENTS='" + HR_REMARKS.Text + "' WHERE R.Job_Req_Id=" + jobReqId.Text + "";
                string result = db.PostData(insertQuery);
                if (result != "Done")
                {
                    Session["ErrorMessage"] = "Your success message here";
                    Response.Redirect("HOD_Review.aspx");
                    return;
                }
                else
                {
                    GetHod_Reject_TA3(jobReqId.Text);
                    Session["InfoMessage"] = "Your success message here";
                    Response.Redirect("TA3.aspx");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
                Console.WriteLine(ex.Message);
            }
        }

       

        public void EmailIntimationHOD_Reject_TA3(string hodemail, string hodname, string JOB_REQ_ID)
        {
            try
            {
                DataTable data = GetData_Reject_TA3(JOB_REQ_ID);

                // Check if data is available
                if (data.Rows.Count > 0)
                {
                    // Loop through each row in the DataTable
                    foreach (DataRow dr in data.Rows)
                    {
                        // Extract data from the DataRow
                        string BUDGET_REF_NO = dr["BUDGET_REF_NO"].ToString();
                        string CADRE_NAME = dr["CADRE_NAME"].ToString();
                        string DESIGNATION_NAME = dr["DESIGNATION_NAME"].ToString();
                        string DIVISION_NAME = dr["DIVISION_NAME"].ToString();
                        string UNIT_NAME = dr["UNIT_NAME"].ToString();
                        string SECTION_NAME = dr["SECTION_NAME"].ToString();
                        string DEPARTMENT_NAME = dr["DEPARTMENT_NAME"].ToString();
                        string HIRING_TYPE = dr["HIRING_TYPE"].ToString();
                        string REMARKS = dr["hr_COMMENTS"].ToString();
                        string HEAD_COUNT = dr["VACANCIES"].ToString();
                        //string Tracking_Id = dr["Tracking_Id"].ToString();
                        //string Action = dr["Action"].ToString();
                        ////string Action_Date = dr["Action_Date"].ToString();
                        //string Tracking_Status = dr["Tracking_Status"].ToString();
                        //string REQ_DATE = dr["REQ_DATE"].ToString();

                        string HRM_CADRE_CD = dr["HRM_CADRE_CD"].ToString();
                        string HRM_DESIGNATION_CD = dr["HRM_DESIGNATION_CD"].ToString();
                        string HRM_DIVISION_CD = dr["HRM_DIVISION_CD"].ToString();
                        string HRM_UNIT_CD = dr["HRM_UNIT_CD"].ToString();
                        string HRM_SECTION_CD = dr["HRM_SECTION_CD"].ToString();
                        string HRM_DEPARTMENT_CD = dr["HRM_DEPARTMENT_CD"].ToString();

                        SendResponse res = new SendResponse();
                        string sub = "HR Rejected Requisition";
                        string eBody = string.Empty;

                        // Email Body
                        eBody += "Dear " + ToTitleCase(hodname) + ",<br><br>";
                        eBody += "Your requisition has been rejected by HR Department. Please contact with HR Department for more information. <br>";
                        eBody += "Following are the details:<br><br>";
                        eBody += "Budget Ref #           : " + BUDGET_REF_NO + "<br>";
                        eBody += "Requisition ID        : " + JOB_REQ_ID + "<br>";
                        //eBody += "Tracking ID           : " + Tracking_Id + "<br>";
                        eBody += "Designation           : " + HRM_DESIGNATION_CD + " - " + DESIGNATION_NAME + "<br>";
                        eBody += "Cadre                   : " + HRM_CADRE_CD + " - " + CADRE_NAME + "<br>";
                        eBody += "Division              : " + HRM_DIVISION_CD + " - " + DIVISION_NAME + "<br>";
                        eBody += "Location               : " + HRM_UNIT_CD + " - " + UNIT_NAME + "<br>";
                        eBody += "Depatrment             : " + HRM_DEPARTMENT_CD + " - " + DEPARTMENT_NAME + "<br>";
                        eBody += "Section                 : " + HRM_SECTION_CD + " - " + SECTION_NAME + "<br>";
                        eBody += "Head Count             : " + HEAD_COUNT + "<br>";
                        eBody += "Hiring Type            : " + HIRING_TYPE + "<br>";
                       // eBody += "Request Status         : " + Tracking_Status + "<br>";
                        eBody += "Hr Remarks                 : " + REMARKS + "<br><br>";
                        //eBody += "Start Date  : " + REQ_DATE + "<br><br>";
                       // eBody += "Action  : " + Action + ", Action Date  : " + Action_Date + "<br><br>";

                        eBody += "This is a system generated e-mail, please do not reply.";

                        // Send Email
                        res.SendEmailTA2(hodemail, eBody, sub);
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public void GetHod_Reject_TA3(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string getHrEmail = "select B.Emp_Hod, e.e_mail, e.emp_name from Hrm_Department_Budget B, Hrm_Job_Requisition R, Hrm_Employee E WHERE b.reg_cd=r.reg_cd AND b.hrm_division_cd=r.hrm_division_cd AND b.hrm_unit_cd=r.hrm_unit_cd AND b.hrm_department_cd=r.hrm_department_cd AND r.job_req_id='" + JOB_REQ_ID + "' AND e.emp_cd=b.emp_hod";

                dt = db.GetData(getHrEmail);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string hodname = dr["emp_name"].ToString();
                        string hodemail = dr["e_mail"].ToString();
                        if (hodemail != "")
                        {
                            EmailIntimationHOD_Reject_TA3(hodemail, hodname, JOB_REQ_ID);
                        }
                    }
                }
            }

            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        public DataTable GetData_Reject_TA3(string job_req_id)
        {
            DataTable dt = new DataTable();
            string getData = "SELECT e.hr_COMMENTS, hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.BUDGET_REF_NO, E.Reg_Cd, E.Hrm_Cadre_Cd, E.Hrm_Department_Cd, E.Hrm_Designation_Cd, e.hrm_division_cd, e.hrm_section_cd, e.hrm_unit_cd, decode(e.hiring_type,'N','New','R','Replacement') hiring_type, e.vacancies, e.remarks FROM HRM_JOB_REQUISITION E WHERE E.Job_Req_Id=" + job_req_id + " and rownum=1";
            dt = db.GetData(getData);
            return dt;
        }

        //protected void btn_GenerateReport_Click(object sender, EventArgs e)
        //{
        //    // Retrieve values from text boxes
        //    string fromDate = txtFromDate.Text;
        //    string toDate = txtToDate.Text;


        //    string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0666.rdf&P_from_date=" + fromDate + "&P_TO_DATE=" + toDate + "&paramform=no";
        //    url = url.Replace("&report=", "&report=\\");
        //    url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
        //    //url = url.Replace("'", "");
        //    Response.Redirect(url);

        //}

        //protected void btn_TrackingReport_Click(object sender, EventArgs e)
        //{
        //    // Retrieve values from text boxes
        //    string fromDate = txtFromDate.Text;
        //    string toDate = txtToDate.Text;


        //    string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0669.rdf&P_from_date=" + fromDate + "&P_TO_DATE=" + toDate + "&paramform=no";
        //    url = url.Replace("&report=", "&report=\\");
        //    url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
        //    //url = url.Replace("'", "");
        //    Response.Redirect(url);

        //}

        protected void btn_TrackingReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from text boxes
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;

                // Define your SQL query
                string query = "SELECT r.job_req_id, r.req_date, r.budget_ref_no, t.tracking_id, DECODE(t.tracking_status, 'O', 'Open', 'C', 'Closed', t.tracking_status) Status, CASE WHEN t.tracking_status = 'C' THEN NVL(TRUNC(t.l$up_date), TRUNC(t.l$in_date)) ELSE NULL END Closed_on, CASE WHEN t.tracking_status = 'C' THEN NVL(TRUNC(t.l$up_date), TRUNC(t.l$in_date)) - TRUNC(r.hod_approved_on) ELSE NULL END Completion_days, r.reg_cd, DECODE(r.reg_cd, '001', 'AKTM', '002', 'AKTM-2', '003', 'AMNA-1', '004', 'AMNA', '005', 'HEAD OFFICE', '006', 'RETAIL', '007', 'AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, r.HRM_DIVISION_CD, hrm_code_desc('DIVISION', r.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, r.HRM_UNIT_CD, hrm_code_desc('UNIT', r.HRM_UNIT_CD, r.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, r.HRM_DEPARTMENT_CD, hrm_code_desc('DEPARTMENT', r.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, r.HRM_SECTION_CD, hrm_code_desc('SECTION', r.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, hrm_code_desc('DESIGNATION', r.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION, e.NO_OF_EMP_HIRED, r.hrm_cadre_cd, r.cadre_subclass_cd, r.hrm_designation_cd, r.is_contractor, r.contractor_cd, DECODE(r.hiring_type, 'N', 'New Hiring', 'R', 'Replacement', r.hiring_type) Hiring_Type, r.gender, r.remarks, r.hr_comments, r.approved_budgeted_salary, u1.emp_name Created_by, r.l$in_date created_on, r.total_gross_salary, r.pc, r.laptop, r.lcd, r.printer, r.workstation, r.cell_phone, r.sim_card, r.residance, r.transport, r.rep_to_emp_cd, r.car_down_payment, r.job_description, r.hod_approved, u2.emp_name Approver_HOD_Name, r.hod_approved_on FROM hrm_job_requisition r LEFT JOIN hrm_job_req_tracking t ON r.job_req_id = t.job_req_id LEFT JOIN ( SELECT EE.BUDGET_REF_NO, COUNT(EE.EMP_CD) AS NO_OF_EMP_HIRED FROM HRM_EMPLOYEE EE GROUP BY EE.BUDGET_REF_NO ) e ON r.BUDGET_REF_NO = e.BUDGET_REF_NO LEFT JOIN hrm_employee u1 ON u1.emp_cd = TO_NUMBER(r.l$usr_in) LEFT JOIN hrm_employee u2 ON u2.emp_cd = TO_NUMBER(r.hod_usr_cd) WHERE (R.REQ_DATE BETWEEN NVL('"+fromDate+"', DATE '2022-06-01') AND NVL('"+toDate+"', SYSDATE)) AND R.HOD_APPROVED_ON IS NOT NULL ORDER BY R.REQ_DATE DESC";

                // Get data from the database
                DataTable dt = db.GetData(query);

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("content-disposition", "attachment; filename=TrackingReport.csv");

                    using (var writer = new StreamWriter(Response.OutputStream, System.Text.Encoding.UTF8))
                    {
                        // Write column headers
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            writer.Write(dt.Columns[i].ColumnName);
                            if (i < dt.Columns.Count - 1)
                            {
                                writer.Write(",");
                            }
                        }
                        writer.WriteLine();

                        // Write rows
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                writer.Write(row[i].ToString());
                                if (i < dt.Columns.Count - 1)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }

                    Response.End();
                }
                else
                {
                    // Handle case when no data is available
                    Response.Write("No data available.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from text boxes
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;

                // Define your SQL query
                string query = "SELECT r.job_req_id, r.req_date, r.budget_ref_no, r.reg_cd, DECODE(r.reg_cd, '001', 'AKTM', '002', 'AKTM-2', '003', 'AMNA-1', '004', 'AMNA', '005', 'HEAD OFFICE', '006', 'RETAIL', '007', 'AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, r.HRM_DIVISION_CD, hrm_code_desc('DIVISION', r.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, r.HRM_UNIT_CD, hrm_code_desc('UNIT', r.HRM_UNIT_CD, r.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, r.HRM_DEPARTMENT_CD, hrm_code_desc('DEPARTMENT', r.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, r.HRM_SECTION_CD, hrm_code_desc('SECTION', r.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, ee_count.NO_OF_EMP_HIRED, r.hrm_cadre_cd, r.cadre_subclass_cd, r.hrm_designation_cd, r.is_contractor, r.contractor_cd, DECODE(r.hiring_type, 'N', 'New Hiring', 'R', 'Replacement', r.hiring_type) Hiring_Type, r.gender, r.remarks, r.hr_comments, r.approved_budgeted_salary, u_created.emp_name Created_by, r.l$in_date created_on, r.total_gross_salary, r.pc, r.laptop, r.lcd, r.printer, r.workstation, r.cell_phone, r.sim_card, r.residance, r.transport, r.rep_to_emp_cd, r.car_down_payment, r.job_description, r.hod_approved, u_approver.emp_name Approver_HOD_Name, r.hod_approved_on FROM hrm_job_requisition r LEFT JOIN (SELECT EE.BUDGET_REF_NO, COUNT(EE.EMP_CD) AS NO_OF_EMP_HIRED FROM HRM_EMPLOYEE EE GROUP BY EE.BUDGET_REF_NO) ee_count ON r.BUDGET_REF_NO = ee_count.BUDGET_REF_NO LEFT JOIN (SELECT ee.emp_cd, ee.emp_name, u.usr_cd FROM hrm_employee ee JOIN users u ON ee.emp_cd = TO_NUMBER(u.emp_cd)) u_created ON u_created.usr_cd = r.l$usr_in LEFT JOIN (SELECT ee.emp_cd, ee.emp_name, u.usr_cd FROM hrm_employee ee JOIN users u ON ee.emp_cd = TO_NUMBER(u.emp_cd)) u_approver ON u_approver.usr_cd = r.hod_usr_cd WHERE r.REQ_DATE BETWEEN NVL(TO_DATE('" + fromDate + "', 'DD-Mon-YYYY'), DATE '2022-06-01') AND NVL(TO_DATE('" + toDate + "', 'DD-Mon-YYYY'), SYSDATE) ORDER BY r.REQ_DATE DESC";
                //SELECT r.job_req_id, r.req_date, r.budget_ref_no, r.reg_cd, DECODE(r.reg_cd, '001', 'AKTM', '002', 'AKTM-2', '003', 'AMNA-1', '004', 'AMNA', '005', 'HEAD OFFICE', '006', 'RETAIL', '007', 'AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, r.HRM_DIVISION_CD, hrm_code_desc('DIVISION', r.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, r.HRM_UNIT_CD, hrm_code_desc('UNIT', r.HRM_UNIT_CD, r.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, r.HRM_DEPARTMENT_CD, hrm_code_desc('DEPARTMENT', r.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, r.HRM_SECTION_CD, hrm_code_desc('SECTION', r.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, (SELECT COUNT(EE.EMP_CD) FROM HRM_EMPLOYEE EE WHERE EE.BUDGET_REF_NO = R.BUDGET_REF_NO ) NO_OF_EMP_HIRED, r.hrm_cadre_cd, r.cadre_subclass_cd, r.hrm_designation_cd, r.is_contractor, r.contractor_cd, DECODE(r.hiring_type, 'N', 'New Hiring', 'R', 'Replacement', r.hiring_type) Hiring_Type, r.gender, r.remarks, r.hr_comments, r.approved_budgeted_salary, (SELECT ee.emp_name FROM hrm_employee ee, users u WHERE ee.emp_cd = TO_NUMBER(u.emp_cd) AND u.usr_cd = r.l$usr_in ) Created_by, r.l$in_date created_on, r.total_gross_salary, r.pc, r.laptop, r.lcd, r.printer, r.workstation, r.cell_phone, r.sim_card, r.residance, r.transport, r.rep_to_emp_cd, r.car_down_payment, r.job_description, r.hod_approved, (SELECT ee.emp_name FROM hrm_employee ee, users u WHERE ee.emp_cd = TO_NUMBER(u.emp_cd) AND u.usr_cd = r.hod_usr_cd ) Approver_HOD_Name, r.hod_approved_on FROM hrm_job_requisition r WHERE (R.REQ_DATE BETWEEN NVL('" + fromDate + "', DATE '2022-06-01') AND NVL('" + toDate + "', SYSDATE)) ORDER BY R.REQ_DATE DESC";

                // Get data from the database
                DataTable dt = db.GetData(query);

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("content-disposition", "attachment; filename=TATReport.csv");

                    using (var writer = new StreamWriter(Response.OutputStream, System.Text.Encoding.UTF8))
                    {
                        // Write column headers
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            writer.Write(dt.Columns[i].ColumnName);
                            if (i < dt.Columns.Count - 1)
                            {
                                writer.Write(",");
                            }
                        }
                        writer.WriteLine();

                        // Write rows
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                writer.Write(row[i].ToString());
                                if (i < dt.Columns.Count - 1)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }

                    Response.End();
                }
                else
                {
                    // Handle case when no data is available
                    Response.Write("No data available.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write("An error occurred: " + ex.Message);
            }
        }



    }
}