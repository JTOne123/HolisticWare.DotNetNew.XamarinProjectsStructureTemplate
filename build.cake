// run this
// dotnet cake --settings_skipverification=true

#addin nuget:?package=Cake.FileHelpers
#addin "MagicChunks"

DateTime dt = DateTime.Now;

FilePath file = GetFiles ($"./nuget/*.nuspec").ToList () [0];	
Information($"{FileReadText(file)}");

string version = $"{dt.Year}.{dt.Month}.{dt.Day}.{dt.ToString("HHmm")}";

// https://github.com/xamarin/GoogleApisForiOSComponents/blob/master/update.cake
// XmlPoke
// (
// 	file,
//     "/ns:package/ns:metadata/ns:version/",
//     version,
//     new XmlPokeSettings 
// 	{
//         Namespaces = new Dictionary<string, string> 
// 		{
//             { "ns", "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd" },
//         }
//     }
// );

// https://github.com/sergeyzwezdin/magic-chunks
TransformConfig
(
	file.ToString(), 
	"HolisticWare.DotNetNew.XamarinProjectsStructureTemplate.CSharp.nuspec",
	new TransformationCollection 
	{
		{ "package/metadata/version", version },
	}
);

int exit_code = StartProcess
(
	"nuget", 
	new ProcessSettings
	{ 
		Arguments = $"pack HolisticWare.DotNetNew.XamarinProjectsStructureTemplate.CSharp.nuspec" 
	}
);

// This should output 0 as valid arguments supplied
Information("Exit code: {0}", exit_code);

// NuGetPackSettings settings =
// 						new NuGetPackSettings 
// 									{
// 										Version = version
// 									}
// 						;
// NuGetPack
// (
// 	"./nuget/HolisticWare.DotNetNew.XamarinProjectsStructureTemplate.CSharp.nuspec",
// 	settings
// );