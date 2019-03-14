<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataAnalysis.aspx.cs" Inherits="DataAnalysis_DataAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <br />
    
    <div class="container" id="Page_Header" style="padding-left:7%; padding-right:7%;">
        <div class="row" >
            <div class="col-12 ">
                <div class="page-header text-center" style="margin: 1px 0 20px;">
                    <h4 >Bayonet Data Analysis</h4>
                </div>
            </div> 
        </div>
    </div>
    
    <div class="container" style="padding-left:7%; padding-right:7%;" id="dataAnalysisPnl">
        <div class="panel panel-default">
            <div id="mainPnl" class="panel-heading">
                <h4 class="panel-title">
                    <a id="pnlChart" style="font-size:small; text-decoration:none"; data-toggle="collapse">Transactions chart</a>
                    <input id="monthFilter" type="checkbox" />
                        <select id="monthSelector" disabled="disabled"> 
                            <option value="1">January</option>
                            <option value="2">February</option>
                            <option value="3">March</option>
                            <option value="4">April</option>
                            <option value="5">May</option>
                            <option value="6">June</option>
                            <option value="7">July</option>
                            <option value="8">August</option>
                            <option value="9">September</option>
                            <option value="10">October</option>
                            <option value="11">November</option>
                            <option value="12">December</option>
                        </select>
                </h4>
            </div>
            <div id="chart_div"></div>
        </div>

        <div class="panel panel-default">
            <div id="mailsPnl" class="panel-heading">
                <h4 class="panel-title">
                    <a id="pnlMail" style="font-size:small; text-decoration:none"; data-toggle="collapse">Table of most likely email domains</a>
                </h4>
            </div>
                <div class="row" id="table_mails"></div>
        </div>
        
        <br />
        
        <div class="panel panel-default">
            <div id="namesPnl" class="panel-heading">
                <h4 class="panel-title">
                    <a id="pnlNames" style="font-size:small; text-decoration:none"; data-toggle="collapse">Tables of most likely first & last names</a>
                </h4>
            </div>
            <div class="row" id="names">
                <div class="col-sm-6" id="table_fname"></div>
                <div class="col-sm-6" id="table_lname"></div>
            </div>
        </div>
        
        <br /> 

        <div class="panel panel-default">
            <div id="cardsPnl" class="panel-heading">
                <h4 class="panel-title">
                    <a id="pnlCards" style="font-size:small; text-decoration:none"; data-toggle="collapse">Tables of most likely bin & last 4 digits on cards</a>
                </h4>
            </div>
            <div class="row" id="cards">
                <div class="col-sm-6" id="table_bin"></div>
                <div class="col-sm-6" id="table_last4"></div>
            </div>
        </div>
        
        <br />
        
        <div id="pnlAverage" class="panel panel-default">
            <div id="avgPnl" class="panel-heading">
                <h4 class="panel-title">
                    <a id="pnlAvg" style="font-size:small; text-decoration:none"; data-toggle="collapse">Table of average amount per transaction</a>
                </h4>
            </div> 
            <div class="row">
                <div class="col-sm-8" id="table_average"></div>
            </div>
        </div>
    </div>

    <br /><br /><br />

    <script type = "text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type = "text/javascript">google.charts.load('current', { packages: ['corechart'] });</script>
    <script type = "text/javascript">google.charts.load('current', { packages: ['table'] });</script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/javascript/DataAnalysis.js") %>"></script>
    
</asp:Content>