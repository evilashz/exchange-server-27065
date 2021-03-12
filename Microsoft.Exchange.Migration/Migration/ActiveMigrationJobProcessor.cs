using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000106 RID: 262
	internal class ActiveMigrationJobProcessor : MigrationJobProcessorBase
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x00038473 File Offset: 0x00036673
		public ActiveMigrationJobProcessor(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000384F8 File Offset: 0x000366F8
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return new Func<int?, IEnumerable<StoreObjectId>>[]
				{
					(int? maxCount) => MigrationJobItem.GetIdsWithFlagPresence(this.DataProvider, this.MigrationObject, true, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Active, null, maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByState(this.DataProvider, this.MigrationObject, MigrationState.Waiting, new ExDateTime?(ExDateTime.UtcNow), maxCount),
					(int? maxCount) => MigrationJobItem.GetIdsByFlag(this.DataProvider, this.MigrationObject, MigrationFlags.Remove, new MigrationState?(MigrationState.Disabled), maxCount)
				};
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00038549 File Offset: 0x00036749
		protected override IEnumerable<StoreObjectId> GetChildObjectIds(Func<int?, IEnumerable<StoreObjectId>>[] queries, int? maxCount = null)
		{
			if (this.MigrationObject.Flags == MigrationFlags.None && this.MigrationObject.Stage != MigrationStage.Processing)
			{
				return Enumerable.Empty<StoreObjectId>();
			}
			return base.GetChildObjectIds(queries, maxCount);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00038578 File Offset: 0x00036778
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
			else if (child.Flags.HasFlag(MigrationFlags.Start))
			{
				migrationJobItemProcessorBase = new StartMigrationJobItemProcessor(child, this.DataProvider);
			}
			else if (child.Flags == MigrationFlags.None && (child.State == MigrationState.Active || (child.State == MigrationState.Waiting && child.NextProcessTime <= ExDateTime.UtcNow)))
			{
				migrationJobItemProcessorBase = new ActiveMigrationJobItemProcessor(child, this.DataProvider);
			}
			if (migrationJobItemProcessorBase != null)
			{
				return migrationJobItemProcessorBase.Process();
			}
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(TimeSpan.MaxValue), null, null, null, null);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003866C File Offset: 0x0003686C
		protected override MigrationJobProcessorResponse ProcessObject()
		{
			MigrationStage stage = this.MigrationObject.Stage;
			if (stage != MigrationStage.Discovery)
			{
				if (stage == MigrationStage.Injection)
				{
					return this.Injection();
				}
				if (stage != MigrationStage.Processing)
				{
					return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
				}
				return this.Processing();
			}
			else
			{
				if (!this.MigrationObject.IsStaged)
				{
					MigrationProcessorResult result = MigrationProcessorResult.Completed;
					string batchInputId = Guid.NewGuid().ToString();
					return MigrationJobProcessorResponse.Create(result, null, null, "", batchInputId, null);
				}
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00038708 File Offset: 0x00036908
		protected override MigrationJobProcessorResponse HandlePermanentException(LocalizedException ex)
		{
			MigrationJobProcessorResponse migrationJobProcessorResponse = base.HandlePermanentException(ex);
			if (this.cursorPosition != null)
			{
				migrationJobProcessorResponse.LastProcessedRow = this.cursorPosition;
			}
			return migrationJobProcessorResponse;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00038734 File Offset: 0x00036934
		protected override MigrationJobProcessorResponse HandleTransientException(LocalizedException ex)
		{
			MigrationJobProcessorResponse migrationJobProcessorResponse = base.HandleTransientException(ex);
			if (this.cursorPosition != null)
			{
				migrationJobProcessorResponse.LastProcessedRow = this.cursorPosition;
			}
			return migrationJobProcessorResponse;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00038760 File Offset: 0x00036960
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			if (response.Result != MigrationProcessorResult.Completed)
			{
				if (this.MigrationObject.Stage == MigrationStage.Processing && (response.Result == MigrationProcessorResult.Waiting || response.Result == MigrationProcessorResult.Working) && this.MigrationObject.ActiveInitialItemCount == 0 && this.MigrationObject.StartingItemCount == 0 && (this.MigrationObject.BatchFlags & MigrationBatchFlags.ReportInitial) == MigrationBatchFlags.ReportInitial && this.MigrationObject.StartTime != null)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob {0} adding report flag to generate a report", new object[]
					{
						this.MigrationObject
					});
					this.MigrationObject.SetStatus(this.DataProvider, this.MigrationObject.Status, this.MigrationObject.State, new MigrationFlags?(this.MigrationObject.Flags | MigrationFlags.Report), null, null, null, null, null, null, true, new MigrationBatchFlags?(this.MigrationObject.BatchFlags & ~MigrationBatchFlags.ReportInitial), response.ProcessingDuration);
				}
				return base.ApplyResponse(response);
			}
			MigrationStage? stage = null;
			MigrationStage stage2 = this.MigrationObject.Stage;
			if (stage2 != MigrationStage.Discovery)
			{
				if (stage2 != MigrationStage.Validation)
				{
					if (stage2 == MigrationStage.Injection)
					{
						stage = new MigrationStage?(MigrationStage.Processing);
					}
				}
				else
				{
					stage = new MigrationStage?(MigrationStage.Injection);
				}
			}
			else
			{
				stage = new MigrationStage?(MigrationStage.Validation);
			}
			if (stage != null)
			{
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.SyncStarting, MigrationState.Active, null, stage, null, null, response.LastProcessedRow, response.BatchInputId, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null);
			}
			if (this.MigrationObject.ActiveItemCount + this.MigrationObject.SyncedItemCount > 0)
			{
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.SyncStarting, MigrationState.Active, null, null, null, null, response.LastProcessedRow, response.BatchInputId, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
			}
			else
			{
				this.MigrationObject.SetStatus(this.DataProvider, MigrationJobStatus.Completed, MigrationState.Completed, null, null, null, null, response.LastProcessedRow, response.BatchInputId, response.ChildStatusChanges, response.ClearPoison, null, response.ProcessingDuration);
			}
			return response;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00038A04 File Offset: 0x00036C04
		private MigrationJobProcessorResponse Injection()
		{
			if (!this.MigrationObject.ShouldProcessDataRows || this.MigrationObject.IsDataRowProcessingDone())
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob {0} has no more rows to read, so injection is finished", new object[]
				{
					this.MigrationObject
				});
				return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
			}
			bool flag = false;
			MigrationProcessorResult result = MigrationProcessorResult.Working;
			MigrationCountCache.MigrationStatusChange migrationStatusChange = MigrationCountCache.MigrationStatusChange.None;
			int num = 0;
			IMigrationDataRow migrationDataRow = null;
			try
			{
				int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaxRowsToProcessInOnePass");
				ExDateTime t = ExDateTime.UtcNow + ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MaxTimeToProcessInOnePass");
				IMigrationDataRowProvider migrationDataRowProvider = MigrationServiceFactory.Instance.GetMigrationDataRowProvider(this.MigrationObject, this.DataProvider);
				foreach (IMigrationDataRow migrationDataRow2 in migrationDataRowProvider.GetNextBatchItem(this.MigrationObject.LastCursorPosition, config))
				{
					MigrationJobItem migrationJobItem = this.ProcessDataRow(migrationDataRow2);
					if (migrationJobItem != null)
					{
						migrationStatusChange += MigrationCountCache.MigrationStatusChange.CreateInject(migrationJobItem.Status);
					}
					migrationDataRow = migrationDataRow2;
					num++;
					if (num >= config || ExDateTime.UtcNow >= t)
					{
						break;
					}
				}
				if (migrationDataRow == null)
				{
					flag = true;
				}
			}
			finally
			{
				if (flag)
				{
					result = MigrationProcessorResult.Completed;
					this.cursorPosition = "EOF";
				}
				else
				{
					this.cursorPosition = ((migrationDataRow == null) ? null : migrationDataRow.CursorPosition.ToString(CultureInfo.InvariantCulture));
				}
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJob {0} injected {1} rows in this pass", new object[]
				{
					this.MigrationObject,
					num
				});
			}
			return MigrationJobProcessorResponse.Create(result, null, null, this.cursorPosition, null, migrationStatusChange);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00038BBC File Offset: 0x00036DBC
		private MigrationJobProcessorResponse Processing()
		{
			if (this.MigrationObject.ShouldLazyRescan)
			{
				this.MigrationObject.UpdateCachedItemCounts(this.DataProvider);
			}
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00038C18 File Offset: 0x00036E18
		private MigrationJobItem ProcessDataRow(IMigrationDataRow dataRow)
		{
			if (MigrationJobItem.GetByIdentifier(this.DataProvider, this.MigrationObject, dataRow.Identifier, null).Any((MigrationJobItem jobItem) => !this.MigrationObject.BatchInputId.Equals(jobItem.BatchInputId)))
			{
				return null;
			}
			MigrationWorkflowPosition initialPosition = this.MigrationObject.Workflow.GetInitialPosition();
			InvalidDataRow invalidDataRow = dataRow as InvalidDataRow;
			if (invalidDataRow != null)
			{
				return MigrationJobItem.CreateFailed(this.DataProvider, this.MigrationObject, invalidDataRow, new MigrationState?(MigrationState.Disabled), initialPosition);
			}
			return MigrationJobItem.Create(this.DataProvider, this.MigrationObject, dataRow, MigrationUserStatus.Validating, new MigrationState?(MigrationState.Active), initialPosition);
		}

		// Token: 0x040004EC RID: 1260
		private string cursorPosition;
	}
}
