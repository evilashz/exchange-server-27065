using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000263 RID: 611
	internal abstract class WlmResourceHealthMonitor
	{
		// Token: 0x06001EEF RID: 7919 RVA: 0x000401F8 File Offset: 0x0003E3F8
		public WlmResourceHealthMonitor(WlmResource owner, ResourceKey resourceKey)
		{
			this.Owner = owner;
			this.WlmResourceKey = resourceKey;
			this.healthTracker = new WlmHealthSLA();
			this.admissionControl = new DefaultAdmissionControl(this.WlmResourceKey, new RemoveResourceDelegate(this.ResetAdmissionControl), null, "MRS_WlmResourceHealthMonitor");
			this.configContext = new GenericSettingsContext("WlmHealthMonitor", this.WlmResourceKey.ToString(), null);
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00040263 File Offset: 0x0003E463
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0004026B File Offset: 0x0003E46B
		public WlmResource Owner { get; private set; }

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00040274 File Offset: 0x0003E474
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0004027C File Offset: 0x0003E47C
		public ResourceKey WlmResourceKey { get; private set; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00040285 File Offset: 0x0003E485
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0004028D File Offset: 0x0003E48D
		public ExPerformanceCounter ResourceHealthPerfCounter { get; protected set; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00040296 File Offset: 0x0003E496
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0004029E File Offset: 0x0003E49E
		public ExPerformanceCounter DynamicCapacityPerfCounter { get; protected set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x000402A8 File Offset: 0x0003E4A8
		public int DynamicCapacity
		{
			get
			{
				ResourceLoad currentLoad = this.GetCurrentLoad();
				int num;
				if (this.DynamicThrottlingDisabled)
				{
					num = this.Owner.StaticCapacity;
				}
				else
				{
					double num2;
					num = this.admissionControl.GetConcurrencyLimit(this.Owner.WorkloadClassification, out num2);
					int maxConcurrency = this.admissionControl.MaxConcurrency;
					if (num > maxConcurrency)
					{
						num = maxConcurrency;
					}
				}
				this.DynamicCapacityPerfCounter.RawValue = (long)num;
				this.healthTracker.AddSample(currentLoad.State, num);
				return num;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00040320 File Offset: 0x0003E520
		public bool IsUnhealthy
		{
			get
			{
				switch (this.GetCurrentLoad().State)
				{
				case ResourceLoadState.Unknown:
					return this.Owner.Utilization > 0;
				case ResourceLoadState.Full:
					return this.Owner.Utilization == 0;
				case ResourceLoadState.Overloaded:
				case ResourceLoadState.Critical:
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0004037C File Offset: 0x0003E57C
		public bool IsDisabled
		{
			get
			{
				bool config;
				using (this.configContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<bool>("IgnoreHealthMonitor");
				}
				return config;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x000403C0 File Offset: 0x0003E5C0
		public bool DynamicThrottlingDisabled
		{
			get
			{
				bool config;
				using (this.configContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<bool>("DisableDynamicThrottling");
				}
				return config;
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00040404 File Offset: 0x0003E604
		public void VerifyDynamicCapacity(ReservationBase reservation)
		{
			if (this.IsDisabled)
			{
				return;
			}
			ResourceLoad currentLoad = this.GetCurrentLoad();
			if (currentLoad.State == ResourceLoadState.Overloaded || currentLoad.State == ResourceLoadState.Critical)
			{
				throw new WlmResourceUnhealthyException(this.Owner.ResourceName, this.Owner.ResourceType, this.WlmResourceKey.ToString(), (int)this.WlmResourceKey.MetricType, currentLoad.LoadRatio, currentLoad.State.ToString(), (currentLoad.Metric != null) ? currentLoad.Metric.ToString() : "(null)");
			}
			if (!this.DynamicThrottlingDisabled && this.Owner.Utilization >= this.DynamicCapacity)
			{
				throw new WlmCapacityExceededReservationException(this.Owner.ResourceName, this.Owner.ResourceType, this.WlmResourceKey.ToString(), (int)this.WlmResourceKey.MetricType, this.DynamicCapacity);
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00040500 File Offset: 0x0003E700
		public void LogHealthState()
		{
			ResourceLoad currentLoad = this.GetCurrentLoad();
			WlmHealthCounters customHealthCounters = this.healthTracker.GetCustomHealthCounters();
			WLMResourceStatsData loggingStatsData = new WLMResourceStatsData
			{
				OwnerResourceName = this.Owner.ResourceName,
				OwnerResourceGuid = this.Owner.ResourceGuid,
				OwnerResourceType = this.Owner.ResourceType,
				WlmResourceKey = this.WlmResourceKey.ToString(),
				DynamicCapacity = (double)this.DynamicCapacity,
				LoadState = currentLoad.State.ToString(),
				LoadRatio = currentLoad.LoadRatio,
				Metric = ((currentLoad.Metric != null) ? currentLoad.Metric.ToString() : string.Empty),
				IsDisabled = (this.IsDisabled ? "true" : null),
				DynamicThrottingDisabled = (this.DynamicThrottlingDisabled ? "true" : null),
				TimeInterval = this.healthTracker.CustomTimeInterval,
				UnderloadedCount = customHealthCounters.UnderloadedCounter,
				FullCount = customHealthCounters.FullCounter,
				OverloadedCount = customHealthCounters.OverloadedCounter,
				CriticalCount = customHealthCounters.CriticalCounter,
				UnknownCount = customHealthCounters.UnknownCounter
			};
			WLMResourceStatsLog.Write(loggingStatsData);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00040663 File Offset: 0x0003E863
		public void UpdateHealthState(bool logHealthState)
		{
			int dynamicCapacity = this.DynamicCapacity;
			if (logHealthState)
			{
				this.LogHealthState();
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00040694 File Offset: 0x0003E894
		public void AddReservation(ReservationBase reservation)
		{
			if (!this.IsDisabled && !this.DynamicThrottlingDisabled)
			{
				IResourceAdmissionControl admissionControl = this.admissionControl;
				double num;
				if (!admissionControl.TryAcquire(this.Owner.WorkloadClassification, out num))
				{
					ResourceLoad currentLoad = this.GetCurrentLoad();
					throw new WlmResourceUnhealthyException(this.Owner.ResourceName, this.Owner.ResourceType, this.WlmResourceKey.ToString(), (int)this.WlmResourceKey.MetricType, currentLoad.LoadRatio, currentLoad.State.ToString(), (currentLoad.Metric != null) ? currentLoad.Metric.ToString() : "(null)");
				}
				reservation.AddReleaseAction(delegate(ReservationBase r)
				{
					this.ReleaseReservation(r, admissionControl);
				});
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0004077C File Offset: 0x0003E97C
		public WlmResourceHealthMonitorDiagnosticInfoXML PopulateDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			if (arguments.HasArgument("unhealthy") && !this.IsUnhealthy)
			{
				return null;
			}
			ResourceLoad currentLoad = this.GetCurrentLoad();
			WlmResourceHealthMonitorDiagnosticInfoXML wlmResourceHealthMonitorDiagnosticInfoXML = new WlmResourceHealthMonitorDiagnosticInfoXML
			{
				WlmResourceKey = this.WlmResourceKey.ToString(),
				DynamicCapacity = (double)this.DynamicCapacity,
				LoadState = currentLoad.State.ToString(),
				LoadRatio = currentLoad.LoadRatio,
				Metric = ((currentLoad.Metric != null) ? currentLoad.Metric.ToString() : string.Empty),
				IsDisabled = (this.IsDisabled ? "true" : null),
				DynamicThrottingDisabled = (this.DynamicThrottlingDisabled ? "true" : null)
			};
			if (arguments.HasArgument("healthstats"))
			{
				wlmResourceHealthMonitorDiagnosticInfoXML.WlmHealthStatistics = this.healthTracker.GetStats();
			}
			return wlmResourceHealthMonitorDiagnosticInfoXML;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00040870 File Offset: 0x0003EA70
		private ResourceLoad GetCurrentLoad()
		{
			ResourceLoad result;
			if (this.IsDisabled)
			{
				result = ResourceLoad.Zero;
			}
			else if (TestIntegration.Instance.AssumeWLMUnhealthyForReservations)
			{
				result = new ResourceLoad(12321.0, new int?(12321), null);
			}
			else
			{
				IResourceLoadMonitor resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(this.WlmResourceKey);
				if (resourceLoadMonitor == null)
				{
					result = new ResourceLoad(24642.0, new int?(24642), null);
				}
				else
				{
					result = resourceLoadMonitor.GetResourceLoad(this.Owner.WorkloadClassification, false, null);
				}
			}
			this.ResourceHealthPerfCounter.RawValue = (long)result.State;
			return result;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0004090F File Offset: 0x0003EB0F
		private void ResetAdmissionControl(ResourceKey key)
		{
			this.admissionControl = new DefaultAdmissionControl(this.WlmResourceKey, new RemoveResourceDelegate(this.ResetAdmissionControl), null, "MRS_WlmResourceHealthMonitor");
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00040934 File Offset: 0x0003EB34
		private void ReleaseReservation(ReservationBase reservation, IResourceAdmissionControl admissionControl)
		{
			try
			{
				admissionControl.Release(this.Owner.WorkloadClassification);
			}
			catch (NonOperationalAdmissionControlException ex)
			{
				MrsTracer.Common.Warning("Releasing a reservation from a non-operational AdmissionControl instance. Ignoring exception {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex, true)
				});
			}
		}

		// Token: 0x04000C7B RID: 3195
		private const string AdmissionControlOwner = "MRS_WlmResourceHealthMonitor";

		// Token: 0x04000C7C RID: 3196
		private WlmHealthSLA healthTracker;

		// Token: 0x04000C7D RID: 3197
		private DefaultAdmissionControl admissionControl;

		// Token: 0x04000C7E RID: 3198
		private GenericSettingsContext configContext;
	}
}
