using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200010B RID: 267
	internal class StartMigrationJobProcessor : MigrationJobProcessorBase
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00039ABF File Offset: 0x00037CBF
		public StartMigrationJobProcessor(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00039B94 File Offset: 0x00037D94
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return new Func<int?, IEnumerable<StoreObjectId>>[]
				{
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Remove, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Failed, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Stopped, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Stop, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Remove, new MigrationState?(MigrationState.Disabled), maxCount)
				};
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00039BF4 File Offset: 0x00037DF4
		protected override MigrationProcessorResponse ProcessChild(MigrationJobItem child)
		{
			MigrationJobItemProcessorBase migrationJobItemProcessorBase = null;
			if (child.Flags.HasFlag(MigrationFlags.Remove))
			{
				migrationJobItemProcessorBase = new RemoveMigrationJobItemProcessor(child, this.DataProvider);
			}
			if (migrationJobItemProcessorBase != null)
			{
				return migrationJobItemProcessorBase.Process();
			}
			return MigrationJobItemProcessorBase.SetFlags(this.DataProvider, child, (child.Flags | MigrationFlags.Start) & ~MigrationFlags.Stop, MigrationProcessorResult.Completed);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00039C4C File Offset: 0x00037E4C
		protected override MigrationJobProcessorResponse ProcessObject()
		{
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00039C6C File Offset: 0x00037E6C
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed)
			{
				MigrationJob migrationObject = this.MigrationObject;
				IMigrationDataProvider dataProvider = this.DataProvider;
				MigrationJobStatus status = MigrationJobStatus.SyncStarting;
				MigrationState state = MigrationState.Active;
				MigrationStage? stage = new MigrationStage?(MigrationStage.Discovery);
				migrationObject.SetStatus(dataProvider, status, state, new MigrationFlags?(this.MigrationObject.Flags & ~MigrationFlags.Start), stage, null, null, null, null, response.ChildStatusChanges, true, null, response.ProcessingDuration);
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null);
			}
			return base.ApplyResponse(response);
		}
	}
}
