namespace APProjS.Test;
using Moq;
using System.Threading;
using Microsoft.AspNetCore.SignalR;

[TestClass]
public class UnitTest1
{


    public ToDoClass Todo;
    [TestMethod]
    public void TestGetTaskById()
    {
        Todo= new ToDoClass();
        var result=Todo.GetTaskById(2);
        Assert.AreEqual(result.Taskname , "Study Math");
    }

    [TestMethod]
    public void TestDeleteTask()
    {
        Todo= new ToDoClass();
        Todo.DeleteTask(5);
        foreach(var t in Todo.MyTodo.ToList())
            Assert.IsFalse(t.Id==5);
    }

     [TestMethod]
    public void TestCreatTask()
    {

        Todo= new ToDoClass();
        Todo.DeleteTask(5);
        Todo.CreatTask(5, "Art exam" , DateTime.Parse("7/2/2023"), 0,11);
        
        Assert.AreEqual(Todo.GetTaskById(5).Taskname , "Art exam");
    }
    
     [TestMethod]
    public void TestEditTask()
    {
        Todo= new ToDoClass();
        Todo.EditTask(5, "Art exam" , DateTime.Parse("7/2/2023"), 0,5);
        
        Assert.AreEqual(Todo.GetTaskById(5).Tasktimeneeded , 5);
    }

    [TestMethod]
    public void TestGetAllTask()
    {
        Todo= new ToDoClass();
       Task[] GetTAsk= Todo.GetAllTask();
       var result =Todo.MyTodo.ToArray();
        for(int i=0; i<Todo.MyTodo.ToArray().Length;i++)
            Assert.AreEqual(result[i].Taskname , GetTAsk[i].Taskname);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task SynchroniztionCllectionHubAddTaskServerTest()
    {
        Todo= new ToDoClass();
        Mock<IHubCallerClients> mockClients =new Mock<IHubCallerClients>(MockBehavior.Strict);
        Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
        mockClients.Setup(clients=>clients.All).Returns(mockClientProxy.Object);
        SynchronizedCollectionHub<Task> syncHub = new SynchronizedCollectionHub<Task>

            {
                Clients=mockClients.Object
            };
        Task test = new Task (50,"testingSignalR",DateTime.Parse("7/1/2023"),10,2);
        await syncHub.AddTaskServer(test,Todo);
        mockClients.Verify(clients=>clients.All ,Times.Once);
        mockClientProxy.Verify(clientproxy=>clientproxy.SendCoreAsync("AddTaskClient",It.Is<object[]>(o=>o[0]==test),default(CancellationToken)));
    }

    [TestMethod]
    public async System.Threading.Tasks.Task SynchroniztionCllectionHubDeleteTaskServerTest()
    {
    
        Todo= new ToDoClass();
        Mock<IHubCallerClients> mockClients =new Mock<IHubCallerClients>(MockBehavior.Strict);
        Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
        mockClients.Setup(clients=>clients.All).Returns(mockClientProxy.Object);
        SynchronizedCollectionHub<Task> syncHub = new SynchronizedCollectionHub<Task>

            {
                Clients=mockClients.Object
            };
        int test = 1;
        await syncHub.DeleteTaskServer(test,Todo);
        mockClients.Verify(clients=>clients.All ,Times.Once);
        mockClientProxy.Verify(clientproxy=>clientproxy.SendCoreAsync("DeleteTaskClient",It.Is<object[]>(o=>(int)o[0]==test),default(CancellationToken)));
    }

}