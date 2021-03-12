using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000084 RID: 132
	internal interface IMExSession : IExecutionControl
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000402 RID: 1026
		string Name { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000403 RID: 1027
		long TotalProcessorTime { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000404 RID: 1028
		IDispatcher Dispatcher { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000405 RID: 1029
		AgentRecord CurrentAgent { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000406 RID: 1030
		string LastAgentName { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000407 RID: 1031
		string EventTopic { get; }

		// Token: 0x06000408 RID: 1032
		void StartCpuTracking(string agentName);

		// Token: 0x06000409 RID: 1033
		void StopCpuTracking();

		// Token: 0x0600040A RID: 1034
		void CleanupCpuTracker();

		// Token: 0x0600040B RID: 1035
		void Invoke(string topic, object source, object e);

		// Token: 0x0600040C RID: 1036
		IAsyncResult BeginInvoke(string topic, object source, object e, AsyncCallback callback, object callbackState);

		// Token: 0x0600040D RID: 1037
		void EndInvoke(IAsyncResult asyncResult);

		// Token: 0x0600040E RID: 1038
		object Clone();

		// Token: 0x0600040F RID: 1039
		void Close();

		// Token: 0x06000410 RID: 1040
		void Dispose();

		// Token: 0x06000411 RID: 1041
		DisposeTracker GetDisposeTracker();

		// Token: 0x06000412 RID: 1042
		void SuppressDisposeTracker();
	}
}
