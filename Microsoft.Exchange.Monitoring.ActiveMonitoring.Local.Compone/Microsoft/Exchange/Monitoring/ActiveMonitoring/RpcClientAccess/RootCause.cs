using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x0200020C RID: 524
	public static class RootCause
	{
		// Token: 0x04000B03 RID: 2819
		public const string Unknown = "UnknownIssue";

		// Token: 0x04000B04 RID: 2820
		public const string HighLatency = "HighLatency";

		// Token: 0x04000B05 RID: 2821
		public const string Passive = "Passive";

		// Token: 0x04000B06 RID: 2822
		public const string SecureChannelFailure = "SecureChannel";

		// Token: 0x04000B07 RID: 2823
		public const string NetworkingFailure = "Networking";

		// Token: 0x04000B08 RID: 2824
		public const string MailboxUpgrade = "MailboxUpgrade";

		// Token: 0x04000B09 RID: 2825
		public const string AccountIssue = "AccountIssue";

		// Token: 0x04000B0A RID: 2826
		public const string Unauthorized = "Unauthorized";

		// Token: 0x04000B0B RID: 2827
		public const string MapiHttpVersionMismatch = "MapiHttpVersionMismatch";

		// Token: 0x04000B0C RID: 2828
		public const string StoreFailure = "StoreFailure";
	}
}
