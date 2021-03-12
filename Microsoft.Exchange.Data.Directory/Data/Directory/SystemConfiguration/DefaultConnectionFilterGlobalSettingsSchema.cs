using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000406 RID: 1030
	internal class DefaultConnectionFilterGlobalSettingsSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x000BE00C File Offset: 0x000BC20C
		public DefaultConnectionFilterGlobalSettingsSchema()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>(base.AllProperties);
			hashSet.Remove(ADObjectSchema.ExchangeVersion);
			base.AllProperties = new ReadOnlyCollection<PropertyDefinition>(hashSet.ToArray());
			base.InitializePropertyCollections();
			base.InitializeADObjectSchemaProperties();
		}
	}
}
