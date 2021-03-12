using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000766 RID: 1894
	internal class SyncUserSchema : UserSchema
	{
		// Token: 0x04003EF8 RID: 16120
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003EF9 RID: 16121
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003EFA RID: 16122
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003EFB RID: 16123
		public static readonly ADPropertyDefinition UsageLocation = ADRecipientSchema.UsageLocation;

		// Token: 0x04003EFC RID: 16124
		public static readonly ADPropertyDefinition RemoteRecipientType = ADUserSchema.RemoteRecipientType;

		// Token: 0x04003EFD RID: 16125
		public static readonly ADPropertyDefinition UsnCreated = ADRecipientSchema.UsnCreated;

		// Token: 0x04003EFE RID: 16126
		public static readonly ADPropertyDefinition ReleaseTrack = ADRecipientSchema.ReleaseTrack;

		// Token: 0x04003EFF RID: 16127
		public static readonly ADPropertyDefinition PreviousExchangeGuid = IADMailStorageSchema.PreviousExchangeGuid;

		// Token: 0x04003F00 RID: 16128
		public static readonly ADPropertyDefinition PreviousDatabase = IADMailStorageSchema.PreviousDatabase;

		// Token: 0x04003F01 RID: 16129
		public static readonly ADPropertyDefinition AccountDisabled = ADUserSchema.AccountDisabled;

		// Token: 0x04003F02 RID: 16130
		public static readonly ADPropertyDefinition StsRefreshTokensValidFrom = ADUserSchema.StsRefreshTokensValidFrom;
	}
}
