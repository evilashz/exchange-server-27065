using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000325 RID: 805
	internal class Watson : IWatson
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x00099BA0 File Offset: 0x00097DA0
		public void SendReport(Exception ex)
		{
			ExWatson.SendReport(ex);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00099BC0 File Offset: 0x00097DC0
		public void SendReportOnUnhandledException(Action action)
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				action();
			});
		}
	}
}
