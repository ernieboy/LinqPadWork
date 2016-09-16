<Query Kind="Program" />

void Main()
{
	Console.WriteLine("Mapping from DTO to entity...");
	var personDto = new PersonDto {Age = 27, FirstName = "Ernie", LastName = "Fakudze"};
	var person = new Person();
	Person p = MapFromAllMatchingProps<PersonDto, Person>(personDto, ref person);

	Console.WriteLine($"{p.FirstName} {p.LastName} {p.Age}");

	Console.WriteLine("\r\nMapping from entity to DTO...");
	var personSource = new Person {Age = 29, FirstName = "Andy" , LastName = "Davidson"};
	var dto = new PersonDto();
	var mappedPersonDto = MapFromAllMatchingProps<Person, PersonDto>(personSource,ref dto);
	Console.WriteLine($"{dto.FirstName} {dto.LastName} {dto.Age}");
	
}

// Define other methods and classes here

public static TTarget MapFromAllMatchingProps<TSource, TTarget>(TSource source, ref TTarget target)
where TSource : new() where TTarget : new()
{
	if(source == null) return target;
	
	//Get all properties from the source type
	var sourceTypeProps = source.GetType().GetProperties();
	//Get all properties from the target type
	var targetTypeProps = target.GetType().GetProperties();

	//Loop through all the properties of the source type and populate the target type for all matching properties.
	foreach (var prop in sourceTypeProps)
	{
		var propInTargetType = targetTypeProps.SingleOrDefault(p => p.Name == prop.Name);
		if(propInTargetType == null) continue;
		object propValue = prop.GetValue(source);
		var propInResultType = targetTypeProps.SingleOrDefault(p => p.Name == prop.Name);
		propInResultType.SetValue(target,propValue);		
	}

	return target;
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
