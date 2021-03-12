using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005EA RID: 1514
	internal sealed class UceContentFilterSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003190 RID: 12688
		public static readonly ADPropertyDefinition SCLJunkThreshold = new ADPropertyDefinition("SCLJunkThreshold", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchUceStoreActionThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 4, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
