using System.Collections.ObjectModel;
using SynchronizedCollection;

public class Program
{
    public static  void Main(string[] args)
    {
         List<Task> AllTasksBackUp = new List<Task>();

        SynchronizedCollection<Task> TasksSync;
        string request;

        Console.WriteLine("Hello! How can I help you?");
        

        while(true)
        {
            int duplicate =0;
            TasksSync = new SynchronizedCollection<Task> (@"http://localhost:5000", "crud");
            TasksSync.StartConnectionBlazorServer();
            System.Console.WriteLine("Add/ Delete/ Edit /List/Search/Sort/Exit");
             request= Console.ReadLine();
             AllTasksBackUp.Clear();
             foreach(var t in TasksSync.collection.OrderBy(t=>t.Id))
                AllTasksBackUp.Add(t);

            if(request.ToLower()=="add")
            {
               
                int id =0;
                DateTime deadline =DateTime.Now;
                string name=" ";
                double percent =0.0;
                int timeneeded =0;

                System.Console.Write("Id?");
                string AddId =Console.ReadLine();
                if(int.TryParse(AddId, out id) )
                    id = int.Parse(AddId);
                else 
                    
                    {
                        System.Console.WriteLine("Your Id must be an integer");
                        TasksSync.StopConnection();
                        return;
                    }

                 System.Console.Write("Name?");
                string AddName=Console.ReadLine();

                if(AddName ==null || AddName =="" || AddName ==" " )
                    {
                        System.Console.WriteLine("Task Name can't be null");
                         TasksSync.StopConnection();
                        return;
                    }
                else 
                    name = AddName;
                
                 System.Console.Write("Deadline?");
                string AddDeadline=Console.ReadLine();

                if (DateTime.TryParse(AddDeadline, out deadline))
                    deadline= DateTime.Parse(AddDeadline);
                else 
                {
                    System.Console.WriteLine("You must give the date of deadline like 1/1/2001");
                    TasksSync.StopConnection();
                    return;
                }

                 System.Console.Write("Timeneede?");
                string AddTimeneeded=Console.ReadLine();
                if(int.TryParse(AddTimeneeded, out timeneeded) )
                    timeneeded = int.Parse(AddTimeneeded);
                else 
                    {
                        System.Console.WriteLine("Your Timeneeded must be an integer");
                        TasksSync.StopConnection();
                        return;
                    }

                 System.Console.Write("PercentDone?");
                string AddPercentDone=Console.ReadLine();
                if(double.TryParse(AddPercentDone, out percent) )
                    percent = int.Parse(AddPercentDone);
                else 
                    {
                        System.Console.WriteLine("Your percentDone must be a double");
                        TasksSync.StopConnection();
                        return;
                    }
                    foreach(var t in TasksSync.collection)
                        if(t.Id==id)
                            duplicate++;
                    if(duplicate>0)
                        {
                            id=(TasksSync.collection.OrderByDescending(t=>t.Id).ToList().First().Id +1);
                                System.Console.WriteLine($":the ID you have choosen is duplicate,so it will be replaced with ID:{id} ");;
                        }

                var Addt = new Task(id,name,deadline,percent,timeneeded);
                TasksSync.Add(Addt);
                System.Console.WriteLine("Task is Added");
                TasksSync.StopConnection();
                


            }
            else if (request.ToLower()=="delete")
            {
                int id =0;

                System.Console.Write("Id?");
                string DelId =Console.ReadLine();
                if(int.TryParse(DelId, out id) )
                {
                    id = int.Parse(DelId);
                    bool exist=false;
                    int index =0;
                        //the IdOP is index here
                    foreach (Task t in TasksSync.collection)
                            {                
                                if (t.Id == int.Parse(DelId))
                                {
                                    index = TasksSync.collection.IndexOf(t);
                                    exist =true;
                                }
                            }
                            if(exist)
                            {
                                TasksSync.RemoveAt(index);
                                System.Console.WriteLine("Done");
                            }
                            else
                            System.Console.WriteLine( "this Task is not exist");
                }
                else 
                    {
                        System.Console.WriteLine("Your Id must be an integer");
                        TasksSync.StopConnection();
                        return;
                    }
                TasksSync.StopConnection();

            }

            else if (request.ToLower()=="edit")
            {



                int id =0;
                DateTime deadline =DateTime.Now;
                string name=" ";
                double percent =0.0;
                int timeneeded =0;

                System.Console.Write("Id?");
                string Id =Console.ReadLine();
                if(int.TryParse(Id, out id) )
                    id = int.Parse(Id);
                else 
                    {
                        System.Console.WriteLine("Your Id must be an integer");
                        TasksSync.StopConnection();
                        return;
                    }

                 System.Console.Write("New Name?");
                string EditName=Console.ReadLine();

                if(EditName ==null || EditName =="" || EditName ==" " )
                    {
                        System.Console.WriteLine("Task Name can't be null");
                            TasksSync.StopConnection();
                        return;
                    }
                else 
                    name = EditName;
                
                 System.Console.Write("New Deadline?");
                string EditDeadline=Console.ReadLine();

                if (DateTime.TryParse(EditDeadline, out deadline))
                    deadline= DateTime.Parse(EditDeadline);
                else 
                {
                    System.Console.WriteLine("You must give the date of deadline like 1/1/2001");
                    TasksSync.StopConnection();
                    return;
                }

                 System.Console.Write("New Timeneede?");
                string EditTimeneeded=Console.ReadLine();
                if(int.TryParse(EditTimeneeded, out timeneeded) )
                    timeneeded = int.Parse(EditTimeneeded);
                else 
                    {
                        System.Console.WriteLine("Your Timeneeded must be an integer");
                        TasksSync.StopConnection();
                        return;

                    }
                 System.Console.Write("New PercentDone?");
                string EditPercentDone=Console.ReadLine();
                if(double.TryParse(EditPercentDone, out percent) )
                    percent = int.Parse(EditPercentDone);
                else 
                    {

                        System.Console.WriteLine("Your percentDone must be a double");
                        TasksSync.StopConnection();
                        return;
                    }
                Task newTask= new Task(id,name,deadline,percent,timeneeded);
                bool exist=false;
                int index =0;
                foreach (Task t in TasksSync.collection)
                    {                
                        if (t.Id == int.Parse(Id))
                        {
                            index = TasksSync.collection.IndexOf(t);
                            exist =true;
                        }

                    }
                if(exist)
                {
                    TasksSync.RemoveAt(index);
                    System.Threading.Tasks.Task.Delay(2000);

                    TasksSync.Add(newTask);
                    

                }
                else
                    System.Console.WriteLine("this Task is not exist");
                System.Threading.Tasks.Task.Delay(2000);

                    
                TasksSync.StopConnection();

                
            }

            else if (request.ToLower()=="list")
            {
                        AllTasksBackUp.Clear();
                        foreach(var b in TasksSync.collection.OrderBy(t=>t.Id))
                            AllTasksBackUp.Add(b);
                        
                        TasksSync.collection.Clear();
                        foreach(var b in AllTasksBackUp.OrderBy(t=>t.Id))
                            TasksSync.collection.Add(b);
                        TasksSync.collection.ToList()
                        .ForEach(t=>System.Console.WriteLine($"{t.Id}: {t.Taskname}, {t.TaskdeadLine}"));
                    TasksSync.StopConnection();
            }
            else if (request.ToLower()=="search")
            {
                System.Console.WriteLine("Id / Name ?");
                string choice= Console.ReadLine();
                if (choice.ToLower()=="id")
                {
                    bool exist=false;
                    int SearchId =0;
                    string id = Console.ReadLine();
                    if(int.TryParse(id, out SearchId))

                        foreach(var t in TasksSync.collection)
                        {
                            if(t.Id== int.Parse(id))
                                {
                                    System.Console.WriteLine($" ID :{t.Id} , Name: {t.Taskname} , DeadLine:{t.TaskdeadLine.ToShortDateString()}");
                                    exist =true;
                                }

                        }
                    else 
                        System.Console.WriteLine("id must be an integer");

                    if(!exist)
                        System.Console.WriteLine("This task doesn't exist");

                }
                else if (choice.ToLower()=="name")
                {
                    
                    string name = Console.ReadLine();
                    if(name!=null && name!="")
                    {
                        foreach(var t in TasksSync.collection)
                            if(t.Taskname.Contains(name) )
                                System.Console.WriteLine($" ID :{t.Id} , Name: {t.Taskname} , DeadLine:{t.TaskdeadLine.ToShortDateString()}");
                    }
                    else
                        System.Console.WriteLine("name must not be  null");
                }
                else
                    System.Console.WriteLine("you must choose id or name");
                TasksSync.StopConnection();
                
            }
            else if (request.ToLower()=="sort")
            {
                System.Console.WriteLine("Id / Deadline ");
                string choice = Console.ReadLine();
                if(choice.ToLower()=="id")
                {

                        AllTasksBackUp.Clear();
                        foreach(var b in TasksSync.collection.OrderBy(t=>t.Id))
                            AllTasksBackUp.Add(b);
                        
                        TasksSync.collection.Clear();
                        foreach(var b in AllTasksBackUp.OrderBy(t=>t.Id))
                            TasksSync.collection.Add(b);
                        TasksSync.collection.ToList()
                        .ForEach(t=>System.Console.WriteLine($"{t.Id}: {t.Taskname}, {t.TaskdeadLine}"));
                }
                else if (choice.ToLower()=="deadline")
                {
                        AllTasksBackUp.Clear();
                        foreach(var b in TasksSync.collection.OrderByDescending(t=>t.TaskdeadLine))
                            AllTasksBackUp.Add(b);
                        
                        TasksSync.collection.Clear();
                        foreach(var b in AllTasksBackUp.OrderByDescending(t=>t.TaskdeadLine))
                            TasksSync.collection.Add(b);

                    TasksSync.collection.ToList()
                        .ForEach(t=>System.Console.WriteLine($"{t.Id}: {t.Taskname}, {t.TaskdeadLine}"));
                }
                else
                    System.Console.WriteLine("you must choose Id or Deadline");
                TasksSync.StopConnection();
                
            }
            else if (request.ToLower()=="exit")
            {
                TasksSync.StopConnection();
                break;
            }
            else
            {
                                System.Console.WriteLine("You must select one of the given options");
                TasksSync.StopConnection();
            }

            

        }
    

    }
}
