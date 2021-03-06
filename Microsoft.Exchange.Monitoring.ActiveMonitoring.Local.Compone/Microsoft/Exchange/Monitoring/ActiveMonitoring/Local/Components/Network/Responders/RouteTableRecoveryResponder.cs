using System;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Responders
{
	// Token: 0x02000236 RID: 566
	public class RouteTableRecoveryResponder : ResponderWorkItem
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x00069B9C File Offset: 0x00067D9C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, ServiceHealthStatus targetHealthState, bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition
			{
				AssemblyPath = RouteTableRecoveryResponder.AssemblyPath,
				TypeName = RouteTableRecoveryResponder.TypeName,
				Name = name,
				ServiceName = serviceName,
				AlertTypeId = alertTypeId,
				AlertMask = alertMask,
				TargetHealthState = targetHealthState,
				RecurrenceIntervalSeconds = 300,
				TimeoutSeconds = 300,
				MaxRetryAttempts = 3,
				Enabled = enabled
			};
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.AddRoute, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00069D38 File Offset: 0x00067F38
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.AddRoute, Environment.MachineName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				ManagementObjectCollection routes = this.GetRoutes("SELECT * FROM Win32_IP4PersistedRouteTable");
				if (routes.Count == 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, "RouteTableRecoveryResponder:: DoResponderWork(): No persistent routes could be found, automatic recovery cannot fix this, returning...", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\RouteTableRecoveryResponder.cs", 96);
					return;
				}
				WTFDiagnostics.TraceInformation<int>(ExTraceGlobals.NetworkTracer, base.TraceContext, "RouteTableRecoveryResponder:: DoResponderWork(): {0} persistent routes are found.", routes.Count, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\RouteTableRecoveryResponder.cs", 103);
				foreach (ManagementBaseObject managementBaseObject in routes)
				{
					string processStartInfoArgument = string.Format("add {0} mask {1} {2}", managementBaseObject["Destination"], managementBaseObject["Mask"], managementBaseObject["Nexthop"]);
					string message = string.Format("RouteTableRecoveryResponder:: DoResponderWork(): Persistent route {0} has been added successfully.", managementBaseObject["Destination"]);
					this.AddPersistentRouteToRouteTable(processStartInfoArgument, message);
				}
				WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, "RouteTableRecoveryResponder:: DoResponderWork(): Recovery action has been completed", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\RouteTableRecoveryResponder.cs", 121);
			});
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00069D68 File Offset: 0x00067F68
		private void AddPersistentRouteToRouteTable(string processStartInfoArgument, string message)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo("route", processStartInfoArgument)
			{
				UseShellExecute = false
			};
			using (Process process = new Process())
			{
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardError = true;
				process.StartInfo = processStartInfo;
				process.Start();
				process.WaitForExit();
				WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, (process.ExitCode == 0) ? message : string.Format("{0}\n{1}", process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd()), null, "AddPersistentRouteToRouteTable", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\RouteTableRecoveryResponder.cs", 150);
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00069E1C File Offset: 0x0006801C
		private ManagementObjectCollection GetRoutes(string query)
		{
			ManagementObjectCollection result;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", query))
			{
				result = managementObjectSearcher.Get();
			}
			return result;
		}

		// Token: 0x04000BE0 RID: 3040
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000BE1 RID: 3041
		private static readonly string TypeName = typeof(RouteTableRecoveryResponder).FullName;
	}
}
