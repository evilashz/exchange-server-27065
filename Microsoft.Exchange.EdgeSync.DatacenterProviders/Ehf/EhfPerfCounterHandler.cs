using System;
using Microsoft.Exchange.EdgeSync.Common;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000023 RID: 35
	internal class EhfPerfCounterHandler
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public virtual void OnOperationSuccessfullyCompleted(string operationName, long latency, int batchSize)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.SuccessfulOperationsTotal.Increment();
			instance.LastLatency.RawValue = latency;
			instance.AverageLatency.IncrementBy(latency);
			instance.AverageLatencyBase.Increment();
			instance.LastEntryCount.RawValue = (long)batchSize;
			instance.EntryCountTotal.IncrementBy((long)batchSize);
			if (batchSize > 0)
			{
				instance.AverageLatencyPerEntry.IncrementBy(latency / (long)batchSize);
				instance.AverageLatencyPerEntryBase.Increment();
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C15C File Offset: 0x0000A35C
		public virtual void OnPerEntryFailures(string operationName, int transientFailureCount, int permanentFailureCount)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.TransientEntryFailuresTotal.IncrementBy((long)transientFailureCount);
			instance.PermanentEntryFailuresTotal.IncrementBy((long)permanentFailureCount);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C190 File Offset: 0x0000A390
		public virtual void OnOperationContractViolationFailure(string operationName)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.ContractViolationFailedOperationsTotal.Increment();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public virtual void OnOperationTransientFailure(string operationName)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.TransientFailedOperationsTotal.Increment();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		public virtual void OnOperationTimeoutFailure(string operationName)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.TimeoutFailedOperationsTotal.Increment();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C220 File Offset: 0x0000A420
		public virtual void OnOperationCommunicationFailure(string operationName)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.CommunicationFailedOperationsTotal.Increment();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C250 File Offset: 0x0000A450
		public virtual void OnOperationInvalidCredentialsFailure(string operationName)
		{
			EhfPerfCountersInstance instance = EhfPerfCounterHandler.GetInstance(operationName);
			if (instance == null)
			{
				return;
			}
			instance.OperationsTotal.Increment();
			instance.InvalidCredentialsFailedOperationsTotal.Increment();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C280 File Offset: 0x0000A480
		private static EhfPerfCountersInstance GetInstance(string instanceName)
		{
			EhfPerfCountersInstance result;
			try
			{
				result = EhfPerfCounters.GetInstance(instanceName);
			}
			catch (InvalidOperationException ex)
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfPerfCountersLoadFailure, instanceName, new object[]
				{
					ex
				});
				result = null;
			}
			return result;
		}
	}
}
