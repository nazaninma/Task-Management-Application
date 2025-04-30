
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace SynchronizedCollection;

public class SynchronizedCollection<T> : IList<T>, INotifyCollectionChanged
{

    public SynchronizedCollection(string url, string name)
    {
        conn = new HubConnectionBuilder()
        .WithUrl(@"http://localhost:5000/crud")
        .WithAutomaticReconnect()
            .Build();
        this.collection.CollectionChanged += this.CollectionChanged;

    }
    public System.Threading.Tasks.Task refreshTask { get; set;}

    public Action<Action> UIDispatcher { get; set;}

    public void StartConnection()
    {
        conn.On<T>("AddTaskClient",  (T t) =>
        {
            UIDispatcher(async () =>  
            {
                collection.Add(t);
                await System.Threading.Tasks.Task.Delay(1000);
            });
        });


        conn.On<int>("DeleteTaskClient", ( t) => 
        {
            UIDispatcher(async()=>
            {
                collection.RemoveAt(t);
                await System.Threading.Tasks.Task.Delay(1000);
            });
        });        

        conn.On<T>("ReciveDatabase", (T item) =>   
        {
            UIDispatcher(async() =>  
            {
                collection.Add(item);
            });
            
        });


        conn.StartAsync();
        
    }

    public void StartConnectionBlazorServer()
    {
        conn.On<T>("AddTaskClient",async (T t) => 
        {
            collection.Add(t);
            await System.Threading.Tasks.Task.Delay(1000);
        });
        conn.On<T>("ReciveDatabase",async (T item) => 
        {
             collection.Add(item);
        });
        conn.On<int>("DeleteTaskClient", async( t) => 
        {
            collection.RemoveAt(t);
            await System.Threading.Tasks.Task.Delay(1000);
        });
        

        conn.StartAsync();
        
        
    }

    public void StartConnectionBlazorWASM()
    {
        conn.On<T>("AddTaskClient", (T t) =>
        {
         collection.Add(t);
         refresh();

        });     
        conn.On<int>("DeleteTaskClient", ( t) => 
        {
            collection.RemoveAt(t);
            refresh();
        });  
         conn.On<T>("ReciveDatabase", (T item) => 
         {
          collection.Add(item);
          refresh();

    });

        conn.StartAsync();
        
        
    }

    private async void refresh()
    {
        await refreshTask;
    }

    public void Add(T item)
    {
        conn.InvokeAsync("AddTaskServer", item);

    }
    public void Clear()
    {
        collection.Clear();
        conn.InvokeAsync("ClearTaskServer");
    }

    public void RemoveAt(int index)
    {

         conn.InvokeAsync("DeleteTaskServer", index);


    }
    public bool Contains(T item)
    {
        return collection.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        collection.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return collection.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return collection.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        collection.Insert(index, item);
    }

    public bool Remove(T item)
    {
        return collection.Remove(item);
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    public void StopConnection()
    {
        conn.StopAsync().Wait();
    }

    public T this[int index] { get => ((IList<T>)collection)[index]; set => ((IList<T>)collection)[index] = value; }
    public int Count => ((ICollection<T>)collection).Count;
    public bool IsReadOnly => ((ICollection<T>)collection).IsReadOnly;
    public ObservableCollection<T> collection = new ObservableCollection<T>();

    public HubConnection conn;
    public static string Url = " ";
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

}



