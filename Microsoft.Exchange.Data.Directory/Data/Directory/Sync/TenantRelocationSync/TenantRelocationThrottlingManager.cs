using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x02000811 RID: 2065
	internal class TenantRelocationThrottlingManager : DisposeTrackableBase
	{
		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x001690D7 File Offset: 0x001672D7
		public string PartitionFqdn
		{
			get
			{
				return this.partitionFqdn;
			}
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x001690DF File Offset: 0x001672DF
		public TenantRelocationThrottlingManager(string partitionFqdn)
		{
			this.partitionFqdn = partitionFqdn;
			this.adMonitor = ResourceHealthMonitorManager.Singleton.Get(ADResourceKey.Key);
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x00169104 File Offset: 0x00167304
		public int Throttle()
		{
			ResourceLoadState resourceLoadState;
			int healthAndCalculateDelay = this.GetHealthAndCalculateDelay(out resourceLoadState);
			Thread.Sleep(healthAndCalculateDelay);
			return healthAndCalculateDelay;
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x00169124 File Offset: 0x00167324
		public int GetHealthAndCalculateDelay(out ResourceLoadState health)
		{
			if (this.TryReadRegistryHealthOverride(out health))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ResourceLoadState, string>((long)this.GetHashCode(), "TenantRelocationThrottlingManager.Throttle() - override detected, override value:{0}. Current forest: {1}", health, this.partitionFqdn);
			}
			else
			{
				health = this.GetWlmADHealthMetric();
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ResourceLoadState, string>((long)this.GetHashCode(), "TenantRelocationThrottlingManager.Throttle() - AD Health monitor returned value:{0}. Current forest: {1}", health, this.partitionFqdn);
			}
			int num;
			switch (health)
			{
			case ResourceLoadState.Unknown:
			case ResourceLoadState.Underloaded:
				num = TenantRelocationConfigImpl.GetConfig<int>("LoadStateNoDelayMs");
				break;
			case ResourceLoadState.Full:
				num = TenantRelocationConfigImpl.GetConfig<int>("LoadStateDefaultDelayMs");
				break;
			case ResourceLoadState.Overloaded:
				num = TenantRelocationConfigImpl.GetConfig<int>("LoadStateOverloadedDelayMs");
				break;
			case ResourceLoadState.Critical:
				num = TenantRelocationConfigImpl.GetConfig<int>("LoadStateCriticalDelayMs");
				break;
			default:
				throw new NotImplementedException();
			}
			if (num < 100)
			{
				num = 100;
			}
			return num;
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x001691E2 File Offset: 0x001673E2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TenantRelocationThrottlingManager>(this);
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x001691EA File Offset: 0x001673EA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.adMonitor = null;
			}
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x001691F8 File Offset: 0x001673F8
		private ResourceLoadState GetWlmADHealthMetric()
		{
			return this.adMonitor.GetResourceLoad(WorkloadClassification.Discretionary, false, this.partitionFqdn).State;
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x00169220 File Offset: 0x00167420
		private bool TryReadRegistryHealthOverride(out ResourceLoadState healthValue)
		{
			uint num;
			bool int32ValueFromRegistryValue = TenantRelocationSyncCoordinator.GetInt32ValueFromRegistryValue("ADHealthOverrideForTenantRelocation", out num);
			if (int32ValueFromRegistryValue)
			{
				healthValue = (ResourceLoadState)num;
			}
			else
			{
				healthValue = ResourceLoadState.Unknown;
			}
			return int32ValueFromRegistryValue;
		}

		// Token: 0x04004388 RID: 17288
		private IResourceLoadMonitor adMonitor;

		// Token: 0x04004389 RID: 17289
		private readonly string partitionFqdn;
	}
}
