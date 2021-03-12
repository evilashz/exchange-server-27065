using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200005F RID: 95
	internal sealed class AirSyncConstants
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000B2DD File Offset: 0x000094DD
		private AirSyncConstants()
		{
		}

		// Token: 0x04000177 RID: 375
		public const string AirSyncBackendVDirRelativePath = "ClientAccess\\sync";

		// Token: 0x04000178 RID: 376
		public const string AirSyncFrontendVDirRelativePath = "FrontEnd\\HttpProxy\\sync";

		// Token: 0x04000179 RID: 377
		public const string AirSyncVDirName = "Microsoft-Server-ActiveSync";

		// Token: 0x0400017A RID: 378
		public const string AirSyncAppPoolName = "MSExchangeSyncAppPool";

		// Token: 0x0400017B RID: 379
		public const string AirSyncFilterName = "Microsoft Exchange Server ActiveSync Filter";

		// Token: 0x0400017C RID: 380
		public const string AirSync = "AirSync";

		// Token: 0x0400017D RID: 381
		public const string AirSyncAssemblyRelativePath = "ClientAccess\\sync\\bin\\Microsoft.Exchange.AirSync.dll";

		// Token: 0x0400017E RID: 382
		public const string GetActiveSyncDeviceCmdLetName = "Get-ActiveSyncDevice";

		// Token: 0x0400017F RID: 383
		public const string GetMobileDeviceCmdLetName = "Get-MobileDevice";

		// Token: 0x04000180 RID: 384
		public const string GetActiveSyncDeviceStatisticsCmdLetName = "Get-ActiveSyncDeviceStatistics";

		// Token: 0x04000181 RID: 385
		public const string GetMobileDeviceStatisticsCmdLetName = "Get-MobileDeviceStatistics";

		// Token: 0x04000182 RID: 386
		public const string RemoveActiveSyncDeviceCmdLetName = "Remove-ActiveSyncDevice";

		// Token: 0x04000183 RID: 387
		public const string RemoveMobileDeviceCmdLetName = "Remove-MobileDevice";

		// Token: 0x04000184 RID: 388
		public const string ClearActiveSyncDeviceCmdLetName = "Clear-ActiveSyncDevice";

		// Token: 0x04000185 RID: 389
		public const string ClearMobileDeviceCmdLetName = "Clear-MobileDevice";

		// Token: 0x04000186 RID: 390
		public const string GetActiveSyncMailboxPolicyCmdLetName = "Get-ActiveSyncMailboxPolicy";

		// Token: 0x04000187 RID: 391
		public const string GetMobileDeviceMailboxPolicyCmdLetName = "Get-MobileDeviceMailboxPolicy";

		// Token: 0x04000188 RID: 392
		public const string SetActiveSyncMailboxPolicyCmdLetName = "Set-ActiveSyncMailboxPolicy";

		// Token: 0x04000189 RID: 393
		public const string SetMobileDeviceMailboxPolicyCmdLetName = "Set-MobileDeviceMailboxPolicy";

		// Token: 0x0400018A RID: 394
		public const string NewActiveSyncMailboxPolicyCmdLetName = "New-ActiveSyncMailboxPolicy";

		// Token: 0x0400018B RID: 395
		public const string NewMobileDeviceMailboxPolicyCmdLetName = "New-MobileDeviceMailboxPolicy";

		// Token: 0x0400018C RID: 396
		public const string RemoveActiveSyncMailboxPolicyCmdLetName = "Remove-ActiveSyncMailboxPolicy";

		// Token: 0x0400018D RID: 397
		public const string RemoveMobileDeviceMailboxPolicyCmdLetName = "Remove-MobileMailboxPolicy";
	}
}
