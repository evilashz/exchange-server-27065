using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobObjectCache
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002D5D0 File Offset: 0x0002B7D0
		public MigrationJobObjectCache(IMigrationDataProvider dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002D600 File Offset: 0x0002B800
		public MigrationJob GetJob(Guid jobGuid)
		{
			MigrationJobSummary jobSummary = this.GetJobSummary(new Guid?(jobGuid));
			if (jobSummary == null)
			{
				return null;
			}
			return this.GetJob(jobSummary.BatchId);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002D62C File Offset: 0x0002B82C
		public MigrationJob GetJob(MigrationBatchId batch)
		{
			if (batch == null)
			{
				return null;
			}
			MigrationJob uniqueByBatchId;
			if (!this.jobsFromBatchId.TryGetValue(batch, out uniqueByBatchId))
			{
				uniqueByBatchId = MigrationJob.GetUniqueByBatchId(this.dataProvider, batch);
				this.jobsFromBatchId[batch] = uniqueByBatchId;
			}
			return uniqueByBatchId;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002D66C File Offset: 0x0002B86C
		public Guid GetBatchGuidById(MigrationBatchId batchId)
		{
			MigrationJobSummary migrationJobSummary = this.FindJobSummaryById(batchId);
			if (migrationJobSummary == null)
			{
				throw new MigrationBatchNotFoundException(batchId.ToString());
			}
			Guid batchGuid = migrationJobSummary.BatchGuid;
			if (!this.jobSummaryFromGuid.ContainsKey(batchGuid))
			{
				this.jobSummaryFromGuid.Add(batchGuid, migrationJobSummary);
			}
			return batchGuid;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
		public MigrationJobSummary GetJobSummary(Guid? guid)
		{
			if (guid == null)
			{
				return null;
			}
			Guid value = guid.Value;
			MigrationJobSummary migrationJobSummary;
			if (this.jobSummaryFromGuid.TryGetValue(value, out migrationJobSummary))
			{
				return migrationJobSummary;
			}
			if (this.unknownBatchGuids.Contains(value))
			{
				return null;
			}
			migrationJobSummary = this.FindJobSummaryByGuid(value);
			if (migrationJobSummary == null)
			{
				this.unknownBatchGuids.Add(value);
			}
			else
			{
				this.jobSummaryFromGuid.Add(value, migrationJobSummary);
			}
			return migrationJobSummary;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002D720 File Offset: 0x0002B920
		private MigrationJobSummary FindJobSummaryById(MigrationBatchId id)
		{
			if (id == null)
			{
				return null;
			}
			if (id.JobId != Guid.Empty)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass),
					new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, id.JobId)
				});
				MigrationJobSummary migrationJobSummary = this.FindJobSummaryByFilter(filter, MigrationJobObjectCache.SortByJobId);
				if (migrationJobSummary != null)
				{
					return migrationJobSummary;
				}
			}
			QueryFilter filter2 = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass),
				new TextFilter(MigrationBatchMessageSchema.MigrationJobName, id.Name, MatchOptions.FullString, MatchFlags.IgnoreCase)
			});
			return this.FindJobSummaryByFilter(filter2, MigrationJobObjectCache.SortByJobName);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
		private MigrationJobSummary FindJobSummaryByGuid(Guid guid)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, guid)
			});
			return this.FindJobSummaryByFilter(filter, MigrationJobObjectCache.SortByJobId);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002D828 File Offset: 0x0002BA28
		private MigrationJobSummary FindJobSummaryByFilter(QueryFilter filter, SortBy[] sortOrder)
		{
			MigrationJobSummary migrationJobSummary = null;
			foreach (object[] propertyValues in this.dataProvider.QueryRows(filter, sortOrder, MigrationJobSummary.PropertyDefinitions, 2))
			{
				migrationJobSummary = MigrationJobSummary.LoadFromRow(propertyValues);
			}
			if (migrationJobSummary == null)
			{
				return null;
			}
			return migrationJobSummary;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002D88C File Offset: 0x0002BA8C
		internal void PreSeed(MigrationJob job)
		{
			MigrationJobSummary migrationJobSummary = MigrationJobSummary.CreateFromJob(job);
			if (!this.jobSummaryFromGuid.ContainsKey(job.JobId))
			{
				this.jobSummaryFromGuid.Add(job.JobId, migrationJobSummary);
			}
			if (!this.jobsFromBatchId.ContainsKey(migrationJobSummary.BatchId))
			{
				this.jobsFromBatchId.Add(migrationJobSummary.BatchId, job);
			}
		}

		// Token: 0x04000421 RID: 1057
		private static readonly SortBy[] SortByJobId = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending)
		};

		// Token: 0x04000422 RID: 1058
		private static readonly SortBy[] SortByJobName = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationJobName, SortOrder.Ascending)
		};

		// Token: 0x04000423 RID: 1059
		private readonly IMigrationDataProvider dataProvider;

		// Token: 0x04000424 RID: 1060
		private readonly Dictionary<MigrationBatchId, MigrationJob> jobsFromBatchId = new Dictionary<MigrationBatchId, MigrationJob>();

		// Token: 0x04000425 RID: 1061
		private readonly List<Guid> unknownBatchGuids = new List<Guid>();

		// Token: 0x04000426 RID: 1062
		private readonly Dictionary<Guid, MigrationJobSummary> jobSummaryFromGuid = new Dictionary<Guid, MigrationJobSummary>();
	}
}
