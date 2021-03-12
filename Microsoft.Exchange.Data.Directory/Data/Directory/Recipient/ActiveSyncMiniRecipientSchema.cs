using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E1 RID: 481
	internal class ActiveSyncMiniRecipientSchema : StorageMiniRecipientSchema
	{
		// Token: 0x04000B28 RID: 2856
		public static readonly ADPropertyDefinition ActiveSyncEnabled = ADUserSchema.ActiveSyncEnabled;

		// Token: 0x04000B29 RID: 2857
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicy = ADUserSchema.ActiveSyncMailboxPolicy;

		// Token: 0x04000B2A RID: 2858
		public static readonly ADPropertyDefinition ActiveSyncAllowedDeviceIDs = ADUserSchema.ActiveSyncAllowedDeviceIDs;

		// Token: 0x04000B2B RID: 2859
		public static readonly ADPropertyDefinition ActiveSyncBlockedDeviceIDs = ADUserSchema.ActiveSyncBlockedDeviceIDs;
	}
}
