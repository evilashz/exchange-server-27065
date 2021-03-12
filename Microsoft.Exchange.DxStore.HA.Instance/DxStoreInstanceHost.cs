using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using FUSE.Paxos;
using FUSE.Weld.Base;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;
using Microsoft.Exchange.DxStore.Server;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x02000002 RID: 2
	internal class DxStoreInstanceHost
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DxStoreInstanceHost(string groupName, string self = null, bool isZeroboxMode = false)
		{
			string instanceName = "dxstore-" + groupName;
			if (!isZeroboxMode)
			{
				DxStoreSetting.RegisterADPerfCounters(instanceName);
			}
			this.GroupName = groupName;
			this.IsZeroboxMode = isZeroboxMode;
			this.EventLogger = new DistributedStoreEventLogger(isZeroboxMode);
			this.ConfigProvider = new DistributedStoreTopologyProvider(this.EventLogger, self, isZeroboxMode);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000212D File Offset: 0x0000032D
		public string GroupName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000213E File Offset: 0x0000033E
		public DxStoreInstance Instance { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000214F File Offset: 0x0000034F
		public bool IsZeroboxMode { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		public DistributedStoreTopologyProvider ConfigProvider { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002169 File Offset: 0x00000369
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002171 File Offset: 0x00000371
		public DistributedStoreEventLogger EventLogger { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000217A File Offset: 0x0000037A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002182 File Offset: 0x00000382
		public InstanceGroupConfig GroupConfig { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		public bool Start(bool isBestEffort)
		{
			bool result = false;
			try
			{
				this.RestrictProcessorAffinity();
				this.GroupConfig = this.ConfigProvider.GetGroupConfig(this.GroupName, this.IsZeroboxMode);
				if (this.GroupConfig != null)
				{
					this.RegisterPerformanceCounters();
					if (DataStoreSettings.IsRunningOnTestBox && this.GroupConfig.Settings.AdditionalLogOptions == LogOptions.None)
					{
						this.GroupConfig.Settings.AdditionalLogOptions = (LogOptions.LogException | LogOptions.LogStart | LogOptions.LogSuccess | LogOptions.LogExceptionFull | LogOptions.LogAlways);
					}
					this.Instance = new DxStoreInstance(this.GroupConfig, this.EventLogger);
					this.Instance.Start();
					result = true;
				}
				else
				{
					DxStoreHACrimsonEvents.FailedToStartInstance.Log<string, string>(this.GroupName, "Group config not found");
				}
			}
			catch (Exception ex)
			{
				DxStoreHACrimsonEvents.FailedToStartInstance.Log<string, Exception>(this.GroupName, ex);
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002264 File Offset: 0x00000464
		public void RegisterPerformanceCounters()
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";
			string valueName = "IsDxStorePerformanceCountersRegistered";
			if ((int)Registry.GetValue(keyName, valueName, 0) > 0)
			{
				return;
			}
			using (Mutex mutex = new Mutex(false, "DxStorePerformanceCounter_Mutex"))
			{
				mutex.WaitOne();
				if ((int)Registry.GetValue(keyName, valueName, 0) == 0)
				{
					PerformanceCounters.Create(new Type[]
					{
						typeof(Counters),
						typeof(MessageCounters)
					});
					Registry.SetValue(keyName, valueName, 1);
				}
				mutex.ReleaseMutex();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002310 File Offset: 0x00000510
		public void RestrictProcessorAffinity()
		{
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					int processorCount = Environment.ProcessorCount;
					int i = Math.Min(16, Math.Max(processorCount / 4, 1));
					long num = (long)currentProcess.ProcessorAffinity;
					long num2 = num;
					long num3 = 1L;
					while (i > 0)
					{
						num2 &= ~num3;
						num3 <<= 1;
						i--;
					}
					this.EventLogger.Log(DxEventSeverity.Info, 0, "Attempting to set processor affinity. (Total processors: {0}, Initial affinity: {1}, New affinity: {2}", new object[]
					{
						processorCount,
						Convert.ToString(num, 2),
						Convert.ToString(num2, 2)
					});
					currentProcess.ProcessorAffinity = (IntPtr)num2;
				}
			}
			catch (Win32Exception ex)
			{
				this.EventLogger.Log(DxEventSeverity.Error, 0, "Failed to set processor affinity (error: {0})", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002400 File Offset: 0x00000600
		public void WaitForStop()
		{
			if (this.Instance != null && this.Instance.StopCompletedEvent != null)
			{
				this.Instance.StopCompletedEvent.WaitOne();
			}
		}
	}
}
