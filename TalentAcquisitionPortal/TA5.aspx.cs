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
    public partial class TA5 : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;
        int resultt = 1;


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


        string getroll()
        {
            string roll = "";
            string finalroll = "";
            try
            {
                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                dt = null;

                string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW T WHERE T.EMP_CD= '" + EmployeeCode + "'";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string JOB_REQ_ID = txtSearch.Text.Trim();

                DataTable dt = new DataTable();
                string roll = getroll();
                if (roll == "SUPER")
                {
                    string query = "SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID WHERE E.TRACKING_ID = '"+JOB_REQ_ID+"' UNION ALL SELECT NULL, E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID WHERE E.TRACKING_ID = '"+JOB_REQ_ID+"' ORDER BY 1 DESC";
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
                    string query = "SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID WHERE T.TRACKING_ID = '"+JOB_REQ_ID+"' AND ( (R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ( (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "') ) ) UNION ALL SELECT NULL, E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID WHERE T.TRACKING_ID = '"+JOB_REQ_ID+"' AND (E.ACTION IN ('C', 'J','F') OR NVL(E.SELECTED_BY_HOD, 'N') = 'Y') AND E.APPLICANT_ID NOT IN (SELECT M.APPLICANT_ID FROM HRM_JOB_REQ_EVAL_MST M) AND ( (R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ( (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "') ) ) ORDER BY 1 DESC";
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
                string roll= getroll();
                if (roll == "SUPER")
                {
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID UNION ALL SELECT NULL, E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID ORDER BY 1 DESC";

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
                    DataTable dt = new DataTable();
                    string query = "SELECT M.EVALUATION_ID, M.APPLICANT_NAME, M.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQ_EVAL_MST M JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON M.TRACKING_ID = E.TRACKING_ID AND M.APPLICANT_ID = E.APPLICANT_ID JOIN HRM_JOB_REQ_TRACKING T ON E.TRACKING_ID = T.TRACKING_ID JOIN HRM_JOB_REQUISITION R ON T.JOB_REQ_ID = R.JOB_REQ_ID WHERE ( (R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ( (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "') ) ) UNION ALL SELECT NULL, E.APPLICANT_NAME, E.APPLICANT_ID, HRM_CODE_DESC('DEPARTMENT', R.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', R.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, R.BUDGET_REF_NO, E.CNIC, E.DOB, E.GENDER, E.QUALIFICATION, E.LAST_SALARY, E.MOBILE_NO, E.APPLICANT_ID, E.EMP_NAME, T.TRACKING_ID, R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD, R.HRM_SECTION_CD FROM HRM_JOB_REQUISITION R JOIN HRM_JOB_REQ_TRACKING T ON R.JOB_REQ_ID = T.JOB_REQ_ID JOIN HRM_JOB_REQ_TRACKING_APPLICANT E ON T.TRACKING_ID = E.TRACKING_ID WHERE (E.ACTION IN ('C', 'J','F') OR NVL(E.SELECTED_BY_HOD, 'N') = 'Y') AND E.APPLICANT_ID NOT IN (SELECT M.APPLICANT_ID FROM HRM_JOB_REQ_EVAL_MST M) AND ( (R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD) IN ( (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.EMP_HOD IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT B.REG_CD, B.HRM_DIVISION_CD, B.HRM_UNIT_CD, B.HRM_DEPARTMENT_CD FROM HRM_DEPARTMENT_BUDGET B WHERE B.NOMINATE_EMP_CODE IN (SELECT X.EMP_CD FROM USERS X WHERE X.USR_CD = '" + EmployeeUCode + "')) UNION ALL (SELECT R.REG_CD, R.HRM_DIVISION_CD, R.HRM_UNIT_CD, R.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, USERS U WHERE SD.DETAIL_NAME = TO_CHAR(T.EMP_CD) AND SD.SEQ_NO = 153 AND U.EMP_CD = TO_CHAR(T.EMP_CD) AND T.REG_CD = R.REG_CD AND T.HRM_DIVISION_CD = R.HRM_DIVISION_CD AND T.HRM_UNIT_CD = R.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD = R.HRM_DEPARTMENT_CD AND U.USR_CD = '" + EmployeeUCode + "') ) ) ORDER BY 1 DESC";

                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();

                        lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;

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
                string Applicant_Id = Ids[0].ToString();
                loadInfo(Applicant_Id);
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
                    PopulateTextBoxesFromDatabase(Applicant_Id);
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



        public void loadInfo(string Applicant_Id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.*, E.DEPARTMENT_NAME FROM HRM_JOB_REQ_TRACKING_APPLICANT A INNER JOIN HRM_JOB_REQ_TRACKING B ON A.TRACKING_ID= B.TRACKING_ID INNER JOIN HRM_JOB_REQUISITION C ON B.JOB_REQ_ID= C.JOB_REQ_ID INNER JOIN HRM_DEPARTMENT_mst E ON C.HRM_DEPARTMENT_CD = E.DEPARTMENT_CD WHERE A.APPLICANT_ID = '" + Applicant_Id + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    div_Details.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //TextBox1.Text = dr[""].ToString();
                        TextBox2.Text = dr["TRACKING_ID"].ToString();
                        //TextBox3.Text = dr["JOB_APPLIED_FOR"].ToString();
                        TextBox4.Text = dr["DEPARTMENT_NAME"].ToString();
                        TextBox5.Text = dr["APPLICANT_NAME"].ToString();
                        //TextBox6.Text = dr[""].ToString();
                        TextBox7.Text = dr["FATHER_NAME"].ToString();
                        //TextBox8.Text = dr[""].ToString();
                        TextBox9.Text = dr["DOB"].ToString();
                        TextBox10.Text = dr["CNIC"].ToString();
                        TextBox11.Text = dr["QUALIFICATION"].ToString();
                        TextBox12.Text = dr["LAST_SALARY"].ToString();
                        TextBox21.Text = dr["APPLICANT_ID"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string applicantId = TextBox21.Text;
                string evaluationIdQuery = "SELECT EVALUATION_ID FROM HRM_JOB_REQ_EVAL_MST WHERE APPLICANT_ID = '" + applicantId + "'";
                string existingEvaluationId = db.GetSingleValue(evaluationIdQuery);

                if (!string.IsNullOrEmpty(existingEvaluationId))
                {
                    // Update operation since evaluation_id exists for the applicant
                    UpdateEvaluation(existingEvaluationId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    // Insert operation as evaluation_id doesn't exist for the applicant
                    InsertEvaluation();
                }
                Response.Redirect("TA5.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }



        protected void btnViewCv_Click(object sender, EventArgs e)
        {
            try
            {
                TA4 ta = new TA4();

                string applicantId = TextBox21.Text;
                string fileName = ta.GetFileNameFromDatabase(applicantId);

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


        protected void btnreport_Click(object sender, EventArgs e)
        {
            try
            {
                string applicantId = TextBox21.Text;
                string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0654.rdf&P_APPLICANT_ID=" + applicantId + "&paramform=no";
                Response.Redirect(reportURL);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }



        private void InsertEvaluation()
        {
            try
            {
                // Generate new evaluation_id
                int newEvaluationId = getevaluationid();

                // Rest of your code for data retrieval from textboxes
                //PopulateTextBoxesFromDatabase(evaluationId);
                string chk = "";
                string applicantid = TextBox21.Text;
                string trackingid = TextBox2.Text;
                string interviedate = TextBox6.Text;
                string applicantname = TextBox5.Text;
                string int1 = TextBox13.Text;
                string int2 = TextBox14.Text;
                string int3 = TextBox15.Text;
                string othbenefits = TextBox20.Text;
                string saldecided = TextBox16.Text;
                string dateoj = TextBox17.Text;
                string shift = TextBox18.Text;

                {
                    if (CheckBox1.Checked)
                    {
                        chk = "N";
                    }
                    else if (CheckBox2.Checked)
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
                    InsertDataIntoTable(column1Value, column2Value, column3Value); // Function to insert data
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        private void UpdateEvaluation(string existingEvaluationId)
        {
            try
            {
                // Get data for update
                string trackingid = TextBox2.Text;
                string interviedate = TextBox6.Text;
                string applicantname = TextBox5.Text;
                string int1 = TextBox13.Text;
                string int2 = TextBox14.Text;
                string int3 = TextBox15.Text;
                string othbenefits = TextBox20.Text;
                string saldecided = TextBox16.Text;
                string dateoj = TextBox17.Text;
                string shift = TextBox18.Text;

                string chk = CheckBox1.Checked ? "N" : (CheckBox2.Checked ? "Y" : "");

                // Perform the update operation
                string updateQuery = "UPDATE HRM_JOB_REQ_EVAL_MST SET INTERVIEWER_1 = '" + int1 + "', INTERVIEWER_2 = '" + int2 + "', INTERVIEWER_3 = '" + int3 + "', OTHER_BENEFITS = '" + othbenefits + "', SALARY_DECIDED = '" + saldecided + "', DOJ = '" + dateoj + "', SHIFT_DETAILS = '" + shift + "', RECOMMENDATION = '" + chk + "' WHERE EVALUATION_ID = '" + existingEvaluationId + "'";

                string updateResult = db.PostData(updateQuery);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Access each cell in the row and insert data into your table
                    string column1Value = row.Cells[0].Text;
                    string column2Value = row.Cells[1].Text;
                    TextBox textBox = row.Cells[1].FindControl("myTextBox") as TextBox;
                    string column3Value = textBox.Text;

                    // Perform the insertion into your table using SQL commands or your preferred method
                    UpdateDataIntoTable(column1Value, column2Value, column3Value, existingEvaluationId, updateResult); // Function to insert data
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }









        private int getevaluationid()
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

        private void UpdateDataIntoTable(string column1Value, string column2Value, string column3Value, string existingEvaluationId, string updateResult)
        {
            try
            {
                // Assuming existingEvaluationId is already available within this scope
                // Modify this query according to your table structure and requirements
                string updateQuery = "UPDATE HRM_JOB_REQ_EVAL_DTL SET PARAMETER = '" + column2Value + "', SCORE = '" + column3Value + "' WHERE EVALUATION_ID = '" + existingEvaluationId + "' AND PARM_ID = '" + column1Value + "'";

                string updateResultt = db.PostData(updateQuery);

                if (updateResultt == "Done" && updateResult == "Done")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    GetHrbp(existingEvaluationId);
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    loadPendingRequests();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    loadPendingRequests();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }









        private void InsertDataIntoTable(string column1Value, string column2Value, string column3Value)
        {


            try
            {
                // Insert into detail table with a new DTL_ID for each record linked to EVALUATION_ID
                string insertDetailQuery = "INSERT INTO HRM_JOB_REQ_EVAL_DTL (EVALUATION_ID, DTL_ID, PARM_ID, PARAMETER, SCORE, L$IN_DATE, L$USR_IN) " + " VALUES ('" + resultt + "', (SELECT NVL(MAX(D.DTL_ID),0)+1 FROM HRM_JOB_REQ_EVAL_DTL D) , '" + column1Value + "', '" + column2Value + "' , '" + column3Value + "', TRUNC(SYSDATE), '" + EmployeeUCode + "' )";
                string resultDetail = db.PostData(insertDetailQuery);

                if (resultDetail == "Done")
                {
                    GetHrbp((resultt).ToString());
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    loadPendingRequests();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    gv_PendingRequests.DataSource = null; // Clear the previous data source
                    loadPendingRequests();
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void gv_PendingRequests_RowDataBound(object sender, GridViewRowEventArgs e)
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

















        private void PopulateTextBoxesFromDatabase(string Applicant_Id)
        {
            // Construct your SQL query to fetch data based on the EVALUATION_ID
            string selectQuery = "SELECT b.Parm_Id, b.parameter, b.score, a.Evaluation_Id, a.TRACKING_ID, a.APPLICANT_ID, a.INTERVIEW_DATE, a.APPLICANT_NAME, a.INTERVIEWER_1, a.INTERVIEWER_2, a.INTERVIEWER_3, a.OTHER_BENEFITS, a.SALARY_DECIDED, a.DOJ, a.SHIFT_DETAILS, a.RECOMMENDATION FROM HRM_JOB_REQ_EVAL_MST a INNER JOIN HRM_JOB_REQ_EVAL_DTL b ON a.evaluation_id=b.evaluation_id WHERE APPLICANT_ID = '" + Applicant_Id + "'";

            // Execute the query to retrieve data
            DataTable data = db.GetData(selectQuery);

            if (data.Rows.Count > 0)
            {
                TextBox1.Text = data.Rows[0]["Evaluation_Id"].ToString();
                TextBox21.Text = data.Rows[0]["APPLICANT_ID"].ToString(); // Populate the Applicant ID textbox
                TextBox2.Text = data.Rows[0]["TRACKING_ID"].ToString();
                TextBox6.Text = data.Rows[0]["INTERVIEW_DATE"].ToString();
                TextBox5.Text = data.Rows[0]["APPLICANT_NAME"].ToString();
                TextBox13.Text = data.Rows[0]["INTERVIEWER_1"].ToString();
                TextBox14.Text = data.Rows[0]["INTERVIEWER_2"].ToString();
                TextBox15.Text = data.Rows[0]["INTERVIEWER_3"].ToString();
                TextBox20.Text = data.Rows[0]["OTHER_BENEFITS"].ToString();
                TextBox16.Text = data.Rows[0]["SALARY_DECIDED"].ToString();
                TextBox17.Text = data.Rows[0]["DOJ"].ToString();
                TextBox18.Text = data.Rows[0]["SHIFT_DETAILS"].ToString();

                string recommendation = data.Rows[0]["RECOMMENDATION"].ToString();
                if (recommendation == "N")
                {
                    CheckBox1.Checked = true;
                }
                else if (recommendation == "Y")
                {
                    CheckBox2.Checked = true;
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
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
            }
        }



        void GetHrbp(string evlid)
        {
            try
            {
                DataTable dt = new DataTable();
                string getHrEmail = "SELECT B.EMP_NAME, B.e_Mail, A.DETAIL_NAME FROM Hrm_Setup_Detl A JOIN HRM_EMPLOYEE B ON TO_CHAR(B.EMP_CD) = A.DETAIL_NAME WHERE A.Seq_No = 121 AND B.EMP_STATUS = 'A'";
                dt = db.GetData(getHrEmail);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //string hrname = dr["EMP_NAME"].ToString();
                        string hremail = dr["e_Mail"].ToString();
                        if (hremail != "")
                        {
                            EmailIntimationHOD(hremail, evlid);
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }









        public void EmailIntimationHOD(string hremail, string evlid)
        {
            try
            {

                SendResponse res = new SendResponse();

                string sub = "HOD Updated an Evaluation Id: '" + evlid + "'";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "Evaluation request has been updated by HOD. Following are the details. " + "<br /><br />";
                eBody += "Budget Code : " + TextBox11 + "<br />";
                eBody += "Evaluation Id : " + TextBox1.Text + "<br />";
                eBody += "Tracking Id : " + TextBox2.Text + "<br />";
                eBody += "Applicant Id : " + TextBox21.Text + "<br />";
                eBody += "Applicant Name : " + TextBox5.Text + "<br />";
                eBody += "Father Name : " + TextBox7.Text + "<br />";
                eBody += "Date of Birth : " + TextBox9.Text + "<br />";
                eBody += "CNIC : " + TextBox10.Text + "<br />";
                eBody += "Department : " + TextBox4.Text + "<br />";
                eBody += "Qualification : " + TextBox11.Text + "<br />";
                eBody += "Interview Date : " + TextBox6.Text + "<br />";

                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                res.SendEmailTA2(hremail, eBody, sub);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }




    }
}