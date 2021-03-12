using System;
using System.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A2 RID: 418
	internal class UnifiedPolicyDataSetSchema
	{
		// Token: 0x0400086E RID: 2158
		internal static readonly HygienePropertyDefinition UnifiedPolicyObjectTableProperty = new HygienePropertyDefinition("tvp_UnifiedPolicyObjects", typeof(DataTable));

		// Token: 0x0400086F RID: 2159
		internal static readonly HygienePropertyDefinition UnifiedPolicyRuleTableProperty = new HygienePropertyDefinition("tvp_UnifiedPolicyRules", typeof(DataTable));

		// Token: 0x04000870 RID: 2160
		internal static readonly HygienePropertyDefinition UnifiedPolicyRuleActionTableProperty = new HygienePropertyDefinition("tvp_UnifiedPolicyRuleActions", typeof(DataTable));

		// Token: 0x04000871 RID: 2161
		internal static readonly HygienePropertyDefinition UnifiedPolicyRuleClassificationTableProperty = new HygienePropertyDefinition("tvp_UnifiedPolicyRuleClassifications", typeof(DataTable));
	}
}
