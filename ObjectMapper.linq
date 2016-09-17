<Query Kind="Program" />

void Main()
{
	Console.WriteLine("Mapping from DTO to entity...");
	var personDto = new PersonDto { TheAge = 27, FirstName = "Ernie", LastName = "Fakudze" };
	personDto.TheFullName = $"{personDto.FirstName} {personDto.LastName}"; 

	Console.WriteLine("\r\nMapping from entity to DTO...");
	var personSource = new Person { Age = 290, FirstName = "Andy", LastName = "Davidson" };
	personSource.FullName = "Mr And Davidson";
	PersonDto mappedPersonDto = new SimpleObjectMapper<Person,PersonDto>()
	.MapProperty(s => s.Age, d => d.TheAge)
	.MapProperty(s => s.FullName, d => d.TheFullName)
	.Map(personSource);
	Console.WriteLine($"{mappedPersonDto.FirstName} {mappedPersonDto.LastName} {mappedPersonDto.TheAge}");

	Person mappedPerson = new SimpleObjectMapper<PersonDto, Person>()
	.MapProperty(source => source.TheAge, destination => destination.Age)
	.MapProperty(source => source.TheFullName, destination => destination.FullName)
	.Map(personDto);
	Console.WriteLine($"mappedPerson age = {mappedPerson.Age}");

}

// Define other methods and classes here


public class Person
{
	public int Age { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string FullName { get; set; }
}

public class PersonDto
{
	public int TheAge { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string TheFullName { get; set; }
}


public class SimpleObjectMapper<TSource, TDestination> where TSource : new() where TDestination : new()
{
	private readonly IDictionary<string, string> propertiesMap = new Dictionary<string, string>();

	public SimpleObjectMapper<TSource, TDestination> MapProperty(
	Expression<Func<TSource, object>> source, Expression<Func<TDestination, object>> destination)
	{
		string sourcePropertyName = GetSourceMemberInfo(source);
		string destinationPropertyName = GetDestinationMemberInfo(destination);
		propertiesMap.Add(sourcePropertyName, destinationPropertyName);

		return this; ;
	}

	/// Code borrowed from https://github.com/TinyMapper/TinyMapper/blob/master/Source/TinyMapper/Bindings/BindingConfigOf.cs
	///
	private static string GetSourceMemberInfo(Expression<Func<TSource, object>> expression)
	{
		var member = expression.Body as MemberExpression;
		if (member == null)
		{
			var unaryExpression = expression.Body as UnaryExpression;
			if (unaryExpression != null)
			{
				member = unaryExpression.Operand as MemberExpression;
			}

			if (member == null)
			{
				throw new ArgumentException("Expression is not a MemberExpression", "expression");
			}
		}
		return member.Member.Name;
	}

	/// Code borrowed from https://github.com/TinyMapper/TinyMapper/blob/master/Source/TinyMapper/Bindings/BindingConfigOf.cs
	///
	private static string GetDestinationMemberInfo(Expression<Func<TDestination, object>> expression)
	{
		var member = expression.Body as MemberExpression;
		if (member == null)
		{
			var unaryExpression = expression.Body as UnaryExpression;
			if (unaryExpression != null)
			{
				member = unaryExpression.Operand as MemberExpression;
			}

			if (member == null)
			{
				throw new ArgumentException("Expression is not a MemberExpression", "expression");
			}
		}
		return member.Member.Name;
	}



	public TDestination Map(TSource source)
	{
		var destination = new TDestination();
		if (source == null) return destination;

		//Get all properties from the source type
		var sourceTypeProperties = source.GetType().GetProperties().Where(p => p.CanWrite);
		//Get all properties from the destination type
		var destinationTypeProperties = destination.GetType().GetProperties().Where(p => p.CanWrite);

		//Loop through all the properties of the source type and populate the target type for all matching properties.
		foreach (var prop in sourceTypeProperties)
		{
			PropertyInfo propertyInDestinationType = destinationTypeProperties.SingleOrDefault(p => p.Name == prop.Name);
			if (propertyInDestinationType == null) continue;
			object propValue = prop.GetValue(source);
			propertyInDestinationType.SetValue(destination, propValue);
		}

		//Now map extra fields which were specified manually using the MapProperty method
		foreach (KeyValuePair<string, string> entry in propertiesMap)
		{
			PropertyInfo propertyInSourceType = sourceTypeProperties.SingleOrDefault(p => p.Name == entry.Key);
			object sourceObjectPropertyValue = propertyInSourceType.GetValue(source);
			PropertyInfo propertyInDestinationType = destinationTypeProperties.SingleOrDefault(p => p.Name == entry.Value);

			propertyInDestinationType.SetValue(destination, sourceObjectPropertyValue);
		}

		return destination;
	}

}


