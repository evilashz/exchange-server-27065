using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006E RID: 110
	internal class ConfigurationSettingBatchSchema
	{
		// Token: 0x0400028F RID: 655
		public static readonly PropertyDefinition OrganizationalUnitRootProp = ADObjectSchema.OrganizationalUnitRoot;

		// Token: 0x04000290 RID: 656
		public static readonly HygienePropertyDefinition BatchProp = new HygienePropertyDefinition("Batch", typeof(IConfigurable), null, ADPropertyDefinitionFlags.MultiValued);
	}
}
