using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200003C RID: 60
	internal abstract class FilterNodeSynchronizer
	{
		// Token: 0x06000256 RID: 598
		public abstract void Synchronize(FilterNode sourceNode, FilterNode targetNode);
	}
}
