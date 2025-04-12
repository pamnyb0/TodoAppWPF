using System.Collections.Generic;

namespace TodoAppWPF
{
    public class TodoList
    {
        private List<string> tasks = new List<string>();

        public void AddTask(string task)
        {
            if (!string.IsNullOrWhiteSpace(task))
            {
                tasks.Add(task);
            }
        }

        public void RemoveTask(string task)
        {
            tasks.Remove(task);
        }

        public List<string> GetAllTasks()
        {
            return new List<string>(tasks);
        }
    }
}