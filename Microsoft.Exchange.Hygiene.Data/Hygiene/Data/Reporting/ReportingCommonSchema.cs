using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C8 RID: 456
	internal class ReportingCommonSchema
	{
		// Token: 0x04000934 RID: 2356
		internal static readonly HygienePropertyDefinition TenantIdProperty = new HygienePropertyDefinition("TenantId", typeof(Guid));

		// Token: 0x04000935 RID: 2357
		internal static readonly HygienePropertyDefinition IPAddressProperty = new HygienePropertyDefinition("IPAddress", typeof(IPAddress));

		// Token: 0x04000936 RID: 2358
		internal static readonly HygienePropertyDefinition FromEmailAddressProperty = new HygienePropertyDefinition("FromEmailAddress", typeof(string));

		// Token: 0x04000937 RID: 2359
		internal static readonly HygienePropertyDefinition AggregationDateProperty = new HygienePropertyDefinition("AggregationDate", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000938 RID: 2360
		internal static readonly HygienePropertyDefinition SpamPercentageProperty = new HygienePropertyDefinition("SpamPercentage", typeof(double));

		// Token: 0x04000939 RID: 2361
		internal static readonly HygienePropertyDefinition SpamMessageCountProperty = new HygienePropertyDefinition("SpamMessageCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400093A RID: 2362
		internal static readonly HygienePropertyDefinition TotalMessageCountProperty = new HygienePropertyDefinition("TotalMessageCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400093B RID: 2363
		internal static readonly HygienePropertyDefinition SpamRecipientCountProperty = new HygienePropertyDefinition("SpamRecipientCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400093C RID: 2364
		internal static readonly HygienePropertyDefinition TotalRecipientCountProperty = new HygienePropertyDefinition("TotalRecipientCount", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400093D RID: 2365
		internal static readonly HygienePropertyDefinition LastNMinutesQueryProperty = new HygienePropertyDefinition("LastNMinutes", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400093E RID: 2366
		internal static readonly HygienePropertyDefinition StartingIPAddressQueryProperty = new HygienePropertyDefinition("StartIPAddress", typeof(IPAddress));

		// Token: 0x0400093F RID: 2367
		internal static readonly HygienePropertyDefinition EndIPAddressQueryProperty = new HygienePropertyDefinition("EndIPAddress", typeof(IPAddress));

		// Token: 0x04000940 RID: 2368
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = new HygienePropertyDefinition("PageSize", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000941 RID: 2369
		internal static readonly HygienePropertyDefinition MinimumGoodMessageCountQueryProperty = new HygienePropertyDefinition("GoodMessageCountThreshold", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000942 RID: 2370
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid?));

		// Token: 0x04000943 RID: 2371
		internal static readonly HygienePropertyDefinition OverriddenOnlyProperty = new HygienePropertyDefinition("OverriddenOnly", typeof(bool?));

		// Token: 0x04000944 RID: 2372
		internal static readonly HygienePropertyDefinition ThrottledOnlyProperty = new HygienePropertyDefinition("ThrottledOnly", typeof(bool?));

		// Token: 0x04000945 RID: 2373
		internal static readonly HygienePropertyDefinition DataCountProperty = new HygienePropertyDefinition("DataCount", typeof(int?));
	}
}
