using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E1 RID: 481
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CopyStatusServerCachedEntry : ICopyStatusCachedEntry
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004D1AD File Offset: 0x0004B3AD
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x0004D1B5 File Offset: 0x0004B3B5
		public RpcDatabaseCopyStatus2 CopyStatus { get; private set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004D1BE File Offset: 0x0004B3BE
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x0004D1C6 File Offset: 0x0004B3C6
		internal DateTime CreateTimeUtc { get; private set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004D1CF File Offset: 0x0004B3CF
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x0004D1D7 File Offset: 0x0004B3D7
		internal bool ForceRefresh { get; set; }

		// Token: 0x06001323 RID: 4899 RVA: 0x0004D1E0 File Offset: 0x0004B3E0
		internal CopyStatusServerCachedEntry(RpcDatabaseCopyStatus2 status)
		{
			this.CreateTimeUtc = DateTime.UtcNow;
			this.CopyStatus = status;
		}
	}
}
