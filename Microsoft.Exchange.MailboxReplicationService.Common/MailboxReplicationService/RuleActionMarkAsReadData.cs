using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	internal sealed class RuleActionMarkAsReadData : RuleActionData
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00009DF7 File Offset: 0x00007FF7
		public RuleActionMarkAsReadData()
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00009DFF File Offset: 0x00007FFF
		public RuleActionMarkAsReadData(RuleAction.MarkAsRead ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00009E08 File Offset: 0x00008008
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.MarkAsRead();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00009E0F File Offset: 0x0000800F
		protected override string ToStringInternal()
		{
			return "MARKASREAD";
		}
	}
}
