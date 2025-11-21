# Cirreum.Graph.Provider

[![NuGet Version](https://img.shields.io/nuget/v/Cirreum.Graph.Provider.svg?style=flat-square&labelColor=1F1F1F&color=003D8F)](https://www.nuget.org/packages/Cirreum.Graph.Provider/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Cirreum.Graph.Provider.svg?style=flat-square&labelColor=1F1F1F&color=003D8F)](https://www.nuget.org/packages/Cirreum.Graph.Provider/)
[![GitHub Release](https://img.shields.io/github/v/release/cirreum/Cirreum.Graph.Provider?style=flat-square&labelColor=1F1F1F&color=FF3B2E)](https://github.com/cirreum/Cirreum.Graph.Provider/releases)
[![License](https://img.shields.io/github/license/cirreum/Cirreum.Graph.Provider?style=flat-square&labelColor=1F1F1F&color=F2F2F2)](https://github.com/cirreum/Cirreum.Graph.Provider/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-003D8F?style=flat-square&labelColor=1F1F1F)](https://dotnet.microsoft.com/)

**Microsoft Graph integration for user presence tracking in Cirreum applications**

## Overview

**Cirreum.Graph.Provider** provides seamless Microsoft Graph API integration for the Cirreum foundation framework, enabling real-time user presence monitoring and status synchronization.

### Key Features

- **Authenticated Graph Access**: Simplified, scoped access to Microsoft Graph APIs with automatic token management
- **Presence Mapping**: Intelligent mapping of Microsoft Graph presence data to standardized Cirreum presence models
- **Service Integration**: Easy registration with Cirreum's dependency injection container
- **Lifecycle Management**: Proper handling of Graph client lifecycles and authentication scopes

### Usage

Register the Microsoft Graph presence service in your Blazor WebAssembly application:

```csharp
builder.Services
    .AddCirreumFoundation()
    .WithGraphUserPresence(refreshInterval: 30000); // 30 seconds
```

The service automatically:
- Authenticates with Microsoft Graph using your configured authentication provider
- Polls for presence updates at the specified interval
- Maps Graph presence statuses to Cirreum's standardized presence model
- Updates the application's presence state in real-time

### Presence Status Mapping

| Microsoft Graph Status | Cirreum Status |
|------------------------|----------------|
| Available, AvailableIdle | Available |
| Busy, BusyIdle | Busy |
| Away, BeRightBack | Away |
| DoNotDisturb | DoNotDisturb |
| Offline | Offline |
| PresenceUnknown | Unknown |

### Architecture

The library uses a provider pattern with `IGraphServiceClientProvider` to manage Graph client authentication and lifecycle. This ensures proper token acquisition and scope management for each operation while respecting service lifetimes.

## Requirements

- .NET 10.0 or later
- Microsoft Graph API access with appropriate permissions for presence data
- Cirreum.Core package for foundational framework support

## Contribution Guidelines

1. **Be conservative with new abstractions**  
   The API surface must remain stable and meaningful.

2. **Limit dependency expansion**  
   Only add foundational, version-stable dependencies.

3. **Favor additive, non-breaking changes**  
   Breaking changes ripple through the entire ecosystem.

4. **Include thorough unit tests**  
   All primitives and patterns should be independently testable.

5. **Document architectural decisions**  
   Context and reasoning should be clear for future maintainers.

6. **Follow .NET conventions**  
   Use established patterns from Microsoft.Extensions.* libraries.

## Versioning

Cirreum.Graph.Provider follows [Semantic Versioning](https://semver.org/):

- **Major** - Breaking API changes
- **Minor** - New features, backward compatible
- **Patch** - Bug fixes, backward compatible

Given its foundational role, major version bumps are rare and carefully considered.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Cirreum Foundation Framework**  
*Layered simplicity for modern .NET*