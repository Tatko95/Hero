using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    public class Data : IDisposable
    {
        private const string _connectionStr = @"Data Source=.\SQLEXPRESS;
                                                Initial Catalog=GameHeroData;
                                                Integrated Security=True;
                                                Pooling=True";
        private SqlConnection _sqlConnection;
        private DataTable _dataTable;

        public Data()
        {
            _sqlConnection = new SqlConnection(_connectionStr);
            this.ConnectionOpen();
        }

        #region Connection
        public void ConnectionOpen()
        {
            try
            {
                _sqlConnection.Open();
                Console.WriteLine("Connection to BD: " + _sqlConnection.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ConnectionClose()
        {
            try
            {
                _sqlConnection.Close();
                Console.WriteLine("Connection to BD: " + _sqlConnection.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Dispose()
        {
            this.ConnectionClose();
        }
        #endregion

        #region Standart
        public void Insert(string SQLcommand)
        {
            SqlCommand sqlCommand = new SqlCommand(SQLcommand, _sqlConnection);
            int rowAffected = sqlCommand.ExecuteNonQuery();
            Console.WriteLine("INSERT command rows affected: " + rowAffected);
        }
        public void Delete(string SQLcommand)
        {
            SqlCommand sqlCommand = new SqlCommand(SQLcommand, _sqlConnection);
            int rowAffected = sqlCommand.ExecuteNonQuery();
            Console.WriteLine("DELETE command rows affected: " + rowAffected);
        }
        public void Update(string SQLcommand)
        {
            SqlCommand sqlCommand = new SqlCommand(SQLcommand, _sqlConnection);
            int rowAffected = sqlCommand.ExecuteNonQuery();
            Console.WriteLine("DELETE command rows affected: " + rowAffected);
        }
        public void Select(string SQLcommand)
        {
            SqlCommand sqlCommand = new SqlCommand(SQLcommand, _sqlConnection);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.WriteLine(reader.GetName(i) + ": " + reader[i]);
                    Console.WriteLine(new string('_', 20));
                }
            }
        }
        #endregion
    }
}
