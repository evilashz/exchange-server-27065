using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200010C RID: 268
	internal class StopMigrationJobProcessor : MigrationJobProcessorBase
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x00039CEE File Offset: 0x00037EEE
		public StopMigrationJobProcessor(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00039DEC File Offset: 0x00037FEC
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return new Func<int?, IEnumerable<StoreObjectId>>[]
				{
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Remove, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Active, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Waiting, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Start, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Stop, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Remove, new MigrationState?(MigrationState.Disabled), maxCount)
				};
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00039E5C File Offset: 0x0003805C
		protected override MigrationProcessorResponse ProcessChild(MigrationJobItem child)
		{
			MigrationJobItemProcessorBase migrationJobItemProcessorBase = null;
			if (child.Flags.HasFlag(MigrationFlags.Remove))
			{
				migrationJobItemProcessorBase = new RemoveMigrationJobItemProcessor(child, this.DataProvider);
			}
			else if (child.Flags.HasFlag(MigrationFlags.Stop))
			{
				migrationJobItemProcessorBase = new StopMigrationJobItemProcessor(child, this.DataProvider);
			}
			if (migrationJobItemProcessorBase != null)
			{
				return migrationJobItemProcessorBase.Process();
			}
			return MigrationJobItemProcessorBase.SetFlags(this.DataProvider, child, (child.Flags | MigrationFlags.Stop) & ~MigrationFlags.Start, MigrationProcessorResult.Working);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00039EDC File Offset: 0x000380DC
		protected override MigrationJobProcessorResponse ProcessObject()
		{
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00039EFC File Offset: 0x000380FC
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed)
			{
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.Stopped, MigrationState.Stopped, new MigrationFlags?(this.MigrationObject.Flags & ~MigrationFlags.Stop), null, null, null, null, null, response.ChildStatusChanges, true, null, response.ProcessingDuration);
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Suspended, null, null, null, null, null);
			}
			return base.ApplyResponse(response);
		}
	}
}
