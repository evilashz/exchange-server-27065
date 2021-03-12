using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.Provisioning.Messages;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200000E RID: 14
	internal class ProvisioningServicelet : Servicelet, IProvisioningHandler
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00004C64 File Offset: 0x00002E64
		public ProvisioningServicelet()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in BulkUserProvisioningCounters.AllCounters)
			{
				exPerformanceCounter.RawValue = 0L;
			}
			ProvisioningServicelet.instance = this;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static ProvisioningServicelet Instance
		{
			get
			{
				if (ProvisioningServicelet.instance == null)
				{
					lock (ProvisioningServicelet.syncObject)
					{
						if (ProvisioningServicelet.instance == null)
						{
							ProvisioningServicelet.instance = new ProvisioningServicelet();
						}
					}
				}
				return ProvisioningServicelet.instance;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004D30 File Offset: 0x00002F30
		internal int RegisteredJobsCount
		{
			get
			{
				return this.registeredJobs.Count;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004D3D File Offset: 0x00002F3D
		internal int MaxCapacityPerTenant
		{
			get
			{
				return Convert.ToInt32(Math.Max(1.0, Math.Ceiling((double)(100 / Math.Max(1, this.registeredJobs.Count)))));
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004DAC File Offset: 0x00002FAC
		internal int WorkersInProgress
		{
			get
			{
				this.UpdateQueues();
				List<Guid> jobIds = new List<Guid>();
				this.queuedItems.ForEach((ProvisioningInfo x) => x.Worker != null, delegate(ObjectId id, ProvisioningInfo info)
				{
					if (!jobIds.Contains(info.JobId))
					{
						jobIds.Add(info.JobId);
					}
				});
				return jobIds.Count<Guid>();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004FE3 File Offset: 0x000031E3
		public override void Work()
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				ExTraceGlobals.ServiceletTracer.TraceInformation(17616, (long)this.GetHashCode(), "Provisioning Servicelet Starting");
				TimeSpan timeout;
				do
				{
					timeout = TimeSpan.FromSeconds(10.0);
					TimeSpan runDuration = TimeSpan.FromSeconds(30.0);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<TimeSpan>(2598776125U, ref runDuration);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<TimeSpan>(3672517949U, ref timeout);
					try
					{
						this.WorkInternal(runDuration, ref timeout);
					}
					catch (TransientException ex)
					{
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_TransientException, string.Empty, new object[]
						{
							ex.ToString()
						});
					}
					catch (StoragePermanentException ex2)
					{
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_PermanentException, string.Empty, new object[]
						{
							ex2.ToString()
						});
					}
					catch (DataValidationException ex3)
					{
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_PermanentException, string.Empty, new object[]
						{
							ex3.ToString()
						});
					}
					catch (LocalServerNotFoundException ex4)
					{
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_PermanentException, string.Empty, new object[]
						{
							ex4.ToString()
						});
					}
					catch (DataSourceOperationException ex5)
					{
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_PermanentException, string.Empty, new object[]
						{
							ex5.ToString()
						});
					}
				}
				while (!base.StopEvent.WaitOne(timeout, false));
				ExTraceGlobals.ServiceletTracer.TraceInformation(17620, (long)this.GetHashCode(), "Provisioning Servicelet Stopped");
			}, (object exception) => true, ReportOptions.None);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005014 File Offset: 0x00003214
		public void RegisterJob(Guid jobId, CultureInfo cultureInfo, Guid ownerExchangeObjectId, ADObjectId ownerId, DelegatedPrincipal delegatedAdminOwner, SubmittedByUserAdminType migrationRequesterRole, string tenantOrganization, OrganizationId organizationId = null)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			MigrationUtil.ThrowOnNullArgument(cultureInfo, "cultureInfo");
			if (ownerExchangeObjectId == Guid.Empty && ownerId == null && delegatedAdminOwner == null)
			{
				throw new ArgumentException("ownerId and delegatedAdminOwner cannot both be null.");
			}
			if (migrationRequesterRole == SubmittedByUserAdminType.Unknown)
			{
				migrationRequesterRole = SubmittedByUserAdminType.TenantAdmin;
			}
			if (migrationRequesterRole == SubmittedByUserAdminType.Partner && tenantOrganization == null)
			{
				throw new ArgumentException("A value for tenantOrganization is expected when migrationRequestRoleType is Partner. Was null");
			}
			if (this.registeredJobs.ContainsKey(jobId))
			{
				throw new MigrationDataCorruptionException(string.Format("jobId {0} is already registered", jobId));
			}
			ProvisioningAgentContext value = new ProvisioningAgentContext(jobId, cultureInfo, ownerExchangeObjectId, ownerId, delegatedAdminOwner, migrationRequesterRole, tenantOrganization, organizationId, this.EventLog);
			this.registeredJobs.Add(jobId, value);
			BulkUserProvisioningCounters.NumberOfRequestsInQueue.Increment();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000050E8 File Offset: 0x000032E8
		public void UnregisterJob(Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			if (!this.IsJobRegistered(jobId))
			{
				throw new MigrationDataCorruptionException(string.Format("jobId {0} is not yet registered", jobId));
			}
			if (!this.CanUnregisterJob(jobId))
			{
				throw new MigrationDataCorruptionException(string.Format("jobId {0} can't be unregistered", jobId));
			}
			ProvisioningAgentContext provisioningAgentContext;
			if (this.registeredJobs.TryGetValue(jobId, out provisioningAgentContext))
			{
				if (provisioningAgentContext != null)
				{
					provisioningAgentContext.Dispose();
				}
				this.registeredJobs.Remove(jobId);
				BulkUserProvisioningCounters.NumberOfRequestsInQueue.Decrement();
				BulkUserProvisioningCounters.NumberOfRoundsWithoutProgress.RawValue = 0L;
				BulkUserProvisioningCounters.NumberOfRequestsCompleted.Increment();
			}
			this.completedItems.RemoveAll((ProvisionedObject x) => x.JobId == jobId);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000051D2 File Offset: 0x000033D2
		public bool IsJobRegistered(Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			return this.registeredJobs.ContainsKey(jobId);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005218 File Offset: 0x00003418
		public bool CanUnregisterJob(Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			if (!this.IsJobRegistered(jobId))
			{
				throw new MigrationDataCorruptionException(string.Format("jobId {0} is not yet registered", jobId));
			}
			this.UpdateQueues();
			bool canUnregister = true;
			this.queuedItems.ForEach((ProvisioningInfo x) => x.JobId == jobId, delegate(ObjectId id, ProvisioningInfo info)
			{
				canUnregister = false;
			});
			return canUnregister;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000052D4 File Offset: 0x000034D4
		public bool HasCapacity(Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			if (!this.IsJobRegistered(jobId))
			{
				throw new MigrationDataCorruptionException(string.Format("jobId {0} is not yet registered", jobId));
			}
			this.UpdateQueues();
			int count = 0;
			this.queuedItems.ForEach((ProvisioningInfo x) => x.JobId == jobId, delegate(ObjectId id, ProvisioningInfo info)
			{
				count++;
			});
			return count < this.MaxCapacityPerTenant;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000536C File Offset: 0x0000356C
		public bool QueueItem(Guid jobId, ObjectId itemId, IProvisioningData data)
		{
			MigrationUtil.ThrowOnNullArgument(jobId, "jobId");
			MigrationUtil.ThrowOnNullArgument(itemId, "itemId");
			MigrationUtil.ThrowOnNullArgument(data, "data");
			if (!this.HasCapacity(jobId))
			{
				return false;
			}
			if (this.IsItemQueued(itemId))
			{
				return false;
			}
			ProvisioningInfo value = new ProvisioningInfo(itemId, jobId, data);
			this.queuedItems.Add(itemId, value);
			return true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000053CD File Offset: 0x000035CD
		public bool IsItemQueued(ObjectId itemId)
		{
			MigrationUtil.ThrowOnNullArgument(itemId, "itemId");
			this.UpdateQueues();
			return this.queuedItems.ContainsKey(itemId) || this.completedItems.ContainsKey(itemId);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000053FC File Offset: 0x000035FC
		public bool IsItemCompleted(ObjectId itemId)
		{
			MigrationUtil.ThrowOnNullArgument(itemId, "itemId");
			this.UpdateQueues();
			return this.completedItems.ContainsKey(itemId);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000541C File Offset: 0x0000361C
		public ProvisionedObject DequeueItem(ObjectId itemId)
		{
			MigrationUtil.ThrowOnNullArgument(itemId, "itemId");
			if (!this.IsItemCompleted(itemId))
			{
				throw new MigrationDataCorruptionException(string.Format("Can not dequeue incomplete item {0}", itemId));
			}
			ProvisionedObject result = this.completedItems[itemId];
			this.completedItems.Remove(itemId);
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000546C File Offset: 0x0000366C
		public void CancelItem(ObjectId itemId)
		{
			MigrationUtil.ThrowOnNullArgument(itemId, "itemId");
			if (!this.IsItemQueued(itemId))
			{
				throw new MigrationDataCorruptionException(string.Format("Can not cancel unqueued item {0}", itemId));
			}
			ProvisioningInfo provisioningInfo;
			if (this.queuedItems.TryGetValue(itemId, out provisioningInfo))
			{
				if (provisioningInfo.Worker != null)
				{
					provisioningInfo.Worker.Cancel(itemId);
					return;
				}
				this.queuedItems.Remove(itemId);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005564 File Offset: 0x00003764
		internal void WorkInternal(TimeSpan runDuration, ref TimeSpan restartDelay)
		{
			BulkUserProvisioningCounters.NumberOfRequestsWithoutProgressInThisRound.RawValue = 0L;
			BulkUserProvisioningCounters.NumberOfRequestsWithProgressInThisRound.RawValue = 0L;
			if (this.RegisteredJobsCount > 0)
			{
				List<ProvisioningAgentContext> sortedAgentContext = new List<ProvisioningAgentContext>();
				this.registeredJobs.ForEach((ProvisioningAgentContext x) => !this.WorkerAllocatedToJob(x.JobId), delegate(Guid guid, ProvisioningAgentContext info)
				{
					sortedAgentContext.Add(info);
				});
				sortedAgentContext = (from x in sortedAgentContext
				orderby x.LastModified
				select x).ToList<ProvisioningAgentContext>();
				int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("ProvisioningMaxNumThreads");
				int num = config - this.WorkersInProgress;
				int count = sortedAgentContext.Count;
				if (num == 0 || count == 0)
				{
					BulkUserProvisioningCounters.NumberOfRoundsWithoutProgress.Increment();
					return;
				}
				int num2 = Math.Min(num, count);
				sortedAgentContext = sortedAgentContext.Take(num2).ToList<ProvisioningAgentContext>();
				bool flag = false;
				for (int i = 0; i < num2; i++)
				{
					ProvisioningAgentContext agentContext = sortedAgentContext[i];
					this.UpdateQueues();
					agentContext.LastModified = ExDateTime.Now;
					this.registeredJobs[agentContext.JobId] = agentContext;
					List<ProvisioningInfo> provisioningInfoQueue = new List<ProvisioningInfo>();
					this.queuedItems.ForEach((ProvisioningInfo x) => x.Worker == null && x.JobId == agentContext.JobId, delegate(ObjectId id, ProvisioningInfo info)
					{
						if (info.TimesAttempted == 0 || ExDateTime.Now - info.LastAttempted > ProvisioningServicelet.minElapsedTimeBetweenItemRetry)
						{
							provisioningInfoQueue.Add(info);
						}
					});
					int num3 = Math.Min(25, provisioningInfoQueue.Count);
					if (num3 > 0)
					{
						BulkUserProvisioningCounters.NumberOfRequestsWithProgressInThisRound.Increment();
						provisioningInfoQueue = provisioningInfoQueue.Take(num3).ToList<ProvisioningInfo>();
						if (!agentContext.Initialize())
						{
							using (List<ProvisioningInfo>.Enumerator enumerator = provisioningInfoQueue.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ProvisioningInfo provisioningInfo = enumerator.Current;
									this.CancelItem(provisioningInfo.ItemId);
									ProvisionedObject value = new ProvisionedObject(provisioningInfo.ItemId, agentContext.JobId, provisioningInfo.Data.ProvisioningType);
									value.Succeeded = false;
									value.Error = Strings.UnknownProvisioningOwner;
									this.completedItems.Add(provisioningInfo.ItemId, value);
								}
								goto IL_2D3;
							}
						}
						ProvisioningWorker provisioningWorker = new ProvisioningWorker(provisioningInfoQueue, agentContext);
						for (int j = 0; j < num3; j++)
						{
							ProvisioningInfo value2 = provisioningInfoQueue[j];
							value2.Worker = provisioningWorker;
							this.queuedItems[value2.ItemId] = value2;
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(provisioningWorker.Work));
						flag = true;
					}
					IL_2D3:;
				}
				if (flag)
				{
					restartDelay = TimeSpan.FromSeconds(0.0);
					BulkUserProvisioningCounters.NumberOfRoundsWithoutProgress.RawValue = 0L;
				}
				if (count > num)
				{
					BulkUserProvisioningCounters.NumberOfRoundsWithRequestsWithoutProgress.Increment();
					BulkUserProvisioningCounters.NumberOfRequestsWithoutProgressInThisRound.RawValue = (long)(count - num);
					return;
				}
				BulkUserProvisioningCounters.NumberOfRoundsWithRequestsWithoutProgress.RawValue = 0L;
				this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_ProvisioningRoundCompleted, string.Empty, new object[0]);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005910 File Offset: 0x00003B10
		internal bool WorkerAllocatedToJob(Guid jobId)
		{
			bool isWorkerAllocated = false;
			this.queuedItems.ForEach((ProvisioningInfo x) => x.JobId == jobId && x.Worker != null && !x.Worker.Completed, delegate(ObjectId id, ProvisioningInfo info)
			{
				isWorkerAllocated = true;
			});
			return isWorkerAllocated;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000059A0 File Offset: 0x00003BA0
		private void UpdateQueues()
		{
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = ProvisioningServicelet.updateQueueLock, ref flag);
				List<ProvisioningInfo> entriesToMove = new List<ProvisioningInfo>();
				this.queuedItems.ForEach((ProvisioningInfo x) => x.Worker != null && x.Worker.ItemCompleted(x.ItemId), delegate(ObjectId id, ProvisioningInfo info)
				{
					entriesToMove.Add(this.queuedItems[id]);
				});
				for (int i = 0; i < entriesToMove.Count; i++)
				{
					ProvisioningInfo value = entriesToMove[i];
					ProvisionedObject completedItem = value.Worker.GetCompletedItem(value.ItemId);
					if (completedItem.IsRetryable && value.TimesAttempted < 3)
					{
						value.TimesAttempted++;
						this.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_ItemWillBeRetried, string.Empty, new object[]
						{
							value.Worker.ProvisioningAgentContext.TenantOrganization,
							value.TimesAttempted,
							value.Data.ToString()
						});
						value.Worker = null;
						value.LastAttempted = completedItem.TimeAttempted;
						this.queuedItems[value.ItemId] = value;
					}
					else
					{
						this.queuedItems.Remove(value.ItemId);
						this.completedItems.Add(completedItem.ItemId, completedItem);
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

		// Token: 0x04000059 RID: 89
		private const string EventLogSourceName = "MSExchange Bulk User Provisioning";

		// Token: 0x0400005A RID: 90
		private const int MaxPerItemRetryAttempts = 3;

		// Token: 0x0400005B RID: 91
		private const int MaxQueueLength = 100;

		// Token: 0x0400005C RID: 92
		private const int MaxItemsPerWorker = 25;

		// Token: 0x0400005D RID: 93
		private const string ProvisioningGuidStringR3 = "9132698f-5149-4949-a24f-1bb1928f9692";

		// Token: 0x0400005E RID: 94
		internal readonly ExEventLog EventLog = new ExEventLog(ProvisioningServicelet.ComponentGuid, "MSExchange Bulk User Provisioning", "MSExchange Bulk User Provisioning");

		// Token: 0x0400005F RID: 95
		private static readonly Guid ComponentGuid = new Guid("9132698f-5149-4949-a24f-1bb1928f9692");

		// Token: 0x04000060 RID: 96
		private static readonly TimeSpan minElapsedTimeBetweenItemRetry = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000061 RID: 97
		private static ProvisioningServicelet instance;

		// Token: 0x04000062 RID: 98
		private static object syncObject = new object();

		// Token: 0x04000063 RID: 99
		private static object updateQueueLock = new object();

		// Token: 0x04000064 RID: 100
		private SynchronizedDictionary<ObjectId, ProvisioningInfo> queuedItems = new SynchronizedDictionary<ObjectId, ProvisioningInfo>();

		// Token: 0x04000065 RID: 101
		private SynchronizedDictionary<ObjectId, ProvisionedObject> completedItems = new SynchronizedDictionary<ObjectId, ProvisionedObject>();

		// Token: 0x04000066 RID: 102
		private SynchronizedDictionary<Guid, ProvisioningAgentContext> registeredJobs = new SynchronizedDictionary<Guid, ProvisioningAgentContext>();
	}
}
