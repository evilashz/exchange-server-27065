using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000126 RID: 294
	internal sealed class RopSummaryReporter : TraceDataReporter<RopSummaryContainer>
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0003990F File Offset: 0x00037B0F
		public RopSummaryReporter(StoreDatabase database, IBinaryLogger logger, RopSummaryContainer data) : base(database, logger, data)
		{
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003991C File Offset: 0x00037B1C
		public static TraceContextFlags GetContextFlags(StoreDatabase database)
		{
			TraceContextFlags traceContextFlags = TraceContextFlags.None;
			try
			{
				database.GetSharedLock();
				if (database.IsOnlinePassive || database.IsOnlinePassiveAttachedReadOnly || database.IsOnlinePassiveReplayingLogs)
				{
					traceContextFlags |= TraceContextFlags.Passive;
				}
			}
			finally
			{
				database.ReleaseSharedLock();
			}
			return traceContextFlags;
		}
	}
}
