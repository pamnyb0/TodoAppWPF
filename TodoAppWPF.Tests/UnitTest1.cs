using Xunit;
using TodoAppWPF;

namespace TodoAppWPF.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void AddTask_ShouldAddTaskToList()
        {
            // Arrange
            var todoList = new TodoList();
            string task = "Test Task";

            // Act
            todoList.AddTask(task);

            // Assert
            Assert.Contains(task, todoList.GetAllTasks());
        }

        [Fact]
        public void RemoveTask_ShouldRemoveTaskFromList()
        {
            // Arrange
            var todoList = new TodoList();
            string task = "Test Task";
            todoList.AddTask(task);

            // Act
            todoList.RemoveTask(task);

            // Assert
            Assert.DoesNotContain(task, todoList.GetAllTasks());
        }

        [Fact]
        public void AddTask_ShouldNotAddEmptyTask()
        {
            // Arrange
            var todoList = new TodoList();
            string emptyTask = "";

            // Act
            todoList.AddTask(emptyTask);

            // Assert
            Assert.Empty(todoList.GetAllTasks());
        }

        [Fact]
        public void RemoveTask_ShouldHandleNonexistentTask()
        {
            // Arrange
            var todoList = new TodoList();
            string task = "Nonexistent Task";

            // Act & Assert (should not throw exception)
            todoList.RemoveTask(task);
        }
    }
}