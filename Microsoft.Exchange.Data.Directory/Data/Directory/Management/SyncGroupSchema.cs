using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000769 RID: 1897
	internal class SyncGroupSchema : WindowsGroupSchema
	{
		// Token: 0x04003F15 RID: 16149
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003F16 RID: 16150
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003F17 RID: 16151
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04003F18 RID: 16152
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003F19 RID: 16153
		public static readonly ADPropertyDefinition UsnCreated = ADRecipientSchema.UsnCreated;
	}
}
