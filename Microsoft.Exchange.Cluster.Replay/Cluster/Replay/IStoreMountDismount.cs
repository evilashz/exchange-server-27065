using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031D RID: 797
	internal interface IStoreMountDismount : IDisposable
	{
		// Token: 0x060020C3 RID: 8387
		void MountDatabase(Guid guidStorageGroup, Guid guidMdb, int flags);

		// Token: 0x060020C4 RID: 8388
		void UnmountDatabase(Guid guidStorageGroup, Guid guidMdb, int flags);
	}
}
