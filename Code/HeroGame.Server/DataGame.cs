using System;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using HeroGame.ModelGame;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                cmd.Parameters.AddWithValue("@Nick", nickName);
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
            using (SqlCommand cmd = new SqlCommand("select * from USERS where Nick = @Nick", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("Nick", nickName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Clan clan;
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(2))
                            clan = null;
                        else
                        {
                            User u = new User((string)reader["Nick"], "", null);
                            clan = new Clan((string)reader["Clan"], u);
                        }
                        returnUser = new User(reader["Nick"].ToString(), reader["Pass"].ToString(), clan);
                    }
                }
            }
            return returnUser;
        }

        public void EndGame(Game game)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT GAMES VALUES (@NameWinner, @NameLoser)", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("NameWinner", game.Winner.NickName);
                cmd.Parameters.AddWithValue("NameLoser", game.Loser.NickName);
                cmd.ExecuteNonQuery();
            }
        }

        public async Task<List<Game>> RefreshResult(User user)
        {
            List<Game> returnList = new List<Game>();
            using (SqlCommand cmd = new SqlCommand("select * from GAMES where NameWinner = @NameWinner OR NameLoser = @NameLoser", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("NameWinner", user.NickName);
                cmd.Parameters.AddWithValue("NameLoser", user.NickName);

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                while (reader.Read())
                {
                    User u1 = new User((string)reader[0], "", null);
                    User u2 = new User((string)reader[1], "", null);
                    Game game = new Game(u1, u2);
                    returnList.Add(game);
                }
                reader.Close();
            }
            return returnList;
        }

        public bool CreateClan(Clan clan)
        {
            using (SqlCommand cmd = new SqlCommand("CreateClanProcedure", _sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", clan.Name);
                cmd.Parameters.AddWithValue("@Creater", clan.Creater.NickName);

                SqlParameter parameter = cmd.Parameters.Add(new SqlParameter());
                parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                if ((int)parameter.Value == 1)
                    return true;
                else
                    return false;
            }
        }

        public void AddFriend(string user1, string user2)
        {
            using(SqlCommand cmd = new SqlCommand("INSERT FRIENDS VALUES(@User1, @User2)", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("User1", user1);
                cmd.Parameters.AddWithValue("User2", user2);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
