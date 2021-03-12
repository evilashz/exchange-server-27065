using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005FD RID: 1533
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class OccurrenceInfoType
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000B3EFB File Offset: 0x000B20FB
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x000B3F03 File Offset: 0x000B2103
		[XmlElement]
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 1)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000B3F0C File Offset: 0x000B210C
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x000B3F14 File Offset: 0x000B2114
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 2)]
		[XmlElement]
		[DateTimeString]
		public string Start { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000B3F1D File Offset: 0x000B211D
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000B3F25 File Offset: 0x000B2125
		[XmlElement]
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 3)]
		[DateTimeString]
		public string End { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000B3F2E File Offset: 0x000B212E
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000B3F36 File Offset: 0x000B2136
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 4)]
		[DateTimeString]
		[XmlElement]
		public string OriginalStart { get; set; }
	}
}
