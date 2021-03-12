using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DefaultAdmissionControl : IResourceAdmissionControl
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00006EB4 File Offset: 0x000050B4
		public DefaultAdmissionControl(ResourceKey resourceKey, RemoveResourceDelegate removeResourceDelegate, ResourceAvailabilityChangeDelegate resourceAvailabilityChanged, string owner)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("owner", owner);
			this.ResourceKey = resourceKey;
			this.resourceAvailabilityChanged = resourceAvailabilityChanged;
			this.removeResourceDelegate = removeResourceDelegate;
			this.lastRefreshUtc = null;
			this.id = string.Concat(new string[]
			{
				owner,
				"_",
				resourceKey.Id,
				"_",
				Guid.NewGuid().ToString("N")
			});
			this.classificationData = new ClassificationDictionary<AdmissionClassificationData>((WorkloadClassification classification) => new AdmissionClassificationData(classification, this.id));
			this.monitor = ResourceHealthMonitorManager.Singleton.Get(this.ResourceKey);
			ushort slotBlockedEventBucketCount = this.GetSlotBlockedEventBucketCount();
			this.slotBlockedEvent = new LogEventIfSlotBlocked(this.monitor, slotBlockedEventBucketCount);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006FAF File Offset: 0x000051AF
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006FB6 File Offset: 0x000051B6
		public static Func<WorkloadClassification, bool> IsClassificationActiveDelegate { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006FC0 File Offset: 0x000051C0
		public TimeSpan RefreshCycle
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.SystemWorkloadManager.RefreshCycle;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006FEC File Offset: 0x000051EC
		public int MaxConcurrency
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.GetObject<IResourceSettings>(this.ResourceKey.MetricType, new object[0]).MaxConcurrency;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000702D File Offset: 0x0000522D
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007035 File Offset: 0x00005235
		public ResourceKey ResourceKey { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007040 File Offset: 0x00005240
		public bool IsAcquired
		{
			get
			{
				lock (this.instanceLock)
				{
					foreach (AdmissionClassificationData admissionClassificationData in this.classificationData.Values)
					{
						if (admissionClassificationData.ConcurrencyUsed > 0)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000070CC File Offset: 0x000052CC
		internal DateTime? LastRefreshUtc
		{
			get
			{
				return this.lastRefreshUtc;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000070D4 File Offset: 0x000052D4
		public bool TryAcquire(WorkloadClassification classification, out double delayFactor)
		{
			return this.TryAcquire(classification, DateTime.UtcNow, out delayFactor);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000070E3 File Offset: 0x000052E3
		public void Release(WorkloadClassification classification)
		{
			this.Release(classification, DateTime.UtcNow);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000070F4 File Offset: 0x000052F4
		internal bool TryAcquire(WorkloadClassification classification, DateTime utcNow, out double delayFactor)
		{
			int maxConcurrency = this.MaxConcurrency;
			this.VerifyOperational();
			this.Refresh(utcNow, classification);
			bool result;
			lock (this.instanceLock)
			{
				int num = 0;
				foreach (AdmissionClassificationData admissionClassificationData in this.classificationData.Values)
				{
					num += admissionClassificationData.ConcurrencyUsed;
					if (num >= maxConcurrency)
					{
						ExTraceGlobals.AdmissionControlTracer.TraceDebug<WorkloadClassification, int, ResourceKey>((long)this.GetHashCode(), "[DefaultAdmissionControl.TryAcquire] Unable to acquire slot for classification {0} because maximum concurrency {1} was reached for resource {2}.", classification, maxConcurrency, this.ResourceKey);
						delayFactor = 0.0;
						return false;
					}
				}
				AdmissionClassificationData admissionClassificationData2 = this.classificationData[classification];
				if (this.id.Equals(admissionClassificationData2.Id) && admissionClassificationData2.TryAquireSlot(out delayFactor))
				{
					this.RefreshAvailable(classification);
					result = true;
				}
				else
				{
					ExTraceGlobals.AdmissionControlTracer.TraceDebug((long)this.GetHashCode(), "[DefaultAdmissionControl.TryAcquire] Unable to acquire slot for classification {0} due to admission control limits.  Concurrency limit: {1}, Active slots: {2}, Resource: {3}", new object[]
					{
						classification,
						admissionClassificationData2.ConcurrencyLimits,
						admissionClassificationData2.ConcurrencyUsed,
						this.ResourceKey
					});
					delayFactor = 0.0;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007278 File Offset: 0x00005478
		internal void Release(WorkloadClassification classification, DateTime utcNow)
		{
			this.VerifyOperational();
			this.Refresh(utcNow, classification);
			lock (this.instanceLock)
			{
				AdmissionClassificationData admissionClassificationData = this.classificationData[classification];
				if (this.id.Equals(admissionClassificationData.Id))
				{
					admissionClassificationData.ReleaseSlot();
					this.RefreshAvailable(classification);
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000072F0 File Offset: 0x000054F0
		internal int GetConcurrencyLimit(WorkloadClassification classification, out double delayFactor)
		{
			return this.GetConcurrencyLimit(classification, DateTime.UtcNow, out delayFactor);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007300 File Offset: 0x00005500
		internal int GetConcurrencyLimit(WorkloadClassification classification, DateTime utcNow, out double delayFactor)
		{
			this.VerifyOperational();
			this.Refresh(utcNow, classification);
			int concurrencyLimits;
			lock (this.instanceLock)
			{
				delayFactor = this.classificationData[classification].DelayFactor;
				concurrencyLimits = this.classificationData[classification].ConcurrencyLimits;
			}
			return concurrencyLimits;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007370 File Offset: 0x00005570
		internal int GetActiveConcurrency(WorkloadClassification classification)
		{
			this.VerifyOperational();
			int concurrencyUsed;
			lock (this.instanceLock)
			{
				concurrencyUsed = this.classificationData[classification].ConcurrencyUsed;
			}
			return concurrencyUsed;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000073C4 File Offset: 0x000055C4
		internal bool GetClassificationAvailableAtLastRefresh(WorkloadClassification classification, DateTime utcNow)
		{
			this.VerifyOperational();
			this.Refresh(utcNow, classification);
			bool availableAtLastStatusChange;
			lock (this.instanceLock)
			{
				availableAtLastStatusChange = this.classificationData[classification].AvailableAtLastStatusChange;
			}
			return availableAtLastStatusChange;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007420 File Offset: 0x00005620
		internal void Test_Refresh(DateTime utcNow, WorkloadClassification classification)
		{
			this.Refresh(utcNow, classification);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000742A File Offset: 0x0000562A
		private static bool ClassificationIsActive(WorkloadClassification classification)
		{
			if (DefaultAdmissionControl.IsClassificationActiveDelegate != null)
			{
				return DefaultAdmissionControl.IsClassificationActiveDelegate(classification);
			}
			return SystemWorkloadManager.Status != WorkloadExecutionStatus.NotInitialized && SystemWorkloadManager.IsClassificationActive(classification);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007450 File Offset: 0x00005650
		private static void SafeTouchMonitor(DefaultAdmissionControl control, Action action)
		{
			LocalizedException ex = null;
			try
			{
				action();
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				control.Remove();
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_AdmissionControlRefreshFailure, control.ResourceKey.ToString(), new object[]
				{
					control.ResourceKey,
					ex
				});
				throw new NonOperationalAdmissionControlException(control.ResourceKey, ex);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007808 File Offset: 0x00005A08
		private void Refresh(DateTime utcNow, WorkloadClassification classification)
		{
			TimeSpan refreshCycle = this.RefreshCycle;
			DefaultAdmissionControl.SafeTouchMonitor(this, delegate
			{
				this.monitor.GetResourceLoad(classification, false, null);
				lock (this.instanceLock)
				{
					TimeSpan t = (this.lastRefreshUtc != null) ? (this.monitor.LastUpdateUtc - this.lastRefreshUtc.Value) : TimeSpan.MaxValue;
					if (t <= TimeSpan.Zero)
					{
						this.slotBlockedEvent.Set(false);
					}
					else
					{
						this.slotBlockedEvent.Set(true);
					}
					if (this.lastRefreshUtc == null || utcNow - this.lastRefreshUtc.Value >= refreshCycle)
					{
						if (t <= TimeSpan.Zero)
						{
							ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, string, DateTime>((long)this.GetHashCode(), "[DefaultAdmissionControl.Refresh] Will not refresh slots because monitor '{0}' has not been updated since last refresh.  Last Refresh: {1}, MonitorUpdate: {2}", this.ResourceKey, (this.lastRefreshUtc != null) ? this.lastRefreshUtc.Value.ToString(CultureInfo.InvariantCulture) : "Never", this.monitor.LastUpdateUtc);
						}
						else
						{
							foreach (AdmissionClassificationData admissionClassificationData in this.classificationData.Values)
							{
								if (DefaultAdmissionControl.ClassificationIsActive(admissionClassificationData.Classification))
								{
									ResourceLoad resourceLoad = this.monitor.GetResourceLoad(admissionClassificationData.Classification, admissionClassificationData.ConcurrencyLimits > 1, null);
									int concurrencyLimits = admissionClassificationData.ConcurrencyLimits;
									admissionClassificationData.Refresh(this.ResourceKey, resourceLoad);
									if (concurrencyLimits > 0 && admissionClassificationData.ConcurrencyLimits == 0)
									{
										IResourceLoadNotification resourceLoadNotification = this.monitor as IResourceLoadNotification;
										if (resourceLoadNotification != null)
										{
											ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification>((long)this.GetHashCode(), "[DefaultAdmissionControl.Refresh] Registering for health notification - resource: {0}, Classification: {1}", this.ResourceKey, admissionClassificationData.Classification);
											resourceLoadNotification.SubscribeToHealthNotifications(admissionClassificationData.Classification, new HealthRecoveryNotification(this.HandleHealthRecovery));
										}
									}
									this.RefreshAvailable(admissionClassificationData.Classification);
									SystemWorkloadManagerLogEntry value = null;
									this.lastEntries.TryGetValue(admissionClassificationData.Classification, out value);
									SystemWorkloadManagerBlackBox.RecordAdmissionUpdate(ref value, this.ResourceKey, admissionClassificationData.Classification, resourceLoad, admissionClassificationData.ConcurrencyLimits, admissionClassificationData.DelayFactor > 0.0);
									this.lastEntries[admissionClassificationData.Classification] = value;
								}
							}
							this.lastRefreshUtc = new DateTime?(utcNow);
						}
					}
				}
			});
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007850 File Offset: 0x00005A50
		private void Remove()
		{
			lock (this.instanceLock)
			{
				this.operational = false;
				if (this.removeResourceDelegate != null)
				{
					this.removeResourceDelegate(this.ResourceKey);
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000078C8 File Offset: 0x00005AC8
		private void HandleHealthRecovery(ResourceKey key, WorkloadClassification classification, Guid notificationCookie)
		{
			ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification>((long)this.GetHashCode(), "[DefaultAdmissionControl.HandleHealthRecovery] Resource '{0}' recovered for classification '{1}'.", key, classification);
			if (this.operational)
			{
				try
				{
					DefaultAdmissionControl.SafeTouchMonitor(this, delegate
					{
						this.RefreshHealthForClassification(classification);
					});
				}
				catch (NonOperationalAdmissionControlException)
				{
					ExTraceGlobals.AdmissionControlTracer.TraceDebug((long)this.GetHashCode(), "[DefaultAdmissionControl.HandleHealthRecovery] Caught NonOperationalAdmissionControlException though this is expected.  Ignoring.");
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007954 File Offset: 0x00005B54
		private void RefreshAvailable(WorkloadClassification classification)
		{
			bool flag2;
			bool availableAtLastStatusChange;
			lock (this.instanceLock)
			{
				AdmissionClassificationData admissionClassificationData = this.classificationData[classification];
				flag2 = admissionClassificationData.RefreshAvailable();
				availableAtLastStatusChange = admissionClassificationData.AvailableAtLastStatusChange;
			}
			if (flag2 && this.resourceAvailabilityChanged != null)
			{
				this.resourceAvailabilityChanged(this.ResourceKey, classification, availableAtLastStatusChange);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000079CC File Offset: 0x00005BCC
		private void RefreshHealthForClassification(WorkloadClassification classification)
		{
			bool flag2;
			bool availableAtLastStatusChange;
			lock (this.instanceLock)
			{
				AdmissionClassificationData admissionClassificationData = this.classificationData[classification];
				ResourceLoad resourceLoad = this.monitor.GetResourceLoad(admissionClassificationData.Classification, admissionClassificationData.ConcurrencyLimits > 1, null);
				admissionClassificationData.Refresh(this.ResourceKey, resourceLoad);
				flag2 = admissionClassificationData.RefreshAvailable();
				availableAtLastStatusChange = admissionClassificationData.AvailableAtLastStatusChange;
			}
			if (flag2 && this.resourceAvailabilityChanged != null)
			{
				this.resourceAvailabilityChanged(this.ResourceKey, classification, availableAtLastStatusChange);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007A6C File Offset: 0x00005C6C
		private void VerifyOperational()
		{
			if (!this.operational)
			{
				throw new NonOperationalAdmissionControlException(this.ResourceKey);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007A84 File Offset: 0x00005C84
		private ushort GetSlotBlockedEventBucketCount()
		{
			int val = (int)Math.Ceiling(this.RefreshCycle.TotalMinutes);
			return (ushort)Math.Max(val, 5);
		}

		// Token: 0x040000CF RID: 207
		private const int MinimumBucketsForSlotBlockedEvent = 5;

		// Token: 0x040000D0 RID: 208
		private readonly string id = string.Empty;

		// Token: 0x040000D1 RID: 209
		private IResourceLoadMonitor monitor;

		// Token: 0x040000D2 RID: 210
		private object instanceLock = new object();

		// Token: 0x040000D3 RID: 211
		private ClassificationDictionary<AdmissionClassificationData> classificationData;

		// Token: 0x040000D4 RID: 212
		private DateTime? lastRefreshUtc;

		// Token: 0x040000D5 RID: 213
		private ResourceAvailabilityChangeDelegate resourceAvailabilityChanged;

		// Token: 0x040000D6 RID: 214
		private RemoveResourceDelegate removeResourceDelegate;

		// Token: 0x040000D7 RID: 215
		private LogEventIfSlotBlocked slotBlockedEvent;

		// Token: 0x040000D8 RID: 216
		private bool operational = true;

		// Token: 0x040000D9 RID: 217
		private ClassificationDictionary<SystemWorkloadManagerLogEntry> lastEntries = new ClassificationDictionary<SystemWorkloadManagerLogEntry>();
	}
}
