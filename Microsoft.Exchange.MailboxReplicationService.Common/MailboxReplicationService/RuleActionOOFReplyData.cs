using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000079 RID: 121
	[DataContract]
	internal sealed class RuleActionOOFReplyData : RuleActionBaseReplyData
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00009E4D File Offset: 0x0000804D
		public RuleActionOOFReplyData()
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00009E55 File Offset: 0x00008055
		public RuleActionOOFReplyData(RuleAction.OOFReply ruleAction) : base(ruleAction, ruleAction.ReplyTemplateMessageEntryID, ruleAction.ReplyTemplateGuid, 0U)
		{
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00009E6B File Offset: 0x0000806B
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.OOFReply(base.ReplyTemplateMessageEntryID, base.ReplyTemplateGuid);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00009E7E File Offset: 0x0000807E
		protected override string ToStringInternal()
		{
			return string.Format("OOFREPLY {0}", base.ToStringInternal());
		}
	}
}
