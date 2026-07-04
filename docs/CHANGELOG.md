# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed

- **Reverted an incompatible `Microsoft.Graph` bump.** The `v1.0.50` tag bumped `Microsoft.Graph` 5.105.0 → 6.1.0, which transitively requires `Microsoft.Kiota.Abstractions >= 2.0.0` — a downgrade conflict with this package's pinned `1.22.2`, so restore failed with `NU1605` and the release never reached NuGet (`v1.0.50`'s "Publish to NuGet" GitHub Action failed; no `v1.0.50` package exists). Reverted `Microsoft.Graph` to `5.105.0` and pinned it (`AutoUpdate="false"`) alongside the existing Kiota pin so the two move together only as a deliberate, coordinated upgrade. `v1.0.51` is the first working release since `v1.0.49`.

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
