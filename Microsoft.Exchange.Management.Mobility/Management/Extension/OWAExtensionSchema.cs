using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OWAExtensionSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04000005 RID: 5
		public static readonly SimpleProviderPropertyDefinition MarketplaceAssetID = new SimpleProviderPropertyDefinition("MarketplaceAssetID", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000006 RID: 6
		public static readonly SimpleProviderPropertyDefinition MarketplaceContentMarket = new SimpleProviderPropertyDefinition("MarketplaceContentMarket ", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000007 RID: 7
		public static readonly SimpleProviderPropertyDefinition ProviderName = new SimpleProviderPropertyDefinition("ProviderName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000008 RID: 8
		public static readonly SimpleProviderPropertyDefinition IconURL = new SimpleProviderPropertyDefinition("IconURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000009 RID: 9
		public static readonly SimpleProviderPropertyDefinition AppId = new SimpleProviderPropertyDefinition("AppId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000A RID: 10
		public static readonly SimpleProviderPropertyDefinition AppVersion = new SimpleProviderPropertyDefinition("AppVersion", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000B RID: 11
		public static readonly SimpleProviderPropertyDefinition Type = new SimpleProviderPropertyDefinition("Type", ExchangeObjectVersion.Exchange2010, typeof(ExtensionType?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000C RID: 12
		public static readonly SimpleProviderPropertyDefinition Scope = new SimpleProviderPropertyDefinition("Scope", ExchangeObjectVersion.Exchange2010, typeof(ExtensionInstallScope?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000D RID: 13
		public static readonly SimpleProviderPropertyDefinition Requirements = new SimpleProviderPropertyDefinition("Requirements", ExchangeObjectVersion.Exchange2010, typeof(RequestedCapabilities?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000E RID: 14
		public static readonly SimpleProviderPropertyDefinition ManifestXml = new SimpleProviderPropertyDefinition("ManifestXml", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400000F RID: 15
		public static readonly SimpleProviderPropertyDefinition Enabled = new SimpleProviderPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000010 RID: 16
		public static readonly SimpleProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000011 RID: 17
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000012 RID: 18
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(AppId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OWAExtensionSchema.DisplayName,
			OWAExtensionSchema.AppId,
			XsoMailboxConfigurationObjectSchema.MailboxOwnerId
		}, null, new GetterDelegate(App.IdentityGetter), null);

		// Token: 0x04000013 RID: 19
		public static readonly SimpleProviderPropertyDefinition DefaultStateForUser = new SimpleProviderPropertyDefinition("DefaultStateForUser", ExchangeObjectVersion.Exchange2010, typeof(DefaultStateForUser?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000014 RID: 20
		public static readonly SimpleProviderPropertyDefinition LicenseType = new SimpleProviderPropertyDefinition("LicenseType", ExchangeObjectVersion.Exchange2012, typeof(LicenseType?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000015 RID: 21
		public static readonly SimpleProviderPropertyDefinition SeatsPurchased = new SimpleProviderPropertyDefinition("SeatsPurchased", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000016 RID: 22
		public static readonly SimpleProviderPropertyDefinition EtokenExpirationDate = new SimpleProviderPropertyDefinition("EtokenExpirationDate", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000017 RID: 23
		public static readonly SimpleProviderPropertyDefinition LicensePurchaser = new SimpleProviderPropertyDefinition("LicensePurchaser", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000018 RID: 24
		public static readonly SimpleProviderPropertyDefinition AppStatus = new SimpleProviderPropertyDefinition("AppStatus", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000019 RID: 25
		public static readonly SimpleProviderPropertyDefinition Etoken = new SimpleProviderPropertyDefinition("Etoken", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
