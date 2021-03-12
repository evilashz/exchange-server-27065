using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000391 RID: 913
	internal sealed class ADSnackyServiceVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x0400197D RID: 6525
		public static readonly ADPropertyDefinition LiveIdAuthentication = new ADPropertyDefinition("LiveIdAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADSnackyServiceVirtualDirectory.LiveIdAuthenticationGetter), new SetterDelegate(ADSnackyServiceVirtualDirectory.LiveIdAuthenticationSetter), null, null);
	}
}
