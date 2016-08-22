﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OauthManager.aspx.cs" Inherits="IDGOauthSample.OauthManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
    <script type="text/javascript" src="https://appcenter.intuit.com/Content/IA/intuit.ipp.anywhere.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {

                intuit.ipp.anywhere.setup({
                     grantUrl: '<%=  GrantUrl  %>',
                    datasources: {
                        quickbooks: true,
                        payments: false
                    }
                });


            });
    </script>
    <% if (HttpContext.Current.Session["accessToken"] != null)
       {
           Response.Write("<script> window.opener.location.reload();window.close(); </script>");
       }

    %>    
    <title>Intuit Developer Group Oauth Sample Code</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div runat="server" id="c2qb">
     <ipp:connectToIntuit></ipp:connectToIntuit><br/>
      Access Token = <%= HttpContext.Current.Session["accessToken"]  %>  <br/>
      Access Token Secret = <%= HttpContext.Current.Session["accessTokenSecret"]  %>
        <br />
    </div>
    <div runat="server" id="disconnect" visible="false">
        <asp:Button ID="btnDisconnect" runat="server" OnClick="btnDisconnect_Click" Text="Disconnect" /><br/>
        Access Token = <%= HttpContext.Current.Session["accessToken"]  %>   <br/>
        Access Token Secret = <%= HttpContext.Current.Session["accessTokenSecret"]  %>
    </div> 
      
        
    </div>

          
    <p>
    <asp:label runat="server" id="lblDisconnect" visible="false">"Your application is disconnected!"</asp:label>
    </p>   

        
    </form>
</body>
</html>
