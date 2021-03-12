using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200035A RID: 858
	internal sealed class ADOrganizationalUnitSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001819 RID: 6169
		internal const int MSOSyncEnabledShift = 0;

		// Token: 0x0400181A RID: 6170
		internal const int SMTPAddressCheckWithAcceptedDomainShift = 1;

		// Token: 0x0400181B RID: 6171
		internal const int SyncMBXAndDLToMservShift = 2;

		// Token: 0x0400181C RID: 6172
		internal const int RelocationInProgressShift = 3;

		// Token: 0x0400181D RID: 6173
		public static readonly ADPropertyDefinition ConfigurationUnitLink = new ADPropertyDefinition("ConfigurationUnitLink", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchConfigurationUnitBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400181E RID: 6174
		public new static readonly ADPropertyDefinition OrganizationId = new ADPropertyDefinition("OrganizationId", ExchangeObjectVersion.Exchange2003, typeof(OrganizationId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOrganizationalUnitSchema.ConfigurationUnitLink,
			ADObjectSchema.OrganizationalUnitRoot,
			ADObjectSchema.ConfigurationUnit
		}, null, new GetterDelegate(ADOrganizationalUnit.OuOrganizationIdGetter), null, null, null);

		// Token: 0x0400181F RID: 6175
		public static readonly ADPropertyDefinition UPNSuffixes = SharedPropertyDefinitions.UPNSuffixes;

		// Token: 0x04001820 RID: 6176
		public static readonly ADPropertyDefinition MServSyncConfigFlags = new ADPropertyDefinition("MServSyncConfigFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchEdgeSyncConfigFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001821 RID: 6177
		public static readonly ADPropertyDefinition MSOSyncEnabled = ADObject.BitfieldProperty("MSOSyncEnabled", 0, ADOrganizationalUnitSchema.MServSyncConfigFlags);

		// Token: 0x04001822 RID: 6178
		public static readonly ADPropertyDefinition SMTPAddressCheckWithAcceptedDomain = ADObject.BitfieldProperty("SMTPAddressCheckWithAcceptedDomain", 1, ADOrganizationalUnitSchema.MServSyncConfigFlags);

		// Token: 0x04001823 RID: 6179
		public static readonly ADPropertyDefinition SyncMBXAndDLToMserv = ADObject.BitfieldProperty("SyncMBXAndDLToMserv", 2, ADOrganizationalUnitSchema.MServSyncConfigFlags);

		// Token: 0x04001824 RID: 6180
		public static readonly ADPropertyDefinition RelocationInProgress = ADObject.BitfieldProperty("RelocationInProgress", 3, ADOrganizationalUnitSchema.MServSyncConfigFlags);
	}
}
