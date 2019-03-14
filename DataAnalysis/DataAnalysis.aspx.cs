using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DataAnalysis_DataAnalysis : System.Web.UI.Page
{

    static OracleConnection _cnObj;

    protected void Page_Load(object sender, EventArgs e)
    {
        MyConnection _connection;
        _connection = new MyConnection();
        _cnObj = _connection.getConnection();
    }

    [WebMethod]
    public static ArrayList getTransactions(int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "SELECT COUNT(*) AS TOTAL, " +
                "TRANSACTION_STATUS AS STATUS " +
                "FROM BAYO_DET_TRANSACTIONS ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
            
            query += "GROUP BY TRANSACTION_STATUS " +
                "ORDER BY TOTAL DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        status = _dr["STATUS"].ToString(),
                        total = _dr["TOTAL"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getLikelyDomains(string transaction, int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "select SUBSTR(email, instr(email,'@'), length(email)-1) as domain, count(*) as total " +
                "from BAYO_DET_TRANSACTIONS " +
                "where TRANSACTION_STATUS = '" + transaction + "' ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }

            query += "group by SUBSTR(email, instr(email,'@'), length(email)-1) " +
            "order by total desc, domain";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        domain = _dr["domain"].ToString(),
                        total = _dr["total"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getLikelyFirstName(string transaction, int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "SELECT SUBSTR(CONSUMER_NAME, 1, INSTR(CONSUMER_NAME, ' ', 1, 1)-1) FNAME, COUNT(*) AS TOTALS " +
                "FROM BAYO_DET_TRANSACTIONS " +
                "WHERE TRANSACTION_STATUS = '" + transaction + "' ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
   
            query += "GROUP BY SUBSTR(CONSUMER_NAME, 1, INSTR(CONSUMER_NAME, ' ', 1, 1)-1) " +
                "ORDER BY TOTALS DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        fname = _dr["FNAME"].ToString(),
                        total = _dr["TOTALS"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getLikelyLastName(string transaction, int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "select regexp_substr(trim(consumer_name), '[^ ]*$') lname, count(*) as totals " +
                "FROM BAYO_DET_TRANSACTIONS " +
                "WHERE TRANSACTION_STATUS = '" + transaction + "' ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
            
            query += "GROUP BY regexp_substr(trim(consumer_name), '[^ ]*$') " +
                "ORDER BY totals DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        lname = _dr["lname"].ToString(),
                        total = _dr["totals"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getLikelyBin(string transaction, int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "SELECT CARD_BIN, COUNT(*) AS TOTALS " +
                "FROM BAYO_DET_TRANSACTIONS " +
                "WHERE TRANSACTION_STATUS = '" + transaction + "' ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
            
            query+= "GROUP BY CARD_BIN " +
                "ORDER BY TOTALS DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        bin = _dr["CARD_BIN"].ToString(),
                        total = _dr["TOTALS"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getLikelyLast4(string transaction, int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "SELECT CARD_LAST_4 AS LAST4, COUNT(*) AS TOTALS " +
                "FROM BAYO_DET_TRANSACTIONS " +
                "WHERE TRANSACTION_STATUS = '" + transaction + "' ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "and to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
            
            query += "GROUP BY CARD_LAST_4 " +
                "ORDER BY TOTALS DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        last4 = _dr["LAST4"].ToString(),
                        total = _dr["TOTALS"].ToString()
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }

    [WebMethod]
    public static ArrayList getTransactionAverage(int month)
    {
        OracleCommand _cmd = null;
        OracleDataReader _dr = null;

        try
        {
            ArrayList data = new ArrayList();
            _cnObj.Open();

            String query = "SELECT TRANSACTION_STATUS AS STATUS,  " +
                "AVG(TRANSACTION_AMOUNT) AS AVERAGE " +
                "FROM BAYO_DET_TRANSACTIONS ";

            if (month != 0)
            {
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/31/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/30/16 23:59', 'mm/dd/yy hh24:mi') ";
                if (month == 2)
                    query += "where to_date(TRANSACTION_TIME, 'mm/dd/yy hh24:mi') between to_date('" + month + "/01/16 00:00', 'mm/dd/yy hh24:mi') and to_date('" + (month) + "/28/16 23:59', 'mm/dd/yy hh24:mi') ";
            }
            
            query += "GROUP BY TRANSACTION_STATUS " +
                "ORDER BY AVERAGE DESC";

            _cmd = new OracleCommand(query, _cnObj);
            _cmd.BindByName = true;

            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows == true)
            {
                while (_dr.Read())
                {
                    data.Add(new
                    {
                        status = _dr["STATUS"].ToString(),
                        average = _dr.GetFloat(1)
                    });
                }
            }
            return data;
        }
        finally
        {
            _cnObj.Close();
            _cmd.Dispose();
            _cmd = null;
            _dr.Dispose();
            _dr = null;
        }
    }
}