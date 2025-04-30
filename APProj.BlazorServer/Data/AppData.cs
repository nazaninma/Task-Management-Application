using SynchronizedCollection;

namespace APProj.BlazorServer.Data;
public class AppData
{
     public SynchronizedCollection<databaseLib.Task> TasksSync = new SynchronizedCollection<databaseLib.Task>(@"http://localhost:5000", "crud");

}