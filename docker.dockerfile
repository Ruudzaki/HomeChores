name: Home Chores CI

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '7.x'
      - name: Restore Dependencies
        run: dotnet restore HomeChores.sln
      - name: Build Solution
        run: dotnet build HomeChores.sln --no-restore
      - name: Run Tests
        run: dotnet test HomeChores.sln --no-build --verbosity normal
