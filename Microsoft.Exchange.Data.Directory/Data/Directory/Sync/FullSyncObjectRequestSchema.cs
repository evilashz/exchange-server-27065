using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007EF RID: 2031
	internal class FullSyncObjectRequestSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040042D5 RID: 17109
		public static readonly SimpleProviderPropertyDefinition ServiceInstanceId = new SimpleProviderPropertyDefinition("ServiceInstanceId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040042D6 RID: 17110
		public static readonly SimpleProviderPropertyDefinition Options = new SimpleProviderPropertyDefinition("Options", ExchangeObjectVersion.Exchange2010, typeof(FullSyncObjectRequestOptions), PropertyDefinitionFlags.None, FullSyncObjectRequestOptions.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040042D7 RID: 17111
		public static readonly SimpleProviderPropertyDefinition State = new SimpleProviderPropertyDefinition("State", ExchangeObjectVersion.Exchange2010, typeof(FullSyncObjectRequestState), PropertyDefinitionFlags.None, FullSyncObjectRequestState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040042D8 RID: 17112
		public static readonly SimpleProviderPropertyDefinition CreationTime = new SimpleProviderPropertyDefinition("CreationTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime), PropertyDefinitionFlags.None, ExDateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
