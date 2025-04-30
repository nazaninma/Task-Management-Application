using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;
using SynchronizedCollection;

namespace APProj.wpf
{

    public partial class MainWindow : Window , INotifyPropertyChanged
    {
        public SynchronizedCollection<Task> TasksSync = new SynchronizedCollection<Task>(@"http://localhost:5000","crud");
        
        public string TaskidOP{   
            get => _TaskidOP; 
            set {
                _TaskidOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("TaskidOP"));
            }
            }
        public string TasknameOP{ 
            get => _TasknameOP; 
            set {
                _TasknameOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("TasknameOP"));
            }
            }

        public string listBoxBind{ 
            get => _listBoxBind; 
            set {
                _listBoxBind = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("listBoxBind"));
            }
            }
        public string deadlineOP{ 
            get => _deadlineOP; 
            set {
                _deadlineOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("deadlineOP"));
            }
            }

        public string TaskPercentOP{ 
            get => _TaskPercentOP; 
            set {
                _TaskPercentOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("TaskPercentOP"));
            }
            }

        public string TimeneededOP{ 
            get => _TimeneededOP; 
            set {
                _TimeneededOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("TimeneededOP"));
            }
            }


        public string theIdOP{ 
            get => _theIdOP; 
            set {
                _theIdOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("theIdOP"));
            }
            }

       public string EditIdOP{  
            get => _EditIdOP; 
            set {
                _EditIdOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("EditIdOP"));
            }
            }


        public string EditnameOP{ 
            get => _EditnameOP; 
            set {
                _EditnameOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("EditnameOP"));
            }
            }

        public string EditdeadlineOP{ 
            get => _EditdeadlineOP; 
            set {
                _EditdeadlineOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("EditdeadlineOP"));
            }
            }


        public string EditTimeneededOP{ 
            get => _EditTimeneededOP; 
            set {
                _EditTimeneededOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("EditTimeneededOP"));
            }
            }


        public string EditPercentOP{ 
            get => _EditPercentOP; 
            set {
                _EditPercentOP = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("EditPercentOP"));
            }
            }
        public string SearchingTask{
            get => _SearchingTask; 
            set {
                _SearchingTask = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(
                        this, 
                        new PropertyChangedEventArgs("SearchingTask"));
            }
            }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            TasksSync.UIDispatcher = Application.Current.Dispatcher.Invoke;
            this.McDataGrid.DataContext = this.TasksSync.collection;
            TasksSync.StartConnection();
        }
// StartConnection برای این هست که از دیتابیس یک کپی بگیرد که بتوانیم از ان برای قابلیت جستوجو و مرتب کردن استفاده کنیم
        public void StartConnetction(object Sender ,  RoutedEventArgs args)
        {
                
            foreach(var t in TasksSync.collection)
                AllTasksBackUp.Add(t);
        }
        public  void AddTask (object Sender ,  RoutedEventArgs args)
        {
            int duplicate=0;
            int test= 0;
            double test2=0;

                if ( string.IsNullOrWhiteSpace(this.TasknameOP))
                {
                        MessageBox.Show("Task Name can't be empty!");
                        return;
                }
                 if ( string.IsNullOrWhiteSpace(this.deadlineOP))
                {
                        MessageBox.Show("Task Deadline can't be empty!");
                        return;
                }
                if ( string.IsNullOrWhiteSpace(this.TimeneededOP)|| int.TryParse(TimeneededOP,out test)==false)
                {
                        MessageBox.Show("Task Timeneeded can't be empty!");
                        return;
                }
                if ( string.IsNullOrWhiteSpace(this.TaskPercentOP)|| double.TryParse(TaskPercentOP,out test2)==false)
                {
                    TaskPercentOP="0";
                        MessageBox.Show("Task PercentDone will save 0!");
                       
                }

                if ( string.IsNullOrWhiteSpace(this.TaskidOP) || int.TryParse(TaskidOP,out test)==false)
                {
                        MessageBox.Show("Task Id can't be empty and must be an integer!");
                        return;
                       
                }
        foreach(var t in TasksSync.collection)
            if(t.Id==int.Parse(TaskidOP))
                duplicate++;
        if(duplicate>0)
            {
             TaskidOP=(TasksSync.collection.OrderByDescending(t=>t.Id).ToList().First().Id +1).ToString();
               MessageBox.Show($":the ID you have choosen is duplicate,so it will be replaced with ID:{TaskidOP} ");
             }


            Task newTask= new Task(int.Parse(TaskidOP),TasknameOP,DateTime.Parse(deadlineOP),double.Parse(TaskPercentOP),int.Parse(TimeneededOP));
            TasksSync.Add(newTask);
            AllTasksBackUp.Add(newTask);  
            MessageBox.Show("Adding Task is done ");          
        }

        public  void Remove (object Sender ,  RoutedEventArgs args)
        {
            int test= 0;

                if ( string.IsNullOrWhiteSpace(this.theIdOP) || int.TryParse(this.theIdOP , out test)==false)
                {
                        MessageBox.Show("Task ID can't be empty and It must be an integer!");
                        return;
                }
            bool exist=false;
            int index =0;
            foreach (Task t in TasksSync.collection)
                {                
                    if (t.Id == int.Parse(theIdOP))
                     {
                         index = TasksSync.collection.IndexOf(t);
                         exist =true;

                     }

                }
            if(exist)
            {
                AllTasksBackUp.RemoveAt(index);
                TasksSync.RemoveAt(index);
                MessageBox.Show("Removing Task is done");
            }
            else
                MessageBox.Show("this Task is not exist");
            
                
                              
        }
// تعریف کردم مجبور شدم ویرایش را اینگونه انجام دهمrecord را به صورت task به دلبل ابنکه من   

        public void EditTask(object Sender ,  RoutedEventArgs args)
        {
            int test= 0;
            double test2=0;


                if ( string.IsNullOrWhiteSpace(EditIdOP) ||int.TryParse(EditIdOP,out test)==false)
                {
                        MessageBox.Show("Task ID can't be empty and the id must be an integer ");
                        return;
                }
                 if ( string.IsNullOrWhiteSpace(EditnameOP))
                {
                        MessageBox.Show("Task New Name can't be empty!");
                        return;
                }

                 if ( string.IsNullOrWhiteSpace(EditdeadlineOP))
                {
                        MessageBox.Show("Task New Deadline can't be empty!");
                        return;
                }
                if ( string.IsNullOrWhiteSpace(EditTimeneededOP)||int.TryParse(EditTimeneededOP,out test)==false)
                {
                        MessageBox.Show("Task New Timeneeded can't be empty and the Timeneeded must be an integer");
                        return;
                }

                if ( string.IsNullOrWhiteSpace(EditPercentOP)||double.TryParse(EditPercentOP,out test2)==false)
                {
                        MessageBox.Show("Task New Percentdone can't be empty and the percent done must be a double");
                        return;
                }


            Task newTask= new Task(int.Parse(EditIdOP),EditnameOP,DateTime.Parse(EditdeadlineOP),double.Parse(EditPercentOP),int.Parse(EditTimeneededOP));
            bool exist=false;
            int index =0;
            foreach (Task t in TasksSync.collection)
                {                
                    if (t.Id == int.Parse(EditIdOP))
                     {
                         index = TasksSync.collection.IndexOf(t);
                         exist =true;
                     }

                }
            if(exist)
            {
                TasksSync.RemoveAt(index);
                TasksSync.Add(newTask);
                MessageBox.Show("Done");
            }
            else
                MessageBox.Show("this Task is not exist");

            AllTasksBackUp.Clear();
            foreach(var t in TasksSync.collection.OrderBy(t=>t.Id))
                AllTasksBackUp.Add(t);
        }


        public void SearchTask(object Sender ,  RoutedEventArgs args)
        {

            if ( string.IsNullOrWhiteSpace(ListBox1.Text))
                {
                        MessageBox.Show("choose type of searching");
                        return;
                }

            if((ListBox1.Text)=="SearchById" )
            {
                int numericValue;
                if(int.TryParse(Search.Text, out numericValue))
                {
                     Task[]  IdResult =AllTasksBackUp.Where(t=>t.Id==(int.Parse(this.SearchingTask))).ToArray(); 
                                                                                                                              
                        TasksSync.collection.Clear();
                       foreach(var t in IdResult)
                            TasksSync.collection.Add(t);
                        MessageBox.Show("done");
                }
                else 
                {
                        MessageBox.Show("write The ID Number");
                        return;
                }

            }

            else if((ListBox1.Text)=="SearchByName")
            {
                if ( string.IsNullOrWhiteSpace(this.SearchingTask))
                {
                        MessageBox.Show("write the name of task");
                        return;
                }
                Task[] NameResult = AllTasksBackUp.Where(t=>t.Taskname.Contains($"{this.SearchingTask}")).ToArray();
                TasksSync.collection.Clear();
                foreach (var t in NameResult)
                        TasksSync.collection.Add(t);

                MessageBox.Show("done");
            }


        }
        

        public void SortDuaDate(object Sender ,  RoutedEventArgs args)
        {
            AllTasksBackUp.Clear();
            foreach(var b in TasksSync.collection.OrderByDescending(t=>t.TaskdeadLine))
                AllTasksBackUp.Add(b);
            
            TasksSync.collection.Clear();
            foreach(var b in AllTasksBackUp.OrderByDescending(t=>t.TaskdeadLine))
                TasksSync.collection.Add(b);

        }


        public void SortId(object Sender ,  RoutedEventArgs args)
        {
            AllTasksBackUp.Clear();
            foreach(var b in TasksSync.collection.OrderBy(t=>t.Id))
                AllTasksBackUp.Add(b);
            
            TasksSync.collection.Clear();
            foreach(var b in AllTasksBackUp.OrderBy(t=>t.Id))
                TasksSync.collection.Add(b);

        }
        public void ShowTasks(object Sender ,  RoutedEventArgs args)
        {

            AllTasksBackUp.Clear();
            foreach(var b in TasksSync.collection.OrderBy(t=>t.Id))
                AllTasksBackUp.Add(b);
            
            TasksSync.collection.Clear();
            foreach(var b in AllTasksBackUp.OrderBy(t=>t.Id))
                TasksSync.collection.Add(b);
        }
        // Start Add properties
        private string _TasknameOP; //need
        private string _TimeneededOP; //need
        private string _TaskidOP; //need
        private string _TaskPercentOP; //need
        private string _deadlineOP; //need
        // end Add properties

// Start delete properties
        private string _theIdOP; //need
// end delete properties
        private string _listBoxBind; //need

        private string _SearchingTask; //need
        public List<Task> AllTasksBackUp = new List<Task>();
// Start edit properties
        
        private string _EditIdOP; //need
        private string _EditnameOP; //need
        private string _EditdeadlineOP; //need
        private string _EditTimeneededOP; //need
        private string _EditPercentOP; //need
// end edit properties

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
