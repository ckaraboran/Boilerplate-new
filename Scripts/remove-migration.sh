#!/usr/bin/env bash

Help()
{
   # Display Help
   echo "This script removes last migration from the project."
   echo
   echo "Syntax: remove-migration"
   echo
}

while getopts ":h" option; do
   case $option in
      h) # display Help
         Help
         exit;;
     \?) # incorrect option
         echo "Error: Invalid option"
         exit;;
   esac
done

dotnet ef migrations remove --project ../Libraries/Boilerplate.Infrastructure -s ../Presentation/Boilerplate.Api/Boilerplate.Api.csproj
echo "Last migration has been removed"
