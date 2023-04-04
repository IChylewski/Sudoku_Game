using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Database
{
    public static class Database
    {
        public static void Initialize()
        {
            if (!File.Exists("./database.sqlite"))
            {
                SQLiteConnection.CreateFile("database.sqlite");
                CreateScoreTable();
            }
        }

        public static void CreateScoreTable()
        {
            using(SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE Score(id INTEGER PRIMARY KEY, Username TEXT, Score DOUBLE, Level TEXT)";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public static void AddScore(string username, double score, string level)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO Score(Username, Score, Level) VALUES ('{username}', '{score}', '{level}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static List<ScoreModel> GetScores()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<ScoreModel> scores = new List<ScoreModel>();
                string sql = "SELECT * FROM Score";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    scores.Add(new ScoreModel
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Score = reader.GetDouble(2),
                        Level = reader.GetString(3)
                    });
                }
                dbConnection.Close();
                return scores.OrderByDescending(item => item.Score).ToList();
            }
        }
    }
}
