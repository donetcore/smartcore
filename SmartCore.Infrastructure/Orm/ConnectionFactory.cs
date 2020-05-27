﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartCore.Infrastructure.Orm
{
    public class ConnectionFactory
    {
        #region 全局变量
        /// <summary>
        /// 主库数据库连接字符串 from 配置文件
        /// </summary>
        private static readonly string connectionString = "";// Geely.Com.Config.BaseConfigs.AppMasterDbConnection;
        #endregion

        #region 属性
        /// <summary>
        /// 主库连接字符串 属性  from 配置文件
        /// </summary>
        public static string ConnectionString
        {
            get { return ""; }
        }
        /// <summary>
        /// end库连接字符串 属性  from 配置文件
        /// </summary>
        public static string EndDbConnectionString
        {
            get { return ""; }
        }
        #endregion

        #region 创建数据库连接
        /// <summary>
        /// 创建数据库连接
        /// </summary> 
        /// <param name="connectionString">连接字符串</param>
        /// <returns>IDbConnection</returns>
        public static IDbConnection CreateConnection(DatabaseType dbType,string connectionString)
        {

            try
            {
                IDbConnection conn = null;
                switch (dbType)
                {
                    case DatabaseType.SqlServer:
                        conn = new System.Data.SqlClient.SqlConnection(connectionString);
                        break;
                    case DatabaseType.MySql:
                        conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                        break;
                    case DatabaseType.Oracle:
                        //connection = new Oracle.DataAccess.Client.OracleConnection(strConn);
                        //connection = new System.Data.OracleClient.OracleConnection(strConn);
                        break;
                    case DatabaseType.DB2:
                        //conn = new System.Data.OleDb.OleDbConnection(connectionString);
                        break;
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                return conn;
            }
            catch (Exception ex)
            {
                throw new DataException(string.Format("CreateConnection Unable to open connection to {0}.", connectionString), ex);
            }
        }
        #endregion

        #region 打开默认的数据库连接
        /// <summary>
        /// 打开默认的数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenConnection()
        {
            return CreateConnection(DatabaseType.SqlServer,ConnectionString);
        }
        #endregion

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns></returns>
        private static DatabaseType GetDataBaseType(string databaseType)
        {
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }
        private T UseDbConnection<T>(Func<IDbConnection, T> queryOrExecSqlFunc)
        {
            IDbConnection dbConn = null;

            try
            {
                //Type modelType = typeof(T);
                //var typeMap = Dapper.SqlMapper.GetTypeMap(modelType);
                //if (typeMap == null || !(typeMap is ColumnAttributeTypeMapper<T>))
                //{
                //    Dapper.SqlMapper.SetTypeMap(modelType, new ColumnAttributeTypeMapper<T>());
                //} 
                dbConn = OpenConnection();  
                return queryOrExecSqlFunc(dbConn);
            }
            catch(Exception ex)
            {
                throw ex;
            } 
        } 
    }

    public enum DatabaseType
    {
        SqlServer,  //SQLServer数据库
        MySql,      //Mysql数据库
        Npgsql,     //PostgreSQL数据库
        Oracle,     //Oracle数据库
        Sqlite,     //SQLite数据库
        DB2         //IBM DB2数据库
    }

}