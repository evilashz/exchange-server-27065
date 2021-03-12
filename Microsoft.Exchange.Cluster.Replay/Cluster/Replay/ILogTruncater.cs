using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E7 RID: 743
	internal interface ILogTruncater : IStartStop
	{
		// Token: 0x06001DE2 RID: 7650
		void RecordReplayGeneration(long genRequired);

		// Token: 0x06001DE3 RID: 7651
		void RecordInspectorGeneration(long genInspected);

		// Token: 0x06001DE4 RID: 7652
		void StopTruncation();
	}
}
