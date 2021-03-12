using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.HA.ManagedAvailability
{
	// Token: 0x020001C0 RID: 448
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HealthStateTracker : IServiceComponent
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x00047CDC File Offset: 0x00045EDC
		internal HealthStateTracker()
		{
			this.healthStateMap = new ConcurrentDictionary<string, MonitorResult>();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00047CFC File Offset: 0x00045EFC
		internal static bool HandleCommonExceptions(string description, Action action)
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				action();
				flag = true;
			}
			catch (EventLogException ex2)
			{
				ex = ex2;
			}
			catch (Win32Exception ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (!flag)
				{
					ReplayCrimsonEvents.HealthStateTrackerError.Log<string, string>(description, (ex != null) ? ex.ToString() : "<UnhandledException>");
				}
			}
			return flag;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00047D6C File Offset: 0x00045F6C
		internal void ResultArrived(MonitorResult result)
		{
			this.healthStateMap[result.ResultName] = result;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00047D80 File Offset: 0x00045F80
		private void ReadExisting()
		{
			using (CrimsonReader<MonitorResult> crimsonReader = new CrimsonReader<MonitorResult>())
			{
				crimsonReader.QueryUserPropertyCondition = "(IsHaImpacting=1)";
				crimsonReader.QueryEndTime = new DateTime?(DateTime.UtcNow);
				crimsonReader.QueryStartTime = crimsonReader.QueryEndTime - TimeSpan.FromSeconds((double)RegistryParameters.HealthStateTrackerLookupDurationInSec);
				while (!crimsonReader.EndOfEventsReached)
				{
					MonitorResult monitorResult = crimsonReader.ReadNext();
					if (monitorResult != null)
					{
						this.healthStateMap[monitorResult.ResultName] = monitorResult;
					}
				}
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00047E30 File Offset: 0x00046030
		private string GetTargetDatabaseName(MonitorResult result)
		{
			string result2 = null;
			if (result.GetHaScope() == HaScopeEnum.Database)
			{
				string[] array = result.ResultName.Split(new char[]
				{
					'/'
				});
				if (array.Length >= 2)
				{
					result2 = array[1];
				}
			}
			return result2;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00047EAC File Offset: 0x000460AC
		internal RpcHealthStateInfo[] GetComponentStates()
		{
			IEnumerable<RpcHealthStateInfo> source = from result in this.healthStateMap.Values
			where result != null
			select new RpcHealthStateInfo(result.Component.Name, (int)result.Component.Priority, result.ResultName, this.GetTargetDatabaseName(result), (int)result.HealthState, result.ExecutionEndTime);
			return source.ToArray<RpcHealthStateInfo>();
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00047EFE File Offset: 0x000460FE
		public string Name
		{
			get
			{
				return "Managed Availability Component";
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00047F05 File Offset: 0x00046105
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.HealthStateTracker;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00047F09 File Offset: 0x00046109
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00047F0C File Offset: 0x0004610C
		public bool IsEnabled
		{
			get
			{
				return !RegistryParameters.HealthStateTrackerDisabled;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00047F16 File Offset: 0x00046116
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00047F19 File Offset: 0x00046119
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00047F21 File Offset: 0x00046121
		public bool Start()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartupThread));
			return true;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00047F90 File Offset: 0x00046190
		private void StartupThread(object notUsed)
		{
			lock (this.locker)
			{
				HealthStateTracker.HandleCommonExceptions("HealthStateTracker.Start", delegate
				{
					this.ReadExisting();
				});
				HealthStateTracker.HandleCommonExceptions("HealthStateTracker.SetupWatcher", delegate
				{
					this.monitorResultWatcher = new ResultWatcher<MonitorResult>(null, null, false);
					this.monitorResultWatcher.ResultArrivedCallback = new ResultWatcher<MonitorResult>.ResultArrivedDelegate(this.ResultArrived);
					this.monitorResultWatcher.QueryUserPropertyCondition = "(IsHaImpacting=1)";
					this.monitorResultWatcher.Start();
				});
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00048018 File Offset: 0x00046218
		public void Stop()
		{
			lock (this.locker)
			{
				if (this.monitorResultWatcher != null)
				{
					HealthStateTracker.HandleCommonExceptions("HealthStateTracker.Stop", delegate
					{
						this.monitorResultWatcher.Stop();
					});
					this.monitorResultWatcher = null;
				}
			}
		}

		// Token: 0x040006AF RID: 1711
		private const string QueryCondition = "(IsHaImpacting=1)";

		// Token: 0x040006B0 RID: 1712
		private ResultWatcher<MonitorResult> monitorResultWatcher;

		// Token: 0x040006B1 RID: 1713
		private ConcurrentDictionary<string, MonitorResult> healthStateMap;

		// Token: 0x040006B2 RID: 1714
		private object locker = new object();
	}
}
