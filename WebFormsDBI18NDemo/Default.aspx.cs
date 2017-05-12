using System;
using System.Threading;

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