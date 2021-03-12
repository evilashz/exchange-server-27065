using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000DE RID: 222
	public class ProcessorAffinityResponder : ResponderWorkItem
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0002AF24 File Offset: 0x00029124
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, int processorAffinityCount, int avoidProcessorCount = 0)
		{
			if (targetHealthState == ServiceHealthStatus.None)
			{
				throw new ArgumentException("The responder does not support ServiceHealthStatus.None as target health state.", "targetHealthState");
			}
			if (string.IsNullOrWhiteSpace(targetResource))
			{
				throw new ArgumentException("Invalid target resource.", "targetResource");
			}
			if (processorAffinityCount < 1)
			{
				throw new ArgumentException("processorAffinityCount must be greater than 0.", "processorAffinityCount");
			}
			if (avoidProcessorCount < 0)
			{
				throw new ArgumentException("avoidProcessorCount must be greater than or equal to 0.", "avoidProcessorCount");
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ProcessorAffinityResponder.AssemblyPath;
			responderDefinition.TypeName = ProcessorAffinityResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.Attributes["ProcessorAffinityCount"] = processorAffinityCount.ToString();
			responderDefinition.Attributes["AvoidProcessorCount"] = avoidProcessorCount.ToString();
			responderDefinition.AlertTypeId = "*";
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.WaitIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			return responderDefinition;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0002B02C File Offset: 0x0002922C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			if (string.IsNullOrWhiteSpace(targetResource))
			{
				throw new InvalidOperationException("Target resource cannot be empty.");
			}
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			int @int = attributeHelper.GetInt("ProcessorAffinityCount", true, 0, new int?(1), null);
			int int2 = attributeHelper.GetInt("AvoidProcessorCount", false, 0, new int?(0), null);
			int processorCount = Environment.ProcessorCount;
			if (@int >= processorCount)
			{
				throw new InvalidOperationException(string.Format("ProcessorAffinityResponder cannot set processor affinity count to {0} on server with {1} processors.", @int, processorCount));
			}
			Process[] processesFromName = this.GetProcessesFromName(targetResource);
			if (processesFromName == null || processesFromName.Length == 0)
			{
				throw new InvalidOperationException(string.Format("ProcessorAffinityResponder is not able to get process with name '{0}'.", targetResource));
			}
			foreach (Process process in processesFromName)
			{
				using (process)
				{
					this.SetProcessorAffinity(process, @int, processorCount, int2);
				}
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002B13C File Offset: 0x0002933C
		private Process[] GetProcessesFromName(string processName)
		{
			if (processName.StartsWith("noderunner#", StringComparison.OrdinalIgnoreCase))
			{
				return this.GetProcessesForNodeRunner(processName);
			}
			Process[] processesByName = Process.GetProcessesByName(processName);
			if (processesByName != null && processesByName.Length > 0)
			{
				return processesByName;
			}
			if (processName.EndsWith("apppool", StringComparison.OrdinalIgnoreCase))
			{
				return this.GetProcessesForAppPool(processName);
			}
			return null;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0002B188 File Offset: 0x00029388
		private Process[] GetProcessesForNodeRunner(string nodeRunnerInstanceName)
		{
			Dictionary<string, int> nodeProcessIds = SearchMonitoringHelper.GetNodeProcessIds();
			foreach (string text in nodeProcessIds.Keys)
			{
				if (nodeRunnerInstanceName.EndsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					return new Process[]
					{
						Process.GetProcessById(nodeProcessIds[text])
					};
				}
			}
			return null;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002B204 File Offset: 0x00029404
		private Process[] GetProcessesForAppPool(string appPoolName)
		{
			Process[] result;
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPoolCollection applicationPools = serverManager.ApplicationPools;
				foreach (ApplicationPool applicationPool in applicationPools)
				{
					if (appPoolName.Equals(applicationPool.Name, StringComparison.OrdinalIgnoreCase))
					{
						WorkerProcessCollection workerProcesses = applicationPool.WorkerProcesses;
						List<Process> list = new List<Process>();
						foreach (WorkerProcess workerProcess in workerProcesses)
						{
							if (workerProcess.State == 1)
							{
								Process processById = Process.GetProcessById(workerProcess.ProcessId);
								if (processById != null)
								{
									list.Add(processById);
								}
							}
						}
						return list.ToArray();
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002B2FC File Offset: 0x000294FC
		private void SetProcessorAffinity(Process process, int affinityCount, int totalProcessorCount, int avoidProcessorCount)
		{
			ulong num = 0UL;
			Random random = new Random();
			int num2 = 0;
			if (avoidProcessorCount > totalProcessorCount - affinityCount)
			{
				avoidProcessorCount = totalProcessorCount - affinityCount;
			}
			for (int i = 0; i < totalProcessorCount; i++)
			{
				if (i >= avoidProcessorCount)
				{
					if (random.Next(totalProcessorCount - i) < affinityCount - num2)
					{
						num |= 1UL << i;
						num2++;
					}
					if (num2 == affinityCount)
					{
						break;
					}
				}
			}
			base.Result.StateAttribute1 = num.ToString();
			process.ProcessorAffinity = (IntPtr)((long)num);
		}

		// Token: 0x040004BA RID: 1210
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004BB RID: 1211
		private static readonly string TypeName = typeof(ProcessorAffinityResponder).FullName;

		// Token: 0x020000DF RID: 223
		internal static class AttributeNames
		{
			// Token: 0x040004BC RID: 1212
			internal const string ProcessorAffinityCount = "ProcessorAffinityCount";

			// Token: 0x040004BD RID: 1213
			internal const string AvoidProcessorCount = "AvoidProcessorCount";
		}
	}
}
