using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000076 RID: 118
	[DataContract]
	internal sealed class RuleActionInMailboxMoveData : RuleActionInMailboxMoveCopyData
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00009DC7 File Offset: 0x00007FC7
		public RuleActionInMailboxMoveData()
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00009DCF File Offset: 0x00007FCF
		public RuleActionInMailboxMoveData(RuleAction.InMailboxMove ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00009DD8 File Offset: 0x00007FD8
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.InMailboxMove(base.FolderEntryID);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00009DE5 File Offset: 0x00007FE5
		protected override string ToStringInternal()
		{
			return string.Format("INMAILBOXMOVE {0}", base.ToStringInternal());
		}
	}
}
