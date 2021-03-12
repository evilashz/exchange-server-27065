using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E4 RID: 228
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DictionaryURIType
	{
		// Token: 0x04000625 RID: 1573
		[XmlEnum("item:InternetMessageHeader")]
		itemInternetMessageHeader,
		// Token: 0x04000626 RID: 1574
		[XmlEnum("contacts:ImAddress")]
		contactsImAddress,
		// Token: 0x04000627 RID: 1575
		[XmlEnum("contacts:PhysicalAddress:Street")]
		contactsPhysicalAddressStreet,
		// Token: 0x04000628 RID: 1576
		[XmlEnum("contacts:PhysicalAddress:City")]
		contactsPhysicalAddressCity,
		// Token: 0x04000629 RID: 1577
		[XmlEnum("contacts:PhysicalAddress:State")]
		contactsPhysicalAddressState,
		// Token: 0x0400062A RID: 1578
		[XmlEnum("contacts:PhysicalAddress:CountryOrRegion")]
		contactsPhysicalAddressCountryOrRegion,
		// Token: 0x0400062B RID: 1579
		[XmlEnum("contacts:PhysicalAddress:PostalCode")]
		contactsPhysicalAddressPostalCode,
		// Token: 0x0400062C RID: 1580
		[XmlEnum("contacts:PhoneNumber")]
		contactsPhoneNumber,
		// Token: 0x0400062D RID: 1581
		[XmlEnum("contacts:EmailAddress")]
		contactsEmailAddress,
		// Token: 0x0400062E RID: 1582
		[XmlEnum("distributionlist:Members:Member")]
		distributionlistMembersMember
	}
}
