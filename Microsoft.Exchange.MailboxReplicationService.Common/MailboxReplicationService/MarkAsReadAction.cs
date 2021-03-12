using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008D RID: 141
	[DataContract]
	internal sealed class MarkAsReadAction : ReplayAction
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0000AC95 File Offset: 0x00008E95
		public MarkAsReadAction()
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000AC9D File Offset: 0x00008E9D
		public MarkAsReadAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		internal override ActionId Id
		{
			get
			{
				return ActionId.MarkAsRead;
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000ACB7 File Offset: 0x00008EB7
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}
	}
}
