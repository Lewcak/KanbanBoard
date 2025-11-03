# Kanban Board 

This is a Kanban board application, built with an ASP.NET Core backend and an Angular frontend. It is a Web app for managing boards, columns, and tasks, including all logic for drag-and-drop re-ordering of tasks.

---

## Features

**Full-Stack Application:** A  backend API (ASP.NET Core) serves data to a single-page application (Angular).
**Frontend UI:** The user interface is built with **Angular** and **TypeScript**.
**Drag-and-Drop:** Utilizes the **Angular CDK** to allow users to select taskt and move them between columns to re-order them.
**Backend:**
    * Full CRUD for boards, columns, and tasks.
    * New boards are automatically created with "To Do", "In Progress", and "Done" columns.
    * A dedicated `/api/tasks/move` endpoint handles all complex re-ordering logic.
**Data Integrity:** The database is configured with **Cascade Deletes**, so deleting a board automatically clears all its child columns and tasks.

---

## Tech Stack

**Frontend:** Angular, TypeScript, Angular CDK (for Drag & Drop), HTML/CSS
**Backend:** ASP.NET Core Web API, C#
**Database:** MS SQL Server, Entity Framework Core (ORM)

---

## How to Run

### Backend (ASP.NET Core)

1) Clone this repository.
2) Open the `appsettings.json` file.
3) Change the `DefaultConnection` string to point to your local MS SQL Server instance.
4) Open a terminal in the project root and run the database migrations:
    ```bash
    dotnet ef database update
    ```
5) Run the application:
    ```bash
    dotnet run
    ```
6) The API will be running. Check your console for the `localhost` URL.

### Frontend (Angular)

This part of the project is currently in development.