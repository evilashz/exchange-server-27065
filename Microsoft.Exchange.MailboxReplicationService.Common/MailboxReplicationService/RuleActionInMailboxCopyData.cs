using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000075 RID: 117
	[DataContract]
	internal sealed class RuleActionInMailboxCopyData : RuleActionInMailboxMoveCopyData
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00009D97 File Offset: 0x00007F97
		public RuleActionInMailboxCopyData()
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00009D9F File Offset: 0x00007F9F
		public RuleActionInMailboxCopyData(RuleAction.InMailboxCopy ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00009DA8 File Offset: 0x00007FA8
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.InMailboxCopy(base.FolderEntryID);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00009DB5 File Offset: 0x00007FB5
		protected override string ToStringInternal()
		{
			return string.Format("INMAILBOXCOPY {0}", base.ToStringInternal());
		}
	}
}
