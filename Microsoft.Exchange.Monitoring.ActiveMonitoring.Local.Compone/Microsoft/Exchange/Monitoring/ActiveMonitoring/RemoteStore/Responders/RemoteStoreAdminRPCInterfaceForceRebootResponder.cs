using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RemoteStore.Responders
{
	// Token: 0x02000423 RID: 1059
	public class RemoteStoreAdminRPCInterfaceForceRebootResponder : RemoteServerRestartResponder
	{
		// Token: 0x06001AF0 RID: 6896 RVA: 0x00095274 File Offset: 0x00093474
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, string alertMask, TimeSpan recurrenceInterval, ServiceHealthStatus responderTargetState, string componentName, string serviceName = "Exchange", int maxNodesToReboot = 1, bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = monitorName;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.Attributes["MinimumRequiredServers"] = -1.ToString();
			responderDefinition.Attributes["MaxNodeToReboot"] = maxNodesToReboot.ToString();
			responderDefinition.AssemblyPath = RemoteStoreAdminRPCInterfaceForceRebootResponder.AssemblyPath;
			responderDefinition.TypeName = RemoteStoreAdminRPCInterfaceForceRebootResponder.TypeName;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.RemoteForceReboot, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00095348 File Offset: 0x00093548
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.Result.RecoveryResult = ServiceRecoveryResult.Failed;
			MonitorResult lastFailedMonitorResult = WorkItemResultHelper.GetLastFailedMonitorResult(this, base.Broker, cancellationToken);
			if (lastFailedMonitorResult == null || !lastFailedMonitorResult.IsAlert || string.IsNullOrWhiteSpace(lastFailedMonitorResult.StateAttribute1))
			{
				WTFDiagnostics.TraceWarning(ExTraceGlobals.RemoteStoreTracer, base.TraceContext, "RemoteStoreAdminRPCInterfaceForceRebootResponder.DoResponderWork: Unable to get last monitor result or the result does not have list of servers to reboot, skipping responder execution", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RemoteStore\\RemoteStoreAdminRPCInterfaceForceRebootResponder.cs", 148);
				base.Result.StateAttribute2 = "SkippingResponderExecutionNotSufficientInformation";
				return;
			}
			List<string> list = this.PopulateServerListBasedOnExceptionType(lastFailedMonitorResult.StateAttribute1, base.Definition.Attributes);
			if (list.Count > 0)
			{
				string text = string.Join(", ", list);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RemoteStoreTracer, base.TraceContext, "RemoteStoreAdminRPCInterfaceForceRebootResponder.DoResponderWork: Invoking remote reboot responder to take reboot action on servers: {0}", text, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RemoteStore\\RemoteStoreAdminRPCInterfaceForceRebootResponder.cs", 125);
				base.Result.StateAttribute2 = "InvokingBaseResponderToTakeRecoveryAction";
				base.Result.StateAttribute3 = lastFailedMonitorResult.StateAttribute1;
				base.Result.StateAttribute4 = text;
				base.Definition.Attributes["RemoteTargetServers"] = text;
				base.DoResponderWork(cancellationToken);
				return;
			}
			base.Result.StateAttribute2 = "NoServerSelectedBasedOnExceptionType";
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00095474 File Offset: 0x00093674
		internal List<string> PopulateServerListBasedOnExceptionType(string data, Dictionary<string, string> extensionAttributes)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrWhiteSpace(data))
			{
				string[] array = data.TrimEnd(new char[]
				{
					']'
				}).Split(new char[]
				{
					']'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Trim(new char[]
					{
						'['
					}).Split(new char[]
					{
						','
					});
					if (array3.Length > 1 && !string.IsNullOrWhiteSpace(array3[0]))
					{
						string text2 = array3[0].Trim();
						for (int j = 1; j < array3.Length; j++)
						{
							string text3 = array3[j].Trim();
							string value;
							bool flag;
							if (!string.IsNullOrWhiteSpace(text3) && extensionAttributes.TryGetValue(text3, out value) && !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out flag) && flag && !list.Contains(text2, StringComparer.InvariantCultureIgnoreCase))
							{
								list.Add(text2);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0400127D RID: 4733
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400127E RID: 4734
		private static readonly string TypeName = typeof(RemoteStoreAdminRPCInterfaceForceRebootResponder).FullName;
	}
}
