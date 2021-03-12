using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008C RID: 140
	[DataContract]
	internal sealed class FlagCompleteAction : ReplayAction
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0000AC56 File Offset: 0x00008E56
		public FlagCompleteAction()
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000AC5E File Offset: 0x00008E5E
		public FlagCompleteAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000AC75 File Offset: 0x00008E75
		internal override ActionId Id
		{
			get
			{
				return ActionId.FlagComplete;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000AC78 File Offset: 0x00008E78
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}
	}
}
