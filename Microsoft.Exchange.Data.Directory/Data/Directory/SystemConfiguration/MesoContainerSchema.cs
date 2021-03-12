using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B0 RID: 1200
	internal sealed class MesoContainerSchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400250F RID: 9487
		internal static readonly int DomainPrepVersion = 13237;

		// Token: 0x04002510 RID: 9488
		public static readonly ADPropertyDefinition ObjectVersion = new ADPropertyDefinition("ObjectVersion", ExchangeObjectVersion.Exchange2003, typeof(int), "objectVersion", ADPropertyDefinitionFlags.PersistDefaultValue, MesoContainerSchema.DomainPrepVersion, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002511 RID: 9489
		public static readonly ADPropertyDefinition ShowInAdvancedViewOnly = new ADPropertyDefinition("ShowInAdvancedViewOnly", ExchangeObjectVersion.Exchange2003, typeof(bool), "showInAdvancedViewOnly", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
