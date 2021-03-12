using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000134 RID: 308
	public sealed class MemoryWorkItemQueueProvider : IWorkItemQueueProvider
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0001E280 File Offset: 0x0001C480
		public MemoryWorkItemQueueProvider(IWorkItemQueueProvider persistentQueueProvider, JobDispatcherBase jobDispatcher, SyncAgentContext syncAgentContext)
		{
			ArgumentValidator.ThrowIfNull("persistentQueueProvider", persistentQueueProvider);
			ArgumentValidator.ThrowIfNull("jobDispatcher", jobDispatcher);
			ArgumentValidator.ThrowIfNull("syncAgentContext", syncAgentContext);
			this.hostStateProvider = syncAgentContext.HostStateProvider;
			this.persistentQueueProvider = persistentQueueProvider;
			jobDispatcher.WorkItemQueue = this;
			this.logProvider = syncAgentContext.LogProvider;
			this.config = syncAgentContext.SyncAgentConfig;
			if (this.config.DispatcherTriggerInterval != null)
			{
				this.dispatchTimer = new Timer(new TimerCallback(jobDispatcher.Dispatch), null, TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001E3BC File Offset: 0x0001C5BC
		public void Enqueue(WorkItemBase item)
		{
			ArgumentValidator.ThrowIfNull("item", item);
			if (this.hostStateProvider.IsShuttingDown())
			{
				SyncAgentTransientException ex = new SyncAgentTransientException("Queue is shutting down", false, SyncAgentErrorCode.EnqueueErrorShutDown);
				this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("In-memory {0} Queue is shutting down", item.GetType().Name), ex, new KeyValuePair<string, object>[0]);
				throw ex;
			}
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.syncObject, ref flag);
				this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Information, string.Format("In-memory {0} Queue Length", item.GetType().Name), string.Format("In-memory {0} Queue Length: {1}", item.GetType().Name, this.internalQueue.Count()), null, new KeyValuePair<string, object>[0]);
				item.ResetStatus();
				if (item.ExecuteTimeUTC == default(DateTime))
				{
					item.ExecuteTimeUTC = DateTime.UtcNow + this.config.WorkItemExecuteDelayTime;
				}
				WorkItemBase similarItem = null;
				if (this.internalQueue.TryGetValue(item.GetPrimaryKey(), out similarItem))
				{
					if (similarItem.IsEqual(item))
					{
						if (item.HasPersistentBackUp)
						{
							Utility.DoWorkAndLogIfFail(delegate
							{
								this.persistentQueueProvider.Delete(item);
							}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Warning, string.Empty, string.Format("Duped {0} WI {1} of Tenant {2} can't be removed from the WI store", item.GetType().Name, item.WorkItemId, item.TenantContext.TenantId), false, false);
						}
						this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Information, string.Empty, string.Format("{0} Notification {1} is de-duped due to the existence of Notification {2}", item.GetType().Name, item.ExternalIdentity, similarItem.ExternalIdentity), null, new KeyValuePair<string, object>[0]);
					}
					else
					{
						if (!similarItem.Merge(item))
						{
							SyncAgentTransientException ex2 = new SyncAgentTransientException("The item insertion has failed due to conflict", false, SyncAgentErrorCode.EnqueueErrorMergeConflict);
							this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("{0} Notification {1} fails to be merged into Notification {2} due to merge conflict", item.GetType().Name, item.ExternalIdentity, similarItem.ExternalIdentity), ex2, new KeyValuePair<string, object>[0]);
							throw ex2;
						}
						Utility.DoWorkAndLogIfFail(delegate
						{
							this.persistentQueueProvider.Update(similarItem);
						}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("{0} WI {1} of Tenant {2} can't be saved to the WI store", item.GetType().Name, item.WorkItemId, item.TenantContext.TenantId), true, false);
						if (item.HasPersistentBackUp)
						{
							Utility.DoWorkAndLogIfFail(delegate
							{
								this.persistentQueueProvider.Delete(item);
							}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Warning, string.Empty, string.Format("Merged {0} WI {1} of Tenant {2} can't be removed from the WI store", item.GetType().Name, item.WorkItemId, item.TenantContext.TenantId), false, false);
						}
						this.internalQueue.Enqueue(similarItem);
						this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Information, string.Empty, string.Format("{0} Notification {1} is merged into Notification {2}. The execution is scheduled at {3}.", new object[]
						{
							item.GetType().Name,
							item.ExternalIdentity,
							similarItem.ExternalIdentity,
							similarItem.ExecuteTimeUTC
						}), null, new KeyValuePair<string, object>[0]);
					}
				}
				else
				{
					if (this.IsFull())
					{
						SyncAgentTransientException ex3 = new SyncAgentTransientException("Queue is full", false, SyncAgentErrorCode.EnqueueErrorQueueFull);
						this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("In-memory {0} Queue is full", item.GetType().Name), ex3, new KeyValuePair<string, object>[0]);
						throw ex3;
					}
					if (!item.HasPersistentBackUp)
					{
						Utility.DoWorkAndLogIfFail(delegate
						{
							this.persistentQueueProvider.Enqueue(item);
						}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("New {0} WI {1} of Tenant {2} can't be saved to the WI store", item.GetType().Name, item.WorkItemId, item.TenantContext.TenantId), true, false);
					}
					this.internalQueue.Enqueue(item);
					this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Information, string.Empty, string.Format("New {0} WI {1} of Tenant {2} is enqueued. The execution is scheduled at {3}.", new object[]
					{
						item.GetType().Name,
						item.WorkItemId,
						item.TenantContext.TenantId,
						item.ExecuteTimeUTC
					}), null, new KeyValuePair<string, object>[0]);
					if (this.dispatchTimer != null && !this.isDispatchScheduled)
					{
						this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Information, string.Empty, string.Format("The dispatcher is scheduled at {0}.", DateTime.UtcNow + this.config.DispatcherTriggerInterval.Value), null, new KeyValuePair<string, object>[0]);
						this.SetDispatcherSchedule(true);
					}
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public IList<WorkItemBase> Dequeue(int maxCount)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("maxCount", maxCount);
			if (this.hostStateProvider.IsShuttingDown())
			{
				return null;
			}
			IList<WorkItemBase> result;
			lock (this.syncObject)
			{
				result = this.internalQueue.Dequeue(maxCount);
			}
			return result;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001EC68 File Offset: 0x0001CE68
		public IList<WorkItemBase> GetAll()
		{
			throw new NotSupportedException("GetAll() isn't supported by in-memory queue");
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001EC74 File Offset: 0x0001CE74
		public bool IsEmpty()
		{
			if (this.hostStateProvider.IsShuttingDown())
			{
				return true;
			}
			bool result;
			lock (this.syncObject)
			{
				result = this.internalQueue.IsEmpty();
			}
			return result;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001ECCC File Offset: 0x0001CECC
		public void Update(WorkItemBase item)
		{
			throw new NotSupportedException("Update() isn't supported by in-memory queue");
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public void Delete(WorkItemBase item)
		{
			throw new NotSupportedException("Delete() isn't supported by in-memory queue");
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public void OnWorkItemCompleted(WorkItemBase item)
		{
			if (this.hostStateProvider.IsShuttingDown())
			{
				return;
			}
			WorkItemStatus status = item.Status;
			SyncAgentExceptionBase exception = (item.Errors != null && item.Errors.Any<SyncAgentExceptionBase>()) ? item.Errors.Last<SyncAgentExceptionBase>() : null;
			lock (this.syncObject)
			{
				this.logProvider.LogOneEntry("UnifiedPolicySyncAgent", item.TenantContext.TenantId.ToString(), item.ExternalIdentity, (status == WorkItemStatus.Fail) ? ExecutionLog.EventType.Error : ExecutionLog.EventType.Information, string.Empty, "Entered OnWorkItemCompleted", exception, new KeyValuePair<string, object>[0]);
				if (status == WorkItemStatus.Success || (WorkItemStatus.Fail == status && !this.config.RetryStrategy.CanRetry(item.TryCount)))
				{
					Utility.DoWorkAndLogIfFail(delegate
					{
						this.persistentQueueProvider.Delete(item);
					}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Warning, string.Empty, string.Format("{0} WI {1} of Tenant {2} can't be removed from the WI store", status, item.WorkItemId, item.TenantContext.TenantId), false, false);
				}
				else
				{
					item.ExecuteTimeUTC = DateTime.UtcNow + this.config.RetryStrategy.GetRetryInterval(item.TryCount);
					if (WorkItemStatus.NotStarted != status)
					{
						Utility.DoWorkAndLogIfFail(delegate
						{
							this.persistentQueueProvider.Update(item);
						}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Warning, string.Empty, string.Format("{0} WI {1} of Tenant {2} can't be saved to the WI store", status, item.WorkItemId, item.TenantContext.TenantId), false, false);
					}
					if (this.config.ReEnqueueNonSuccessWorkItem)
					{
						Utility.DoWorkAndLogIfFail(delegate
						{
							this.Enqueue(item);
						}, this.logProvider, item.TenantContext.TenantId.ToString(), item.ExternalIdentity, ExecutionLog.EventType.Error, string.Empty, string.Format("{0} WI {1} of Tenant {2}  can't be re-inserted back to the queue", status, item.WorkItemId, item.TenantContext.TenantId), false, false);
					}
				}
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001F048 File Offset: 0x0001D248
		public void OnAllWorkItemDispatched()
		{
			if (this.hostStateProvider.IsShuttingDown())
			{
				return;
			}
			lock (this.syncObject)
			{
				if (this.dispatchTimer != null)
				{
					this.SetDispatcherSchedule(!this.internalQueue.IsEmpty());
				}
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		private void SetDispatcherSchedule(bool schedule)
		{
			if (schedule)
			{
				this.dispatchTimer.Change(this.config.DispatcherTriggerInterval.Value, TimeSpan.FromMilliseconds(-1.0));
			}
			else
			{
				this.dispatchTimer.Change(-1, -1);
			}
			this.isDispatchScheduled = schedule;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001F100 File Offset: 0x0001D300
		private bool IsFull()
		{
			return this.hostStateProvider.IsShuttingDown() || this.config.MaxQueueLength == this.internalQueue.Count();
		}

		// Token: 0x040004AE RID: 1198
		private readonly object syncObject = new object();

		// Token: 0x040004AF RID: 1199
		private readonly IndexedQueue internalQueue = new IndexedQueue();

		// Token: 0x040004B0 RID: 1200
		private readonly Timer dispatchTimer;

		// Token: 0x040004B1 RID: 1201
		private IWorkItemQueueProvider persistentQueueProvider;

		// Token: 0x040004B2 RID: 1202
		private HostStateProvider hostStateProvider;

		// Token: 0x040004B3 RID: 1203
		private bool isDispatchScheduled;

		// Token: 0x040004B4 RID: 1204
		private ExecutionLog logProvider;

		// Token: 0x040004B5 RID: 1205
		private SyncAgentConfiguration config;
	}
}
