using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000042 RID: 66
	internal sealed class ResourceManager
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00006F14 File Offset: 0x00005114
		public ResourceManager(ResourceManagerConfiguration resourceManagerConfig, ResourceMonitorFactory resourceMonitorFactory, ResourceManagerEventLogger eventLogger, ResourceManagerComponentsAdapter componentsAdapter, ResourceManagerResources resourcesToMonitor, ResourceLog resourceLog)
		{
			ArgumentValidator.ThrowIfNull("resourceManagerConfig", resourceManagerConfig);
			ArgumentValidator.ThrowIfNull("resourceMonitorFactory", resourceMonitorFactory);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			ArgumentValidator.ThrowIfNull("componentsAdapter", componentsAdapter);
			ArgumentValidator.ThrowIfNull("resourceLog", resourceLog);
			this.components = componentsAdapter;
			this.resourcesToMonitor = resourcesToMonitor;
			this.resourceManagerConfig = resourceManagerConfig;
			this.resourceMonitorFactory = resourceMonitorFactory;
			this.eventLogger = eventLogger;
			this.resourceLog = resourceLog;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006FFA File Offset: 0x000051FA
		public bool IsMonitoringEnabled
		{
			get
			{
				return this.resourceManagerConfig.EnableResourceMonitoring;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007007 File Offset: 0x00005207
		public TimeSpan MonitorInterval
		{
			get
			{
				return this.resourceManagerConfig.ResourceMonitoringInterval;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007014 File Offset: 0x00005214
		public ResourceUses CurrentPrivateBytesUses
		{
			get
			{
				ResourceUses result;
				try
				{
					this.updateLock.AcquireReaderLock(-1);
					result = this.currentPrivateBytesUses;
				}
				finally
				{
					this.updateLock.ReleaseReaderLock();
				}
				return result;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007054 File Offset: 0x00005254
		public DateTime LastTimeResourceMonitored
		{
			get
			{
				return this.lastTimeResourceMonitored;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000705C File Offset: 0x0000525C
		public bool ShouldShrinkDownMemoryCaches
		{
			get
			{
				return this.IsMonitoringEnabled && (this.resourceMonitors[3].CurrentResourceUsesRaw > ResourceUses.Normal || this.resourceMonitors[3].ResourceUses > ResourceUses.Normal || this.resourceMonitors[4].ResourceUses > ResourceUses.Normal);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000070B4 File Offset: 0x000052B4
		public void OnMonitorResource(object obj)
		{
			this.lastTimeResourceMonitored = DateTime.UtcNow;
			foreach (ResourceMonitor resourceMonitor in this.resourceMonitors)
			{
				resourceMonitor.UpdateReading();
			}
			ResourceMonitor resourceMonitor2 = this.resourceMonitors[3];
			if (resourceMonitor2.CurrentResourceUsesRaw > ResourceUses.Normal && !this.collectingGarbage)
			{
				bool flag = resourceMonitor2.PreviousResourceUses != resourceMonitor2.ResourceUses;
				DateTime utcNow = DateTime.UtcNow;
				if (flag || utcNow > this.lastTimeGCCollectCalled.Add(this.gcCollectInterval))
				{
					int num = GC.CollectionCount(2);
					if (flag || this.lastGCCollectionCount == -1 || num == this.lastGCCollectionCount)
					{
						this.collectingGarbage = true;
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoGarbageCollection));
					}
					else
					{
						this.lastGCCollectionCount = num;
					}
				}
			}
			ComponentsState componentsState = ComponentsState.AllowAllComponents;
			ResourceMonitor resourceMonitor3 = this.resourceMonitors[2];
			ResourceMonitor resourceMonitor4 = this.resourceMonitors[5];
			bool flag2 = false;
			if (resourceMonitor3.CurrentPressureRaw >= resourceMonitor3.LowPressureLimit || resourceMonitor4.CurrentPressureRaw >= resourceMonitor4.LowPressureLimit)
			{
				ExTraceGlobals.ResourceManagerTracer.TraceDebug(0L, "CurrentPressureRaw = {0}, CurrentPressureStabilized = {1}, ResourceUsesRaw = {2}, ResourceUsesStabilized = {3}.", new object[]
				{
					resourceMonitor3.CurrentPressureRaw,
					resourceMonitor3.CurrentPressure,
					resourceMonitor3.CurrentResourceUsesRaw,
					resourceMonitor3.ResourceUses
				});
				flag2 = true;
				componentsState &= ~ComponentsState.AllowContentAggregation;
			}
			if (flag2)
			{
				this.smtpThrottlingController.Increase();
			}
			else
			{
				this.smtpThrottlingController.Decrease();
			}
			if (this.components.SmtpInComponent != null)
			{
				TimeSpan current = this.smtpThrottlingController.GetCurrent();
				if (current == TimeSpan.Zero)
				{
					this.components.SmtpInComponent.SetThrottleDelay(current, null);
				}
				else
				{
					string throttleDelayContext = string.Format(CultureInfo.InvariantCulture, "VB={0};QS={1}", new object[]
					{
						resourceMonitor3.CurrentPressureRaw,
						resourceMonitor4.CurrentPressureRaw
					});
					this.components.SmtpInComponent.SetThrottleDelay(current, throttleDelayContext);
				}
			}
			foreach (ResourceMonitor resourceMonitor5 in this.resourceMonitors)
			{
				resourceMonitor5.DoCleanup();
			}
			this.PostMonitorResource(componentsState);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007350 File Offset: 0x00005550
		public void HintGCCollectCouldBeEffective()
		{
			this.gcCollectInterval = ResourceManager.gcCollectLowInterval;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007360 File Offset: 0x00005560
		public void Load()
		{
			if (this.IsMonitoringEnabled)
			{
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.MailDatabase) > ResourceManagerResources.None) ? this.resourceMonitorFactory.MailDatabaseMonitor : new NullResourceMonitor("NullMailDatabaseMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.MailDatabaseLoggingFolder) > ResourceManagerResources.None) ? this.resourceMonitorFactory.MailDatabaseLoggingFolderMonitor : new NullResourceMonitor("NullMailDatabaseLoggingFolderMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.VersionBuckets) > ResourceManagerResources.None) ? this.resourceMonitorFactory.VersionBucketResourceMonitor : new NullResourceMonitor("NullVersionBucketsMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.PrivateBytes) > ResourceManagerResources.None) ? this.resourceMonitorFactory.MemoryPrivateBytesMonitor : new NullResourceMonitor("NullPrivateBytesMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.TotalBytes) > ResourceManagerResources.None) ? this.resourceMonitorFactory.MemoryTotalBytesMonitor : new NullResourceMonitor("NullTotalBytesMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.SubmissionQueue) > ResourceManagerResources.None) ? this.resourceMonitorFactory.SubmissionQueueMonitor : new NullResourceMonitor("NullSubmissionQueueMonitor"));
				this.resourceMonitors.Add(((this.resourcesToMonitor & ResourceManagerResources.TempDrive) > ResourceManagerResources.None) ? this.resourceMonitorFactory.TempDriveMonitor : new NullResourceMonitor("NullSubmissionQueueMonitor"));
				foreach (ResourceMonitor resourceMonitor in this.resourceMonitors)
				{
					resourceMonitor.UpdateConfig();
				}
				this.smtpThrottlingController = new ThrottlingController(ExTraceGlobals.ResourceManagerTracer, this.resourceManagerConfig.ThrottlingControllerConfig);
				this.OnMonitorResource(null);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007520 File Offset: 0x00005720
		public void Unload()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007522 File Offset: 0x00005722
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007525 File Offset: 0x00005725
		public void RefreshComponentsState()
		{
			this.RefreshComponentsState(ComponentsState.AllowAllComponents);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007534 File Offset: 0x00005734
		public void RefreshComponentsState(ComponentsState requiredComponentsState)
		{
			if (this.IsMonitoringEnabled)
			{
				ResourceUses resourceUses;
				ResourceUses resourceUses2;
				ResourceUses resourceUses3;
				ResourceUses resourceUses4;
				try
				{
					this.updateLock.AcquireReaderLock(-1);
					resourceUses = this.currentResourceUses;
					resourceUses2 = this.currentPrivateBytesUses;
					resourceUses3 = this.currentVersionBucketUses;
					resourceUses4 = this.currentSubmissionQueueUses;
				}
				finally
				{
					this.updateLock.ReleaseReaderLock();
				}
				if (resourceUses == ResourceUses.High)
				{
					ExTraceGlobals.ResourceManagerTracer.TraceDebug(0L, "BackPressure: High: Disable SmtpIn (from organization and internet), Pickup, Replay, Aggregation, and Store Driver (inbound submission from mailbox)");
					requiredComponentsState &= ComponentsState.HighResourcePressureState;
				}
				else if (resourceUses == ResourceUses.Medium)
				{
					ExTraceGlobals.ResourceManagerTracer.TraceDebug(0L, "BackPressure: Medium: Disable incomding email from Internet but allow incoming email from Organization. Disable Pick, Replay, Aggregation. Enable Store Driver (inbound submission from mailbox).");
					requiredComponentsState &= ComponentsState.MediumResourcePressureState;
				}
				else
				{
					ExTraceGlobals.ResourceManagerTracer.TraceDebug(0L, "BackPressure: Normal: Enable SmtpIn from Internet and Organization, Pickup, Replay, Aggregation, and Store Driver (inbound submission from mailbox).");
				}
				if (resourceUses2 > ResourceUses.Normal && resourceUses2 > this.resourceMonitors[3].PreviousResourceUses && !this.components.ShuttingDown && this.components.IsActive)
				{
					lock (this.components.SyncRoot)
					{
						if (this.components.TransportIsMemberOfResolverComponent != null && !this.components.ShuttingDown && this.components.IsActive)
						{
							this.components.TransportIsMemberOfResolverComponent.ClearCache();
						}
					}
				}
				if (resourceUses2 > ResourceUses.Normal || resourceUses4 > ResourceUses.Normal)
				{
					requiredComponentsState &= ~ComponentsState.AllowBootScannerRunning;
				}
				if (resourceUses3 > ResourceUses.Normal)
				{
					requiredComponentsState &= ~ComponentsState.AllowOutboundMailDeliveryToRemoteDomains;
				}
			}
			this.components.UpdateComponentsState(requiredComponentsState);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000076A4 File Offset: 0x000058A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			string text = this.components.ToString();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			foreach (ResourceMonitor resourceMonitor in this.resourceMonitors)
			{
				if (resourceMonitor.ResourceUses == ResourceUses.Normal)
				{
					stringBuilder2.AppendLine(resourceMonitor.ToString());
				}
				else
				{
					stringBuilder3.AppendLine(resourceMonitor.ToString());
				}
			}
			if (stringBuilder3.Length > 0)
			{
				stringBuilder.AppendLine(Strings.ResourcesInAboveNormalPressure(stringBuilder3.ToString()));
			}
			if (text.Length > 0)
			{
				stringBuilder.AppendLine(Strings.ComponentsDisabledByBackPressure(text));
			}
			else
			{
				stringBuilder.AppendLine(Strings.ComponentsDisabledNone);
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.AppendLine(Strings.ResourcesInNormalPressure(stringBuilder2.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000077BC File Offset: 0x000059BC
		internal static string MapToLocalizedString(ResourceUses resourceUses)
		{
			string result = string.Empty;
			switch (resourceUses)
			{
			case ResourceUses.Normal:
				result = Strings.NormalResourceUses;
				break;
			case ResourceUses.Medium:
				result = Strings.MediumResourceUses;
				break;
			case ResourceUses.High:
				result = Strings.HighResourceUses;
				break;
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000780C File Offset: 0x00005A0C
		internal void AddDiagnosticInfoTo(XElement resourceManagerElement, bool showBasic, bool showVerbose)
		{
			if (resourceManagerElement == null)
			{
				throw new ArgumentNullException("resourceManagerElement");
			}
			this.resourceManagerConfig.AddDiagnosticInfo(resourceManagerElement);
			if (!this.IsMonitoringEnabled)
			{
				return;
			}
			XElement xelement = new XElement("SmtpThrottlingController");
			resourceManagerElement.Add(xelement);
			this.smtpThrottlingController.AddDiagnosticInfo(xelement, showBasic);
			if (showBasic)
			{
				resourceManagerElement.Add(new object[]
				{
					new XElement("overallResourceUses", this.currentResourceUses),
					new XElement("privateBytesResourceUses", this.currentPrivateBytesUses),
					new XElement("versionBucketsResourceUses", this.currentVersionBucketUses),
					new XElement("versionBucketsPressureRaw", this.currentVersionBucketPressureRaw),
					new XElement("submissionQueueResourceUses", this.currentSubmissionQueueUses),
					new XElement("GarbageCollection", new object[]
					{
						new XElement("collectingGarbage", this.collectingGarbage),
						new XElement("gcCollectInterval", this.gcCollectInterval),
						new XElement("lastTimeGcCollectCalled", this.lastTimeGCCollectCalled),
						new XElement("lastGcCollectionCount", this.lastGCCollectionCount),
						new XElement("gcCollectLowInterval", ResourceManager.gcCollectLowInterval),
						new XElement("gcCollectHighInterval", ResourceManager.gcCollectHighInterval)
					})
				});
				XElement xelement2 = new XElement("ResourceMonitors", new XElement("count", this.resourceMonitors.Count));
				foreach (ResourceMonitor resourceMonitor in this.resourceMonitors)
				{
					xelement2.Add(resourceMonitor.GetDiagnosticInfo(showVerbose));
				}
				resourceManagerElement.Add(xelement2);
			}
			this.components.AddDiagnosticInfo(resourceManagerElement);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007A64 File Offset: 0x00005C64
		private static bool IsGCCollectionEffective(long lastMemoryChange)
		{
			long num = (long)(MemoryPrivateBytesMonitor.TotalPhysicalMemory / 100UL);
			return lastMemoryChange > num;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007A80 File Offset: 0x00005C80
		private void DoGarbageCollection(object obj)
		{
			try
			{
				ResourceMonitor resourceMonitor = this.resourceMonitors[3];
				ExTraceGlobals.ResourceManagerTracer.TraceDebug(0L, "Calling GC.Collect: PreviousResourceUses: {0}, CurrentResourceUses: {1}, CurrentPressureRaw: {2}, CurrentResourceUsesRaw: {3}", new object[]
				{
					resourceMonitor.PreviousResourceUses,
					resourceMonitor.ResourceUses,
					resourceMonitor.CurrentPressureRaw,
					resourceMonitor.CurrentResourceUsesRaw
				});
				long totalMemory = GC.GetTotalMemory(false);
				GC.Collect();
				this.lastTimeGCCollectCalled = DateTime.UtcNow;
				this.lastGCCollectionCount = GC.CollectionCount(2);
				long totalMemory2 = GC.GetTotalMemory(false);
				if (ResourceManager.IsGCCollectionEffective(totalMemory - totalMemory2))
				{
					this.gcCollectInterval = ResourceManager.gcCollectLowInterval;
				}
				else
				{
					this.gcCollectInterval = ResourceManager.gcCollectHighInterval;
				}
				ExTraceGlobals.ResourceManagerTracer.TraceDebug<ResourceUses, TimeSpan, long>(0L, "After GC.Collect: CurrentResourceUses: {0}, Next GC Interval: {1}, TotalFreedMemory: {2}", resourceMonitor.CurrentResourceUsesRaw, this.gcCollectInterval, totalMemory - totalMemory2);
			}
			finally
			{
				this.collectingGarbage = false;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007B90 File Offset: 0x00005D90
		private void PostMonitorResource(ComponentsState requiredComponentsState)
		{
			ResourceUses resourceUses = this.currentResourceUses;
			try
			{
				this.updateLock.AcquireWriterLock(-1);
				this.currentPrivateBytesUses = this.resourceMonitors[3].ResourceUses;
				this.currentVersionBucketUses = this.resourceMonitors[2].ResourceUses;
				this.currentVersionBucketPressureRaw = this.resourceMonitors[2].CurrentPressureRaw;
				this.currentSubmissionQueueUses = this.resourceMonitors[5].ResourceUses;
				this.currentResourceUses = (from m in this.resourceMonitors
				where m != this.resourceMonitors[4]
				select m.ResourceUses).Max<ResourceUses>();
			}
			finally
			{
				this.updateLock.ReleaseWriterLock();
			}
			this.RefreshComponentsState(requiredComponentsState);
			if (DateTime.UtcNow - this.statusLastPublished > TimeSpan.FromMinutes(1.0) || this.currentResourceUses != resourceUses)
			{
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "RefreshComponentsState.Notification", null, this.currentResourceUses.ToString(), (this.currentResourceUses == ResourceUses.Normal) ? ResultSeverityLevel.Informational : ResultSeverityLevel.Error, false);
				if (this.currentResourceUses == resourceUses)
				{
					this.LogResourceUsePeriodic(this.aggregateResource, this.currentResourceUses, resourceUses);
				}
				this.statusLastPublished = DateTime.UtcNow;
			}
			if (this.currentResourceUses != resourceUses)
			{
				ResourceUses resourceUses2;
				bool flag = this.CheckToLogLowOnDiskSpace(out resourceUses2);
				this.eventLogger.LogResourcePressureChangedEvent(resourceUses, this.currentResourceUses, this.ToString());
				this.LogResourceUseChanged(this.aggregateResource, this.currentResourceUses, resourceUses);
				if (flag)
				{
					this.eventLogger.LogLowOnDiskSpaceEvent(resourceUses2, this.ToString());
				}
			}
			if (this.ShouldShrinkDownMemoryCaches)
			{
				if (this.resourceMonitors[3].ResourceUses > ResourceUses.Normal)
				{
					if (this.components.EnhancedDnsComponent != null)
					{
						this.components.EnhancedDnsComponent.FlushCache();
					}
					if (this.components.IsBridgehead)
					{
						Schema.FlushCache();
					}
					this.eventLogger.LogPrivateBytesHighEvent(this.ToString());
				}
				if (this.components.RemoteDeliveryComponent != null && this.resourceManagerConfig.DehydrateMessagesUnderMemoryPressure)
				{
					this.components.RemoteDeliveryComponent.CommitLazyAndDehydrateMessages();
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007DDC File Offset: 0x00005FDC
		private void LogResourceUseChanged(ResourceIdentifier resource, ResourceUses currentResourceUses, ResourceUses previousResourceUses)
		{
			ResourceUse resourceUse = new ResourceUse(resource, this.resourceUseToUseLevelMap[currentResourceUses], this.resourceUseToUseLevelMap[previousResourceUses]);
			this.resourceLog.LogResourceUseChange(resourceUse, null);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007E18 File Offset: 0x00006018
		private void LogResourceUsePeriodic(ResourceIdentifier resource, ResourceUses currentResourceUses, ResourceUses previousResourceUses)
		{
			ResourceUse resourceUse = new ResourceUse(resource, this.resourceUseToUseLevelMap[currentResourceUses], this.resourceUseToUseLevelMap[previousResourceUses]);
			this.resourceLog.LogResourceUsePeriodic(resourceUse, null);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007E6C File Offset: 0x0000606C
		private bool CheckToLogLowOnDiskSpace(out ResourceUses maxResourceUsage)
		{
			maxResourceUsage = (from m in this.resourceMonitors.OfType<DiskSpaceMonitor>()
			where m.ResourceUses > m.PreviousResourceUses
			select m.ResourceUses).DefaultIfEmpty(ResourceUses.Normal).Max<ResourceUses>();
			return maxResourceUsage != ResourceUses.Normal;
		}

		// Token: 0x040000D1 RID: 209
		private const int DatabaseMonitorIndex = 0;

		// Token: 0x040000D2 RID: 210
		private const int PrivateBytesMonitorIndex = 3;

		// Token: 0x040000D3 RID: 211
		private const int TotalBytesMonitorIndex = 4;

		// Token: 0x040000D4 RID: 212
		private const int VersionBucketsMonitorIndex = 2;

		// Token: 0x040000D5 RID: 213
		private const int DatabaseLoggingFolderMonitorIndex = 1;

		// Token: 0x040000D6 RID: 214
		private const int SubmissionQueueMonitorIndex = 5;

		// Token: 0x040000D7 RID: 215
		private static TimeSpan gcCollectLowInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000D8 RID: 216
		private static TimeSpan gcCollectHighInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x040000D9 RID: 217
		private ResourceManagerEventLogger eventLogger;

		// Token: 0x040000DA RID: 218
		private ResourceUses currentResourceUses;

		// Token: 0x040000DB RID: 219
		private ResourceUses currentPrivateBytesUses;

		// Token: 0x040000DC RID: 220
		private ResourceUses currentVersionBucketUses;

		// Token: 0x040000DD RID: 221
		private int currentVersionBucketPressureRaw;

		// Token: 0x040000DE RID: 222
		private ResourceUses currentSubmissionQueueUses;

		// Token: 0x040000DF RID: 223
		private List<ResourceMonitor> resourceMonitors = new List<ResourceMonitor>();

		// Token: 0x040000E0 RID: 224
		private DateTime lastTimeResourceMonitored;

		// Token: 0x040000E1 RID: 225
		private int lastGCCollectionCount = -1;

		// Token: 0x040000E2 RID: 226
		private DateTime lastTimeGCCollectCalled = DateTime.MinValue;

		// Token: 0x040000E3 RID: 227
		private TimeSpan gcCollectInterval = ResourceManager.gcCollectLowInterval;

		// Token: 0x040000E4 RID: 228
		private volatile bool collectingGarbage;

		// Token: 0x040000E5 RID: 229
		private FastReaderWriterLock updateLock = new FastReaderWriterLock();

		// Token: 0x040000E6 RID: 230
		private readonly ResourceManagerComponentsAdapter components;

		// Token: 0x040000E7 RID: 231
		private ThrottlingController smtpThrottlingController;

		// Token: 0x040000E8 RID: 232
		private readonly ResourceManagerConfiguration resourceManagerConfig;

		// Token: 0x040000E9 RID: 233
		private readonly ResourceMonitorFactory resourceMonitorFactory;

		// Token: 0x040000EA RID: 234
		private readonly ResourceManagerResources resourcesToMonitor;

		// Token: 0x040000EB RID: 235
		private readonly ResourceLog resourceLog;

		// Token: 0x040000EC RID: 236
		private readonly Dictionary<ResourceUses, UseLevel> resourceUseToUseLevelMap = new Dictionary<ResourceUses, UseLevel>
		{
			{
				ResourceUses.High,
				UseLevel.High
			},
			{
				ResourceUses.Medium,
				UseLevel.Medium
			},
			{
				ResourceUses.Normal,
				UseLevel.Low
			}
		};

		// Token: 0x040000ED RID: 237
		private readonly ResourceIdentifier aggregateResource = new ResourceIdentifier("Aggregate", "");

		// Token: 0x040000EE RID: 238
		private DateTime statusLastPublished;
	}
}
