# Book Shop Service

## Endpoints

- [x] GET - /api/authors/{id} - Gets author with id, first name, last name and a list of all his/her book titles.
- [x] POST - /api/authors - Creates a new author with first name and last name (mandatory).
- [x] GET - /api/authors/{id}/books	- Gets books from author by id. Returns all data about the book + category names.

## Features

- [x] AutoMapper
- [x] EntityFramework Core with SQL Server and ASP.NET Identity
- [x] JWT authentication/authorization
- [x] File logging with Serilog
- [x] Stylecop
- [x] Neat folder structure

```
├───src
│   ├───configuration
│   └───server
│       ├───BookShop.Api
│       ├───BookShop.Business
│       ├───BookShop.Core
│       ├───BookShop.Data
│       └───BookShop.Data.EntityFramework
└───tests
    └───BookShop.Business.Tests

```

- [x] Swagger UI + Fully Documented Controllers
- [x] Global Model Errors Handler
- [x] Global Environment-Dependent Exception Handler

### Test Suite
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly
- [x] Arrange Act Assert Pattern

-> Powerd by Dev Adventures .NET Core project template. (https://devadventures.net/2018/06/09/introducing-dev-adventures-net-core-project-template/)
