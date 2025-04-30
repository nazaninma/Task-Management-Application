namespace databaseLib;

public interface CRUD
{
    public Task GetTaskById(int id);
    public void CreatTask(int id , string Name ,DateTime deadLine , double perentCompeleted, int timeneeded);
    public void DeleteTask(int id);
    public void EditTask(int id , string newName ,DateTime newdeadLine , double newperentCompeleted, int newtimeneeded );
    public Task[] GetAllTask();
}