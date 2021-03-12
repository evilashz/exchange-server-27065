using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067C RID: 1660
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TimeZoneOffsetsType
	{
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000B8322 File Offset: 0x000B6522
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x000B832A File Offset: 0x000B652A
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string TimeZoneId { get; set; }

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000B8333 File Offset: 0x000B6533
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x000B833B File Offset: 0x000B653B
		[XmlArrayItem(ElementName = "OffsetRange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlArray(ElementName = "OffsetRanges")]
		public TimeZoneRangeType[] OffsetRanges { get; set; }
	}
}
