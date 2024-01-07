.PHONY: build
build:
	dotnet build

.PHONY: test
test:
	dotnet test

.PHONY: run
run:
	dotnet run --project Satellite.API --profile https

.PHONY: restore
restore:
	dotnet restore

.PHONY: publish
publish:
	dotnet publish -c Release -o out