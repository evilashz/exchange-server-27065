using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000070 RID: 112
	internal class ConfigurationSettingStatusBatchSchema
	{
		// Token: 0x04000291 RID: 657
		public static readonly PropertyDefinition TenantIdProp = ADObjectSchema.OrganizationalUnitRoot;

		// Token: 0x04000292 RID: 658
		public static readonly HygienePropertyDefinition BatchProp = new HygienePropertyDefinition("Batch", typeof(IConfigurable), null, ADPropertyDefinitionFlags.MultiValued);
	}
}
