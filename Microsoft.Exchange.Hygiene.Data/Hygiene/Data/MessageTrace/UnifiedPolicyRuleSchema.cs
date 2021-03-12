using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A9 RID: 425
	internal class UnifiedPolicyRuleSchema
	{
		// Token: 0x04000887 RID: 2183
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = UnifiedPolicyCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000888 RID: 2184
		internal static readonly HygienePropertyDefinition ObjectIdProperty = UnifiedPolicyCommonSchema.ObjectIdProperty;

		// Token: 0x04000889 RID: 2185
		internal static readonly HygienePropertyDefinition DataSourceProperty = UnifiedPolicyCommonSchema.DataSourceProperty;

		// Token: 0x0400088A RID: 2186
		internal static readonly HygienePropertyDefinition RuleIdProperty = new HygienePropertyDefinition("RuleId", typeof(Guid));

		// Token: 0x0400088B RID: 2187
		internal static readonly HygienePropertyDefinition DLPIdProperty = new HygienePropertyDefinition("DLPId", typeof(Guid?));

		// Token: 0x0400088C RID: 2188
		internal static readonly HygienePropertyDefinition ModeProperty = new HygienePropertyDefinition("Mode", typeof(string));

		// Token: 0x0400088D RID: 2189
		internal static readonly HygienePropertyDefinition SeverityProperty = new HygienePropertyDefinition("Severity", typeof(string));

		// Token: 0x0400088E RID: 2190
		internal static readonly HygienePropertyDefinition OverrideTypeProperty = new HygienePropertyDefinition("OverrideType", typeof(string));

		// Token: 0x0400088F RID: 2191
		internal static readonly HygienePropertyDefinition OverrideJustificationProperty = new HygienePropertyDefinition("OverrideJustification", typeof(string));
	}
}
