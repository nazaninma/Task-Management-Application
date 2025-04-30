using Microsoft.AspNetCore.SignalR;
using AiLib;
using System.Collections.ObjectModel;

using ToDoClass personalTodo= new ToDoClass();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(); 
builder.Services.AddSignalR(opt=>{opt.EnableDetailedErrors=true;});
builder.Services.AddSingleton(personalTodo);
builder.Services.AddCors(option=>
{
    option.AddDefaultPolicy(
        policy=>
        {
            policy.WithOrigins("http://localhost:5092")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );

});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
 app.UseSwagger();
 app.UseSwaggerUI();
app.MapRazorPages();
app.MapHub<SynchronizedCollectionHub<Task>>("/crud");

app.MapGet("/", () => "Hello World!");
app.MapGet("/AllTasks", () => personalTodo.MyTodo.ToArray());
app.MapGet("/GetCompletedToDos", () => personalTodo.MyTodo.Where(f=>f.TaskperentCompeleted==100).ToArray() ); 
app.MapGet("/GetInCompleteToDos", () => personalTodo.MyTodo.Where(f=>f.TaskperentCompeleted!=100).ToArray() ); 
app.MapGet("/GetAllToDos", () => personalTodo.MyTodo.ToArray() ); 



app.MapPost("/GetMotivation", (string interest , int  taskId) =>  
{
    AiLib.Motivate T = new Motivate();
    T.chat_input_interst=interest; T.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    T.Motivation();
  return T.chat_output;


} ); 

app.MapPost("/BreakTasks", (int time , int  taskId) =>  
{
    AiLib.BreakTask b = new BreakTask();
    b.chat_input_time=time; b.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    b.Breaking();
  return b.chat_output;


} );

app.MapPost("/ReasonOfStudy", (string job  , int  taskId) =>  
{
    AiLib.Why w = new Why();
    w.chat_input_work=job; w.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    w.WhyShouldStudy();
  return w.chat_output;


} );


app.MapPost("/WritingPoem", ( int  taskId) =>  
{
    AiLib.Poem p = new Poem();
     p.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    p.WritePoem();
  return p.chat_output;



} );


//new

app.MapPost("/BestWorst", (string job  , int  taskId) =>  
{
    AiLib.BestWorst bw = new BestWorst();
    bw.chat_input_work=job; bw.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    bw.BestWorstResutl();
  return bw.chat_output;


} );


app.MapPost("/MakeFun", ( int  taskId) =>  
{
    AiLib.MakeFun mf = new MakeFun();
     mf.chat_input_task=personalTodo.MyTodo.Where(f=>f.Id==taskId).ToList().First().Taskname;
    mf.MakeItFun();
  return mf.chat_output;



} );
//new


app.MapPost("/CreateToDo", (Task t) => 
{
    personalTodo.MyTodo.Add(t);
    personalTodo.SaveChanges();
} );

app.MapPost("/DeleteToDo", (int id) => 
{
    int count = personalTodo.MyTodo.ToList().Count;
    foreach(var t in personalTodo.MyTodo)
         if (t.Id==id)
            personalTodo.MyTodo.Remove(t);
    personalTodo.SaveChanges();
} );

app.MapPost("/MarkToDoAsComplete", (int id) => 
{
    foreach(Task t in personalTodo.MyTodo)
        if(t.Id==id)
            {
                int saveId= t.Id;
                string name= t.Taskname;
                var deadLine= t.TaskdeadLine;
                var time= t.Tasktimeneeded;
                personalTodo.MyTodo.Remove(t);
                personalTodo.MyTodo.Add(new Task(id,name,deadLine,100,time));
            }
    personalTodo.SaveChanges();

} );



app.Run();
public class SynchronizedCollectionHub<T>:Hub
{
    public ToDoClass dbase= new ToDoClass ();
    
    
    public override async System.Threading.Tasks.Task OnConnectedAsync()
    {
        string connectionId=Context.ConnectionId;
        var readDB= dbase.GetAllTask();
        foreach(var t in readDB)
            Clients.Client(connectionId).SendAsync("ReciveDatabase", t).Wait();
       await base.OnConnectedAsync();
    }
    public async System.Threading.Tasks.Task AddTaskServer (T t , ToDoClass db)
    {
        var f= t as Task;
        
        db.CreatTask(f.Id,f.Taskname ,f.TaskdeadLine,f.TaskperentCompeleted,f.Tasktimeneeded);
       
        await Clients.All.SendAsync("AddTaskClient" , t);
    }
    public async System.Threading.Tasks.Task DeleteTaskServer (int t , ToDoClass db)
    {
        int index=db.GetAllTask()[t].Id;


       
        await Clients.All.SendAsync("DeleteTaskClient" , t);
          db.DeleteTask(index);
    }

    public async System.Threading.Tasks.Task ClearTaskServer ()
    {
       
        await Clients.All.SendAsync("ClearTaskClient");
    }

}
