
using System.Net.Http;
using OpenAI_API;
using OpenAI_API.Completions;
namespace AiLib;
public class Motivate
{

     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public string chat_input_interst="";
    public string chat_input_task="";
    public string chat_output="";
    public  void Motivation()
    {
        chat_output= UseChatGPTMotivation(chat_input_interst, chat_input_task).Result;
    }

    public async Task<string> UseChatGPTMotivation(string interest , string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = 
        $"I have to do the following task, but i'm not in the mood at all.I am very interested in {interest}.can you give me enough motivation to do this task by using {interest} .My task is {taskN}";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}
public class BreakTask
{
     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public int chat_input_time;
    public string chat_input_task="";
    public string chat_output="";
    public  void Breaking()
    {
        chat_output= UseChatGPTBreaking(chat_input_time, chat_input_task).Result;
    }

    public async Task<string> UseChatGPTBreaking(int subTaskMinutes , string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = $"Please break down the following task into {subTaskMinutes} minute subtask. please use the JSON template which contains the task number, the task name and the task description .the task is : ";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}


public class Why
{

     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public string chat_input_work="";
    public string chat_input_task="";
    public string chat_output="";
    public  void WhyShouldStudy()
    {
        chat_output= UseChatGPTWhy(chat_input_work, chat_input_task).Result;
    }

    public async Task<string> UseChatGPTWhy(string job , string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = 
        $"Why is learning {taskN} useful for me as {job}";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}


public class Poem
{

     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public string chat_input_task="";
    public string chat_output="";
    public  void WritePoem()
    {
        chat_output= UseChatGPTPoem( chat_input_task).Result;
    }

    public async Task<string> UseChatGPTPoem( string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = 
        $"Write a short poem about {taskN}";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}
//new
public class BestWorst
{

     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public string chat_input_work="";
    public string chat_input_task="";
    public string chat_output="";
    public  void BestWorstResutl()
    {
        chat_output= UseChatGPTBestWorst(chat_input_work, chat_input_task).Result;
    }

    public async Task<string> UseChatGPTBestWorst(string job , string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = 
        $"What is the best result I can get from {taskN} and the worst result I can get from not {taskN} as{job}";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}


public class MakeFun
{

     public OpenAIAPI openai = new OpenAIAPI(Secrets.OpenAIKey);
    public CompletionRequest completionRequest = new CompletionRequest();
    public string chat_input_task="";
    public string chat_output="";
    public  void MakeItFun()
    {
        chat_output= UseChatGPTMakeFun( chat_input_task).Result;
    }

    public async Task<string> UseChatGPTMakeFun( string  taskN)
    {
        string outputResult = "";
        completionRequest.Prompt = 
        $"How can I make {taskN} fun for me";
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}
//new 

// public static class Secrets
// {
//        public static string OpenAIKey = YOUR KEY;
// }