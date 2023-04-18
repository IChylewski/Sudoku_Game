using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
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
                CreateGameTable();
                CreateScoreTable();
                CreatePlayerMoveTable();
                CreateNumberFieldTable();
                CreateEditableFieldTable();
            }
        }
        public static void CreateGameTable()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE Game(Id INTEGER PRIMARY KEY, Username TEXT, Date TEXT)";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void CreateScoreTable()
        {
            using(SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE Score(Id INTEGER PRIMARY KEY, GameId INTEGER, Username TEXT, Score DOUBLE, Level TEXT, FOREIGN KEY(GameId) REFERENCES Game(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void CreatePlayerMoveTable()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE PlayerMove(Id INTEGER PRIMARY KEY, Username TEXT, GameId INTEGER, EditableField TEXT, Value INTEGER, FOREIGN KEY(GameId) REFERENCES Game(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void CreateNumberFieldTable()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE NumberField(Id INTEGER PRIMARY KEY, GameId INTEGER, Coordinate TEXT, Value INTEGER, FOREIGN KEY(GameId) REFERENCES Game(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void CreateEditableFieldTable()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE EditableField(Id INTEGER PRIMARY KEY, GameId INTEGER, Coordinate TEXT, FOREIGN KEY(GameId) REFERENCES Game(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void CreateIndexToHideTable()            
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = "CREATE TABLE IndexToHide(Id INTEGER PRIMARY KEY, GameId INTEGER, Coordinate TEXT, FOREIGN KEY(GameId) REFERENCES Game(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public static void AddScore(string username, int gameId, double score, string level)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO Score(Username, GameId, Score, Level) VALUES ('{username}', '{gameId}', '{score}', '{level}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void AddGame(string username, string date)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO Game(Username, Date) VALUES ('{username}','{date}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void AddPlayerMove(int gameId, string username, string editableField, int value)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO PlayerMove(Username, GameId, EditableField, Value) VALUES ('{username}','{gameId}','{editableField}','{value}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void AddNumberField(int gameId, string coordinate, int value)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO NumberField(GameId, Coordinate, Value) VALUES ('{gameId}','{coordinate}','{value}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void AddEditableField(int gameId, string coordinate)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO EditableField(GameId, Coordinate) VALUES ('{gameId}','{coordinate}')";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
        public static void AddIndexToHide(int gameId, string coordinate)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();

                string sql = $"INSERT INTO EditableField(GameId, Coordinate) VALUES ('{gameId}','{coordinate}')";
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
                        GameID = reader.GetInt32(1),
                        Username = reader.GetString(2),
                        Score = reader.GetDouble(3),
                        Level = reader.GetString(4)
                    });
                }
                dbConnection.Close();
                return scores.OrderByDescending(item => item.Score).ToList();
            }
        }
        public static List<PlayerMoveModel> GetPlayerMoves(int gameId)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<PlayerMoveModel> playerMoves = new List<PlayerMoveModel>();
                string sql = $"SELECT * FROM PlayerMove WHERE GameId = {gameId}";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    playerMoves.Add(new PlayerMoveModel
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        GameID = reader.GetInt32(2),
                        EditableField = reader.GetString(3),
                        Value = reader.GetInt32(4)
                    });
                }
                return playerMoves;
            }
        }
        public static List<GameModel> GetGames()
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<GameModel> games = new List<GameModel>();
                string sql = "SELECT * FROM Game";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                //string formatString = "dd/MM/yyyy HH:mm:ss";

                while (reader.Read())
                {
                    games.Add(new GameModel
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Date = reader.GetString(2)
                    });
                }
                return games;
            }
        }
        public static Dictionary<string, int> GetNumberFields(int gameId)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<NumberFieldModel> numberFields = new List<NumberFieldModel>();
                Dictionary<string, int> numberFieldsDictonary = new Dictionary<string, int>();
                string sql = $"SELECT * FROM NumberField WHERE GameId = {gameId}";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                //string formatString = "dd/MM/yyyy HH:mm:ss";

                while (reader.Read())
                {
                    numberFields.Add(new NumberFieldModel
                    {
                        ID = reader.GetInt32(0),
                        GameId = reader.GetInt32(1),
                        Coordinate = reader.GetString(2),
                        Value = reader.GetInt32(3)
                    });
                }

                foreach(NumberFieldModel numberFieldModel in numberFields)
                {
                    numberFieldsDictonary.Add(numberFieldModel.Coordinate, numberFieldModel.Value);
                }
                return numberFieldsDictonary;
            }
        }
        public static List<string> GetEditableFields(int gameId)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<EditableFieldModel> editableFields = new List<EditableFieldModel>();
                List<string> editableFieldsString = new List<string>();
                string sql = $"SELECT * FROM EditableField WHERE GameId = {gameId}";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                //string formatString = "dd/MM/yyyy HH:mm:ss";

                while (reader.Read())
                {
                    editableFields.Add(new EditableFieldModel
                    {
                        Id = reader.GetInt32(0),
                        GameId = reader.GetInt32(1),
                        Coordinate = reader.GetString(2)
                    });
                }
                foreach (EditableFieldModel editableField in editableFields)
                {
                    editableFieldsString.Add(editableField.Coordinate);
                }

                return editableFieldsString;
            }
        }
        public static List<string> GetIndexesToHide(int gameId)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database.sqlite"))
            {
                dbConnection.Open();
                List<IndexToHideModel> indexesToHide = new List<IndexToHideModel>();
                List<string> indexes = new List<string>();
                string sql = $"SELECT * FROM EditableField WHERE GameId = {gameId}";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                //string formatString = "dd/MM/yyyy HH:mm:ss";

                while (reader.Read())
                {
                    indexesToHide.Add(new IndexToHideModel
                    {
                        ID = reader.GetInt32(0),
                        GameId = reader.GetInt32(1),
                        Coordinate = reader.GetString(2)
                    });
                }

                foreach(IndexToHideModel indexToHide in indexesToHide)
                {
                    indexes.Add(indexToHide.Coordinate);
                }
                return indexes;
            }
        }
    }
}
