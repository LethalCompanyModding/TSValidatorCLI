# TSValidatorCLI

A small commandline tool to validate thunderstore packages locally before upload

## Usage

Use `--help` to see all command line switches. Default use is `-f <foldername>` to validate all files contained in the package.

## Current Validations

- Manifest.json is present
  - And valid JSON
  - And Contains all required keys
  - [Warn] And does not contain the `installers` key (unused)
 
- Icon.png is present
  - And contains a valid IHDR header
  - And has the correct width&height

- [Warn] License is present
- [Warn] Changelog.md is present
