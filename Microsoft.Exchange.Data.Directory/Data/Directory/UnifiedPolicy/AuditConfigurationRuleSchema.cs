using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A13 RID: 2579
	internal class AuditConfigurationRuleSchema : ADPresentationSchema
	{
		// Token: 0x06007737 RID: 30519 RVA: 0x0018891A File Offset: 0x00186B1A
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<RuleStorageSchema>();
		}

		// Token: 0x04004C6F RID: 19567
		public static readonly ADPropertyDefinition MasterIdentity = UnifiedPolicyStorageBaseSchema.MasterIdentity;

		// Token: 0x04004C70 RID: 19568
		public static readonly ADPropertyDefinition RuleBlob = RuleStorageSchema.RuleBlob;

		// Token: 0x04004C71 RID: 19569
		public static readonly ADPropertyDefinition Workload = UnifiedPolicyStorageBaseSchema.WorkloadProp;

		// Token: 0x04004C72 RID: 19570
		public static readonly ADPropertyDefinition Policy = RuleStorageSchema.ParentPolicyId;
	}
}
