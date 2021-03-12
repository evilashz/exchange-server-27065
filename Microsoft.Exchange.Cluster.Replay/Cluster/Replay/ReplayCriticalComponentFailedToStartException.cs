using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002CC RID: 716
	internal class ReplayCriticalComponentFailedToStartException : Exception
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x00078FA6 File Offset: 0x000771A6
		public ReplayCriticalComponentFailedToStartException(string componentName)
		{
			this.ComponentName = componentName;
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00078FB5 File Offset: 0x000771B5
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x00078FBD File Offset: 0x000771BD
		public string ComponentName { get; private set; }
	}
}
