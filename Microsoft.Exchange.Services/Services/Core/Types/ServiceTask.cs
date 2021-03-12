using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000698 RID: 1688
	internal class ServiceTask<T> : BaseServiceTask<T>
	{
		// Token: 0x060033CE RID: 13262 RVA: 0x000B9272 File Offset: 0x000B7472
		internal ServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult) : base(request, callContext, serviceAsyncResult)
		{
			base.Request.InitializeServiceCommand(callContext);
			OwsLogRegistry.Register(ServiceTask<T>.ServiceTaskActionName, typeof(ServiceTaskMetadata), new Type[0]);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000B936C File Offset: 0x000B756C
		protected internal override TaskExecuteResult InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime)
		{
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.Current;
			double num = -1.0;
			long watsonReportCount = ExWatson.WatsonReportCount;
			AggregatedOperationStatistics s = requestDetailsLogger.ActivityScope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls);
			AggregatedOperationStatistics s2 = requestDetailsLogger.ActivityScope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs);
			TaskExecuteResult taskExecuteResult = TaskExecuteResult.Undefined;
			try
			{
				taskExecuteResult = requestDetailsLogger.TrackLatency<TaskExecuteResult>(ServiceLatencyMetadata.CoreExecutionLatency, delegate()
				{
					T resultData;
					if (this.TryGetResponse(out resultData))
					{
						this.SetResultData(resultData);
						return TaskExecuteResult.ProcessingComplete;
					}
					TaskExecuteResult result;
					try
					{
						TaskExecuteResult taskExecuteResult2 = this.ExecuteHelper(() => base.Request.ServiceCommand.ExecuteStep());
						if (taskExecuteResult2 == TaskExecuteResult.ProcessingComplete)
						{
							this.SetResponse(base.ServiceAsyncResult.Data, base.ServiceAsyncResult.CompletionState as Exception);
						}
						result = taskExecuteResult2;
					}
					catch (FaultException error)
					{
						this.SetResponse(default(T), error);
						throw;
					}
					catch (LocalizedException error2)
					{
						this.SetResponse(default(T), error2);
						throw;
					}
					return result;
				}, out num);
			}
			finally
			{
				AggregatedOperationStatistics aggregatedOperationStatistics = requestDetailsLogger.ActivityScope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls) - s;
				this.totalADCount += aggregatedOperationStatistics.Count;
				this.totalADLatency += aggregatedOperationStatistics.TotalMilliseconds;
				AggregatedOperationStatistics aggregatedOperationStatistics2 = requestDetailsLogger.ActivityScope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs) - s2;
				this.totalRpcCount += aggregatedOperationStatistics2.Count;
				this.totalRpcLatency += aggregatedOperationStatistics2.TotalMilliseconds;
				long num2 = ExWatson.WatsonReportCount - watsonReportCount;
				this.totalWatsonCount += num2;
				if (taskExecuteResult == TaskExecuteResult.ProcessingComplete || taskExecuteResult == TaskExecuteResult.Undefined)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceLatencyMetadata.CoreExecutionLatency, num);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceTaskMetadata.WatsonReportCount, this.totalWatsonCount);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceTaskMetadata.ADCount, this.totalADCount);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceTaskMetadata.ADLatency, this.totalADLatency);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceTaskMetadata.RpcCount, this.totalRpcCount);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(base.CallContext.ProtocolLog, ServiceTaskMetadata.RpcLatency, this.totalRpcLatency);
				}
			}
			return taskExecuteResult;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000B9558 File Offset: 0x000B7758
		private bool TryGetResponse(out T response)
		{
			try
			{
				return base.CallContext.TryGetResponse<T>(out response);
			}
			catch (FaultException executionException)
			{
				this.executionException = executionException;
			}
			catch (LocalizedException executionException2)
			{
				this.executionException = executionException2;
			}
			response = default(T);
			return true;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000B95B0 File Offset: 0x000B77B0
		private void SetResponse(T response, Exception error)
		{
			try
			{
				base.CallContext.SetResponse<T>(response, error);
			}
			catch (FaultException executionException)
			{
				this.executionException = executionException;
			}
			catch (LocalizedException executionException2)
			{
				this.executionException = executionException2;
			}
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000B9624 File Offset: 0x000B7824
		protected internal override TaskExecuteResult InternalCancelStep(LocalizedException exception)
		{
			return this.ExecuteHelper(() => this.Request.ServiceCommand.CancelStep(exception));
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000B9738 File Offset: 0x000B7938
		private TaskExecuteResult ExecuteHelper(Func<TaskExecuteResult> multiStepAction)
		{
			TaskExecuteResult result = TaskExecuteResult.ProcessingComplete;
			base.SendWatsonReportOnGrayException(delegate()
			{
				try
				{
					bool flag = true;
					if (this.initial)
					{
						flag = this.Request.ServiceCommand.PreExecute();
						this.initial = false;
					}
					TaskExecuteResult taskExecuteResult;
					if (!flag)
					{
						taskExecuteResult = TaskExecuteResult.ProcessingComplete;
					}
					else
					{
						taskExecuteResult = multiStepAction();
					}
					if (taskExecuteResult == TaskExecuteResult.ProcessingComplete)
					{
						this.SetResultData((T)((object)this.Request.ServiceCommand.PostExecute()));
					}
					result = taskExecuteResult;
				}
				catch (FaultException ex)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, ex, "ServiceTask_ExecuteHelper");
					this.executionException = ex;
				}
				catch (BailOutException executionException)
				{
					this.executionException = executionException;
				}
			});
			return result;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000B9778 File Offset: 0x000B7978
		protected internal override void InternalComplete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.FinishRequest("[C]", queueAndDelayTime, totalTime, this.executionException);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000B97A6 File Offset: 0x000B79A6
		protected internal override void InternalTimeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.ExecuteHelper(delegate
			{
				base.Request.ServiceCommand.CancelStep(new RequestTimeoutException());
				return TaskExecuteResult.ProcessingComplete;
			});
			this.FinishRequest("[T]", queueAndDelayTime, totalTime, this.executionException);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000B97D0 File Offset: 0x000B79D0
		protected internal override ResourceKey[] InternalGetResources()
		{
			ResourceKey[] resources = base.Request.ServiceCommand.GetResources();
			if (resources == null || resources.Length == 0)
			{
				return ServiceTask<T>.ResourceKeysWithLocalCPUOnly;
			}
			ResourceKey[] array = new ResourceKey[resources.Length + 1];
			array[0] = ProcessorResourceKey.Local;
			Array.Copy(resources, 0, array, 1, resources.Length);
			return array;
		}

		// Token: 0x04001D43 RID: 7491
		private const string CompleteLog = "[C]";

		// Token: 0x04001D44 RID: 7492
		private const string CompleteWithExceptionLog = "[CWE]";

		// Token: 0x04001D45 RID: 7493
		private const string CanceledLog = "[X]";

		// Token: 0x04001D46 RID: 7494
		private const string TimedOutLog = "[T]";

		// Token: 0x04001D47 RID: 7495
		private static readonly string ServiceTaskActionName = "ServiceTask";

		// Token: 0x04001D48 RID: 7496
		private static readonly ResourceKey[] ResourceKeysWithLocalCPUOnly = new ResourceKey[]
		{
			ProcessorResourceKey.Local
		};

		// Token: 0x04001D49 RID: 7497
		private bool initial = true;

		// Token: 0x04001D4A RID: 7498
		private long totalADCount;

		// Token: 0x04001D4B RID: 7499
		private long totalRpcCount;

		// Token: 0x04001D4C RID: 7500
		private long totalWatsonCount;

		// Token: 0x04001D4D RID: 7501
		private double totalADLatency;

		// Token: 0x04001D4E RID: 7502
		private double totalRpcLatency;
	}
}
