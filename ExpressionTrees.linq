<Query Kind="Program" />

void Main()
{
	Func<int> myFunc = () => 10;
	Expression<Func<string, int, bool>> myExpression = (s, i) => false;

	Expression<Action<string, int, bool>> myAction = (s,i,f) => Console.WriteLine(s);
	var compile = myExpression.Compile();
	//Console.WriteLine(DoSomething(myExpression.Compile() );
}

// Define other methods and classes here

public static void DoSomething(Expression<Func<int, bool>> expression)
{
	
}
