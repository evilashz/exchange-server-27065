using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001BF RID: 447
	internal class AggOutboundIPSchema
	{
		// Token: 0x040008F2 RID: 2290
		internal static readonly HygienePropertyDefinition TenantIdProperty = ReportingCommonSchema.TenantIdProperty;

		// Token: 0x040008F3 RID: 2291
		internal static readonly HygienePropertyDefinition IPAddressProperty = ReportingCommonSchema.IPAddressProperty;

		// Token: 0x040008F4 RID: 2292
		internal static readonly HygienePropertyDefinition FromEmailAddressProperty = ReportingCommonSchema.FromEmailAddressProperty;

		// Token: 0x040008F5 RID: 2293
		internal static readonly HygienePropertyDefinition SpamMessageCountProperty = ReportingCommonSchema.SpamMessageCountProperty;

		// Token: 0x040008F6 RID: 2294
		internal static readonly HygienePropertyDefinition TotalMessageCountProperty = ReportingCommonSchema.TotalMessageCountProperty;

		// Token: 0x040008F7 RID: 2295
		internal static readonly HygienePropertyDefinition SpamRecipientCountProperty = ReportingCommonSchema.SpamRecipientCountProperty;

		// Token: 0x040008F8 RID: 2296
		internal static readonly HygienePropertyDefinition TotalRecipientCountProperty = ReportingCommonSchema.TotalRecipientCountProperty;

		// Token: 0x040008F9 RID: 2297
		internal static readonly HygienePropertyDefinition NDRSpamMessageCountProperty = new HygienePropertyDefinition("NDRSpamMessageCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FA RID: 2298
		internal static readonly HygienePropertyDefinition NDRTotalMessageCountProperty = new HygienePropertyDefinition("NDRTotalMessageCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FB RID: 2299
		internal static readonly HygienePropertyDefinition NDRSpamRecipientCountProperty = new HygienePropertyDefinition("NDRSpamRecipientCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FC RID: 2300
		internal static readonly HygienePropertyDefinition NDRTotalRecipientCountProperty = new HygienePropertyDefinition("NDRTotalRecipientCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FD RID: 2301
		internal static readonly HygienePropertyDefinition UniqueDomainsCountProperty = new HygienePropertyDefinition("UniqueDomainsCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FE RID: 2302
		internal static readonly HygienePropertyDefinition NonProvisionedDomainCountProperty = new HygienePropertyDefinition("NonProvisionedDomainCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008FF RID: 2303
		internal static readonly HygienePropertyDefinition UniqueSendersCountProperty = new HygienePropertyDefinition("UniqueSendersCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000900 RID: 2304
		internal static readonly HygienePropertyDefinition ToSameDomainCountProperty = new HygienePropertyDefinition("ToSameDomainCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000901 RID: 2305
		internal static readonly HygienePropertyDefinition MaxRecipientCountProperty = new HygienePropertyDefinition("MaxRecipientCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000902 RID: 2306
		internal static readonly HygienePropertyDefinition ProvisionedDomainCountProperty = new HygienePropertyDefinition("ProvisionedDomainCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000903 RID: 2307
		internal static readonly HygienePropertyDefinition LastNMinutesQueryProperty = ReportingCommonSchema.LastNMinutesQueryProperty;

		// Token: 0x04000904 RID: 2308
		internal static readonly HygienePropertyDefinition MinimumEmailThresholdQueryProperty = new HygienePropertyDefinition("MinimumEmailThreshold", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000905 RID: 2309
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = ReportingCommonSchema.PageSizeQueryProperty;

		// Token: 0x04000906 RID: 2310
		internal static readonly HygienePropertyDefinition SummaryOnlyQueryProperty = new HygienePropertyDefinition("SummaryOnly", typeof(bool), true, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
