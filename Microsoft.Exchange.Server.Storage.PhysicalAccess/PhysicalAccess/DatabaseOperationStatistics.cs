using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000009 RID: 9
	public class DatabaseOperationStatistics : IExecutionTrackingData<DatabaseOperationStatistics>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000B5D6 File Offset: 0x000097D6
		public DatabaseOperationStatistics()
		{
			this.SmallRowStats.Initialize();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000B5E9 File Offset: 0x000097E9
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000B5F1 File Offset: 0x000097F1
		public int Count { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000B5FA File Offset: 0x000097FA
		public TimeSpan TotalTime
		{
			get
			{
				return this.TimeInDatabase;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000B604 File Offset: 0x00009804
		public void Aggregate(DatabaseOperationStatistics dataToAggregate)
		{
			this.TimeInDatabase += dataToAggregate.TimeInDatabase;
			this.Count += dataToAggregate.Count;
			this.ThreadStats += dataToAggregate.ThreadStats;
			this.SmallRowStats.Aggregate(dataToAggregate.SmallRowStats);
			this.OffPageBlobHits += dataToAggregate.OffPageBlobHits;
			this.Planner = ((this.Planner != null) ? this.Planner : dataToAggregate.Planner);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000B694 File Offset: 0x00009894
		public void AppendToTraceContentBuilder(TraceContentBuilder cb)
		{
			cb.Append(((long)this.TimeInDatabase.TotalMicroseconds()).ToString("N0", CultureInfo.InvariantCulture));
			cb.Append(" us");
			DatabaseOperationStatistics.AppendThreadStatsToTraceContentBuilder(ref this.ThreadStats, cb);
			if (!this.SmallRowStats.IsEmpty)
			{
				cb.Append(", STOR:[");
				this.SmallRowStats.AppendToString(cb);
				cb.Append("]");
			}
			if (this.OffPageBlobHits != 0)
			{
				cb.Append(", opg:[");
				cb.Append(this.OffPageBlobHits);
				cb.Append("]");
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000B736 File Offset: 0x00009936
		public void AppendDetailsToTraceContentBuilder(TraceContentBuilder cb, int indentLevel)
		{
			if (this.Planner != null)
			{
				this.Planner.AppendToTraceContentBuilder(cb, indentLevel, "DB plan steps:");
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000B754 File Offset: 0x00009954
		internal static void AppendThreadStatsToTraceContentBuilder(ref JET_THREADSTATS threadStats, TraceContentBuilder cb)
		{
			if (threadStats.cPageReferenced != 0 || threadStats.cPageRead != 0 || threadStats.cPagePreread != 0 || threadStats.cPageDirtied != 0 || threadStats.cPageRedirtied != 0 || threadStats.cLogRecord != 0 || threadStats.cbLogRecord != 0)
			{
				cb.Append(", JET:[");
				bool flag = false;
				if (threadStats.cPageReferenced != 0)
				{
					cb.Append("ref:");
					cb.Append(threadStats.cPageReferenced);
					flag = true;
				}
				if (threadStats.cPageRead != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("rd:");
					cb.Append(threadStats.cPageRead);
					flag = true;
				}
				if (threadStats.cPagePreread != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("prd:");
					cb.Append(threadStats.cPagePreread);
					flag = true;
				}
				if (threadStats.cPageDirtied != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("dt:");
					cb.Append(threadStats.cPageDirtied);
					flag = true;
				}
				if (threadStats.cPageRedirtied != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("rdt:");
					cb.Append(threadStats.cPageRedirtied);
					flag = true;
				}
				if (threadStats.cLogRecord != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("clg:");
					cb.Append(threadStats.cLogRecord);
					flag = true;
				}
				if (threadStats.cbLogRecord != 0)
				{
					if (flag)
					{
						cb.Append(", ");
					}
					cb.Append("blg:");
					cb.Append((uint)threadStats.cbLogRecord);
				}
				cb.Append("]");
			}
		}

		// Token: 0x04000053 RID: 83
		public JET_THREADSTATS ThreadStats;

		// Token: 0x04000054 RID: 84
		public SmallRowStats SmallRowStats;

		// Token: 0x04000055 RID: 85
		public int OffPageBlobHits;

		// Token: 0x04000056 RID: 86
		public TimeSpan TimeInDatabase;

		// Token: 0x04000057 RID: 87
		public IExecutionPlanner Planner;
	}
}
