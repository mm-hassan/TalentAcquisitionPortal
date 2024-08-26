using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalentAcquisitionPortal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string data= text1.Text;

            string script = "alert('text box value is, '" + data + "'')";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", script, true);
        }
    }
}