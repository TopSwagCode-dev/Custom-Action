#!/bin/sh -l

echo "Testing echo"
echo $(ls)
dotnet /app/Custom-Action-Console.dll $@