using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006B RID: 107
	[DataContract]
	internal sealed class RuleActionCopyData : RuleActionMoveCopyData
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00009ABB File Offset: 0x00007CBB
		public RuleActionCopyData()
		{
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00009AC3 File Offset: 0x00007CC3
		public RuleActionCopyData(RuleAction.MoveCopy ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00009ACC File Offset: 0x00007CCC
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.MoveCopy(RuleAction.Type.OP_COPY, base.StoreEntryID, base.FolderEntryID);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00009AE0 File Offset: 0x00007CE0
		protected override string ToStringInternal()
		{
			return string.Format("COPY {0}", base.ToStringInternal());
		}
	}
}
