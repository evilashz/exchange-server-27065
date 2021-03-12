using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000053 RID: 83
	internal class ScopeSettingsSchema : ObjectSchema
	{
		// Token: 0x040000DE RID: 222
		public static readonly AdminPropertyDefinition ForestViewEnabled = new AdminPropertyDefinition("ForestViewEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000DF RID: 223
		public static readonly AdminPropertyDefinition OrganizationalUnit = new AdminPropertyDefinition("OrganizationalUnit", ExchangeObjectVersion.Exchange2003, typeof(string), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
