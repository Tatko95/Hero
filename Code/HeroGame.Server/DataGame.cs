using System;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using HeroGame.ModelGame;
using System.Collections.Generic;

namespace HeroGame.Server
{
    public class DataGame : IDisposable
    {
        private const string _connectionStr = @"Data Source=.\SQLEXPRESS;
                                                Initial Catalog=GameHeroData;
                                                Integrated Security=True;
                                                Pooling=True";
        private SqlConnection _sqlConnection;

        public DataGame()
        {
            _sqlConnection = new SqlConnection(_connectionStr);
            this.ConnectionOpen();
        }
        public void ConnectionOpen()
        {
            try
            {
                _sqlConnection.Open();
                Console.WriteLine("Connection to BD: " + _sqlConnection.State);
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
        public void ConnectionClose()
        {
            try
            {
                _sqlConnection.Close();
                Console.WriteLine("Connection to BD: " + _sqlConnection.State);
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
        public void Dispose()
        {
            this.ConnectionClose();
        }

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

        public bool Registration(string nickName, string pass)
        {
            using (SqlCommand cmd = new SqlCommand("Registration", _sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@NickName", nickName);
                cmd.Parameters.AddWithValue("@Pass", pass);

                SqlParameter parameter = cmd.Parameters.Add(new SqlParameter());
                parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                if ((int)parameter.Value == 1)
                    return true;
                else
                    return false;
            }
        }

        public User Login(string nickName)
        {
            User returnUser = null;
            using (SqlCommand cmd = new SqlCommand("select * from USERS where NickName = @nickName", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("nickName", nickName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        returnUser = new User(reader["NickName"].ToString(), reader["Pass"].ToString(), (int) reader["ID"], new Clan((int)reader["IDClan"]));
                }
            }
            return returnUser;
        }

        public void EndGame(Game game)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT GAME VALUES (@IDWinner, @IDLoser)", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("IDWinner", game.Winner.ID);
                cmd.Parameters.AddWithValue("IDLoser", game.Loser.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Game> RefreshResult(User user)
        {
            List<Game> returnList = new List<Game>();
            using (SqlCommand cmd = new SqlCommand("select * from GAME where IDWinner = @id1 OR IDLoser = @id2", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("id1", user.ID);
                cmd.Parameters.AddWithValue("id2", user.ID);

                SqlDataReader reader = cmd.ExecuteReader();
                List<int> list1 = new List<int>();
                List<int> list2 = new List<int>();
                while (reader.Read())
                {
                    list1.Add((int)reader[1]);
                    list2.Add((int)reader[2]);
                }
                reader.Close();
                for (int i = 0; i < list1.Count; i++)
                {
                    SqlCommand cmd2 = new SqlCommand("select * from USERS where ID = @id", _sqlConnection);
                    cmd2.Parameters.AddWithValue("id", list1[i]);
                    var reader2 = cmd2.ExecuteReader();
                    reader2.Read();
                    User u1 = new User((string)reader2[1], "", (int)reader2[0], null);
                    reader2.Close();

                    SqlCommand cmd3 = new SqlCommand("select * from USERS where ID = @id", _sqlConnection);
                    cmd3.Parameters.AddWithValue("id", list2[i]);
                    var reader3 = cmd3.ExecuteReader();
                    reader3.Read();
                    User u2 = new User((string)reader3[1], "", (int)reader3[0], null);
                    reader3.Close();

                    Game game = new Game(u1, u2);
                    returnList.Add(game);
                }
            }
            return returnList;
        }

        public bool CreateClan(Clan clan)
        {
            using (SqlCommand cmd = new SqlCommand("CreateClanProcedure", _sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", clan.Name);
                cmd.Parameters.AddWithValue("@IDCreater", clan.Creater.ID);

                SqlParameter parameter = cmd.Parameters.Add(new SqlParameter());
                parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                if ((int)parameter.Value == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}
