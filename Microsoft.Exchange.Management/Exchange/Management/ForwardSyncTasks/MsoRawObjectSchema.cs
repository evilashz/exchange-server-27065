using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000368 RID: 872
	internal class MsoRawObjectSchema : ObjectSchema
	{
		// Token: 0x04001921 RID: 6433
		public static readonly SimpleProviderPropertyDefinition SyncObjectData = new SimpleProviderPropertyDefinition("SyncObjectData", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001922 RID: 6434
		public static readonly SimpleProviderPropertyDefinition ExternalObjectId = new SimpleProviderPropertyDefinition("ExternalObjectId", ExchangeObjectVersion.Exchange2010, typeof(SyncObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001923 RID: 6435
		public static readonly SimpleProviderPropertyDefinition ServiceInstanceId = new SimpleProviderPropertyDefinition("ServiceInstanceId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001924 RID: 6436
		public static readonly SimpleProviderPropertyDefinition ObjectAndLinks = new SimpleProviderPropertyDefinition("ObjectAndLinks", ExchangeObjectVersion.Exchange2010, typeof(DirectoryObjectsAndLinks), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001925 RID: 6437
		public static readonly SimpleProviderPropertyDefinition SerializedObjectAndLinks = new SimpleProviderPropertyDefinition("SerializedObjectAndLinks", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001926 RID: 6438
		public static readonly SimpleProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001927 RID: 6439
		public static readonly SimpleProviderPropertyDefinition WindowsLiveNetId = new SimpleProviderPropertyDefinition("WindowsLiveNetId", ExchangeObjectVersion.Exchange2010, typeof(NetID), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001928 RID: 6440
		public static readonly SimpleProviderPropertyDefinition AllLinksCollected = new SimpleProviderPropertyDefinition("AllLinksCollected", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001929 RID: 6441
		public static readonly SimpleProviderPropertyDefinition LinksCollected = new SimpleProviderPropertyDefinition("LinksCollected", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400192A RID: 6442
		public static readonly SimpleProviderPropertyDefinition AssignedPlanCapabilities = new SimpleProviderPropertyDefinition("AssignedPlanCapabilities", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400192B RID: 6443
		public static readonly SimpleProviderPropertyDefinition ExchangeValidationError = new SimpleProviderPropertyDefinition("ExchangeValidationError", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
