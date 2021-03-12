using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200000A RID: 10
	internal class ResourceLoadDelayInfo : DelayInfo
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002442 File Offset: 0x00000642
		public ResourceLoadDelayInfo(TimeSpan delay, ResourceKey resourceKey, ResourceLoad resourceLoad, TimeSpan workAccomplished, bool required) : base(delay, required)
		{
			this.ResourceKey = resourceKey;
			this.ResourceLoad = resourceLoad;
			this.WorkAccomplished = workAccomplished;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002463 File Offset: 0x00000663
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000246A File Offset: 0x0000066A
		public static bool IgnoreSleepsForTest { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002472 File Offset: 0x00000672
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002479 File Offset: 0x00000679
		public static Func<DelayEnforcementResults> EnforceDelayTestHook { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002489 File Offset: 0x00000689
		public ResourceKey ResourceKey { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000249A File Offset: 0x0000069A
		public ResourceLoad ResourceLoad { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000024A3 File Offset: 0x000006A3
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000024AB File Offset: 0x000006AB
		public TimeSpan WorkAccomplished { get; private set; }

		// Token: 0x0600005A RID: 90 RVA: 0x000024B4 File Offset: 0x000006B4
		public static void Initialize()
		{
			SettingOverrideSync.Instance.Start(true);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000024C4 File Offset: 0x000006C4
		public static void CheckResourceHealth(IBudget budget, WorkloadSettings settings, ResourceKey[] resourcesToAccess)
		{
			ResourceUnhealthyException ex = null;
			if (ResourceLoadDelayInfo.TryCheckResourceHealth(budget, settings, resourcesToAccess, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000024E4 File Offset: 0x000006E4
		public static bool TryCheckResourceHealth(IBudget budget, WorkloadSettings settings, ResourceKey[] resourcesToAccess, out ResourceUnhealthyException resourceUnhealthyException)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			resourceUnhealthyException = null;
			if (budget.ThrottlingPolicy.IsServiceAccount || settings.IsBackgroundLoad)
			{
				ResourceKey resourceKey;
				ResourceLoad resourceLoad;
				ResourceLoadDelayInfo.GetWorstResource(settings.WorkloadType, resourcesToAccess, out resourceKey, out resourceLoad);
				if (resourceKey != null && resourceLoad.State == ResourceLoadState.Critical)
				{
					resourceUnhealthyException = new ResourceUnhealthyException(resourceKey);
				}
			}
			return resourceUnhealthyException != null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002546 File Offset: 0x00000746
		public static DelayInfo GetDelay(IBudget budget, WorkloadSettings settings, ResourceKey[] resourcesToAccess, bool scopeDelay = true)
		{
			return ResourceLoadDelayInfo.GetDelay(budget, settings, Budget.AllCostTypes, resourcesToAccess, scopeDelay);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002558 File Offset: 0x00000758
		public static DelayInfo GetDelay(IBudget budget, WorkloadSettings settings, ICollection<CostType> costTypesToConsider, ResourceKey[] resourcesToAccess, bool scopeDelay = true)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			ResourceLoadDelayInfo resourceLoadDelayInfo = null;
			DelayInfo delay = budget.GetDelay(costTypesToConsider);
			DelayInfo delayInfo = delay;
			if (delay.Delay == Budget.IndefiniteDelay)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey>(0L, "[ResourceLoadDelayInfo.GetDelay] The user delay for '{0}' was Int32.MaxValue, so no need to consider resource health based delay.", budget.Owner);
			}
			else
			{
				if (settings.IsBackgroundLoad || budget.ThrottlingPolicy.IsServiceAccount)
				{
					resourceLoadDelayInfo = ResourceLoadDelayInfo.GetResourceLoadDelayInfo(budget, settings.WorkloadType, resourcesToAccess);
				}
				else
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug(0L, "[ResourceLoadDelayInfo.GetWorstResource] The work is interactive and therefore will not consider the health of the resources.");
				}
				if (resourceLoadDelayInfo != null && resourceLoadDelayInfo.Delay > delay.Delay)
				{
					delayInfo = resourceLoadDelayInfo;
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug(0L, "[ResourceLoadDelayInfo.GetDelay] Resource delay for '{0}' was greater than user delay for '{1}' of '{2}'. Using resource delay '{3}'.", new object[]
					{
						resourceLoadDelayInfo.ResourceKey,
						budget.Owner,
						delay.Delay,
						delayInfo.Delay
					});
				}
			}
			if (delayInfo != null)
			{
				string instance = ResourceLoadDelayInfo.GetInstance(delayInfo);
				bool flag = delayInfo is ResourceLoadDelayInfo;
				if (delayInfo.Delay == Budget.IndefiniteDelay && flag)
				{
					WorkloadManagementLogger.SetResourceBlocked(instance, null);
				}
				else if (delayInfo.Delay != TimeSpan.Zero && scopeDelay)
				{
					WorkloadManagementLogger.SetThrottlingValues(delayInfo.Delay, !flag, instance, null);
				}
			}
			return delayInfo;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000026A9 File Offset: 0x000008A9
		public static DelayEnforcementResults EnforceDelay(IBudget budget, WorkloadSettings settings, ResourceKey[] resourcesToAccess, TimeSpan preferredMaxDelay, Func<DelayInfo, bool> onBeforeDelay)
		{
			return ResourceLoadDelayInfo.EnforceDelay(budget, settings, Budget.AllCostTypes, resourcesToAccess, preferredMaxDelay, onBeforeDelay);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000026BC File Offset: 0x000008BC
		public static DelayEnforcementResults EnforceDelay(IBudget budget, WorkloadSettings settings, ICollection<CostType> costTypesToConsider, ResourceKey[] resourcesToAccess, TimeSpan preferredMaxDelay, Func<DelayInfo, bool> onBeforeDelay)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			if (ResourceLoadDelayInfo.EnforceDelayTestHook != null)
			{
				return ResourceLoadDelayInfo.EnforceDelayTestHook();
			}
			budget.EndLocal();
			DelayInfo delay = ResourceLoadDelayInfo.GetDelay(budget, settings, costTypesToConsider, resourcesToAccess, false);
			string text = null;
			TimeSpan timeSpan = TimeSpan.Zero;
			if (delay.Delay == TimeSpan.Zero)
			{
				text = "No Delay Necessary";
			}
			else if (delay.Required && delay.Delay > preferredMaxDelay)
			{
				text = "Strict Delay Exceeds Preferred Delay";
			}
			else
			{
				timeSpan = ((delay.Delay > preferredMaxDelay) ? preferredMaxDelay : delay.Delay);
				if (timeSpan.TotalMilliseconds > 2147483647.0)
				{
					text = "Delay Too Long";
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey, string>(0L, "[ResourceLoadDelayInfo.EnforceDelay] Budget: {0}.  Not enforcing delay for reason {1}", budget.Owner, text);
				return new DelayEnforcementResults(delay, text);
			}
			bool flag = false;
			DelayEnforcementResults result;
			try
			{
				BudgetTypeSetting budgetTypeSetting = BudgetTypeSettings.Get(budget.Owner.BudgetType);
				if (onBeforeDelay == null || onBeforeDelay(delay))
				{
					lock (ResourceLoadDelayInfo.staticLock)
					{
						if (ResourceLoadDelayInfo.delayedThreads >= budgetTypeSetting.MaxDelayedThreads - 1)
						{
							ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey, string>(0L, "[ResourceLoadDelayInfo.EnforceDelay] Budget: {0}.  Not enforcing delay for reason {1}", budget.Owner, "Max Delayed Threads Exceeded");
							return new DelayEnforcementResults(delay, "Max Delayed Threads Exceeded");
						}
						ResourceLoadDelayInfo.delayedThreads++;
						ThrottlingPerfCounterWrapper.SetDelayedThreads((long)ResourceLoadDelayInfo.delayedThreads);
						flag = true;
					}
					budget.ResetWorkAccomplished();
					if (!ResourceLoadDelayInfo.IgnoreSleepsForTest)
					{
						ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey, TimeSpan, TimeSpan>(0L, "[ResourceLoadDelayInfo.EnforceDelay] Budget: {0} sleeping for {1}.  Capped Delay: {2}.", budget.Owner, timeSpan, delay.Delay);
						string instance = ResourceLoadDelayInfo.GetInstance(delay);
						WorkloadManagementLogger.SetThrottlingValues(timeSpan, !(delay is ResourceLoadDelayInfo), instance, null);
						Thread.Sleep(timeSpan);
					}
					result = new DelayEnforcementResults(delay, timeSpan);
				}
				else
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey>(0L, "[ResourceLoadDelayInfo.EnforceDelay] Budget: {0} did not sleep because OnBeforeDelay callback returned false.", budget.Owner);
					result = new DelayEnforcementResults(delay, "OnBeforeDelay delegate returned false");
				}
			}
			finally
			{
				if (flag)
				{
					lock (ResourceLoadDelayInfo.staticLock)
					{
						ResourceLoadDelayInfo.delayedThreads--;
						ThrottlingPerfCounterWrapper.SetDelayedThreads((long)ResourceLoadDelayInfo.delayedThreads);
					}
				}
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000293C File Offset: 0x00000B3C
		public static string GetInstance(DelayInfo delayInfo)
		{
			if (delayInfo is ResourceLoadDelayInfo)
			{
				return ((ResourceLoadDelayInfo)delayInfo).ResourceKey.ToString();
			}
			return null;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002958 File Offset: 0x00000B58
		public static void GetWorstResource(WorkloadType workloadType, ResourceKey[] resourcesToAccess, out ResourceKey worstResource, out ResourceLoad worstLoad)
		{
			ResourceLoad[] array = null;
			ResourceLoadDelayInfo.GetWorstResourceHelper(workloadType, resourcesToAccess, false, out array, out worstResource, out worstLoad);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002973 File Offset: 0x00000B73
		public static void GetWorstResourceAndAllHealthValues(WorkloadType workloadType, ResourceKey[] resourcesToAccess, out ResourceLoad[] resourceLoadList, out ResourceKey worstResource, out ResourceLoad worstLoad)
		{
			ResourceLoadDelayInfo.GetWorstResourceHelper(workloadType, resourcesToAccess, true, out resourceLoadList, out worstResource, out worstLoad);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002984 File Offset: 0x00000B84
		private static void GetWorstResourceHelper(WorkloadType workloadType, ResourceKey[] resourcesToAccess, bool getAllResourceLoads, out ResourceLoad[] resourceLoadList, out ResourceKey worstResource, out ResourceLoad worstLoad)
		{
			resourceLoadList = null;
			worstResource = null;
			worstLoad = ResourceLoad.Zero;
			WorkloadPolicy workloadPolicy = new WorkloadPolicy(workloadType);
			if (resourcesToAccess == null || resourcesToAccess.Length == 0)
			{
				return;
			}
			resourceLoadList = (getAllResourceLoads ? new ResourceLoad[resourcesToAccess.Length] : null);
			for (int i = 0; i < resourcesToAccess.Length; i++)
			{
				IResourceLoadMonitor resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(resourcesToAccess[i]);
				if (resourceLoadMonitor == null)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug<ResourceKey, string>(0L, "[ResourceLoadDelayInfo.GetWorstResource] Monitor '{0}' does not implement IResourceLoadMonitor.  Ignoring for delay calculation.  Actual type: {1}", resourcesToAccess[i], resourcesToAccess[i].GetType().FullName);
				}
				ResourceLoad resourceLoad = resourceLoadMonitor.GetResourceLoad(workloadPolicy.Classification, false, null);
				if (resourceLoadList != null)
				{
					resourceLoadList[i] = resourceLoad;
				}
				if (resourceLoad != ResourceLoad.Unknown && resourceLoad.LoadRatio > worstLoad.LoadRatio)
				{
					worstLoad = resourceLoad;
					worstResource = resourcesToAccess[i];
					if (worstLoad == ResourceLoad.Critical)
					{
						return;
					}
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002A6C File Offset: 0x00000C6C
		private static ResourceLoadDelayInfo GetResourceLoadDelayInfo(IBudget budget, WorkloadType workloadType, ResourceKey[] resourcesToAccess)
		{
			ResourceKey resourceKey = null;
			ResourceLoad zero = ResourceLoad.Zero;
			ResourceLoadDelayInfo.GetWorstResource(workloadType, resourcesToAccess, out resourceKey, out zero);
			if (resourceKey != null && (zero.State == ResourceLoadState.Overloaded || zero.State == ResourceLoadState.Critical))
			{
				BudgetTypeSetting budgetTypeSetting = BudgetTypeSettings.Get(budget.Owner.BudgetType);
				TimeSpan timeSpan = Budget.IndefiniteDelay;
				TimeSpan delay = Budget.IndefiniteDelay;
				if (zero.State != ResourceLoadState.Critical)
				{
					try
					{
						timeSpan = TimeSpan.FromMilliseconds((zero.LoadRatio - 1.0) * budget.ResourceWorkAccomplished.TotalMilliseconds);
					}
					catch (OverflowException)
					{
						timeSpan = Budget.IndefiniteDelay;
					}
					delay = ((timeSpan > budgetTypeSetting.MaxDelay) ? budgetTypeSetting.MaxDelay : timeSpan);
				}
				return new ResourceLoadDelayInfo(delay, resourceKey, zero, budget.ResourceWorkAccomplished, true);
			}
			return null;
		}

		// Token: 0x04000027 RID: 39
		private static object staticLock = new object();

		// Token: 0x04000028 RID: 40
		private static int delayedThreads = 0;
	}
}
