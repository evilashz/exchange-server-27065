using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000571 RID: 1393
	internal sealed class ADRpcHttpVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x04002A33 RID: 10803
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("Flags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchRpcHttpFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A34 RID: 10804
		public static readonly ADPropertyDefinition SSLOffloading = new ADPropertyDefinition("SSLOffloading", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRpcHttpVirtualDirectorySchema.Flags
		}, null, ADObject.FlagGetterDelegate(1, ADRpcHttpVirtualDirectorySchema.Flags), ADObject.FlagSetterDelegate(1, ADRpcHttpVirtualDirectorySchema.Flags), null, null);

		// Token: 0x04002A35 RID: 10805
		public static readonly ADPropertyDefinition ExternalClientAuthenticationMethod = new ADPropertyDefinition("ExternalClientAuthenticationMethod", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationMethod), null, ADPropertyDefinitionFlags.Calculated, AuthenticationMethod.Basic, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValueDefinedConstraint<AuthenticationMethod>(new AuthenticationMethod[]
			{
				AuthenticationMethod.Basic,
				AuthenticationMethod.Ntlm,
				AuthenticationMethod.NegoEx,
				AuthenticationMethod.Negotiate
			})
		}, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetExternalClientAuthenticationMethod), new SetterDelegate(ADRpcHttpVirtualDirectory.SetExternalClientAuthenticationMethod), null, null);

		// Token: 0x04002A36 RID: 10806
		public static readonly ADPropertyDefinition InternalClientAuthenticationMethod = new ADPropertyDefinition("InternalClientAuthenticationMethod", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationMethod), "msExchManagementSettings", ADPropertyDefinitionFlags.None, AuthenticationMethod.Ntlm, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValueDefinedConstraint<AuthenticationMethod>(new AuthenticationMethod[]
			{
				AuthenticationMethod.Basic,
				AuthenticationMethod.Ntlm,
				AuthenticationMethod.NegoEx,
				AuthenticationMethod.Negotiate
			})
		}, null, null);

		// Token: 0x04002A37 RID: 10807
		public static readonly ADPropertyDefinition IISAuthenticationMethods = new ADPropertyDefinition("IISAuthenticationMethods", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationMethod), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValueDefinedConstraint<AuthenticationMethod>(new AuthenticationMethod[]
			{
				AuthenticationMethod.Basic,
				AuthenticationMethod.Ntlm,
				AuthenticationMethod.Kerberos,
				AuthenticationMethod.Negotiate,
				AuthenticationMethod.LiveIdBasic,
				AuthenticationMethod.NegoEx,
				AuthenticationMethod.LiveIdNegotiate
			})
		}, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetIISAuthenticationMethods), new SetterDelegate(ADVirtualDirectory.InternalAuthenticationMethodsSetter), null, null);

		// Token: 0x04002A38 RID: 10808
		public static readonly ADPropertyDefinition ExternalHostname = new ADPropertyDefinition("ExternalHostname", ExchangeObjectVersion.Exchange2007, typeof(Hostname), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.ExternalUrl
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetExternalHostname), new SetterDelegate(ADRpcHttpVirtualDirectory.SetExternalHostname), null, null);

		// Token: 0x04002A39 RID: 10809
		public static readonly ADPropertyDefinition ExternalClientsRequireSsl = new ADPropertyDefinition("ExternalClientsRequireSsl", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.ExternalUrl
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetExternalClientsRequireSsl), new SetterDelegate(ADRpcHttpVirtualDirectory.SetExternalClientsRequireSsl), null, null);

		// Token: 0x04002A3A RID: 10810
		public static readonly ADPropertyDefinition InternalHostname = new ADPropertyDefinition("InternalHostname", ExchangeObjectVersion.Exchange2007, typeof(Hostname), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalUrl
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetInternalHostname), new SetterDelegate(ADRpcHttpVirtualDirectory.SetInternalHostname), null, null);

		// Token: 0x04002A3B RID: 10811
		public static readonly ADPropertyDefinition InternalClientsRequireSsl = new ADPropertyDefinition("InternalClientsRequireSsl", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalUrl
		}, null, new GetterDelegate(ADRpcHttpVirtualDirectory.GetInternalClientsRequireSsl), new SetterDelegate(ADRpcHttpVirtualDirectory.SetInternalClientsRequireSsl), null, null);

		// Token: 0x04002A3C RID: 10812
		public static readonly ADPropertyDefinition XropUrl = new ADPropertyDefinition("Xrop", ExchangeObjectVersion.Exchange2003, typeof(Uri), "url", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);
	}
}
