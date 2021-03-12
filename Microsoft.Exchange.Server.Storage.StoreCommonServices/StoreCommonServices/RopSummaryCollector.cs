using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000123 RID: 291
	public sealed class RopSummaryCollector : TraceCollector<RopSummaryAggregator, RopSummaryContainer, RopTraceKey, RopSummaryParameters>, IRopSummaryCollector, ITraceCollector<RopTraceKey, RopSummaryParameters>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00039851 File Offset: 0x00037A51
		public RopSummaryCollector(StoreDatabase database) : base(database, LoggerType.RopSummary)
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0003985B File Offset: 0x00037A5B
		internal static void Initialize()
		{
			if (RopSummaryCollector.ropSummaryCollectorDatabaseSlot == -1)
			{
				RopSummaryCollector.ropSummaryCollectorDatabaseSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0003986F File Offset: 0x00037A6F
		internal static void MountHandler(Context context)
		{
			context.Database.ComponentData[RopSummaryCollector.ropSummaryCollectorDatabaseSlot] = new RopSummaryCollector(context.Database);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00039891 File Offset: 0x00037A91
		internal static IRopSummaryCollector GetRopSummaryCollector(Context context)
		{
			return RopSummaryCollector.GetRopSummaryCollector(context.Database);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0003989E File Offset: 0x00037A9E
		internal static IRopSummaryCollector GetRopSummaryCollector(StoreDatabase database)
		{
			if (database != null)
			{
				return (RopSummaryCollector)database.ComponentData[RopSummaryCollector.ropSummaryCollectorDatabaseSlot];
			}
			return RopSummaryCollector.Null;
		}

		// Token: 0x0400064A RID: 1610
		public static IRopSummaryCollector Null = new RopSummaryCollector.NullSummaryCollector();

		// Token: 0x0400064B RID: 1611
		private static int ropSummaryCollectorDatabaseSlot = -1;

		// Token: 0x02000124 RID: 292
		internal class NullSummaryCollector : IRopSummaryCollector, ITraceCollector<RopTraceKey, RopSummaryParameters>
		{
			// Token: 0x06000B68 RID: 2920 RVA: 0x000398D0 File Offset: 0x00037AD0
			public void Add(RopTraceKey key, RopSummaryParameters parameters)
			{
			}
		}
	}
}
