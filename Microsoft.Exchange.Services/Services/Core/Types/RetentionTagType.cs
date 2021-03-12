using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000633 RID: 1587
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RetentionTagType
	{
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000B7152 File Offset: 0x000B5352
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000B715A File Offset: 0x000B535A
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public bool IsExplicit { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000B7163 File Offset: 0x000B5363
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x000B716B File Offset: 0x000B536B
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 2)]
		[XmlText]
		public string Value { get; set; }
	}
}
