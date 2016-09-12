<Query Kind="Program" />

void Main()
{
	string filePath = @"C:\Users\ernest.fakudze\Desktop\temp\Unidata\live\tsa-crb-application-TEST.xml";

	using (XmlReader reader = XmlReader.Create(filePath))
	{
		var crbApplications = reader.CrbApplications().ToList();
	}


}



public static class XmlReaderExtensions
{
	public static IEnumerable<CrbApplication> CrbApplications(this XmlReader source)
	{
		while (source.Read())
		{
			if (source.NodeType == XmlNodeType.Element &&
				source.Name == "TSA.CRB.APPLICATION")
			{

				//Console.WriteLine(source.ReadOuterXml());
				string xml = "<root>" + source.ReadOuterXml() + "</root>";
				//Console.WriteLine(xml);

				var app = new CrbApplication
				{
					CrbApplicationCode = GetNodesAttributeValue(xml, source.Name, "CODE"),
					MemberKey = GetNodesAttributeValue(xml, source.Name, "MEMBER")
				};

				app.PreviousAddressesMultiValueCollection = GetMultiValueNodesDataAsListOfStrings(xml, "PREVIOUS.ADDRESS_MV", "PREVIOUS.ADDRESS");
				app.PreviousAddressFromMultiValuesCollection = GetMultiValueNodesDataAsListOfStrings(xml, "PREVIOUS.ADD.FROM_MV", "PREVIOUS.ADD.FROM");
				app.PreviousAddressTosMultiValueCollection = GetMultiValueNodesDataAsListOfStrings(xml, "PREVIOUS.ADD.TO_MV", "PREVIOUS.ADD.TO");

				app.ContactNumberMultiValuesCollection = GetMultiValueNodesDataAsListOfStrings(xml, "CONTACT.NUMBER_MV", "CONTACT.NUMBER");
                app.ContactNumberTypeMultiValuesCollection = GetMultiValueNodesDataAsListOfStrings(xml, "CONTACT.NUMBER.TYPE_MV", "CONTACT.NUMBER.TYPE");
				
				yield return app;
			}
		}
	}

	public static string GetNodesAttributeValue(
	this string xml, string elementName, string attributeName)
	{
		string attributeValue = string.Empty;
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xml);
		attributeValue = doc.SelectSingleNode("/" + elementName + "/@" + attributeName).Value;

		return attributeValue;
	}

	public static ICollection<string> GetMultiValueNodesDataAsListOfStrings(
	this string xml, string elementName, string attributeName)
	{
		ICollection<string> list = new List<string>();
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xml);

		foreach (XmlNode node in doc.GetElementsByTagName(elementName))
		{
			list.Add(node.Attributes[attributeName].Value);
		}

		return list;
	}
}

public class CrbApplication : BaseModel
{
	private ICollection<CrbApplicationPreviousAddress> _crbApplicationPreviousAddresses;
	private ICollection<CrbApplicationPreviousContactNumber> _crbApplicationPreviousContactNumbers;
	private ICollection<string> _previousAddressFromsMultiValueCollection;
	private ICollection<string> _previousAddressTosMultiValueCollection;
	private ICollection<string> _previousAddressesMultiValueCollection;
	private ICollection<string> _contactNumberMultiValuesCollection;
	private ICollection<string> _contactNumberTypeMultiValuesCollection;

	public CrbApplication()
	{

	}

	public CrbApplication(string memberKey, string applicationNumber)
	{
		MemberKey = memberKey;
		ApplicationNumber = applicationNumber;
	}

	public int MembershipNumber { get; set; }
	public string MemberKey { get; set; }
	public string CrbApplicationCode { get; set; }
	public string ApplicationNumber { get; set; }
	public string ControlStop { get; set; }
	public string Surname { get; set; }
	public string DisclosureNumber { get; set; }
	public DateTime? AppStartDate { get; set; }
	public DateTime? SentToCrbDate { get; set; }
	public DateTime? DiscClear { get; set; }
	public DateTime? DiscIssuedDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string WDiscClear { get; set; }
	public DateTime? BeingProcessedDate { get; set; }
	public string Position { get; set; }
	public string PositionFull { get; set; }
	public string Gender { get; set; }
	public string Title { get; set; }
	public string Country { get; set; }
	public string Region { get; set; }
	public string County { get; set; }
	public string District { get; set; }
	public string GroupCode { get; set; }
	public DateTime? WReturnedCountyDate { get; set; }
	public string WSentToCrbDate { get; set; }
	public string PostCode { get; set; }
	public string WAdversePending { get; set; }
	public string BirthTown { get; set; }
	public string BirthCounty { get; set; }
	public string BirthCountry { get; set; }
	public string BirthNationality { get; set; }
	public char UnspentConvictions { get; set; }
	public char Declaration { get; set; }
	public string Language { get; set; }
	public string DisclosureType { get; set; }
	public char CurrentAddressChecked { get; set; }
	public char Volunteer { get; set; }
	public char SendToMember { get; set; }
	public string ReturnMailName { get; set; }
	public string ReturnPostCode { get; set; }
	public string ReturnEmail { get; set; }
	public string Nda { get; set; }
	public string DiscPoliceList { get; set; }
	public string DiscEduList { get; set; }
	public string DiscChildList { get; set; }
	public string DiscAdultList { get; set; }
	public string DiscOtherInfo { get; set; }
	public string DiscStatus { get; set; }
	public string CsigRegBody { get; set; }
	public string CsigFullName { get; set; }
	public string CsigRole { get; set; }
	public string CsigAddressPipe { get; set; }
	public string CsigPosition { get; set; }
	public string CsigOrgName { get; set; }
	public string ReturnAddressLine1 { get; set; }
	public string ReturnAddressLine2 { get; set; }
	public string ReturnAddressLine3 { get; set; }
	public string ReturnAddressLine4 { get; set; }
	public string ReturnAddressLine5 { get; set; }
	public string ReturnAddressPostCode { get; set; }
	public string ReturnAddressEmail { get; set; }

	public ICollection<string> ContactNumberMultiValuesCollection
	{
		get
		{
			return _contactNumberMultiValuesCollection ??
				   (_contactNumberMultiValuesCollection = new List<string>());
		}
		set { _contactNumberMultiValuesCollection = value; }
	}

	public ICollection<string> ContactNumberTypeMultiValuesCollection
	{
		get
		{
			return _contactNumberTypeMultiValuesCollection ??
				   (_contactNumberTypeMultiValuesCollection = new List<string>());
		}
		set { _contactNumberTypeMultiValuesCollection = value; }
	}

	public ICollection<string> PreviousAddressFromMultiValuesCollection
	{
		get
		{
			return _previousAddressFromsMultiValueCollection ??
				   (_previousAddressFromsMultiValueCollection = new List<string>());
		}
		set { _previousAddressFromsMultiValueCollection = value; }
	}

	public ICollection<string> PreviousAddressTosMultiValueCollection
	{
		get
		{
			return _previousAddressTosMultiValueCollection ??
				   (_previousAddressTosMultiValueCollection = new List<string>());
		}
		set { _previousAddressTosMultiValueCollection = value; }
	}

	public ICollection<string> PreviousAddressesMultiValueCollection
	{
		get
		{
			return _previousAddressesMultiValueCollection ??
				   (_previousAddressesMultiValueCollection = new List<string>());
		}
		set { _previousAddressesMultiValueCollection = value; }
	}

	public ICollection<CrbApplicationPreviousAddress> CrbApplicationPreviousAddresses
	{
		get
		{
			return _crbApplicationPreviousAddresses ??
				   (_crbApplicationPreviousAddresses = new List<CrbApplicationPreviousAddress>());
		}
		set { _crbApplicationPreviousAddresses = value; }
	}

	public ICollection<CrbApplicationPreviousContactNumber> CrbApplicationPreviousContactNumbers
	{
		get
		{
			return _crbApplicationPreviousContactNumbers ??
				   (_crbApplicationPreviousContactNumbers = new List<CrbApplicationPreviousContactNumber>());
		}
		set { _crbApplicationPreviousContactNumbers = value; }
	}


}




public class CrbApplicationPreviousAddress : BaseModel
{
	public CrbApplicationPreviousAddress()
	{

	}

	public CrbApplicationPreviousAddress(int crbApplicationId)
	{
		CrbApplicationId = crbApplicationId;
	}

	public int CrbApplicationId { get; set; }
	public string FromDate { get; set; }
	public string ToDate { get; set; }
	public string AddressLine1 { get; set; }
	public string AddressLine2 { get; set; }
	public string AddressLine3 { get; set; }
	public string AddressLine4 { get; set; }
	public string AddressLine5 { get; set; }
	public string AddressLine6 { get; set; }
	public string AddressLine7 { get; set; }
}

public class CrbApplicationPreviousContactNumber : BaseModel
{
	public CrbApplicationPreviousContactNumber()
	{

	}

	public CrbApplicationPreviousContactNumber(
		int crbApplicationId, string number, string contactNumberType)
	{
		CrbApplicationId = crbApplicationId;
		Number = number;
		ContactNumberType = contactNumberType;
	}

	public int CrbApplicationId { get; set; }
	public string Number { get; set; }
	public string ContactNumberType { get; set; }
}

public abstract class BaseModel
{
	public int Id { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime? DateLastModified { get; set; }
}