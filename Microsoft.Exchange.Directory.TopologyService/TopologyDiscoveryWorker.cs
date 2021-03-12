using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.EventLog;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200001B RID: 27
	internal class TopologyDiscoveryWorker
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00007CCA File Offset: 0x00005ECA
		private TopologyDiscoveryWorker()
		{
			this.cancellationSource = new CancellationTokenSource();
			this.brokers = new ConcurrentDictionary<string, TopologyDiscoveryWorkProvider>(StringComparer.OrdinalIgnoreCase);
			this.waitHandler = new ManualResetEventSlim(false);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public static TopologyDiscoveryWorker Instance
		{
			get
			{
				return TopologyDiscoveryWorker.instance;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public void Start(Action hostCallback)
		{
			ArgumentValidator.ThrowIfNull("hostCallback", hostCallback);
			Task task = Task.Factory.StartNew(delegate()
			{
				this.QueueWork();
			}, this.cancellationSource.Token, TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning, TaskScheduler.Current);
			this.workerTask = task.ContinueWith(delegate(Task t)
			{
				if (t.Exception != null)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Stopping worker due to an unhandled exception {0}", t.Exception.Flatten().ToString());
					this.AnalyzeWorkItemExceptionAndSendWatson(null, t.Exception.Flatten().InnerException, true);
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_WorkerFatalException, null, new object[]
					{
						t.Exception.Flatten().InnerException
					});
				}
				ExTraceGlobals.ServiceTracer.TraceInformation<bool>(this.GetHashCode(), (long)this.GetHashCode(), "Stopping worker. IsCancellationRequested {0}", this.cancellationSource.IsCancellationRequested);
				Task.Factory.StartNew(delegate()
				{
					hostCallback();
				});
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007E64 File Offset: 0x00006064
		public void Stop()
		{
			this.cancellationSource.Cancel();
			try
			{
				this.waitHandler.Set();
				if (this.workerTask != null)
				{
					this.workerTask.Wait();
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyDiscoveryManager - Stop. Exception {0}", arg);
				throw;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007EC8 File Offset: 0x000060C8
		public bool TryRegisterWorkQueue(string id, TopologyDiscoveryWorkProvider workQueue)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNull("workQueue", workQueue);
			if (this.brokers.TryAdd(id, workQueue))
			{
				workQueue.NewWork += this.OnNewWork;
				return true;
			}
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007F04 File Offset: 0x00006104
		public bool TryUnRegisterWorkQueue(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			TopologyDiscoveryWorkProvider topologyDiscoveryWorkProvider;
			if (this.brokers.TryRemove(id, out topologyDiscoveryWorkProvider))
			{
				topologyDiscoveryWorkProvider.NewWork -= this.OnNewWork;
				return true;
			}
			return false;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007F84 File Offset: 0x00006184
		private void QueueWork()
		{
			List<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple> list = new List<TopologyDiscoveryWorkProvider.WorkItemCallBackTuple>(ConfigurationData.Instance.MaxRunningTasks + 10);
			ExTraceGlobals.ServiceTracer.Information<int>((long)this.GetHashCode(), "Max number of scheduled work items {0}", ConfigurationData.Instance.MaxRunningTasks + 10);
			do
			{
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					TopologyDiscoveryWorkProvider.WorkItemCallBackTuple workItemTuple = list[i];
					if (workItemTuple.WorkItem.IsCompleted || workItemTuple.WorkItem.IsOverdue)
					{
						list.RemoveAt(i);
						i--;
						ExTraceGlobals.ServiceTracer.Information<string, string>((long)this.GetHashCode(), "Work Item {0} is {1}", workItemTuple.WorkItem.Id, workItemTuple.WorkItem.IsOverdue ? "Overdue" : "Completed");
						if (workItemTuple.WorkItem.IsOverdue)
						{
							ExTraceGlobals.ServiceTracer.Information<string>((long)this.GetHashCode(), "Cancelling Work Item {0}", workItemTuple.WorkItem.Id);
							workItemTuple.WorkItem.StartCancel((int)ConfigurationData.Instance.WaitAmountBeforeRestartRequest.TotalMilliseconds, delegate(IWorkItemResult result)
							{
								this.CompleteWorkItem(result, workItemTuple.CompletionCallback);
							});
						}
					}
					else if (workItemTuple.WorkItem.IsPending)
					{
						num++;
					}
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2579901757U);
				ExTraceGlobals.ServiceTracer.Information<int>((long)this.GetHashCode(), "Number of pending tasks {0}", num);
				if (this.cancellationSource.IsCancellationRequested)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug<bool>((long)this.GetHashCode(), "IsCancellationRequested {0}", this.cancellationSource.IsCancellationRequested);
					this.waitHandler.Reset();
					this.waitHandler.Wait(ConfigurationData.Instance.ThrottleOnEmptyQueue);
				}
				else
				{
					ExTraceGlobals.ServiceTracer.Information<int>((long)this.GetHashCode(), "Number of scheduledWorkItems {0}", list.Count);
					if (num == 0 && list.Count <= ConfigurationData.Instance.MaxRunningTasks)
					{
						ExTraceGlobals.ServiceTracer.Information((long)this.GetHashCode(), "Scheduling workItems");
						while (list.Count <= ConfigurationData.Instance.MaxRunningTasks + 10)
						{
							bool flag = false;
							foreach (KeyValuePair<string, TopologyDiscoveryWorkProvider> keyValuePair in this.brokers)
							{
								TopologyDiscoveryWorkProvider.WorkItemCallBackTuple itemToEnqueue = keyValuePair.Value.NextWork();
								if (itemToEnqueue != null)
								{
									itemToEnqueue.WorkItem.StartExecuting(delegate(IWorkItemResult result)
									{
										this.CompleteWorkItem(result, itemToEnqueue.CompletionCallback);
									});
									list.Add(itemToEnqueue);
									ExTraceGlobals.ServiceTracer.Information<string>((long)this.GetHashCode(), "Scheduling workItem {0}", itemToEnqueue.WorkItem.Id);
									flag = true;
								}
							}
							if (!flag)
							{
								break;
							}
						}
					}
					TopologyServicePerfCounters.WorkItemCount.RawValue = (long)list.Count;
					TimeSpan timeSpan = (num != 0 || list.Count >= ConfigurationData.Instance.MaxRunningTasks) ? ConfigurationData.Instance.ThrottleOnFullQueue : ConfigurationData.Instance.ThrottleOnEmptyQueue;
					ExTraceGlobals.ServiceTracer.Information<TimeSpan>((long)this.GetHashCode(), "Sleeping thread for {0}", timeSpan);
					this.waitHandler.Reset();
					this.waitHandler.Wait(timeSpan);
				}
			}
			while (list.Count > 0 || !this.cancellationSource.IsCancellationRequested);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000834C File Offset: 0x0000654C
		private void CompleteWorkItem(IWorkItemResult result, Action<IWorkItemResult> callBack)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfNull("callBack", callBack);
			ExTraceGlobals.ServiceTracer.Information<string, ResultType>((long)this.GetHashCode(), "WorkItem completed {0}. Result {1}", result.WorkItemId, result.ResultType);
			TopologyServicePerfCounters.WorkItemsExecuted.Increment();
			switch (result.ResultType)
			{
			case ResultType.Abandoned:
			case ResultType.TimedOut:
				TopologyServicePerfCounters.WorkItemsCancelled.Increment();
				break;
			case ResultType.Succeeded:
				TopologyServicePerfCounters.AverageWorkItemRunTime.IncrementBy((result.WhenCompleted - result.WhenStarted).Ticks);
				TopologyServicePerfCounters.AverageWorkItemRunTimeBase.Increment();
				break;
			case ResultType.Failed:
				TopologyServicePerfCounters.WorkItemsFailures.Increment();
				break;
			}
			try
			{
				callBack(result);
				this.AnalyzeWorkItemExceptionAndSendWatson(result, null, false);
				if (ResultType.Failed == result.ResultType)
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3653643581U);
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2311466301U);
				}
			}
			catch (Exception ex)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_WorkItemUnhandledException, null, new object[]
				{
					ex
				});
				this.AnalyzeWorkItemExceptionAndSendWatson(result, ex, true);
				if (!GrayException.IsGrayException(ex))
				{
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_WorkItemFatalException, null, new object[]
					{
						ex
					});
					this.cancellationSource.Cancel();
				}
			}
			finally
			{
				this.waitHandler.Set();
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000084BC File Offset: 0x000066BC
		private void OnNewWork()
		{
			ExTraceGlobals.ServiceTracer.Information((long)this.GetHashCode(), "New work arrived");
			this.waitHandler.Set();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000084E0 File Offset: 0x000066E0
		private void AnalyzeWorkItemExceptionAndSendWatson(IWorkItemResult result, Exception ex = null, bool forceWatson = false)
		{
			Exception ex2 = ex ?? ((result != null) ? result.Exception : null);
			if (ex2 == null)
			{
				ExTraceGlobals.ServiceTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "TopologyDiscoveryWorker-AnalizeExceptionAndSendWatson. No Exception to analize. {0}", (result != null) ? result.WorkItemId : string.Empty);
				return;
			}
			if (!forceWatson)
			{
				foreach (Type type in TopologyDiscoveryWorker.handledExceptions)
				{
					if (type.IsInstanceOfType(ex2))
					{
						ExTraceGlobals.ServiceTracer.TraceDebug<Exception>((long)this.GetHashCode(), "TopologyDiscoveryWorker-AnalizeExceptionAndSendWatson. Exception is known {0}", ex2);
						return;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (result != null)
			{
				stringBuilder.Append("ResultType:");
				stringBuilder.Append(result.GetType());
				stringBuilder.Append(" .WorkItemResult:");
				stringBuilder.Append(result.ToString());
			}
			if (ex != null)
			{
				stringBuilder.AppendLine(" .Exception:");
				stringBuilder.Append(ex.ToString());
			}
			if (!ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				if (forceWatson || GrayException.IsGrayException(ex2))
				{
					ReportOptions options = ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash | ReportOptions.DoNotFreezeThreads;
					ExWatson.SendReport(ex2, options, stringBuilder.ToString());
				}
				return;
			}
		}

		// Token: 0x04000066 RID: 102
		private const uint UnhandledExceptionInLoop = 2579901757U;

		// Token: 0x04000067 RID: 103
		private const uint UnhandledGrayExceptionOnPublish = 3653643581U;

		// Token: 0x04000068 RID: 104
		private const uint HandledGrayExceptionOnPublish = 2311466301U;

		// Token: 0x04000069 RID: 105
		private static readonly List<Type> handledExceptions = new List<Type>
		{
			typeof(TopologyServiceTransientException),
			typeof(DataValidationException),
			typeof(DataSourceTransientException),
			typeof(DataSourceOperationException)
		};

		// Token: 0x0400006A RID: 106
		private static TopologyDiscoveryWorker instance = new TopologyDiscoveryWorker();

		// Token: 0x0400006B RID: 107
		private CancellationTokenSource cancellationSource;

		// Token: 0x0400006C RID: 108
		private Task workerTask;

		// Token: 0x0400006D RID: 109
		private ConcurrentDictionary<string, TopologyDiscoveryWorkProvider> brokers;

		// Token: 0x0400006E RID: 110
		private ManualResetEventSlim waitHandler;
	}
}
