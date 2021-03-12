using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001BB RID: 443
	internal class AggInboundIPSchema
	{
		// Token: 0x040008E5 RID: 2277
		internal static readonly HygienePropertyDefinition IPAddressProperty = ReportingCommonSchema.IPAddressProperty;

		// Token: 0x040008E6 RID: 2278
		internal static readonly HygienePropertyDefinition MinimumSpamPercentageQueryProperty = new HygienePropertyDefinition("SpamPercentageThreshold", typeof(double));

		// Token: 0x040008E7 RID: 2279
		internal static readonly HygienePropertyDefinition MinimumSpamCountQueryProperty = new HygienePropertyDefinition("SpamCountThreshold", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008E8 RID: 2280
		internal static readonly HygienePropertyDefinition StartingIPAddressQueryProperty = ReportingCommonSchema.StartingIPAddressQueryProperty;

		// Token: 0x040008E9 RID: 2281
		internal static readonly HygienePropertyDefinition EndIPAddressQueryProperty = ReportingCommonSchema.EndIPAddressQueryProperty;

		// Token: 0x040008EA RID: 2282
		internal static readonly HygienePropertyDefinition AggregationDateProperty = ReportingCommonSchema.AggregationDateProperty;

		// Token: 0x040008EB RID: 2283
		internal static readonly HygienePropertyDefinition SpamPercentageProperty = ReportingCommonSchema.SpamPercentageProperty;

		// Token: 0x040008EC RID: 2284
		internal static readonly HygienePropertyDefinition SpamMessageCountProperty = ReportingCommonSchema.SpamMessageCountProperty;

		// Token: 0x040008ED RID: 2285
		internal static readonly HygienePropertyDefinition LastNMinutesQueryProperty = ReportingCommonSchema.LastNMinutesQueryProperty;

		// Token: 0x040008EE RID: 2286
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = ReportingCommonSchema.PageSizeQueryProperty;
	}
}
