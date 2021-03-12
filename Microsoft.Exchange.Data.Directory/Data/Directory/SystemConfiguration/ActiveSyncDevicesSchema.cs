using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000305 RID: 773
	internal class ActiveSyncDevicesSchema : ADContainerSchema
	{
		// Token: 0x04001640 RID: 5696
		public static readonly ADPropertyDefinition DeletionPeriod = new ADPropertyDefinition("DeletionPeriod", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchDeletionPeriod", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001641 RID: 5697
		public static readonly ADPropertyDefinition ObjectsDeletedThisPeriod = new ADPropertyDefinition("ObjectsDeletedThisPeriod", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchObjectsDeletedThisPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
