using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x0200019C RID: 412
	internal sealed class AllRolesMonitoringContext : MonitoringContextBase
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004A52A File Offset: 0x0004872A
		public AllRolesMonitoringContext(IMaintenanceWorkBroker broker, TracingContext traceContext) : base(broker, traceContext)
		{
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0004A54C File Offset: 0x0004874C
		public override void CreateContext()
		{
			base.InvokeCatchAndLog(delegate
			{
				this.CreateOnlineResponderContext();
			});
			base.InvokeCatchAndLog(delegate
			{
				this.CreateServerWideOfflineMonitorContext();
			});
			if (LocalEndpointManager.IsDataCenter)
			{
				base.InvokeCatchAndLog(delegate
				{
					this.CreateSystemDriveSpaceMonitor();
				});
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0004A5A0 File Offset: 0x000487A0
		private void CreateOnlineResponderContext()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "Adding OnlineResponder definition.", null, "CreateOnlineResponderContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\MonitoringContext\\AllRolesMonitoringContext.cs", 63);
			ResponderDefinition workDefinition = OnlineResponder.CreateDefinition();
			base.EnrollWorkItem<ResponderDefinition>(workDefinition);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.HighAvailabilityTracer, base.TraceContext, "Added OnlineResponder", null, "CreateOnlineResponderContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\MonitoringContext\\AllRolesMonitoringContext.cs", 68);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0004A600 File Offset: 0x00048800
		private void CreateServerWideOfflineMonitorContext()
		{
			double num = (double)(HighAvailabilityConstants.ServerInMaintenanceModeTurnaroundTime / 3600);
			ProbeDefinition probeDefinition = ServerWideOfflineProbe.CreateDefinition("ServerWideOfflineProbe", HighAvailabilityConstants.ServiceName, num, 600);
			base.EnrollWorkItem<ProbeDefinition>(probeDefinition);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("ServerWideOfflineMonitor", probeDefinition.ConstructWorkItemResultName(), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 2, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = 600;
			monitorDefinition.MonitoringIntervalSeconds = 1800;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by any issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition("ServerWideOfflineEscalate", HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), probeDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "High Availability", Strings.ServerInMaintenanceModeForTooLongEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, string.Format("{0:0.##}", num)), Strings.ServerInMaintenanceModeForTooLongEscalationMessage(HighAvailabilityConstants.ServiceName, Environment.MachineName), true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0004A72C File Offset: 0x0004892C
		private void CreateSystemDriveSpaceMonitor()
		{
			string[] array = new string[]
			{
				"C:",
				"D:"
			};
			foreach (string text in array)
			{
				if (new DriveInfo(text).DriveType != DriveType.NoRootDirectory)
				{
					string perfCounterName = string.Format("LogicalDisk\\% Free Space\\{0}", text);
					MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueBelowThresholdMonitor.CreateDefinition(string.Format("SystemDriveSpaceMonitor_{0}", text.Replace(":", string.Empty)), PerformanceCounterNotificationItem.GenerateResultName(perfCounterName), ExchangeComponent.Cafe.Name, ExchangeComponent.Cafe, 5.0, 1, true);
					monitorDefinition.RecurrenceIntervalSeconds = 300;
					monitorDefinition.MonitoringIntervalSeconds = 600;
					monitorDefinition.TargetResource = text;
					monitorDefinition.ServicePriority = 0;
					monitorDefinition.ScenarioDescription = "Validate HA health is not impacted any system drive disk issues";
					base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
					{
						new MonitorStateResponderTuple
						{
							MonitorState = new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
							Responder = ReleaseDiskSpaceResponder.CreateDefinition(string.Format("SystemDriveSpaceRelease_{0}", text.Replace(":", string.Empty)), ExchangeComponent.Cafe.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Degraded, text)
						},
						new MonitorStateResponderTuple
						{
							MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 600),
							Responder = EscalateResponder.CreateDefinition(string.Format("SystemDriveSpaceEscalate_{0}", text.Replace(":", string.Empty)), ExchangeComponent.Cafe.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "High Availability", Strings.SystemDriveSpaceEscalationSubject(ExchangeComponent.Cafe.Name, Environment.MachineName, text, monitorDefinition.MonitoringThreshold.ToString()), Strings.SystemDriveSpaceEscalationMessage(Environment.MachineName, text, monitorDefinition.MonitoringThreshold.ToString()), true, NotificationServiceClass.Urgent, 86400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
						}
					});
				}
			}
		}
	}
}
