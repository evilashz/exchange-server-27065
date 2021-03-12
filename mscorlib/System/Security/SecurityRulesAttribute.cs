using System;

namespace System.Security
{
	// Token: 0x020001CC RID: 460
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		// Token: 0x06001C26 RID: 7206 RVA: 0x00060E0D File Offset: 0x0005F00D
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00060E1C File Offset: 0x0005F01C
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x00060E24 File Offset: 0x0005F024
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00060E2D File Offset: 0x0005F02D
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x040009CA RID: 2506
		private SecurityRuleSet m_ruleSet;

		// Token: 0x040009CB RID: 2507
		private bool m_skipVerificationInFullTrust;
	}
}
