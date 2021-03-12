using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009FD RID: 2557
	internal class ResourceLoadPerfCounterWrapper
	{
		// Token: 0x0600766C RID: 30316 RVA: 0x00185A3C File Offset: 0x00183C3C
		public static ResourceLoadPerfCounterWrapper Get(ResourceKey resource, WorkloadClassification classification)
		{
			ResourceLoadPerfCounterWrapper resourceLoadPerfCounterWrapper = null;
			Tuple<ResourceKey, WorkloadClassification> key = new Tuple<ResourceKey, WorkloadClassification>(resource, classification);
			lock (ResourceLoadPerfCounterWrapper.staticLock)
			{
				if (ResourceLoadPerfCounterWrapper.instances.TryGetValue(key, out resourceLoadPerfCounterWrapper))
				{
					resourceLoadPerfCounterWrapper.lastUpdateUtc = DateTime.UtcNow;
				}
			}
			if (resourceLoadPerfCounterWrapper == null)
			{
				if (ResourceHealthMonitorManager.Active)
				{
					string text = string.Concat(new object[]
					{
						ResourceLoadPerfCounterWrapper.GetDefaultInstanceName(),
						"_",
						resource,
						"_",
						classification
					});
					try
					{
						MSExchangeResourceLoadInstance instance = MSExchangeResourceLoad.GetInstance(text);
						resourceLoadPerfCounterWrapper = new ResourceLoadPerfCounterWrapper(instance);
					}
					catch (Exception ex)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_InitializeResourceHealthPerformanceCountersFailed, string.Empty, new object[]
						{
							ResourceLoadPerfCounterWrapper.GetDefaultInstanceName(),
							ex.ToString()
						});
						ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string, string>(0L, "[ResourceLoadPerfCounterWrapper::Get] Perf counter initialization failed for key instance: {0} with exception type: {1}, Messsage: {2}", text, ex.GetType().FullName, ex.Message);
					}
				}
				if (resourceLoadPerfCounterWrapper == null)
				{
					resourceLoadPerfCounterWrapper = new ResourceLoadPerfCounterWrapper(null);
				}
				lock (ResourceLoadPerfCounterWrapper.staticLock)
				{
					ResourceLoadPerfCounterWrapper resourceLoadPerfCounterWrapper2;
					if (!ResourceLoadPerfCounterWrapper.instances.TryGetValue(key, out resourceLoadPerfCounterWrapper2))
					{
						ResourceLoadPerfCounterWrapper.instances.Add(key, resourceLoadPerfCounterWrapper);
					}
					else
					{
						resourceLoadPerfCounterWrapper = resourceLoadPerfCounterWrapper2;
					}
				}
			}
			return resourceLoadPerfCounterWrapper;
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x00185BB4 File Offset: 0x00183DB4
		internal static string GetDefaultInstanceName()
		{
			if (ResourceLoadPerfCounterWrapper.defaultInstanceName == null)
			{
				string text = null;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					text = currentProcess.ProcessName;
					int id = currentProcess.Id;
					if (text.IndexOf("w3wp", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						ServerManager serverManager = new ServerManager();
						WorkerProcessCollection workerProcesses = serverManager.WorkerProcesses;
						foreach (WorkerProcess workerProcess in workerProcesses)
						{
							try
							{
								if (id == workerProcess.ProcessId)
								{
									text = workerProcess.AppPoolName;
									break;
								}
							}
							catch (InvalidOperationException)
							{
							}
						}
					}
				}
				ResourceLoadPerfCounterWrapper.defaultInstanceName = text;
			}
			return ResourceLoadPerfCounterWrapper.defaultInstanceName;
		}

		// Token: 0x0600766E RID: 30318 RVA: 0x00185C84 File Offset: 0x00183E84
		private static void CleanupStaleCounters(object state)
		{
			Tuple<ResourceKey, WorkloadClassification>[] array = null;
			lock (ResourceLoadPerfCounterWrapper.staticLock)
			{
				array = new Tuple<ResourceKey, WorkloadClassification>[ResourceLoadPerfCounterWrapper.instances.Keys.Count];
				ResourceLoadPerfCounterWrapper.instances.Keys.CopyTo(array, 0);
			}
			foreach (Tuple<ResourceKey, WorkloadClassification> key in array)
			{
				lock (ResourceLoadPerfCounterWrapper.staticLock)
				{
					ResourceLoadPerfCounterWrapper resourceLoadPerfCounterWrapper = null;
					if (ResourceLoadPerfCounterWrapper.instances.TryGetValue(key, out resourceLoadPerfCounterWrapper) && DateTime.UtcNow - resourceLoadPerfCounterWrapper.lastUpdateUtc > ResourceLoadPerfCounterWrapper.CleanupWindow)
					{
						if (resourceLoadPerfCounterWrapper.perfCounters != null)
						{
							resourceLoadPerfCounterWrapper.perfCounters.Remove();
						}
						ResourceLoadPerfCounterWrapper.instances.Remove(key);
					}
				}
			}
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x00185D7C File Offset: 0x00183F7C
		private ResourceLoadPerfCounterWrapper(MSExchangeResourceLoadInstance instance)
		{
			this.perfCounters = instance;
			this.perfCounterResourceMetric = this.CreateSetCounter((this.perfCounters == null) ? null : this.perfCounters.ResourceMetric);
			this.perfCounterResourceLoad = this.CreateSetCounter((this.perfCounters == null) ? null : this.perfCounters.ResourceLoad);
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x00185DE8 File Offset: 0x00183FE8
		private SetCounterIf CreateSetCounter(ExPerformanceCounter perfCounter)
		{
			return new SetCounterIf((perfCounter == null) ? null : new SetCounterIf.CounterWrapper(perfCounter), CounterCompareType.Changed, (int)ResourceLoadPerfCounterWrapper.RefreshWindow.TotalMilliseconds);
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x00185E18 File Offset: 0x00184018
		public void Update(int metric, ResourceLoad load)
		{
			if (this.perfCounters != null)
			{
				try
				{
					this.perfCounterResourceMetric.Set((long)metric);
					this.perfCounterResourceLoad.Set(this.Convert(load));
				}
				catch (Exception ex)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<string>((long)this.GetHashCode(), "[ResourceLoadPerfCounterWrapper::Update] Failed to update perf counters. Disabling perf counter updates. Error: {0}", ex.Message);
					this.perfCounters = null;
				}
			}
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x00185E84 File Offset: 0x00184084
		private long Convert(ResourceLoad load)
		{
			ResourceLoadState state = load.State;
			if (state == ResourceLoadState.Unknown)
			{
				return 0L;
			}
			if (state == ResourceLoadState.Critical)
			{
				return long.MaxValue;
			}
			if (load.LoadRatio >= 92233720368547760.0)
			{
				return long.MaxValue;
			}
			return (long)(load.LoadRatio * 100.0);
		}

		// Token: 0x04004BBE RID: 19390
		private static readonly TimeSpan RefreshWindow = TimeSpan.FromSeconds(1.0);

		// Token: 0x04004BBF RID: 19391
		private static readonly TimeSpan CleanupWindow = TimeSpan.FromMinutes(5.0);

		// Token: 0x04004BC0 RID: 19392
		private static object staticLock = new object();

		// Token: 0x04004BC1 RID: 19393
		private static Dictionary<Tuple<ResourceKey, WorkloadClassification>, ResourceLoadPerfCounterWrapper> instances = new Dictionary<Tuple<ResourceKey, WorkloadClassification>, ResourceLoadPerfCounterWrapper>();

		// Token: 0x04004BC2 RID: 19394
		private static Timer cleanupTimer = new Timer(new TimerCallback(ResourceLoadPerfCounterWrapper.CleanupStaleCounters), null, ResourceLoadPerfCounterWrapper.CleanupWindow, ResourceLoadPerfCounterWrapper.CleanupWindow);

		// Token: 0x04004BC3 RID: 19395
		private static string defaultInstanceName = null;

		// Token: 0x04004BC4 RID: 19396
		private MSExchangeResourceLoadInstance perfCounters;

		// Token: 0x04004BC5 RID: 19397
		private SetCounterIf perfCounterResourceMetric;

		// Token: 0x04004BC6 RID: 19398
		private SetCounterIf perfCounterResourceLoad;

		// Token: 0x04004BC7 RID: 19399
		private DateTime lastUpdateUtc = DateTime.UtcNow;
	}
}
