using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A88 RID: 2696
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040037D6 RID: 14294
		public static readonly ProviderPropertyDefinition FirstAttemptedSyncTime = new SimpleProviderPropertyDefinition("FirstAttemptedSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D7 RID: 14295
		public static readonly ProviderPropertyDefinition LastAttemptedSyncTime = new SimpleProviderPropertyDefinition("LastAttemptedSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D8 RID: 14296
		public static readonly ProviderPropertyDefinition LastSuccessfulSyncTime = new SimpleProviderPropertyDefinition("LastSuccessfulSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D9 RID: 14297
		public static readonly ProviderPropertyDefinition LastFailedSyncTime = new SimpleProviderPropertyDefinition("LastFailedSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037DA RID: 14298
		public static readonly ProviderPropertyDefinition LastFailedSyncEmailTime = new SimpleProviderPropertyDefinition("LastFailedSyncEmailTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037DB RID: 14299
		public static readonly ProviderPropertyDefinition LastSyncFailure = new SimpleProviderPropertyDefinition("LastSyncFailure", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037DC RID: 14300
		public static readonly ProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037DD RID: 14301
		public static readonly ProviderPropertyDefinition Url = new SimpleProviderPropertyDefinition("Url", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
