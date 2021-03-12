using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A28 RID: 2600
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationEndpointSchema : ObjectSchema
	{
		// Token: 0x04003609 RID: 13833
		public static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2012, typeof(MigrationEndpointId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400360A RID: 13834
		public static readonly ProviderPropertyDefinition EndpointType = new SimpleProviderPropertyDefinition("EndpointType", ExchangeObjectVersion.Exchange2012, typeof(MigrationType), PropertyDefinitionFlags.TaskPopulated, MigrationType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400360B RID: 13835
		public static readonly ProviderPropertyDefinition MaxConcurrentMigrations = new SimpleProviderPropertyDefinition("MaxConcurrentMigrations", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x0400360C RID: 13836
		public static readonly ProviderPropertyDefinition MaxConcurrentIncrementalSyncs = new SimpleProviderPropertyDefinition("MaxConcurrentIncrementalSyncs", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x0400360D RID: 13837
		public static readonly ProviderPropertyDefinition RemoteServer = new SimpleProviderPropertyDefinition("RemoteServer", ExchangeObjectVersion.Exchange2012, typeof(Fqdn), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400360E RID: 13838
		public static readonly ProviderPropertyDefinition Username = new SimpleProviderPropertyDefinition("Username", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400360F RID: 13839
		public static readonly ProviderPropertyDefinition Port = new SimpleProviderPropertyDefinition("Port", ExchangeObjectVersion.Exchange2012, typeof(int?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003610 RID: 13840
		public static readonly ProviderPropertyDefinition AuthenticationMethod = new SimpleProviderPropertyDefinition("AuthenticationMethod", ExchangeObjectVersion.Exchange2012, typeof(AuthenticationMethod?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003611 RID: 13841
		public static readonly ProviderPropertyDefinition Security = new SimpleProviderPropertyDefinition("Security", ExchangeObjectVersion.Exchange2012, typeof(IMAPSecurityMechanism?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003612 RID: 13842
		public static readonly ProviderPropertyDefinition RPCProxyServer = new SimpleProviderPropertyDefinition("RPCProxyServer", ExchangeObjectVersion.Exchange2012, typeof(Fqdn), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003613 RID: 13843
		public static readonly ProviderPropertyDefinition ExchangeServer = new SimpleProviderPropertyDefinition("ExchangeServer", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003614 RID: 13844
		public static readonly ProviderPropertyDefinition NspiServer = new SimpleProviderPropertyDefinition("NspiServer", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003615 RID: 13845
		public static readonly ProviderPropertyDefinition MailboxPermission = new SimpleProviderPropertyDefinition("MailboxPermission", ExchangeObjectVersion.Exchange2012, typeof(MigrationMailboxPermission), PropertyDefinitionFlags.TaskPopulated, MigrationMailboxPermission.Admin, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003616 RID: 13846
		public static readonly ProviderPropertyDefinition UseAutoDiscover = new SimpleProviderPropertyDefinition("UseAutoDiscover", ExchangeObjectVersion.Exchange2012, typeof(bool?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003617 RID: 13847
		public static readonly PropertyDefinition SourceMailboxLegacyDN = new SimpleProviderPropertyDefinition("SourceMailboxLegacyDN", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003618 RID: 13848
		public static PropertyDefinition PublicFolderDatabaseServerLegacyDN = new SimpleProviderPropertyDefinition("PublicFolderDatabaseServerLegacyDN", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003619 RID: 13849
		public static readonly ProviderPropertyDefinition ObjectState = UserConfigurationObjectSchema.ObjectState;

		// Token: 0x0400361A RID: 13850
		public static readonly ProviderPropertyDefinition ExchangeVersion = UserConfigurationObjectSchema.ExchangeVersion;
	}
}
