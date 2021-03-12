using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C5 RID: 453
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DictionaryURIType
	{
		// Token: 0x04000A77 RID: 2679
		[XmlEnum("item:InternetMessageHeader")]
		itemInternetMessageHeader,
		// Token: 0x04000A78 RID: 2680
		[XmlEnum("contacts:ImAddress")]
		contactsImAddress,
		// Token: 0x04000A79 RID: 2681
		[XmlEnum("contacts:PhysicalAddress:Street")]
		contactsPhysicalAddressStreet,
		// Token: 0x04000A7A RID: 2682
		[XmlEnum("contacts:PhysicalAddress:City")]
		contactsPhysicalAddressCity,
		// Token: 0x04000A7B RID: 2683
		[XmlEnum("contacts:PhysicalAddress:State")]
		contactsPhysicalAddressState,
		// Token: 0x04000A7C RID: 2684
		[XmlEnum("contacts:PhysicalAddress:CountryOrRegion")]
		contactsPhysicalAddressCountryOrRegion,
		// Token: 0x04000A7D RID: 2685
		[XmlEnum("contacts:PhysicalAddress:PostalCode")]
		contactsPhysicalAddressPostalCode,
		// Token: 0x04000A7E RID: 2686
		[XmlEnum("contacts:PhoneNumber")]
		contactsPhoneNumber,
		// Token: 0x04000A7F RID: 2687
		[XmlEnum("contacts:EmailAddress")]
		contactsEmailAddress,
		// Token: 0x04000A80 RID: 2688
		[XmlEnum("distributionlist:Members:Member")]
		distributionlistMembersMember
	}
}
