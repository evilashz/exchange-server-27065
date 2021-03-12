using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200061C RID: 1564
	internal sealed class Imap4AdConfigurationSchema : PopImapAdConfigurationSchema
	{
		// Token: 0x04003358 RID: 13144
		public static readonly ADPropertyDefinition MaxCommandSize = new ADPropertyDefinition("MaxCommandSize", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPopImapCommandSize", ADPropertyDefinitionFlags.PersistDefaultValue, 10240, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1024, 16384)
		}, null, null);

		// Token: 0x04003359 RID: 13145
		public static readonly ADPropertyDefinition ShowHiddenFoldersEnabled = new ADPropertyDefinition("ShowHiddenFoldersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PopImapAdConfigurationSchema.PopImapFlags
		}, null, new GetterDelegate(Imap4AdConfiguration.ShowHiddenFoldersEnabledGetter), new SetterDelegate(Imap4AdConfiguration.ShowHiddenFoldersEnabledSetter), null, null);
	}
}
