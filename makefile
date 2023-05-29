all : clean restore test build

clean:
	dotnet clean

restore:
	dotnet restore --interactive

test: 
	dotnet test

build:
	dotnet build 
