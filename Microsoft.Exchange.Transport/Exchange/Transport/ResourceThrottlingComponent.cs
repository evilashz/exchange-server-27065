using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.ResourceMonitoring;
using Microsoft.Exchange.Transport.ResourceThrottling;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000051 RID: 81
	internal sealed class ResourceThrottlingComponent : ITransportComponent, IDiagnosable
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00009744 File Offset: 0x00007944
		public ResourceThrottlingComponent(ResourceMeteringConfig resourceMeteringConfig, ResourceThrottlingConfig resourceThrottlingConfig, IComponentsWrapper componentsWrapper, MessagingDatabaseComponent messagingDatabaseComponent, CategorizerComponent categorizerComponent, ITransportConfiguration configComponent, ResourceManagerResources resourcesToMonitor, ResourceObservingComponents observingComponents)
		{
			ArgumentValidator.ThrowIfNull("resourceMeteringConfig", resourceMeteringConfig);
			ArgumentValidator.ThrowIfNull("resourceThrottlingConfig", resourceThrottlingConfig);
			ArgumentValidator.ThrowIfNull("componentsWrapper", componentsWrapper);
			ArgumentValidator.ThrowIfNull("messagingDatabaseComponent", messagingDatabaseComponent);
			ArgumentValidator.ThrowIfNull("configComponent", configComponent);
			this.resourceMeteringConfig = resourceMeteringConfig;
			this.resourceThrottlingConfig = resourceThrottlingConfig;
			this.componentsWrapper = componentsWrapper;
			this.messagingDatabaseComponent = messagingDatabaseComponent;
			this.categorizerComponent = categorizerComponent;
			this.configComponent = configComponent;
			this.resourcesToMonitor = resourcesToMonitor;
			this.observingComponents = observingComponents;
			ResourceIdentifier resourceIdentifier = new ResourceIdentifier("QueueLength", "SubmissionQueue");
			this.resourceMeterCreators.Add("PrivateBytes", new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreatePrivateBytesResourceMeter));
			this.resourceMeterCreators.Add("DatabaseUsedSpace", new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreateDatabaseUsedSpaceResourceMeter));
			this.resourceMeterCreators.Add(resourceIdentifier.ToString(), new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreateSubmissionQueueLengthResourceMeter));
			this.resourceMeterCreators.Add("UsedVersionBuckets", new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreateUsedVersionBucketsResourceMeter));
			this.resourceMeterCreators.Add("SystemMemory", new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreateSystemMemoryResourceMeter));
			this.resourceMeterCreators.Add("UsedDiskSpace", new Func<ResourceIdentifier, PressureTransitions, IResourceMeter>(this.CreateUsedDiskSpaceMeter));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000099C4 File Offset: 0x00007BC4
		public void PublishStatus()
		{
			if (this.ShouldPublishStatus())
			{
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "RefreshComponentsState.Notification", null, this.resourceTracker.AggregateResourceUse.CurrentUseLevel.ToString(), (this.resourceTracker.AggregateResourceUse.CurrentUseLevel == UseLevel.Low) ? ResultSeverityLevel.Informational : ResultSeverityLevel.Error, false);
				string backPressureDescription = this.GetBackPressureDescription();
				this.eventLogger.LogResourcePressureChangedEvent(this.resourceTracker.AggregateResourceUse, backPressureDescription);
				ResourceUse resourceUse2 = this.resourceTracker.ResourceUses.SingleOrDefault((ResourceUse resourceUse) => resourceUse.Resource == this.privateBytesResource);
				if (resourceUse2 != null && resourceUse2.CurrentUseLevel != UseLevel.Low)
				{
					this.eventLogger.LogPrivateBytesHighEvent(backPressureDescription);
				}
				IEnumerable<ResourceUse> source = from resourceUse in this.resourceTracker.ResourceUses
				where resourceUse.Resource.Name == "UsedDiskSpace"
				select resourceUse;
				if (source.Any<ResourceUse>())
				{
					UseLevel useLevel = source.Max((ResourceUse resourceUse) => resourceUse.CurrentUseLevel);
					if (useLevel != UseLevel.Low)
					{
						this.eventLogger.LogLowOnDiskSpaceEvent(useLevel, backPressureDescription);
					}
				}
				this.statusLastPublishTime = DateTime.UtcNow;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009AF4 File Offset: 0x00007CF4
		private bool ShouldPublishStatus()
		{
			bool result = false;
			if (DateTime.UtcNow - this.statusLastPublishTime > this.resourceMeteringConfig.StatusPublishInterval)
			{
				result = true;
			}
			else if (this.statusLastPublishTime < this.resourceTracker.LastUpdateTime && this.resourceTracker.AggregateResourceUse.CurrentUseLevel != this.resourceTracker.AggregateResourceUse.PreviousUseLevel)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009B66 File Offset: 0x00007D66
		public void ObserveExceptions()
		{
			if (this.resourceTrackingTask != null && this.resourceTrackingTask.IsCompleted && this.resourceTrackingTask.Exception != null)
			{
				throw this.resourceTrackingTask.Exception;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009B98 File Offset: 0x00007D98
		void ITransportComponent.Load()
		{
			if (!this.resourceMeteringConfig.IsResourceTrackingEnabled)
			{
				return;
			}
			this.cancellationTokenSource = new CancellationTokenSource();
			ResourceLog resourceLog = this.CreateResourceLog();
			this.GetResourcesAndPressureTransitions();
			List<IResourceMeter> list = new List<IResourceMeter>();
			foreach (ResourceIdentifier resource in this.meteredResources)
			{
				list.Add(this.CreateResourceMeter(resource));
			}
			this.resourceTracker = new ResourceTracker(list, this.resourceMeteringConfig.ResourceMeteringInterval, this.resourceMeteringConfig.ResourceMeterTimeout, resourceLog, this.resourceMeteringConfig.ResourceLoggingInterval, new Func<IEnumerable<ResourceUse>, UseLevel>(this.GetAggregateUseLevel), this.resourceMeteringConfig.MaxTransientExceptionsAllowed);
			if (this.resourceThrottlingConfig.IsResourceThrottlingEnabled)
			{
				IEnumerable<IResourceLevelObserver> resourceLevelObservers = this.GetResourceLevelObservers();
				this.resourceLevelMediator = new ResourceLevelMediator(this.resourceTracker, resourceLevelObservers, this.resourceThrottlingConfig.ResourceObserverTimeout, this.resourceThrottlingConfig.MaxTransientExceptionsAllowed);
			}
			int maxSamples = 1;
			if (this.resourceMeteringConfig.SustainedDuration > this.resourceMeteringConfig.ResourceMeteringInterval)
			{
				maxSamples = (int)(this.resourceMeteringConfig.SustainedDuration.TotalSeconds / this.resourceMeteringConfig.ResourceMeteringInterval.TotalSeconds);
			}
			this.sustainedBackPressureStabilizer = new ResourceSampleStabilizer(maxSamples, new ResourceSample(UseLevel.Low, 0L));
			this.perfCountersInstance = ResourceThrottlingPerfCounters.GetInstance(Process.GetCurrentProcess().ProcessName);
			this.resourceTrackingTask = this.resourceTracker.StartResourceTrackingAsync(this.cancellationTokenSource.Token);
			this.statusTimer = new GuardedTimer(new TimerCallback(this.StatusUpdate), null, this.resourceMeteringConfig.ResourceMeteringInterval);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009D78 File Offset: 0x00007F78
		private UseLevel GetAggregateUseLevel(IEnumerable<ResourceUse> resourceUses)
		{
			UseLevel result = UseLevel.Low;
			if (resourceUses != null && resourceUses.Any<ResourceUse>())
			{
				ResourceIdentifier systemMemory = new ResourceIdentifier("SystemMemory", "");
				IEnumerable<ResourceUse> source = from use in resourceUses
				where use.Resource != systemMemory
				select use;
				if (source.Any<ResourceUse>())
				{
					result = source.Max((ResourceUse use) => use.CurrentUseLevel);
				}
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009DED File Offset: 0x00007FED
		private void StatusUpdate(object state)
		{
			if (this.resourceMeteringConfig.IsResourceTrackingEnabled)
			{
				this.ObserveExceptions();
				this.CollectDiagnostics();
				this.PublishStatus();
				this.PublishDiagnostics();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009E14 File Offset: 0x00008014
		private void CollectDiagnostics()
		{
			this.sustainedBackPressureStabilizer.AddSample(new ResourceSample(this.resourceTracker.AggregateResourceUse.CurrentUseLevel, 0L));
			if (this.sustainedBackPressureStabilizer.GetUseLevelPercentage(UseLevel.Low) < 15)
			{
				if (this.backPressureStartTime == DateTime.MaxValue)
				{
					this.backPressureStartTime = DateTime.UtcNow - this.resourceMeteringConfig.SustainedDuration;
				}
			}
			else
			{
				this.backPressureStartTime = DateTime.MaxValue;
			}
			ResourceTrackerDiagnosticsData diagnosticsData = this.resourceTracker.GetDiagnosticsData();
			this.maxResourceMeterCallDuration = TimeSpan.Zero;
			foreach (IResourceMeter resourceMeter in this.resourceTracker.ResourceMeters)
			{
				TimeSpan resourceMeterCallDuration = diagnosticsData.GetResourceMeterCallDuration(resourceMeter.Resource);
				if (resourceMeterCallDuration > this.maxResourceMeterCallDuration)
				{
					this.maxResourceMeterCallDuration = resourceMeterCallDuration;
				}
			}
			ResourceLevelMediatorDiagnosticsData diagnosticsData2 = this.resourceLevelMediator.GetDiagnosticsData();
			this.maxResourceObserverCallDuration = TimeSpan.Zero;
			foreach (IResourceLevelObserver resourceLevelObserver in this.resourceLevelMediator.ResourceLevelObservers)
			{
				TimeSpan resourceObserverCallDuration = diagnosticsData2.GetResourceObserverCallDuration(resourceLevelObserver.Name);
				if (resourceObserverCallDuration > this.maxResourceObserverCallDuration)
				{
					this.maxResourceObserverCallDuration = resourceObserverCallDuration;
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009F88 File Offset: 0x00008188
		private void PublishDiagnostics()
		{
			if (this.backPressureStartTime >= DateTime.UtcNow)
			{
				this.perfCountersInstance.BackPressureTime.RawValue = 0L;
			}
			else
			{
				this.perfCountersInstance.BackPressureTime.RawValue = (long)(DateTime.UtcNow - this.backPressureStartTime).TotalSeconds;
			}
			this.perfCountersInstance.ResourceMeterLongestCallDuration.RawValue = (long)this.maxResourceMeterCallDuration.TotalMilliseconds;
			this.perfCountersInstance.ResourceObserverLongestCallDuration.RawValue = (long)this.maxResourceObserverCallDuration.TotalMilliseconds;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A01C File Offset: 0x0000821C
		public bool IsResourceTrackingEnabled
		{
			get
			{
				return this.resourceMeteringConfig.IsResourceTrackingEnabled;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A02C File Offset: 0x0000822C
		private void GetResourcesAndPressureTransitions()
		{
			this.meteredResources = new List<ResourceIdentifier>();
			if ((this.resourcesToMonitor & ResourceManagerResources.PrivateBytes) > ResourceManagerResources.None)
			{
				this.meteredResources.Add(this.privateBytesResource);
				this.localizedResourceNames.Add(this.privateBytesResource, Strings.PrivateBytesResource);
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.SubmissionQueue) > ResourceManagerResources.None)
			{
				ResourceIdentifier resourceIdentifier = new ResourceIdentifier("QueueLength", "SubmissionQueue");
				this.meteredResources.Add(resourceIdentifier);
				this.localizedResourceNames.Add(resourceIdentifier, Strings.QueueLength(Strings.Submission));
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.TotalBytes) > ResourceManagerResources.None)
			{
				ResourceIdentifier resourceIdentifier2 = new ResourceIdentifier("SystemMemory", "");
				this.meteredResources.Add(resourceIdentifier2);
				this.localizedResourceNames.Add(resourceIdentifier2, Strings.SystemMemory);
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.VersionBuckets) > ResourceManagerResources.None)
			{
				string databasePath = this.messagingDatabaseComponent.Database.DataSource.DatabasePath;
				ResourceIdentifier resourceIdentifier3 = new ResourceIdentifier("UsedVersionBuckets", databasePath);
				this.meteredResources.Add(resourceIdentifier3);
				this.localizedResourceNames.Add(resourceIdentifier3, Strings.VersionBuckets(databasePath));
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.MailDatabase) > ResourceManagerResources.None)
			{
				string databasePath2 = this.messagingDatabaseComponent.Database.DataSource.DatabasePath;
				string directoryName = Path.GetDirectoryName(databasePath2);
				ResourceIdentifier resourceIdentifier4 = new ResourceIdentifier("DatabaseUsedSpace", directoryName);
				this.meteredResources.Add(resourceIdentifier4);
				this.localizedResourceNames.Add(resourceIdentifier4, Strings.DatabaseResource(databasePath2));
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.MailDatabaseLoggingFolder) > ResourceManagerResources.None)
			{
				string logFilePath = this.messagingDatabaseComponent.Database.DataSource.LogFilePath;
				string directoryName2 = Path.GetDirectoryName(logFilePath);
				ResourceIdentifier resourceIdentifier5 = new ResourceIdentifier("UsedDiskSpace", directoryName2);
				if (!this.meteredResources.Contains(resourceIdentifier5))
				{
					this.meteredResources.Add(resourceIdentifier5);
					this.localizedResourceNames.Add(resourceIdentifier5, Strings.UsedDiskSpaceResource(directoryName2));
				}
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.TempDrive) > ResourceManagerResources.None)
			{
				string directoryName3 = Path.GetDirectoryName(Components.TransportAppConfig.WorkerProcess.TemporaryStoragePath);
				ResourceIdentifier resourceIdentifier6 = new ResourceIdentifier("UsedDiskSpace", directoryName3);
				if (!this.meteredResources.Contains(resourceIdentifier6))
				{
					this.meteredResources.Add(resourceIdentifier6);
					this.localizedResourceNames.Add(resourceIdentifier6, Strings.UsedDiskSpaceResource(directoryName3));
				}
			}
			this.pressureTransitionsForResources = this.resourceMeteringConfig.GetPressureTransitionsForResources(this.meteredResources);
			this.AdjustDiskSpaceResources();
			ResourceIdentifier key = new ResourceIdentifier("Aggregate", "");
			this.localizedResourceNames.Add(key, Strings.AggregateResource);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A2CC File Offset: 0x000084CC
		private void AdjustDiskSpaceResources()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			long num = 0L;
			if ((this.resourcesToMonitor & ResourceManagerResources.MailDatabase) > ResourceManagerResources.None)
			{
				text2 = Path.GetDirectoryName(this.messagingDatabaseComponent.Database.DataSource.DatabasePath);
				ResourceIdentifier key = new ResourceIdentifier("DatabaseUsedSpace", text2);
				if (this.pressureTransitionsForResources.ContainsKey(key) && this.pressureTransitionsForResources[key].MediumToHigh == 100L)
				{
					this.pressureTransitionsForResources[key] = this.EnsureMinDiskSpaceRequired(text2, this.pressureTransitionsForResources[key], (long)ByteQuantifiedSize.FromMB(500UL).ToBytes());
				}
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.MailDatabaseLoggingFolder) > ResourceManagerResources.None)
			{
				text = Path.GetDirectoryName(this.messagingDatabaseComponent.Database.DataSource.LogFilePath);
				ResourceIdentifier key2 = new ResourceIdentifier("UsedDiskSpace", text);
				if (this.pressureTransitionsForResources.ContainsKey(key2))
				{
					num = Math.Min((long)(Components.TransportAppConfig.JetDatabase.CheckpointDepthMax.ToBytes() * 3UL), (long)ByteQuantifiedSize.FromGB(5UL).ToBytes());
					this.pressureTransitionsForResources[key2] = this.EnsureMinDiskSpaceRequired(text, this.pressureTransitionsForResources[key2], num);
				}
			}
			if ((this.resourcesToMonitor & ResourceManagerResources.TempDrive) > ResourceManagerResources.None)
			{
				string directoryName = Path.GetDirectoryName(Components.TransportAppConfig.WorkerProcess.TemporaryStoragePath);
				ResourceIdentifier key3 = new ResourceIdentifier("UsedDiskSpace", directoryName);
				if (this.pressureTransitionsForResources.ContainsKey(key3))
				{
					long num2 = (long)Components.TransportAppConfig.ResourceManager.TempDiskSpaceRequired.ToBytes();
					if (string.Compare(text, directoryName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num2 = Math.Max(num2, num);
					}
					this.pressureTransitionsForResources[key3] = this.EnsureMinDiskSpaceRequired(directoryName, this.pressureTransitionsForResources[key3], num2);
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private PressureTransitions EnsureMinDiskSpaceRequired(string path, PressureTransitions pressureTransitions, long minSpaceRequired)
		{
			INativeMethodsWrapper nativeMethodsWrapper = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();
			ulong num;
			ulong num2;
			ulong num3;
			if (nativeMethodsWrapper.GetDiskFreeSpaceEx(path, out num, out num2, out num3))
			{
				long val = (long)((num2 - (ulong)minSpaceRequired) * 100UL / num2);
				long num4 = Math.Min(pressureTransitions.MediumToHigh, val);
				long num5 = Math.Min(pressureTransitions.HighToMedium, num4 - 2L);
				long num6 = Math.Min(pressureTransitions.LowToMedium, num5 - 1L);
				long mediumToLow = Math.Min(pressureTransitions.MediumToLow, num6 - 2L);
				return new PressureTransitions(num4, num5, num6, mediumToLow);
			}
			return pressureTransitions;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A530 File Offset: 0x00008730
		private ResourceLog CreateResourceLog()
		{
			Server transportServer = this.configComponent.LocalServer.TransportServer;
			bool enabled = transportServer.ResourceLogEnabled;
			string logDirectory = string.Empty;
			if (transportServer.ResourceLogPath == null || string.IsNullOrEmpty(transportServer.ResourceLogPath.PathName))
			{
				enabled = false;
			}
			else
			{
				logDirectory = transportServer.ResourceLogPath.PathName;
			}
			return new ResourceLog(enabled, "ResourceThrottling", logDirectory, transportServer.ResourceLogMaxAge, this.resourceMeteringConfig.ResourceLogFlushInterval, this.resourceMeteringConfig.ResourceLogBackgroundWriteInterval, (long)(transportServer.ResourceLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.ResourceLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.ResourceLogMaxFileSize.IsUnlimited ? 0UL : transportServer.ResourceLogMaxDirectorySize.Value.ToBytes()), this.resourceMeteringConfig.ResourceLogBufferSize);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A61C File Offset: 0x0000881C
		private IEnumerable<IResourceLevelObserver> GetResourceLevelObservers()
		{
			List<IResourceLevelObserver> list = new List<IResourceLevelObserver>();
			if ((this.observingComponents & ResourceObservingComponents.BootScanner) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("BootScanner"))
			{
				list.Add(new BootScannerResourceLevelObserver(Components.BootScanner));
			}
			IStartableTransportComponent contentAggregator;
			if ((this.observingComponents & ResourceObservingComponents.ContentAggregator) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("ContentAggregator") && Components.TryGetAggregator(out contentAggregator))
			{
				list.Add(new ContentAggregatorResourceLevelObserver(contentAggregator));
			}
			if ((this.observingComponents & ResourceObservingComponents.EnhancedDns) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("EnhancedDns"))
			{
				list.Add(new EnhancedDnsResourceLevelObserver(Components.EnhancedDns, this.componentsWrapper));
			}
			if ((this.observingComponents & ResourceObservingComponents.IsMemberOfResolver) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("IsMemberOfResolver"))
			{
				list.Add(new IsMofRResourceLevelObserver(Components.TransportIsMemberOfResolverComponent, this.componentsWrapper));
			}
			if ((this.observingComponents & ResourceObservingComponents.MessageResubmission) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("MessageResubmission"))
			{
				list.Add(new MessageResubmissionResourceLevelObserver(Components.MessageResubmissionComponent));
			}
			if ((this.observingComponents & ResourceObservingComponents.PickUp) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("Pickup"))
			{
				list.Add(new PickupResourceLevelObserver(Components.PickupComponent));
			}
			if ((this.observingComponents & ResourceObservingComponents.RemoteDelivery) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("RemoteDelivery"))
			{
				list.Add(new RemoteDeliveryResourceLevelObserver(Components.RemoteDeliveryComponent, this.messagingDatabaseComponent.Database.DataSource.DatabasePath, this.resourceThrottlingConfig.DehydrateMessagesUnderMemoryPressure));
			}
			if ((this.observingComponents & ResourceObservingComponents.ShadowRedundancy) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("ShadowRedundancy"))
			{
				list.Add(new ShadowRedundancyResourceLevelObserver(Components.ShadowRedundancyComponent));
			}
			if ((this.observingComponents & ResourceObservingComponents.SmtpIn) > ResourceObservingComponents.None && !this.resourceThrottlingConfig.DisabledResourceLevelObservers.Contains("SmtpIn"))
			{
				ResourceManagerConfiguration.ThrottlingControllerConfiguration config = new ResourceManagerConfiguration.ThrottlingControllerConfiguration(this.resourceThrottlingConfig.BaseThrottlingDelayInterval, this.resourceThrottlingConfig.StartThrottlingDelayInterval, this.resourceThrottlingConfig.StepThrottlingDelayInterval, this.resourceThrottlingConfig.MaxThrottlingDelayInterval);
				ThrottlingController throttlingController = new ThrottlingController(ExTraceGlobals.ResourceManagerTracer, config);
				list.Add(new SmtpInResourceLevelObserver(Components.SmtpInComponent, throttlingController, this.componentsWrapper));
			}
			return list;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A874 File Offset: 0x00008A74
		private IResourceMeter CreateResourceMeter(ResourceIdentifier resource)
		{
			PressureTransitions arg = this.pressureTransitionsForResources[resource];
			Func<ResourceIdentifier, PressureTransitions, IResourceMeter> func;
			if (!this.resourceMeterCreators.TryGetValue(resource.ToString(), out func) && !this.resourceMeterCreators.TryGetValue(resource.Name, out func))
			{
				throw new InvalidOperationException("Resource Meter Creator is not found for " + resource);
			}
			return func(resource, arg);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		private IResourceMeter CreatePrivateBytesResourceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			IResourceMeter rawResourceMeter = new PrivateBytesResourceMeter(pressureTransitions, ResourceMeteringConfig.TotalPhysicalMemory);
			return new StabilizedResourceMeter(rawResourceMeter, this.resourceMeteringConfig.PrivateBytesStabilizationSamples);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A900 File Offset: 0x00008B00
		private IResourceMeter CreateDatabaseUsedSpaceResourceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			IMeterableJetDataSource meterableDataSource = MeterableJetDataSourceFactory.CreateMeterableDataSource(this.messagingDatabaseComponent.Database.DataSource);
			return new DatabaseUsedSpaceMeter(meterableDataSource, pressureTransitions);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A92C File Offset: 0x00008B2C
		private IResourceMeter CreateSubmissionQueueLengthResourceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			IMeterableQueue meterableQueue = MeterableQueueFactory.CreateMeterableQueue("SubmissionQueue", this.categorizerComponent.SubmitMessageQueue);
			IResourceMeter rawResourceMeter = new QueueLengthResourceMeter(meterableQueue, pressureTransitions);
			return new StabilizedResourceMeter(rawResourceMeter, this.resourceMeteringConfig.SubmissionQueueStabilizationSamples);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A968 File Offset: 0x00008B68
		private IResourceMeter CreateSystemMemoryResourceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			return new SystemMemoryResourceMeter(pressureTransitions);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A970 File Offset: 0x00008B70
		private IResourceMeter CreateUsedVersionBucketsResourceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			IMeterableJetDataSource meterableDataSourcee = MeterableJetDataSourceFactory.CreateMeterableDataSource(this.messagingDatabaseComponent.Database.DataSource);
			IResourceMeter rawResourceMeter = new UsedVersionBucketsResourceMeter(meterableDataSourcee, pressureTransitions);
			return new StabilizedResourceMeter(rawResourceMeter, this.resourceMeteringConfig.VersionBucketsStabilizationSamples);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A9AC File Offset: 0x00008BAC
		private IResourceMeter CreateUsedDiskSpaceMeter(ResourceIdentifier resource, PressureTransitions pressureTransitions)
		{
			return new UsedDiskSpaceResourceMeter(resource.InstanceName, pressureTransitions);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A9BC File Offset: 0x00008BBC
		private string GetBackPressureDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			if (!this.IsResourceTrackingEnabled)
			{
				return string.Empty;
			}
			foreach (ResourceUse resourceUse in this.resourceTracker.ResourceUses)
			{
				if (resourceUse.CurrentUseLevel == UseLevel.Low)
				{
					stringBuilder2.AppendLine(this.localizedResourceNames[resourceUse.Resource]);
				}
				else
				{
					stringBuilder3.AppendLine(this.localizedResourceNames[resourceUse.Resource]);
				}
			}
			if (stringBuilder3.Length > 0)
			{
				stringBuilder.AppendLine(Strings.ResourcesInAboveNormalPressure(stringBuilder3.ToString()));
			}
			StringBuilder stringBuilder4 = new StringBuilder();
			if (this.resourceThrottlingConfig.IsResourceThrottlingEnabled)
			{
				foreach (IResourceLevelObserver resourceLevelObserver in this.resourceLevelMediator.ResourceLevelObservers)
				{
					if (resourceLevelObserver.Paused)
					{
						stringBuilder4.AppendLine(this.componentsStateMap[resourceLevelObserver.Name + resourceLevelObserver.SubStatus]);
					}
				}
				if (stringBuilder4.Length > 0)
				{
					stringBuilder.AppendLine(Strings.ComponentsDisabledByBackPressure(stringBuilder4.ToString()));
				}
				else
				{
					stringBuilder.AppendLine(Strings.ComponentsDisabledNone);
				}
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.AppendLine(Strings.ResourcesInNormalPressure(stringBuilder2.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AB68 File Offset: 0x00008D68
		void ITransportComponent.Unload()
		{
			if (!this.IsResourceTrackingEnabled)
			{
				return;
			}
			this.ThrowIfNotLoaded();
			this.statusTimer.Dispose(true);
			this.cancellationTokenSource.Cancel();
			try
			{
				this.resourceTrackingTask.Wait();
			}
			catch (OperationCanceledException)
			{
			}
			catch (AggregateException ex)
			{
				if (!(ex.InnerException is OperationCanceledException))
				{
					throw;
				}
			}
			this.resourceLevelMediator = null;
			this.resourceTracker = null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		string ITransportComponent.OnUnhandledException(Exception e)
		{
			if (this.resourceTracker != null)
			{
				return "ResourceThrottlingComponent is loaded.";
			}
			return "ResourceThrottlingComponent is not loaded.";
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000ABFD File Offset: 0x00008DFD
		private void ThrowIfNotLoaded()
		{
			if (this.resourceTracker == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "ResourceThrottlingComponent is not loaded.", new object[0]));
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AC22 File Offset: 0x00008E22
		public string GetDiagnosticComponentName()
		{
			return "ResourceThrottling";
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool verbose = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			XElement xelement = new XElement("ResourceThrottling");
			if (this.resourceMeteringConfig.IsResourceTrackingEnabled)
			{
				xelement.Add(this.resourceTracker.GetDiagnosticInfo(verbose));
				if (this.resourceThrottlingConfig.IsResourceThrottlingEnabled)
				{
					xelement.Add(this.resourceLevelMediator.GetDiagnosticInfo(verbose));
				}
			}
			return xelement;
		}

		// Token: 0x0400013D RID: 317
		private const int SustainedBackpressureSamplePercent = 85;

		// Token: 0x0400013E RID: 318
		private const string ComponentName = "ResourceThrottling";

		// Token: 0x0400013F RID: 319
		private readonly ResourceMeteringConfig resourceMeteringConfig;

		// Token: 0x04000140 RID: 320
		private readonly ResourceThrottlingConfig resourceThrottlingConfig;

		// Token: 0x04000141 RID: 321
		private MessagingDatabaseComponent messagingDatabaseComponent;

		// Token: 0x04000142 RID: 322
		private readonly CategorizerComponent categorizerComponent;

		// Token: 0x04000143 RID: 323
		private readonly ITransportConfiguration configComponent;

		// Token: 0x04000144 RID: 324
		private readonly ResourceManagerResources resourcesToMonitor;

		// Token: 0x04000145 RID: 325
		private readonly ResourceObservingComponents observingComponents;

		// Token: 0x04000146 RID: 326
		private readonly IComponentsWrapper componentsWrapper;

		// Token: 0x04000147 RID: 327
		private readonly Dictionary<string, Func<ResourceIdentifier, PressureTransitions, IResourceMeter>> resourceMeterCreators = new Dictionary<string, Func<ResourceIdentifier, PressureTransitions, IResourceMeter>>();

		// Token: 0x04000148 RID: 328
		private ResourceTracker resourceTracker;

		// Token: 0x04000149 RID: 329
		private ResourceLevelMediator resourceLevelMediator;

		// Token: 0x0400014A RID: 330
		private Task resourceTrackingTask;

		// Token: 0x0400014B RID: 331
		private CancellationTokenSource cancellationTokenSource;

		// Token: 0x0400014C RID: 332
		private Dictionary<ResourceIdentifier, PressureTransitions> pressureTransitionsForResources;

		// Token: 0x0400014D RID: 333
		private DateTime statusLastPublishTime;

		// Token: 0x0400014E RID: 334
		private List<ResourceIdentifier> meteredResources;

		// Token: 0x0400014F RID: 335
		private GuardedTimer statusTimer;

		// Token: 0x04000150 RID: 336
		private ResourceSampleStabilizer sustainedBackPressureStabilizer;

		// Token: 0x04000151 RID: 337
		private ResourceThrottlingPerfCountersInstance perfCountersInstance;

		// Token: 0x04000152 RID: 338
		private DateTime backPressureStartTime = DateTime.MaxValue;

		// Token: 0x04000153 RID: 339
		private Dictionary<string, string> componentsStateMap = new Dictionary<string, string>
		{
			{
				"BootScanner",
				Strings.BootScannerComponent
			},
			{
				"ContentAggregator",
				Strings.ContentAggregationComponent
			},
			{
				"MessageResubmission",
				Strings.MessageResubmissionComponentBanner
			},
			{
				"Pickup",
				Strings.InboundMailSubmissionFromPickupDirectoryComponent + Environment.NewLine + Strings.InboundMailSubmissionFromReplayDirectoryComponent
			},
			{
				"RemoteDelivery",
				Strings.OutboundMailDeliveryToRemoteDomainsComponent
			},
			{
				"ShadowRedundancy",
				Strings.ShadowRedundancyComponentBanner
			},
			{
				"SmtpIn",
				Strings.InboundMailSubmissionFromInternetComponent
			},
			{
				"SmtpInRejecting Submissions",
				Strings.InboundMailSubmissionFromHubsComponent
			}
		};

		// Token: 0x04000154 RID: 340
		private readonly Dictionary<ResourceIdentifier, string> localizedResourceNames = new Dictionary<ResourceIdentifier, string>();

		// Token: 0x04000155 RID: 341
		private readonly ResourceManagerEventLogger eventLogger = new ResourceManagerEventLogger();

		// Token: 0x04000156 RID: 342
		private readonly ResourceIdentifier privateBytesResource = new ResourceIdentifier("PrivateBytes", "");

		// Token: 0x04000157 RID: 343
		private TimeSpan maxResourceMeterCallDuration;

		// Token: 0x04000158 RID: 344
		private TimeSpan maxResourceObserverCallDuration;
	}
}
