using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000FE RID: 254
	public class SetMonitorStateRepairingResponder : ResponderWorkItem
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, string monitorName, ServiceHealthStatus targetHealthState, bool enabled = true, int recurrenceIntervalSeconds = 300)
		{
			return new ResponderDefinition
			{
				AssemblyPath = SetMonitorStateRepairingResponder.AssemblyPath,
				TypeName = SetMonitorStateRepairingResponder.TypeName,
				Name = name,
				ServiceName = serviceName,
				AlertTypeId = alertTypeId,
				AlertMask = alertMask,
				TargetResource = targetResource,
				TargetPartition = monitorName,
				TargetHealthState = targetHealthState,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = (int)SetMonitorStateRepairingResponder.DefaultTimeout.TotalSeconds,
				MaxRetryAttempts = 3,
				Enabled = enabled
			};
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002ED70 File Offset: 0x0002CF70
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			string targetPartition = base.Definition.TargetPartition;
			string targetResource = base.Definition.TargetResource;
			if (string.IsNullOrWhiteSpace(targetPartition))
			{
				throw new ArgumentNullException("MonitorName");
			}
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Setting monitor {0} for target resource {1} into repairing", targetPartition, targetResource, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\SetMonitorStateRepairingResponder.cs", 97);
			RpcSetServerMonitor.Invoke(Environment.MachineName, targetPartition, targetResource, new bool?(true), 30000);
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Successfully set monitor {0} for target resource {1} into repairing", targetPartition, targetResource, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\SetMonitorStateRepairingResponder.cs", 109);
		}

		// Token: 0x0400054F RID: 1359
		private static TimeSpan DefaultTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000550 RID: 1360
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000551 RID: 1361
		private static readonly string TypeName = typeof(SetMonitorStateRepairingResponder).FullName;
	}
}
