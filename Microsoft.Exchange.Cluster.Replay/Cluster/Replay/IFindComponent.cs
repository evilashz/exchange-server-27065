using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C7 RID: 711
	internal interface IFindComponent
	{
		// Token: 0x06001BDE RID: 7134
		MonitoredDatabase FindMonitoredDatabase(string nodeName, Guid dbGuid);

		// Token: 0x06001BDF RID: 7135
		LogCopier FindLogCopier(string nodeName, Guid dbGuid);
	}
}
