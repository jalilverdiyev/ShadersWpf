using System;
using System.IO;
using System.Threading.Tasks;

namespace ShadersWpf.Helpers;

public static class ResourcesHelper
{
	public const string ResourcesBasePath = "pack://application:,,,/";

	public static async Task CopyResource(string resource, string destinationFile)
	{
		await using var fs = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.ReadWrite,
				FileShare.ReadWrite, 4096, true);

		await using var defaultsStream = GetResourceStream(resource);

		if (defaultsStream == null)
		{
			fs.Close();
			return;
		}

		await defaultsStream.CopyToAsync(fs);
		fs.Close();
		defaultsStream.Close();
	}

	public static Stream? GetResourceStream(string resource)
		=> App.GetResourceStream(new Uri(resource))?.Stream;

	public static Uri GetShaderUri(string shaderAlias)
		=> new Uri(string.Format(shaderAlias, ResourcesBasePath + "Shaders/"));
}
