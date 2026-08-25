# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.69] - 2026-08-25

### Updated

- Updated NuGet packages.

## [1.0.68] - 2026-08-25

### Updated

- **`Microsoft.Graph` 6.2.0 to 6.5.0, and the version pins are lifted.** 6.5.0 resolves the same
  dependency graph 6.2.0 did — `Microsoft.Graph.Core` 4.0.1, the `Microsoft.Kiota.*` 2.0.0 set,
  `Std.UriTemplate` 2.0.8 — so nothing else moved with it. `AutoUpdate="false"` is removed from
  `Microsoft.Graph`: the pin existed because no test suite could tell whether a bump had broken
  anything, and the package now has one.
- **The direct `Microsoft.Kiota.Abstractions` reference is dropped.** It existed only as the other
  half of the pin — a floor for `Microsoft.Graph.Core` 4.x, which `Microsoft.Graph` already brings.
  Unpinned but still declared, it could have been swept to a Kiota major that `Graph.Core` 4.x was
  not built against. It now resolves transitively at the version Graph asks for.
- Updated NuGet packages.


## [1.0.67] - 2026-08-24

### Updated

- Updated NuGet packages.

## [1.0.66] - 2026-08-20

### Updated

- Updated NuGet packages.

## [1.0.65] - 2026-08-19

### Updated

- Updated NuGet packages.

## [1.0.64] - 2026-08-17

### Updated

- Updated NuGet packages.

## [1.0.63] - 2026-08-04

### Updated

- Updated NuGet packages (Cirreum spine 4.2.0 wave: `Cirreum.Contracts` 4.2.0 / `Cirreum.Domain` 4.2.0 and current patch releases).

## [1.0.62] - 2026-07-31

### Updated

- Updated NuGet packages (Cirreum spine 4.0.1 wave: `Cirreum.Contracts` 4.0.1 / `Cirreum.Domain` 4.0.1 / `Cirreum.Kernel` 2.0.1 / `Cirreum.AuthenticationProvider` 2.0.3).

## [1.0.61] - 2026-07-30

### Updated

- Re-pinned `Cirreum.Domain` `2.0.0` → `3.0.0` — restores operation-authorization enforcement
  (the fail-open intercept fix shipped in Domain 2.0.1/3.0.0) and adopts the `IPolicyAuthorizer`
  vocabulary; see Cirreum.Domain `MIGRATION-v3.md`.

## [1.0.60] - 2026-07-27

### Updated

- Updated NuGet packages.

## [1.0.59] - 2026-07-24

### Updated

- Updated NuGet packages.

## [1.0.58] - 2026-07-24

### Updated

- Updated NuGet packages.

## [1.0.57] - 2026-07-20

### Updated

- Updated NuGet packages.

## [1.0.56] - 2026-07-19

### Updated

- Updated NuGet packages.

## [1.0.53] - 2026-07-04

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
