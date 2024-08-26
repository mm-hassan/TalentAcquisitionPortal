using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class TA1 : System.Web.UI.Page
    {
        string EmployeeCode;
        string EmployeeUCode;
        // Class-level variables
        private string reg_cd, div_cd, unit_cd, dept_cd;
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {

            DropDownEdu.Items.Clear();
            DropDownReports.Items.Clear();
            DropDownGender.SelectedIndex = 0;
            hiring_ddl.SelectedIndex = 0;
            DropDownMinExp.SelectedIndex = 0;
            getSession();

            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                // Clear the DataTable stored in ViewState
                ViewState["Employees"] = null;

                // Clear the dropdown list selection
                DropDownListExEmployee.SelectedIndex = -1;
                DropDownListExEmployee.Items.Clear();


                div_EmployeeDetails.Visible = false;
                //cvUpload.Visible = false;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
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
                    string query = "SELECT  X.START_DATE, X.BUDGET_REF_NO, X.TOTAT_STR , SUM(NVL(X.UT_BUD,0))UT_BUD , NVL(X.TOTAT_STR,0) - SUM(NVL(X.UT_BUD,0))VACANT_BUD , X.REG_CD, X.REG, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY FROM ( select V.SANCTION_STR  TOTAT_STR ,V.REF_NO BUDGET_REF_NO, V.START_DATE, V.EMP_DIVISION_CD,  hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD ,hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD   , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD   , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD,hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  ,hrm_code_desc('DESIGNATION', V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.EMP_CD EMP_CD_TP_NO, (CASE WHEN E.EMP_CD IS NULL THEN  0 WHEN E.NOTICE_PAY_FROM IS NOT NULL THEN 0 ELSE 1 END) UT_BUD, e.emp_name, e.appointment_date appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, E.CADRE_SUBCLASS_CD ||' - '|| hrm_code_desc('CADRE_SUBCLASS', E.CADRE_SUBCLASS_CD, NULL, NULL, NULL) CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'EMP INFO' DATA_FROM from HRM_LIVE.HRM_EMPLOYEE E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO=E.BUDGET_REF_NO(+) and E.emp_status (+)  IN ('A','D') and v.STR_STATUS in ('FA') UNION ALL select V.SANCTION_STR   ,V.REF_NO BUDGET_REF_NO,V.START_DATE ,  V.EMP_DIVISION_CD,   hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD  , hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD  , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD    , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD , hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  , hrm_code_desc('DESIGNATION',  V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.TRIAL_PASS_NO, (CASE WHEN E.TRIAL_PASS_NO IS NULL THEN 0 ELSE 1 END) UT_BUD, e.name, e.FROMDATE appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, '' CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'TRIAL PASS' DATA_FROM from TRIAL_PASS_INFO E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO= E.BUDGET_REF_NO(+) and E.emp_status (+) IN ('A') and v.STR_STATUS in ('FA') ) X WHERE NOT EXISTS ( SELECT 1 FROM HRM_JOB_REQ_TRACKING R WHERE R.BUDGET_REF_NO=X.BUDGET_REF_NO AND NVL(R.TRACKING_STATUS,'N') !='C') GROUP BY  X.TOTAT_STR ,X.BUDGET_REF_NO, X.START_DATE, X.REG, X.REG_CD, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY HAVING   NVL(X.TOTAT_STR,0) >  SUM(NVL(X.UT_BUD,0)) order by 3";
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
                    string query = "select CASE WHEN (SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id) IS NULL THEN 'Pending' ELSE TO_CHAR((SELECT t.tracking_id FROM HRM_JOB_REQ_TRACKING t WHERE t.job_req_id = e.job_req_id)) END AS Tracking_id, e.job_req_id, e.budget_ref_no, e.req_date, e.hrm_cadre_cd, e.approve_str, e.utilize_str, decode(e.reg_cd,'001','AKTM','002','AKTM-2','003','AMNA-1', '004','AMNA', '005','HEAD OFFICE', '006','RETAIL', '007','AKTM-3', '008', 'SATTAR', '009', 'AK-Nooriabad', '') Region_Name, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD, e.hrm_section_cd,e.hrm_designation_cd, e.status, E.CONTRACTOR_CD, E.HIRING_TYPE, E.REMARKS, E.HR_COMMENTS, E.APPROVED_BUDGETED_SALARY, E.JOB_EVAL_ID, E.STATUS, E.APPROVE_STR, E.UTILIZE_STR, E.ATTEND_ALLOWANCE, E.PROD_ALLOWANCE, E.BENEFITS, E.OVERTIME, E.HOLIDAY_OT, E.TOTAL_GROSS_SALARY, e.closing_date from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='Y' AND JOB_REQ_ID='" + JOB_REQ_ID + "' and e.hrm_cadre_cd  NOT in ('13') order by TRACKING_ID desc , e.job_req_id DESC";

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

                string query = "";
                
                DataTable dt = new DataTable();
                string roll = getroll();
                if (roll == "SUPER")
                {
                    query = "SELECT  X.START_DATE, X.BUDGET_REF_NO, X.TOTAT_STR , SUM(NVL(X.UT_BUD,0))UT_BUD , NVL(X.TOTAT_STR,0) - SUM(NVL(X.UT_BUD,0))VACANT_BUD , X.REG_CD, X.REG, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY FROM ( select V.SANCTION_STR  TOTAT_STR ,V.REF_NO BUDGET_REF_NO, V.START_DATE, V.EMP_DIVISION_CD,  hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD ,hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD   , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD   , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD,hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  ,hrm_code_desc('DESIGNATION', V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.EMP_CD EMP_CD_TP_NO, (CASE WHEN E.EMP_CD IS NULL THEN  0 WHEN E.NOTICE_PAY_FROM IS NOT NULL THEN 0 ELSE 1 END) UT_BUD, e.emp_name, e.appointment_date appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, E.CADRE_SUBCLASS_CD ||' - '|| hrm_code_desc('CADRE_SUBCLASS', E.CADRE_SUBCLASS_CD, NULL, NULL, NULL) CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'EMP INFO' DATA_FROM from HRM_LIVE.HRM_EMPLOYEE E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO=E.BUDGET_REF_NO(+) and E.emp_status (+)  IN ('A','D') and v.STR_STATUS in ('FA') UNION ALL select V.SANCTION_STR   ,V.REF_NO BUDGET_REF_NO,V.START_DATE ,  V.EMP_DIVISION_CD,   hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD  , hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD  , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD    , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD , hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  , hrm_code_desc('DESIGNATION',  V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.TRIAL_PASS_NO, (CASE WHEN E.TRIAL_PASS_NO IS NULL THEN 0 ELSE 1 END) UT_BUD, e.name, e.FROMDATE appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, '' CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'TRIAL PASS' DATA_FROM from TRIAL_PASS_INFO E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO= E.BUDGET_REF_NO(+) and E.emp_status (+) IN ('A') and v.STR_STATUS in ('FA') ) X WHERE NOT EXISTS ( SELECT 1 FROM HRM_JOB_REQ_TRACKING R WHERE R.BUDGET_REF_NO=X.BUDGET_REF_NO AND NVL(R.TRACKING_STATUS,'N') !='C') GROUP BY  X.TOTAT_STR ,X.BUDGET_REF_NO, X.START_DATE, X.REG, X.REG_CD, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY HAVING   NVL(X.TOTAT_STR,0) >  SUM(NVL(X.UT_BUD,0)) order by 3";
                }
                else
                {
                    query = "SELECT * FROM HRM_LIVE.HRM_REQUISITION_BUDGET_VIEW b WHERE (b.REG_CD, b.EMP_DIVISION_CD, b.EMP_LOCATION_CD, b.EMP_DEPARTMENT_CD) IN ( SELECT REG_CD, DIVISION_CD, UNIT_CD, DEPARTMENT_CD FROM HRM_DEPARTMENT B JOIN USERS U ON B.Emp_Kpi_Nominate = U.EMP_CD WHERE U.USR_CD = '" + EmployeeUCode + "' UNION SELECT REG_CD, DIVISION_CD, UNIT_CD, DEPARTMENT_CD FROM HRM_DEPARTMENT B JOIN USERS U ON B.EMP_HOD13 = U.EMP_CD WHERE U.USR_CD = '" + EmployeeUCode + "' UNION SELECT REG_CD, HRM_DIVISION_CD, HRM_UNIT_CD, HRM_Department_Cd FROM HRM_DEPARTMENT_BUDGET B JOIN USERS U ON B.EMP_HOD = U.EMP_CD WHERE U.USR_CD = '" + EmployeeUCode + "' UNION SELECT REG_CD, HRM_DIVISION_CD, HRM_UNIT_CD, HRM_Department_Cd FROM HRM_DEPARTMENT_BUDGET B JOIN USERS U ON B.NOMINATE_EMP_CODE = U.EMP_CD WHERE U.USR_CD = '" + EmployeeUCode + "' )";
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void lbl_View_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownEdu.Items.Clear();
                DropDownReports.Items.Clear();
                DropDownGender.SelectedIndex = 0;
                DropDownMinExp.SelectedIndex = 0;
                hiring_ddl.SelectedIndex = 0;

                replacementRow.Visible = false;
                // Clear the DataTable stored in ViewState
                ViewState["Employees"] = null;


                // Clear the dropdown list selection
                DropDownListExEmployee.Items.Clear();
                DropDownListExEmployee.SelectedIndex = -1;

                GridView1.DataSource = null;
                GridView1.DataBind();





                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string BUDGET_REF_NO = Ids[0].ToString();
                loadInfo(BUDGET_REF_NO);
                //loadApplicants(JOB_REQ_ID);
                div_EmployeeDetails.Visible = true;



                
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

        public void loadInfo(string BUDGET_REF_NO)
        {
            try
            {

                DataTable dt = new DataTable();
                string query = "SELECT  X.START_DATE, X.BUDGET_REF_NO, X.TOTAT_STR , SUM(NVL(X.UT_BUD,0))UT_BUD , NVL(X.TOTAT_STR,0) - SUM(NVL(X.UT_BUD,0))VACANT_BUD , X.REG_CD, X.REG, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY FROM ( select V.SANCTION_STR  TOTAT_STR ,V.REF_NO BUDGET_REF_NO, V.START_DATE, V.EMP_DIVISION_CD,  hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD ,hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD   , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD   , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD,hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  ,hrm_code_desc('DESIGNATION', V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.EMP_CD EMP_CD_TP_NO, (CASE WHEN E.EMP_CD IS NULL THEN  0 WHEN E.NOTICE_PAY_FROM IS NOT NULL THEN 0 ELSE 1 END) UT_BUD, e.emp_name, e.appointment_date appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, E.CADRE_SUBCLASS_CD ||' - '|| hrm_code_desc('CADRE_SUBCLASS', E.CADRE_SUBCLASS_CD, NULL, NULL, NULL) CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'EMP INFO' DATA_FROM from HRM_LIVE.HRM_EMPLOYEE E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO=E.BUDGET_REF_NO(+) and E.emp_status (+)  IN ('A','D') and v.STR_STATUS in ('FA') UNION ALL select V.SANCTION_STR   ,V.REF_NO BUDGET_REF_NO,V.START_DATE ,  V.EMP_DIVISION_CD,   hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD  , hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD  , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD    , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD , hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  , hrm_code_desc('DESIGNATION',  V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.TRIAL_PASS_NO, (CASE WHEN E.TRIAL_PASS_NO IS NULL THEN 0 ELSE 1 END) UT_BUD, e.name, e.FROMDATE appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, '' CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'TRIAL PASS' DATA_FROM from TRIAL_PASS_INFO E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO= E.BUDGET_REF_NO(+) and E.emp_status (+) IN ('A') and v.STR_STATUS in ('FA') ) X WHERE X.BUDGET_REF_NO= '" + BUDGET_REF_NO + "' AND NOT EXISTS ( SELECT 1 FROM HRM_JOB_REQ_TRACKING R WHERE R.BUDGET_REF_NO=X.BUDGET_REF_NO AND NVL(R.TRACKING_STATUS,'N') !='C') GROUP BY  X.TOTAT_STR ,X.BUDGET_REF_NO, X.START_DATE, X.REG, X.REG_CD, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY HAVING   NVL(X.TOTAT_STR,0) >  SUM(NVL(X.UT_BUD,0)) order by 3";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {

                    div_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        approved.Text = dr["UT_BUD"].ToString();
                        current.Text = dr["TOTAT_STR"].ToString();
                        balance.Text = dr["VACANT_BUD"].ToString();
                        region.Text = dr["REG_CD"].ToString() + " - " + dr["REG"].ToString();
                        section.Text = dr["EMP_SECTION_CD"].ToString() + " - " + dr["BUD_SECTION_NAME"].ToString();
                        division.Text =  dr["EMP_DIVISION_CD"].ToString() + " - " + dr["BUD_DIVISION_NAME"].ToString();
                        cadre.Text = dr["EMP_CADRE_CD"].ToString() + " - " + dr["BUD_CADRE_NAME"].ToString();
                        unit.Text = dr["EMP_LOCATION_CD"].ToString() + " - " + dr["BUD_UNIT_NAME"].ToString();
                        cadresubclass.Text = dr["EMP_CADRE_SUBCLASS_CD"].ToString() + " - " + dr["BUD_CADRE_SUBCLASS"].ToString();
                        department.Text = dr["EMP_DEPARTMENT_CD"].ToString() + " - " + dr["BUD_DEPARTMENT_NAME"].ToString();
                        designation.Text = dr["EMP_DESIGNATION_CD"].ToString() + " - " + dr["BUD_DESIGNATION_NAME"].ToString();




                        reg_cd = dr["REG_CD"].ToString();
                        div_cd = dr["EMP_DIVISION_CD"].ToString();
                        unit_cd = dr["EMP_LOCATION_CD"].ToString();
                        dept_cd = dr["EMP_DEPARTMENT_CD"].ToString();

                        // Store these values in ViewState to persist across postbacks
                        ViewState["reg_cd"] = reg_cd;
                        ViewState["div_cd"] = div_cd;
                        ViewState["unit_cd"] = unit_cd;
                        ViewState["dept_cd"] = dept_cd;


                        }
                    }



                DataTable dtt = new DataTable();
                string queryy = "SELECT E.EMP_CD, E.EMP_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME FROM HRM_EMPLOYEE E WHERE  E.REG_CD= '" + reg_cd + "' AND E.HRM_DIVISION_CD= '" + div_cd + "' AND E.HRM_UNIT_CD='" + unit_cd + "' AND E.HRM_DEPARTMENT_CD='" + dept_cd + "' AND E.HRM_CADRE_CD in ( '04', '05', '06', '07', '08','09','10') AND E.BUDGET_REF_NO IS NOT NULL AND E.EMP_STATUS = 'A' UNION SELECT EE.EMP_CD, EE.EMP_NAME, hrm_code_desc('DESIGNATION', EE.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME FROM HRM_EMPLOYEE EE WHERE  EE.HRM_CADRE_CD in ( '02', '03','04', '05', '06', '07') AND EE.EMP_STATUS = 'A' ORDER BY 2";
                dtt = db.GetData(queryy);
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow row in dtt.Rows)
                    {
                        string text = row["EMP_CD"].ToString() + "                - " + row["EMP_NAME"].ToString() + "                - (" + row["DESIGNATION_NAME"].ToString()+")";
                        string value = row["EMP_CD"].ToString();
                        DropDownReports.Items.Add(new ListItem(text, value));
                    }
                }

                // Add a default item at the top of the list
                DropDownReports.Items.Insert(0, new ListItem("Select Reportee", "0"));






                DataTable dttt = new DataTable();
                string queryyy = "SELECT D.DETAIL_NAME, D.DETAIL_ID FROM HRM_SETUP_DETL D WHERE D.MASTER_CD = 'QUALIFICATION' Order By 1";
                dttt = db.GetData(queryyy);
                if (dttt.Rows.Count > 0)
                {
                    foreach (DataRow row in dttt.Rows)
                    {
                        string text = row["DETAIL_NAME"].ToString();
                        string value = row["DETAIL_ID"].ToString();
                        DropDownEdu.Items.Add(new ListItem(text, value));
                    }
                }

                // Add a default item at the top of the list
                DropDownEdu.Items.Insert(0, new ListItem("Select Min Education", "0"));










                DataTable dtttt = new DataTable();
                string queryyyy = "SELECT s.REF_NO, s.RATEOFPAY, s.PROD_ALLOWANCE, s.OVERTIME, s.BENEFITS, s.TOTAL_GROSS_SALARY, s.ATTEND_ALLOWANCE, s.HOLIDAY_OT FROM Hrm_Approved_Strenght_View s WHERE s.REF_NO= 50001";
                dtttt = db.GetData(queryyyy);
                if (dtttt.Rows.Count > 0)
                {
                    foreach (DataRow row in dtttt.Rows)
                    {
                        TextBox3.Text = row["RATEOFPAY"].ToString();
                        TextBox5.Text = row["PROD_ALLOWANCE"].ToString();
                        TextBox7.Text = row["OVERTIME"].ToString();
                        TextBox9.Text = row["BENEFITS"].ToString();
                        TextBox17.Text = row["TOTAL_GROSS_SALARY"].ToString();
                        TextBox18.Text = row["ATTEND_ALLOWANCE"].ToString();
                        TextBox19.Text = row["HOLIDAY_OT"].ToString();
                    }
                }

                }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hiring_ddl.SelectedValue == "R")
            {
                LoadExEmployeeCodes();
                replacementRow.Visible = true;
            }
            else
            {
                replacementRow.Visible = false;
                // Clear the DataTable stored in ViewState
                ViewState["Employees"] = null;

                // Clear the dropdown list selection

                DropDownListExEmployee.SelectedIndex = -1;
                DropDownListExEmployee.Items.Clear();
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        protected void LoadExEmployeeCodes()
        {
            // Retrieve the stored values from ViewState
            reg_cd = ViewState["reg_cd"].ToString();
            div_cd = ViewState["div_cd"].ToString();
            unit_cd = ViewState["unit_cd"].ToString();
            dept_cd = ViewState["dept_cd"].ToString();

            DataTable dt = new DataTable();
            string query = "SELECT E.EMP_CD, E.EMP_NAME, decode(E.EMP_STATUS,'I','In-Active','A','Active', 'S', 'Temporary In-Active') EMP_STATUS,  E.BUDGET_REF_NO,  E.LAST_DUTY FROM HRM_EMPLOYEE E WHERE  E.REG_CD= '" + reg_cd + "' AND E.HRM_DIVISION_CD= '" + div_cd + "' AND E.HRM_UNIT_CD='" + unit_cd + "' AND E.HRM_DEPARTMENT_CD='" + dept_cd + "' AND E.BUDGET_REF_NO IS NOT NULL AND (( E.EMP_STATUS != 'A' ) or (E.NOTICE_PAY_FROM IS NOT NULL)) ORDER BY E.EMP_NAME";
            dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string text = row["EMP_CD"].ToString() + "                - " + row["EMP_NAME"].ToString() + "                - " + row["EMP_STATUS"].ToString() + "                - (" + row["LAST_DUTY"].ToString() + ")";
                    string value = row["EMP_CD"].ToString();
                    DropDownListExEmployee.Items.Add(new ListItem(text, value));
                }
             }
          }

        protected void DdlExEmployee_IndexChanged(object sender, EventArgs e)
        {
            if (hiring_ddl.SelectedValue == "R" && DropDownListExEmployee.SelectedValue != "0")
            {
                AddExEmployeeToGrid(DropDownListExEmployee.SelectedValue);
            }
        }
        

        protected void AddExEmployeeToGrid(string employeeCode)
        {
        DataTable dt = new DataTable();
        string query = "SELECT E.EMP_CD, E.EMP_NAME,E.EMP_STATUS,  E.BUDGET_REF_NO,  E.LAST_DUTY FROM HRM_EMPLOYEE E WHERE E.Emp_Cd = '" + employeeCode + "' AND E.EMP_STATUS != 'A' ";
            dt = db.GetData(query);
            if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }               
        }

        private DataTable LoadEmployeeData(string empCode)
        {
            // Replace with actual data access logic
            DataTable dt = new DataTable();
            string query = "SELECT EMP_CD, EMP_NAME, EMP_STATUS, BUDGET_REF_NO, LAST_DUTY " +
                           "FROM HRM_EMPLOYEE " +
                           "WHERE EMP_CD = '" + empCode + "'";
            dt = db.GetData(query);
            return dt;
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string empcd= DropDownListExEmployee.SelectedValue;
            string query = "SELECT E.EMP_CD, E.EMP_NAME, decode(E.EMP_STATUS,'I','In-Active','A','Active', 'S', 'Temporary In-Active') EMP_STATUS,  E.BUDGET_REF_NO,  E.LAST_DUTY FROM HRM_EMPLOYEE E WHERE E.Emp_Cd = '" + empcd + "' AND (( E.EMP_STATUS != 'A' ) or (E.NOTICE_PAY_FROM IS NOT NULL))";
            DataTable newData = new DataTable();
            newData = db.GetData(query);
            if (newData.Rows.Count > 0)
            {
                DataTable dt;


                // Retrieve the existing DataTable from ViewState, or create a new one if it doesn't exist
                if (ViewState["Employees"] != null)
                {
                    dt = (DataTable)ViewState["Employees"];
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("EMP_CD");
                    dt.Columns.Add("EMP_NAME");
                    dt.Columns.Add("EMP_STATUS");
                    dt.Columns.Add("BUDGET_REF_NO");
                    dt.Columns.Add("LAST_DUTY");
                }



                // Add new rows to the DataTable
                foreach (DataRow row in newData.Rows)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["EMP_CD"] = row["EMP_CD"] != DBNull.Value ? row["EMP_CD"] : DBNull.Value;
                    newRow["EMP_NAME"] = row["EMP_NAME"] != DBNull.Value ? row["EMP_NAME"] : DBNull.Value;
                    newRow["EMP_STATUS"] = row["EMP_STATUS"] != DBNull.Value ? row["EMP_STATUS"] : DBNull.Value;
                    newRow["BUDGET_REF_NO"] = row["BUDGET_REF_NO"] != DBNull.Value ? row["BUDGET_REF_NO"] : DBNull.Value;
                    newRow["LAST_DUTY"] = row["LAST_DUTY"] != DBNull.Value ? row["LAST_DUTY"] : DBNull.Value;

                    dt.Rows.Add(newRow);
                }

                // Save the updated DataTable to ViewState
                ViewState["Employees"] = dt;


                // Bind the DataTable to the GridView
                GridView1.DataSource = dt;
                GridView1.DataBind();


                lbl_GridMsg.Text = "Data added successfully.";
                lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl_GridMsg.Text = "No data found for the selected employee.";
                lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        protected void Hiring_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hiring_ddl.SelectedValue == "R")
            {
                LoadExEmployeeCodes();
                //.Visible = true;
            }
            else
            {
                //replacementRow.Visible = false;
                //// Clear the DataTable stored in ViewState
                //ViewState["Employees"] = null;

                //// Clear the dropdown list selection

                //DropDownListExEmployee.SelectedIndex = -1;
                //DropDownListExEmployee.Items.Clear();
                //GridView1.DataSource = null;
                //GridView1.DataBind();
            }
        }

    }
}