using Microsoft.EntityFrameworkCore;

public  record Task (int Id , string Taskname , DateTime TaskdeadLine , double TaskperentCompeleted, int Tasktimeneeded );


public class ToDoClass :DbContext , CRUD
{

    public DbSet<Task> MyTodo{get;set;}

    public void CreatTask(int id , string Name ,DateTime deadLine , double perentCompeleted, int timeneeded)
    {
        MyTodo.Add(new Task(id,Name,deadLine,perentCompeleted,timeneeded));
        SaveChanges();
    }

    public void DeleteTask(int id)
    {
            
            foreach(var t in MyTodo)
                if (t.Id==id)
                    MyTodo.Remove(t);
            SaveChanges();
    }

    public void EditTask(int id, string newName, DateTime newdeadLine, double newperentCompeleted, int newtimeneeded)
    {
        int saveId= id;
        DeleteTask(id);
        CreatTask(saveId,newName,newdeadLine,newperentCompeleted,newtimeneeded);
        SaveChanges();

    }

    public Task[] GetAllTask()
    {
        return MyTodo.ToArray();
    }

    public Task GetTaskById(int id)
    {
        return MyTodo.Where(s=>s.Id==id).ToList().First();
    }
        


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlite(@"Data Source=data.db");
     }

    
}