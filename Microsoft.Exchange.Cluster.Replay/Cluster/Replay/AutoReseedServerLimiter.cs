using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200027E RID: 638
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutoReseedServerLimiter
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x00065D54 File Offset: 0x00063F54
		public AutoReseedServerLimiter(IEnumerable<CopyStatusClientCachedEntry> serverCopyStatuses)
		{
			if (serverCopyStatuses != null)
			{
				foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in serverCopyStatuses)
				{
					if (copyStatusClientCachedEntry.Result == CopyStatusRpcResult.Success && !copyStatusClientCachedEntry.IsActive)
					{
						if (copyStatusClientCachedEntry.CopyStatus.CopyStatus == CopyStatusEnum.Seeding)
						{
							this.m_seedsInProgress++;
						}
						if (copyStatusClientCachedEntry.CopyStatus.ContentIndexStatus == ContentIndexStatusType.Seeding)
						{
							this.m_ciSeedsInProgress++;
						}
					}
				}
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00065E00 File Offset: 0x00064000
		public bool TryStartSeed(out int maximumLimit)
		{
			maximumLimit = this.m_maxSeedsInParallel;
			if (this.m_seedsInProgress >= this.m_maxSeedsInParallel)
			{
				return false;
			}
			this.m_seedsInProgress++;
			return true;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00065E29 File Offset: 0x00064029
		public bool TryStartCiSeed(out int maximumLimit)
		{
			maximumLimit = this.m_maxCiSeedsInParallel;
			if (this.m_ciSeedsInProgress >= this.m_maxCiSeedsInParallel)
			{
				return false;
			}
			this.m_ciSeedsInProgress++;
			return true;
		}

		// Token: 0x040009E5 RID: 2533
		private readonly int m_maxSeedsInParallel = RegistryParameters.AutoReseedDbMaxConcurrentSeeds;

		// Token: 0x040009E6 RID: 2534
		private readonly int m_maxCiSeedsInParallel = RegistryParameters.AutoReseedCiMaxConcurrentSeeds;

		// Token: 0x040009E7 RID: 2535
		private int m_seedsInProgress;

		// Token: 0x040009E8 RID: 2536
		private int m_ciSeedsInProgress;
	}
}
