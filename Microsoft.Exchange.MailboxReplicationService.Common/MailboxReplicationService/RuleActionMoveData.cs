using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000078 RID: 120
	[DataContract]
	internal sealed class RuleActionMoveData : RuleActionMoveCopyData
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00009E16 File Offset: 0x00008016
		public RuleActionMoveData()
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00009E1E File Offset: 0x0000801E
		public RuleActionMoveData(RuleAction.MoveCopy ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00009E27 File Offset: 0x00008027
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.MoveCopy(RuleAction.Type.OP_MOVE, base.StoreEntryID, base.FolderEntryID);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00009E3B File Offset: 0x0000803B
		protected override string ToStringInternal()
		{
			return string.Format("MOVE {0}", base.ToStringInternal());
		}
	}
}
