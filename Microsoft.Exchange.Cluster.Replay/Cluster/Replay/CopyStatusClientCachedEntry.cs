using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E2 RID: 482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CopyStatusClientCachedEntry : ICopyStatusCachedEntry
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004D1FA File Offset: 0x0004B3FA
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0004D202 File Offset: 0x0004B402
		public RpcDatabaseCopyStatus2 CopyStatus { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004D20B File Offset: 0x0004B40B
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0004D213 File Offset: 0x0004B413
		internal Guid DbGuid { get; private set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004D21C File Offset: 0x0004B41C
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004D224 File Offset: 0x0004B424
		internal AmServerName ServerContacted { get; private set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004D22D File Offset: 0x0004B42D
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004D235 File Offset: 0x0004B435
		internal AmServerName ActiveServer { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004D23E File Offset: 0x0004B43E
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0004D246 File Offset: 0x0004B446
		internal DateTime CreateTimeUtc { get; private set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0004D24F File Offset: 0x0004B44F
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x0004D257 File Offset: 0x0004B457
		internal TimeSpan RpcDuration { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004D260 File Offset: 0x0004B460
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0004D268 File Offset: 0x0004B468
		internal Exception LastException { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004D271 File Offset: 0x0004B471
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0004D279 File Offset: 0x0004B479
		internal CopyStatusRpcResult Result { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004D282 File Offset: 0x0004B482
		internal bool IsActive
		{
			get
			{
				return this.ServerContacted.Equals(this.ActiveServer);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0004D295 File Offset: 0x0004B495
		internal bool IsLocalCopy
		{
			get
			{
				return this.ServerContacted.IsLocalComputerName;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004D2A2 File Offset: 0x0004B4A2
		internal CopyStatusClientCachedEntry(Guid dbGuid, AmServerName serverContacted)
		{
			this.CreateTimeUtc = DateTime.UtcNow;
			this.DbGuid = dbGuid;
			this.ServerContacted = serverContacted;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004D2C3 File Offset: 0x0004B4C3
		internal void TestSetCreateTimeUtc(DateTime time)
		{
			this.CreateTimeUtc = time;
		}
	}
}
