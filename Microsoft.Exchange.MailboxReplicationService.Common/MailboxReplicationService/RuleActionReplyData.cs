using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007A RID: 122
	[DataContract]
	internal sealed class RuleActionReplyData : RuleActionBaseReplyData
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00009E90 File Offset: 0x00008090
		public RuleActionReplyData()
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00009E98 File Offset: 0x00008098
		public RuleActionReplyData(RuleAction.Reply ruleAction) : base(ruleAction, ruleAction.ReplyTemplateMessageEntryID, ruleAction.ReplyTemplateGuid, (uint)ruleAction.Flags)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00009EB3 File Offset: 0x000080B3
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Reply(base.ReplyTemplateMessageEntryID, base.ReplyTemplateGuid, (RuleAction.Reply.ActionFlags)base.Flags);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00009ECC File Offset: 0x000080CC
		protected override string ToStringInternal()
		{
			return string.Format("REPLY {0}", base.ToStringInternal());
		}
	}
}
