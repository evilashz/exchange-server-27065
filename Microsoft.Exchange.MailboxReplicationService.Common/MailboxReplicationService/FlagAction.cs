using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008A RID: 138
	[DataContract]
	internal sealed class FlagAction : ReplayAction
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public FlagAction()
		{
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public FlagAction(byte[] itemId, byte[] folderId, string watermark) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		internal override ActionId Id
		{
			get
			{
				return ActionId.Flag;
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000ABFA File Offset: 0x00008DFA
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}
	}
}
