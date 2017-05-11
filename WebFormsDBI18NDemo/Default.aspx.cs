using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsDBI18NDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Current locale: " + Thread.CurrentThread.CurrentCulture.Name);
            locale.Text = Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}