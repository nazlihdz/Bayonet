using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MyConnection
/// </summary>
public class MyConnection
{

    private OracleConnection cnObj { get; set; }

	public OracleConnection getConnection()
	{
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["BAYONET_TEST"].ToString();

        cnObj = new OracleConnection(connection);

        return cnObj;
	}
}