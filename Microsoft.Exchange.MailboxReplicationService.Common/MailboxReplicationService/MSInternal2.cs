using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009F RID: 159
	[DataContract]
	internal sealed class MSInternal2
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000B11E File Offset: 0x0000931E
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0000B126 File Offset: 0x00009326
		[DataMember]
		public long Field1 { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000B12F File Offset: 0x0000932F
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000B137 File Offset: 0x00009337
		[DataMember]
		public byte Field2 { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000B140 File Offset: 0x00009340
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000B148 File Offset: 0x00009348
		[DataMember]
		public MSInternal3[] Field3 { get; set; }
	}
}
