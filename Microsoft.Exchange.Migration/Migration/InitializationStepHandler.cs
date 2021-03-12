using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000187 RID: 391
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InitializationStepHandler : IStepHandler
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x0004D868 File Offset: 0x0004BA68
		public InitializationStepHandler(IMigrationDataProvider dataProvider)
		{
			this.DataProvider = dataProvider;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0004D877 File Offset: 0x0004BA77
		public bool ExpectMailboxData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0004D87A File Offset: 0x0004BA7A
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0004D882 File Offset: 0x0004BA82
		private protected IMigrationDataProvider DataProvider { protected get; private set; }

		// Token: 0x06001247 RID: 4679 RVA: 0x0004D88B File Offset: 0x0004BA8B
		public IStepSettings Discover(MigrationJobItem jobItem, MailboxData localMailbox)
		{
			return null;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004D88E File Offset: 0x0004BA8E
		public void Validate(MigrationJobItem jobItem)
		{
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004D890 File Offset: 0x0004BA90
		public IStepSnapshot Inject(MigrationJobItem jobItem)
		{
			return null;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0004D894 File Offset: 0x0004BA94
		public IStepSnapshot Process(ISnapshotId id, MigrationJobItem jobItem, out bool updated)
		{
			updated = false;
			string identifier = jobItem.Identifier;
			List<MigrationStep> remainingSteps = jobItem.MigrationJob.Workflow.GetRemainingSteps(jobItem.WorkflowPosition);
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(this.DataProvider);
			migrationJobObjectCache.PreSeed(jobItem.MigrationJob);
			foreach (MigrationJobItem migrationJobItem in MigrationJobItem.GetByIdentifier(this.DataProvider, null, identifier, migrationJobObjectCache))
			{
				if (!(migrationJobItem.JobItemGuid == jobItem.JobItemGuid) && migrationJobItem.State != MigrationState.Disabled)
				{
					if (jobItem.MigrationJobId.Equals(migrationJobItem.MigrationJobId))
					{
						throw new UserDuplicateInCSVException(identifier);
					}
					if (migrationJobItem.MigrationType == jobItem.MigrationType && migrationJobItem.MigrationJob != null && migrationJobItem.MigrationJob.JobDirection == jobItem.MigrationJob.JobDirection && migrationJobItem.State != MigrationState.Completed && remainingSteps.Intersect(migrationJobItem.MigrationJob.Workflow.GetRemainingSteps(migrationJobItem.WorkflowPosition)).Any<MigrationStep>())
					{
						throw new UserDuplicateInOtherBatchException(identifier, migrationJobItem.MigrationJob.JobName);
					}
				}
			}
			return null;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
		public void Start(ISnapshotId id)
		{
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004D9D6 File Offset: 0x0004BBD6
		public IStepSnapshot Stop(ISnapshotId id)
		{
			return null;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004D9D9 File Offset: 0x0004BBD9
		public void Delete(ISnapshotId id)
		{
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004D9DB File Offset: 0x0004BBDB
		public bool CanProcess(MigrationJobItem jobItem)
		{
			return true;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004D9E0 File Offset: 0x0004BBE0
		public MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null)
		{
			MigrationUserStatus? migrationUserStatus = MigrationJobItem.ResolveFlagStatus(flags);
			if (migrationUserStatus != null)
			{
				return migrationUserStatus.Value;
			}
			return MigrationUserStatus.Validating;
		}

		// Token: 0x0400065A RID: 1626
		public static readonly MigrationStage[] AllowedStages = new MigrationStage[]
		{
			MigrationStage.Processing
		};
	}
}
