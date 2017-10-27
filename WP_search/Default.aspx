<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WP_search._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="SearchWordTextBox" runat="server"></asp:TextBox>
&nbsp;&nbsp;
                <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
            </p>
            <p>
                &nbsp;</p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
            <div>
                <h3>Posts : <asp:Label ID="PostCountLabel1" runat="server" Text="Label"></asp:Label> </h3>
                <br>
                <h3>Pages :  <asp:Label ID="MatchedPagesLabel1" runat="server" Text="Label"></asp:Label> / <asp:Label ID="PagesCountLabel1" runat="server" Text="Label"></asp:Label></h3>
            </div>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
            <div>
                 <asp:Repeater id="postrptResults" runat="server">
                     <ItemTemplate>
                         <h4 class="the-title"><a href="<%# DataBinder.Eval(Container.DataItem, "link") %>"><%# DataBinder.Eval(Container.DataItem, "title.rendered") %></a></h4>
                     </ItemTemplate>
                 </asp:Repeater>
            </div>
            ----------------------------------------- end --------------------------------------
             <div>
                 <asp:Repeater id="PageRepeater" runat="server">
                     <ItemTemplate>
                         <h4 class="the-title"><a href="<%# DataBinder.Eval(Container.DataItem, "link") %>"><%# DataBinder.Eval(Container.DataItem, "title.rendered") %></a></h4>
                     </ItemTemplate>
                 </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
