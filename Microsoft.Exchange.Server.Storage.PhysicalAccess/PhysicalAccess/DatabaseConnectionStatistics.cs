using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000A RID: 10
	public class DatabaseConnectionStatistics : IExecutionTrackingData<DatabaseConnectionStatistics>
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000B8EF File Offset: 0x00009AEF
		public DatabaseConnectionStatistics()
		{
			this.dumpedRowStats.Initialize();
			this.RowStats.Initialize();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000B90D File Offset: 0x00009B0D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000B915 File Offset: 0x00009B15
		public int Count { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000B91E File Offset: 0x00009B1E
		public TimeSpan TotalTime
		{
			get
			{
				return this.TimeInDatabase;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000B928 File Offset: 0x00009B28
		public void Reset()
		{
			this.TimeInDatabase = TimeSpan.Zero;
			this.Count = 0;
			this.ThreadStats = default(JET_THREADSTATS);
			this.RowStats.Reset();
			this.OffPageBlobHits = 0;
			this.dumpedRowStats.Reset();
			this.dumpedOffPageBlobHits = 0;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000B978 File Offset: 0x00009B78
		public void Aggregate(DatabaseConnectionStatistics dataToAggregate)
		{
			this.TimeInDatabase += dataToAggregate.TimeInDatabase;
			this.Count += dataToAggregate.Count;
			this.ThreadStats += dataToAggregate.ThreadStats;
			this.RowStats.Aggregate(dataToAggregate.RowStats);
			this.OffPageBlobHits += dataToAggregate.OffPageBlobHits;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000B9EC File Offset: 0x00009BEC
		public void AppendToTraceContentBuilder(TraceContentBuilder cb)
		{
			cb.Append(((long)this.TimeInDatabase.TotalMicroseconds()).ToString("N0", CultureInfo.InvariantCulture));
			cb.Append(" us");
			DatabaseOperationStatistics.AppendThreadStatsToTraceContentBuilder(ref this.ThreadStats, cb);
			if (!this.RowStats.IsEmpty)
			{
				cb.Append(", STOR:[");
				this.RowStats.AppendToString(cb);
				cb.Append("]");
			}
			if (this.OffPageBlobHits != 0)
			{
				cb.Append(", opg:[");
				cb.Append(this.OffPageBlobHits);
				cb.Append("]");
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000BA8E File Offset: 0x00009C8E
		public void AppendDetailsToTraceContentBuilder(TraceContentBuilder cb, int indentLevel)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000BA90 File Offset: 0x00009C90
		internal void DumpStatistics(Database database)
		{
			if (database != null && database.PerfInstance != null)
			{
				if (this.RowStats.DumpStats(database.PerfInstance, this.dumpedRowStats))
				{
					this.dumpedRowStats.CopyFrom(this.RowStats);
				}
				if (this.dumpedOffPageBlobHits != this.OffPageBlobHits)
				{
					database.PerfInstance.OffPageBlobHitsPerSec.IncrementBy((long)((ulong)(this.OffPageBlobHits - this.dumpedOffPageBlobHits)));
					this.dumpedOffPageBlobHits = this.OffPageBlobHits;
				}
			}
		}

		// Token: 0x04000059 RID: 89
		private RowStats dumpedRowStats;

		// Token: 0x0400005A RID: 90
		private int dumpedOffPageBlobHits;

		// Token: 0x0400005B RID: 91
		public JET_THREADSTATS ThreadStats;

		// Token: 0x0400005C RID: 92
		public RowStats RowStats;

		// Token: 0x0400005D RID: 93
		public int OffPageBlobHits;

		// Token: 0x0400005E RID: 94
		public TimeSpan TimeInDatabase;
	}
}
