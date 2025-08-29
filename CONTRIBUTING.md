# How to Contribute

> Note:
>
> For now I won't be taking any contributions.

## Formatting

This project requires the tool `csharpier` to format the code. This is one of the tools liksted in the
`config/dotnet-tools.json` file, and so it should be installed during restoration of the solution.

Follow the [instructions](https://csharpier.com/docs/Installation) from `csharpier` to install and configure it
correctly.

## About Warnings

At some point, warnings will be enforced as errors, and so you will need to resolve them before submitting a PR.

## Naming

Be verbose with variables, methods, and classes, to reduce the amount of comments required. Self-documenting code is the
key.

## General Guidelines

- **DO** use explicity types combined with non-explicit `new` for class instantiation (i.e.: `MyClass myClass = new())`
- **DO** prefer `var` over explicit types for anything that has been returned to you and you haven't instantiated.
- **DO** prefer `var` over explicit types for basic types, like `int` or `string`.
- **DO** use uppercase letters for hexadecimal numbers and for subscripts.
- **DO** respect `csharpsquid` warnings (through `SonarQube`) and either resolve them or explicitly suppress them to
  indicate why that warning is not necessary.
- **DO** respect roslyn and compiler warnings and treat them as errors, suppressing them if the warnings are not
  necessary for that portion of the code.
- Do **NOT** use naked loops if possible. Use `foreach` or `LINQ` instead.
- Do **NOT** expose imported or unsafe methods, including in code that imports from an external, native library. Wrap it
  safely.
- **DO** separate integers by thousands (i.e.: `1_000 + 10_000 + 10_000_000`).
- **DO** separate hexadecimal numbers by groups of bytes and **ALWAYS** set the amount of bytes of the type (i.e.:
  `var hex1 = 0x00_00_00_01U; var hex2 = 0x00_00_00_00_00_00_00_01UL;`)
