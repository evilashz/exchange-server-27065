using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200009A RID: 154
	public class PolicyDefinitionConfig : PolicyConfigBase
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000C37A File Offset: 0x0000A57A
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000C38C File Offset: 0x0000A58C
		public virtual string Description
		{
			get
			{
				return (string)base[PolicyDefinitionConfigSchema.Description];
			}
			set
			{
				base[PolicyDefinitionConfigSchema.Description] = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000C39A File Offset: 0x0000A59A
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public virtual string Comment
		{
			get
			{
				return (string)base[PolicyDefinitionConfigSchema.Comment];
			}
			set
			{
				base[PolicyDefinitionConfigSchema.Comment] = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000C3BA File Offset: 0x0000A5BA
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000C3CC File Offset: 0x0000A5CC
		public virtual Guid? DefaultPolicyRuleConfigId
		{
			get
			{
				return (Guid?)base[PolicyDefinitionConfigSchema.DefaultPolicyRuleConfigId];
			}
			set
			{
				base[PolicyDefinitionConfigSchema.DefaultPolicyRuleConfigId] = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000C404 File Offset: 0x0000A604
		public virtual Mode Mode
		{
			get
			{
				object obj = base[PolicyDefinitionConfigSchema.Mode];
				if (obj != null)
				{
					return (Mode)obj;
				}
				return Mode.Enforce;
			}
			set
			{
				base[PolicyDefinitionConfigSchema.Mode] = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000C418 File Offset: 0x0000A618
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000C43C File Offset: 0x0000A63C
		public virtual PolicyScenario Scenario
		{
			get
			{
				object obj = base[PolicyDefinitionConfigSchema.Scenario];
				if (obj != null)
				{
					return (PolicyScenario)obj;
				}
				return PolicyScenario.Retention;
			}
			set
			{
				base[PolicyDefinitionConfigSchema.Scenario] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000C450 File Offset: 0x0000A650
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000C474 File Offset: 0x0000A674
		public virtual bool Enabled
		{
			get
			{
				object obj = base[PolicyDefinitionConfigSchema.Enabled];
				return obj != null && (bool)obj;
			}
			set
			{
				base[PolicyDefinitionConfigSchema.Enabled] = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000C487 File Offset: 0x0000A687
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000C499 File Offset: 0x0000A699
		public virtual string CreatedBy
		{
			get
			{
				return (string)base[PolicyDefinitionConfigSchema.CreatedBy];
			}
			set
			{
				base[PolicyDefinitionConfigSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		public virtual string LastModifiedBy
		{
			get
			{
				return (string)base[PolicyDefinitionConfigSchema.LastModifiedBy];
			}
			set
			{
				base[PolicyDefinitionConfigSchema.LastModifiedBy] = value;
			}
		}
	}
}
