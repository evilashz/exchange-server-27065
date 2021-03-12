using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000559 RID: 1369
	internal static class ReplicationEventId
	{
		// Token: 0x0400229B RID: 8859
		public const int NoMonitoringError = 10000;

		// Token: 0x0400229C RID: 8860
		public const int CannotReadRoleFromRegistry = 10001;

		// Token: 0x0400229D RID: 8861
		public const int NoMailboxRoleInstalled = 10002;

		// Token: 0x0400229E RID: 8862
		public const int ServerConfigurationError = 10003;

		// Token: 0x0400229F RID: 8863
		public const int HighPriorityCheckFailedError = 10004;

		// Token: 0x040022A0 RID: 8864
		public const int HighPriorityCheckWarningError = 10005;

		// Token: 0x040022A1 RID: 8865
		public const int DatabaseCheckFailedError = 10006;

		// Token: 0x040022A2 RID: 8866
		public const int DatabaseCheckWarningError = 10007;

		// Token: 0x040022A3 RID: 8867
		public const int MediumPriorityCheckFailedError = 10008;

		// Token: 0x040022A4 RID: 8868
		public const int MediumPriorityCheckWarningError = 10009;

		// Token: 0x040022A5 RID: 8869
		public const int CannotLocateServer = 10010;

		// Token: 0x040022A6 RID: 8870
		public const int CannotRunMonitoringTaskRemotely = 10011;
	}
}
