using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000695 RID: 1685
	internal abstract class BaseServiceTask<T> : ITask
	{
		// Token: 0x0600339E RID: 13214 RVA: 0x000B8A14 File Offset: 0x000B6C14
		internal BaseServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult)
		{
			this.Request = request;
			this.CallContext = callContext;
			this.ServiceAsyncResult = serviceAsyncResult;
			this.WorkloadSettings = new WorkloadSettings(callContext.WorkloadType, callContext.BackgroundLoad);
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000B8A48 File Offset: 0x000B6C48
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x000B8A50 File Offset: 0x000B6C50
		public object State { get; set; }

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000B8A59 File Offset: 0x000B6C59
		// (set) Token: 0x060033A2 RID: 13218 RVA: 0x000B8A61 File Offset: 0x000B6C61
		public string Description { get; set; }

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000B8A6A File Offset: 0x000B6C6A
		// (set) Token: 0x060033A4 RID: 13220 RVA: 0x000B8A72 File Offset: 0x000B6C72
		internal CallContext CallContext { get; private set; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000B8A7B File Offset: 0x000B6C7B
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x000B8A83 File Offset: 0x000B6C83
		internal BaseRequest Request { get; private set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000B8A8C File Offset: 0x000B6C8C
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x000B8A94 File Offset: 0x000B6C94
		internal ServiceAsyncResult<T> ServiceAsyncResult { get; private set; }

		// Token: 0x060033A9 RID: 13225 RVA: 0x000B8A9D File Offset: 0x000B6C9D
		public void Cancel()
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[BaseServiceTask.Cancel] Task.Cancel called for task {0}", this.Description);
			this.InternalCancel();
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000B8AC4 File Offset: 0x000B6CC4
		public IActivityScope GetActivityScope()
		{
			IActivityScope result = null;
			if (this.CallContext != null && this.CallContext.ProtocolLog != null)
			{
				result = this.CallContext.ProtocolLog.ActivityScope;
			}
			return result;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000B8CA0 File Offset: 0x000B6EA0
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			TaskExecuteResult result = TaskExecuteResult.ProcessingComplete;
			this.ExecuteWithinCallContext(delegate
			{
				RequestDetailsLogger.LogEvent(this.CallContext.ProtocolLog, ServiceTaskMetadata.ServiceCommandBegin);
				using (CpuTracker.StartCpuTracking("CMD"))
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					Guid relatedActivityId;
					if (currentActivityScope != null)
					{
						if (string.IsNullOrEmpty(currentActivityScope.Component) && this.CallContext != null)
						{
							currentActivityScope.Component = this.CallContext.WorkloadType.ToString();
						}
						if (string.IsNullOrEmpty(currentActivityScope.Action) && this.Request.ServiceCommand != null)
						{
							currentActivityScope.Action = this.Request.ServiceCommand.GetType().Name;
						}
						relatedActivityId = currentActivityScope.ActivityId;
					}
					else
					{
						relatedActivityId = Guid.NewGuid();
					}
					using (ExPerfTrace.RelatedActivity(relatedActivityId))
					{
						((IEwsBudget)this.Budget).StartPerformanceContext();
						try
						{
							result = this.InternalExecute(queueAndDelayTime, totalTime);
						}
						finally
						{
							((IEwsBudget)this.Budget).StopPerformanceContext();
						}
					}
					RequestDetailsLogger.LogEvent(this.CallContext.ProtocolLog, ServiceTaskMetadata.ServiceCommandEnd);
				}
			});
			return result;
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000B8CE7 File Offset: 0x000B6EE7
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000B8CEF File Offset: 0x000B6EEF
		public WorkloadSettings WorkloadSettings { get; private set; }

		// Token: 0x060033AE RID: 13230 RVA: 0x000B8CF8 File Offset: 0x000B6EF8
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[BaseServiceTask.Complete] Complete with no exception called for task {0}.  Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			this.InternalComplete(queueAndDelayTime, totalTime);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000B8D20 File Offset: 0x000B6F20
		private void ExecuteWithinCallContext(Action action)
		{
			CallContext current = CallContext.Current;
			try
			{
				CallContext.SetCurrent(this.CallContext);
				if (ExchangeVersion.Current == null)
				{
					ExchangeVersion.Current = ExchangeVersion.Latest;
				}
				bool flag = false;
				if (this.CallContext.AccessingPrincipal != null && ExUserTracingAdaptor.Instance.IsTracingEnabledUser(this.CallContext.AccessingPrincipal.LegacyDn))
				{
					flag = true;
					BaseTrace.CurrentThreadSettings.EnableTracing();
				}
				try
				{
					action();
				}
				finally
				{
					if (flag)
					{
						BaseTrace.CurrentThreadSettings.DisableTracing();
					}
				}
			}
			finally
			{
				CallContext.SetCurrent(current);
			}
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000B8DC8 File Offset: 0x000B6FC8
		protected void CompleteWCFRequest(Exception exception)
		{
			if (exception != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, exception, "BaseServiceTask_CompleteWCFRequest");
			}
			if (!this.ServiceAsyncResult.IsCompleted)
			{
				this.ServiceAsyncResult.Complete(exception);
				return;
			}
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[BaseServiceTask::CompleteWCFRequest] WCF request was already complete on another worker thread.  Exception: {0}", (exception == null) ? "<NULL>" : exception.ToString());
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000B8E30 File Offset: 0x000B7030
		protected virtual void FinishRequest(string logType, TimeSpan queueAndDelayTime, TimeSpan totalTime, Exception exception)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.CallContext.ProtocolLog, BudgetMetadata.ThrottlingDelay, queueAndDelayTime.TotalMilliseconds);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.CallContext.ProtocolLog, BudgetMetadata.ThrottlingRequestType, logType);
			if (exception != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, exception, "BaseServiceTask_FinishRequest");
			}
			this.WriteThrottlingDiagnostics(logType, queueAndDelayTime, totalTime);
			this.CompleteWCFRequest(exception);
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000B8EA2 File Offset: 0x000B70A2
		public virtual IBudget Budget
		{
			get
			{
				return this.CallContext.Budget;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000B8EB0 File Offset: 0x000B70B0
		public virtual TimeSpan MaxExecutionTime
		{
			get
			{
				if (this.Request != null && this.Request.ServiceCommand != null && this.Request.ServiceCommand.MaxExecutionTime != null && this.Request.ServiceCommand.MaxExecutionTime != null)
				{
					return this.Request.ServiceCommand.MaxExecutionTime.Value;
				}
				return BaseServiceTask<T>.DefaultMaxExecutionTime;
			}
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000B8F24 File Offset: 0x000B7124
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[BaseServiceTask.Timeout] Timeout called for task {0}.  Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			try
			{
				CallContext.SetCurrent(this.CallContext);
				this.InternalTimeout(queueAndDelayTime, totalTime);
			}
			finally
			{
				CallContext.SetCurrent(null);
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000B8F7C File Offset: 0x000B717C
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			TaskExecuteResult result;
			try
			{
				CallContext.SetCurrent(this.CallContext);
				if (exception != null)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, exception, "BaseServiceTask_Cancel");
				}
				result = this.InternalCancelStep(exception);
			}
			finally
			{
				CallContext.SetCurrent(null);
			}
			return result;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000B8FEC File Offset: 0x000B71EC
		public ResourceKey[] GetResources()
		{
			ResourceKey[] resourceKeys = null;
			try
			{
				CallContext.SetCurrent(this.CallContext);
				this.SendWatsonReportOnGrayException(delegate()
				{
					resourceKeys = this.InternalGetResources();
				}, null, false);
			}
			finally
			{
				CallContext.SetCurrent(null);
			}
			return resourceKeys;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000B9054 File Offset: 0x000B7254
		protected virtual void SetResultData(T response)
		{
			this.ServiceAsyncResult.Data = response;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000B9062 File Offset: 0x000B7262
		protected internal virtual void InternalCancel()
		{
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000B9064 File Offset: 0x000B7264
		protected string GetTimesLogString(string logType, TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			return string.Format("{0}Queues:{1}msec/Execute:{2}msec;", logType, queueAndDelayTime.TotalMilliseconds, (totalTime == TimeSpan.Zero) ? 0.0 : (totalTime - queueAndDelayTime).TotalMilliseconds);
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000B90B4 File Offset: 0x000B72B4
		protected void SendWatsonReportOnGrayException(BaseServiceTask<T>.GrayExceptionCallback callback)
		{
			this.SendWatsonReportOnGrayException(callback, null, true);
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000B90BF File Offset: 0x000B72BF
		protected void SendWatsonReportOnGrayException(BaseServiceTask<T>.GrayExceptionCallback callback, BaseServiceTask<T>.GrayExceptionHandlerCallback exceptionHandlerCallback)
		{
			this.SendWatsonReportOnGrayException(callback, exceptionHandlerCallback, true);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000B90E0 File Offset: 0x000B72E0
		private void SendWatsonReportOnGrayException(BaseServiceTask<T>.GrayExceptionCallback callback, BaseServiceTask<T>.GrayExceptionHandlerCallback exceptionHandlerCallback, bool isGrayExceptionTaskFailure)
		{
			Exception ex = null;
			string formatString = null;
			ServiceDiagnostics.RegisterAdditionalWatsonData();
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					callback();
				}, new GrayException.ExceptionFilterDelegate(BaseServiceTask<T>.GrayExceptionFilter));
			}
			catch (GrayException ex2)
			{
				ex = ex2;
				if (isGrayExceptionTaskFailure)
				{
					formatString = "Task {0} failed: {1}";
					if (this.Budget != null)
					{
						this.IncrementFailedCount();
					}
					this.executionException = ex2;
					if (ex2 != null)
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.CallContext.ProtocolLog, ex2, "BaseServiceTask_SendWatsonReportOnGrayException");
					}
					if (exceptionHandlerCallback != null)
					{
						exceptionHandlerCallback();
					}
				}
				else
				{
					formatString = "Task {0} ignored exception: {1}";
				}
			}
			finally
			{
				ExWatson.ClearReportActions(WatsonActionScope.Thread);
			}
			if (ex != null)
			{
				ExTraceGlobals.ThrottlingTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), formatString, this.Description, ex);
			}
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000B91BC File Offset: 0x000B73BC
		protected static bool GrayExceptionFilter(object exception)
		{
			bool flag = false;
			Exception ex = exception as Exception;
			if (ex != null && ExWatson.IsWatsonReportAlreadySent(ex))
			{
				flag = true;
			}
			bool flag2 = GrayException.ExceptionFilter(exception);
			if (flag2 && !flag && ex != null)
			{
				ExWatson.SetWatsonReportAlreadySent(ex);
			}
			return flag2;
		}

		// Token: 0x060033BE RID: 13246
		protected internal abstract TaskExecuteResult InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime);

		// Token: 0x060033BF RID: 13247
		protected internal abstract void InternalComplete(TimeSpan queueAndDelayTime, TimeSpan totalTime);

		// Token: 0x060033C0 RID: 13248
		protected internal abstract void InternalTimeout(TimeSpan queueAndDelayTime, TimeSpan totalTime);

		// Token: 0x060033C1 RID: 13249
		protected internal abstract TaskExecuteResult InternalCancelStep(LocalizedException exception);

		// Token: 0x060033C2 RID: 13250
		protected internal abstract ResourceKey[] InternalGetResources();

		// Token: 0x060033C3 RID: 13251 RVA: 0x000B91F8 File Offset: 0x000B73F8
		protected virtual void WriteThrottlingDiagnostics(string logType, TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			if (Global.WriteThrottlingDiagnostics)
			{
				string timesLogString = this.GetTimesLogString(logType, queueAndDelayTime, totalTime);
				this.CallContext.HttpContext.Response.AppendHeader("X-ThrottlingDiagnostics", timesLogString + this.Budget.ToString());
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000B9241 File Offset: 0x000B7441
		private void IncrementFailedCount()
		{
			UserWorkloadManager.GetPerfCounterWrapper(this.Budget.Owner.BudgetType).UpdateTotalTaskExecutionFailuresCount();
		}

		// Token: 0x04001D38 RID: 7480
		private const string WlmQueueReSubmitKey = "WlmQueueReSubmitTime";

		// Token: 0x04001D39 RID: 7481
		private const string WlmQueueSubmitKey = "WlmQueueSubmitTime";

		// Token: 0x04001D3A RID: 7482
		private const string WlmLatencyKey = "WlmQueueingLatency";

		// Token: 0x04001D3B RID: 7483
		private static readonly TimeSpan DefaultMaxExecutionTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x04001D3C RID: 7484
		protected Exception executionException;

		// Token: 0x02000696 RID: 1686
		// (Invoke) Token: 0x060033C7 RID: 13255
		internal delegate void GrayExceptionCallback();

		// Token: 0x02000697 RID: 1687
		// (Invoke) Token: 0x060033CB RID: 13259
		internal delegate void GrayExceptionHandlerCallback();
	}
}
