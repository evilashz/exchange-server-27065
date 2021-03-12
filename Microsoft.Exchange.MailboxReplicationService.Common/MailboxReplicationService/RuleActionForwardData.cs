using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000073 RID: 115
	[DataContract]
	internal sealed class RuleActionForwardData : RuleActionFwdDelegateData
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00009D1D File Offset: 0x00007F1D
		public RuleActionForwardData()
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00009D25 File Offset: 0x00007F25
		public RuleActionForwardData(RuleAction.Forward ruleAction) : base(ruleAction, (uint)ruleAction.Flags)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00009D34 File Offset: 0x00007F34
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Forward(DataConverter<AdrEntryConverter, AdrEntry, AdrEntryData>.GetNative(base.Recipients), (RuleAction.Forward.ActionFlags)base.Flags);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00009D4C File Offset: 0x00007F4C
		protected override string ToStringInternal()
		{
			return string.Format("FORWARD {0}", base.ToStringInternal());
		}
	}
}
