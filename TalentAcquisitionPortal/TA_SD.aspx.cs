using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class TA_SD : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;
        // Class-level variables
        private string reg_cd, div_cd, unit_cd, dept_cd, sect_cd, cadre_cd, cadresubclass_cd, desig_cd;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            getSession();

            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                DropDownEdu.Items.Clear();
                DropDownReports.Items.Clear();


                DropDownMinExp.SelectedIndex = 0;
                //Clear the dropdown list selection
                DropDownGender.SelectedIndex = 0;
                Ex_Emp.Visible = false;
                New_Pending.Visible = false;
                //div_EmployeeDetails.Visible = false;

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

        protected void Select_Hiring_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Select_Hiring.SelectedValue == "R")
            {
                Rep.Text = "R";
                LoadExEmployeeCodes();
                Ex_Emp.Visible = true;
                New_Pending.Visible = false;
            }
            else
            {
                loadPendingRequests();
                Ex_Emp.Visible = false;
                New_Pending.Visible = true;
            }
        }

        protected void LoadExEmployeeCodes()
        {
            DataTable dt = new DataTable();
            string roll = getroll();
            if (roll == "SUPER")
            {
                string QUERY = "SELECT E.EMP_CD, E.EMP_NAME, DECODE(E.EMP_STATUS, 'I', 'In-Active', 'A', 'Active', 'S', 'Temporary In-Active') AS EMP_STATUS, E.BUDGET_REF_NO, E.LAST_DUTY FROM HRM_EMPLOYEE E JOIN HRM_DEPARTMENT D ON D.DEPARTMENT_CD = E.HRM_DEPARTMENT_CD AND D.UNIT_CD = E.HRM_UNIT_CD AND D.DIVISION_CD = E.HRM_DIVISION_CD AND D.REG_CD = E.REG_CD AND D.END_DATE IS NULL JOIN HRM_LIVE.HRM_REQUISITION_BUDGET_VIEW R ON E.BUDGET_REF_NO = R.BUDGET_REF_NO WHERE (E.EMP_STATUS != 'A' or e.notice_pay_from is not null) and E.Emp_Cd NOT IN (SELECT D.EMP_CD FROM HRM_JOB_REQ_REPLACE_DTL D)";
                //string QUERY = "SELECT E.EMP_CD, E.EMP_NAME, DECODE(E.EMP_STATUS, 'I', 'In-Active', 'A', 'Active', 'S', 'Temporary In-Active') AS EMP_STATUS, E.BUDGET_REF_NO, E.LAST_DUTY FROM HRM_EMPLOYEE E JOIN HRM_DEPARTMENT D ON D.DEPARTMENT_CD = E.HRM_DEPARTMENT_CD AND D.UNIT_CD = E.HRM_UNIT_CD AND D.DIVISION_CD = E.HRM_DIVISION_CD AND D.REG_CD = E.REG_CD AND D.END_DATE IS NULL JOIN HRM_LIVE.HRM_REQUISITION_BUDGET_VIEW R ON E.BUDGET_REF_NO = R.BUDGET_REF_NO WHERE E.EMP_STATUS != 'A'";
                dt = db.GetData(QUERY);
                if (dt.Rows.Count > 0)
                {
                    gv_ExEmp_Requests.DataSource = dt;
                    gv_ExEmp_Requests.DataBind();

                    lbl_EX_GridMsg.Text = "Please click on 'view' to process the request.";
                    lbl_EX_GridMsg.ForeColor = System.Drawing.Color.Green;

                }
                else
                {

                    lbl_EX_GridMsg.Text = "No any pending request.";
                    lbl_EX_GridMsg.ForeColor = System.Drawing.Color.Red;
                    gv_ExEmp_Requests.DataSource = dt;
                    gv_ExEmp_Requests.DataBind();
                }
            }
            else
            {
                string QUERY = "SELECT E.EMP_CD, E.EMP_NAME, DECODE(E.EMP_STATUS, 'I', 'In-Active', 'A', 'Active', 'S', 'Temporary In-Active') AS EMP_STATUS, E.BUDGET_REF_NO, E.LAST_DUTY FROM HRM_EMPLOYEE E JOIN HRM_DEPARTMENT D ON D.DEPARTMENT_CD = E.HRM_DEPARTMENT_CD AND D.UNIT_CD = E.HRM_UNIT_CD AND D.DIVISION_CD = E.HRM_DIVISION_CD AND D.REG_CD = E.REG_CD AND D.END_DATE IS NULL AND D.EMP_KPI_NOMINATE = '" + EmployeeCode + "' JOIN HRM_LIVE.HRM_REQUISITION_BUDGET_VIEW R ON E.BUDGET_REF_NO = R.BUDGET_REF_NO WHERE (E.EMP_STATUS != 'A' or e.notice_pay_from is not null) and E.Emp_Cd NOT IN (SELECT D.EMP_CD FROM HRM_JOB_REQ_REPLACE_DTL D)";
                dt = db.GetData(QUERY);
                if (dt.Rows.Count > 0)
                {
                    gv_ExEmp_Requests.DataSource = dt;
                    gv_ExEmp_Requests.DataBind();

                    lbl_EX_GridMsg.Text = "Please click on 'view' to process the request.";
                    lbl_EX_GridMsg.ForeColor = System.Drawing.Color.Green;

                }
                else
                {

                    lbl_EX_GridMsg.Text = "No any pending request.";
                    lbl_EX_GridMsg.ForeColor = System.Drawing.Color.Red;
                    gv_ExEmp_Requests.DataSource = dt;
                    gv_ExEmp_Requests.DataBind();
                }
            }
            //string query = "SELECT E.EMP_CD, E.EMP_NAME, decode(E.EMP_STATUS,'I','In-Active','A','Active', 'S', 'Temporary In-Active') EMP_STATUS,  E.BUDGET_REF_NO,  E.LAST_DUTY FROM HRM_EMPLOYEE E WHERE  E.REG_CD= '" + reg_cd + "' AND E.HRM_DIVISION_CD= '" + div_cd + "' AND E.HRM_UNIT_CD='" + unit_cd + "' AND E.HRM_DEPARTMENT_CD='" + dept_cd + "' AND E.BUDGET_REF_NO IS NOT NULL AND (( E.EMP_STATUS != 'A' ) or (E.NOTICE_PAY_FROM IS NOT NULL)) ORDER BY E.EMP_NAME";
            New_Pending.Visible = false;
            //div_EmployeeDetails.Visible = false;
        }


        public void loadPendingRequests()
        {
            try
            {
                New_Pending.Visible = false;
                //div_EmployeeDetails.Visible = false;
                string query = "";

                DataTable dt = new DataTable();
                string roll = getroll();
                if (roll == "SUPER")
                {
                    query = "SELECT * FROM HRM_LIVE.HRM_REQUISITION_BUDGET_VIEW b";
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
                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string BUDGET_REF_NO = Ids[0];

                bool isDataLoaded = loadInfo(BUDGET_REF_NO);

                if (isDataLoaded)
                {
                    // Use ScriptManager to trigger the modal only if data is loaded
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#ta2Modal').modal('show');", true);
                }
                else
                {
                    // Show message if no data is found
                    ScriptManager.RegisterStartupScript(this, GetType(), "showNoDataToast", "showNoDataToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public bool loadInfo(string BUDGET_REF_NO)
        {
            try
            {
                bdgtcd.Text = BUDGET_REF_NO;
                budget_Code.Text = BUDGET_REF_NO;
                DataTable dt = new DataTable();
                string query = "SELECT  X.START_DATE, X.BUDGET_REF_NO, X.TOTAT_STR , SUM(NVL(X.UT_BUD,0))UT_BUD , NVL(X.TOTAT_STR,0) - SUM(NVL(X.UT_BUD,0))VACANT_BUD , X.REG_CD, X.REG, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY FROM ( select V.SANCTION_STR  TOTAT_STR ,V.REF_NO BUDGET_REF_NO, V.START_DATE, V.EMP_DIVISION_CD,  hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD ,hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD   , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD   , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD,hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  ,hrm_code_desc('DESIGNATION', V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.EMP_CD EMP_CD_TP_NO, (CASE WHEN E.EMP_CD IS NULL THEN  0 WHEN E.NOTICE_PAY_FROM IS NOT NULL THEN 0 ELSE 1 END) UT_BUD, e.emp_name, e.appointment_date appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, E.CADRE_SUBCLASS_CD ||' - '|| hrm_code_desc('CADRE_SUBCLASS', E.CADRE_SUBCLASS_CD, NULL, NULL, NULL) CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'EMP INFO' DATA_FROM from HRM_LIVE.HRM_EMPLOYEE E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO=E.BUDGET_REF_NO(+) and E.emp_status (+)  IN ('A','D') and v.STR_STATUS in ('FA') UNION ALL select V.SANCTION_STR   ,V.REF_NO BUDGET_REF_NO,V.START_DATE ,  V.EMP_DIVISION_CD,   hrm_code_desc('DIVISION', V.EMP_DIVISION_CD, NULL, NULL, NULL) BUD_DIVISION_NAME, V.EMP_LOCATION_CD  , hrm_code_desc('UNIT',  V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) BUD_UNIT_NAME, V.EMP_DEPARTMENT_CD  , hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) BUD_DEPARTMENT_NAME, V.EMP_SECTION_CD    , hrm_code_desc('SECTION', V.EMP_SECTION_CD, NULL, NULL, NULL) BUD_SECTION_NAME, V.EMP_CADRE_CD , hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) BUD_CADRE_NAME, V.EMP_CADRE_SUBCLASS_CD , hrm_code_desc('CADRE_SUBCLASS', V.EMP_CADRE_SUBCLASS_CD, NULL, NULL, NULL) BUD_CADRE_SUBCLASS, V.EMP_DESIGNATION_CD  , hrm_code_desc('DESIGNATION',  V.EMP_DESIGNATION_CD, NULL, NULL, NULL) BUD_DESIGNATION_NAME, V.RATEOFPAY, V.REG_CD, DECODE(NVL(V.REG_CD,E.REG_CD), '001', 'AK-1', '002', 'AK-2','007','AK-3','006', 'AK-RETAIL','008','SATTAR','009','AK-NOORIBAD',e.reg_cd) REG, E.TRIAL_PASS_NO, (CASE WHEN E.TRIAL_PASS_NO IS NULL THEN 0 ELSE 1 END) UT_BUD, e.name, e.FROMDATE appointment_TP_FROM_DATE, E.hrm_division_cd||' - '||   hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, E.HRM_UNIT_CD  ||' - '|| hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, E.HRM_DEPARTMENT_CD   ||' - '|| hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.HRM_SECTION_CD    ||' - '|| hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, E.HRM_CADRE_CD ||' - '|| hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, '' CADRE_SUBCLASS, E.HRM_DESIGNATION_CD  ||' - '|| hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, 'TRIAL PASS' DATA_FROM from TRIAL_PASS_INFO E,HRM_APPROVED_STRENGHT_VIEW V where V.REF_NO= E.BUDGET_REF_NO(+) and E.emp_status (+) IN ('A') and v.STR_STATUS in ('FA') ) X WHERE X.BUDGET_REF_NO= '" + BUDGET_REF_NO + "' AND NOT EXISTS ( SELECT 1 FROM HRM_JOB_REQ_TRACKING R WHERE R.BUDGET_REF_NO=X.BUDGET_REF_NO AND NVL(R.TRACKING_STATUS,'N') !='C') GROUP BY  X.TOTAT_STR ,X.BUDGET_REF_NO, X.START_DATE, X.REG, X.REG_CD, X.EMP_DIVISION_CD, X.BUD_DIVISION_NAME, X.EMP_LOCATION_CD ,X.BUD_UNIT_NAME, X.EMP_DEPARTMENT_CD   ,X.BUD_DEPARTMENT_NAME, X.EMP_SECTION_CD   , X.BUD_SECTION_NAME, X.EMP_CADRE_CD,X. BUD_CADRE_NAME, X.EMP_CADRE_SUBCLASS_CD ,X.BUD_CADRE_SUBCLASS, X.EMP_DESIGNATION_CD, X.BUD_DESIGNATION_NAME, X.RATEOFPAY HAVING   NVL(X.TOTAT_STR,0) >  SUM(NVL(X.UT_BUD,0)) order by 3";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    //div_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        current.Text = dr["UT_BUD"].ToString();
                        approved.Text = dr["TOTAT_STR"].ToString();
                        balance.Text = dr["VACANT_BUD"].ToString();
                        region.Text = dr["REG_CD"].ToString() + " - " + dr["REG"].ToString();
                        section.Text = dr["EMP_SECTION_CD"].ToString() + " - " + dr["BUD_SECTION_NAME"].ToString();
                        division.Text = dr["EMP_DIVISION_CD"].ToString() + " - " + dr["BUD_DIVISION_NAME"].ToString();
                        cadre.Text = dr["EMP_CADRE_CD"].ToString() + " - " + dr["BUD_CADRE_NAME"].ToString();
                        unit.Text = dr["EMP_LOCATION_CD"].ToString() + " - " + dr["BUD_UNIT_NAME"].ToString();
                        cadresubclass.Text = dr["EMP_CADRE_SUBCLASS_CD"].ToString() + " - " + dr["BUD_CADRE_SUBCLASS"].ToString();
                        department.Text = dr["EMP_DEPARTMENT_CD"].ToString() + " - " + dr["BUD_DEPARTMENT_NAME"].ToString();
                        designation.Text = dr["EMP_DESIGNATION_CD"].ToString() + " - " + dr["BUD_DESIGNATION_NAME"].ToString();




                        reg_cd = dr["REG_CD"].ToString();
                        div_cd = dr["EMP_DIVISION_CD"].ToString();
                        unit_cd = dr["EMP_LOCATION_CD"].ToString();
                        dept_cd = dr["EMP_DEPARTMENT_CD"].ToString();
                        sect_cd = dr["EMP_SECTION_CD"].ToString();
                        cadre_cd = dr["EMP_CADRE_CD"].ToString();
                        cadresubclass_cd = dr["EMP_CADRE_SUBCLASS_CD"].ToString();
                        desig_cd = dr["EMP_DESIGNATION_CD"].ToString();


                        // Store these values in ViewState to persist across postbacks
                        ViewState["reg_cd"] = reg_cd;
                        ViewState["div_cd"] = div_cd;
                        ViewState["unit_cd"] = unit_cd;
                        ViewState["dept_cd"] = dept_cd;
                        ViewState["sect_cd"] = sect_cd;
                        ViewState["cadre_cd"] = cadre_cd;
                        ViewState["cadresubclass_cd"] = cadresubclass_cd;
                        ViewState["desig_cd"] = desig_cd;




                        DataTable dtt = new DataTable();
                        string queryy = "SELECT E.EMP_CD, E.EMP_NAME, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME FROM HRM_EMPLOYEE E WHERE  E.REG_CD= '" + reg_cd + "' AND E.HRM_DIVISION_CD= '" + div_cd + "' AND E.HRM_UNIT_CD='" + unit_cd + "' AND E.HRM_DEPARTMENT_CD='" + dept_cd + "' AND E.HRM_CADRE_CD in ( '04', '05', '06', '07', '08','09','10') AND E.BUDGET_REF_NO IS NOT NULL AND E.EMP_STATUS = 'A' UNION SELECT EE.EMP_CD, EE.EMP_NAME, hrm_code_desc('DESIGNATION', EE.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME FROM HRM_EMPLOYEE EE WHERE  EE.HRM_CADRE_CD in ( '02', '03','04', '05', '06', '07') AND EE.EMP_STATUS = 'A' ORDER BY 2";
                        dtt = db.GetData(queryy);
                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtt.Rows)
                            {
                                string text = row["EMP_CD"].ToString() + "                - " + row["EMP_NAME"].ToString() + "                - (" + row["DESIGNATION_NAME"].ToString() + ")";
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
                    }
                    return true;
                }
                else
                {
                    // No data found
                    return false;
                }



            }
            catch (Exception ex)
            {
                // Log error
                return false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        protected void lbl_View_Click_ExEmp(object sender, EventArgs e)
        {
            try
            {
                //div_EmployeeDetails.Visible = true;



                LinkButton btn = (LinkButton)sender;
                string commandArgument = btn.CommandArgument;
                string[] args = commandArgument.Split('|');

                string BUDGET_REF_NO = args[0];
                Rep_EmpCd.Text = args[1];

                bool isDataLoaded = loadInfo(BUDGET_REF_NO);

                if (isDataLoaded)
                {
                    // Use ScriptManager to trigger the modal only if data is loaded
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#ta2Modal').modal('show');", true);
                }
                else
                {
                    // Show message if no data is found
                    ScriptManager.RegisterStartupScript(this, GetType(), "showNoDataToast", "showNoDataToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


         protected void Submit_TA1(object sender, EventArgs e)
         {
            try
            {
                string Emp_Status = ""; string Last_Duty = ""; string EMP_NAME = ""; string EMP_CD = ""; string New_ReqId = ""; string REG_CD = "", IS_CONTRACTOR = "", CONTRACTOR_CD = "", EMP_DIVISION_CD = "", EMP_LOCATION_CD = "", EMP_DEPARTMENT_CD = "", EMP_SECTION_CD = "", EMP_DESIGNATION_CD = "", EMP_CADRE_CD = "", EMP_CADRE_SUBCLASS_CD = "", ATTEND_ALLOWANCE = "", PROD_ALLOWANCE = "", OVERTIME = "", HOLIDAY_OT = "", BENEFITS = "", TOTAL_GROSS_SALARY = ""; 
                DataTable dt = new DataTable();
                string fetch_Query = "SELECT V.REG_CD, V.IS_CONTRACTOR, V.CONTRACTOR_CD, V.EMP_DIVISION_CD, hrm_code_desc('DIVISION',V.EMP_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, V.EMP_LOCATION_CD, hrm_code_desc('UNIT', V.EMP_LOCATION_CD, V.EMP_DIVISION_CD, NULL, NULL) UNIT_NAME, V.EMP_DEPARTMENT_CD, hrm_code_desc('DEPARTMENT', V.EMP_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, V.EMP_SECTION_CD, hrm_code_desc('SECTION',V.EMP_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, V.EMP_DESIGNATION_CD, hrm_code_desc('DESIGNATION', V.EMP_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, V.EMP_CADRE_CD, hrm_code_desc('CADRE', V.EMP_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, v.EMP_CADRE_SUBCLASS_CD, round(V.ATTEND_ALLOWANCE/V.SANCTION_STR)  ATTEND_ALLOWANCE,V.PROD_ALLOWANCE/V.SANCTION_STR  PROD_ALLOWANCE, round(v.TOTAL_GROSS_SALARY) AS TOTAL_GROSS_SALARY, case when nvl(v.OVERTIME,0) > 0 then round(v.OVERTIME/V.SANCTION_STR) else 0 end OVERTIME  , case when nvl(v.HOLIDAY_OT,0) > 0 then round(v.HOLIDAY_OT/V.SANCTION_STR) else 0 end HOLIDAY_OT, round(V.BENEFITS/V.SANCTION_STR) BENEFITS FROM HRM_APPROVED_STRENGHT_VIEW V WHERE V.REF_NO = '" + budget_Code.Text + "' AND V.STR_STATUS='FA'";
                dt = db.GetData(fetch_Query);
                if (dt.Rows.Count > 0)
                {
                    //div_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                         REG_CD = dr["REG_CD"].ToString();
                         IS_CONTRACTOR = dr["IS_CONTRACTOR"].ToString();
                         CONTRACTOR_CD = dr["CONTRACTOR_CD"].ToString();
                         EMP_DIVISION_CD = dr["EMP_DIVISION_CD"].ToString();
                         EMP_LOCATION_CD = dr["EMP_LOCATION_CD"].ToString();
                         EMP_DEPARTMENT_CD = dr["EMP_DEPARTMENT_CD"].ToString();
                         EMP_SECTION_CD = dr["EMP_SECTION_CD"].ToString();
                         EMP_DESIGNATION_CD = dr["EMP_DESIGNATION_CD"].ToString();
                         EMP_CADRE_CD = dr["EMP_CADRE_CD"].ToString();
                         EMP_CADRE_SUBCLASS_CD = dr["EMP_CADRE_SUBCLASS_CD"].ToString();
                         PROD_ALLOWANCE = dr["PROD_ALLOWANCE"].ToString();
                         OVERTIME = dr["OVERTIME"].ToString();
                         HOLIDAY_OT = dr["HOLIDAY_OT"].ToString();
                         BENEFITS = dr["BENEFITS"].ToString();
                         TOTAL_GROSS_SALARY = dr["TOTAL_GROSS_SALARY"].ToString();

                    }
                    DataTable dtt = new DataTable();
                    string newreqId_Query = "SELECT NVL(MAX(R.JOB_REQ_ID), 0) + 1 AS New_ReqId FROM HRM_JOB_REQUISITION R";
                    dtt = db.GetData(newreqId_Query);
                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow drr in dtt.Rows)
                        {
                            New_ReqId = drr["New_ReqId"].ToString();
                        }
                    }

                    string Query = "INSERT INTO HRM_JOB_REQUISITION (JOB_REQ_ID, REQ_DATE, BUDGET_REF_NO, REG_CD, HRM_DIVISION_CD, HRM_UNIT_CD, HRM_DEPARTMENT_CD, HRM_SECTION_CD, HRM_CADRE_CD, CADRE_SUBCLASS_CD, HRM_DESIGNATION_CD, IS_CONTRACTOR, CONTRACTOR_CD, HIRING_TYPE, GENDER, VACANCIES, REMARKS, APPROVED_BUDGETED_SALARY, L$USR_IN, L$IN_DATE, APPROVE_STR, UTILIZE_STR, ATTEND_ALLOWANCE, PROD_ALLOWANCE, BENEFITS, OVERTIME, TOTAL_GROSS_SALARY, HOLIDAY_OT, MIN_EDU_ID, MIN_EXPERIANCE, REP_TO_EMP_CD, MIN_AGE, MAX_AGE, JOB_DESCRIPTION) " +
                 "VALUES ('"+New_ReqId+"', TRUNC(SYSDATE), '" +
                 budget_Code.Text + "', '" +
                 REG_CD + "', '" +
                 EMP_DIVISION_CD + "', '" +
                 EMP_LOCATION_CD + "', '" +
                 EMP_DEPARTMENT_CD + "', '" +
                 EMP_SECTION_CD + "', '" +
                 EMP_CADRE_CD + "', '" +
                 EMP_CADRE_SUBCLASS_CD + "', '" +
                 EMP_DESIGNATION_CD + "', '" +
                 IS_CONTRACTOR + "', '" +
                 CONTRACTOR_CD + "', '" +
                 Select_Hiring.SelectedValue + "', '" +
                 DropDownGender.SelectedValue + "', '" +
                 balance.Text + "', '" +
                 TextBox12.Text + "', '" +
                 TOTAL_GROSS_SALARY + "', '" +
                 EmployeeUCode + "', TRUNC(SYSDATE), '" +
                 approved.Text + "', '" +
                 current.Text + "', '" +
                 ATTEND_ALLOWANCE + "', '" +
                 PROD_ALLOWANCE + "', '" +
                 BENEFITS + "', '" +
                 OVERTIME + "', '" +
                 TOTAL_GROSS_SALARY + "', '" +
                 HOLIDAY_OT + "', '" +
                 DropDownEdu.SelectedValue + "', '" +
                 DropDownMinExp.SelectedValue +"', '" +
                 DropDownReports.SelectedValue + "', '" +
                 TextBox14.Text + "', '" +
                 TextBox16.Text + "', '" +
                 TextBox10.Text + "')";

                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        if (Rep.Text == "R")
                        {
                            //DataTable dttt = new DataTable();
                            //string Get_Emp = "SELECT EMP_CD, EMP_NAME, Last_Duty, Emp_Status from Hrm_Employee WHERE EMP_CD ='" + Rep_EmpCd.Text + "'";
                            //dttt = db.GetData(Get_Emp);
                            //if (dttt.Rows.Count > 0)
                            //{     
                            //    foreach (DataRow drrr in dttt.Rows)
                            //    {
                            //        EMP_CD = drrr["EMP_CD"].ToString();
                            //        EMP_NAME = drrr["EMP_NAME"].ToString();
                            //        Last_Duty = drrr["Last_Duty"].ToString();
                            //        Emp_Status = drrr["Emp_Status"].ToString();
                                  
                                    
                            //    }
                            string Rep_Query = "INSERT INTO HRM_JOB_REQ_REPLACE_DTL D (D.Rep_Id, D.JOB_REQ_ID, D.EMP_CD,D.BUDGET_REF_NO, L$USR_IN, L$IN_DATE) VALUES ((SELECT NVL(MAX(R.REP_ID), 0) + 1 AS New_RepId FROM HRM_JOB_REQ_REPLACE_DTL R WHERE JOB_REQ_ID='" + New_ReqId + "'),'" + New_ReqId + "', '" + EMP_CD + "', '" + budget_Code.Text + "', '" + EmployeeUCode + "', trunc(SYSDATE))";
                                string Rep_Result = db.PostData(Rep_Query);
                                if (Result == "Done")
                                {
                                    GetHod_TA1(New_ReqId);
                                    Session["SuccessMessage"] = "Your success message here";
                                    Response.Redirect("TA_SD.aspx");
                                }
                                else
                                {
                                    Session["ErrorMessage"] = "Your success message here";
                                }
                            
                        }
                        else
                        {
                            GetHod_TA1(New_ReqId);
                            Session["SuccessMessage"] = "Your success message here";
                            Response.Redirect("TA_SD.aspx");
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
         }


         protected void ButtonReport_TA1_Click(object sender, EventArgs e)
         {
             string job_req_id="";
                DataTable dt = new DataTable();
                string query = "SELECT h.job_req_id FROM Hrm_Job_Requisition h where NVL(h.status, 'X') != 'C' AND h.budget_ref_no='"+budget_Code.Text+"' and rownum=1";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    //div_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        job_req_id = dr["job_req_id"].ToString();
                    }
                }
             string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0643.rdf&P_JOB_REQ_ID=" + job_req_id + "&paramform=no";
             Response.Redirect(reportURL);
         }

         public void GetHod_TA1(string JOB_REQ_ID)
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
                             EmailIntimationHOD_TA1(hodemail, hodname, JOB_REQ_ID);
                         }
                     }
                 }
             }

             catch
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
             }

         }

         public void EmailIntimationHOD_TA1(string hodemail, string hodname, string New_ReqId)
         {
             try
             {
                 SendResponse res = new SendResponse();
                 string sub = "HR Completed Tracking Against Requisition";
                 string eBody = string.Empty;

                 // Email Body
                 eBody += "Dear " + ToTitleCase(hodname) + ",<br><br>";
                 eBody += "New requisition has been added by your concern SD. Now you can check requisition and can perform action on it.<br>";
                 eBody += "Following are the details:<br><br>";
                         eBody += "Budget Ref #           : " + bdgtcd.Text + "<br>";
                         eBody += "Requisition ID        : " + New_ReqId + "<br>";
                         //eBody += "Tracking ID           : " + Tracking_Id + "<br>";
                         eBody += "Designation           : " + designation.Text + "<br>";
                         eBody += "Cadre                   : " + cadre.Text + "<br>";
                         eBody += "Division              : " + division.Text + "<br>";
                         eBody += "Location               : " + unit.Text + "<br>";
                         eBody += "Depatrment             : " + department.Text + "<br>";
                         eBody += "Section                 : " + section.Text + "<br><br>";
                         //eBody += "Head Count             : " + HEAD_COUNT + "<br>";
                         //eBody += "Hiring Type            : " + Rep.Text + "<br><br>";
                         // eBody += "Request Status         : " + Tracking_Status + "<br>";
                         //eBody += "Hr Remarks                 : " + REMARKS + "<br><br>";
                         //eBody += "Start Date  : " + REQ_DATE + "<br><br>";
                         // eBody += "Action  : " + Action + ", Action Date  : " + Action_Date + "<br><br>";

                         eBody += "This is a system generated e-mail, please do not reply.";

                         // Send Email
                         res.SendEmailTA2(hodemail, eBody, sub);                 
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


    }
}