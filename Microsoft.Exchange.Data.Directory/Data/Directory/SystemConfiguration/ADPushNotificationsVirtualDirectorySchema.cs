using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000380 RID: 896
	internal sealed class ADPushNotificationsVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x04001930 RID: 6448
		public static readonly ADPropertyDefinition LiveIdAuthentication = new ADPropertyDefinition("LiveIdAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADPushNotificationsVirtualDirectory.LiveIdAuthenticationGetter), new SetterDelegate(ADPushNotificationsVirtualDirectory.LiveIdAuthenticationSetter), null, null);
	}
}
