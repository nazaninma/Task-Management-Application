using APProj.Client;
using System.Net;

using HttpClient httpclient= new HttpClient();

swaggerClient client = new swaggerClient("http://localhost:5000/",httpclient);

foreach(var s in client.AllTasksAsync().Result)
    System.Console.WriteLine($"{s.Id} :{s.Taskname}");

foreach(var s in client.GetInCompleteToDosAsync().Result)
    System.Console.WriteLine($"{s.Id} :{s.Taskname}");

// client.CreateToDoAsync(new Task(6,"testing client",DateTime.Parse("7/2/2023")));

// public class SynchronizedCollectionHub<T>:Hub
// {
//     public async System.Threading.Tasks.Task AddTaskServer (T t)
//     {
       
//         await Clients.All.SendAsync("AddTaskClient" , t);
//     }
// }