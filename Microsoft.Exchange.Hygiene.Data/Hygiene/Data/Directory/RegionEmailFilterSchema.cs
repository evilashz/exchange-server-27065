using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000FA RID: 250
	internal class RegionEmailFilterSchema : ADObjectSchema
	{
		// Token: 0x04000527 RID: 1319
		public static readonly HygienePropertyDefinition FilterStatus = new HygienePropertyDefinition("Enabled", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
