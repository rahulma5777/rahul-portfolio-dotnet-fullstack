<!--
    This README describes the NetFullStack project.  It was created as part of an
    interview exercise to demonstrate proficiency with .NET, SQL Server,
    React and CI/CD tooling.  The structure and content has been organised
    to be recruiter‑friendly: clear overview, concise instructions and
    alignment with the author's professional resume.  For more details
    or enhancements see the `/docs` folder.
-->

# NetFullStack – Full‑Stack Portfolio Project

NetFullStack is a full‑stack web application that showcases hands‑on
experience across back‑end, front‑end and database development using
**ASP.NET Core**, **SQL Server** and **React**.  The project is deliberately
kept simple so that recruiters and interviewers can quickly understand
its structure, run it locally and evaluate the technical skills it
demonstrates.  The codebase is thoroughly documented and organised to
reflect common production patterns such as dependency injection, entity
relationships, RESTful endpoints and a clear separation of concerns.

## ✨ Project Features

* **Back‑end API:** A RESTful API built with ASP.NET Core 8.0.  It
  exposes CRUD endpoints for managing users and their tasks.  The API
  uses Entity Framework Core to model a one‑to‑many relationship (a
  user can have multiple task items) and includes seed data loaded at
  runtime.  Swagger UI is enabled to explore the endpoints.
* **Database Layer:** SQL scripts define the schema, relationships and
  stored procedures for a SQL Server database.  An initial data seed
  script creates sample users and tasks.  Stored procedures show how
  to encapsulate common queries (e.g. retrieving tasks by user).
* **Front‑end UI:** A lightweight React application consumes the API
  and displays live data.  It demonstrates component‑based
  development, hooks for state management and responsive layouts using
  simple CSS.  You can fetch users, view their tasks and add new
  users from the browser.
* **CI/CD Notes:** The `/docs` folder includes a high‑level description
  of how an Azure DevOps or Jenkins pipeline could build, test and
  deploy the back‑end and front‑end components.  Screenshots and
  YAML snippets can be added if actual pipelines are configured.

## 🛠️ Technologies Used

| Layer      | Tech                           | Notes                                        |
|------------|--------------------------------|-----------------------------------------------|
| Back‑end   | ASP.NET Core Web API           | .NET 8.0, minimal hosting model, dependency injection |
| Data       | Entity Framework Core + SQL Server | Code‑first models backed by SQL scripts and stored procedures |
| Front‑end  | React 18 (JSX)                 | Functional components, hooks, fetch API       |
| Tooling    | Swagger / OpenAPI             | Automatic API documentation                   |
| CI/CD      | Azure DevOps or Jenkins (docs only) | Pipelines described in `/docs/CI‑CD.md`      |

## 📂 Repository Structure

```
.netfullstack/
├── backend/         # ASP.NET Core Web API
│   ├── Controllers/ # RESTful endpoints
│   ├── Data/        # EF Core DbContext and seed
│   ├── Models/      # Plain C# entity classes
│   ├── NetFullStack.API.csproj
│   ├── Program.cs
│   └── appsettings.json
├── sql/             # SQL Server schema & procedures
│   ├── schema.sql   # Table and relationship definitions
│   ├── seed.sql     # Insert sample data
│   ├── stored_procedures.sql # Stored procedures
│   └── init.sql     # Runs schema + seed + procedures
├── frontend/        # React UI
│   ├── public/
│   ├── src/
│   ├── package.json
│   └── README.md    # Front‑end specific instructions
├── docs/            # Optional additional documentation
│   └── CI‑CD.md     # Notes on CI/CD pipelines
├── .gitignore       # Ignores .NET, Node and OS artefacts
└── README.md        # You are here!
```

## 🚀 Getting Started

These instructions assume you have **.NET 8 SDK**, **SQL Server** and
**Node.js** installed locally.  The code has not been tested in this
container, so please follow the steps on your own machine.

### 1. Clone the repository

```bash
git clone https://github.com/your‑github‑username/.netfullstack.git
cd .netfullstack
```

### 2. Set up the database

1. Create a new SQL Server database (e.g. `NetFullStackDb`).
2. Run the scripts in `/sql` in order:
   ```sql
   -- In SQL Server Management Studio or sqlcmd:
   :r schema.sql
   :r seed.sql
   :r stored_procedures.sql
   ```
   Alternatively, execute `init.sql` to run everything at once.
3. Update the connection string in `/backend/appsettings.json` to point
   at your database instance.

### 3. Run the back‑end API

```bash
cd backend
dotnet restore        # restores NuGet packages
dotnet run            # runs the API on http://localhost:5000
```
The API will automatically create the database (if using code‑first)
and seed sample data.  Swagger UI will be available at
`/swagger`.

### 4. Run the front‑end UI

```bash
cd frontend
npm install           # installs React dependencies
npm run dev           # runs the development server on http://localhost:3000
```
Open your browser to `http://localhost:3000` to view the UI.  The
application will fetch data from the API at `http://localhost:5000` by
default.  You can configure the API base URL in
`frontend/src/services/api.js`.

### 5. Explore stored procedures (optional)

Once the database is set up you can call the stored procedures
directly in SQL Server Management Studio:

```sql
EXEC usp_GetTasksByUser @UserId = 1;
EXEC usp_GetUserCount;
```

## 🎓 Resume Alignment

This project is intentionally designed to align with the skills and
experience described in my resume:

* **.NET & C#:** The back‑end uses ASP.NET Core with minimal hosting,
  dependency injection, controllers, EF Core and LINQ queries.
* **SQL Server:** Tables, relationships, seed data and stored
  procedures demonstrate familiarity with relational modelling and
  T‑SQL.  Scripts are separated into schema, seed and procedures for
  clarity.
* **Front‑end Development:** A React front‑end consumes the API
  asynchronously and updates the UI in real time.  Component
  composition and hooks illustrate modern front‑end patterns.
* **RESTful Design:** Endpoints follow REST conventions (GET, POST,
  PUT, DELETE) and return appropriate HTTP status codes.  Swagger
  documents the API.
* **CI/CD:** Although no live pipelines are included here, the
  `/docs/CI‑CD.md` file outlines how to build, test and deploy the
  solution with Azure DevOps or Jenkins.  This demonstrates an
  understanding of continuous integration and delivery practices.

## 🧭 Future Enhancements

* Implement authentication & authorisation (e.g. JWT Bearer tokens).
* Add validation and error handling to the API controllers.
* Replace the in‑memory seed with migrations and real data stores.
* Improve the front‑end with routing, state management (e.g. Redux)
  and styling frameworks (e.g. Tailwind CSS or Material UI).
* Configure actual CI/CD pipelines and include status badges in this
  README.
* Deploy the back‑end to Azure App Service and the front‑end to
  GitHub Pages, Netlify or Vercel.

## 📄 License

This project is provided as a sample portfolio and is licensed under
the MIT License.  Feel free to fork, extend or customise it for your
own learning and job search.
