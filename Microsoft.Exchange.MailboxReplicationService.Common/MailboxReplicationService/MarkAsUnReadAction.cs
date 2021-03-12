using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008E RID: 142
	[DataContract]
	internal sealed class MarkAsUnReadAction : ReplayAction
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public MarkAsUnReadAction()
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public MarkAsUnReadAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000ACF3 File Offset: 0x00008EF3
		internal override ActionId Id
		{
			get
			{
				return ActionId.MarkAsUnRead;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000ACF6 File Offset: 0x00008EF6
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}
	}
}
