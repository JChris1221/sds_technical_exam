using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web;
using sds_technical_exam.Models;

namespace sds_technical_exam.Data
{
    public class RecyclableTypeRepository : RepositoryBase, IRepository<RecyclableType>
    {
        public ErrorLog ErrorLog = new ErrorLog();

        #region CRUD Functions

        public int Add(RecyclableType entity)
        {
            RecyclableType recyclableType;
            string query = "INSERT INTO Recyclable_Type (Type, Rate, MinKg, MaxKg)" +
                    "values (@Type, @Rate,@MinKg, @MaxKg)";

            var param = new Dictionary<string, object>{
                    {"@Type", entity.Type},
                    {"@Rate", entity.Rate},
                    {"@MinKg", entity.MinKg},
                    {"@MaxKg", entity.MaxKg}
                };
            try
            {
                OpenDBConnection();
                ExecuteNonQuery(query, param);
                CloseDBConnection();
                recyclableType = GetEntityByTypeName(entity.Type);
                if(recyclableType == null)
                    return 0;
            }
            catch
            {
                return 0;
            }
            return recyclableType.Id;
        }

        public IEnumerable<RecyclableType> GetAll()
        {
            List<RecyclableType> recyclableTypes = new List<RecyclableType>();
            string query = "SELECT * FROM Recyclable_Type";

            try
            {
                OpenDBConnection();

                DataReader = ExecuteReader(query);

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        recyclableTypes.Add(new RecyclableType()
                        {
                            Id = int.Parse(DataReader["Id"].ToString()),
                            Type = DataReader["Type"].ToString(),
                            Rate = float.Parse(DataReader["Rate"].ToString()),
                            MinKg = float.Parse(DataReader["MinKg"].ToString()),
                            MaxKg = float.Parse(DataReader["MaxKg"].ToString())
                        });
                    }
                }
                else
                {
                    ErrorLog.ErrorMessage = "No Recyclable Types";
                    recyclableTypes = null;
                }
                DataReader.Close();
                CloseDBConnection();
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                recyclableTypes = null;
            }
            return recyclableTypes;
        }

        public RecyclableType GetEntityByTypeName(string type)
        {
            RecyclableType recyclableType;
            string query = "SELECT * FROM [Recyclable_Type] " +
                            "where Type = @Type";
            var param = new Dictionary<string, object>()
            {
                { "@Type", type}
            };

            try
            {
                OpenDBConnection();

                DataReader = ExecuteReader(query, param);

                if (DataReader.HasRows)
                {
                    DataReader.Read();

                    recyclableType = new RecyclableType()
                    {
                        Id = int.Parse(DataReader["Id"].ToString()),
                        Type = DataReader["Type"].ToString(),
                        Rate = float.Parse(DataReader["Rate"].ToString()),
                        MinKg = float.Parse(DataReader["MinKg"].ToString()),
                        MaxKg = float.Parse(DataReader["MaxKg"].ToString())
                    };
                }
                else
                {
                    ErrorLog.ErrorMessage = "Entry not found";
                    recyclableType = null;
                }

                DataReader.Close();

                CloseDBConnection();
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                recyclableType = null;
            }

            return recyclableType;
        }

        public RecyclableType GetEntityById(int id)
        {
            RecyclableType recyclableType;
            string query = "SELECT * FROM [Recyclable_Type] " +
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

                    recyclableType = new RecyclableType()
                    {
                        Id = int.Parse(DataReader["Id"].ToString()),
                        Type = DataReader["Type"].ToString(),
                        Rate = float.Parse(DataReader["Rate"].ToString()),
                        MinKg = float.Parse(DataReader["MinKg"].ToString()),
                        MaxKg = float.Parse(DataReader["MaxKg"].ToString())
                    };
                }
                else
                {
                    ErrorLog.ErrorMessage = "Entry not found";
                    recyclableType = null;
                }

                DataReader.Close();

                CloseDBConnection();
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                recyclableType = null;
            }

            return recyclableType;
        }

        public bool Update(RecyclableType entity)
        {
            bool flag = true;
            string query = "UPDATE [Recyclable_Type] Set Type = @Type, Rate = @Rate, MinKg = @MinKg, MaxKg = @MaxKg WHERE Id = @Id";

            var param = new Dictionary<string, object>{
                    {"@Id", entity.Id},
                    {"@Type", entity.Type},
                    {"@Rate", entity.Rate},
                    {"@MinKg", entity.MinKg},
                    {"@MaxKg", entity.MaxKg}
                };
            try
            {
                OpenDBConnection();
                int affected_rows = ExecuteNonQuery(query, param);
                CloseDBConnection();
                
                if (affected_rows == 0)
                    flag = false;
            }
            catch (Exception ex)
            {
                ErrorLog.Exception = ex;
                flag = false;
            }
            return flag;
        }
        public bool Delete(int id)
        {
            bool flag = true;
            return flag;
        }
        #endregion
        public bool IsExisting(int Id) => GetEntityById(Id) != null;
        public bool IsExisting(string type) => GetEntityByTypeName(type) != null;

    }
}