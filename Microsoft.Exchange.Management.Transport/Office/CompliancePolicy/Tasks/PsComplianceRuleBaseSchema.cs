using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D7 RID: 215
	internal class PsComplianceRuleBaseSchema : ADPresentationSchema
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00024D59 File Offset: 0x00022F59
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<RuleStorageSchema>();
		}

		// Token: 0x040003AD RID: 941
		public static readonly ADPropertyDefinition MasterIdentity = UnifiedPolicyStorageBaseSchema.MasterIdentity;

		// Token: 0x040003AE RID: 942
		public static readonly ADPropertyDefinition RuleBlob = RuleStorageSchema.RuleBlob;

		// Token: 0x040003AF RID: 943
		public static readonly ADPropertyDefinition Workload = UnifiedPolicyStorageBaseSchema.WorkloadProp;

		// Token: 0x040003B0 RID: 944
		public static readonly ADPropertyDefinition Policy = RuleStorageSchema.ParentPolicyId;

		// Token: 0x040003B1 RID: 945
		public static readonly ADPropertyDefinition Comment = RuleStorageSchema.Comments;

		// Token: 0x040003B2 RID: 946
		public static readonly ADPropertyDefinition Enabled = RuleStorageSchema.IsEnabled;

		// Token: 0x040003B3 RID: 947
		public static readonly ADPropertyDefinition Mode = RuleStorageSchema.EnforcementMode;

		// Token: 0x040003B4 RID: 948
		public static readonly ADPropertyDefinition ObjectVersion = UnifiedPolicyStorageBaseSchema.PolicyVersion;

		// Token: 0x040003B5 RID: 949
		public static readonly ADPropertyDefinition ContentMatchQuery = RuleStorageSchema.ContentMatchQuery;
	}
}
