using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000526 RID: 1318
	internal sealed class ADOrganizationConfigSchema : OrganizationSchema
	{
		// Token: 0x040027D0 RID: 10192
		public static readonly ADPropertyDefinition ServicePlan = new ADPropertyDefinition("ServicePlan", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027D1 RID: 10193
		public static readonly ADPropertyDefinition TargetServicePlan = new ADPropertyDefinition("TargetServicePlan", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027D2 RID: 10194
		public static readonly ADPropertyDefinition SharePointUrl = new ADPropertyDefinition("SharePointUrl", ExchangeObjectVersion.Exchange2003, typeof(Uri), "wWWHomePage", "msExchShadowWWWHomePage", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute),
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
