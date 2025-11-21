# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Common Development Commands

**Build:**
```bash
dotnet restore Cirreum.Graph.Provider.slnx
dotnet build Cirreum.Graph.Provider.slnx --configuration Release --no-restore
```

**Pack:**
```bash
dotnet pack Cirreum.Graph.Provider.slnx --configuration Release --no-build --output ./artifacts
```

**Local Development Version:**
- Uses version `1.0.100-rc` for Release builds when built locally
- CI/CD uses version from Git tags

## Architecture

This is a .NET 10.0 library that provides Microsoft Graph integration for the Cirreum foundation framework, specifically focused on user presence functionality.

**Key Components:**
- `IGraphServiceClientProvider` - Core abstraction for accessing Microsoft Graph API with proper authentication and scoping
- `MsGraphPresenceService` - Implementation that maps Microsoft Graph presence data to Cirreum's presence model
- `GraphEnabledBuilderExtensions` - Configuration extensions for registering Graph presence services

**Core Pattern:**
The provider pattern is used to manage Graph client lifecycles and authentication. The `IGraphServiceClientProvider.UseClientAsync()` methods create properly authenticated clients for the duration of operations, handling token acquisition and scope management automatically.

**Presence Mapping:**
Microsoft Graph availability statuses are mapped to standardized `PresenceStatus` enum values:
- `Available/AvailableIdle` → `Available`
- `Busy/BusyIdle` → `Busy`  
- `Away/BeRightBack` → `Away`
- `DoNotDisturb` → `DoNotDisturb`
- `Offline` → `Offline`
- `PresenceUnknown` → `Unknown`

**Dependencies:**
- `Microsoft.Graph` (5.97.0) for Graph API access
- `Cirreum.Core` (1.0.11) for foundational types and interfaces

## Project Structure

**Build Configuration:**
- Targets .NET 10.0 with latest C# language version
- Nullable reference types enabled
- Uses MSBuild props files in `/build` for shared configuration
- CI/CD detection and versioning handled in `Directory.Build.props`

**Code Style:**
- EditorConfig enforces tab indentation and CRLF line endings
- Namespace must match folder structure (IDE0130)
- Primary constructors preferred
- Using directives inside namespaces
- Interface naming with 'I' prefix required