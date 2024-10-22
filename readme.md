## Credits
- Learned from: https://www.youtube.com/watch?v=DuNLR_NJv8U&t=284s

## How to run in vscode
- > .NET Maui: Pick start up project & start up device
- run the app

## Blazor & Maui interop
- Create Blazor Maui project(A)
- Create Blazor project(B)
 - to start Blazor project in vscode, select Program.cs and run with right top start button.
- Create RazorClassLibrary(C)
- Add reference
```bash 
# in folder A
dotnet add reference ../C #folder name
# in folder B
dotnet add reference ../C #folder name
```

