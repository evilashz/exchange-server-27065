using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000071 RID: 113
	[DataContract]
	internal sealed class RuleActionExternalCopyData : RuleActionExternalMoveCopyData
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00009CB1 File Offset: 0x00007EB1
		public RuleActionExternalCopyData()
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00009CB9 File Offset: 0x00007EB9
		public RuleActionExternalCopyData(RuleAction.ExternalCopy ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00009CC2 File Offset: 0x00007EC2
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.ExternalCopy(base.StoreEntryID, base.FolderEntryID);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00009CD5 File Offset: 0x00007ED5
		protected override string ToStringInternal()
		{
			return string.Format("EXTERNALCOPY {0}", base.ToStringInternal());
		}
	}
}
