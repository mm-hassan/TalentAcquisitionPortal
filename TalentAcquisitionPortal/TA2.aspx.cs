using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class TA2 : System.Web.UI.Page
    {
        Database db = new Database();
        string jobreqid;
        string hodapp;
        string EmployeeCode;
        string EmployeeUCode;
        string cadreno;
        string super;

        protected void Page_Load(object sender, EventArgs e)
        {

            getSession();

            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                loadPendingRequests();
                div_EmployeeDetails.Visible = false;

                //LoadDataIntoGridView();
                //BindDropDownWithData();
                if (Session["SuccessMessage"] != null)
                {
                    string successMessage = Session["SuccessMessage"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("SuccessMessage");
                }

                if (Session["showInfoToast"] != null)
                {
                    string successMessage = Session["showInfoToast"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showInfoToast", "showInfoToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("showInfoToast");
                }

                if (Session["showWarningToast"] != null)
                {
                    string successMessage = Session["showWarningToast"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showWarningToast", "showWarningToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("showWarningToast");
                }
                
            }

            //ClearCheckBoxes();
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
            string finalroll="";
            try
            {
                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                dt = null;

                string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW T WHERE T.EMP_CD= '" + EmployeeCode + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                        DataRow firstRow = dt.Rows[0]; // Access the first row directly
                        finalroll = firstRow["ROLL"].ToString();

                        foreach (DataRow drr in dt.Rows)
                        {
                            roll = drr["ROLL"].ToString();
                            if (roll == "SUPER")
                            {
                                finalroll = roll;
                                break;
                            }
                            else if (roll == "HOD")
                            {
                                finalroll = roll;
                                break;
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

                if (hidden.Text == "true")
                {
                    string query = "SELECT A.JOB_REQ_ID, D.DEPARTMENT_NAME, F.DESIGNATION_NAME, A.* FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD where nvl(A.HOD_APPROVED,'x') !='Y' AND JOB_REQ_ID='" + JOB_REQ_ID + "'";

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
                    string query = "select e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies req_strength, e.approve_str, e.utilize_str from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='N' and e.job_req_id not in ( select t.job_req_id from HRM_JOB_REQ_TRACKING t where t.req_status='C' ) and ( (e.reg_cd, e.hrm_division_cd,e.hrm_unit_cd,e.hrm_department_cd) in  (select b.reg_cd, b.division_cd, b.unit_cd, b.department_cd from hrm_department b, users u where u.emp_cd = to_char(b.emp_hod13) and u.usr_cd='" + EmployeeUCode + "' UNION ALL SELECT E.REG_CD, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, users u WHERE SD.DETAIL_NAME=TO_CHAR(T.EMP_CD) AND SD.SEQ_NO=153 AND u.emp_cd = TO_CHAR(T.EMP_CD) AND T.REG_CD=E.REG_CD AND T.HRM_DIVISION_CD=E.HRM_DIVISION_CD AND T.HRM_UNIT_CD=E.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD=E.HRM_DEPARTMENT_CD and u.usr_cd='" + EmployeeUCode + "'  )) union all select e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies req_strength, e.approve_str, e.utilize_str from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='N' and e.job_req_id not in ( select t.job_req_id from HRM_JOB_REQ_TRACKING t where t.req_status='C' ) and (e.reg_cd, e.hrm_division_cd,e.hrm_unit_cd,e.hrm_department_cd) in  (select b.reg_cd, b.hrm_division_cd,b.hrm_unit_cd, b.hrm_department_cd from hrm_department_budget b, users u where u.emp_cd = to_char(b.emp_hod) and u.usr_cd='" + EmployeeUCode + "' UNION ALL select b.reg_cd, b.hrm_division_cd,b.hrm_unit_cd, b.hrm_department_cd from hrm_department_budget b, users u where u.emp_cd = to_char(b.Nominate_Emp_Code) AND JOB_REQ_ID='" + JOB_REQ_ID + "' and u.usr_cd='" + EmployeeUCode + "' ) order by 1 desc";

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
            try{
                EmployeeCode = Session["EmployeeCode"].ToString();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM Hrm_Setup_Detl N WHERE N.Detail_Name='" + EmployeeCode + "' and (N.Seq_No=121 or N.Seq_No=141)";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    hidden.Text = "true";
                    DataTable dtt = new DataTable();
                    string queryy = "SELECT A.JOB_REQ_ID, D.DEPARTMENT_NAME, F.DESIGNATION_NAME, A.* FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD where nvl(A.HOD_APPROVED,'x') !='Y'";
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
                    string queryyy = "select e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies req_strength, e.approve_str, e.utilize_str from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='N' and e.job_req_id not in ( select t.job_req_id from HRM_JOB_REQ_TRACKING t where t.req_status='C' ) and ( (e.reg_cd, e.hrm_division_cd,e.hrm_unit_cd,e.hrm_department_cd) in  (select b.reg_cd, b.division_cd, b.unit_cd, b.department_cd from hrm_department b, users u where u.emp_cd = to_char(b.emp_hod13) and u.usr_cd='" + EmployeeUCode + "' UNION ALL SELECT E.REG_CD, E.HRM_DIVISION_CD, E.HRM_UNIT_CD, E.HRM_DEPARTMENT_CD FROM HRM_EMPLOYEE T, HRM_SETUP_DETL SD, users u WHERE SD.DETAIL_NAME=TO_CHAR(T.EMP_CD) AND SD.SEQ_NO=153 AND u.emp_cd = TO_CHAR(T.EMP_CD) AND T.REG_CD=E.REG_CD AND T.HRM_DIVISION_CD=E.HRM_DIVISION_CD AND T.HRM_UNIT_CD=E.HRM_UNIT_CD AND T.HRM_DEPARTMENT_CD=E.HRM_DEPARTMENT_CD and u.usr_cd='" + EmployeeUCode + "'  )) union all select e.job_req_id, e.budget_ref_no, e.req_date, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, e.hrm_cadre_cd, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, E.TOTAL_GROSS_SALARY, e.vacancies req_strength, e.approve_str, e.utilize_str from HRM_JOB_REQUISITION e where nvl(e.hod_approved,'N') ='N' and e.job_req_id not in ( select t.job_req_id from HRM_JOB_REQ_TRACKING t where t.req_status='C' ) and (e.reg_cd, e.hrm_division_cd,e.hrm_unit_cd,e.hrm_department_cd) in  (select b.reg_cd, b.hrm_division_cd,b.hrm_unit_cd, b.hrm_department_cd from hrm_department_budget b, users u where u.emp_cd = to_char(b.emp_hod) and u.usr_cd='" + EmployeeUCode + "' UNION ALL select b.reg_cd, b.hrm_division_cd,b.hrm_unit_cd, b.hrm_department_cd from hrm_department_budget b, users u where u.emp_cd = to_char(b.Nominate_Emp_Code) and u.usr_cd='" + EmployeeUCode + "' ) order by 1 desc";
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


        public void loadInfo(string JOB_REQ_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.*, A.HOD_APPROVED, B.DIVISION_NAME, C.UNIT_NAME, D.DEPARTMENT_NAME, E.SECTION_NAME, F.DESIGNATION_NAME, G.CADRE_NAME, I.REG_NAME, hrm_code_desc('EMP_NAME', A.rep_to_emp_cd, NULL, NULL, NULL) AS EMP_NAME, hrm_code_desc('CADRE_SUBCLASS', A.cadre_subclass_cd , NULL, NULL, NULL) AS CADRE_SUBCLASS_NAME, (SELECT v.SANCTION_STR FROM hrm_approved_strenght_view v WHERE v.REF_NO = 53866) - (SELECT COUNT(e.emp_cd) FROM hrm_employee e WHERE e.budget_ref_no = 53866 AND e.emp_status = 'A' AND e.notice_pay_from IS NULL) AS result FROM HRM_JOB_REQUISITION A INNER JOIN HRM_DIVISION_mst B ON A.HRM_DIVISION_CD = B.DIVISION_CD INNER JOIN HRM_UNIT_mst C ON A.HRM_UNIT_CD = C.UNIT_CD AND B.DIVISION_CD = C.DIVISION_CD INNER JOIN HRM_DEPARTMENT_mst D ON A.HRM_DEPARTMENT_CD = D.DEPARTMENT_CD INNER JOIN HRM_SECTION_MST E ON A.HRM_SECTION_CD = E.SECTION_CD INNER JOIN HRM_DESIGNATION_mst F ON A.HRM_DESIGNATION_CD = F.DESIGNATION_CD INNER JOIN hrM_cadre_mst G ON A.HRM_CADRE_CD = G.CADRE_CD INNER JOIN REGIONS I ON A.REG_CD = I.REG_CD WHERE A.JOB_REQ_ID = '" + JOB_REQ_ID + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {

                    div_EmployeeDetails.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        cadreno = dr["HRM_CADRE_CD"].ToString();

                        Text29.Text = dr["JOB_REQ_ID"].ToString();
                        Text30.Text = dr["VACANCIES"].ToString();
                        Text31.Text = dr["REQ_DATE"].ToString();
                        TextBox11.Text = dr["BUDGET_REF_NO"].ToString();
                        Text21.Text = dr["REG_CD"].ToString() + " - " + dr["REG_NAME"].ToString();
                        Text43.Text = dr["HRM_DIVISION_CD"].ToString() + " - " + dr["DIVISION_NAME"].ToString();
                        Text44.Text = dr["HRM_UNIT_CD"].ToString() + " - " + dr["UNIT_NAME"].ToString();
                        Text45.Text = dr["HRM_DEPARTMENT_CD"].ToString() + " - " + dr["DEPARTMENT_NAME"].ToString();
                        Text46.Text = dr["HRM_SECTION_CD"].ToString() + " - " + dr["SECTION_NAME"].ToString(); ;
                        Text47.Text = dr["HRM_CADRE_CD"].ToString() + " - " + dr["CADRE_NAME"].ToString();
                        Text48.Text = dr["CADRE_SUBCLASS_CD"].ToString() + " - " + dr["CADRE_SUBCLASS_NAME"].ToString();
                        Text49.Text = dr["HRM_DESIGNATION_CD"].ToString() + " - " + dr["DESIGNATION_NAME"].ToString();
                        TextBox8.Text = dr["REQ_DATE"].ToString();
                        TextBox12.Text = dr["REMARKS"].ToString();
                        TextBox6.Text = dr["MIN_EDU_ID"].ToString();
                        TextBox8.Text = dr["MIN_EXPERIANCE"].ToString();
                        TextBox3.Text = dr["APPROVED_BUDGETED_SALARY"].ToString();
                        TextBox17.Text = dr["TOTAL_GROSS_SALARY"].ToString();
                        TextBox5.Text = dr["PROD_ALLOWANCE"].ToString();
                        TextBox18.Text = dr["ATTEND_ALLOWANCE"].ToString();
                        TextBox7.Text = dr["OVERTIME"].ToString();
                        TextBox9.Text = dr["BENEFITS"].ToString();
                        Text32.Text = dr["APPROVE_STR"].ToString();
                        Text33.Text = dr["UTILIZE_STR"].ToString();
                        TextBox12.Text = dr["REMARKS"].ToString();
                        TextBox1.Text = dr["REP_TO_EMP_CD"].ToString() + " - " + dr["EMP_NAME"].ToString();
                        TextBox10.Text = dr["JOB_DESCRIPTION"].ToString();
                        TextBox14.Text = dr["MIN_AGE"].ToString();
                        TextBox16.Text = dr["MAX_AGE"].ToString();
                        TextBox19.Text = dr["HOLIDAY_OT"].ToString();
                        //TextBox2.Text = dr["NOTES_BY_HR"].ToString();
                        TextBox13.Text = dr["HOD_APPROVED"].ToString();
                        Text34.Text = dr["result"].ToString();



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
                                TextBox20.Text = "New";
                            }

                            else if (dr["HIRING_TYPE"].ToString() == "R")
                            {
                                TextBox20.Text = "Replacement";
                                bindhiringgrid(JOB_REQ_ID);

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
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
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



        protected void lbl_View_Click(object sender, EventArgs e)
        {
            try
            {
                string ROWID = (sender as LinkButton).CommandArgument;
                string[] Ids = ROWID.Split('-');
                string JOB_REQ_ID = Ids[0].ToString();
                loadInfo(JOB_REQ_ID);




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                jobreqid = Text29.Text;
                hodapp = TextBox13.Text;

                if (TextBox13.Text == "Y")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (TextBox13.Text != "Y")
                {
                    
                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_APPROVED = 'Y', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Approved !');", true);
                        GetHrbp();
                        Session["SuccessMessage"] = "Your success message here";
                        Response.Redirect("TA2.aspx");
                        Session["SuccessMessage"] = "Your success message here";
                    }
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        protected void ButtonHold_Click(object sender, EventArgs e)
        {
            try
            {
                jobreqid = Text29.Text;
                hodapp = TextBox13.Text;

                if (TextBox13.Text == "H")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (TextBox13.Text != "H")
                {

                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_APPROVED = 'H', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        Session["showInfoToast"] = "Your success message here";
                        Response.Redirect("TA2.aspx");
                        Session["showInfoToast"] = "Your success message here";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        protected void ButtonReject_Click(object sender, EventArgs e)
        {
            try
            {
                jobreqid = Text29.Text;
                hodapp = TextBox13.Text;

                if (TextBox13.Text == "R")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Already Approved');", true);
                }
                else if (TextBox13.Text != "R")
                {

                    string Query = "UPDATE HRM_JOB_REQUISITION SET HOD_APPROVED = 'R', HOD_APPROVED_ON= SYSDATE , HOD_USR_CD='" + EmployeeUCode + "'  WHERE JOB_REQ_ID  = '" + jobreqid + "' ";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        Session["showWarningToast"] = "Your success message here";
                        Response.Redirect("TA2.aspx");
                        Session["showWarningToast"] = "Your success message here";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            jobreqid = Text29.Text;



            string reportURL = @"http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0643.rdf&P_JOB_REQ_ID=" + jobreqid + "&paramform=no";
            Response.Redirect(reportURL);




        }





        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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





        void GetHrbp()
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
                        string hrname = dr["EMP_NAME"].ToString();
                        string hremail = dr["e_Mail"].ToString();
                        if (hremail != "")
                        {
                            EmailIntimationHOD(hremail);
                        }
                    }
                }
            }

            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }









        public void EmailIntimationHOD(string hremail)
        {
            try
            {

                SendResponse res = new SendResponse();

                string sub = "HOD Approved Requisition";

                //#### Email Body
                string eBody = string.Empty;
                //eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";
                eBody += "New requisition request has been submitted for your working, following are the details. " + "<br /><br />";
                eBody += "Budget Code : " + TextBox11 + "<br />";
                eBody += "Job Req. ID : " + Text29.Text + "<br />";
                eBody += "Request Date : " + Text31.Text + "<br />";
                eBody += "Hiring Type : " + TextBox20.Text + "<br />";
                eBody += "No. of Vacancies : " + Text30.Text + "<br />";
                eBody += "Designation : " + Text49.Text + "<br />";
                eBody += "Cadre : " + Text47.Text + "<br />";
                eBody += "Division : " + Text43.Text + "<br />";
                eBody += "Department : " + Text45.Text + "<br />";
                eBody += "Section : " + Text45.Text + "<br />";
                eBody += "Approved Head Count : " + Text32.Text + "<br />";
                eBody += "Current Head Count : " + Text33.Text + "<br />";
                eBody += "Balance Head Count : " + Text34.Text + "<br />";
                eBody += "Remarks : " + TextBox12.Text + "<br />";
                eBody += " **System Generated Email** ";


                //res.SendEmail(dr["email_addr"].ToString(), eBody);
                //res.SendEmailTA2(hremail, eBody, sub);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }













    }
}


