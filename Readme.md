# About the project

This is a simple ASP.Net Core WebAPI Application with mocked database.
Application manages database records of nested folders:
It lists parent Catalog and lets Add, Edit or Remove selected Catalog by entering it ID.
More of that, Catalog could be requested by Id.
There is a name validation for Catalog, because of that you can't add a Catalog to the same parent with the same name.
Database records CRUD operations are implemented on the Backend side and are accessed through the REST requests.
Backend part implements Repository Patern in order to separate responsibilities.
Project API is the backend part, and project ClientApp is a Frontend part, which is a simple ConsoleApp.

## Build with

- .NET Core 3.1

## Published

The site is published at https://gamedb-denis.herokuapp.com/

## REST API

The REST API to the app is described below.

### Get a Root Catalog

`GET api/catalog/`

### Get a specific Catalog

`GET api/catalog/{id}`

### Add a specific Catalog

`POST api/catalog/`
with body:
{"name":"...","parentid":"..."}

### Edit a specific Catalog

`PUT api/catalog/{id}`
with body:
{"name":"...","parentid":"...", catalogids:["...", "..."]}

### Remove a specific Catalog with it's children

`DELETE api/catalog/{id}`

