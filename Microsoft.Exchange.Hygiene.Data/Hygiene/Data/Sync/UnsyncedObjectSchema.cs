using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200022E RID: 558
	internal class UnsyncedObjectSchema : UnpublishedObjectSchema
	{
		// Token: 0x04000B69 RID: 2921
		public static readonly HygienePropertyDefinition SyncTypeProp = CommonSyncProperties.SyncTypeProp;

		// Token: 0x04000B6A RID: 2922
		public static readonly HygienePropertyDefinition BatchIdProp = CommonSyncProperties.BatchIdProp;
	}
}
