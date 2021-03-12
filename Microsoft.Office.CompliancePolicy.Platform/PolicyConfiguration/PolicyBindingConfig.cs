using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000096 RID: 150
	public class PolicyBindingConfig : PolicyConfigBase
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C054 File Offset: 0x0000A254
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000C080 File Offset: 0x0000A280
		public virtual Guid PolicyDefinitionConfigId
		{
			get
			{
				object obj = base[PolicyBindingConfigSchema.PolicyDefinitionConfigId];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return default(Guid);
			}
			set
			{
				base[PolicyBindingConfigSchema.PolicyDefinitionConfigId] = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C093 File Offset: 0x0000A293
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000C0A5 File Offset: 0x0000A2A5
		public virtual Guid? PolicyRuleConfigId
		{
			get
			{
				return (Guid?)base[PolicyBindingConfigSchema.PolicyRuleConfigId];
			}
			set
			{
				base[PolicyBindingConfigSchema.PolicyRuleConfigId] = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		public virtual Guid? PolicyAssociationConfigId
		{
			get
			{
				return (Guid?)base[PolicyBindingConfigSchema.PolicyAssociationConfigId];
			}
			set
			{
				base[PolicyBindingConfigSchema.PolicyAssociationConfigId] = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000C0DD File Offset: 0x0000A2DD
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		public virtual BindingMetadata Scope
		{
			get
			{
				return (BindingMetadata)base[PolicyBindingConfigSchema.Scope];
			}
			set
			{
				base[PolicyBindingConfigSchema.Scope] = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000C100 File Offset: 0x0000A300
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000C124 File Offset: 0x0000A324
		public virtual bool IsExempt
		{
			get
			{
				object obj = base[PolicyBindingConfigSchema.IsExempt];
				return obj != null && (bool)obj;
			}
			set
			{
				base[PolicyBindingConfigSchema.IsExempt] = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000C137 File Offset: 0x0000A337
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000C149 File Offset: 0x0000A349
		public virtual DateTime? WhenAppliedUTC
		{
			get
			{
				return (DateTime?)base[PolicyBindingConfigSchema.WhenAppliedUTC];
			}
			set
			{
				base[PolicyBindingConfigSchema.WhenAppliedUTC] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000C15C File Offset: 0x0000A35C
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000C180 File Offset: 0x0000A380
		public virtual Mode Mode
		{
			get
			{
				object obj = base[PolicyBindingConfigSchema.Mode];
				if (obj != null)
				{
					return (Mode)obj;
				}
				return Mode.Enforce;
			}
			set
			{
				base[PolicyBindingConfigSchema.Mode] = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C194 File Offset: 0x0000A394
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public virtual PolicyApplyStatus PolicyApplyStatus
		{
			get
			{
				object obj = base[PolicyBindingConfigSchema.PolicyApplyStatus];
				if (obj != null)
				{
					return (PolicyApplyStatus)obj;
				}
				return PolicyApplyStatus.Pending;
			}
			set
			{
				base[PolicyBindingConfigSchema.PolicyApplyStatus] = value;
			}
		}
	}
}
