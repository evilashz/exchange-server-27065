using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200074C RID: 1868
	[XmlType(TypeName = "DictionaryURIType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DictionaryUriEnum
	{
		// Token: 0x04001F1A RID: 7962
		[XmlEnum("item:InternetMessageHeader")]
		InternetMessageHeader,
		// Token: 0x04001F1B RID: 7963
		[XmlEnum("contacts:ImAddress")]
		ImAddress,
		// Token: 0x04001F1C RID: 7964
		[XmlEnum("contacts:PhysicalAddress")]
		PhysicalAddress,
		// Token: 0x04001F1D RID: 7965
		[XmlEnum("contacts:PhoneNumber")]
		PhoneNumber,
		// Token: 0x04001F1E RID: 7966
		[XmlEnum("contacts:EmailAddress")]
		EmailAddress,
		// Token: 0x04001F1F RID: 7967
		[XmlEnum("contacts:PhysicalAddress:Street")]
		PhysicalAddressStreet,
		// Token: 0x04001F20 RID: 7968
		[XmlEnum("contacts:PhysicalAddress:City")]
		PhysicalAddressCity,
		// Token: 0x04001F21 RID: 7969
		[XmlEnum("contacts:PhysicalAddress:State")]
		PhysicalAddressState,
		// Token: 0x04001F22 RID: 7970
		[XmlEnum("contacts:PhysicalAddress:CountryOrRegion")]
		PhysicalAddressCountryOrRegion,
		// Token: 0x04001F23 RID: 7971
		[XmlEnum("contacts:PhysicalAddress:PostalCode")]
		PhysicalAddressPostalCode,
		// Token: 0x04001F24 RID: 7972
		[XmlEnum("distributionlist:Members:Member")]
		DistributionListMembersMember
	}
}
