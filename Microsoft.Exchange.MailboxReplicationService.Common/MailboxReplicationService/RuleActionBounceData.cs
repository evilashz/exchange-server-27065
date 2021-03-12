using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000069 RID: 105
	[DataContract]
	internal sealed class RuleActionBounceData : RuleActionData
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00009856 File Offset: 0x00007A56
		public RuleActionBounceData()
		{
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000985E File Offset: 0x00007A5E
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00009866 File Offset: 0x00007A66
		[DataMember(EmitDefaultValue = false)]
		public uint Code { get; set; }

		// Token: 0x0600050B RID: 1291 RVA: 0x0000986F File Offset: 0x00007A6F
		public RuleActionBounceData(RuleAction.Bounce ruleAction) : base(ruleAction)
		{
			this.Code = (uint)ruleAction.Code;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00009884 File Offset: 0x00007A84
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Bounce((RuleAction.Bounce.BounceCode)this.Code);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00009891 File Offset: 0x00007A91
		protected override string ToStringInternal()
		{
			return string.Format("BOUNCE Code:{0}", this.Code);
		}
	}
}
