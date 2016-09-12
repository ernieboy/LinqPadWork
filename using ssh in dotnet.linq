<Query Kind="Program">
  <NuGetReference>SSH.NET</NuGetReference>
  <Namespace>Renci.SshNet</Namespace>
</Query>

void Main()
{
	// Setup Credentials and Server Information
	ConnectionInfo ConnNfo = new ConnectionInfo("172.17.43.233", 22, "csd.sshuser",
		new AuthenticationMethod[]{

			// Pasword based Authentication
			new PasswordAuthenticationMethod("csd.sshuser","Gilwell2020!")			
		}
	);

	// Execute a (SHELL) Command - prepare upload directory
	using (var sshclient = new SshClient(ConnNfo))
	{
		sshclient.Connect();
		using (var cmd = sshclient.CreateCommand("mkdir -p /home/ernest.fakudze/uploadtest && chmod +rw /home/ernest.fakudze/uploadtest"))
		{
			cmd.Execute();
			Console.WriteLine("Command>" + cmd.CommandText);
			Console.WriteLine("Return Value = {0}", cmd.ExitStatus);
		}
		sshclient.Disconnect();
	}

}

// Define other methods and classes here
