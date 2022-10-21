using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model3;
using Dapper;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbConnection;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        IDbConnection db = new SqlConnection(connectionString);

        public IEnumerable<T> GetAll()
        {
            return db.Query<T>("SELECT * FROM Students").ToList();
        }

        public T GetById(int id)
        {
            return db.Query<T>("SELECT * FROM Students WHERE Id = @id", new { id }).FirstOrDefault();
        }

        public void Create(T obj)
        {
            var sqlQuery = "INSERT INTO Students (Name, [Group], Speciality) VALUES(@Name, @Group, @Speciality); SELECT CAST(SCOPE_IDENTITY() as int)";
            int Id = db.Query<int>(sqlQuery, obj).FirstOrDefault();
            obj.Id = Id;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Students WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        /// <summary>
        /// удаляет всех студентов и сбрасывает счётчик id
        /// </summary>
        public void DeleteAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "TRUNCATE TABLE Students";
                db.Execute(sqlQuery, new { });


                //DBCC CHECKIDENT ('имя_таблицы', RESEED, новое_стартовое_значение) - сбрасывает значение счетчика в указанное значение.
                //Способ подходит когда вы выкидываете часть таблицы и хотите чтобы поле счетчика не имело провалов.
            }
        }

        public void Save() { }
    }
}
