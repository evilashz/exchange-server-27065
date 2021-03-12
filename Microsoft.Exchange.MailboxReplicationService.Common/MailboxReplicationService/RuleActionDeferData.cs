using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006C RID: 108
	[DataContract]
	internal sealed class RuleActionDeferData : RuleActionData
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00009AF2 File Offset: 0x00007CF2
		public RuleActionDeferData()
		{
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00009AFA File Offset: 0x00007CFA
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00009B02 File Offset: 0x00007D02
		[DataMember(EmitDefaultValue = false)]
		public byte[] Data { get; set; }

		// Token: 0x0600051F RID: 1311 RVA: 0x00009B0B File Offset: 0x00007D0B
		public RuleActionDeferData(RuleAction.Defer ruleAction) : base(ruleAction)
		{
			this.Data = ruleAction.Data;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00009B20 File Offset: 0x00007D20
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Defer(this.Data);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00009B2D File Offset: 0x00007D2D
		protected override string ToStringInternal()
		{
			return string.Format("DEFER {0}", TraceUtils.DumpBytes(this.Data));
		}
	}
}
