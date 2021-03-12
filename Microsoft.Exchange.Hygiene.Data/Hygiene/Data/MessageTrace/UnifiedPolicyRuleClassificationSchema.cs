using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A8 RID: 424
	internal class UnifiedPolicyRuleClassificationSchema
	{
		// Token: 0x04000880 RID: 2176
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = UnifiedPolicyCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000881 RID: 2177
		internal static readonly HygienePropertyDefinition ObjectIdProperty = UnifiedPolicyCommonSchema.ObjectIdProperty;

		// Token: 0x04000882 RID: 2178
		internal static readonly HygienePropertyDefinition DataSourceProperty = UnifiedPolicyCommonSchema.DataSourceProperty;

		// Token: 0x04000883 RID: 2179
		internal static readonly HygienePropertyDefinition RuleIdProperty = UnifiedPolicyRuleSchema.RuleIdProperty;

		// Token: 0x04000884 RID: 2180
		internal static readonly HygienePropertyDefinition ClassificationIdProperty = new HygienePropertyDefinition("ClassificationId", typeof(Guid));

		// Token: 0x04000885 RID: 2181
		internal static readonly HygienePropertyDefinition CountProperty = new HygienePropertyDefinition("Count", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000886 RID: 2182
		internal static readonly HygienePropertyDefinition ConfidenceProperty = new HygienePropertyDefinition("Confidence", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
