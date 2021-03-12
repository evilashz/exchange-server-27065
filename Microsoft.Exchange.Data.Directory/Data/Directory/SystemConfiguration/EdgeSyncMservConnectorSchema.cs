using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000436 RID: 1078
	internal class EdgeSyncMservConnectorSchema : EdgeSyncConnectorSchema
	{
		// Token: 0x0400208E RID: 8334
		public static readonly ADPropertyDefinition ProvisionUrl = new ADPropertyDefinition("ProvisionUrl", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchEdgeSyncMservProvisionUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x0400208F RID: 8335
		public static readonly ADPropertyDefinition SettingUrl = new ADPropertyDefinition("SettingUrl", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchEdgeSyncMservSettingUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002090 RID: 8336
		public static readonly ADPropertyDefinition LocalCertificate = new ADPropertyDefinition("LocalCertificate", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncMservLocalCertificate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002091 RID: 8337
		public static readonly ADPropertyDefinition RemoteCertificate = new ADPropertyDefinition("RemoteCertificate", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncMservRemoteCertificate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002092 RID: 8338
		public static readonly ADPropertyDefinition PrimaryLeaseLocation = new ADPropertyDefinition("PrimaryLeaseLocation", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncMservPrimaryLeaseLocation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002093 RID: 8339
		public static readonly ADPropertyDefinition BackupLeaseLocation = new ADPropertyDefinition("BackupLeaseLocation", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncMservBackupLeaseLocation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
