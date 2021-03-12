using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Completion;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationScheduler
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004298 File Offset: 0x00002498
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000042A0 File Offset: 0x000024A0
		private ExDateTime? LastTimeWorkStarted
		{
			get
			{
				return this.lastTimeWorkStarted;
			}
			set
			{
				this.lastTimeWorkStarted = value;
				this.checkLastTimeWorkStarted = true;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000042B0 File Offset: 0x000024B0
		internal AggregationScheduler(SyncLogSession syncLogSession) : this(AggregationConfiguration.Instance.MaximumPendingWorkItems, syncLogSession)
		{
			SyncStoreLoadManager.Create(syncLogSession);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000042CC File Offset: 0x000024CC
		internal AggregationScheduler(int maximumPendingWorkItems, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.maximumPendingWorkItems = maximumPendingWorkItems;
			this.WorkItemDropped += PerfCounterHandler.Instance.OnWorkItemDropped;
			this.WorkItemSubmitted += PerfCounterHandler.Instance.OnWorkItemSubmitted;
			this.WorkItemAggregated += PerfCounterHandler.Instance.OnWorkItemAggregated;
			this.RetryQueueLengthChanged += PerfCounterHandler.Instance.OnRetryQueueLengthChanged;
			this.pendingWorkList = new Dictionary<Guid, AggregationScheduler.PendingWork>(maximumPendingWorkItems);
			this.state = AggregationScheduler.RunState.Initializing;
			this.syncLogSession = syncLogSession;
			this.healthCheckTimer = new GuardedTimer(new TimerCallback(this.Worker), null, -1, -1);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000083 RID: 131 RVA: 0x0000438C File Offset: 0x0000258C
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x000043C4 File Offset: 0x000025C4
		private event EventHandler<EventArgs> WorkItemDropped;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000085 RID: 133 RVA: 0x000043FC File Offset: 0x000025FC
		// (remove) Token: 0x06000086 RID: 134 RVA: 0x00004434 File Offset: 0x00002634
		private event EventHandler<EventArgs> WorkItemSubmitted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000087 RID: 135 RVA: 0x0000446C File Offset: 0x0000266C
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x000044A4 File Offset: 0x000026A4
		private event EventHandler<EventArgs> WorkItemAggregated;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000089 RID: 137 RVA: 0x000044DC File Offset: 0x000026DC
		// (remove) Token: 0x0600008A RID: 138 RVA: 0x00004514 File Offset: 0x00002714
		private event EventHandler<RetryableWorkQueueEventArgs> RetryQueueLengthChanged;

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004549 File Offset: 0x00002749
		protected SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004551 File Offset: 0x00002751
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000455B File Offset: 0x0000275B
		internal AggregationSubscriptionType EnabledAggregationTypes
		{
			get
			{
				return this.enabledAggregationTypes;
			}
			set
			{
				this.enabledAggregationTypes = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004568 File Offset: 0x00002768
		internal string CurrentState
		{
			get
			{
				int pendingWorkListCount = 0;
				int num = 0;
				lock (this.workListLock)
				{
					if (this.state != AggregationScheduler.RunState.Unload)
					{
						pendingWorkListCount = this.pendingWorkList.Count;
						num = this.retryItemsCount;
					}
				}
				return AggregationScheduler.GetCurrentState(pendingWorkListCount, num);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000045CC File Offset: 0x000027CC
		internal static string GetCurrentState(int pendingWorkListCount, int retryItemsCount)
		{
			XElement xelement = new XElement("Scheduler");
			AggregationScheduler.GetQueueCounts(xelement, pendingWorkListCount, retryItemsCount);
			return xelement.ToString(SaveOptions.DisableFormatting);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000045F8 File Offset: 0x000027F8
		protected virtual void TriggerHealthCheckAfter(TimeSpan dueTime)
		{
			this.healthCheckTimer.Change((int)dueTime.TotalMilliseconds, -1);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004610 File Offset: 0x00002810
		internal XElement GetDiagnosticInfo(bool verbose)
		{
			XElement xelement = new XElement("Scheduler");
			xelement.Add(new XElement("lastTimeWorkItemSubmitAttemptTime", (this.lastSubmitAttempt != null) ? this.lastSubmitAttempt.Value.ToString("o") : string.Empty));
			xelement.Add(new XElement("lastTimeWorkItemStartTime", (this.LastTimeWorkStarted != null) ? this.LastTimeWorkStarted.Value.ToString("o") : string.Empty));
			XElement xelement2 = new XElement("Counts");
			AggregationScheduler.GetQueueCounts(xelement2, this.pendingWorkList.Count, this.retryItemsCount);
			xelement.Add(xelement2);
			if (verbose)
			{
				XElement xelement3 = new XElement("WorkItems");
				lock (this.workListLock)
				{
					foreach (AggregationScheduler.PendingWork pendingWork in this.pendingWorkList.Values)
					{
						if (pendingWork != null)
						{
							TimeSpan timeSpan = ExDateTime.UtcNow - pendingWork.WorkItemStartTime;
							XElement diagnosticInfo = pendingWork.WorkItem.GetDiagnosticInfo();
							diagnosticInfo.Add(new XElement("status", "InProgress"));
							diagnosticInfo.Add(new XElement("timeInQueue", timeSpan.ToString()));
							xelement3.Add(diagnosticInfo);
						}
					}
				}
				xelement.Add(xelement3);
			}
			return xelement;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000047E0 File Offset: 0x000029E0
		internal virtual void NotifyMailboxOfCompletion(AggregationWorkItem item, SubscriptionCompletionStatus itemStatus)
		{
			SubscriptionCompletionClient.NotifyCompletion(item, itemStatus);
			item.SyncLogSession.LogVerbose((TSLID)1328UL, ExTraceGlobals.SchedulerTracer, "Notify mailbox of completion. Status: {0}", new object[]
			{
				itemStatus
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004828 File Offset: 0x00002A28
		internal SubscriptionSubmissionResult SubmitWorkItem(AggregationWorkItem workItem)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			this.lastSubmitAttempt = new ExDateTime?(ExDateTime.UtcNow);
			workItem.SyncLogSession.LogVerbose((TSLID)448UL, ExTraceGlobals.SchedulerTracer, "Trying to submit WorkItem. Scheduler State: {0}", new object[]
			{
				this.CurrentState
			});
			if (!this.CanAggregate(workItem.SubscriptionType))
			{
				workItem.SyncLogSession.LogError((TSLID)450UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not accepted as it's aggregation type is disabled.", new object[0]);
				return SubscriptionSubmissionResult.SubscriptionTypeDisabled;
			}
			lock (this.workListLock)
			{
				if (this.state != AggregationScheduler.RunState.Running && (this.state != AggregationScheduler.RunState.RunningInBackPressure || workItem.AggregationType == AggregationType.Aggregation))
				{
					workItem.SyncLogSession.LogError((TSLID)449UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not accepted as Scheduler is shutting down.", new object[0]);
					return SubscriptionSubmissionResult.EdgeTransportStopped;
				}
				if (this.pendingWorkList.ContainsKey(workItem.SubscriptionId))
				{
					workItem.SyncLogSession.LogError((TSLID)451UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not accepted as subscription is already being processed.", new object[0]);
					return SubscriptionSubmissionResult.SubscriptionAlreadyOnHub;
				}
				if (workItem.CurrentRetryCount > 0)
				{
					throw new ArgumentOutOfRangeException("item", "Item.CurrentRetryCount should be 0.");
				}
				SubscriptionSubmissionResult subscriptionSubmissionResult;
				if (!this.TryStartWork(workItem, out subscriptionSubmissionResult))
				{
					workItem.SyncLogSession.LogError((TSLID)452UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not accepted: {0}", new object[]
					{
						subscriptionSubmissionResult
					});
					return subscriptionSubmissionResult;
				}
			}
			workItem = null;
			if (this.WorkItemSubmitted != null)
			{
				this.WorkItemSubmitted(this, null);
			}
			return SubscriptionSubmissionResult.Success;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000049EC File Offset: 0x00002BEC
		internal void Start(bool initialPausedState)
		{
			this.syncLogSession.LogDebugging((TSLID)453UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.Start", new object[0]);
			if (this.state == AggregationScheduler.RunState.Unload)
			{
				throw new InvalidOperationException("AggregationComponent cannot be started after unloaded.");
			}
			AggregationScheduler.RunState runState = initialPausedState ? AggregationScheduler.RunState.RunningInBackPressure : AggregationScheduler.RunState.Running;
			if (this.state != runState)
			{
				if (this.state != AggregationScheduler.RunState.Running && this.state != AggregationScheduler.RunState.RunningInBackPressure)
				{
					this.TriggerHealthCheckAfter(AggregationConfiguration.Instance.HealthCheckRetryInterval);
				}
				this.state = runState;
				SyncStoreLoadManager.StartExecution();
				this.syncLogSession.LogDebugging((TSLID)454UL, ExTraceGlobals.SchedulerTracer, "Starting Scheduler.", new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004AA4 File Offset: 0x00002CA4
		internal void Stop()
		{
			this.syncLogSession.LogDebugging((TSLID)455UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.Stop", new object[0]);
			if (this.state == AggregationScheduler.RunState.Unload)
			{
				throw new InvalidOperationException("AggregationComponent cannot be stopped after unloaded.");
			}
			if (this.state == AggregationScheduler.RunState.Shutdown)
			{
				return;
			}
			this.state = AggregationScheduler.RunState.Shutdown;
			SyncStoreLoadManager.StopExecution();
			this.syncLogSession.LogVerbose((TSLID)456UL, ExTraceGlobals.SchedulerTracer, "Stopping Scheduler. Scheduler State: {0}.", new object[]
			{
				this.CurrentState
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004B38 File Offset: 0x00002D38
		internal void Pause()
		{
			this.syncLogSession.LogDebugging((TSLID)326UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.Pause", new object[0]);
			if (this.state == AggregationScheduler.RunState.Unload)
			{
				throw new InvalidOperationException("AggregationComponent cannot be paused after unloaded.");
			}
			if (this.state == AggregationScheduler.RunState.RunningInBackPressure)
			{
				return;
			}
			this.state = AggregationScheduler.RunState.RunningInBackPressure;
			this.syncLogSession.LogVerbose((TSLID)327UL, ExTraceGlobals.SchedulerTracer, "Paused the Scheduler. Scheduler State: {0}.", new object[]
			{
				this.CurrentState
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004BC8 File Offset: 0x00002DC8
		internal void Unload()
		{
			this.syncLogSession.LogDebugging((TSLID)328UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.Unload", new object[0]);
			if (this.state == AggregationScheduler.RunState.Unload)
			{
				return;
			}
			this.state = AggregationScheduler.RunState.Unload;
			this.healthCheckTimer.Dispose(false);
			this.healthCheckTimer = null;
			this.CancelPendingWork();
			SyncStoreLoadManager.StopExecution();
			this.syncLogSession.LogVerbose((TSLID)457UL, ExTraceGlobals.SchedulerTracer, "Scheduler.Unload finished", new object[0]);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004C54 File Offset: 0x00002E54
		private static void GetQueueCounts(XElement parentElement, int pendingWorkListCount, int retryItemsCount)
		{
			parentElement.Add(new object[]
			{
				new XElement("inProgress", pendingWorkListCount),
				new XElement("retryItemsCount", retryItemsCount)
			});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004CA0 File Offset: 0x00002EA0
		private void CancelPendingWork()
		{
			this.syncLogSession.LogDebugging((TSLID)458UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.CancelPendingWork", new object[0]);
			lock (this.workListLock)
			{
				if (this.pendingWorkList.Count > 0)
				{
					this.pendingWorkListEmpty = new ManualResetEvent(false);
					AggregationScheduler.PendingWork[] array = new AggregationScheduler.PendingWork[this.pendingWorkList.Count];
					this.pendingWorkList.Values.CopyTo(array, 0);
					foreach (AggregationScheduler.PendingWork pendingWork in array)
					{
						if (pendingWork != null && !pendingWork.AsyncResult.IsCompleted)
						{
							pendingWork.WorkItem.Cancel(pendingWork.AsyncResult);
						}
					}
				}
			}
			if (this.pendingWorkListEmpty != null)
			{
				this.syncLogSession.LogVerbose((TSLID)459UL, ExTraceGlobals.SchedulerTracer, "Waiting for pending work items to finish.", new object[0]);
				this.pendingWorkListEmpty.WaitOne();
				this.syncLogSession.LogVerbose((TSLID)460UL, ExTraceGlobals.SchedulerTracer, "Pending work items finished.", new object[0]);
				this.pendingWorkListEmpty.Close();
				this.pendingWorkListEmpty = null;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004DEC File Offset: 0x00002FEC
		private bool CanAggregate(AggregationSubscriptionType subscriptionType)
		{
			return (this.enabledAggregationTypes & subscriptionType) != AggregationSubscriptionType.Unknown || this.enabledAggregationTypes == subscriptionType;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E08 File Offset: 0x00003008
		private void TryStartWorkItem(AggregationWorkItem item)
		{
			item.SyncLogSession.LogDebugging((TSLID)465UL, ExTraceGlobals.SchedulerTracer, (long)item.GetHashCode(), "Calling BeginProcess on WorkItem.", new object[0]);
			ThreadPool.QueueUserWorkItem(AsyncResult<object, object>.GetWaitCallbackWithClearPoisonContext(new WaitCallback(this.StartProcessingWorker)), item);
			item = null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004E60 File Offset: 0x00003060
		private bool TryAcceptWorkItem(AggregationWorkItem item, out SubscriptionSubmissionResult result)
		{
			result = SubscriptionSubmissionResult.Success;
			if (this.pendingWorkList.Count >= this.maximumPendingWorkItems)
			{
				this.syncLogSession.LogVerbose((TSLID)1329UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: Cannot accept WI. Maximum number of pending requests ({0}) reached", new object[]
				{
					this.maximumPendingWorkItems
				});
				result = SubscriptionSubmissionResult.SchedulerQueueFull;
				return false;
			}
			return SyncStoreLoadManager.Instance.TryAcceptWorkItem(item, out result);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004ED0 File Offset: 0x000030D0
		private bool TryStartWork(AggregationWorkItem item, out SubscriptionSubmissionResult result)
		{
			result = SubscriptionSubmissionResult.Success;
			if (!this.TryAcceptWorkItem(item, out result))
			{
				return false;
			}
			Guid subscriptionId = item.SubscriptionId;
			this.pendingWorkList[subscriptionId] = null;
			this.TryStartWorkItem(item);
			this.LastTimeWorkStarted = new ExDateTime?(ExDateTime.UtcNow);
			this.syncLogSession.LogVerbose((TSLID)466UL, ExTraceGlobals.SchedulerTracer, "{0}<scheduled>{1}</scheduled>", new object[]
			{
				this.CurrentState,
				subscriptionId
			});
			return true;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004F54 File Offset: 0x00003154
		private void StartProcessingWorker(object state)
		{
			AggregationWorkItem aggregationWorkItem = (AggregationWorkItem)state;
			aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)467UL, ExTraceGlobals.SchedulerTracer, "AggregationScheduler.StartProcessingWorker.", new object[0]);
			Guid subscriptionId = aggregationWorkItem.SubscriptionId;
			IExecutionEngine executionEngine = aggregationWorkItem.GetExecutionEngine();
			IAsyncResult asyncResult = executionEngine.BeginExecution(aggregationWorkItem, new AsyncCallback(this.OnWorkItemCompleted), aggregationWorkItem);
			lock (this.workListLock)
			{
				if (!this.pendingWorkList.ContainsKey(subscriptionId))
				{
					this.syncLogSession.LogDebugging((TSLID)468UL, ExTraceGlobals.SchedulerTracer, "We didn't had to add the PendingWork to the work list as the work item had already finished.", new object[0]);
				}
				else
				{
					this.pendingWorkList[subscriptionId] = new AggregationScheduler.PendingWork(aggregationWorkItem, asyncResult, ExDateTime.UtcNow);
					this.syncLogSession.LogVerbose((TSLID)469UL, ExTraceGlobals.SchedulerTracer, "Added work with GUID: {0} to pendingWorkList. ActiveCount: {1}. RetryCount: {2}", new object[]
					{
						subscriptionId,
						this.pendingWorkList.Count,
						this.retryItemsCount
					});
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005088 File Offset: 0x00003288
		private void OnWorkItemCompleted(IAsyncResult asyncResult)
		{
			AggregationWorkItem aggregationWorkItem = (AggregationWorkItem)asyncResult.AsyncState;
			aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)470UL, ExTraceGlobals.SchedulerTracer, "WorkItem completed: {0}.", new object[]
			{
				aggregationWorkItem
			});
			IExecutionEngine executionEngine = aggregationWorkItem.GetExecutionEngine();
			aggregationWorkItem.LastWorkItemResultData = executionEngine.EndExecution(asyncResult);
			aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)471UL, ExTraceGlobals.SchedulerTracer, "Called EndProcess on {0} with result {1}.", new object[]
			{
				aggregationWorkItem,
				aggregationWorkItem.LastWorkItemResultData
			});
			SubscriptionCompletionStatus subscriptionCompletionStatus = SubscriptionCompletionStatus.NoError;
			if (this.state == AggregationScheduler.RunState.Shutdown || this.state == AggregationScheduler.RunState.Unload)
			{
				aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)472UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not processed any further as we're shutting down: {0}.", new object[]
				{
					aggregationWorkItem
				});
				subscriptionCompletionStatus = SubscriptionCompletionStatus.HubShutdown;
			}
			else
			{
				aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)473UL, ExTraceGlobals.SchedulerTracer, "WorkItem hasn't expired and we haven't shutdown: {0}.", new object[]
				{
					aggregationWorkItem
				});
				if (aggregationWorkItem.LastWorkItemResultData.IsSucceeded)
				{
					aggregationWorkItem.SyncLogSession.LogDebugging((TSLID)474UL, ExTraceGlobals.SchedulerTracer, "WorkItem completed successfully: {0}.", new object[]
					{
						aggregationWorkItem
					});
				}
				else if (this.ShouldRetryWorkItem(aggregationWorkItem))
				{
					aggregationWorkItem.SyncLogSession.LogVerbose((TSLID)481UL, ExTraceGlobals.SchedulerTracer, "Enqueuing the work item in retry queue: {0}.", new object[]
					{
						aggregationWorkItem
					});
					if (this.TrySubmitWorkItemForRetry(aggregationWorkItem))
					{
						return;
					}
					subscriptionCompletionStatus = SubscriptionCompletionStatus.HubShutdown;
				}
				else
				{
					subscriptionCompletionStatus = SubscriptionCompletionStatus.SyncError;
				}
			}
			this.ProcessWorkItemSessionCompleted(aggregationWorkItem, subscriptionCompletionStatus);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005228 File Offset: 0x00003428
		private bool ShouldRetryWorkItem(AggregationWorkItem item)
		{
			bool isRetryable = item.LastWorkItemResultData.IsRetryable;
			bool flag = item.LastWorkItemResultData.Exception.InnerException is MailboxInSiteFailoverException;
			if (!isRetryable && !flag)
			{
				item.SyncLogSession.LogError((TSLID)477UL, ExTraceGlobals.SchedulerTracer, "WorkItem has hit a permanent error: {0} with {1}.", new object[]
				{
					item,
					item.LastWorkItemResultData
				});
				return false;
			}
			item.SyncLogSession.LogVerbose((TSLID)475UL, ExTraceGlobals.SchedulerTracer, "WorkItem has hit a transient error: {0} with {1}.", new object[]
			{
				item,
				item.LastWorkItemResultData
			});
			if (item.IsMaximumNumberOfAttemptsReached)
			{
				item.SyncLogSession.LogInformation((TSLID)476UL, ExTraceGlobals.SchedulerTracer, "WorkItem has exhausted max tries: {0}.", new object[]
				{
					item
				});
				return false;
			}
			if (flag)
			{
				item.ResetSyncEngineState();
			}
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005310 File Offset: 0x00003510
		private void ProcessWorkItemSessionCompleted(AggregationWorkItem item, SubscriptionCompletionStatus subscriptionCompletionStatus)
		{
			item.SyncLogSession.LogInformation((TSLID)482UL, ExTraceGlobals.SchedulerTracer, "The work item is not being processed anymore and will be disposed off: {0}, lifetime: {1}.", new object[]
			{
				item,
				item.Lifetime
			});
			try
			{
				try
				{
					this.LogHealthEvent(item);
					this.RecordItemPercentTimeInStore(item);
					EventHandler<EventArgs> eventHandler = (item.LastWorkItemResultData != null && item.LastWorkItemResultData.IsSucceeded) ? this.WorkItemAggregated : this.WorkItemDropped;
					if (eventHandler != null)
					{
						eventHandler(this, null);
					}
				}
				finally
				{
					lock (this.workListLock)
					{
						if (item.CurrentRetryCount >= 1)
						{
							this.retryItemsCount--;
							if (this.RetryQueueLengthChanged != null)
							{
								this.RetryQueueLengthChanged(this, RetryableWorkQueueEventArgs.DecrementByOneEventArgs);
							}
						}
					}
					SyncStoreLoadManager.Instance.RemoveWorkItem(item);
					bool aggregationInitial = item.AggregationType == AggregationType.Aggregation && item.InitialSync;
					this.RemovePendingSubscription(item.SubscriptionId, aggregationInitial);
				}
				this.NotifyMailboxOfCompletion(item, subscriptionCompletionStatus);
			}
			finally
			{
				item.Dispose();
				item = null;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000544C File Offset: 0x0000364C
		internal bool TrySubmitWorkItemForRetry(AggregationWorkItem item)
		{
			item.SyncLogSession.LogDebugging((TSLID)331UL, ExTraceGlobals.SchedulerTracer, "SubmitWorkItemForRetry: {0}.", new object[]
			{
				item
			});
			if (item.IsMaximumNumberOfAttemptsReached)
			{
				throw new ArgumentOutOfRangeException("item", "Item.IsMaximumRetryReached should be false.");
			}
			if (this.state != AggregationScheduler.RunState.Running)
			{
				item.SyncLogSession.LogVerbose((TSLID)1301UL, ExTraceGlobals.SchedulerTracer, "WorkItem was not re-enqueued as we entered state: {0}.", new object[]
				{
					this.state
				});
				return false;
			}
			if (SyncStoreLoadManager.Sleep(item.UpdateRetryStateOnRetry()))
			{
				return false;
			}
			item.SyncLogSession.LogVerbose((TSLID)480UL, ExTraceGlobals.SchedulerTracer, "Work item is being submitted for retry.", new object[0]);
			this.TryStartWorkItem(item);
			lock (this.workListLock)
			{
				if (item.CurrentRetryCount == 1)
				{
					this.retryItemsCount++;
					if (this.RetryQueueLengthChanged != null)
					{
						this.RetryQueueLengthChanged(this, RetryableWorkQueueEventArgs.IncrementByOneEventArgs);
					}
				}
			}
			return true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000557C File Offset: 0x0000377C
		private void RemovePendingSubscription(Guid subscriptionId, bool aggregationInitial)
		{
			lock (this.workListLock)
			{
				this.pendingWorkList.Remove(subscriptionId);
				this.syncLogSession.LogVerbose((TSLID)483UL, ExTraceGlobals.SchedulerTracer, "Subscription with GUID {0} removed from pendingWorkList. ActiveCount: {1} RetryCount: {2}", new object[]
				{
					subscriptionId,
					this.pendingWorkList.Count,
					this.retryItemsCount
				});
				if (this.pendingWorkList.Count == 0 && this.pendingWorkListEmpty != null)
				{
					this.pendingWorkListEmpty.Set();
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005638 File Offset: 0x00003838
		private void RecordItemPercentTimeInStore(AggregationWorkItem workItem)
		{
			double totalSeconds = workItem.Lifetime.TotalSeconds;
			double num = (double)workItem.SyncHealthData.TotalSuccessfulRemoteRoundtrips * workItem.SyncHealthData.AverageSuccessfulRemoteRoundtripTime.TotalMilliseconds + (double)workItem.SyncHealthData.TotalUnsuccessfulRemoteRoundtrips * workItem.SyncHealthData.AverageUnsuccessfulRemoteRoundtripTime.TotalMilliseconds;
			double num2 = num / 1000.0;
			double num3 = (double)(workItem.SyncHealthData.TotalSuccessfulNativeRoundtrips + workItem.SyncHealthData.TotalUnsuccessfulNativeRoundtrips) * workItem.SyncHealthData.AverageNativeBackoffTime.TotalMilliseconds + (double)(workItem.SyncHealthData.TotalSuccessfulEngineRoundtrips + workItem.SyncHealthData.TotalUnsuccessfulEngineRoundtrips) * workItem.SyncHealthData.AverageEngineBackoffTime.TotalMilliseconds;
			double num4 = num3 / 1000.0;
			int num5 = workItem.SyncHealthData.TotalSuccessfulRemoteRoundtrips + workItem.SyncHealthData.TotalUnsuccessfulRemoteRoundtrips;
			int num6 = workItem.SyncHealthData.TotalSuccessfulNativeRoundtrips + workItem.SyncHealthData.TotalUnsuccessfulNativeRoundtrips + workItem.SyncHealthData.TotalSuccessfulEngineRoundtrips + workItem.SyncHealthData.TotalUnsuccessfulEngineRoundtrips;
			if (num6 == 0 || num5 == 0 || totalSeconds <= 0.0 || totalSeconds < num2 + num4)
			{
				this.syncLogSession.LogError((TSLID)1331UL, ExTraceGlobals.SchedulerTracer, "Invalid sync data for {0}: WILT:{1}, RT:{2}, BT:{3}", new object[]
				{
					workItem.SubscriptionId,
					totalSeconds,
					num2,
					num4
				});
				return;
			}
			double num7 = totalSeconds - (num2 + num4);
			float storeLatency = (float)(num7 * 1000.0 / (double)num6);
			float cloudLatency = (float)(num / (double)num5);
			float storeCloudRatio = (float)(num6 / num5);
			SyncStoreLoadManager.RecordPercentTimeInStore(storeLatency, cloudLatency, storeCloudRatio);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005804 File Offset: 0x00003A04
		private void LogHealthEvent(AggregationWorkItem workItem)
		{
			this.syncLogSession.LogDebugging((TSLID)485UL, ExTraceGlobals.SchedulerTracer, "Logging Sync Health Event for work Item: {0}.", new object[]
			{
				workItem
			});
			if (workItem.LastWorkItemResultData == null || workItem.LastWorkItemResultData.Data == null)
			{
				this.syncLogSession.LogError((TSLID)486UL, ExTraceGlobals.SchedulerTracer, "LastWorkItemResultData not present, skipping the sync health log entry.", new object[0]);
				return;
			}
			if (workItem.IsProcessedBySyncEngine())
			{
				this.LogSyncHealthEvent(workItem);
				return;
			}
			this.LogDeletionHealthEvent(workItem);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005890 File Offset: 0x00003A90
		private void LogSyncHealthEvent(AggregationWorkItem workItem)
		{
			ISyncWorkerData updatedSubscription = workItem.LastWorkItemResultData.Data.UpdatedSubscription;
			SyncPhase syncPhaseBeforeSync = workItem.LastWorkItemResultData.Data.SyncPhaseBeforeSync;
			if (updatedSubscription == null)
			{
				this.syncLogSession.LogError((TSLID)1032UL, ExTraceGlobals.SchedulerTracer, "Updated Subscription not found in the work item, skipping the sync health log entry.", new object[0]);
				return;
			}
			if (updatedSubscription.SubscriptionGuid.Equals(Guid.Empty))
			{
				this.syncLogSession.LogError((TSLID)487UL, ExTraceGlobals.SchedulerTracer, "Subscription Guid not found in the subscription ({0}), skipping the sync health log entry.", new object[]
				{
					updatedSubscription
				});
				return;
			}
			string machineName = Environment.MachineName;
			string userMailboxGuid = string.Empty;
			if (!workItem.UserMailboxGuid.Equals(Guid.Empty))
			{
				userMailboxGuid = workItem.UserMailboxGuid.ToString();
			}
			else if (workItem.LegacyDN != null)
			{
				userMailboxGuid = workItem.LegacyDN;
			}
			string text;
			if (updatedSubscription.IncomingServerName != null)
			{
				text = updatedSubscription.IncomingServerName;
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "IncomingServerName:{0}:{1}", new object[]
				{
					updatedSubscription.Status,
					updatedSubscription.DetailedAggregationStatus
				});
				this.syncLogSession.LogInformation((TSLID)488UL, ExTraceGlobals.SchedulerTracer, "Incoming Server Name not found for Subscription {0}, assiging the default one: {1}.", new object[]
				{
					updatedSubscription,
					text
				});
			}
			AggregationConfiguration.Instance.SyncHealthLog.LogSync(machineName, workItem.DatabaseGuid.ToString(), userMailboxGuid, updatedSubscription.SubscriptionGuid.ToString(), workItem.TenantGuid.ToString(), text, updatedSubscription.Domain, updatedSubscription.SubscriptionType.ToString(), updatedSubscription.AggregationType.ToString(), workItem.SyncHealthData.SyncDuration, updatedSubscription.Status.ToString(), updatedSubscription.DetailedAggregationStatus.ToString(), workItem.SyncHealthData.Exceptions, workItem.SyncHealthData.SyncEngineException, workItem.SyncHealthData.TotalItemAddsEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalItemAddsAppliedToLocalServer, workItem.SyncHealthData.TotalItemChangesEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalItemChangesAppliedToLocalServer, workItem.SyncHealthData.TotalItemDeletesEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalItemDeletesAppliedToLocalServer, workItem.SyncHealthData.TotalFolderAddsEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalFolderAddsAppliedToLocalServer, workItem.SyncHealthData.TotalFolderChangesEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalFolderChangesAppliedToLocalServer, workItem.SyncHealthData.TotalFolderDeletesEnumeratedFromRemoteServer, workItem.SyncHealthData.TotalFolderDeletesAppliedToLocalServer, workItem.SyncHealthData.TotalBytesEnumeratedFromRemoteServer, workItem.SyncHealthData.IsPermanentSyncError, workItem.SyncHealthData.IsTransientSyncError, workItem.SyncHealthData.OverSizeItemErrorsCount, workItem.SyncHealthData.PoisonItemErrorsCount, workItem.SyncHealthData.UnresolveableFolderNameErrorsCount, workItem.SyncHealthData.ObjectNotFoundErrorsCount, workItem.SyncHealthData.OtherItemErrorsCount, workItem.SyncHealthData.PermanentItemErrorsCount, workItem.SyncHealthData.TransientItemErrorsCount, workItem.SyncHealthData.PermanentFolderErrorsCount, workItem.SyncHealthData.TransientFolderErrorsCount, workItem.SyncHealthData.TotalItemAddsPermanentExceptions, workItem.SyncHealthData.TotalItemAddsTransientExceptions, workItem.SyncHealthData.TotalItemDeletesPermanentExceptions, workItem.SyncHealthData.TotalItemDeletesTransientExceptions, workItem.SyncHealthData.TotalItemChangesPermanentExceptions, workItem.SyncHealthData.TotalItemChangesTransientExceptions, workItem.SyncHealthData.TotalFolderAddsPermanentExceptions, workItem.SyncHealthData.TotalFolderAddsTransientExceptions, workItem.SyncHealthData.TotalFolderDeletesPermanentExceptions, workItem.SyncHealthData.TotalFolderDeletesTransientExceptions, workItem.SyncHealthData.TotalFolderChangesPermanentExceptions, workItem.SyncHealthData.TotalFolderChangesTransientExceptions, workItem.SyncHealthData.TotalSuccessfulRemoteRoundtrips, workItem.SyncHealthData.AverageSuccessfulRemoteRoundtripTime, workItem.SyncHealthData.TotalUnsuccessfulRemoteRoundtrips, workItem.SyncHealthData.AverageUnsuccessfulRemoteRoundtripTime, workItem.SyncHealthData.TotalSuccessfulNativeRoundtrips, workItem.SyncHealthData.AverageSuccessfulNativeRoundtripTime, workItem.SyncHealthData.TotalUnsuccessfulNativeRoundtrips, workItem.SyncHealthData.AverageUnsuccessfulNativeRoundtripTime, workItem.SyncHealthData.AverageNativeBackoffTime, workItem.SyncHealthData.TotalSuccessfulEngineRoundtrips, workItem.SyncHealthData.AverageSuccessfulEngineRoundtripTime, workItem.SyncHealthData.TotalUnsuccessfulEngineRoundtrips, workItem.SyncHealthData.AverageUnsuccessfulEngineRoundtripTime, workItem.SyncHealthData.AverageEngineBackoffTime, workItem.SyncHealthData.CloudStatistics.TotalItemsInSourceMailbox ?? SyncUtilities.DataNotAvailable, workItem.SyncHealthData.CloudStatistics.TotalFoldersInSourceMailbox ?? SyncUtilities.DataNotAvailable, workItem.SyncHealthData.CloudStatistics.TotalSizeOfSourceMailbox ?? SyncUtilities.DataNotAvailable, workItem.Lifetime, updatedSubscription.SyncPhase.ToString(), syncPhaseBeforeSync.ToString(), workItem.CurrentRetryCount, workItem.SyncHealthData.RecoverySync, workItem.WasAttemptMadeToOpenMailboxSession, workItem.SyncHealthData.ThrottlingStatistics.TotalCpuUnhealthyBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalCpuFairBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalCpuUnknownBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseReplicationLogUnhealthyBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseReplicationLogFairBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseReplicationLogUnknownBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseRPCLatencyUnhealthyBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseRPCLatencyFairBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalDatabaseRPCLatencyUnknownBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalUnknownUnhealthyBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalUnknownFairBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalUnknownUnknownBackoffTime, workItem.SyncHealthData.ThrottlingStatistics.TotalBackoffTime);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005E5C File Offset: 0x0000405C
		private void LogDeletionHealthEvent(AggregationWorkItem workItem)
		{
			AggregationConfiguration.Instance.SyncHealthLog.LogPolicyInducedSubscriptionDeletion(Environment.MachineName, workItem.TenantGuid.ToString(), workItem.DatabaseGuid.ToString(), workItem.UserMailboxGuid.ToString(), workItem.SubscriptionId.ToString(), workItem.SubscriptionType.ToString(), workItem.CurrentRetryCount, workItem.SyncHealthData.RecoverySync, workItem.SyncHealthData.IsPermanentSyncError, workItem.SyncHealthData.IsTransientSyncError, workItem.SyncHealthData.Exceptions);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005F0F File Offset: 0x0000410F
		private void Worker(object stateObject)
		{
			if (this.state != AggregationScheduler.RunState.Running)
			{
				return;
			}
			this.WorkerHealthCheck();
			this.TriggerHealthCheckAfter(AggregationConfiguration.Instance.HealthCheckRetryInterval);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005F34 File Offset: 0x00004134
		private void WorkerHealthCheck()
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (this.checkLastTimeWorkStarted)
			{
				TimeSpan t = utcNow - this.LastTimeWorkStarted.Value;
				if (t >= AggregationConfiguration.Instance.HubInactivityThreshold)
				{
					this.syncLogSession.LogInformation((TSLID)489UL, ExTraceGlobals.SchedulerTracer, "Hub server {0} has not performed work for {1} minutes since the last dispatch. This may be due to a bug, the hub is not reachable, or the hub fails to accept any attempted dispatch.", new object[]
					{
						Environment.MachineName,
						t.TotalMinutes
					});
					AggregationComponent.EventLogger.LogEvent(TransportSyncWorkerEventLogConstants.Tuple_HubInactivityForLongTime, string.Empty, new object[]
					{
						Environment.MachineName,
						t.TotalMinutes
					});
					this.checkLastTimeWorkStarted = false;
				}
			}
		}

		// Token: 0x0400004E RID: 78
		private const string DefaultIncomingServerNameWithStatusFormatString = "IncomingServerName:{0}:{1}";

		// Token: 0x0400004F RID: 79
		private const int InitialTimerInterval = 2000;

		// Token: 0x04000050 RID: 80
		private const int TimerBackoffFactor = 2;

		// Token: 0x04000051 RID: 81
		private const int MaxTimerInterval = 128000;

		// Token: 0x04000052 RID: 82
		private ExDateTime? lastSubmitAttempt;

		// Token: 0x04000053 RID: 83
		private ExDateTime? lastTimeWorkStarted;

		// Token: 0x04000054 RID: 84
		private bool checkLastTimeWorkStarted;

		// Token: 0x04000055 RID: 85
		private object workListLock = new object();

		// Token: 0x04000056 RID: 86
		private Dictionary<Guid, AggregationScheduler.PendingWork> pendingWorkList;

		// Token: 0x04000057 RID: 87
		private int maximumPendingWorkItems;

		// Token: 0x04000058 RID: 88
		private int retryItemsCount;

		// Token: 0x04000059 RID: 89
		private volatile AggregationSubscriptionType enabledAggregationTypes;

		// Token: 0x0400005A RID: 90
		private volatile AggregationScheduler.RunState state;

		// Token: 0x0400005B RID: 91
		private ManualResetEvent pendingWorkListEmpty;

		// Token: 0x0400005C RID: 92
		private SyncLogSession syncLogSession;

		// Token: 0x04000061 RID: 97
		private GuardedTimer healthCheckTimer;

		// Token: 0x02000007 RID: 7
		internal enum RunState
		{
			// Token: 0x04000063 RID: 99
			Uninitialized,
			// Token: 0x04000064 RID: 100
			Initializing,
			// Token: 0x04000065 RID: 101
			Running,
			// Token: 0x04000066 RID: 102
			RunningInBackPressure,
			// Token: 0x04000067 RID: 103
			Shutdown,
			// Token: 0x04000068 RID: 104
			Unload
		}

		// Token: 0x02000008 RID: 8
		private sealed class PendingWork
		{
			// Token: 0x060000AA RID: 170 RVA: 0x00005FF8 File Offset: 0x000041F8
			public PendingWork(AggregationWorkItem item, IAsyncResult asyncResult, ExDateTime workItemStartTime)
			{
				SyncUtilities.ThrowIfArgumentNull("item", item);
				SyncUtilities.ThrowIfArgumentNull("asyncResult", asyncResult);
				SyncUtilities.ThrowIfArgumentNull("workItemStartTime", workItemStartTime);
				this.item = item;
				this.asyncResult = asyncResult;
				this.workItemStartTime = workItemStartTime;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00006046 File Offset: 0x00004246
			public AggregationWorkItem WorkItem
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000AC RID: 172 RVA: 0x0000604E File Offset: 0x0000424E
			public IAsyncResult AsyncResult
			{
				get
				{
					return this.asyncResult;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00006056 File Offset: 0x00004256
			internal ExDateTime WorkItemStartTime
			{
				get
				{
					return this.workItemStartTime;
				}
			}

			// Token: 0x04000069 RID: 105
			private readonly AggregationWorkItem item;

			// Token: 0x0400006A RID: 106
			private readonly IAsyncResult asyncResult;

			// Token: 0x0400006B RID: 107
			private ExDateTime workItemStartTime;
		}
	}
}
