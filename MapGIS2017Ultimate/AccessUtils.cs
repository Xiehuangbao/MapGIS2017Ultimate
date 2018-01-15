using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
namespace MapGIS2017Ultimate
{
    class AccessUtils
    {

        //��ȡ���ݿ�����
        public static OleDbConnection GetConn(string path)
        {
            OleDbConnection Conn = null;
            string ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=False";
            Conn = new OleDbConnection(ConnectionString);
            Conn.Open();
            return Conn;
        }

        //��ȡOleDbDataReader
        public static OleDbDataReader GetDataReader(string Sql,OleDbConnection Conn)
        {
            OleDbCommand Comm = null;
            OleDbDataReader Adr = null;
            Comm = new OleDbCommand(Sql, Conn);
            Adr = Comm.ExecuteReader();
            return Adr;
        }
    }

 


}
