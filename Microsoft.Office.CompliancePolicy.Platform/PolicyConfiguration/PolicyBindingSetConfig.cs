using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000098 RID: 152
	public sealed class PolicyBindingSetConfig : PolicyConfigBase
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000C23C File Offset: 0x0000A43C
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000C268 File Offset: 0x0000A468
		public Guid PolicyDefinitionConfigId
		{
			get
			{
				object obj = base[PolicyBindingSetConfigSchema.PolicyDefinitionConfigId];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return default(Guid);
			}
			set
			{
				base[PolicyBindingSetConfigSchema.PolicyDefinitionConfigId] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000C27B File Offset: 0x0000A47B
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000C28D File Offset: 0x0000A48D
		public IEnumerable<PolicyBindingConfig> AppliedScopes
		{
			get
			{
				return (IEnumerable<PolicyBindingConfig>)base[PolicyBindingSetConfigSchema.AppliedScopes];
			}
			set
			{
				base[PolicyBindingSetConfigSchema.AppliedScopes] = value;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C29C File Offset: 0x0000A49C
		public override void ResetChangeTracking()
		{
			if (this.AppliedScopes != null)
			{
				foreach (PolicyBindingConfig policyBindingConfig in this.AppliedScopes)
				{
					policyBindingConfig.ResetChangeTracking();
				}
			}
			base.ResetChangeTracking();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public override void MarkAsDeleted()
		{
			base.MarkAsDeleted();
			if (this.AppliedScopes != null)
			{
				foreach (PolicyBindingConfig policyBindingConfig in this.AppliedScopes)
				{
					policyBindingConfig.MarkAsDeleted();
				}
			}
		}
	}
}
