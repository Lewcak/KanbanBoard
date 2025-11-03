# KanbanBoard

Features

    Created a backend API that serves data to angular frontend.

    Frontend: The UI is built with Angular and TypeScript

    Drag-and-Drop Interface: Will be using the Angular CDK to allow users to move tasks between columns and re-order them.

    Full CRUD for boards, columns, and tasks.

    New boards are automatically created with "To Do", "In Progress", and "Done" columns.

    Dedicated /api/tasks/move endpoint handles all re-ordering logic.

    The database is configured with Cascade Deletes, so deleting a board automatically clears all its child columns and tasks.

Tech Stack

    FrontEnd: Angular, TypeScript, Angular CDK (for Drag & Drop), HTML/CSS
    Backend: ASP.NET Core Web API, C#
    Database: MS SQL Server, Entity Framework Core (ORM)

How to Run

    Backend (ASP.NET Core)

        Clone this repository and open the backend project folder.

        Open the appsettings.json file.

        Change the DefaultConnection string to point to your local MS SQL Server instance.

        Open a terminal in the project root and run the database migrations:

        dotnet ef database update

        Run the application:
        
        dotnet run

        Check console for localhost where it is running

    Frontend Angular

        This is currently not completed
