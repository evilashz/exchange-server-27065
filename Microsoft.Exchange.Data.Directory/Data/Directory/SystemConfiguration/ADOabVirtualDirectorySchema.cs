using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000356 RID: 854
	internal sealed class ADOabVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x0400180E RID: 6158
		public static readonly ADPropertyDefinition PollInterval = new ADPropertyDefinition("PollInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPollInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 480, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 71582)
		}, null, null);

		// Token: 0x0400180F RID: 6159
		public static readonly ADPropertyDefinition OfflineAddressBooks = new ADPropertyDefinition("OfflineAddressBooks", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchOABVirtualDirectoriesBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001810 RID: 6160
		public static readonly ADPropertyDefinition RequireSSL = new ADPropertyDefinition("RequireSSL", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001811 RID: 6161
		public static readonly ADPropertyDefinition BasicAuthentication = new ADPropertyDefinition("BasicAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001812 RID: 6162
		public static readonly ADPropertyDefinition WindowsAuthentication = new ADPropertyDefinition("WindowsAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001813 RID: 6163
		public static readonly ADPropertyDefinition OAuthAuthentication = new ADPropertyDefinition("OAuthAuthentication", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
