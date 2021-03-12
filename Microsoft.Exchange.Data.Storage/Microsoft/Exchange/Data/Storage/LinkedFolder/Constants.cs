using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000966 RID: 2406
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Constants
	{
		// Token: 0x040030D7 RID: 12503
		public const string TeamMailboxSyncSessionClientString = "Client=TeamMailbox;Action=CommitChanges;Interactive=False";

		// Token: 0x040030D8 RID: 12504
		public const string TeamMailboxGetDiagnosticsSessionClientString = "Client=TeamMailbox;Action=GetDiagnostics;Interactive=False";

		// Token: 0x040030D9 RID: 12505
		public const string TeamMailboxSendWelcomeMessageSessionClientString = "Client=TeamMailbox;Action=SendWelcomeMessageToSiteMailbox;Interactive=False";

		// Token: 0x040030DA RID: 12506
		public const string TeamMailboxSendNotificationSessionClientString = "Client=TeamMailbox;Action=Send_Notification";

		// Token: 0x040030DB RID: 12507
		public const string TeamMailboxCmdletInitialLogonSessionClientString = "Client=TeamMailbox;Action=CmdletInitialLogon";

		// Token: 0x040030DC RID: 12508
		public const string DocumentLibSynchronizerMetadataName = "DocumentLibSynchronizerConfigurations";

		// Token: 0x040030DD RID: 12509
		public const string SiteSychronizerMetadataName = "SiteSynchronizerConfigurations";

		// Token: 0x040030DE RID: 12510
		public const string MembershipSychronizerMetadataName = "MembershipSynchronizerConfigurations";

		// Token: 0x040030DF RID: 12511
		public const string MaintenanceSychronizerMetadataName = "MaintenanceSynchronizerConfigurations";

		// Token: 0x040030E0 RID: 12512
		public const string AssistantMetadataName = "SiteMailboxAssistantConfigurations";

		// Token: 0x040030E1 RID: 12513
		public const string DocumentSyncLogConfigurationName = "TeamMailboxDocumentLastSyncCycleLog";

		// Token: 0x040030E2 RID: 12514
		public const string MembershipSyncLogConfigurationName = "TeamMailboxMembershipLastSyncCycleLog";

		// Token: 0x040030E3 RID: 12515
		public const string MaintenanceSyncLogConfigurationName = "TeamMailboxMaintenanceLastSyncCycleLog";

		// Token: 0x040030E4 RID: 12516
		public const string AssistantLogConfigurationName = "SiteMailboxAssistantCycleLog";

		// Token: 0x040030E5 RID: 12517
		public const string FirstAttemptedSyncTime = "FirstAttemptedSyncTime";

		// Token: 0x040030E6 RID: 12518
		public const string LastAttemptedSyncTime = "LastAttemptedSyncTime";

		// Token: 0x040030E7 RID: 12519
		public const string LastSuccessfulSyncTime = "LastSuccessfulSyncTime";

		// Token: 0x040030E8 RID: 12520
		public const string LastSyncFailure = "LastSyncFailure";

		// Token: 0x040030E9 RID: 12521
		public const string LastFailedSyncTime = "LastFailedSyncTime";

		// Token: 0x040030EA RID: 12522
		public const string LastFailedSyncEmailTime = "LastFailedSyncEmailTime";

		// Token: 0x040030EB RID: 12523
		public static readonly TimeSpan MailboxSemaphoreTimeout = TimeSpan.FromSeconds(30.0);
	}
}
