<Query Kind="Program" />

void Main()
{
	Console.WriteLine("Mapping from DTO to entity...");
	var personDto = new PersonDto {Age = 27, FirstName = "Ernie", LastName = "Fakudze"};

	Person p = MapTypes<PersonDto, Person>(
	personDto, () => new Dictionary<string, object>() {
	{"test", "hello"}
	
	});

	Console.WriteLine($"{p.FirstName} {p.LastName} {p.Age}");

	Console.WriteLine("\r\nMapping from entity to DTO...");
	var personSource = new Person {Age = 290, FirstName = "Andy" , LastName = "Davidson"};
	PersonDto mappedPersonDto = MapTypes<Person, PersonDto>(personSource, null);
	Console.WriteLine($"{mappedPersonDto.FirstName} {mappedPersonDto.LastName} {mappedPersonDto.Age}");
	
}

// Define other methods and classes here

public static TDestination MapTypes<TSource, TDestination>(TSource source, 
Func<IDictionary<string,object>> mapProps)
where TSource : new() where TDestination : new()
{
	var destination = new TDestination();
	if(source == null) return destination;
	
	//Get all properties from the source type
	var sourceTypeProperties = source.GetType().GetProperties().Where(p => p.CanWrite);
	//Get all properties from the destination type
	var destinationTypeProperties = destination.GetType().GetProperties().Where(p => p.CanWrite);

	//Loop through all the properties of the source type and populate the target type for all matching properties.
	foreach (var prop in sourceTypeProperties)
	{
		PropertyInfo propertyInDestinationType = destinationTypeProperties.SingleOrDefault(p => p.Name == prop.Name);
		if(propertyInDestinationType == null) continue;
		object propValue = prop.GetValue(source);
		propertyInDestinationType.SetValue(destination,propValue);		
	}
	return destination;
}

public class Person
{
	public int Age { get; set; }
	public string FirstName { get; set; }	
	public string LastName { get; set; }
}

public class PersonDto
{
	public int Age { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
}
