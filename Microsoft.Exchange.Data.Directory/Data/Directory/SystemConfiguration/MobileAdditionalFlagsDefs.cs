using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F2 RID: 1266
	[Flags]
	internal enum MobileAdditionalFlagsDefs
	{
		// Token: 0x0400265C RID: 9820
		AllowStorageCard = 1,
		// Token: 0x0400265D RID: 9821
		AllowCamera = 2,
		// Token: 0x0400265E RID: 9822
		RequireDeviceEncryption = 4,
		// Token: 0x0400265F RID: 9823
		AllowUnsignedApplications = 8,
		// Token: 0x04002660 RID: 9824
		AllowUnsignedInstallationPackages = 16,
		// Token: 0x04002661 RID: 9825
		MinPasswordComplexCharacters = 32,
		// Token: 0x04002662 RID: 9826
		AllowWiFi = 64,
		// Token: 0x04002663 RID: 9827
		AllowTextMessaging = 128,
		// Token: 0x04002664 RID: 9828
		AllowPOPIMAPEmail = 256,
		// Token: 0x04002665 RID: 9829
		AllowIrDA = 512,
		// Token: 0x04002666 RID: 9830
		RequireManualSyncWhenRoaming = 1024,
		// Token: 0x04002667 RID: 9831
		AllowDesktopSync = 2048,
		// Token: 0x04002668 RID: 9832
		AllowHTMLEmail = 4096,
		// Token: 0x04002669 RID: 9833
		RequireSignedSMIMEMessages = 8192,
		// Token: 0x0400266A RID: 9834
		RequireEncryptedSMIMEMessages = 16384,
		// Token: 0x0400266B RID: 9835
		AllowSMIMESoftCerts = 32768,
		// Token: 0x0400266C RID: 9836
		AllowBrowser = 65536,
		// Token: 0x0400266D RID: 9837
		AllowConsumerEmail = 131072,
		// Token: 0x0400266E RID: 9838
		AllowRemoteDesktop = 262144,
		// Token: 0x0400266F RID: 9839
		AllowInternetSharing = 524288,
		// Token: 0x04002670 RID: 9840
		AllowExternalDeviceManagement = 1048576,
		// Token: 0x04002671 RID: 9841
		AllowMobileOTAUpdate = 2097152,
		// Token: 0x04002672 RID: 9842
		IrmEnabled = 4194304
	}
}
