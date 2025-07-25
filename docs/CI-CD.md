# CI/CD Notes for NetFullStack

This document outlines a high‑level approach to implementing
continuous integration and continuous deployment for the NetFullStack
project using either **Azure DevOps** or **Jenkins**.  These notes are
intended as guidance and are not executable in this repository.

## Azure DevOps Pipeline

1. **Repository Setup:**  Host the project on GitHub and link it to
   Azure DevOps via a service connection.  Grant the pipeline access to
   fetch code and update build statuses.
2. **Build Stage:**
   * Use the `ubuntu-latest` agent pool.
   * Install .NET 8 SDK and Node.js via built‑in tasks.
   * Restore NuGet packages: `dotnet restore backend/NetFullStack.API.csproj`.
   * Build the API: `dotnet build --configuration Release`.
   * Run unit tests if present: `dotnet test`.
   * Install front‑end dependencies: `npm install --prefix frontend`.
   * Build the front‑end: `npm run build --prefix frontend`.
   * Publish build artefacts (e.g. compiled API and `frontend/dist`).
3. **Release Stage:**
   * Deploy the API to an Azure App Service using the `Azure Web App` deploy task.
   * Deploy the front‑end to an Azure Static Web App or blob storage.
4. **Environment Variables:**  Store connection strings and API keys in
   Azure Key Vault and reference them in the pipeline via variable
   groups.
5. **Optional:**  Add quality gates such as code analysis, linting
   (`eslint` for React, `dotnet format` for C#), and vulnerability
   scanning.

## Jenkins Pipeline

1. **Jenkinsfile:**  Create a `Jenkinsfile` at the root of the
   repository with declarative syntax.  Define stages for checkout,
   build, test and deploy.
2. **Stages:**
   ```groovy
   pipeline {
     agent any
     tools { nodejs 'Node 18' }
     stages {
       stage('Checkout') {
         steps { checkout scm }
       }
       stage('Restore & Build API') {
         steps {
           sh 'dotnet restore backend/NetFullStack.API.csproj'
           sh 'dotnet build backend/NetFullStack.API.csproj -c Release'
         }
       }
       stage('Build Front‑end') {
         steps {
           sh 'npm install --prefix frontend'
           sh 'npm run build --prefix frontend'
         }
       }
       stage('Test') {
         steps { sh 'dotnet test' }
       }
       stage('Publish') {
         steps {
           // publish artefacts or deploy to your environment
         }
       }
     }
   }
   ```
3. **Credentials:**  Store secrets such as database passwords and
   deployment keys in Jenkins credentials and inject them via
   environment variables.
4. **Agents:**  Use self‑hosted agents or containers with the .NET SDK
   and Node.js installed.

## General Recommendations

* Use pull request triggers to build and test code on every merge.
* Run linter and unit tests early in the pipeline to catch issues.
* Enable caching of NuGet packages and npm modules to speed up builds.
* Publish build artefacts so they can be downloaded and deployed from
  later stages or different environments.
* Add status badges to the README once pipelines are set up.
