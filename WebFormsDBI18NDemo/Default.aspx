<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsDBI18NDemo.WebForm1" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CMS Demo</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css" integrity="sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ" crossorigin="anonymous"/>
    <link rel="stylesheet" href="styles/styles.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js" integrity="sha384-vBWWzlZJ8ea9aCX4pEW3rVHjgjt7zpkNpZk+02D9phzyeVkE+jo0ieGizqPLForn" crossorigin="anonymous"></script>
</head>
<body>
    <script runat="server">
        
        protected override void InitializeCulture()
        {
            if (Request.Form["ListBox1"] != null)
            {
                String selectedLanguage = Request.Form["ListBox1"];
                UICulture = selectedLanguage;
                Culture = selectedLanguage;
               
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            }
            base.InitializeCulture();
        }
    </script>
    
    <div class="container">
        <div class="jumbotron">
            <h4 class="display-3">Localization Demo</h4>
            <p class="lead">This is a simple demo that showcases how .net handles localization</p>
            <hr class="my-4" />
            <div class="row">
                <div class="col">
                    <p class="lead">Language/Region Selections</p>
                    <form id="form1" runat="server">
                        <asp:ListBox ID="ListBox1" CssClass="form-control locale-selector" runat="server">
                            <asp:ListItem Value="">Default Locale</asp:ListItem>
                            <asp:ListItem Value="en-US">English - United States - en-US</asp:ListItem>
                            <asp:ListItem Value="es-US">Espanol - United States - es-US</asp:ListItem>
                            <asp:ListItem Value="es">Espanol - es</asp:ListItem>
                            <asp:ListItem Value="en-GB">English - United Kingdom - en-GB</asp:ListItem>
                        </asp:ListBox>
                        <br />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Set Language" meta:resourcekey="Button1" />
                        <br />
                    </form>
                </div>
                <div class="col">
                    <p class="lead">Output</p>
                    <div class="card">
                        <div class="card-block">
                            <asp:Label ID="keyTitle" runat="server" Font-Bold="true" Text="Message Key Value: "/>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:CommonTerms, helloWorld %>" />
                            <br />
                            <asp:Label ID="localTitle" runat="server" Font-Bold="true" Text="Current Browser Locale: "/><asp:Label ID="locale" runat="server" Text="locale" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
