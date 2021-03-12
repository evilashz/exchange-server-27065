using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000E0 RID: 224
	public class ProcessKillResponder : ResponderWorkItem
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0002B39C File Offset: 0x0002959C
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0002B3A4 File Offset: 0x000295A4
		internal string ProcessName { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0002B3AD File Offset: 0x000295AD
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0002B3B5 File Offset: 0x000295B5
		internal HashSet<int> ProcessIds { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0002B3BE File Offset: 0x000295BE
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0002B3C6 File Offset: 0x000295C6
		internal bool IsMasterAndWorker { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0002B3CF File Offset: 0x000295CF
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0002B3D7 File Offset: 0x000295D7
		internal ProcessKillMode MasterAndWorkerKillMode { get; set; }

		// Token: 0x0600073B RID: 1851 RVA: 0x0002B3E0 File Offset: 0x000295E0
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, string processName, ServiceHealthStatus responderTargetState, bool isMasterWorker = false, string serviceName = "Exchange", bool enabled = true, int timeoutSeconds = 60, string throttleGroupName = null, string sampleMask = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ProcessKillResponder.AssemblyPath;
			responderDefinition.TypeName = ProcessKillResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = processName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.MaxRetryAttempts = 10;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 10;
			responderDefinition.Enabled = enabled;
			responderDefinition.TimeoutSeconds = timeoutSeconds;
			responderDefinition.Attributes["ProcessName"] = processName;
			responderDefinition.Attributes["IsMasterAndWorker"] = isMasterWorker.ToString();
			responderDefinition.Attributes["SampleMask"] = sampleMask.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.KillProcess, processName, null);
			return responderDefinition;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0002B4BC File Offset: 0x000296BC
		protected void InitializeServiceAttributes(AttributeHelper attributeHelper)
		{
			this.ProcessName = attributeHelper.GetString("ProcessName", true, null);
			string @string = attributeHelper.GetString("SampleMask", false, null);
			ProbeResult probeResult = (from r in base.Broker.GetProbeResults(@string, base.Result.ExecutionStartTime.AddHours(-1.0), base.Result.ExecutionStartTime)
			where r.ResultType == ResultType.Failed
			orderby r.ExecutionEndTime
			select r).FirstOrDefault<ProbeResult>();
			this.ProcessIds = new HashSet<int>();
			string pidsFromErrorText = ProcessIsolationDiscovery.GetPidsFromErrorText(probeResult.Error);
			if (!string.IsNullOrEmpty(pidsFromErrorText))
			{
				string[] array = pidsFromErrorText.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in array)
				{
					int item;
					if (int.TryParse(s, out item))
					{
						this.ProcessIds.Add(item);
					}
				}
			}
			this.IsMasterAndWorker = attributeHelper.GetBool("IsMasterAndWorker", false, false);
			this.MasterAndWorkerKillMode = attributeHelper.GetEnum<ProcessKillMode>("MasterAndWorkerKillMode", false, this.IsMasterAndWorker ? ProcessKillMode.SelfAndChildren : ProcessKillMode.SelfOnly);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002B608 File Offset: 0x00029808
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeServiceAttributes(attributeHelper);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0002B750 File Offset: 0x00029950
		protected void InternalKillProcess(RecoveryActionEntry startEntry, CancellationToken cancellationToken)
		{
			Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
			{
				if (this.ProcessIds.Count != 0)
				{
					using (HashSet<int>.Enumerator enumerator = this.ProcessIds.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int num = enumerator.Current;
							try
							{
								using (Process processById = Process.GetProcessById(num))
								{
									this.SafeKillProcess(processById, startEntry.InstanceId);
								}
							}
							catch (Exception ex)
							{
								ManagedAvailabilityCrimsonEvents.ActiveMonitoringUnexpectedError.Log<int, string>(num, ex.Message);
							}
						}
						return;
					}
				}
				Process[] processesByName = Process.GetProcessesByName(this.ProcessName);
				foreach (Process process in processesByName)
				{
					using (process)
					{
						this.SafeKillProcess(process, startEntry.InstanceId);
					}
				}
			});
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0002B788 File Offset: 0x00029988
		protected void SafeKillProcess(Process process, string instanceId)
		{
			try
			{
				if (process != null)
				{
					try
					{
						try
						{
							ProcessHelper.Kill(process, this.IsMasterAndWorker ? this.MasterAndWorkerKillMode : ProcessKillMode.SelfOnly, instanceId);
						}
						finally
						{
							if (process != null)
							{
								((IDisposable)process).Dispose();
							}
						}
					}
					catch (Win32Exception)
					{
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			finally
			{
				if (process != null)
				{
					((IDisposable)process).Dispose();
				}
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002B824 File Offset: 0x00029A24
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.KillProcess, this.ProcessName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				this.InternalKillProcess(startEntry, cancellationToken);
			});
		}

		// Token: 0x040004BE RID: 1214
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004BF RID: 1215
		private static readonly string TypeName = typeof(ProcessKillResponder).FullName;

		// Token: 0x020000E1 RID: 225
		internal static class AttributeNames
		{
			// Token: 0x040004C6 RID: 1222
			internal const string ProcessName = "ProcessName";

			// Token: 0x040004C7 RID: 1223
			internal const string IsMasterAndWorker = "IsMasterAndWorker";

			// Token: 0x040004C8 RID: 1224
			internal const string MasterAndWorkerKillMode = "MasterAndWorkerKillMode";

			// Token: 0x040004C9 RID: 1225
			internal const string throttleGroupName = "throttleGroupName";

			// Token: 0x040004CA RID: 1226
			internal const string SampleMask = "SampleMask";
		}

		// Token: 0x020000E2 RID: 226
		internal class DefaultValues
		{
			// Token: 0x040004CB RID: 1227
			internal const bool IsMasterWorker = false;

			// Token: 0x040004CC RID: 1228
			internal const int TimeoutInSeconds = 60;

			// Token: 0x040004CD RID: 1229
			internal const string ServiceName = "Exchange";

			// Token: 0x040004CE RID: 1230
			internal const bool Enabled = true;
		}
	}
}
