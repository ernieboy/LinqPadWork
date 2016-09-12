<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{

	List<MethodInvoker> list = new List<MethodInvoker>();
	
	for (int index = 0; index < 5; index++)
	{
		int count = index;
		int counter = index * 10;
		list.Add(delegate
		{
			Console.WriteLine(counter);
			counter++;
			Console.WriteLine("Index = {0} and Captured counter = {1}.",count, counter);
		});
	}
	foreach (MethodInvoker t in list)
	{
		t();
	}
	/*list[0]();
	list[0]();
	list[0]();
	list[1]();
	*/

}


// Define other methods and classes here
