(function () {
    $(document).ready(function () {
        google.charts.setOnLoadCallback(fillTransactions);
        google.charts.setOnLoadCallback(populateMailTable);
        google.charts.setOnLoadCallback(populateFNameTable);
        google.charts.setOnLoadCallback(populateLNameTable);
        google.charts.setOnLoadCallback(populateBinTable);
        google.charts.setOnLoadCallback(populateLast4Table);
        google.charts.setOnLoadCallback(populateAverageTable);
    });

    function fillTransactions() {
        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var tempData = [];
        var param = {};

        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getTransactions',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    tempData.push([msg.d[i].status, parseInt(msg.d[i].total)]);
                }
            },
            error: function (xhr) {
            },
        });


        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Transaction Type');
        data.addColumn('number', 'Totals');
        data.addRows(tempData);

        var options = { title: 'Total Number of Transactions' };

        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    }

    function getLikelyDomains(transactionType) {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var data = [];
        var param = {};

        param.transaction = transactionType;
        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getLikelyDomains',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    data.push(msg.d[i].domain);
                }
            },
            error: function (xhr) {
            },
        });

        return data;
    }

    function getLikelyFName(transactionType) {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var data = [];
        var param = {};

        param.transaction = transactionType;
        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getLikelyFirstName',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    data.push(msg.d[i].fname);
                }
            },
            error: function (xhr) {
            },
        });

        return data;
    }

    function getLikelyLName(transactionType) {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var data = [];
        var param = {};

        param.transaction = transactionType;
        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getLikelyLastName',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    data.push(msg.d[i].lname);
                }
            },
            error: function (xhr) {
            },
        });

        return data;
    }

    function getLikelyBin(transactionType) {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var data = [];
        var param = {};

        param.transaction = transactionType;
        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getLikelyBin',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    data.push(msg.d[i].bin);
                }
            },
            error: function (xhr) {
            },
        });

        return data;
    }

    function getLikelyLast4(transactionType) {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var data = [];
        var param = {};

        param.transaction = transactionType;
        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getLikelyLast4',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    data.push(msg.d[i].last4);
                }
            },
            error: function (xhr) {
            },
        });

        return data;
    }

    function populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, tableToPopulate) {
        
        var rows = Math.max(dataSuccess.length, dataSuspected.length, dataDecline.length, dataChargeback.length);

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Success');
        data.addColumn('string', 'Suspected Fraud');
        data.addColumn('string', 'Bank Decline');
        data.addColumn('string', 'Chargeback');
        data.addRows(rows);

        for (i = 0; i < dataSuccess.length; i++) {
            data.setCell(i, 0, dataSuccess[i]);
        }

        for (i = 0; i < dataSuspected.length; i++) {
            data.setCell(i, 1, dataSuspected[i]);
        }

        for (i = 0; i < dataDecline.length; i++) {
            data.setCell(i, 2, dataDecline[i]);
        }

        for (i = 0; i < dataChargeback.length; i++) {
            data.setCell(i, 3, dataChargeback[i]);
        }
        var table = new google.visualization.Table(document.getElementById(tableToPopulate));
        table.draw(data, { showRowNumber: true, width: '100%', height: '100%' });
    }

    function populateMailTable() {

        var dataSuccess = getLikelyDomains("success");
        var dataSuspected = getLikelyDomains("suspected_fraud");
        var dataDecline = getLikelyDomains("bank_decline");
        var dataChargeback = getLikelyDomains("chargeback");

        populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, 'table_mails');

    }

    function populateFNameTable() {

        var dataSuccess = getLikelyFName("success");
        var dataSuspected = getLikelyFName("suspected_fraud");
        var dataDecline = getLikelyFName("bank_decline");
        var dataChargeback = getLikelyFName("chargeback");

        populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, 'table_fname');

    }

    function populateLNameTable() {

        var dataSuccess = getLikelyLName("success");
        var dataSuspected = getLikelyLName("suspected_fraud");
        var dataDecline = getLikelyLName("bank_decline");
        var dataChargeback = getLikelyLName("chargeback");

        populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, 'table_lname');

    }

    function populateBinTable() {

        var dataSuccess = getLikelyBin("success");
        var dataSuspected = getLikelyBin("suspected_fraud");
        var dataDecline = getLikelyBin("bank_decline");
        var dataChargeback = getLikelyBin("chargeback");

        populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, 'table_bin');

    }

    function populateLast4Table() {

        var dataSuccess = getLikelyLast4("success");
        var dataSuspected = getLikelyLast4("suspected_fraud");
        var dataDecline = getLikelyLast4("bank_decline");
        var dataChargeback = getLikelyLast4("chargeback");

        populateTable(dataSuccess, dataSuspected, dataDecline, dataChargeback, 'table_last4');
    }

    function populateAverageTable() {

        var month;

        if ($('#monthFilter').is(":checked"))
            month = $("#monthSelector").val();
        else
            month = 0;

        var dataT = [];
        var param = {};

        param.month = month;

        $.ajax({
            type: 'POST',
            url: 'DataAnalysis.aspx/getTransactionAverage',
            async: false,
            data: JSON.stringify(param),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                for (i = 0; i < msg.d.length; i++) {
                    dataT.push([msg.d[i].status, parseFloat(msg.d[i].average)]);
                }
            },
            error: function (xhr) {
            },
        });

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Transaction Status');
        data.addColumn('number', 'Average Transaction Amount');
        data.addRows(dataT);

        var table = new google.visualization.Table(document.getElementById('table_average'));
        table.draw(data, { showRowNumber: true, width: '100%', height: '100%' })
    }

    $("#mainPnl").on('click', 'input[type="checkbox"]', function () {
        var chkBox = $(this);
        if (chkBox.is(":checked")) {
            $('#monthSelector').prop("disabled", false);
            $('#pnlChart').text('Transactions chart (' + $('#monthSelector option:selected').text() + ')');
            $('#pnlMail').text('Table of most likely email domains (' + $('#monthSelector option:selected').text() + ')');
            $('#pnlNames').text('Tables of most likely first & last names (' + $('#monthSelector option:selected').text() + ')');
            $('#pnlCards').text('Tables of most likely bin & last 4 digits on cards (' + $('#monthSelector option:selected').text() + ')');
            $('#pnlAvg').text('Table of average amount per transaction (' + $('#monthSelector option:selected').text() + ')');
            fillTransactions();
            populateMailTable();
            populateFNameTable();
            populateLNameTable();
            populateBinTable();
            populateLast4Table();
            populateAverageTable();
        }
        else {
            $('#monthSelector').prop("disabled", true);
            $('#pnlChart').text('Transactions chart');
            $('#pnlMail').text('Table of most likely email domains');
            $('#pnlNames').text('Tables of most likely first & last names');
            $('#pnlCards').text('Tables of most likely bin & last 4 digits on cards');
            $('#pnlAvg').text('Table of average amount per transaction');
            fillTransactions();
            populateMailTable();
            populateFNameTable();
            populateLNameTable();
            populateBinTable();
            populateLast4Table();
            populateAverageTable();
        }
    });

    $('#monthSelector').on('change', function () {
        $('#pnlChart').text('Transactions chart (' + $('#monthSelector option:selected').text() + ')');
        $('#pnlMail').text('Table of most likely email domains (' + $('#monthSelector option:selected').text() + ')');
        $('#pnlNames').text('Tables of most likely first & last names (' + $('#monthSelector option:selected').text() + ')');
        $('#pnlCards').text('Tables of most likely bin & last 4 digits on cards (' + $('#monthSelector option:selected').text() + ')');
        $('#pnlAvg').text('Table of average amount per transaction (' + $('#monthSelector option:selected').text() + ')');
        fillTransactions();
        populateMailTable();
        populateFNameTable();
        populateLNameTable();
        populateBinTable();
        populateLast4Table();
        populateAverageTable();
    });

}());