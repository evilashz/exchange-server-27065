using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000068 RID: 104
	public interface ITopologyProvider
	{
		// Token: 0x06000440 RID: 1088
		TopologyInfo GetLocalServerTopology(bool isForceRefresh = false);
	}
}
