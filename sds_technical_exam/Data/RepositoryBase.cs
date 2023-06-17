using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace sds_technical_exam.Data
{
    public class RepositoryBase
    {
        protected readonly IDbConnection dbConnection;
        protected readonly  SqlConnection SqlConnection;
        protected SqlCommand SqlCommand;
        protected SqlDataReader DataReader;

        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["localDB"].ToString();

        public RepositoryBase()
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        protected bool OpenDBConnection()
        {
            try
            {
                if (SqlConnection != null)
                {
                    SqlConnection.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        protected bool CloseDBConnection()
        {
            try
            {
                if (SqlConnection != null)
                    SqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected int ExecuteNonQuery(string query)
        {
            SqlCommand = new SqlCommand(query, SqlConnection);
            return SqlCommand.ExecuteNonQuery();
        }

        protected int ExecuteNonQuery(string query, IDictionary<string, object> parameters)
        {
            SqlCommand = new SqlCommand(query, SqlConnection);
            foreach (var p in parameters)
            {
                SqlCommand.Parameters.AddWithValue(p.Key, p.Value);
            }

            return SqlCommand.ExecuteNonQuery();
        }

        protected SqlDataReader ExecuteReader(string query, IDictionary<string, object> parameters)
        {
            SqlCommand = new SqlCommand(query, SqlConnection);
            foreach (var p in parameters)
            {
                SqlCommand.Parameters.AddWithValue(p.Key, p.Value);
            }
            return SqlCommand.ExecuteReader();
        }

        protected SqlDataReader ExecuteReader(string query)
        {
            SqlCommand = new SqlCommand(query, SqlConnection);
            return SqlCommand.ExecuteReader();
        }

        //protected int AddEntity<T>(T entity)
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        int? id = SqlConnection.Insert<T>(entity);

        //        CloseDBConnection();

        //        return id != null ? (int)id : -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }
        //}

        //protected int UpdateEntity<T>(T entity)
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        SqlConnection.Update<T>(entity);

        //        CloseDBConnection();

        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }
        //}

        //protected List<T> GetList<T>()
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        List<T> list = SqlConnection.GetList<T>().ToList();

        //        CloseDBConnection();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //protected List<T> GetList<T>(object whereConditions)
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        List<T> list = SqlConnection.GetList<T>(whereConditions).ToList();

        //        CloseDBConnection();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //protected T GetEntityByID<T>(int ID)
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        T entity = SqlConnection.Get<T>(ID);

        //        CloseDBConnection();

        //        return entity;
        //    }
        //    catch (Exception ex)
        //    {
        //        return default;
        //    }
        //}

        //protected int DeleteEntity<T>(int ID)
        //{
        //    try
        //    {
        //        OpenDBConnection();

        //        int rowsAffected = SqlConnection.Delete<T>(ID);

        //        CloseDBConnection();

        //        return rowsAffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        return default;
        //    }
        //}
    }
}