# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed

- **Dropped the `Cirreum.AuthenticationProvider` reference.** It was added in `v1.0.51` solely for `IGraphEnabledBuilder`, which has since relocated to `Cirreum.Contracts`/`Cirreum.Domain` (host-agnostic profile enrichment doesn't belong in the Authentication feature track). `IGraphEnabledBuilder`/`IExternalGraphEnabledBuilder` now flow in transitively through the existing `Cirreum.Domain` reference (re-pinned to `1.2.0`) — no source changes needed.

## [1.0.52] - 2026-07-04

### Fixed

- **Coordinated `Microsoft.Graph` / `Microsoft.Kiota.Abstractions` upgrade.** Bumped `Microsoft.Graph` 5.105.0 → 6.2.0 and `Microsoft.Kiota.Abstractions` 1.22.2 → 2.0.0 together (the pair `v1.0.51` pinned after the `v1.0.50` `NU1605` incident). The original Kiota floor was set for a CVE (`GHSA-7j59-v9qr-6fq9`/`CVE-2026-44503`, a redirect-handler header-leak, fixed in `1.22.0`) — `2.0.0` clears that regardless, and Microsoft.Graph 6.x's `Microsoft.Graph.Core 4.x` dependency requires it. Both `AutoUpdate="false"` pins stay in place at the new versions, so a routine sweep still can't bump one without the other.

## [1.0.51] - 2026-07-04

### Fixed

- **Reverted an incompatible `Microsoft.Graph` bump.** The `v1.0.50` tag bumped `Microsoft.Graph` 5.105.0 → 6.1.0, which transitively requires `Microsoft.Kiota.Abstractions >= 2.0.0` — a downgrade conflict with this package's pinned `1.22.2`, so restore failed with `NU1605` and the release never reached NuGet (`v1.0.50`'s "Publish to NuGet" GitHub Action failed; no `v1.0.50` package exists). Reverted `Microsoft.Graph` to `5.105.0` and pinned it (`AutoUpdate="false"`) alongside the existing Kiota pin so the two move together only as a deliberate, coordinated upgrade.
- **Migrated off legacy `Cirreum.Core` onto the foundation reset packages** (`Cirreum.Domain` + `Cirreum.AuthenticationProvider`), completing this repo's Tier-2 cutover. This was blocked pending two gaps in the target packages — both now fixed upstream: `IGraphEnabledBuilder`/`IExternalGraphEnabledBuilder` were documented as relocated to `Cirreum.AuthenticationProvider` but never actually shipped there (fixed in `Cirreum.AuthenticationProvider 1.1.1`), and `IUserPresenceBuilder.AddPresenceService<T>()` never made the jump to `Cirreum.Domain` (fixed in `Cirreum.Domain 1.1.2`). No source changes in this repo beyond the dependency swap — same public surface, same behavior. `v1.0.51` is the first working release since `v1.0.49`.

## [1.0.50] - 2026-07-04

### Updated

- Updated NuGet packages.

## [1.0.49] - 2026-05-10

### Updated

- Updated NuGet packages.

## [1.0.48] - 2026-05-08

### Updated

- Updated NuGet packages.

## [1.0.47] - 2026-05-07

### Updated

- Updated NuGet packages.

## [1.0.46] - 2026-05-01

### Updated

- Updated NuGet packages.
