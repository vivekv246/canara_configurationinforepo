using MasterAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
//using System.Data.SqlClient;

namespace MasterAPI.Repository.Common
{
    public class CommonRepository : ICommonRepository
    {
    
        private readonly IConfiguration _configuration;

        public string m_connectionString = "";
        SqlConnection con;
        SqlCommand cmd;
        int OperationCommandTime = 0;

        public CommonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            try
            {
                m_connectionString = _configuration.GetConnectionString("Defaultconnection");
                string[] ConString = m_connectionString.Split(';');
             //   m_connectionString = ConString[0] + ";" + ConString[1] + ";" + ConString[2] + ";" + "Password=" + Decrypt(ConString[3].Substring(12, ConString[3].Length - 12), "CMS") + ";";


            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }

        }

        public string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
        public bool OpenConnection()
        {

            try
            {
                con = new SqlConnection();
                con.ConnectionString = m_connectionString;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else
                {
                    con.Open();
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public void CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int ExecuteNonQuerry(SqlCommand com)
        {
            int Rowcount = 0;
            try
            {
                OpenConnection();
                com.Connection = con;
                Rowcount = com.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                CloseConnection();
            }
            finally
            {
                CloseConnection();
            }
            return Rowcount;
        }
        public DataTable SelectDataTable(SqlCommand com)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                com.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = com;
                da.Fill(dt);
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        public DataSet SelectDataSet(SqlCommand com)
        {
            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                com.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = com;
                da.Fill(ds);
                CloseConnection();
            }
            catch (Exception ex)
            {

                CloseConnection();
                throw new Exception(ex.ToString());
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }
        public int InsertErrorLog(string KioskID, string method, string detail)
        {
            int RowCount = 0;
            try
            {
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "SP_INS_ErrorLog";
                com.Parameters.AddWithValue("@KioskID", KioskID);
                com.Parameters.AddWithValue("@Method", method);
                com.Parameters.AddWithValue("@ErrorDetail", detail);
                RowCount = ExecuteNonQuerry(com);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RowCount;
        }
    }
}
