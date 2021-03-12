using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008B RID: 139
	[DataContract]
	internal sealed class FlagClearAction : ReplayAction
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0000AC17 File Offset: 0x00008E17
		public FlagClearAction()
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000AC1F File Offset: 0x00008E1F
		public FlagClearAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000AC36 File Offset: 0x00008E36
		internal override ActionId Id
		{
			get
			{
				return ActionId.FlagClear;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000AC39 File Offset: 0x00008E39
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}
	}
}
