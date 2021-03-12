using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F1 RID: 1265
	[Flags]
	internal enum MobileFlagsDefs
	{
		// Token: 0x0400264D RID: 9805
		PasswordEnabled = 2,
		// Token: 0x0400264E RID: 9806
		AlphanumericPasswordRequired = 4,
		// Token: 0x0400264F RID: 9807
		PasswordRecoveryEnabled = 16,
		// Token: 0x04002650 RID: 9808
		RequireStorageCardEncryption = 32,
		// Token: 0x04002651 RID: 9809
		AttachmentsEnabled = 64,
		// Token: 0x04002652 RID: 9810
		IsDefault = 4096,
		// Token: 0x04002653 RID: 9811
		AllowNonProvisionableDevices = 131072,
		// Token: 0x04002654 RID: 9812
		AllowSimplePassword = 262144,
		// Token: 0x04002655 RID: 9813
		WSSAccessEnabled = 1048576,
		// Token: 0x04002656 RID: 9814
		UNCAccessEnabled = 2097152,
		// Token: 0x04002657 RID: 9815
		DenyMicrosoftPushNotifications = 16777216,
		// Token: 0x04002658 RID: 9816
		DenyApplePushNotifications = 33554432,
		// Token: 0x04002659 RID: 9817
		DenyGooglePushNotifications = 67108864,
		// Token: 0x0400265A RID: 9818
		AllowPendingGetNotifications = 134217728
	}
}
