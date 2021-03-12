using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000872 RID: 2162
	[DataContract(Name = "SearchRefinerItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SearchRefinerItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchRefinerItem
	{
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000D8197 File Offset: 0x000D6397
		// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000D819F File Offset: 0x000D639F
		[XmlElement("Name")]
		[DataMember(Name = "Name", IsRequired = true)]
		public string Name { get; set; }

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000D81A8 File Offset: 0x000D63A8
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000D81B0 File Offset: 0x000D63B0
		[XmlElement("Value")]
		[DataMember(Name = "Value", IsRequired = true)]
		public string Value { get; set; }

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000D81B9 File Offset: 0x000D63B9
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000D81C1 File Offset: 0x000D63C1
		[XmlElement("Count")]
		[DataMember(Name = "Count", IsRequired = true)]
		public long Count { get; set; }

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000D81CA File Offset: 0x000D63CA
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000D81D2 File Offset: 0x000D63D2
		[XmlElement("Token")]
		[DataMember(Name = "Token", IsRequired = true)]
		public string Token { get; set; }
	}
}
