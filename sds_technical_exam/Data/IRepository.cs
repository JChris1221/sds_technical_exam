using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sds_technical_exam.Data
{
    interface IRepository<T>
    {
        int Add(T entity);
        T GetEntityById(int id);
        
        bool Update(T entity);
        bool Delete(int id);
        IEnumerable<T> GetAll();
        bool IsExisting(int id);
    }
}