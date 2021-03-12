using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000126 RID: 294
	internal class MigrationSessionProcessor : MigrationHierarchyProcessorBase<MigrationSession, MigrationJob, StoreObjectId, MigrationProcessorResponse>
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x00041261 File Offset: 0x0003F461
		public MigrationSessionProcessor(MigrationSession migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0004126C File Offset: 0x0003F46C
		protected override MigrationProcessorResponse DefaultCorruptedChildResponse
		{
			get
			{
				return MigrationProcessorResponse.Create(MigrationProcessorResult.Failed, null, null);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00041289 File Offset: 0x0003F489
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return this.GetChildObjectQueries(true);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00041292 File Offset: 0x0003F492
		protected override int? MaxChildObjectsToProcessCount
		{
			get
			{
				return new int?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("ProcessingSessionSize"));
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000412A4 File Offset: 0x0003F4A4
		protected override MigrationProcessorResponse PerformPoisonDetection()
		{
			return MigrationProcessorResponse.Create(MigrationProcessorResult.Working, null, null);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000412C1 File Offset: 0x0003F4C1
		protected override bool TryLoad(StoreObjectId childId, out MigrationJob child)
		{
			return MigrationJob.TryLoad(this.DataProvider, childId, out child);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000412D0 File Offset: 0x0003F4D0
		protected override MigrationProcessorResponse ProcessChild(MigrationJob child)
		{
			MigrationJobProcessorBase migrationJobProcessorBase = null;
			if (child.Flags.HasFlag(MigrationFlags.Remove))
			{
				migrationJobProcessorBase = new RemoveMigrationJobProcessor(child, this.DataProvider);
			}
			else if (child.Flags.HasFlag(MigrationFlags.Stop))
			{
				migrationJobProcessorBase = new StopMigrationJobProcessor(child, this.DataProvider);
			}
			else if (child.Flags.HasFlag(MigrationFlags.Start))
			{
				migrationJobProcessorBase = new StartMigrationJobProcessor(child, this.DataProvider);
			}
			else if (child.ShouldReport)
			{
				migrationJobProcessorBase = new ReportMigrationJobProcessor(child, this.DataProvider);
			}
			else if (child.Flags == MigrationFlags.None && (child.State == MigrationState.Active || (child.State == MigrationState.Waiting && child.NextProcessTime <= ExDateTime.UtcNow)))
			{
				migrationJobProcessorBase = new ActiveMigrationJobProcessor(child, this.DataProvider);
			}
			if (migrationJobProcessorBase != null)
			{
				return migrationJobProcessorBase.Process();
			}
			return MigrationProcessorResponse.CreateWaitingMax();
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000413CE File Offset: 0x0003F5CE
		protected override void SetContext()
		{
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000413D0 File Offset: 0x0003F5D0
		protected override void RestoreContext()
		{
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000413D4 File Offset: 0x0003F5D4
		protected override MigrationProcessorResponse ProcessObject()
		{
			IUpgradeConstraintAdapter upgradeConstraintAdapter = MigrationServiceFactory.Instance.GetUpgradeConstraintAdapter(this.MigrationObject);
			upgradeConstraintAdapter.AddUpgradeConstraintIfNeeded(this.DataProvider, this.MigrationObject);
			return MigrationProcessorResponse.Create(MigrationProcessorResult.Deleted, null, null);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00041414 File Offset: 0x0003F614
		protected override MigrationProcessorResponse ApplyResponse(MigrationProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed)
			{
				response.Result = MigrationProcessorResult.Working;
			}
			if (response.Result == MigrationProcessorResult.Deleted && this.GetChildObjectIds(this.GetChildObjectQueries(false), new int?(1)).Any<StoreObjectId>())
			{
				response.Result = MigrationProcessorResult.Waiting;
			}
			return response;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00041454 File Offset: 0x0003F654
		protected override MigrationProcessorResponse HandlePermanentException(LocalizedException ex)
		{
			return MigrationProcessorResponse.Create(MigrationProcessorResult.Failed, null, ex);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00041474 File Offset: 0x0003F674
		protected override MigrationProcessorResponse HandleTransientException(LocalizedException ex)
		{
			return MigrationProcessorResponse.Create(MigrationProcessorResult.Failed, null, ex);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000414E8 File Offset: 0x0003F6E8
		private Func<int?, IEnumerable<StoreObjectId>>[] GetChildObjectQueries(bool processable)
		{
			ExDateTime? nextProcessTime = processable ? new ExDateTime?(ExDateTime.UtcNow) : null;
			return new Func<int?, IEnumerable<StoreObjectId>>[]
			{
				(int? maxCount) => MigrationJob.GetIdsWithFlagPresence(this.DataProvider, true, maxCount),
				(int? maxCount) => MigrationJob.GetIdsByState(this.DataProvider, MigrationState.Active, null, maxCount),
				(int? maxCount) => MigrationJob.GetIdsByState(this.DataProvider, MigrationState.Waiting, nextProcessTime, maxCount)
			};
		}
	}
}
