using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006F RID: 111
	[DataContract]
	internal sealed class RuleActionDeleteData : RuleActionData
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00009C81 File Offset: 0x00007E81
		public RuleActionDeleteData()
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00009C89 File Offset: 0x00007E89
		public RuleActionDeleteData(RuleAction.Delete ruleAction) : base(ruleAction)
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00009C92 File Offset: 0x00007E92
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Delete();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00009C99 File Offset: 0x00007E99
		protected override string ToStringInternal()
		{
			return "DELETE";
		}
	}
}
