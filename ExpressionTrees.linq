<Query Kind="Program" />


//Notes to myself: A plain delegate can be Invok()ed but an Expression<TDelegate> has to be Compile()'d, then Invoke()d.

void Main()
{
	Expression<Func<string, string, bool>> expression = (s1, s2) =>  true;
	
	Map(expression);
	
	Expression<Func<int, int, bool>> myExpression = (first, second) => (first + second > 100);

	bool myExpressionResult = myExpression.Compile().Invoke(3, 5);
	Console.WriteLine($"myExpression invocation = {myExpressionResult}");

	Console.WriteLine("Now executing action lambda...");
	ExecuteActionLambda();

	Console.WriteLine($"BuildAndInvokeBasicExpression invocation = {BuildAndInvokeBasicExpression(10, 21)}");
	
	Ignore(x => "hello");
		
}

// Define other methods and classes here

public static void Ignore(Expression<Func<string,string>> expression)
{
	var value = expression.Body;

}

public static void Map(Expression<Func<string, string, bool>> expression)
{
	var parameters = expression.Parameters;
}

public static int BuildAndInvokeBasicExpression(int firstArg, int secondArg)
{
	Expression firstArgExpression = Expression.Constant(firstArg);
	Expression secondArgExpression = Expression.Constant(secondArg);
	Expression add = Expression.Add(firstArgExpression, secondArgExpression);
	Func<int> compiled = Expression.Lambda<Func<int>>(add).Compile();
	int result = compiled.Invoke();
	return result;
}

public static void ExecuteActionLambda()
{
	Action<IEnumerable<string>, int, bool> myAction = (s, i, f) => 
	{ 
		foreach (string item in s) Console.WriteLine(item); 
	};
	myAction.Invoke(new List<string> { "hello", "world" }, 3, false);
}