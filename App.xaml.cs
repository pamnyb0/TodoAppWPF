using System;
using System.Windows;
using System.IO;
using System.Data.SQLite;

namespace TodoAppWPF
{
    public partial class App : Application
    {
        private static string dbPath = "TodoApp.db";
        public static string DbPath { get { return dbPath; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
                using (var connection = new SQLiteConnection($"Data Source={DbPath};Version=3;"))
                {
                    connection.Open();
                    string sql = @"CREATE TABLE TodoItems (
                                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                   Description TEXT NOT NULL,
                                   IsDone INTEGER NOT NULL
                               )";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}