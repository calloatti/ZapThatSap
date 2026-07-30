Include ..\AGENTS.md

# Zap That Sap — Mod-Specific Agent Instructions

## Identity
- **Assembly:** `zapthatzap`
- **Namespace:** `Calloatti.ZapThatSap`
- **Framework:** Harmony (ID `"calloatti.zapthatsap"`)
- **ModId:** `Calloatti.ZapThatSap`
- **Min Game Version:** 1.0.12.5 — uses `timberborn-decompiled-1.0.*`

## What This Mod Does
Hides the sap/resin visual on Pine trees by patching `GatherableModel.UpdateMaterial` to disable the detail texture (`_EnableDetail = 0`).

## Source Architecture (`Version-1.0/Source/`)

| File | Role |
|---|---|
| `ZapThatSap.cs` | `IModStarter` entry point + `GatherableModel.UpdateMaterial` prefix patch |
