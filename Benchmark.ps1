Push-Location $PSScriptRoot

./Build.ps1

foreach ($test in ls test/*.Benchmarks) {
    Push-Location $test

	echo "perf: Running benchmark project in $test"

    & dotnet test -c Release
    if($LASTEXITCODE -ne 0) { exit 2 }

    Pop-Location
}

Pop-Location