# Deployment Guide

This project can be containerised and run locally using Docker and
Docker Compose.  Containerisation provides an easy way to get the API
and its dependencies (SQL Server) up and running without installing
anything on your host machine.  It also mirrors the setup you might
use for cloud hosting.

## Prerequisites

* [Docker](https://www.docker.com/get-started) and
  [Docker Compose](https://docs.docker.com/compose/) installed on your
  machine.
* Optional: an IDE or editor for working with the code.

## Building and Running with Docker Compose

1. **Build and start the containers:** In the root of the repository
   run:

   ```bash
   docker-compose up --build
   ```

   This command builds the API image using the provided `Dockerfile`
   and starts both the API and SQL Server containers.  The first
   startup may take a few minutes while Docker pulls base images.

2. **Access the API:** Once the containers are running, the API will
   be reachable at `http://localhost:5000`.  You can test endpoints
   using a tool like curl or Postman.  For example:

   ```bash
   curl http://localhost:5000/api/users
   ```

3. **Stopping the stack:** To stop and remove the containers, press
   `Ctrl+C` in the terminal where compose is running, then execute:

   ```bash
   docker-compose down
   ```

   This removes the containers but preserves the SQL data volume
   (`sql-data`) so your database state is persisted between runs.

## Cloud Deployment

For deployment to a cloud platform such as Azure App Service or AWS
Elastic Beanstalk, you can publish the API from Visual Studio or the
`dotnet` CLI and provision a managed SQL Server instance.  The
containerisation approach shown here is also compatible with Azure
Container Instances, Azure Kubernetes Service, or Amazon ECS.  See the
official documentation for those services for detailed instructions.