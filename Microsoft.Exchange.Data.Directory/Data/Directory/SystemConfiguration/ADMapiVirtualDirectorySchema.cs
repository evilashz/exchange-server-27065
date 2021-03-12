using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000348 RID: 840
	internal class ADMapiVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x040017C2 RID: 6082
		public static readonly ADPropertyDefinition IISAuthenticationMethods = new ADPropertyDefinition("IISAuthenticationMethods", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationMethod), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValueDefinedConstraint<AuthenticationMethod>(new AuthenticationMethod[]
			{
				AuthenticationMethod.Basic,
				AuthenticationMethod.Ntlm,
				AuthenticationMethod.Kerberos,
				AuthenticationMethod.Negotiate,
				AuthenticationMethod.LiveIdBasic,
				AuthenticationMethod.OAuth
			})
		}, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADMapiVirtualDirectory.GetIISAuthenticationMethods), new SetterDelegate(ADMapiVirtualDirectory.SetIISAuthenticationMethods), null, null);
	}
}
