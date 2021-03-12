using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationBatchDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0000D5B2 File Offset: 0x0000B7B2
		public MigrationBatchDataProvider(MigrationDataProvider dataProvider, MigrationBatchStatus? status) : base(dataProvider.MailboxSession)
		{
			this.dataProvider = dataProvider;
			this.status = status;
			this.MigrationSession = MigrationSession.Get(this.dataProvider);
			this.diagnosticEnabled = false;
			this.JobCache = new MigrationJobObjectCache(dataProvider);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D5F2 File Offset: 0x0000B7F2
		public IMigrationDataProvider MailboxProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D5FA File Offset: 0x0000B7FA
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000D602 File Offset: 0x0000B802
		public MigrationSession MigrationSession { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000D60B File Offset: 0x0000B80B
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000D613 File Offset: 0x0000B813
		public MigrationJob MigrationJob { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000D61C File Offset: 0x0000B81C
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000D624 File Offset: 0x0000B824
		public bool IncludeReport { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D62D File Offset: 0x0000B82D
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000D635 File Offset: 0x0000B835
		public MigrationJobObjectCache JobCache { get; private set; }

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D640 File Offset: 0x0000B840
		public static MigrationBatchDataProvider CreateDataProvider(string action, IRecipientSession recipientSession, MigrationBatchStatus? status, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationBatchDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationDataProvider disposable = MigrationDataProvider.CreateProviderForMigrationMailbox(action, recipientSession, partitionMailbox);
				disposeGuard.Add<MigrationDataProvider>(disposable);
				MigrationBatchDataProvider migrationBatchDataProvider = new MigrationBatchDataProvider(disposable, status);
				disposeGuard.Success();
				result = migrationBatchDataProvider;
			}
			return result;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public static bool IsKnownException(Exception exception)
		{
			return exception is MigrationBatchNotFoundException || exception is CsvValidationException || exception is StorageTransientException || exception is StoragePermanentException || exception is MigrationTransientException || exception is MigrationPermanentException || exception is MigrationDataCorruptionException || exception is DiagnosticArgumentException;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D700 File Offset: 0x0000B900
		public void EnableDiagnostics(string argument)
		{
			this.diagnosticEnabled = true;
			this.diagnosticArgument = new MigrationDiagnosticArgument(argument);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D718 File Offset: 0x0000B918
		public MigrationJob CreateBatch(MigrationBatch migrationBatch)
		{
			migrationBatch.OriginalCreationTime = DateTime.UtcNow;
			migrationBatch.OriginalStatisticsEnabled = true;
			MigrationJob migrationJob = this.MigrationSession.CreateJob(this.dataProvider, migrationBatch);
			migrationBatch.Identity = new MigrationBatchId(migrationJob.JobName, migrationJob.JobId);
			return migrationJob;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D762 File Offset: 0x0000B962
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (this.MigrationJob != null)
			{
				return this.CreateBatchFromJob<T>(this.MigrationJob);
			}
			return this.FindBatches<T>(rootId);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D780 File Offset: 0x0000B980
		protected override void InternalSave(ConfigurableObject instance)
		{
			switch (instance.ObjectState)
			{
			case ObjectState.New:
			case ObjectState.Unchanged:
			case ObjectState.Changed:
				break;
			case ObjectState.Deleted:
				base.Delete(instance);
				break;
			default:
				return;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.dataProvider != null)
					{
						this.dataProvider.Dispose();
					}
					this.dataProvider = null;
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationBatchDataProvider>(this);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D800 File Offset: 0x0000BA00
		private MigrationBatch GetMigrationBatch(MigrationJob job)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			if (this.IncludeReport)
			{
				this.dataProvider.LoadReport(job.ReportData);
			}
			MigrationBatch migrationBatch = MigrationJob.GetMigrationBatch(this.dataProvider, this.MigrationSession, job);
			if (this.diagnosticEnabled)
			{
				XElement diagnosticInfo = job.GetDiagnosticInfo(this.dataProvider, this.diagnosticArgument);
				if (diagnosticInfo != null)
				{
					migrationBatch.DiagnosticInfo = diagnosticInfo.ToString();
				}
			}
			return migrationBatch;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		private IEnumerable<T> CreateBatchFromJob<T>(MigrationJob migrationJob)
		{
			if (typeof(T) != typeof(MigrationBatch))
			{
				throw new ArgumentException("unknown type: " + typeof(T));
			}
			if (migrationJob != null && migrationJob.MigrationType != MigrationType.BulkProvisioning)
			{
				MigrationBatch migrationBatch = this.GetMigrationBatch(migrationJob);
				yield return (T)((object)migrationBatch);
			}
			yield break;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		private IEnumerable<T> FindBatches<T>(ObjectId rootId)
		{
			if (typeof(T) != typeof(MigrationBatch))
			{
				throw new ArgumentException("unknown type: " + typeof(T));
			}
			MigrationBatchId batchId = (MigrationBatchId)rootId;
			foreach (MigrationJob job in this.MigrationSession.GetOrderedJobs(this.dataProvider))
			{
				if (this.status == null && batchId == null && job.Status == MigrationJobStatus.Removed)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Skipping batch {0} because it's removed", new object[]
					{
						job
					});
				}
				else if (job.MigrationType == MigrationType.BulkProvisioning)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Skipping batch {0} because it's bulk provisioning", new object[]
					{
						job
					});
				}
				else
				{
					if (batchId != null && !batchId.Equals(MigrationBatchId.Any))
					{
						if (!batchId.Equals(new MigrationBatchId(job.JobName, job.JobId)))
						{
							continue;
						}
					}
					MigrationBatch migrationBatch;
					try
					{
						migrationBatch = this.GetMigrationBatch(job);
					}
					catch (ObjectNotFoundException innerException)
					{
						if (batchId != null && !MigrationBatchId.Any.Equals(batchId))
						{
							throw new MigrationBatchNotFoundException(job.JobName, innerException);
						}
						continue;
					}
					if (this.status != null && migrationBatch.Status != this.status.Value)
					{
						MigrationLogger.Log(MigrationEventType.Verbose, "Skipping batch {0} because status doesn't match {1}", new object[]
						{
							migrationBatch,
							this.status.Value
						});
					}
					else
					{
						yield return (T)((object)migrationBatch);
					}
				}
			}
			yield break;
		}

		// Token: 0x04000106 RID: 262
		private MigrationDataProvider dataProvider;

		// Token: 0x04000107 RID: 263
		private MigrationBatchStatus? status;

		// Token: 0x04000108 RID: 264
		private bool diagnosticEnabled;

		// Token: 0x04000109 RID: 265
		private MigrationDiagnosticArgument diagnosticArgument;
	}
}
