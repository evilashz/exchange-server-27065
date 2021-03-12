using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000324 RID: 804
	internal interface IWatson
	{
		// Token: 0x06002111 RID: 8465
		void SendReport(Exception ex);

		// Token: 0x06002112 RID: 8466
		void SendReportOnUnhandledException(Action action);
	}
}
