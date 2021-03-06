using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200012B RID: 299
	internal sealed class StatusUpdatePublishJob : JobBase
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001AB50 File Offset: 0x00018D50
		public StatusUpdatePublishJob(IEnumerable<SyncStatusUpdateWorkitem> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext) : base(workItems, onJobCompleted, syncAgentContext)
		{
			if (!base.WorkItems.Any<WorkItemBase>())
			{
				throw new ArgumentException("Status update job must contain at least one work item");
			}
			this.workItemQueue = new Queue<SyncStatusUpdateWorkitem>(base.WorkItems.Count<WorkItemBase>());
			foreach (WorkItemBase workItemBase in base.WorkItems)
			{
				SyncStatusUpdateWorkitem item = (SyncStatusUpdateWorkitem)workItemBase;
				this.workItemQueue.Enqueue(item);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		private IPolicySyncWebserviceClient SyncSvcClient { get; set; }

		// Token: 0x06000879 RID: 2169 RVA: 0x0001AC0C File Offset: 0x00018E0C
		public override void Begin(object state)
		{
			this.SafeExecute(delegate
			{
				this.BeginImpl(state);
			});
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001AC40 File Offset: 0x00018E40
		private bool InitializeJob()
		{
			bool result = false;
			SyncStatusUpdateWorkitem syncStatusUpdateWorkitem = (SyncStatusUpdateWorkitem)base.WorkItems.First<WorkItemBase>();
			string partnerName = base.SyncAgentContext.SyncAgentConfig.PartnerName;
			string statusUpdateSvcUrl = syncStatusUpdateWorkitem.StatusUpdateSvcUrl;
			try
			{
				if (base.SyncAgentContext.SyncAgentConfig.CertificateSubject == null)
				{
					ICredentials credential = base.SyncAgentContext.CredentialFactory.GetCredential(syncStatusUpdateWorkitem.TenantContext);
					this.SyncSvcClient = base.SyncAgentContext.SyncSvcClientFactory.CreatePolicySyncWebserviceClient(new EndpointAddress(statusUpdateSvcUrl), credential, partnerName);
				}
				else
				{
					X509Certificate2 credential2 = base.SyncAgentContext.CredentialFactory.GetCredential(base.SyncAgentContext.SyncAgentConfig.CertificateSubject);
					this.SyncSvcClient = base.SyncAgentContext.SyncSvcClientFactory.CreatePolicySyncWebserviceClient(new EndpointAddress(statusUpdateSvcUrl), credential2, partnerName);
				}
				result = true;
			}
			catch (SyncAgentExceptionBase item)
			{
				foreach (WorkItemBase workItemBase in base.WorkItems)
				{
					workItemBase.Errors.Add(item);
					this.WorkItemCompleted(workItemBase);
				}
			}
			return result;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001AD70 File Offset: 0x00018F70
		private void SafeExecute(Action executionDelegate)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(executionDelegate);
			}
			catch (GrayException ex)
			{
				SyncAgentPermanentException ex2 = new SyncAgentPermanentException(ex.Message, ex, false, SyncAgentErrorCode.Generic);
				foreach (WorkItemBase workItemBase in base.WorkItems)
				{
					workItemBase.Errors.Add(new SyncAgentPermanentException(string.Format("Permanent error in async status publishing web service invocation - Begin. Details: {0}", ex2.Message), ex2));
					this.WorkItemCompleted(workItemBase);
				}
				this.JobCompleted();
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001AE0C File Offset: 0x0001900C
		private void BeginImpl(object state)
		{
			if (this.InitializeJob())
			{
				if (base.SyncAgentContext.SyncAgentConfig.AsyncCallSyncSvc)
				{
					this.asyncCallsInProgress = 0;
					while (this.workItemQueue.Any<SyncStatusUpdateWorkitem>() && !base.HostStateProvider.IsShuttingDown())
					{
						SyncStatusUpdateWorkitem workItem = this.workItemQueue.Dequeue();
						this.BeginExecute(workItem);
					}
					return;
				}
				while (this.workItemQueue.Any<SyncStatusUpdateWorkitem>() && !base.HostStateProvider.IsShuttingDown())
				{
					SyncStatusUpdateWorkitem workItem2 = this.workItemQueue.Dequeue();
					this.ExecuteWorkItem(workItem2);
					this.WorkItemCompleted(workItem2);
				}
			}
			this.JobCompleted();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001AEA4 File Offset: 0x000190A4
		private void BeginExecute(SyncStatusUpdateWorkitem workItem)
		{
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItem.TenantContext.TenantId.ToString(), workItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Status Publish WorkItem Begin", string.Format("Unified Policy Status Publish WorkItem Begin with {0} status objects. {1}", workItem.StatusUpdates.Count<UnifiedPolicyStatus>(), Utility.GetThreadPoolStatus()), null, new KeyValuePair<string, object>[0]);
			this.LogStatusesToPublish(workItem);
			workItem.TryCount++;
			Interlocked.Increment(ref this.asyncCallsInProgress);
			try
			{
				this.SyncSvcClient.BeginPublishStatus(new PublishStatusRequest
				{
					CallerContext = SyncCallerContext.Create(base.SyncAgentContext.SyncAgentConfig.PartnerName),
					ConfigurationStatuses = workItem.StatusUpdates
				}, new AsyncCallback(this.PublishStatusAsyncCallback), workItem);
			}
			catch (SyncAgentPermanentException ex)
			{
				workItem.Errors.Add(new SyncAgentPermanentException(string.Format("Permanent error in async status publishing web service invocation - BeginPublishStatus. Details: {0}", ex.Message), ex));
				this.CompleteAsyncCall(workItem);
			}
			catch (SyncAgentTransientException ex2)
			{
				workItem.Errors.Add(new SyncAgentTransientException(string.Format("Transient error in async status publishing web service invocation - BeginPublishStatus. Details: {0}", ex2.Message), ex2));
				this.CompleteAsyncCall(workItem);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001B004 File Offset: 0x00019204
		private void PublishStatusAsyncCallback(IAsyncResult result)
		{
			this.SafeExecute(delegate
			{
				this.PublishStatusAsyncCallbackImpl(result);
			});
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001B038 File Offset: 0x00019238
		private void PublishStatusAsyncCallbackImpl(IAsyncResult result)
		{
			SyncStatusUpdateWorkitem syncStatusUpdateWorkitem = null;
			AsyncCallStateObject asyncCallStateObject = result.AsyncState as AsyncCallStateObject;
			if (asyncCallStateObject != null)
			{
				syncStatusUpdateWorkitem = (SyncStatusUpdateWorkitem)asyncCallStateObject.CallerStateObject;
			}
			else
			{
				syncStatusUpdateWorkitem = (result.AsyncState as SyncStatusUpdateWorkitem);
			}
			ArgumentValidator.ThrowIfNull("asyncResult.State for PublishStatusAsyncCallbackImpl must not be null and of SyncStatusUpdateWorkitem type", syncStatusUpdateWorkitem);
			try
			{
				this.SyncSvcClient.EndPublishStatus(result);
			}
			catch (SyncAgentPermanentException ex)
			{
				syncStatusUpdateWorkitem.Errors.Add(new SyncAgentTransientException(string.Format("Permanent error in async status publishing web service invocation - PublishStatusAsyncCallback. Details: {0}", ex.Message), ex));
			}
			catch (SyncAgentTransientException ex2)
			{
				syncStatusUpdateWorkitem.Errors.Add(new SyncAgentTransientException(string.Format("Transient error in async status publishing web service invocation - PublishStatusAsyncCallback. Details: {0}", ex2.Message), ex2));
			}
			this.CompleteAsyncCall(syncStatusUpdateWorkitem);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001B0F4 File Offset: 0x000192F4
		private void CompleteAsyncCall(SyncStatusUpdateWorkitem workItem)
		{
			this.WorkItemCompleted(workItem);
			if (Interlocked.Decrement(ref this.asyncCallsInProgress) == 0 && !this.workItemQueue.Any<SyncStatusUpdateWorkitem>())
			{
				this.JobCompleted();
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001B120 File Offset: 0x00019320
		private void ExecuteWorkItem(SyncStatusUpdateWorkitem workItem)
		{
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItem.TenantContext.TenantId.ToString(), workItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Status Publish WorkItem Begin", string.Format("Unified Policy Status Publish WorkItem Begin with {0} status objects. {1}", workItem.StatusUpdates.Count<UnifiedPolicyStatus>(), Utility.GetThreadPoolStatus()), null, new KeyValuePair<string, object>[0]);
			this.LogStatusesToPublish(workItem);
			workItem.TryCount++;
			try
			{
				this.SyncSvcClient.PublishStatus(new PublishStatusRequest
				{
					CallerContext = SyncCallerContext.Create(base.SyncAgentContext.SyncAgentConfig.PartnerName),
					ConfigurationStatuses = workItem.StatusUpdates
				});
			}
			catch (SyncAgentPermanentException ex)
			{
				workItem.Errors.Add(new SyncAgentPermanentException(string.Format("Permanent error in status publishing web service invocation - BeginPublishStatus. Details: {0}", ex.Message), ex));
			}
			catch (SyncAgentTransientException ex2)
			{
				workItem.Errors.Add(new SyncAgentTransientException(string.Format("Transient error in status publishing web service invocation - BeginPublishStatus. Details: {0}", ex2.Message), ex2));
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001B23C File Offset: 0x0001943C
		private void WorkItemCompleted(WorkItemBase workItem)
		{
			if (workItem.Errors != null && workItem.Errors.Any<SyncAgentExceptionBase>())
			{
				workItem.Status = WorkItemStatus.Fail;
				using (List<SyncAgentExceptionBase>.Enumerator enumerator = workItem.Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SyncAgentExceptionBase exception = enumerator.Current;
						base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItem.TenantContext.TenantId.ToString(), workItem.ExternalIdentity, ExecutionLog.EventType.Error, "Unified Policy Status Publish WorkItem Error", "Unified Policy Status Publish WorkItem Error", exception, new KeyValuePair<string, object>[0]);
					}
					goto IL_96;
				}
			}
			workItem.Status = WorkItemStatus.Success;
			IL_96:
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItem.TenantContext.TenantId.ToString(), workItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Status Publish WorkItem End", string.Format("Unified Policy Status Publish WorkItem End. Result is {0}. TryCount is {1}.", workItem.Status, workItem.TryCount), null, new KeyValuePair<string, object>[0]);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001B34C File Offset: 0x0001954C
		private void JobCompleted()
		{
			if (this.SyncSvcClient != null)
			{
				this.SyncSvcClient.Dispose();
				this.SyncSvcClient = null;
			}
			if (base.SyncAgentContext.SyncAgentConfig.EnableMonitor)
			{
				this.TriggerAlertIfNecessary();
			}
			base.OnJobCompleted(this);
			this.PublishPerfData();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001B3E0 File Offset: 0x000195E0
		private void TriggerAlertIfNecessary()
		{
			WorkItemBase workItemBase = base.WorkItems.FirstOrDefault((WorkItemBase p) => p.Errors.Any((SyncAgentExceptionBase ex) => ex is SyncAgentPermanentException));
			if (workItemBase != null)
			{
				base.SyncAgentContext.MonitorProvider.PublishEvent("UnifiedPolicySync.PermanentStatusPublishError", workItemBase.TenantContext.TenantId.ToString(), string.Format("Timestamp={0};Sync status notification Id={1}", DateTime.UtcNow, workItemBase.ExternalIdentity), workItemBase.Errors.Last((SyncAgentExceptionBase ex) => ex is SyncAgentPermanentException));
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001B494 File Offset: 0x00019694
		private void PublishPerfData()
		{
			int num = 0;
			foreach (WorkItemBase workItemBase in base.WorkItems)
			{
				if (workItemBase.Errors.Any<SyncAgentExceptionBase>())
				{
					num += workItemBase.Errors.Count((SyncAgentExceptionBase p) => p is SyncAgentPermanentException);
				}
			}
			if (num > 0)
			{
				base.SyncAgentContext.PerfCounterProvider.IncrementBy("Status Update Permanent Error Number", (long)num);
				base.SyncAgentContext.PerfCounterProvider.IncrementBy("Status Update Permanent Error Number Per Second", (long)num);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001B548 File Offset: 0x00019748
		private void LogStatusesToPublish(SyncStatusUpdateWorkitem workItem)
		{
			IEnumerable<UnifiedPolicyStatus> statusUpdates = workItem.StatusUpdates;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("Unified Policy Status Publish WorkItem Details: Count: {0}; Objects: ", statusUpdates.Count<UnifiedPolicyStatus>()));
			foreach (UnifiedPolicyStatus unifiedPolicyStatus in workItem.StatusUpdates)
			{
				stringBuilder.Append(string.Format("{0};", unifiedPolicyStatus.ToString()));
			}
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", workItem.TenantContext.TenantId.ToString(), workItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Status Publish WorkItem Details", stringBuilder.ToString(), null, new KeyValuePair<string, object>[0]);
		}

		// Token: 0x04000475 RID: 1141
		private Queue<SyncStatusUpdateWorkitem> workItemQueue;

		// Token: 0x04000476 RID: 1142
		private int asyncCallsInProgress;
	}
}
