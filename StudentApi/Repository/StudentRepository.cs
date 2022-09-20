using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using StudentApi.Core;
using StudentApi.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private string conStr;
        public StudentRepository(IOptions<SqliteSettings> options)
        {
            //conStr = @"Data Source=C:/Users/jlosada/Downloads/sample.db3";
            conStr = options.Value.ConnectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqliteConnection(conStr);
            }
        }

        /*
         * GET ALL STUDENTS
         */
        public async Task<IEnumerable<Student>> GetStudents()
        {
            using (IDbConnection dbconnection = Connection)
            {
                dbconnection.Open();
                return await dbconnection.QueryAsync<Student>("SELECT * FROM Student");
            }
        }

        /*
         * GET BY ID STUDENT
         */
        public async Task<Student> GetStudentByIdAsync(int Id)
        {
            using (IDbConnection dbconnection = Connection)
            {
                dbconnection.Open();
                var result = await dbconnection.QueryAsync<Student>("SELECT * FROM Student WHERE Id=@Id", new { Id = Id });

                return result.FirstOrDefault();
            }
        }

        /*
         * INSERT STUDENT
         */
        public async Task CreateStudent(Student student)
        {
            using (IDbConnection dbconnection = Connection)
            {
                string sql = @"INSERT INTO Student (  Username, FirstName, LastName, Age, Career ) VALUES (  @Username, @FirstName, @LastName, @Age, @Career);";
                dbconnection.Open();
                
                await dbconnection.ExecuteAsync(sql, student);
            }
        }

        /*
         * UPDATE STUDENT
         */
        public async Task UpdateStudent(Student student)
        {
            try
            {
                using (IDbConnection dbconnection = Connection)
                {
                    string sql = @"UPDATE Student SET Username = @Username, FirstName = @FirstName, LastName = @LastName, Age = @Age, Career = @Career WHERE Id = @Id";
                    dbconnection.Open();
                    await dbconnection.ExecuteAsync(sql, student);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * DELETE STUDENT
         */
        public async Task<Student> DeleteStudent(int Id)
        {
            using (IDbConnection dbconnection = Connection)
            {
                dbconnection.Open();
                var result = await dbconnection.QueryAsync<Student>("DELETE FROM Student WHERE Id = @Id", new { Id = Id });
                return result.FirstOrDefault();
            }
        }

    }
}
