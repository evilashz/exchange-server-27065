using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008F RID: 143
	[DataContract]
	internal sealed class MoveActionResult : ReplayActionResult
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0000AD13 File Offset: 0x00008F13
		public MoveActionResult()
		{
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000AD1B File Offset: 0x00008F1B
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000AD23 File Offset: 0x00008F23
		[DataMember]
		public byte[] ItemId { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000AD2C File Offset: 0x00008F2C
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000AD34 File Offset: 0x00008F34
		[DataMember]
		public byte[] PreviousItemId { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000AD3D File Offset: 0x00008F3D
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000AD45 File Offset: 0x00008F45
		[DataMember]
		public bool MoveAsDelete { get; set; }

		// Token: 0x060005FD RID: 1533 RVA: 0x0000AD4E File Offset: 0x00008F4E
		public MoveActionResult(byte[] itemId, byte[] prevItemId, bool moveAsDelete = false)
		{
			this.ItemId = itemId;
			this.PreviousItemId = prevItemId;
			this.MoveAsDelete = moveAsDelete;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000AD6C File Offset: 0x00008F6C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				", ItemId: ",
				TraceUtils.DumpEntryId(this.ItemId),
				", PreviousItemId: ",
				TraceUtils.DumpEntryId(this.PreviousItemId)
			});
		}
	}
}
