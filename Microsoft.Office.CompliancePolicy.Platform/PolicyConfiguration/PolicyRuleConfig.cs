using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200009C RID: 156
	public class PolicyRuleConfig : PolicyConfigBase
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000C535 File Offset: 0x0000A735
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000C547 File Offset: 0x0000A747
		public virtual string RuleBlob
		{
			get
			{
				return (string)base[PolicyRuleConfigSchema.RuleBlob];
			}
			set
			{
				base[PolicyRuleConfigSchema.RuleBlob] = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000C558 File Offset: 0x0000A758
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000C57C File Offset: 0x0000A77C
		public virtual int Priority
		{
			get
			{
				object obj = base[PolicyRuleConfigSchema.Priority];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				base[PolicyRuleConfigSchema.Priority] = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C58F File Offset: 0x0000A78F
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		public virtual string Description
		{
			get
			{
				return (string)base[PolicyRuleConfigSchema.Description];
			}
			set
			{
				base[PolicyRuleConfigSchema.Description] = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000C5AF File Offset: 0x0000A7AF
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		public virtual string Comment
		{
			get
			{
				return (string)base[PolicyRuleConfigSchema.Comment];
			}
			set
			{
				base[PolicyRuleConfigSchema.Comment] = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public virtual Guid PolicyDefinitionConfigId
		{
			get
			{
				object obj = base[PolicyRuleConfigSchema.PolicyDefinitionConfigId];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return default(Guid);
			}
			set
			{
				base[PolicyRuleConfigSchema.PolicyDefinitionConfigId] = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000C610 File Offset: 0x0000A810
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000C634 File Offset: 0x0000A834
		public virtual Mode Mode
		{
			get
			{
				object obj = base[PolicyRuleConfigSchema.Mode];
				if (obj != null)
				{
					return (Mode)obj;
				}
				return Mode.Enforce;
			}
			set
			{
				base[PolicyRuleConfigSchema.Mode] = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000C648 File Offset: 0x0000A848
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000C66C File Offset: 0x0000A86C
		public virtual PolicyScenario Scenario
		{
			get
			{
				object obj = base[PolicyRuleConfigSchema.Scenario];
				if (obj != null)
				{
					return (PolicyScenario)obj;
				}
				return PolicyScenario.Hold;
			}
			set
			{
				base[PolicyRuleConfigSchema.Scenario] = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000C680 File Offset: 0x0000A880
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public virtual bool Enabled
		{
			get
			{
				object obj = base[PolicyRuleConfigSchema.Enabled];
				return obj != null && (bool)obj;
			}
			set
			{
				base[PolicyRuleConfigSchema.Enabled] = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000C6B7 File Offset: 0x0000A8B7
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000C6C9 File Offset: 0x0000A8C9
		public virtual string CreatedBy
		{
			get
			{
				return (string)base[PolicyRuleConfigSchema.CreatedBy];
			}
			set
			{
				base[PolicyRuleConfigSchema.CreatedBy] = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000C6D7 File Offset: 0x0000A8D7
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000C6E9 File Offset: 0x0000A8E9
		public virtual string LastModifiedBy
		{
			get
			{
				return (string)base[PolicyRuleConfigSchema.LastModifiedBy];
			}
			set
			{
				base[PolicyRuleConfigSchema.LastModifiedBy] = value;
			}
		}
	}
}
