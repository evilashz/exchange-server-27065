using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E4 RID: 1764
	[XmlType(TypeName = "BodyContentAttributedValue", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "BodyContentAttributedValue")]
	[Serializable]
	public class BodyContentAttributedValue
	{
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000C1C97 File Offset: 0x000BFE97
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000C1C9F File Offset: 0x000BFE9F
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public BodyContentType Value { get; set; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000C1CA8 File Offset: 0x000BFEA8
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000C1CB0 File Offset: 0x000BFEB0
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = true, Order = 2)]
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] Attributions { get; set; }

		// Token: 0x06003608 RID: 13832 RVA: 0x000C1CB9 File Offset: 0x000BFEB9
		public BodyContentAttributedValue()
		{
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000C1CC1 File Offset: 0x000BFEC1
		public BodyContentAttributedValue(BodyContentType value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
