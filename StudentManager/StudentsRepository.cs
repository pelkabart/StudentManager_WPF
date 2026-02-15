using Microsoft.Data.Sqlite;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentSearch
{
    public class StudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository()
        {
            // DemoDB.db musi leżeć obok exe
            var dbPath = Path.Combine(AppContext.BaseDirectory, "DemoDB.db");
            _connectionString = $"Data Source={dbPath};";
        }

        public List<Student> Search(string searchText)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                cmd.CommandText = @"
                    SELECT id, name, surname, year
                    FROM Students
                    ORDER BY CAST(SUBSTR(id, 2) AS INTEGER);";
            }
            else
            {
                cmd.CommandText = @"
                    SELECT id, name, surname, year
                    FROM Students
                    WHERE id LIKE @q OR name LIKE @q OR surname LIKE @q
                    ORDER BY CAST(SUBSTR(id, 2) AS INTEGER);";
                cmd.Parameters.AddWithValue("@q", $"%{searchText}%");
            }

            var result = new List<Student>();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new Student
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Year = reader.GetInt32(3),

                    
                    AvgGrade = 0f
                };

                result.Add(s);
            }

            return result;
        }

        public List<Student> GetStudents()
        {
            return Search(null);
        }

        public Student GetStudent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT id, name, surname, year
                FROM Students
                WHERE id = @id
                LIMIT 1;";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new Student
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                Year = reader.GetInt32(3),
                AvgGrade = 0f
            };
        }

        public string nextId()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT MAX(CAST(SUBSTR(id, 2) AS INTEGER)) FROM Students;";

            var scalar = cmd.ExecuteScalar();
            int max = 0;

            if (scalar != null && scalar != DBNull.Value)
                max = Convert.ToInt32(scalar);

            int next = max + 1;
            return $"S{next:000}";
        }

        public bool AddStudent(string name, string surname, string year)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                return false;

            if (!int.TryParse(year, out int parsedYear))
                return false;

            string newId = nextId();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Students (id, name, surname, year)
                VALUES (@id, @name, @surname, @year);";
            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@year", parsedYear);

            try
            {
                return cmd.ExecuteNonQuery() == 1;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteStudent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM Students WHERE id = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                return cmd.ExecuteNonQuery() == 1; 
            }
            catch
            {
                return false;
            }
        }
    }
}
