#!/usr/bin/env bash

dotnet ef database update --project ../Libraries/Boilerplate.Infrastructure -s ../Presentation/Boilerplate.Api/Boilerplate.Api.csproj
echo "Database has been updated"