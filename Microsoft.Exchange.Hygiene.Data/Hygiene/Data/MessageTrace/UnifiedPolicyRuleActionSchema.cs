using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A6 RID: 422
	internal class UnifiedPolicyRuleActionSchema
	{
		// Token: 0x0400087A RID: 2170
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = UnifiedPolicyCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x0400087B RID: 2171
		internal static readonly HygienePropertyDefinition ObjectIdProperty = UnifiedPolicyCommonSchema.ObjectIdProperty;

		// Token: 0x0400087C RID: 2172
		internal static readonly HygienePropertyDefinition DataSourceProperty = UnifiedPolicyCommonSchema.DataSourceProperty;

		// Token: 0x0400087D RID: 2173
		internal static readonly HygienePropertyDefinition RuleIdProperty = UnifiedPolicyRuleSchema.RuleIdProperty;

		// Token: 0x0400087E RID: 2174
		internal static readonly HygienePropertyDefinition ActionProperty = new HygienePropertyDefinition("Action", typeof(string));
	}
}
