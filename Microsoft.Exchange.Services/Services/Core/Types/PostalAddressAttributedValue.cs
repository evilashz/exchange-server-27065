using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000849 RID: 2121
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PostalAddressAttributedValue")]
	[XmlType(TypeName = "PostalAddressAttributedValue", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PostalAddressAttributedValue
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000D73A0 File Offset: 0x000D55A0
		// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000D73A8 File Offset: 0x000D55A8
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public PostalAddress Value { get; set; }

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000D73B1 File Offset: 0x000D55B1
		// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000D73B9 File Offset: 0x000D55B9
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(IsRequired = true, Order = 2)]
		public string[] Attributions { get; set; }

		// Token: 0x06003D47 RID: 15687 RVA: 0x000D73C2 File Offset: 0x000D55C2
		public PostalAddressAttributedValue()
		{
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000D73CA File Offset: 0x000D55CA
		public PostalAddressAttributedValue(PostalAddress value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
