using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000074 RID: 116
	[DataContract]
	internal abstract class RuleActionInMailboxMoveCopyData : RuleActionMoveCopyData
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x00009D5E File Offset: 0x00007F5E
		public RuleActionInMailboxMoveCopyData()
		{
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00009D66 File Offset: 0x00007F66
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00009D6E File Offset: 0x00007F6E
		private new byte[] StoreEntryID { get; set; }

		// Token: 0x06000544 RID: 1348 RVA: 0x00009D77 File Offset: 0x00007F77
		public RuleActionInMailboxMoveCopyData(RuleAction.MoveCopy ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00009D80 File Offset: 0x00007F80
		protected override string ToStringInternal()
		{
			return string.Format("FolderEID:{0}", TraceUtils.DumpEntryId(base.FolderEntryID));
		}
	}
}
