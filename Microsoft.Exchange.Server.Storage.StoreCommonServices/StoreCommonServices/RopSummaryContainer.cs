using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000121 RID: 289
	public sealed class RopSummaryContainer : TraceContainer<RopSummaryAggregator, RopTraceKey, RopSummaryParameters>
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x00039343 File Offset: 0x00037543
		public RopSummaryContainer()
		{
			this.traceInterval = ConfigurationSchema.TraceIntervalRopSummary.Value;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0003935B File Offset: 0x0003755B
		internal override TimeSpan TraceInterval
		{
			get
			{
				return this.traceInterval;
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00039363 File Offset: 0x00037563
		internal override RopSummaryAggregator CreateEmptyAggregator()
		{
			return new RopSummaryAggregator();
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003936A File Offset: 0x0003756A
		internal override RopSummaryAggregator UpdateAggregator(RopSummaryAggregator aggregator, RopSummaryParameters parameters)
		{
			aggregator.Add(parameters);
			return aggregator;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00039374 File Offset: 0x00037574
		internal override void WriteTrace(StoreDatabase database, IBinaryLogger logger)
		{
			Task<RopSummaryReporter>.TaskCallback callback = TaskExecutionWrapper<RopSummaryReporter>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.RopSummaryCollection, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<RopSummaryReporter>.TaskCallback<Context>(this.WriteTrace));
			SingleExecutionTask<RopSummaryReporter>.CreateSingleExecutionTask(Storage.TaskList, callback, new RopSummaryReporter(database, logger, this), true);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000393B8 File Offset: 0x000375B8
		private static WorkLoadType GetWorkLoadType(byte client)
		{
			if (client <= 20)
			{
				if (client != 10)
				{
					switch (client)
					{
					case 18:
					case 20:
						break;
					case 19:
						return WorkLoadType.NonDiscretionary;
					default:
						return WorkLoadType.NonDiscretionary;
					}
				}
			}
			else if (client != 29 && client != 32)
			{
				return WorkLoadType.NonDiscretionary;
			}
			return WorkLoadType.Discretionary;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000393F8 File Offset: 0x000375F8
		private void WriteTrace(Context context, RopSummaryReporter reporter, Func<bool> shouldCallbackContinue)
		{
			if (reporter.Logger == null || reporter.Logger.IsDisposed || !reporter.Logger.IsLoggingEnabled || !reporter.DataContainer.HasDataToLog || !shouldCallbackContinue())
			{
				return;
			}
			int hashCode = reporter.Database.MdbGuid.GetHashCode();
			uint contextFlags = (uint)RopSummaryReporter.GetContextFlags(reporter.Database);
			SyntheticCounters syntheticCounters = SyntheticCounters.Create();
			foreach (KeyValuePair<RopTraceKey, RopSummaryAggregator> keyValuePair in reporter.DataContainer.Data)
			{
				if (!shouldCallbackContinue())
				{
					break;
				}
				byte operationType = (byte)keyValuePair.Key.OperationType;
				int mailboxNumber = keyValuePair.Key.MailboxNumber;
				byte clientType = (byte)keyValuePair.Key.ClientType;
				uint activityId = keyValuePair.Key.ActivityId;
				byte operationId = keyValuePair.Key.OperationId;
				uint detailId = keyValuePair.Key.DetailId;
				bool sharedLock = keyValuePair.Key.SharedLock;
				RopSummaryAggregator value = keyValuePair.Value;
				double num = value.TotalCpuTimeKernel.TotalMilliseconds + value.TotalCpuTimeUser.TotalMilliseconds;
				WorkLoadType workLoadType = RopSummaryContainer.GetWorkLoadType(clientType);
				if (num > 0.0)
				{
					syntheticCounters.Add(keyValuePair.Key.OperationType, num);
					syntheticCounters.Add(keyValuePair.Key.ClientType, num);
					syntheticCounters.Add(keyValuePair.Key.ActivityId, num);
					syntheticCounters.Add(keyValuePair.Key.OperationType, keyValuePair.Key.OperationId, num);
					syntheticCounters.Add(workLoadType, num);
				}
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.RopSummary, false, false, 100, contextFlags, operationType, hashCode, mailboxNumber, clientType, activityId, operationId, detailId, sharedLock, value.TotalCalls, value.NumberOfCallsSlow, (uint)value.MaximumElapsedTime.TotalMilliseconds, value.NumberOfCallsInError, value.LastKnownError, (uint)value.TotalTime.TotalMilliseconds, value.NumberOfActivities, value.TotalLogBytes, value.TotalPagesPreread, value.TotalPagesRead, value.TotalPagesDirtied, value.TotalPagesRedirtied, value.TotalJetReservedAlpha, value.TotalJetReservedBeta, value.TotalDirectoryOperations, value.TotalOffPageHits, (uint)value.TotalCpuTimeKernel.TotalMilliseconds, (uint)value.TotalCpuTimeUser.TotalMilliseconds, value.TotalChunks, (uint)value.MaximumChunkTime.TotalMilliseconds, (uint)value.TotalLockWaitTime.TotalMilliseconds, (uint)value.TotalDirectoryWaitTime.TotalMilliseconds, (uint)value.TotalDatabaseTime.TotalMilliseconds, (uint)value.TotalFastWaitTime.TotalMilliseconds, value.TotalUndefinedAlpha, value.TotalUndefinedBeta, value.TotalUndefinedGamma, value.TotalUndefinedDelta, value.TotalUndefinedOmega))
				{
					reporter.Logger.TryWrite(traceBuffer);
				}
			}
			syntheticCounters.WriteTrace();
		}

		// Token: 0x04000645 RID: 1605
		private readonly TimeSpan traceInterval;
	}
}
