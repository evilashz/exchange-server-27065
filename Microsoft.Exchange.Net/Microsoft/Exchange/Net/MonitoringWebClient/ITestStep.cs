using System;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000782 RID: 1922
	internal interface ITestStep
	{
		// Token: 0x0600262E RID: 9774
		IAsyncResult BeginExecute(IHttpSession session, AsyncCallback callback, object state);

		// Token: 0x0600262F RID: 9775
		void EndExecute(IAsyncResult result);

		// Token: 0x06002630 RID: 9776
		Task CreateTask(IHttpSession session);

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002631 RID: 9777
		object Result { get; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002632 RID: 9778
		// (set) Token: 0x06002633 RID: 9779
		TimeSpan? MaxRunTime { get; set; }
	}
}
