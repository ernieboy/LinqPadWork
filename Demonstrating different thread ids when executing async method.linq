<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	int threadId = Environment.CurrentManagedThreadId;
	
	Console.WriteLine("ThreadId before await is {0}", threadId);
	
	var result = await GetDataFromUrl("http://www.ernieboy.com");
	Console.WriteLine(result);
	Console.WriteLine("ThreadId after await is {0}", Environment.CurrentManagedThreadId);
}

public async static Task<string> GetDataFromUrl(string url)
{
	using (var client = new HttpClient())
	{
		var content = await client.GetStringAsync(url);
		return content;
	}

}

// Define other methods and classes here
