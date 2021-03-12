using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000071 RID: 113
	internal sealed class JournalingRule : TransportRule
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00014241 File Offset: 0x00012441
		public JournalingRule(string name) : this(name, null)
		{
			this.gccRuleType = GccType.None;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00014252 File Offset: 0x00012452
		public JournalingRule(string name, Condition condition) : base(name, condition)
		{
			this.gccRuleType = GccType.None;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00014263 File Offset: 0x00012463
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0001426B File Offset: 0x0001246B
		public GccType GccRuleType
		{
			get
			{
				return this.gccRuleType;
			}
			set
			{
				this.gccRuleType = value;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00014274 File Offset: 0x00012474
		public override int GetEstimatedSize()
		{
			int num = 0;
			num += 4;
			return num + base.GetEstimatedSize();
		}

		// Token: 0x0400023E RID: 574
		private GccType gccRuleType;
	}
}
