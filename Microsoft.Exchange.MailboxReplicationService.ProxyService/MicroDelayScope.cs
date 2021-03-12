using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal class MicroDelayScope : DisposeTrackableBase
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003310 File Offset: 0x00001510
		private MicroDelayScope(MailboxReplicationProxyService mrsProxy, params ResourceKey[] resources)
		{
			this.startedCallProcessingAt = ExDateTime.UtcNow;
			this.resources = resources;
			this.budget = StandardBudget.AcquireUnthrottledBudget("MrsProxyBudget", BudgetType.ResourceTracking);
			this.workLoadSettings = (mrsProxy.IsHighPriority ? CommonUtils.WorkloadSettingsHighPriority : CommonUtils.WorkloadSettings);
			this.skipWLMThrottling = (!ResourceHealthMonitorManager.Active || !TestIntegration.Instance.MicroDelayEnabled || mrsProxy.SkipWLMThrottling || mrsProxy.IsInFinalization);
			bool flag = false;
			try
			{
				if (!this.skipWLMThrottling)
				{
					ResourceLoadDelayInfo.CheckResourceHealth(this.budget, this.workLoadSettings, this.resources);
				}
				this.budget.StartConnection("MailboxReplicationService.MicroDelayScope.MicroDelayScope");
				this.budget.StartLocal("MailboxReplicationService.MicroDelayScope.MicroDelayScope", default(TimeSpan));
				if (!ActivityContext.IsStarted)
				{
					this.scope = ActivityContext.Start(null);
					this.scope.Action = "MailboxReplicationProxyService";
					if (OperationContext.Current != null)
					{
						this.scope.UpdateFromMessage(OperationContext.Current);
					}
					this.scope.UserId = mrsProxy.ExchangeGuid.ToString();
					if (mrsProxy.ClientVersion != null)
					{
						this.scope.ClientInfo = mrsProxy.ClientVersion.ComputerName;
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.SuppressDisposeTracker();
					if (this.budget != null)
					{
						this.budget.Dispose();
						this.budget = null;
					}
					if (this.scope != null)
					{
						this.scope.End();
						this.scope = null;
					}
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000034A0 File Offset: 0x000016A0
		public static MicroDelayScope Create(MailboxReplicationProxyService mrsProxy, DelayScopeKind kind)
		{
			Guid guid = ConfigBase<MRSConfigSchema>.CurrentContext.DatabaseGuid ?? Guid.Empty;
			if (guid == Guid.Empty && (kind == DelayScopeKind.DbRead || kind == DelayScopeKind.DbWrite))
			{
				kind = DelayScopeKind.CPUOnly;
			}
			switch (kind)
			{
			case DelayScopeKind.CPUOnly:
				return new MicroDelayScope(mrsProxy, MicroDelayScope.LocalCpuResourcesOnly);
			case DelayScopeKind.DbRead:
				if (mrsProxy.IsE15OrHigher)
				{
					return new MicroDelayScope(mrsProxy, new ResourceKey[]
					{
						ProcessorResourceKey.Local,
						new MdbResourceHealthMonitorKey(guid),
						new DiskLatencyResourceKey(guid)
					});
				}
				return new MicroDelayScope(mrsProxy, new ResourceKey[]
				{
					ProcessorResourceKey.Local,
					new MdbResourceHealthMonitorKey(guid)
				});
			case DelayScopeKind.DbWrite:
				if (mrsProxy.IsE15OrHigher)
				{
					return new MicroDelayScope(mrsProxy, new ResourceKey[]
					{
						ProcessorResourceKey.Local,
						new MdbResourceHealthMonitorKey(guid),
						new MdbReplicationResourceHealthMonitorKey(guid),
						new MdbAvailabilityResourceHealthMonitorKey(guid),
						new CiAgeOfLastNotificationResourceKey(guid),
						new DiskLatencyResourceKey(guid)
					});
				}
				return new MicroDelayScope(mrsProxy, new ResourceKey[]
				{
					ProcessorResourceKey.Local,
					new MdbResourceHealthMonitorKey(guid),
					new LegacyResourceHealthMonitorKey(guid)
				});
			}
			return null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000035EC File Offset: 0x000017EC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.budget != null)
				{
					if (!this.skipWLMThrottling)
					{
						TimeSpan timeSpan = MicroDelayScope.maxCallProcessingTime - (ExDateTime.UtcNow - this.startedCallProcessingAt);
						if (timeSpan < TimeSpan.Zero)
						{
							timeSpan = TimeSpan.Zero;
						}
						DelayEnforcementResults delayEnforcement = ResourceLoadDelayInfo.EnforceDelay(this.budget, this.workLoadSettings, this.resources, timeSpan, null);
						this.TraceDelay(delayEnforcement);
					}
					this.budget.Dispose();
					this.budget = null;
				}
				if (this.scope != null)
				{
					this.scope.End();
					this.scope = null;
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000368A File Offset: 0x0000188A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MicroDelayScope>(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003694 File Offset: 0x00001894
		private void TraceDelay(DelayEnforcementResults delayEnforcement)
		{
			if (delayEnforcement.DelayInfo != DelayInfo.NoDelay)
			{
				MrsTracer.ResourceHealth.Debug("Micro Delay: {0} msec due to resource: '{1}'", new object[]
				{
					delayEnforcement.DelayedAmount.TotalMilliseconds,
					(delayEnforcement.DelayInfo as ResourceLoadDelayInfo).ResourceKey
				});
			}
		}

		// Token: 0x04000026 RID: 38
		private const string CallerDescription = "MailboxReplicationService.MicroDelayScope.MicroDelayScope";

		// Token: 0x04000027 RID: 39
		private static readonly TimeSpan maxCallProcessingTime = TimeSpan.FromSeconds(40.0);

		// Token: 0x04000028 RID: 40
		private static readonly ResourceKey[] LocalCpuResourcesOnly = new ResourceKey[]
		{
			ProcessorResourceKey.Local
		};

		// Token: 0x04000029 RID: 41
		private readonly ExDateTime startedCallProcessingAt;

		// Token: 0x0400002A RID: 42
		private readonly bool skipWLMThrottling;

		// Token: 0x0400002B RID: 43
		private ResourceKey[] resources;

		// Token: 0x0400002C RID: 44
		private IStandardBudget budget;

		// Token: 0x0400002D RID: 45
		private IActivityScope scope;

		// Token: 0x0400002E RID: 46
		private WorkloadSettings workLoadSettings;
	}
}
