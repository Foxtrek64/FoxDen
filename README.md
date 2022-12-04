# FoxDen

FoxDen is an application suite designed to centralize convention management into a convenient, easy to use, and extensible platform.

## Features

* Time tracking system with unlimited<sup>1</sup> users.
* Dynamic forms system with branching paths based on user response.
* JSON Rest API for integration with external services
* Backing with Directory Services providers like G-Suite, Azure AD, and OpenLDAP.
* Role-based Authentication

1: User limit determined by directory services backing.

## Components

* FoxDen.API - Provides a REST API for integrating with services. Can be ignored if not needed.
* FoxDen.Data - Common libraries for communicating with the database and sharing data types. Used by both FoxDen.API and FoxDen.Web
* FoxDen.Web - Blazor WebAssembly application used for accessing the management interface.

## Database Support

By default, Microsoft SQL Server is supported. Support for PostgreSQL is planned but is not yet implemented.