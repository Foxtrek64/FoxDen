# FoxDen

FoxDen is an application suite designed to centralize convention management into a convenient, easy to use, and extensible platform.

## Features

* Time tracking system with unlimited<sup>1</sup> users.
* Dynamic forms system with branching paths based on user response.
* JSON Rest API for integration with external services
* Backing with Directory Services providers like G-Suite, Azure AD, and OpenLDAP.
* Role-based Authentication

1: User limit determined by directory services backing.

## Project Layout

* FoxDen.App.Shared - The core user interface project. Most if not all UI code will live here, including business logic, view models, and views.
* ./Platforms/ - The platform-specific code for each platform.
  * FoxDen.App.Mobile - Android and iOS, MacOS
  * FoxDen.Skia.Gtk - Linux
  * FoxDen.Skia.Wpf and FoxDen.App.Skia.Host - Windows (8.1 and earlier)
  * FoxDen.App.UWP - Windows (10 and newer)
  * FoxDen.App.Wasm - WebAssembly
* ./Tests/ - XUnit Tests
