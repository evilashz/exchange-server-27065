using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000070 RID: 112
	[DataContract]
	internal abstract class RuleActionExternalMoveCopyData : RuleActionMoveCopyData
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x00009CA0 File Offset: 0x00007EA0
		public RuleActionExternalMoveCopyData()
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public RuleActionExternalMoveCopyData(RuleAction.MoveCopy ruleAction) : base(ruleAction)
		{
		}
	}
}
