using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000895 RID: 2197
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "StringArrayAttributedValue")]
	[XmlType(TypeName = "StringArrayAttributedValueType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class StringArrayAttributedValue
	{
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000D9916 File Offset: 0x000D7B16
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x000D991E File Offset: 0x000D7B1E
		[XmlArray(ElementName = "Values", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(IsRequired = true, Order = 1)]
		[XmlArrayItem("Value", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] Values { get; set; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000D9927 File Offset: 0x000D7B27
		// (set) Token: 0x06003ECD RID: 16077 RVA: 0x000D992F File Offset: 0x000D7B2F
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(IsRequired = true, Order = 2)]
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] Attributions { get; set; }

		// Token: 0x06003ECE RID: 16078 RVA: 0x000D9938 File Offset: 0x000D7B38
		public StringArrayAttributedValue()
		{
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000D9940 File Offset: 0x000D7B40
		public StringArrayAttributedValue(string[] value, string[] attributions)
		{
			this.Values = value;
			this.Attributions = attributions;
		}
	}
}
