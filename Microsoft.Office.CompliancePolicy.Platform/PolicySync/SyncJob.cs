using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200012C RID: 300
	internal sealed class SyncJob : JobBase
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x0001B614 File Offset: 0x00019814
		public SyncJob(IEnumerable<WorkItemBase> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext) : base(workItems, onJobCompleted, syncAgentContext)
		{
			if (base.WorkItems.Count<WorkItemBase>() != 1)
			{
				throw new ArgumentException("For sync job, only one work item per job is allowed. The input work item count is instead " + base.WorkItems.Count<WorkItemBase>());
			}
			this.nextSyncCycleWork = new Dictionary<ConfigurationObjectType, List<SyncChangeInfo>>();
			this.CurrentWorkItem = (SyncWorkItem)base.WorkItems.First<WorkItemBase>();
			this.Errors = new List<SyncAgentExceptionBase>();
			this.DeletedObjectList = new HashSet<Guid>();
			this.MonitorEventTracker = new SyncMonitorEventTracker(this);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001B69B File Offset: 0x0001989B
		internal SyncJob(IEnumerable<WorkItemBase> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext, bool noCleanUp = false) : this(workItems, onJobCompleted, syncAgentContext)
		{
			this.noCleanUp = noCleanUp;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001B6AE File Offset: 0x000198AE
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001B6B6 File Offset: 0x000198B6
		internal SyncWorkItem CurrentWorkItem { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001B6BF File Offset: 0x000198BF
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001B6C7 File Offset: 0x000198C7
		internal List<SyncAgentExceptionBase> Errors { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001B6D0 File Offset: 0x000198D0
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0001B6D8 File Offset: 0x000198D8
		internal HashSet<Guid> DeletedObjectList { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001B6E1 File Offset: 0x000198E1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001B6E9 File Offset: 0x000198E9
		internal IPolicySyncWebserviceClient SyncSvcClient { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001B6F2 File Offset: 0x000198F2
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001B6FA File Offset: 0x000198FA
		internal ITenantInfoProvider TenantInfoProvider { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001B703 File Offset: 0x00019903
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001B70B File Offset: 0x0001990B
		internal PolicyConfigProvider PolicyConfigProvider { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001B714 File Offset: 0x00019914
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001B71C File Offset: 0x0001991C
		internal TenantInfo TenantInfo { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001B725 File Offset: 0x00019925
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001B72D File Offset: 0x0001992D
		internal List<Guid> LocalPolicyIdList { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001B736 File Offset: 0x00019936
		internal bool IsLastTry
		{
			get
			{
				return !base.SyncAgentContext.SyncAgentConfig.RetryStrategy.CanRetry(this.CurrentWorkItem.TryCount);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001B75B File Offset: 0x0001995B
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0001B763 File Offset: 0x00019963
		internal SyncMonitorEventTracker MonitorEventTracker { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001B76C File Offset: 0x0001996C
		private bool TenantPermanentErrorOccurs
		{
			get
			{
				if (this.Errors.Any<SyncAgentExceptionBase>())
				{
					SyncAgentExceptionBase syncAgentExceptionBase = this.Errors.Last<SyncAgentExceptionBase>();
					return !syncAgentExceptionBase.IsPerObjectException && (syncAgentExceptionBase is SyncAgentPermanentException || this.IsLastTry);
				}
				return false;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001B7B0 File Offset: 0x000199B0
		private bool AreAllErrorsObjectPermanentErrors
		{
			get
			{
				if (this.Errors.Any<SyncAgentExceptionBase>())
				{
					foreach (SyncAgentExceptionBase syncAgentExceptionBase in this.Errors)
					{
						if (!syncAgentExceptionBase.IsPerObjectException || syncAgentExceptionBase is SyncAgentTransientException)
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001B860 File Offset: 0x00019A60
		public override void Begin(object state)
		{
			base.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.CurrentWorkItem.TenantContext.TenantId.ToString(), this.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Job Begin", "Unified Policy Sync Job Begin. " + Utility.GetThreadPoolStatus(), null, new KeyValuePair<string, object>[0]);
			this.MonitorEventTracker.MarkNotificationPickedUp();
			this.subWorkItemQueue = this.BuildSubWorkitemQueue(this.CurrentWorkItem);
			if (base.SyncAgentContext.SyncAgentConfig.AsyncCallSyncSvc)
			{
				SubWorkItemBase subWorkItem = this.subWorkItemQueue.Dequeue();
				if (this.ExecuteSubWorkItemWrapper(delegate
				{
					subWorkItem.BeginExecute(new Action<SubWorkItemBase>(this.OnSubWorkItemEnd));
				}, subWorkItem, true))
				{
					this.OnWorkItemCompleted(this.CurrentWorkItem);
					return;
				}
			}
			else
			{
				bool flag = false;
				while (!flag)
				{
					SubWorkItemBase subWorkItem = this.subWorkItemQueue.Dequeue();
					flag = this.ExecuteSubWorkItemWrapper(delegate
					{
						subWorkItem.Execute();
					}, subWorkItem, false);
				}
				this.OnWorkItemCompleted(this.CurrentWorkItem);
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001B97C File Offset: 0x00019B7C
		private Queue<SubWorkItemBase> BuildSubWorkitemQueue(SyncWorkItem workItem)
		{
			Queue<SubWorkItemBase> queue = new Queue<SubWorkItemBase>();
			queue.Enqueue(new InitializationSubWorkItem(this));
			foreach (ConfigurationObjectType key in new ConfigurationObjectType[]
			{
				ConfigurationObjectType.Policy,
				ConfigurationObjectType.Rule,
				ConfigurationObjectType.Binding,
				ConfigurationObjectType.Association
			})
			{
				if (workItem.WorkItemInfo.ContainsKey(key))
				{
					foreach (SyncChangeInfo syncChangeInfo in workItem.WorkItemInfo[key])
					{
						queue.Enqueue((syncChangeInfo.ObjectId != null) ? new ObjectSyncSubWorkItem(this, syncChangeInfo) : new TypeSyncSubWorkItem(this, syncChangeInfo));
					}
				}
			}
			return queue;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001BA88 File Offset: 0x00019C88
		private void OnSubWorkItemEnd(SubWorkItemBase subWorkItem)
		{
			SyncJob.<>c__DisplayClass8 CS$<>8__locals1 = new SyncJob.<>c__DisplayClass8();
			CS$<>8__locals1.subWorkItem = subWorkItem;
			CS$<>8__locals1.<>4__this = this;
			if (this.ExecuteSubWorkItemWrapper(delegate
			{
				CS$<>8__locals1.subWorkItem.EndExecute();
			}, CS$<>8__locals1.subWorkItem, false))
			{
				this.OnWorkItemCompleted(this.CurrentWorkItem);
				return;
			}
			SubWorkItemBase nextSubWorkItem = this.subWorkItemQueue.Dequeue();
			if (this.ExecuteSubWorkItemWrapper(delegate
			{
				nextSubWorkItem.BeginExecute(new Action<SubWorkItemBase>(CS$<>8__locals1.<>4__this.OnSubWorkItemEnd));
			}, nextSubWorkItem, true))
			{
				this.OnWorkItemCompleted(this.CurrentWorkItem);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001BC44 File Offset: 0x00019E44
		private bool ExecuteSubWorkItemWrapper(Action executeDelegate, SubWorkItemBase subWorkItem, bool isAsyncBegin = false)
		{
			bool stopEntireWorkItem = false;
			bool currentSubWorkItemSuccess = false;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						executeDelegate();
						currentSubWorkItemSuccess = true;
					}
					catch (SyncAgentTransientException ex2)
					{
						this.Errors.Add(ex2);
						if (!this.IsLastTry)
						{
							this.AddToNextSyncCycle(subWorkItem);
						}
						if (!ex2.IsPerObjectException)
						{
							stopEntireWorkItem = true;
						}
					}
					catch (SyncAgentPermanentException ex3)
					{
						this.Errors.Add(ex3);
						stopEntireWorkItem = !ex3.IsPerObjectException;
					}
				});
			}
			catch (GrayException ex)
			{
				SyncAgentPermanentException item = new SyncAgentPermanentException(ex.Message, ex, false, SyncAgentErrorCode.Generic);
				this.Errors.Add(item);
				stopEntireWorkItem = true;
			}
			if (!stopEntireWorkItem)
			{
				if ((!currentSubWorkItemSuccess && subWorkItem is InitializationSubWorkItem) || base.HostStateProvider.IsShuttingDown())
				{
					stopEntireWorkItem = true;
				}
				if (!stopEntireWorkItem)
				{
					if (!isAsyncBegin)
					{
						stopEntireWorkItem = !this.subWorkItemQueue.Any<SubWorkItemBase>();
					}
					else if (!currentSubWorkItemSuccess)
					{
						if (this.subWorkItemQueue.Any<SubWorkItemBase>())
						{
							SubWorkItemBase nextSubWorkItem = this.subWorkItemQueue.Dequeue();
							ThreadPool.QueueUserWorkItem(delegate(object state)
							{
								if (this.ExecuteSubWorkItemWrapper(delegate
								{
									nextSubWorkItem.BeginExecute(new Action<SubWorkItemBase>(this.OnSubWorkItemEnd));
								}, nextSubWorkItem, true))
								{
									this.OnWorkItemCompleted(this.CurrentWorkItem);
								}
							});
						}
						else
						{
							stopEntireWorkItem = true;
						}
					}
				}
			}
			return stopEntireWorkItem;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001C210 File Offset: 0x0001A410
		private void OnWorkItemCompleted(SyncWorkItem workItem)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						if (this.Errors.Any<SyncAgentExceptionBase>())
						{
							workItem.Status = WorkItemStatus.Fail;
							using (List<SyncAgentExceptionBase>.Enumerator enumerator = this.Errors.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									SyncAgentExceptionBase exception = enumerator.Current;
									this.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.CurrentWorkItem.TenantContext.TenantId.ToString(), this.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Error, "Unified Policy Sync Job Error", "Unified Policy Sync Job Error", exception, new KeyValuePair<string, object>[0]);
								}
								goto IL_D7;
							}
						}
						workItem.Status = (this.nextSyncCycleWork.Any<KeyValuePair<ConfigurationObjectType, List<SyncChangeInfo>>>() ? WorkItemStatus.Stopped : WorkItemStatus.Success);
						IL_D7:
						workItem.Errors = this.Errors;
						int tryCount = workItem.TryCount;
						if (this.TenantPermanentErrorOccurs || this.AreAllErrorsObjectPermanentErrors)
						{
							workItem.TryCount = -1;
						}
						else
						{
							while (this.subWorkItemQueue.Any<SubWorkItemBase>())
							{
								this.AddToNextSyncCycle(this.subWorkItemQueue.Dequeue());
							}
						}
						if (this.nextSyncCycleWork.Any<KeyValuePair<ConfigurationObjectType, List<SyncChangeInfo>>>())
						{
							workItem.WorkItemInfo = this.nextSyncCycleWork;
						}
						if (this.TenantPermanentErrorOccurs)
						{
							this.MonitorEventTracker.ReportTenantLevelFailure(this.Errors.Last<SyncAgentExceptionBase>());
						}
						if (this.TenantInfo != null)
						{
							DateTime utcNow = DateTime.UtcNow;
							switch (workItem.Status)
							{
							case WorkItemStatus.Success:
								this.TenantInfo.LastSuccessfulSyncUTC = new DateTime?(utcNow);
								this.TenantInfo.LastErrors = null;
								break;
							case WorkItemStatus.Fail:
								this.TenantInfo.LastErrorTimeUTC = new DateTime?(utcNow);
								this.TenantInfo.LastErrors = (from p in workItem.Errors
								select new SerializableException(p).ExceptionChain).ToArray<string>();
								break;
							}
							this.TenantInfo.LastAttemptedSyncUTC = new DateTime?(utcNow);
							this.MonitorEventTracker.TrackLatencyWrapper(LatencyType.TenantInfo, delegate()
							{
								this.TenantInfoProvider.Save(this.TenantInfo);
							});
						}
						if (!this.noCleanUp)
						{
							if (this.SyncSvcClient != null)
							{
								this.SyncSvcClient.Dispose();
								this.SyncSvcClient = null;
							}
							if (this.PolicyConfigProvider != null)
							{
								this.PolicyConfigProvider.Dispose();
								this.PolicyConfigProvider = null;
							}
							if (this.TenantInfoProvider != null)
							{
								this.TenantInfoProvider.Dispose();
								this.TenantInfoProvider = null;
							}
						}
						if (this.SyncAgentContext.SyncAgentConfig.EnableMonitor)
						{
							this.MonitorEventTracker.TriggerAlertIfNecessary();
						}
						this.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.CurrentWorkItem.TenantContext.TenantId.ToString(), this.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Job End", string.Format(string.Format("Unified Policy Sync Job End. Result is {0}. TryCount is {1}.", workItem.Status, tryCount), new object[0]), null, new KeyValuePair<string, object>[0]);
						this.MonitorEventTracker.TrackLatencyWrapper(LatencyType.PersistentQueue, delegate()
						{
							this.OnJobCompleted(this);
						});
						this.MonitorEventTracker.PublishPerfData();
					}
					catch (SyncAgentExceptionBase)
					{
					}
				});
			}
			catch (GrayException)
			{
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001C260 File Offset: 0x0001A460
		private void AddToNextSyncCycle(SubWorkItemBase subWorkItem)
		{
			if (!(subWorkItem is ObjectSyncSubWorkItem) && !(subWorkItem is TypeSyncSubWorkItem))
			{
				return;
			}
			ConfigurationObjectType objectType = subWorkItem.ChangeInfo.ObjectType;
			if (!this.nextSyncCycleWork.ContainsKey(objectType))
			{
				this.nextSyncCycleWork[objectType] = new List<SyncChangeInfo>();
			}
			this.nextSyncCycleWork[objectType].Add(subWorkItem.ChangeInfo);
		}

		// Token: 0x0400047C RID: 1148
		private readonly bool noCleanUp;

		// Token: 0x0400047D RID: 1149
		private Queue<SubWorkItemBase> subWorkItemQueue;

		// Token: 0x0400047E RID: 1150
		private Dictionary<ConfigurationObjectType, List<SyncChangeInfo>> nextSyncCycleWork;
	}
}
