using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007E RID: 126
	[DataContract]
	internal sealed class SortOrderData
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000A5A7 File Offset: 0x000087A7
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000A5AF File Offset: 0x000087AF
		[DataMember]
		public SortOrderMember[] Members { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000A5B8 File Offset: 0x000087B8
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000A5C0 File Offset: 0x000087C0
		[DataMember(EmitDefaultValue = false)]
		public int LCID { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000A5C9 File Offset: 0x000087C9
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000A5D1 File Offset: 0x000087D1
		[DataMember(EmitDefaultValue = false)]
		public bool FAI { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000A5DA File Offset: 0x000087DA
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000A5E2 File Offset: 0x000087E2
		[DataMember(EmitDefaultValue = false)]
		public bool Conversation { get; set; }

		// Token: 0x0600059B RID: 1435 RVA: 0x0000A5EC File Offset: 0x000087EC
		public override string ToString()
		{
			return string.Format("SortOrder: LCID=0x{0:X}, FAI={1}, Conversation={2}, {3}", new object[]
			{
				this.LCID,
				this.FAI,
				this.Conversation,
				CommonUtils.ConcatEntries<SortOrderMember>(this.Members, null)
			});
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000A644 File Offset: 0x00008844
		internal void Enumerate(SortOrderData.EnumSortOrderMemberDelegate del)
		{
			foreach (SortOrderMember som in this.Members)
			{
				del(som);
			}
		}

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x0600059E RID: 1438
		internal delegate void EnumSortOrderMemberDelegate(SortOrderMember som);
	}
}
