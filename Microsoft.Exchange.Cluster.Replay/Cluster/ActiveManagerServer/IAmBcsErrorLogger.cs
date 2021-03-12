using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAmBcsErrorLogger
	{
		// Token: 0x0600069D RID: 1693
		bool IsFailedForServer(AmServerName server);

		// Token: 0x0600069E RID: 1694
		void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage);

		// Token: 0x0600069F RID: 1695
		void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs);

		// Token: 0x060006A0 RID: 1696
		void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage);

		// Token: 0x060006A1 RID: 1697
		void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, bool overwriteAllowed);

		// Token: 0x060006A2 RID: 1698
		void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs);

		// Token: 0x060006A3 RID: 1699
		Exception GetLastException();

		// Token: 0x060006A4 RID: 1700
		string[] GetAllExceptions();
	}
}
