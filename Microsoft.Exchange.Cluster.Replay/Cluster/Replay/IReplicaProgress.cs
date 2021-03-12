using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReplicaProgress
	{
		// Token: 0x06000A7E RID: 2686
		void ReportOneLogCopied();

		// Token: 0x06000A7F RID: 2687
		void ReportLogsReplayed(long numLogs);
	}
}
