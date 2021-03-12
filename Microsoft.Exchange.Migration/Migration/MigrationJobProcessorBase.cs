using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000105 RID: 261
	internal abstract class MigrationJobProcessorBase : MigrationHierarchyProcessorBase<MigrationJob, MigrationJobItem, StoreObjectId, MigrationJobProcessorResponse>
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00037FC6 File Offset: 0x000361C6
		protected MigrationJobProcessorBase(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00037FD0 File Offset: 0x000361D0
		protected override MigrationProcessorResponse DefaultCorruptedChildResponse
		{
			get
			{
				return MigrationProcessorResponse.Create(MigrationProcessorResult.Failed, null, null);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00037FED File Offset: 0x000361ED
		protected override int? MaxChildObjectsToProcessCount
		{
			get
			{
				return new int?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("ProcessingBatchSize"));
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00038000 File Offset: 0x00036200
		protected override MigrationJobProcessorResponse PerformPoisonDetection()
		{
			if (this.MigrationObject.PoisonCount >= ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationPoisonedCountThreshold"))
			{
				return this.HandlePermanentException(new MigrationPoisonCountThresholdExceededException());
			}
			MigrationJobProcessorResponse result;
			try
			{
				this.MigrationObject.UpdatePoisonCount(this.DataProvider, this.MigrationObject.PoisonCount + 1);
				result = MigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null);
			}
			catch (SaveConflictException ex)
			{
				MigrationApplication.NotifyOfTransientException(ex, "MigrationApplication::RunJobOperation - UpdatePoisonCount: job " + this.MigrationObject);
				result = MigrationJobProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(TimeSpan.Zero), null, null, null, null);
			}
			catch (LocalizedException ex2)
			{
				if (CommonUtils.IsTransientException(ex2))
				{
					MigrationApplication.NotifyOfTransientException(ex2, "MigrationApplication::RunJobOperation - UpdatePoisonCount: job " + this.MigrationObject);
					throw;
				}
				MigrationApplication.NotifyOfCriticalError(ex2, "MigrationApplication::RunJobOperation - UpdatePoisonCount => job = " + this.MigrationObject);
				throw new MigrationTransientException(ex2.LocalizedString, ex2);
			}
			return result;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000380F4 File Offset: 0x000362F4
		protected override bool TryLoad(StoreObjectId childId, out MigrationJobItem child)
		{
			try
			{
				MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(this.DataProvider);
				migrationJobObjectCache.PreSeed(this.MigrationObject);
				child = MigrationJobItem.Load(this.DataProvider, childId, migrationJobObjectCache, false);
			}
			catch (ObjectNotFoundException ex)
			{
				MigrationApplication.NotifyOfIgnoredException(ex, "Couldn't find job-item: " + childId);
				child = null;
				return false;
			}
			return child != null;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00038160 File Offset: 0x00036360
		protected override void SetContext()
		{
			this.originalCurrentCulture = Thread.CurrentThread.CurrentCulture;
			this.originalCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (this.MigrationObject.AdminCulture.IsNeutralCulture)
			{
				throw new UnsupportedAdminCultureException(this.MigrationObject.AdminCulture.ToString());
			}
			Thread.CurrentThread.CurrentCulture = this.MigrationObject.AdminCulture;
			Thread.CurrentThread.CurrentUICulture = this.MigrationObject.AdminCulture;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000381DF File Offset: 0x000363DF
		protected override void RestoreContext()
		{
			Thread.CurrentThread.CurrentCulture = this.originalCurrentCulture;
			Thread.CurrentThread.CurrentUICulture = this.originalCurrentUICulture;
			MigrationLogContext.Current.Job = null;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003820C File Offset: 0x0003640C
		protected override MigrationJobProcessorResponse HandlePermanentException(LocalizedException ex)
		{
			if (ex is MigrationDataCorruptionException)
			{
				MigrationApplication.NotifyOfCorruptJob(ex, "MigrationJobProcessorBase::Process => job " + this.MigrationObject);
				MigrationHelper.SendFriendlyWatson(ex, true, this.MigrationObject.ToString());
			}
			else
			{
				MigrationApplication.NotifyOfCriticalError(ex, "MigrationJobProcessorBase::Process => job " + this.MigrationObject);
			}
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Failed, null, ex, null, null, null);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00038274 File Offset: 0x00036474
		protected override MigrationJobProcessorResponse HandleTransientException(LocalizedException ex)
		{
			if (MigrationApplication.HasTransientErrorReachedThreshold<MigrationJobStatus>(this.MigrationObject.StatusData))
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Transient error reached threshold for job " + this.MigrationObject, new object[0]);
				return this.HandlePermanentException(new TooManyTransientFailuresException(this.MigrationObject.JobName, ex));
			}
			MigrationApplication.NotifyOfTransientException(ex, "MigrationJobItemProcessorBase::Process => job " + this.MigrationObject);
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorTransientErrorRunDelay")), ex, null, null, null);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000382F8 File Offset: 0x000364F8
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			switch (response.Result)
			{
			case MigrationProcessorResult.Working:
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.SyncStarting, MigrationState.Active, null, null, response.DelayTime, null, response.LastProcessedRow, null, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
				return response;
			case MigrationProcessorResult.Waiting:
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.SyncStarting, MigrationState.Waiting, null, null, response.DelayTime, response.Error, response.LastProcessedRow, null, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
				return response;
			case MigrationProcessorResult.Completed:
				throw new NotSupportedException("expected " + response.Result + " to be handled at a different level... ");
			case MigrationProcessorResult.Failed:
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.Failed, MigrationState.Failed, null, null, null, response.Error, response.LastProcessedRow, null, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
				return response;
			}
			throw new NotSupportedException("Not expected to see " + response.Result + " result for a job processor...");
		}

		// Token: 0x040004EA RID: 1258
		private CultureInfo originalCurrentCulture;

		// Token: 0x040004EB RID: 1259
		private CultureInfo originalCurrentUICulture;
	}
}
