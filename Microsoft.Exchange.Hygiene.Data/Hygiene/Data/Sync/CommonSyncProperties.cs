using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x020000B8 RID: 184
	internal class CommonSyncProperties : ADObjectSchema
	{
		// Token: 0x040003B5 RID: 949
		public static readonly HygienePropertyDefinition ObjectIdProp = new HygienePropertyDefinition("ObjectId", typeof(ADObjectId));

		// Token: 0x040003B6 RID: 950
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("TenantId", typeof(ADObjectId));

		// Token: 0x040003B7 RID: 951
		public static readonly HygienePropertyDefinition ServiceInstanceProp = new HygienePropertyDefinition("ServiceInstance", typeof(string));

		// Token: 0x040003B8 RID: 952
		public static readonly HygienePropertyDefinition SyncOnlyProp = new HygienePropertyDefinition("SyncOnly", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003B9 RID: 953
		public static readonly HygienePropertyDefinition MailEnabledGroupProp = new HygienePropertyDefinition("IsMailEnabledGroup", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003BA RID: 954
		public static readonly HygienePropertyDefinition PublishedProp = new HygienePropertyDefinition("isPublished", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003BB RID: 955
		public static readonly HygienePropertyDefinition ProvisioningFlagsProperty = new HygienePropertyDefinition("ProvisioningFlags", typeof(ProvisioningFlags), ProvisioningFlags.Default, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003BC RID: 956
		public static readonly HygienePropertyDefinition SyncTypeProp = new HygienePropertyDefinition("SyncType", typeof(SyncType));

		// Token: 0x040003BD RID: 957
		public static readonly HygienePropertyDefinition ObjectTypeProp = new HygienePropertyDefinition("ObjectType", typeof(DirectoryObjectClass));

		// Token: 0x040003BE RID: 958
		public static readonly HygienePropertyDefinition BatchIdProp = new HygienePropertyDefinition("BatchId", typeof(Guid));

		// Token: 0x040003BF RID: 959
		public static readonly HygienePropertyDefinition ForwardSyncObjectProp = new HygienePropertyDefinition("ForwardSyncObject", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
