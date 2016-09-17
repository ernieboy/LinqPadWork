<Query Kind="Program" />

void Main()
{
	Func<int> myFunc = () => 10;
	Expression<Func<int, int, bool>> myExpression = (first, second) => (first + second > 100);

	bool myExpressionResult = myExpression.Compile().Invoke(3, 5);
	Console.WriteLine($"myExpression invocation = {myExpressionResult}");


	Action<IEnumerable<string>, int, bool> myAction = (s, i, f) => { foreach (string item in s) Console.WriteLine(item);};
	myAction.Invoke(new List<string> {"hello", "world"}, 3,false); 

	Console.WriteLine($"BuildAndInvokeBasicExpression invocation = {BuildAndInvokeBasicExpression(10, 21)}");
}

// Define other methods and classes here

public static void DoSomething(Expression<Func<int, bool>> expression)
{
	
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