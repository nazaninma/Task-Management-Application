# Task Management Application  

A professional task management application developed as part of the Advanced Programming course project. The application follows SOLID principles and modern software development practices, leveraging technologies like Entity Framework, SignalR, and OpenAI.  

## Features  

### Core Modules  
1. **Database**: SQL-based database for storing task-related data.  
2. **CRUD Module**: Implemented using Entity Framework (C# library) with unit tests.  
3. **Web API**: RESTful API with Swagger support for database interactions.  
4. **AI Module**: Integration with OpenAI for intelligent task suggestions and automation.  
5. **Distributed MVVM Library**: Synchronized task management across multiple clients using SignalR.  

### User Interfaces  
6. **WPF Client**: Desktop application for task management.  
7. **Blazor Server & WASM**: Web-based interfaces for task management.  
8. **Command-Line Interface (CLI)**: For quick task operations via terminal.  


## Technologies Used  
- **Backend**: C#, .NET, Entity Framework, SignalR.  
- **Frontend**: WPF, Blazor.  
- **AI Integration**: OpenAI API for task automation and suggestions.  


## Key Highlights  
- **Real-Time Synchronization**: Tasks updated in real-time across all clients using SignalR.  
- **AI-Powered Features**:  
  - Task breakdown into subtasks.  
  - Motivational prompts based on user interests.  
  - Smart task categorization.  
- **Multi-Platform Support**: Desktop and web.  

## Setup Instructions  
 
   ```bash  
   git clone [my-repo-url]
   dotnet restore
   dotnet run   
