namespace Task_Control
{
    public class TaskManager
    {
        public List<Task> Tasks { get; private set; }
        public TaskManager()
        {
            Tasks = new List<Task>();
            LoadTasks();
        }
        public void AddTask(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Описание задачи不能为空.");
            }
            Tasks.Add(new Task(description));
            SaveTasks();
        }
        public void RemoveTask(int index)
        {
            if (index < 0 || index >= Tasks.Count)
            {
                throw new IndexOutOfRangeException("Некорректный индекс задачи.");
            }
            Tasks.RemoveAt(index);
            SaveTasks();
        }
        public void ToggleTaskCompletion(int index)
        {
            if (index < 0 || index >= Tasks.Count)
            {
                throw new IndexOutOfRangeException("Некорректный индекс задачи.");
            }
            Tasks[index].IsCompleted = !Tasks[index].IsCompleted;
            SaveTasks();
        }
        private void SaveTasks()
        {
            File.WriteAllLines("tasks.txt", Tasks.Select(t => $"{t.IsCompleted}|{t.Description}"));
        }
        private void LoadTasks()
        {
            if (File.Exists("tasks.txt"))
            {
                var lines = File.ReadAllLines("tasks.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        bool isCompleted = bool.Parse(parts[0]);
                        string description = parts[1];
                        Tasks.Add(new Task(description) { IsCompleted = isCompleted });
                    }
                }
            }
        }
    }
    public class Task
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Task(string description)
        {
            Description = description;
            IsCompleted = false;
        }
    }
}