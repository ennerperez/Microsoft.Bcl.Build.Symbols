# Runs every time a package is installed in a project

# param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.

param($installPath, $toolsPath, $package, $project)
	# This is the MSBuild targets file to add
	$targetsFile = [System.IO.Path]::Combine($installPath, 'build', 'Microsoft.Bcl.Build.Symbols.targets')

	# Need to load MSBuild assembly if it's not loaded yet.
	Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
	# Grab the loaded MSBuild project for the project
	$msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1

	$importToRemove = $msbuild.Xml.Imports | Where-Object { $_.Project.Endswith(‘Microsoft.Bcl.Build.Symbols.targets’) }
	# Add the import and save the project
	$msbuild.Xml.RemoveChild($importToRemove) | out-null
	$project.Save()

	# Make the path to the targets file relative.
	$projectUri = new-object Uri('file://' + $project.FullName)
	$targetUri = new-object Uri('file://' + $targetsFile)
	$relativePath = $projectUri.MakeRelativeUri($targetUri).ToString().Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)

	# Add the import and save the project
	$msbuild.Xml.AddImport($relativePath) | out-null
	$project.Save()