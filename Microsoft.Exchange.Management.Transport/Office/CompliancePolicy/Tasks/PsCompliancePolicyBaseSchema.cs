using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000DF RID: 223
	internal class PsCompliancePolicyBaseSchema : ADPresentationSchema
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000259CE File Offset: 0x00023BCE
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<PolicyStorageSchema>();
		}

		// Token: 0x040003CD RID: 973
		public static readonly ADPropertyDefinition MasterIdentity = UnifiedPolicyStorageBaseSchema.MasterIdentity;

		// Token: 0x040003CE RID: 974
		public static readonly ADPropertyDefinition Comment = PolicyStorageSchema.Comments;

		// Token: 0x040003CF RID: 975
		public static readonly ADPropertyDefinition Enabled = PolicyStorageSchema.IsEnabled;

		// Token: 0x040003D0 RID: 976
		public static readonly ADPropertyDefinition Mode = PolicyStorageSchema.EnforcementMode;

		// Token: 0x040003D1 RID: 977
		public static readonly ADPropertyDefinition Workload = UnifiedPolicyStorageBaseSchema.WorkloadProp;

		// Token: 0x040003D2 RID: 978
		public static readonly ADPropertyDefinition ObjectVersion = UnifiedPolicyStorageBaseSchema.PolicyVersion;
	}
}
