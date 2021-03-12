using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000360 RID: 864
	internal class ExchangeWebAppVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x04001837 RID: 6199
		public static readonly ADPropertyDefinition WindowsAuthentication = new ADPropertyDefinition("WindowsAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001838 RID: 6200
		public static readonly ADPropertyDefinition DigestAuthentication = new ADPropertyDefinition("DigestAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001839 RID: 6201
		public static readonly ADPropertyDefinition FormsAuthentication = new ADPropertyDefinition("FormsAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400183A RID: 6202
		public static readonly ADPropertyDefinition BasicAuthentication = new ADPropertyDefinition("BasicAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400183B RID: 6203
		public static readonly ADPropertyDefinition LiveIdAuthentication = new ADPropertyDefinition("LiveIdAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ExchangeWebAppVirtualDirectory.LiveIdAuthenticationGetter), new SetterDelegate(ExchangeWebAppVirtualDirectory.LiveIdAuthenticationSetter), null, null);

		// Token: 0x0400183C RID: 6204
		public static readonly ADPropertyDefinition AdfsAuthentication = new ADPropertyDefinition("AdfsAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ExchangeWebAppVirtualDirectory.AdfsAuthenticationGetter), new SetterDelegate(ExchangeWebAppVirtualDirectory.AdfsAuthenticationSetter), null, null);

		// Token: 0x0400183D RID: 6205
		public static readonly ADPropertyDefinition OAuthAuthentication = new ADPropertyDefinition("OAuthAuthentication", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		}, null, new GetterDelegate(ExchangeWebAppVirtualDirectory.OAuthAuthenticationGetter), new SetterDelegate(ExchangeWebAppVirtualDirectory.OAuthAuthenticationSetter), null, null);

		// Token: 0x0400183E RID: 6206
		public static readonly ADPropertyDefinition DefaultDomain = new ADPropertyDefinition("DefaultDomain", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400183F RID: 6207
		public static readonly ADPropertyDefinition ADGzipLevel = new ADPropertyDefinition("GzipLevel", ExchangeObjectVersion.Exchange2007, typeof(GzipLevel), null, ADPropertyDefinitionFlags.TaskPopulated, GzipLevel.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001840 RID: 6208
		public static readonly ADPropertyDefinition WebSite = new ADPropertyDefinition("WebSite", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001841 RID: 6209
		public static readonly ADPropertyDefinition DisplayName = new ADPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
