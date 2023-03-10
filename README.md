# Clean Architecture real word Example Movies API

Movies API  - created with following Clean Architecture principles with .Net Core Web API 6.

## Give a Star! :star:
If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Table Of Contents
  
- [Design Decisions and Dependencies](#design-decisions-and-dependencies)
  * [The Application Project](#the-application-project)
  * [The Application.Contracts Project](#the-application-contracts-project)
  * [The Domain Project](#the-domain-project)
  * [The Infrastructure Project](#the-infrastructure-project)
  * [The Persistence Project](#the-infrastructure.persistence-project)
  * [The SharedKernel Project](#the-sharedkernel-project)
  
  * [The Web- API Project](#the-web-project)  


## Features and Technologies
 - Onion Architecture
 - JWT Identity
 - Entity Framework Core - Code First
 - Database Seeding
 - Audit Implementation
 - Repository Pattern
 - Swagger UI
 - API Versioning
 - Pagination      
 - Global Api Exception Filter
 - Global Api Validation Filter
 - Fluent Validation
 - FluentValidation Localization
 - AutoMapper (Dynamic)
 - AppGuards simple implementation
 
## Versions

The master branch is now using .NET 6.

# Design Decisions and Dependencies

The goal of this sample is to provide a fairly bare-bones starter kit for new projects. It does not include every possible framework, tool, or feature that a particular enterprise application might benefit from. Its choices of technology for things like data access are rooted in what is the most common, accessible technology for most business software developers using Microsoft's technology stack. It doesn't (currently) include extensive support for things like logging, monitoring, or analytics, though these can all be added easily. Below is a list of the technology dependencies it includes, and why they were chosen. Most of these can easily be swapped out for your technology of choice, since the nature of this architecture is to support modularity and encapsulation.

## The Core Layer

The Core layer is the center of the Clean Architecture design, and all other project dependencies should point toward it. As such, it has very few external dependencies. 
The Core layer contains The Application Project and The Domain Project.

## The Domain Project
This will contain all entities, enums, domain exceptions, interfaces, types and logic specific to the domain layer.

## The Application Project

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

## The Application.Contracts Project

This layer contains all application abstractions, data transfer objects and some shared datas related to application layer. For example, if the application need to access a notification service, a new interface would be added to application contracts layer and an implementation would be created within infrastructure. Or if application need some EntityService, a new interface would be added to application contracts layer and implementation would be created within application layer.


## The Infrastructure Layer
The Infrastructure Project and The Persistence Project.

## The Infrastructure Project

Most of your application's dependencies on external resources should be implemented in classes defined in the Infrastructure project. These classes should implement interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have multiple Infrastructure projects (e.g. Infrastructure.Data), but for most projects one Infrastructure project with folders works fine. The sample includes data access, but you would also add things like email providers, file access, web api clients, etc. to this project so they're not adding coupling to your Core or UI projects.

## The Infrastructure Persistence Project
When you use relational databases such as SQL Server, Oracle, or PostgreSQL, a recommended approach is to implement the persistence layer based on Entity Framework (EF). EF supports LINQ and provides strongly typed objects for your model, as well as simplified persistence into your database.

The Infrastructure project depends on `Microsoft.EntityFrameworkCore.SqlServer`. The former is used because it's built into the default ASP.NET Core templates and is the least common denominator of data access. If desired, it can easily be replaced with a lighter-weight ORM like Dapper. In this case, ConfigureServices class can be used in the Infrastructure class to allow wireup of dependencies there, without the entry point of the application even having to have a reference to the project or its types.

## The SharedKernel Project

Many solutions will also reference a separate **Shared Kernel** project/package. I recommend creating a separate SharedKernel project and solution if you will require sharing code between other modules. I further recommend this be published as a NuGet package (most likely privately within your organization) and referenced as a NuGet dependency by those projects that require it. For this sample, in the interest of simplicity, I've added a SharedKernel project to the solution. It contains types that would likely be shared between multiple modules (VS solutions, typically), in my experience. 

## The Web API Project

The entry point of the application is the ASP.NET Core Web API project. It currently uses the default  ASP.NET Core API project template code. This includes its configuration system, which uses the default `appsettings.json` file plus environment variables and configured services. The project delegates to the `Infrastructure` project.


