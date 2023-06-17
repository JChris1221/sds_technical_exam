using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web;
using sds_technical_exam.Models;
using sds_technical_exam.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections;

namespace sds_technical_exam.Data
{
    public class RecyclableItemRepository : RepositoryBase, IRepository<RecyclableItem>
    {
        public ErrorLog ErrorLog = new ErrorLog();

        #region CRUD Functions

        public int Add(RecyclableItem entity)
        {
            int r;
            string proc_name = "spInsert_Recyclable_Item";

            var param = new Dictionary<string, object>{
                    {"@recyclable_type_id", entity.RecyclableTypeId},
                    {"@weight", entity.Weight},
                    {"@item_description", entity.ItemDescription}
                };
            try
            {
                OpenDBConnection();
                r = ExecuteStoredProc(proc_name, param);
                CloseDBConnection();
            }
            catch
            {
                r = 0;
            }
            return r;
        }

        public IEnumerable<RecyclableItem> GetAll()
        {
            List<RecyclableItem> recyclableItems = new List<RecyclableItem>();
            string query = "SELECT * FROM [Recyclable_Item]";

            try
            {
                OpenDBConnection();

                DataReader = ExecuteReader(query);

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        recyclableItems.Add(new RecyclableItem()
                        {
                            Id = int.Parse(DataReader["Id"].ToString()),
                            RecyclableTypeId = int.Parse(DataReader["RecyclableTypeId"].ToString()),
                            RecyclableType = new RecyclableTypeRepository().GetEntityById(int.Parse(DataReader["RecyclableTypeId"].ToString())),
                            Weight = float.Parse(DataReader["Weight"].ToString()),
                            ComputedRate = float.Parse(DataReader["ComputedRate"].ToString()),
                            ItemDescription = DataReader["ItemDescription"].ToString()
                        });
                    }
                }
                else
                {
                    ErrorLog.ErrorMessage = "No Recyclable Types";
                    recyclableItems = null;
                }
                DataReader.Close();
                CloseDBConnection();
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                Debug.Write(ex.Message);
                recyclableItems = null;
            }
            return recyclableItems;
        }

       

        public RecyclableItem GetEntityById(int id)
        {
            RecyclableItem recyclableItem;
            string query = "SELECT * FROM [Recyclable_Item] " +
                            "where Id = @id";
            var param = new Dictionary<string, object>()
            {
                { "@id", id}
            };

            try
            {
                OpenDBConnection();

                DataReader = ExecuteReader(query, param);

                if (DataReader.HasRows)
                {
                    DataReader.Read();

                    recyclableItem = new RecyclableItem()
                    {
                        Id = int.Parse(DataReader["Id"].ToString()),
                        RecyclableTypeId = int.Parse(DataReader["RecyclableTypeId"].ToString()),
                        RecyclableType = new RecyclableTypeRepository().GetEntityById(int.Parse(DataReader["RecyclableTypeId"].ToString())),
                        Weight = float.Parse(DataReader["Weight"].ToString()),
                        ComputedRate = float.Parse(DataReader["ComputedRate"].ToString()),
                        ItemDescription = DataReader["ItemDescription"].ToString()
                    };
                }
                else
                {
                    ErrorLog.ErrorMessage = "Entry not found";
                    recyclableItem = null;
                }

                DataReader.Close();

                CloseDBConnection();
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                recyclableItem = null;
            }

            return recyclableItem;
        }

        public bool Update(RecyclableItem entity)
        {
            int r;
            bool flag = true;
            string proc_name = "spUpdate_Recyclable_Item";

            var param = new Dictionary<string, object>{
                    {"@id", entity.Id},
                    {"@recyclable_type_id", entity.RecyclableTypeId},
                    {"@weight", entity.Weight},
                    {"@item_description", entity.ItemDescription}
                };
            try
            {
                OpenDBConnection();
                r = ExecuteStoredProc(proc_name, param);
                CloseDBConnection();
                if (r <= 0)
                    flag = false;
            }
            catch
            {
                flag = false;
            }
            return flag;
            
        }
        public bool Delete(int id)
        {
            bool flag = true;
            int rowsAffected;
            string query = "DELETE FROM Recyclable_Item where Id = @Id;";
            var param = new Dictionary<string, object>()
            {
                { "@Id", id}
            };

            try
            {
                OpenDBConnection();

                rowsAffected = ExecuteNonQuery(query, param);

                CloseDBConnection();

                if (rowsAffected == 0)
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                return false;
            }

            return flag;
        }
        #endregion
        public bool IsExisting(int Id) => GetEntityById(Id) != null;

        private int ExecuteStoredProc(string proc_name, IDictionary<string, object> parameters)
        {
            SqlCommand = new SqlCommand(proc_name, SqlConnection);
            SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var p in parameters)
            {
                SqlCommand.Parameters.AddWithValue(p.Key, p.Value);
            }

            return SqlCommand.ExecuteNonQuery();
        }

    }
}