using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization
{
	// Token: 0x02000105 RID: 261
	internal static class SCStrings
	{
		// Token: 0x0400055D RID: 1373
		public static readonly string UnrecognizedFailure = "Unrecognized failure.";

		// Token: 0x0400055E RID: 1374
		public static readonly string FailureDetails = "{0} failure in \"{1}\" component.";

		// Token: 0x0400055F RID: 1375
		public static readonly string FailureFrontendBackendAuthN = "Cafe could not authenticate itself with the mailbox server. Potentially an issue with AD cross-forest trust or authentication on the backend.";

		// Token: 0x04000560 RID: 1376
		public static readonly string FailureActiveDirectory = "An error occured during an active directory operation. Look at the request failure context below or the protocol logs for the full details and contact Directory team if required.";

		// Token: 0x04000561 RID: 1377
		public static readonly string FailureServerLocator = "Cafe could not lookup the user's backend to proxy to due to an error in server locator. Contact Directory/LiveId team for assistance.";

		// Token: 0x04000562 RID: 1378
		public static readonly string FailureLiveId = "The monitoring account failed to authenticate with Cafe due to a {0} error in the LiveIdBasic auth module. Contact Directory/LiveId team for assistance.";

		// Token: 0x04000563 RID: 1379
		public static readonly string FailureMonitoringAccount = "The monitoring account failed to authenticate with Cafe due to AuthFailure error. Monitoring account is likely misconfigured - contact monitoring team for assistance.";
	}
}
