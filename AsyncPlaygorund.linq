<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	Task<string> responseHtmlTask ;
	string url = string.Empty;
	try
	{	
		url = "http://www.google.com";
		Task<string> response = DownloadFileFromUrlAsync(url);
		Console.WriteLine(response.Result);		

		url = "http://www.google.com";
		string response2 = DownloadFileFromUrlUsingTaskRun(url);
		Console.WriteLine(response2);

		url = "http://www.ernieboy.coms";
		responseHtmlTask = DownloadFileFromUrlAsync2(url);
		Console.WriteLine(responseHtmlTask.Result);
	}
	catch (Exception ex)
	{
		string error = BuildExceptionDetail(ex,new StringBuilder()).ToString();
		Console.WriteLine(error);
	}	
}

// Define other methods and classes here

public static string DownloadFileFromUrlUsingTaskRun(string url)
{
	string result = string.Empty;
	Task.Run(async () =>
	{
		using (var httpClient = new HttpClient())
		{
			Task<string> responseTask = httpClient.GetStringAsync(url);
			result = await responseTask;
		}
	}).Wait();
	return result;
}

public static async Task<string> DownloadFileFromUrlAsync(string url)
{
	string result = string.Empty;
	using (var httpClient = new HttpClient())
	{
		Task<string> responseTask = httpClient.GetStringAsync(url);
		result = await responseTask;
	}
	return result;
}

public static async Task<string> DownloadFileFromUrlAsync2(string url)
{
	string result = string.Empty;
	using (var httpClient = new HttpClient())
	{
		result = await httpClient.GetStringAsync(url);
	}
	return result;
}

/// <summary>
///     Returns full details about an Exception object
/// </summary>
/// <param name="ex">The Exception object which was thrown when the error occurred</param>
/// <param name="sb">A string builder object to build the final error message string</param>
/// <returns>A string builder object which contains the final error message</returns>
public static StringBuilder BuildExceptionDetail(Exception ex, StringBuilder sb)
{
	if (ex == null) throw new NullReferenceException("ex");
	if (sb == null) throw new NullReferenceException("sb");
	sb.AppendLine("Message: " + ex.Message);
	sb.AppendLine("Source: " + ex.Source);
	sb.AppendLine("TargetSite: " + ex.TargetSite);
	sb.AppendLine("StackTrace: " + ex.StackTrace);

	//Loop recursivly through the inner exceptions if there are any.
	if (ex.InnerException != null)
	{
		sb.AppendLine("InnerException: ");
		BuildExceptionDetail(ex.InnerException, sb);
	}
	return sb;
}
