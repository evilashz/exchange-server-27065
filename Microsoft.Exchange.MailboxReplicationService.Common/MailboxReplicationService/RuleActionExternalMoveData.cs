using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000072 RID: 114
	[DataContract]
	internal sealed class RuleActionExternalMoveData : RuleActionExternalMoveCopyData
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00009CE7 File Offset: 0x00007EE7
		public RuleActionExternalMoveData()
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00009CEF File Offset: 0x00007EEF
		public RuleActionExternalMoveData(RuleAction.ExternalMove ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00009CF8 File Offset: 0x00007EF8
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.ExternalMove(base.StoreEntryID, base.FolderEntryID);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00009D0B File Offset: 0x00007F0B
		protected override string ToStringInternal()
		{
			return string.Format("EXTERNALMOVE {0}", base.ToStringInternal());
		}
	}
}
