#addin nuget:?package=Cake.SemVer&version=3.0.0
#addin nuget:?package=semver&version=2.0.4

// Enviroment
var isRunningBitrise = Bitrise.IsRunningOnBitrise;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.Android.Fingerprint.sln");
var artifactsDirectory = new DirectoryPath("artifacts");

// Versioning.
var version = CreateSemVer(1, 0, 2);

Setup((context) =>
{
	Information("Bitrise: {0}", isRunningBitrise);
	Information("Running on Windows: {0}", isRunningOnWindows);
	Information("Configuration: {0}", configuration);
});

Task("Clean")
	.Does(() =>
	{	
		CleanDirectory(artifactsDirectory);

		MSBuild(solutionFile, settings => settings
				.SetConfiguration(configuration)
				.WithTarget("Clean")
				.SetVerbosity(Verbosity.Minimal));
	});

Task("Restore")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		MSBuild(solutionFile, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));
	});

Task ("NuGet")
	.IsDependentOn ("Build")
	.Does (() =>
	{
		Information("Nuget version: {0}", version);
		
  		var nugetVersion = Bitrise.Environment.Repository.GitBranch == "master" ? version.ToString() : version.Change(prerelease: "pre" + Bitrise.Environment.Build.BuildNumber).ToString();
	
		NuGetPack ("./nuspec/Xamarin.Android.FingerprintAuthenticator.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = nugetVersion,
					Verbosity = NuGetVerbosity.Normal,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	
	});

Task("Default")
	.IsDependentOn("NuGet")
	.Does(() => {});

RunTarget(target);