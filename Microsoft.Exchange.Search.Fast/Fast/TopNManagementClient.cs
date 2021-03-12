using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Ceres.ContentEngine.Admin.FlowService;
using Microsoft.Ceres.ContentEngine.Services.ContentIntegrationEngine;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000022 RID: 34
	internal class TopNManagementClient : FastManagementClient
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000C598 File Offset: 0x0000A798
		internal TopNManagementClient(ISearchServiceConfig configuration)
		{
			base.DiagnosticsSession.ComponentName = "TopNManagementClient";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.TopNManagementClientTracer;
			this.flowExecutionTimeout = configuration.TopNFlowExecutionTimeout;
			this.minimumFrequency = configuration.TopNMinimumFrequency.ToString();
			this.isInitialized = 0;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C633 File Offset: 0x0000A833
		public static string TopNCompilationFlowName
		{
			get
			{
				return TopNManagementClient.topNCompilationFlowName;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C63A File Offset: 0x0000A83A
		protected override int ManagementPortOffset
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C63D File Offset: 0x0000A83D
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TopNManagementClient>(this);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C648 File Offset: 0x0000A848
		public ICancelableAsyncResult BeginExecuteFlow(Guid databaseGuid, Guid mailboxGuid, object state, AsyncCallback callback)
		{
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new LazyAsyncResultWithTimeout(new TopNManagementClient.TopNInput
			{
				CorrelationId = Guid.NewGuid(),
				DatabaseGuid = databaseGuid,
				MailboxGuid = mailboxGuid,
				FlowExecutionRequestTime = DateTime.UtcNow
			}, state, callback);
			lock (this.pendingOperationsLock)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("TopNManagementClient", "BeginExecuteFlow");
				}
				if (this.pendingOperationKeys.Add(mailboxGuid))
				{
					if (this.pendingOperationKeys.Count == 1)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteFlow), lazyAsyncResultWithTimeout);
					}
					else
					{
						this.pendingOperations.Enqueue(lazyAsyncResultWithTimeout);
					}
				}
				else
				{
					lazyAsyncResultWithTimeout.Cancel();
				}
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C718 File Offset: 0x0000A918
		public void EndExecuteFlow(IAsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			DateTime utcNow = DateTime.UtcNow;
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = LazyAsyncResult.EndAsyncOperation<LazyAsyncResultWithTimeout>(asyncResult);
			TopNManagementClient.TopNInput topNInput = (TopNManagementClient.TopNInput)lazyAsyncResultWithTimeout.AsyncObject;
			if (lazyAsyncResultWithTimeout.Result == null)
			{
				base.DiagnosticsSession.LogDictionaryInfo(DiagnosticsLoggingTag.Informational, 0, topNInput.CorrelationId, topNInput.DatabaseGuid, topNInput.MailboxGuid, "Dictionary compilation completed successfully. RequestTime: {0}, StartTime: {1}, EndTime: {2}, CorrelationId: {3}.", new object[]
				{
					topNInput.FlowExecutionRequestTime,
					topNInput.FlowExecutionStartTime,
					utcNow,
					topNInput.CorrelationId
				});
				return;
			}
			Exception ex = lazyAsyncResultWithTimeout.Result as Exception;
			if (ex == null)
			{
				return;
			}
			if (ex is OperationCanceledException)
			{
				base.DiagnosticsSession.LogDictionaryInfo(DiagnosticsLoggingTag.Informational, 1, topNInput.CorrelationId, topNInput.DatabaseGuid, topNInput.MailboxGuid, "Dictionary compilation canceled. RequestTime: {0}, StartTime: {1}, EndTime: {2}, CorrelationId: {3}.", new object[]
				{
					topNInput.FlowExecutionRequestTime,
					(topNInput.FlowExecutionStartTime == DateTime.MinValue) ? "NONE" : topNInput.FlowExecutionStartTime.ToString(),
					utcNow,
					topNInput.CorrelationId
				});
				return;
			}
			base.DiagnosticsSession.LogDictionaryInfo(DiagnosticsLoggingTag.Informational, 2, topNInput.CorrelationId, topNInput.DatabaseGuid, topNInput.MailboxGuid, "Dictionary compilation failed. RequestTime: {0}, StartTime: {1}, EndTime: {2}, CorrelationId: {3}, Failure: {4}", new object[]
			{
				topNInput.FlowExecutionRequestTime,
				(topNInput.FlowExecutionStartTime == DateTime.MinValue) ? "NONE" : topNInput.FlowExecutionStartTime.ToString(),
				utcNow,
				topNInput.CorrelationId,
				ex
			});
			if (ex is PerformingFastOperationException)
			{
				throw ex;
			}
			throw new OperationFailedException(ex);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C8FB File Offset: 0x0000AAFB
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.ctsService = client.GetManagementAgent<IContentIntegrationEngineManagementAgent>("ContentTransformation/ContentIntegrationEngine");
			this.flowService = client.GetManagementAgent<IFlowServiceManagementAgent>("ContentTransformation/FlowService");
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C924 File Offset: 0x0000AB24
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.pendingOperationsLock)
				{
					this.disposed = true;
					while (this.pendingOperations.Count > 0)
					{
						LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = this.pendingOperations.Dequeue();
						lazyAsyncResultWithTimeout.Cancel();
					}
					this.pendingOperationKeys.Clear();
					this.stopEvent.Set();
				}
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		private void InitializeIfNecessary()
		{
			if (Interlocked.CompareExchange(ref this.isInitialized, 1, 0) == 0)
			{
				base.ConnectManagementAgents();
				this.EnsureTopNManagementFlow();
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CA74 File Offset: 0x0000AC74
		private void ExecuteFlow(object asyncResult)
		{
			LazyAsyncResultWithTimeout lazyAsyncResult = (LazyAsyncResultWithTimeout)asyncResult;
			if (lazyAsyncResult.IsCompleted)
			{
				this.CompleteProcessing(lazyAsyncResult, null);
				return;
			}
			TopNManagementClient.TopNInput input = (TopNManagementClient.TopNInput)lazyAsyncResult.AsyncObject;
			try
			{
				this.InitializeIfNecessary();
				FlowEvaluationConfig flowConfig = new FlowEvaluationConfig
				{
					FlowProperties = new Dictionary<string, string>(),
					ProfilingMode = 0
				};
				flowConfig.FlowProperties["CorrelationId"] = input.CorrelationId.ToString();
				flowConfig.FlowProperties["MailboxGuid"] = input.MailboxGuid.ToString();
				flowConfig.FlowProperties["DatabaseGuid"] = input.DatabaseGuid.ToString();
				flowConfig.FlowProperties["IndexSystemName"] = FastIndexVersion.GetIndexSystemName(input.DatabaseGuid);
				flowConfig.FlowProperties["MinimumFrequency"] = this.minimumFrequency;
				base.PerformFastOperation(delegate()
				{
					input.FlowExecutionStartTime = DateTime.UtcNow;
					input.Evaluations = this.ctsService.ExecuteFlow(TopNManagementClient.topNCompilationFlowName, flowConfig);
					RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(this.stopEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.WaitForFlowExecutionToComplete)), lazyAsyncResult, TopNManagementClient.flowExecutionRetryInterval, true);
				}, "ExecuteFlow");
			}
			catch (PerformingFastOperationException failure)
			{
				Interlocked.Exchange(ref this.isInitialized, 0);
				this.CompleteProcessing(lazyAsyncResult, failure);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CC88 File Offset: 0x0000AE88
		private void WaitForFlowExecutionToComplete(object asyncResult, bool timerFired)
		{
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (LazyAsyncResultWithTimeout)asyncResult;
			TopNManagementClient.TopNInput input = (TopNManagementClient.TopNInput)lazyAsyncResultWithTimeout.AsyncObject;
			if (timerFired)
			{
				try
				{
					if (!this.PerformFastOperation<bool>(delegate()
					{
						input.Evaluations = this.ctsService.Refresh(input.Evaluations.Identifier);
						return input.Evaluations.HasActiveFlow;
					}, "Refresh the flow evaluation status"))
					{
						using (IEnumerator<FlowExecutionInfo> enumerator = input.Evaluations.Evaluations.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								FlowExecutionInfo flowExecutionInfo = enumerator.Current;
								if (flowExecutionInfo.State == 6 && flowExecutionInfo.FailCause != null)
								{
									this.CompleteProcessing(lazyAsyncResultWithTimeout, flowExecutionInfo.FailCause);
									return;
								}
							}
						}
						this.CompleteProcessing(lazyAsyncResultWithTimeout, null);
						return;
					}
					if (DateTime.UtcNow.Subtract(input.FlowExecutionStartTime) > this.flowExecutionTimeout)
					{
						base.PerformFastOperation(delegate()
						{
							this.ctsService.AbortFlow(input.Evaluations.Identifier);
						}, "Abort the current flow execution.");
						this.CompleteProcessing(lazyAsyncResultWithTimeout, new TimeoutException(string.Format("Execution of the TopN Compilation flow took longer than the allowed timeout of {0}.", this.flowExecutionTimeout)));
						return;
					}
					ThreadPool.RegisterWaitForSingleObject(this.stopEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.WaitForFlowExecutionToComplete)), lazyAsyncResultWithTimeout, TopNManagementClient.flowExecutionRetryInterval, true);
					return;
				}
				catch (PerformingFastOperationException failure)
				{
					Interlocked.Exchange(ref this.isInitialized, 0);
					this.CompleteProcessing(lazyAsyncResultWithTimeout, failure);
					return;
				}
			}
			this.ContinueProcessing(input.MailboxGuid);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CE40 File Offset: 0x0000B040
		private void CompleteProcessing(LazyAsyncResultWithTimeout asyncResult, Exception failure)
		{
			TopNManagementClient.TopNInput topNInput = (TopNManagementClient.TopNInput)asyncResult.AsyncObject;
			asyncResult.InvokeCallback(failure);
			this.ContinueProcessing(topNInput.MailboxGuid);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CE70 File Offset: 0x0000B070
		private void ContinueProcessing(Guid previousGuid)
		{
			lock (this.pendingOperationsLock)
			{
				this.pendingOperationKeys.Remove(previousGuid);
				if (this.pendingOperationKeys.Count > 0)
				{
					LazyAsyncResultWithTimeout state = this.pendingOperations.Dequeue();
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteFlow), state);
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CF18 File Offset: 0x0000B118
		private void EnsureTopNManagementFlow()
		{
			bool flag = false;
			IList<string> list = this.PerformFastOperation<IList<string>>(() => this.flowService.GetFlows(), "GetFlows");
			using (IEnumerator<string> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string flowName = enumerator.Current;
					bool flag2;
					if (this.MatchTopNFlowName(flowName, out flag2))
					{
						if (flag2)
						{
							base.PerformFastOperation(delegate()
							{
								this.flowService.DeleteFlow(flowName);
							}, "RemoveTopNFlow");
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				this.AddTopNCompilationFlow();
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		private void AddTopNCompilationFlow()
		{
			string flowXml = this.GetFlowXmlFromResource("TopNCompilationFlow.xml");
			base.PerformFastOperation(delegate()
			{
				this.flowService.PutFlow(TopNManagementClient.topNCompilationFlowName, flowXml);
			}, "Add the TopN Compilation flow");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D03A File Offset: 0x0000B23A
		private bool MatchTopNFlowName(string flowName, out bool isOldFlow)
		{
			isOldFlow = false;
			if (!flowName.StartsWith("Microsoft.Exchange.TopNCompilation"))
			{
				return false;
			}
			if (flowName != TopNManagementClient.topNCompilationFlowName)
			{
				isOldFlow = true;
			}
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000D060 File Offset: 0x0000B260
		private string GetFlowXmlFromResource(string resourceXmlName)
		{
			string result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceXmlName))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream, Encoding.UTF8))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x040000E2 RID: 226
		private const string TopNCompilationFlowNameFormat = "{0}.{1}";

		// Token: 0x040000E3 RID: 227
		private const string TopNCompilationFlowNamePrefix = "Microsoft.Exchange.TopNCompilation";

		// Token: 0x040000E4 RID: 228
		private const int TopNCompilationFlowCurrentVersion = 1;

		// Token: 0x040000E5 RID: 229
		private static readonly string topNCompilationFlowName = string.Format("{0}.{1}", "Microsoft.Exchange.TopNCompilation", 1);

		// Token: 0x040000E6 RID: 230
		private static readonly TimeSpan flowExecutionRetryInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000E7 RID: 231
		private readonly TimeSpan flowExecutionTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x040000E8 RID: 232
		private readonly string minimumFrequency;

		// Token: 0x040000E9 RID: 233
		private readonly Queue<LazyAsyncResultWithTimeout> pendingOperations = new Queue<LazyAsyncResultWithTimeout>();

		// Token: 0x040000EA RID: 234
		private readonly HashSet<Guid> pendingOperationKeys = new HashSet<Guid>();

		// Token: 0x040000EB RID: 235
		private readonly object pendingOperationsLock = new object();

		// Token: 0x040000EC RID: 236
		private volatile IContentIntegrationEngineManagementAgent ctsService;

		// Token: 0x040000ED RID: 237
		private volatile IFlowServiceManagementAgent flowService;

		// Token: 0x040000EE RID: 238
		private int isInitialized;

		// Token: 0x040000EF RID: 239
		private volatile bool disposed;

		// Token: 0x040000F0 RID: 240
		private ManualResetEvent stopEvent = new ManualResetEvent(false);

		// Token: 0x02000023 RID: 35
		private class TopNInput
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000200 RID: 512 RVA: 0x0000D0F3 File Offset: 0x0000B2F3
			// (set) Token: 0x06000201 RID: 513 RVA: 0x0000D0FB File Offset: 0x0000B2FB
			public Guid CorrelationId { get; set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000202 RID: 514 RVA: 0x0000D104 File Offset: 0x0000B304
			// (set) Token: 0x06000203 RID: 515 RVA: 0x0000D10C File Offset: 0x0000B30C
			public Guid MailboxGuid { get; set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000204 RID: 516 RVA: 0x0000D115 File Offset: 0x0000B315
			// (set) Token: 0x06000205 RID: 517 RVA: 0x0000D11D File Offset: 0x0000B31D
			public Guid DatabaseGuid { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000206 RID: 518 RVA: 0x0000D126 File Offset: 0x0000B326
			// (set) Token: 0x06000207 RID: 519 RVA: 0x0000D12E File Offset: 0x0000B32E
			public FlowEvaluations Evaluations { get; set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000208 RID: 520 RVA: 0x0000D137 File Offset: 0x0000B337
			// (set) Token: 0x06000209 RID: 521 RVA: 0x0000D13F File Offset: 0x0000B33F
			public DateTime FlowExecutionRequestTime { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x0600020A RID: 522 RVA: 0x0000D148 File Offset: 0x0000B348
			// (set) Token: 0x0600020B RID: 523 RVA: 0x0000D150 File Offset: 0x0000B350
			public DateTime FlowExecutionStartTime { get; set; }
		}
	}
}
