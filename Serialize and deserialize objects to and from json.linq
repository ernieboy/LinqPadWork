<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var obj = new LogonScreenMessageModel(true,"Heading", "Body of message");
	string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
	File.WriteAllText(@"C:\data\tmp\logon.json",result);
	
	string objectTextFromFile = File.ReadAllText(@"C:\data\tmp\logon.json");
	
	var fromFile = JsonConvert.DeserializeObject<LogonScreenMessageModel>(objectTextFromFile);
	string body = fromFile.Message;
}

// Define other methods and classes here

public class LogonScreenMessageModel
{
	public LogonScreenMessageModel()
	{
	}

	public LogonScreenMessageModel(bool display, string header, string message)
	{
		Display = display;
		Header = header;
		Message = message;
	}

	public bool Display { get; set; }
	public string Header { get; set; }
	public string Message { get; set; }
}
