using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C6 RID: 454
	internal class GoodMessageSchema
	{
		// Token: 0x0400092E RID: 2350
		internal static readonly HygienePropertyDefinition GoodMessageExistsProperty = new HygienePropertyDefinition("GoodMessageExists", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400092F RID: 2351
		internal static readonly HygienePropertyDefinition LastNMinutesQueryProperty = ReportingCommonSchema.LastNMinutesQueryProperty;

		// Token: 0x04000930 RID: 2352
		internal static readonly HygienePropertyDefinition MinimumGoodMessageCountQueryProperty = ReportingCommonSchema.MinimumGoodMessageCountQueryProperty;

		// Token: 0x04000931 RID: 2353
		internal static readonly HygienePropertyDefinition StartingIPAddressQueryProperty = ReportingCommonSchema.StartingIPAddressQueryProperty;

		// Token: 0x04000932 RID: 2354
		internal static readonly HygienePropertyDefinition EndIPAddressQueryProperty = ReportingCommonSchema.EndIPAddressQueryProperty;
	}
}
