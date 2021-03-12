using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006E RID: 110
	[DataContract]
	internal sealed class RuleActionDelegateData : RuleActionFwdDelegateData
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00009C4B File Offset: 0x00007E4B
		public RuleActionDelegateData()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00009C53 File Offset: 0x00007E53
		public RuleActionDelegateData(RuleAction.Delegate ruleAction) : base(ruleAction, 0U)
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00009C5D File Offset: 0x00007E5D
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Delegate(DataConverter<AdrEntryConverter, AdrEntry, AdrEntryData>.GetNative(base.Recipients));
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00009C6F File Offset: 0x00007E6F
		protected override string ToStringInternal()
		{
			return string.Format("DELEGATE {0}", base.ToStringInternal());
		}
	}
}
