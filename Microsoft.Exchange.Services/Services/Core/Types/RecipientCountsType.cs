using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200061A RID: 1562
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RecipientCountsType")]
	[Serializable]
	public class RecipientCountsType
	{
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000B6D2A File Offset: 0x000B4F2A
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x000B6D32 File Offset: 0x000B4F32
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public int ToRecipientsCount { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000B6D3B File Offset: 0x000B4F3B
		// (set) Token: 0x06003112 RID: 12562 RVA: 0x000B6D43 File Offset: 0x000B4F43
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int CcRecipientsCount { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000B6D4C File Offset: 0x000B4F4C
		// (set) Token: 0x06003114 RID: 12564 RVA: 0x000B6D54 File Offset: 0x000B4F54
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public int BccRecipientsCount { get; set; }
	}
}
