using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000093 RID: 147
	public class PolicyAssociationConfig : PolicyConfigBase
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000BE4E File Offset: 0x0000A04E
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000BE60 File Offset: 0x0000A060
		public virtual string Description
		{
			get
			{
				return (string)base[PolicyAssociationConfigSchema.Description];
			}
			set
			{
				base[PolicyAssociationConfigSchema.Description] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000BE6E File Offset: 0x0000A06E
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000BE80 File Offset: 0x0000A080
		public virtual string Comment
		{
			get
			{
				return (string)base[PolicyAssociationConfigSchema.Comment];
			}
			set
			{
				base[PolicyAssociationConfigSchema.Comment] = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000BE8E File Offset: 0x0000A08E
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public virtual IEnumerable<Guid> PolicyDefinitionConfigIds
		{
			get
			{
				return (IEnumerable<Guid>)base[PolicyAssociationConfigSchema.PolicyDefinitionConfigIds];
			}
			set
			{
				base[PolicyAssociationConfigSchema.PolicyDefinitionConfigIds] = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public virtual Guid? DefaultPolicyDefinitionConfigId
		{
			get
			{
				return (Guid?)base[PolicyAssociationConfigSchema.DefaultPolicyDefinitionConfigId];
			}
			set
			{
				base[PolicyAssociationConfigSchema.DefaultPolicyDefinitionConfigId] = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public virtual bool AllowOverride
		{
			get
			{
				object obj = base[PolicyAssociationConfigSchema.AllowOverride];
				return obj != null && (bool)obj;
			}
			set
			{
				base[PolicyAssociationConfigSchema.AllowOverride] = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000BF0B File Offset: 0x0000A10B
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public virtual string Scope
		{
			get
			{
				return (string)base[PolicyAssociationConfigSchema.Scope];
			}
			set
			{
				base[PolicyAssociationConfigSchema.Scope] = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000BF2B File Offset: 0x0000A12B
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000BF3D File Offset: 0x0000A13D
		public virtual DateTime? WhenAppliedUTC
		{
			get
			{
				return (DateTime?)base[PolicyAssociationConfigSchema.WhenAppliedUTC];
			}
			set
			{
				base[PolicyAssociationConfigSchema.WhenAppliedUTC] = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000BF50 File Offset: 0x0000A150
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000BF74 File Offset: 0x0000A174
		public virtual PolicyApplyStatus PolicyApplyStatus
		{
			get
			{
				object obj = base[PolicyAssociationConfigSchema.PolicyApplyStatus];
				if (obj != null)
				{
					return (PolicyApplyStatus)obj;
				}
				return PolicyApplyStatus.Pending;
			}
			set
			{
				base[PolicyAssociationConfigSchema.PolicyApplyStatus] = value;
			}
		}
	}
}
