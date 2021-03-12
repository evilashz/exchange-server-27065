using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200011E RID: 286
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobValidatingProcessor : JobProcessor
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003F3CC File Offset: 0x0003D5CC
		protected virtual bool IsProvisioningSupported
		{
			get
			{
				return base.Job.IsProvisioningSupported;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0003F3D9 File Offset: 0x0003D5D9
		protected virtual bool IsValidationSupported
		{
			get
			{
				return base.SubscriptionHandler.SupportsAdvancedValidation;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0003F3E6 File Offset: 0x0003D5E6
		protected virtual bool IsValidationEnabled
		{
			get
			{
				return base.Job.UseAdvancedValidation;
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003F3F4 File Offset: 0x0003D5F4
		internal static MigrationJobValidatingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type == MigrationType.IMAP || type == MigrationType.ExchangeOutlookAnywhere || type == MigrationType.BulkProvisioning)
				{
					throw new ArgumentException("This MigrationType does not support Validation!: " + type);
				}
			}
			else if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove || type == MigrationType.PublicFolder)
			{
				return new MigrationJobValidatingProcessor();
			}
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003F458 File Offset: 0x0003D658
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003F45B File Offset: 0x0003D65B
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (this.IsProvisioningSupported)
			{
				return MigrationJobStatus.ProvisionStarting;
			}
			return MigrationJobStatus.SyncStarting;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003F4BC File Offset: 0x0003D6BC
		protected sealed override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(0);
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(0);
			if (!this.IsValidationSupported)
			{
				return legacyMigrationJobProcessorResponse;
			}
			JobItemOperationResult jobItemOperationResult = base.FindAndRunJobItemOperation(MigrationJobValidatingProcessor.PollingValidationStatuses, MigrationUserStatus.Failed, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"), (MigrationUserStatus status, int itemCount) => base.Job.GetItemsByStatus(base.DataProvider, status, new int?(itemCount)), delegate(MigrationJobItem jobItem)
			{
				if (this.IsValidationEnabled && jobItem.SupportsAdvancedValidation)
				{
					return base.SubscriptionHandler.TestCreateUnderlyingSubscriptions(jobItem);
				}
				jobItem.SetStatus(base.DataProvider, this.IsProvisioningSupported ? MigrationUserStatus.Provisioning : MigrationUserStatus.Queued);
				return true;
			});
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(jobItemOperationResult.NumItemsProcessed);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(jobItemOperationResult.NumItemsTransitioned);
			legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(base.GetJobItemCount(MigrationJobValidatingProcessor.PollingValidationStatuses));
			if (legacyMigrationJobProcessorResponse.NumItemsProcessed > 0 || legacyMigrationJobProcessorResponse.NumItemsOutstanding > 0)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Working;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003F5A7 File Offset: 0x0003D7A7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobValidatingProcessor>(this);
		}

		// Token: 0x0400051F RID: 1311
		private static readonly MigrationUserStatus[] PollingValidationStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Validating
		};
	}
}
