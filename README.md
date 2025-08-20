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

* **Back‑end API:** A RESTful API built with ASP.NET Core 8.0.  It
  exposes CRUD endpoints for managing users and their tasks and now
  supports **JWT Bearer authentication**.  The API uses
  Entity Framework Core to model a one‑to‑many relationship (a user
  can have multiple task items) and includes seed data loaded at
  runtime.  Swagger UI is enabled to explore the endpoints and an
  `/api/auth/login` endpoint issues tokens for authenticated calls.
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
* **Tests:** The solution now includes an `backend.Tests` project with
  integration tests written using xUnit and
  `Microsoft.AspNetCore.Mvc.Testing`.  These tests spin up the API in
  memory and verify that key endpoints return the expected status and
  data structures.  While optional, they illustrate best practices
  around automated testing and give recruiters confidence in the code
  quality.
  of how an Azure DevOps or Jenkins pipeline could build, test and
  deploy the back‑end and front‑end components.  Screenshots and
  YAML snippets can be added if actual pipelines are configured.

## 🛠️ Technologies Used

| Layer      | Tech                           | Notes                                        |
|------------|--------------------------------|-----------------------------------------------|
| Back‑end   | ASP.NET Core Web API           | .NET 8.0, minimal hosting model, dependency injection |
| Security   | JWT Bearer Authentication      | Protects API endpoints and issues JSON Web Tokens |
| Data       | Entity Framework Core + SQL Server | Code‑first models backed by SQL scripts and stored procedures |
| Front‑end  | React 18 (JSX)                 | Functional components, hooks, fetch API       |
| Tooling    | Swagger / OpenAPI             | Automatic API documentation                   |
| Testing    | xUnit, WebApplicationFactory  | Integration tests validate API endpoints      |
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

## ✅ Latest Improvements (August 2025)

The project has been updated to include modern DevOps and deployment tooling:

* **Frontend containerization:** A multi‑stage Dockerfile has been added to `/frontend` to build and serve the React UI with Nginx.
* **GitHub Actions CI workflow:** See `.github/workflows/ci.yml` for a pipeline that restores, builds and tests both the back‑end and front‑end on every commit.
* **Kubernetes manifests:** Deployment and Service manifests for the back‑end and front‑end are included in the `/k8s` directory for easy deployment to any K8s cluster.
* **Improved documentation:** Additional docs describe how to run the application locally with Docker Compose and deploy it to the cloud.

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

To obtain a **JWT token** for calling protected endpoints, send a
`POST` request to `/api/auth/login` with a JSON body containing a
`username` (matching one of the seeded users) and an optional
`email`.  The response will include a `token` property that you
should include in the `Authorization` header of subsequent requests
as `Bearer &lt;token&gt;`.

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

> **Note:** The current front‑end does not implement authentication.  
> Protected API endpoints (those requiring a token) are intended for
> demonstration purposes only.  You can extend the UI to support
> logging in and storing the JWT token for authenticated calls.

### 5. Explore stored procedures (optional)

Once the database is set up you can call the stored procedures
directly in SQL Server Management Studio:

```sql
EXEC usp_GetTasksByUser @UserId = 1;
EXEC usp_GetUserCount;
```

### 6. Run the tests (optional)

If you wish to execute the automated tests, restore the test project and run the
test suite using the .NET CLI:

    cd backend.Tests
    dotnet restore
    dotnet test

The tests spin up the API in memory and verify that the `/api/users` endpoint
returns a successful response.  You can add more tests to cover additional
endpoints and scenarios.

### 7. Running with Docker (optional)

If you'd like to run the API and SQL Server without installing any
development tools, you can leverage Docker Compose.  A `Dockerfile`
and `docker-compose.yml` are included at the root of the repository.

```bash
# build and start the API and database
docker-compose up --build

# query the running API
curl http://localhost:5000/api/users

# when finished, stop and remove containers
docker-compose down
```

This will start a SQL Server container with a default SA password and
an API container bound to port 5000.  Data is persisted in a
named volume (`sql-data`) between runs.  See
`docs/DEPLOYMENT.md` for further details.

## 🎓 Resume Alignment

The design of this project intentionally mirrors the real‑world
experience described in my professional resume.  Over the past
**4+ years** I have worked as a .NET developer for major
organisations such as **Johnson & Johnson** and **TATA Capital**.  In
these roles I built and maintained web applications using
ASP.NET Core, C#, VB.NET and MVC, implemented RESTful services and
optimised SQL Server queries and stored procedures.  I also
contributed to migrating legacy systems to .NET Core, resulting in
performance improvements and reduced technical debt.  This sample
project reflects those same competencies:

* **.NET & C#:** The back‑end API is built with ASP.NET Core 8 using a
  minimal hosting model, dependency injection and EF Core.  This
  demonstrates my day‑to‑day work designing RESTful services and
  reusable components in C#.
* **SQL Server Expertise:** Tables, foreign key relationships, seed
  data and stored procedures mirror the data‑layer tasks I have
  performed professionally.  In past roles I created indexes and
  optimised queries, which is reflected here through structured
  scripts and sample stored procedures.
* **Full‑Stack Development:** On the front‑end I have worked with
  Angular 8+ and React JS to build responsive user interfaces.  This
  project uses a React client to fetch and display data from the
  API, showcasing my ability to integrate the front‑end with the
  back‑end and provide a cohesive user experience.
* **Performance & Migration:** At Johnson & Johnson I improved web
  service performance by 35 percent and migrated legacy VB.NET
  applications to .NET Core.  Here I demonstrate similar
  architecture patterns that enable scalability and maintainability.
* **CI/CD & Cloud:** In my previous roles I automated builds and
  deployments using Azure DevOps and Jenkins, reducing deployment
  times significantly.  This repository now includes a GitHub Actions
  workflow that restores, builds and tests both the back‑end and front‑end
  on every push or pull request.  See `.github/workflows/ci.yml` for the
  pipeline.  The `/docs/CI‑CD.md` file still outlines how to build and
  deploy using other tools, reflecting my experience with CI/CD
  practices and cloud platforms.

## 🧭 Future Enhancements

* **Authentication implemented:** This project now includes a basic JWT
  Bearer implementation.  Future work could expand this to a full
  identity system with user registration, password hashing and role‑based
  authorisation policies.
* Add validation and error handling to the API controllers.
* Replace the in‑memory seed with migrations and real data stores.
* Improve the front‑end with routing, state management (e.g. Redux)
  and styling frameworks (e.g. Tailwind CSS or Material UI).
* Enhance the existing GitHub Actions pipeline to include automated
  deployment to a cloud platform (Azure App Service, AWS or Kubernetes)
  and add build/test status badges to this README.
* Package the back‑end and front‑end into Docker images and publish them
  to a container registry (Docker Hub or GitHub Container Registry).  Kubernetes
  deployment manifests are provided in `/k8s` to run the services in a cluster.
* Deploy the back‑end to Azure App Service and the front‑end to
  GitHub Pages, Netlify or Vercel.

## 📄 License

This project is provided as a sample portfolio and is licensed under
the MIT License.  Feel free to fork, extend or customise it for your
own learning and job search.
