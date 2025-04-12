using System;
using System.Windows;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TodoAppWPF
{
    public partial class MainWindow : Window
    {
        private TodoList todoList = new TodoList();

        public MainWindow()
        {
            InitializeComponent();
            RefreshTaskList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(task))
            {
                AddTodo(task);
                RefreshTaskList();
                TaskTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Skriv in en uppgift.", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem != null)
            {
                string selectedTask = TasksListBox.SelectedItem.ToString();
                RemoveTodo(selectedTask);
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Välj en uppgift att ta bort.", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshTaskList()
        {
            TasksListBox.Items.Clear();

            foreach (var todo in LoadTodos())
            {
                TasksListBox.Items.Add(todo.Description);
            }
        }

        public void AddTodo(string description)
        {
            using (var connection = new SQLiteConnection($"Data Source={App.DbPath};Version=3;"))
            {
                connection.Open();
                string query = "INSERT INTO TodoItems (Description, IsDone) VALUES (@desc, 0)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@desc", description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveTodo(string description)
        {
            using (var connection = new SQLiteConnection($"Data Source={App.DbPath};Version=3;"))
            {
                connection.Open();
                string query = "DELETE FROM TodoItems WHERE Description = @desc";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@desc", description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<TodoItem> LoadTodos()
        {
            var todos = new List<TodoItem>();

            using (var connection = new SQLiteConnection($"Data Source={App.DbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT Id, Description, IsDone FROM TodoItems";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(new TodoItem
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            IsDone = reader.GetInt32(2) == 1
                        });
                    }
                }
            }

            return todos;
        }
    }
}