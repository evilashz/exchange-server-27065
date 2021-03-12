using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x0200010C RID: 268
	internal class TenantConfigurationCacheEntrySchema : ADObjectSchema
	{
		// Token: 0x0400055F RID: 1375
		public static readonly HygienePropertyDefinition Reason = new HygienePropertyDefinition("Reason", typeof(TenantConfigurationCacheEntryReason), TenantConfigurationCacheEntryReason.Pinned, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
