using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001AA RID: 426
	internal interface IClientPerformanceDataSink
	{
		// Token: 0x0600087B RID: 2171
		void ReportEvent(ClientPerformanceEventArgs clientEvent);

		// Token: 0x0600087C RID: 2172
		void ReportLatency(TimeSpan clientLatency);
	}
}
