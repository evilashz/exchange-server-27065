using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationJobStage
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0001DE40 File Offset: 0x0001C040
		static MigrationJobStage()
		{
			int length = Enum.GetValues(typeof(MigrationJobStatus)).Length;
			MigrationJobStage.stageMap = new Dictionary<MigrationJobStatus, MigrationJobStage>();
			MigrationJobStage[] array = new MigrationJobStage[]
			{
				MigrationJobStage.Sync,
				MigrationJobStage.Incremental,
				MigrationJobStage.Completion,
				MigrationJobStage.Completed,
				MigrationJobStage.Dormant,
				MigrationJobStage.Corrupted
			};
			foreach (MigrationJobStage migrationJobStage in array)
			{
				foreach (MigrationJobStatus migrationJobStatus in migrationJobStage.SupportedStatuses)
				{
					if (MigrationJobStage.stageMap.ContainsKey(migrationJobStatus))
					{
						throw new InvalidOperationException("expect each job status to have 1 and exactly 1 stage, but found 2 status:" + migrationJobStatus);
					}
					MigrationJobStage.stageMap[migrationJobStatus] = migrationJobStage;
				}
			}
			if (MigrationJobStage.stageMap.Count != length)
			{
				throw new InvalidOperationException("expect every job status to be accounted for, but missing " + (length - MigrationJobStage.stageMap.Count));
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001E02F File Offset: 0x0001C22F
		private MigrationJobStage(string name, MigrationJobStatus[] supportedStatuses)
		{
			this.Name = name;
			this.SupportedStatuses = supportedStatuses;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001E045 File Offset: 0x0001C245
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0001E04D File Offset: 0x0001C24D
		public string Name { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001E056 File Offset: 0x0001C256
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001E05E File Offset: 0x0001C25E
		public MigrationJobStatus[] SupportedStatuses { get; private set; }

		// Token: 0x06000694 RID: 1684 RVA: 0x0001E067 File Offset: 0x0001C267
		public static MigrationJobStage GetStage(MigrationJobStatus status)
		{
			return MigrationJobStage.stageMap[status];
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001E074 File Offset: 0x0001C274
		public bool IsStatusSupported(MigrationJobStatus status)
		{
			foreach (MigrationJobStatus migrationJobStatus in this.SupportedStatuses)
			{
				if (status == migrationJobStatus)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001E0A5 File Offset: 0x0001C2A5
		public override string ToString()
		{
			return " states:" + string.Join<MigrationJobStatus>(" ", this.SupportedStatuses);
		}

		// Token: 0x0400029A RID: 666
		public static readonly MigrationJobStage Sync = new MigrationJobStage("Sync", new MigrationJobStatus[]
		{
			MigrationJobStatus.SyncInitializing,
			MigrationJobStatus.Validating,
			MigrationJobStatus.ProvisionStarting,
			MigrationJobStatus.SyncStarting,
			MigrationJobStatus.SyncCompleting
		});

		// Token: 0x0400029B RID: 667
		public static readonly MigrationJobStage Incremental = new MigrationJobStage("Incremental", new MigrationJobStatus[]
		{
			MigrationJobStatus.SyncCompleted
		});

		// Token: 0x0400029C RID: 668
		public static readonly MigrationJobStage Completion = new MigrationJobStage("Completion", new MigrationJobStatus[]
		{
			MigrationJobStatus.CompletionInitializing,
			MigrationJobStatus.CompletionStarting,
			MigrationJobStatus.Completing
		});

		// Token: 0x0400029D RID: 669
		public static readonly MigrationJobStage Completed = new MigrationJobStage("Completed", new MigrationJobStatus[]
		{
			MigrationJobStatus.Completed,
			MigrationJobStatus.Removing
		});

		// Token: 0x0400029E RID: 670
		public static readonly MigrationJobStage Dormant = new MigrationJobStage("Dormant", new MigrationJobStatus[]
		{
			MigrationJobStatus.Created,
			MigrationJobStatus.Stopped,
			MigrationJobStatus.Failed,
			MigrationJobStatus.Removed
		});

		// Token: 0x0400029F RID: 671
		public static readonly MigrationJobStage Corrupted = new MigrationJobStage("Corrupted", new MigrationJobStatus[]
		{
			MigrationJobStatus.Corrupted
		});

		// Token: 0x040002A0 RID: 672
		private static readonly Dictionary<MigrationJobStatus, MigrationJobStage> stageMap;
	}
}
