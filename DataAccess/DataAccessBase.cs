#region Usings
using System;
using System.Data;
using System.Data.SqlClient;
using Commons;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
#endregion

namespace DataAccess
{
    public class DataAccessBase
    {
        #region Variables

        protected string _connectionString;
        protected Database _dataBase;

        #endregion

        #region Constructores
        protected DataAccessBase()
        {
        }

        /// <summary>
        /// Inicializa una DB para uso del sistema basado en el
        /// connection string
        /// </summary>
        /// <param name="dbName">Connection string a utilizar</param>
        protected DataAccessBase(DataBases pDBName)
        {
            _dataBase = DatabaseFactory.CreateDatabase(StringEnum.GetStringValue(pDBName));
            _connectionString = _dataBase.ConnectionString;
        }

        protected DataAccessBase(string dbConn)
        {
            _dataBase = new SqlDatabase(dbConn);
            _connectionString = _dataBase.ConnectionString;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene la conneción en uso
        /// </summary>
        /// <returns>un SqlConnection que esta en uso</returns>
        protected SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _dataBase.ConnectionString;
            return connection;
        }

        /// <summary>
        /// Verifica si en un DataReader existe una columna
        /// </summary>
        /// <param name="reader">reader a revisar</param>
        /// <param name="column">nombre de la columna a buscar</param>
        /// <returns><b>true</b>si existe la columna en el reader, <b>false</b> si no puede encontrarla</returns>
        protected bool ContainsColumn(IDataReader reader, string column)
        {
            bool hasColumn = false;
            for (int index = 0; index < reader.FieldCount; index++)
            {
                string name = reader.GetName(index);
                if (name.ToUpper() == column.ToUpper())
                    hasColumn = true;
            }
            return hasColumn;
        }

        /// <summary>
        /// Verifica la que la conexion a un server este activa
        /// </summary>
        /// <param name="dbConn">Connection string de la db a verificar</param>
        /// <returns>True si se puede conectar</returns>
        public bool CanConnect(string dbConn)
        {
            bool val = false;
            try
            {
                SqlConnection conn = new SqlConnection(dbConn);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    val = true;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                val = false;
            }
            return val;
        }
        #endregion
    }
}
