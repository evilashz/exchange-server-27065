using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000376 RID: 886
	internal sealed class ADO365SuiteServiceVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x04001921 RID: 6433
		public static readonly ADPropertyDefinition LiveIdAuthentication = new ADPropertyDefinition("LiveIdAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADO365SuiteServiceVirtualDirectory.LiveIdAuthenticationGetter), new SetterDelegate(ADO365SuiteServiceVirtualDirectory.LiveIdAuthenticationSetter), null, null);
	}
}
