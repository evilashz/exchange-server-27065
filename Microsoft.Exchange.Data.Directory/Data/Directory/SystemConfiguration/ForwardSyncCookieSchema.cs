using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000626 RID: 1574
	internal sealed class ForwardSyncCookieSchema : ForwardSyncCookieHeaderSchema
	{
		// Token: 0x0400337D RID: 13181
		internal static readonly ADPropertyDefinition Version = new ADPropertyDefinition("Version", ExchangeObjectVersion.Exchange2010, typeof(int), "VersionNumber", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400337E RID: 13182
		internal static readonly ADPropertyDefinition SyncPropertySetVersion = new ADPropertyDefinition("SyncPropertySetVersion", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchMSOForwardSyncCookiePropertySetVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400337F RID: 13183
		public static readonly ADPropertyDefinition IsUpgradingSyncPropertySet = ADObject.BitfieldProperty("IsSyncPropertySetUpgrading", 4, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x04003380 RID: 13184
		internal static readonly ADPropertyDefinition Data = new ADPropertyDefinition("Data", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchSyncCookie", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 262144)
		}, null, null);

		// Token: 0x04003381 RID: 13185
		internal static readonly ADPropertyDefinition FilteredContextIds = new ADPropertyDefinition("FilteredContextIds", ExchangeObjectVersion.Exchange2010, typeof(string), "description", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, null, null);
	}
}
