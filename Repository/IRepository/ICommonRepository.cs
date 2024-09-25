using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MasterAPI.Repository.IRepository
{
    public interface ICommonRepository
    {
        public string Decrypt(string strEncrypted, string strKey);
        public bool OpenConnection();
        public void CloseConnection();
        public int ExecuteNonQuerry(SqlCommand com);
        public DataTable SelectDataTable(SqlCommand com);
        public DataSet SelectDataSet(SqlCommand com);
        public int InsertErrorLog(string KioskID, string method, string detail);

    }
}
