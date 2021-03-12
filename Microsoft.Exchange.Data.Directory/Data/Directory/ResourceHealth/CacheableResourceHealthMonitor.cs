using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009EC RID: 2540
	internal abstract class CacheableResourceHealthMonitor
	{
		// Token: 0x17002A38 RID: 10808
		// (get) Token: 0x060075D9 RID: 30169 RVA: 0x00183BCA File Offset: 0x00181DCA
		// (set) Token: 0x060075DA RID: 30170 RVA: 0x00183BD1 File Offset: 0x00181DD1
		public static TimeSpan NotificationCheckFrequency { get; set; } = CacheableResourceHealthMonitor.DefaultNotificationCheckFrequency;

		// Token: 0x060075DB RID: 30171 RVA: 0x00183BDC File Offset: 0x00181DDC
		static CacheableResourceHealthMonitor()
		{
			IntAppSettingsEntry intAppSettingsEntry = new IntAppSettingsEntry("ResourceMonitor.NumberOfAdjustmentSteps", 5, ExTraceGlobals.ResourceHealthManagerTracer);
			CacheableResourceHealthMonitor.numberOfAdjustmentSteps = intAppSettingsEntry.Value;
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x00183C28 File Offset: 0x00181E28
		protected CacheableResourceHealthMonitor(ResourceKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.resourceKey = key;
			this.notifications = new List<CacheableResourceHealthMonitor.NotificationWrapper>();
			this.notificationCount = 0;
			this.LastAccessUtc = TimeProvider.UtcNow;
			this.CreationStack = new StackTrace().ToString();
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x00183CB4 File Offset: 0x00181EB4
		public virtual ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new ResourceHealthMonitorWrapper(this);
		}

		// Token: 0x17002A39 RID: 10809
		// (get) Token: 0x060075DE RID: 30174 RVA: 0x00183CBC File Offset: 0x00181EBC
		// (set) Token: 0x060075DF RID: 30175 RVA: 0x00183CC4 File Offset: 0x00181EC4
		public virtual bool Expired { get; private set; }

		// Token: 0x17002A3A RID: 10810
		// (get) Token: 0x060075E0 RID: 30176 RVA: 0x00183CCD File Offset: 0x00181ECD
		public ResourceKey Key
		{
			get
			{
				return this.resourceKey;
			}
		}

		// Token: 0x17002A3B RID: 10811
		// (get) Token: 0x060075E1 RID: 30177 RVA: 0x00183CD5 File Offset: 0x00181ED5
		// (set) Token: 0x060075E2 RID: 30178 RVA: 0x00183CDD File Offset: 0x00181EDD
		public virtual DateTime LastAccessUtc { get; protected set; }

		// Token: 0x17002A3C RID: 10812
		// (get) Token: 0x060075E3 RID: 30179 RVA: 0x00183CE6 File Offset: 0x00181EE6
		// (set) Token: 0x060075E4 RID: 30180 RVA: 0x00183CEE File Offset: 0x00181EEE
		internal string CreationStack { get; private set; }

		// Token: 0x060075E5 RID: 30181 RVA: 0x00183CF7 File Offset: 0x00181EF7
		internal void Expire()
		{
			this.Expired = true;
			this.HandleExpired();
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x00183D06 File Offset: 0x00181F06
		protected virtual void HandleExpired()
		{
		}

		// Token: 0x060075E7 RID: 30183 RVA: 0x00183D08 File Offset: 0x00181F08
		public virtual bool ShouldRemoveResourceFromCache()
		{
			return this.notificationCount == 0;
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x00183D14 File Offset: 0x00181F14
		public Guid SubscribeToHealthNotifications(WorkloadClassification classification, HealthRecoveryNotification delegateToFire)
		{
			if (delegateToFire == null)
			{
				throw new ArgumentNullException("delegateToFire");
			}
			if (classification == WorkloadClassification.Unknown)
			{
				throw new ArgumentException("You cannot use Unknown to register for health change notifications.", "classification");
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey, WorkloadClassification>((long)this.GetHashCode(), "[CacheableResourceHealthMonitor.SubscribeToHealthNotifications] Registration was made for resource '{0}', classification: {1}.", this.Key, classification);
			Guid key;
			lock (this.instanceLock)
			{
				CacheableResourceHealthMonitor.NotificationWrapper notificationWrapper = new CacheableResourceHealthMonitor.NotificationWrapper(classification, delegateToFire);
				this.notifications.Add(notificationWrapper);
				this.notificationCount++;
				key = notificationWrapper.Key;
			}
			return key;
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x00183DB8 File Offset: 0x00181FB8
		public bool UnsubscribeFromHealthNotifications(Guid registrationKey)
		{
			if (registrationKey == Guid.Empty)
			{
				throw new ArgumentException("Guid.Empty is never an acceptable registration key", "registrationKey");
			}
			bool flag = false;
			lock (this.instanceLock)
			{
				if (this.notificationCount > 0)
				{
					for (int i = this.notifications.Count - 1; i >= 0; i--)
					{
						CacheableResourceHealthMonitor.NotificationWrapper notificationWrapper = this.notifications[i];
						if (notificationWrapper.Key == registrationKey)
						{
							this.notifications.RemoveAt(i);
							this.notificationCount--;
							flag = true;
							break;
						}
					}
				}
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey, Guid, bool>((long)this.GetHashCode(), "[CacheableResourceHealthMonitor.UnsubscribeFromHealthNotifications] Unregistered for Resource Key: '{0}', Guid: '{1}', Found? {2}", this.Key, registrationKey, flag);
			return flag;
		}

		// Token: 0x17002A3D RID: 10813
		// (get) Token: 0x060075EA RID: 30186 RVA: 0x00183E8C File Offset: 0x0018208C
		public int HealthSubscriptionCount
		{
			get
			{
				return this.notificationCount;
			}
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x00183E94 File Offset: 0x00182094
		internal int FireNotifications()
		{
			int num = 0;
			List<CacheableResourceHealthMonitor.NotificationWrapper> list = null;
			if (CacheableResourceHealthMonitor.NotificationCheckFrequency == TimeSpan.Zero || TimeProvider.UtcNow - this.lastNotificationCheckUtc >= CacheableResourceHealthMonitor.NotificationCheckFrequency)
			{
				lock (this.instanceLock)
				{
					if (CacheableResourceHealthMonitor.NotificationCheckFrequency == TimeSpan.Zero || TimeProvider.UtcNow - this.lastNotificationCheckUtc >= CacheableResourceHealthMonitor.NotificationCheckFrequency)
					{
						for (int i = this.notifications.Count - 1; i >= 0; i--)
						{
							CacheableResourceHealthMonitor.NotificationWrapper notificationWrapper = this.notifications[i];
							if (this.GetResourceLoad(notificationWrapper.Classification, false, null).State == ResourceLoadState.Underloaded)
							{
								if (list == null)
								{
									list = new List<CacheableResourceHealthMonitor.NotificationWrapper>();
								}
								list.Add(notificationWrapper);
								this.notificationCount--;
								num++;
								this.notifications.RemoveAt(i);
							}
						}
					}
					this.lastNotificationCheckUtc = TimeProvider.UtcNow;
				}
			}
			if (list != null)
			{
				this.FireNotificationsAsync(list);
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int, ResourceKey>((long)this.GetHashCode(), "[CacheableResourceHealthMonitor.FireNotifications] Firing off {0} notifications for resource '{1}'", num, this.Key);
			}
			return num;
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x0018404C File Offset: 0x0018224C
		private void FireNotificationsAsync(List<CacheableResourceHealthMonitor.NotificationWrapper> notifications)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				foreach (CacheableResourceHealthMonitor.NotificationWrapper notificationWrapper in notifications)
				{
					notificationWrapper.Notification(this.Key, notificationWrapper.Classification, notificationWrapper.Key);
				}
			});
		}

		// Token: 0x17002A3E RID: 10814
		// (get) Token: 0x060075ED RID: 30189 RVA: 0x0018407F File Offset: 0x0018227F
		public ResourceMetricType MetricType
		{
			get
			{
				return this.Key.MetricType;
			}
		}

		// Token: 0x17002A3F RID: 10815
		// (get) Token: 0x060075EE RID: 30190 RVA: 0x0018408C File Offset: 0x0018228C
		// (set) Token: 0x060075EF RID: 30191 RVA: 0x001840A7 File Offset: 0x001822A7
		public virtual DateTime LastUpdateUtc
		{
			get
			{
				if (!this.Settings.Enabled)
				{
					return DateTime.UtcNow;
				}
				return this.lastUpdateUtc;
			}
			protected internal set
			{
				this.lastUpdateUtc = value;
			}
		}

		// Token: 0x17002A40 RID: 10816
		// (get) Token: 0x060075F0 RID: 30192 RVA: 0x001840B0 File Offset: 0x001822B0
		public int MetricValue
		{
			get
			{
				this.LastAccessUtc = TimeProvider.UtcNow;
				return this.InternalMetricValue;
			}
		}

		// Token: 0x17002A41 RID: 10817
		// (get) Token: 0x060075F1 RID: 30193
		protected abstract int InternalMetricValue { get; }

		// Token: 0x060075F2 RID: 30194 RVA: 0x001840C3 File Offset: 0x001822C3
		public int GetMetricValue(object optionalData)
		{
			this.LastAccessUtc = TimeProvider.UtcNow;
			return this.InternalGetMetricValue(optionalData);
		}

		// Token: 0x060075F3 RID: 30195 RVA: 0x001840D7 File Offset: 0x001822D7
		protected virtual int InternalGetMetricValue(object optionalData)
		{
			return this.InternalMetricValue;
		}

		// Token: 0x17002A42 RID: 10818
		// (get) Token: 0x060075F4 RID: 30196 RVA: 0x001840E0 File Offset: 0x001822E0
		protected IResourceSettings Settings
		{
			get
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
				return snapshot.WorkloadManagement.GetObject<IResourceSettings>(this.MetricType, new object[0]);
			}
		}

		// Token: 0x060075F5 RID: 30197 RVA: 0x0018411C File Offset: 0x0018231C
		public virtual ResourceLoad GetResourceLoad(WorkloadType type, bool raw = false, object optionalData = null)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			IWorkloadSettings @object = snapshot.WorkloadManagement.GetObject<IWorkloadSettings>(type, new object[0]);
			return this.GetResourceLoad(@object.Classification, raw, optionalData);
		}

		// Token: 0x060075F6 RID: 30198 RVA: 0x00184160 File Offset: 0x00182360
		public virtual ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null)
		{
			if (!this.Settings.Enabled)
			{
				return ResourceLoad.Zero;
			}
			int metricValue = this.GetMetricValue(optionalData);
			if (metricValue < 0)
			{
				return ResourceLoad.Unknown;
			}
			ResourceMetricPolicy resourceMetricPolicy = new ResourceMetricPolicy(this.MetricType, classification, this.Settings);
			ResourceLoad resourceLoad = resourceMetricPolicy.InterpretMetricValue(metricValue);
			if (!raw)
			{
				CacheableResourceHealthMonitor.LoadInfo loadInfo = this.loadInfo[classification];
				if (this.UpdateIsNeeded(loadInfo, resourceLoad))
				{
					try
					{
						if (Monitor.TryEnter(this.instanceLock) && this.UpdateIsNeeded(loadInfo, resourceLoad))
						{
							switch (resourceLoad.State)
							{
							case ResourceLoadState.Unknown:
								loadInfo.Load = resourceLoad;
								break;
							case ResourceLoadState.Underloaded:
							case ResourceLoadState.Full:
								if (loadInfo.Load < ResourceLoad.Full)
								{
									loadInfo.Load = resourceLoad;
								}
								else if (this.LastUpdateUtc > loadInfo.UpdateUtc)
								{
									if (loadInfo.Load == ResourceLoad.Full)
									{
										loadInfo.Load = resourceLoad;
									}
									else if (loadInfo.Load == ResourceLoad.Critical)
									{
										loadInfo.Load = new ResourceLoad(resourceMetricPolicy.MaxOverloaded.LoadRatio, new int?(metricValue), null);
									}
									else if (resourceLoad.State == ResourceLoadState.Underloaded)
									{
										double num = (resourceMetricPolicy.MaxOverloaded - ResourceLoad.Full) / (double)CacheableResourceHealthMonitor.numberOfAdjustmentSteps;
										if (loadInfo.Load - ResourceLoad.Full > num)
										{
											loadInfo.Load -= num;
										}
										else
										{
											loadInfo.Load = new ResourceLoad(ResourceLoad.Full.LoadRatio, new int?(metricValue), null);
										}
									}
								}
								break;
							case ResourceLoadState.Overloaded:
								if (loadInfo.Load < resourceLoad)
								{
									loadInfo.Load = resourceLoad;
								}
								else if (this.LastUpdateUtc > loadInfo.UpdateUtc)
								{
									double delta = (resourceMetricPolicy.MaxOverloaded - ResourceLoad.Full) / (double)CacheableResourceHealthMonitor.numberOfAdjustmentSteps;
									if (loadInfo.Load + delta <= resourceMetricPolicy.MaxOverloaded)
									{
										loadInfo.Load += delta;
									}
									else
									{
										loadInfo.Load = new ResourceLoad(ResourceLoad.Critical.LoadRatio, new int?(metricValue), null);
									}
								}
								break;
							case ResourceLoadState.Critical:
								loadInfo.Load = resourceLoad;
								break;
							}
						}
					}
					finally
					{
						if (Monitor.IsEntered(this.instanceLock))
						{
							Monitor.Exit(this.instanceLock);
						}
					}
				}
				resourceLoad = loadInfo.Load;
			}
			object resourceLoadlInfo = this.GetResourceLoadlInfo(resourceLoad);
			if (resourceLoadlInfo != null)
			{
				resourceLoad = new ResourceLoad(resourceLoad.LoadRatio, resourceLoad.Metric, resourceLoadlInfo);
			}
			ResourceLoadPerfCounterWrapper.Get(this.resourceKey, classification).Update(metricValue, resourceLoad);
			lock (this.lastEntries)
			{
				SystemWorkloadManagerLogEntry value = null;
				this.lastEntries.TryGetValue(classification, out value);
				SystemWorkloadManagerBlackBox.RecordMonitorUpdate(ref value, this.resourceKey, classification, resourceLoad);
				this.lastEntries[classification] = value;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceMetricType, ResourceLoad>((long)this.GetHashCode(), "[ResourceLoadMonitor.GetResourceLoad] MetricType={0}, Current Load={1}.", this.MetricType, resourceLoad);
			return resourceLoad;
		}

		// Token: 0x060075F7 RID: 30199 RVA: 0x001844BC File Offset: 0x001826BC
		protected virtual object GetResourceLoadlInfo(ResourceLoad load)
		{
			return null;
		}

		// Token: 0x060075F8 RID: 30200 RVA: 0x001844C0 File Offset: 0x001826C0
		private static Dictionary<WorkloadClassification, CacheableResourceHealthMonitor.LoadInfo> CreateLoadInfoDictionary()
		{
			Dictionary<WorkloadClassification, CacheableResourceHealthMonitor.LoadInfo> dictionary = new Dictionary<WorkloadClassification, CacheableResourceHealthMonitor.LoadInfo>();
			foreach (object obj in Enum.GetValues(typeof(WorkloadClassification)))
			{
				WorkloadClassification workloadClassification = (WorkloadClassification)obj;
				if (workloadClassification != WorkloadClassification.Unknown)
				{
					dictionary.Add(workloadClassification, new CacheableResourceHealthMonitor.LoadInfo());
				}
			}
			return dictionary;
		}

		// Token: 0x060075F9 RID: 30201 RVA: 0x00184534 File Offset: 0x00182734
		private bool UpdateIsNeeded(CacheableResourceHealthMonitor.LoadInfo loadInfo, ResourceLoad newLoad)
		{
			return (loadInfo.Load == ResourceLoad.Unknown && newLoad != ResourceLoad.Unknown) || this.LastUpdateUtc > loadInfo.UpdateUtc || newLoad == ResourceLoad.Critical || newLoad > loadInfo.Load;
		}

		// Token: 0x04004B92 RID: 19346
		private const int DefaultNumberOfAdjustmentSteps = 5;

		// Token: 0x04004B93 RID: 19347
		private const string ConfigNumberOfAdjustmentSteps = "ResourceMonitor.NumberOfAdjustmentSteps";

		// Token: 0x04004B94 RID: 19348
		protected const int UnknownMetricValue = -1;

		// Token: 0x04004B95 RID: 19349
		private static readonly TimeSpan DefaultNotificationCheckFrequency = TimeSpan.FromSeconds(10.0);

		// Token: 0x04004B96 RID: 19350
		private static int numberOfAdjustmentSteps = 5;

		// Token: 0x04004B97 RID: 19351
		private ResourceKey resourceKey;

		// Token: 0x04004B98 RID: 19352
		private object instanceLock = new object();

		// Token: 0x04004B99 RID: 19353
		private List<CacheableResourceHealthMonitor.NotificationWrapper> notifications;

		// Token: 0x04004B9A RID: 19354
		private int notificationCount;

		// Token: 0x04004B9B RID: 19355
		private DateTime lastNotificationCheckUtc = TimeProvider.UtcNow;

		// Token: 0x04004B9C RID: 19356
		private DateTime lastUpdateUtc = DateTime.MinValue;

		// Token: 0x04004B9D RID: 19357
		private Dictionary<WorkloadClassification, SystemWorkloadManagerLogEntry> lastEntries = new Dictionary<WorkloadClassification, SystemWorkloadManagerLogEntry>();

		// Token: 0x04004B9E RID: 19358
		private Dictionary<WorkloadClassification, CacheableResourceHealthMonitor.LoadInfo> loadInfo = CacheableResourceHealthMonitor.CreateLoadInfoDictionary();

		// Token: 0x020009ED RID: 2541
		private class NotificationWrapper
		{
			// Token: 0x060075FA RID: 30202 RVA: 0x00184597 File Offset: 0x00182797
			public NotificationWrapper(WorkloadClassification classification, HealthRecoveryNotification delegateToFire)
			{
				this.Classification = classification;
				this.Notification = delegateToFire;
				this.Key = Guid.NewGuid();
			}

			// Token: 0x17002A43 RID: 10819
			// (get) Token: 0x060075FB RID: 30203 RVA: 0x001845B8 File Offset: 0x001827B8
			// (set) Token: 0x060075FC RID: 30204 RVA: 0x001845C0 File Offset: 0x001827C0
			public HealthRecoveryNotification Notification { get; private set; }

			// Token: 0x17002A44 RID: 10820
			// (get) Token: 0x060075FD RID: 30205 RVA: 0x001845C9 File Offset: 0x001827C9
			// (set) Token: 0x060075FE RID: 30206 RVA: 0x001845D1 File Offset: 0x001827D1
			public Guid Key { get; private set; }

			// Token: 0x17002A45 RID: 10821
			// (get) Token: 0x060075FF RID: 30207 RVA: 0x001845DA File Offset: 0x001827DA
			// (set) Token: 0x06007600 RID: 30208 RVA: 0x001845E2 File Offset: 0x001827E2
			public WorkloadClassification Classification { get; private set; }
		}

		// Token: 0x020009EE RID: 2542
		private class LoadInfo
		{
			// Token: 0x06007601 RID: 30209 RVA: 0x001845EB File Offset: 0x001827EB
			public LoadInfo()
			{
				this.Load = ResourceLoad.Unknown;
			}

			// Token: 0x17002A46 RID: 10822
			// (get) Token: 0x06007602 RID: 30210 RVA: 0x001845FE File Offset: 0x001827FE
			// (set) Token: 0x06007603 RID: 30211 RVA: 0x00184606 File Offset: 0x00182806
			public DateTime UpdateUtc { get; private set; }

			// Token: 0x17002A47 RID: 10823
			// (get) Token: 0x06007604 RID: 30212 RVA: 0x0018460F File Offset: 0x0018280F
			// (set) Token: 0x06007605 RID: 30213 RVA: 0x00184617 File Offset: 0x00182817
			public ResourceLoad Load
			{
				get
				{
					return this.load;
				}
				set
				{
					this.load = value;
					this.UpdateUtc = TimeProvider.UtcNow;
				}
			}

			// Token: 0x04004BA6 RID: 19366
			private ResourceLoad load;
		}
	}
}
