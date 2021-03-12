using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws
{
	// Token: 0x0200045A RID: 1114
	public sealed class RwsDatamartAvailabilityDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001C2D RID: 7213 RVA: 0x000A3EF8 File Offset: 0x000A20F8
		private string[] GetTargetServers()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery:GetTargetServers from RwsDatamartAvailability.xml", null, "GetTargetServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 122);
			if (base.Definition.Attributes.ContainsKey("TargetServers"))
			{
				List<string> source = new List<string>(base.Definition.Attributes["TargetServers"].Replace(Environment.NewLine, "").Replace(" ", "").Split(new char[]
				{
					','
				}));
				return source.Distinct<string>().ToArray<string>();
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery: Failed to getTargetServers from RwsDatamartAvailability.xml", null, "GetTargetServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 131);
			return new string[0];
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000A3FC0 File Offset: 0x000A21C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 142);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			string[] targetServers = this.GetTargetServers();
			foreach (string text in targetServers)
			{
				string str = "." + text.Replace("-", "_");
				ProbeDefinition definition = this.CreateProbe(RwsDatamartAvailabilityDiscovery.ProbeName + str, text, RwsDatamartAvailabilityDiscovery.RwsDatamartAvailabilityProbeTypeName, RwsDatamartAvailabilityDiscovery.ProbeRecurrenceIntervalSeconds, RwsDatamartAvailabilityDiscovery.ProbeTimeoutSeconds, RwsDatamartAvailabilityDiscovery.ProbeRetryTimes);
				base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
				MonitorDefinition monitorDefinition = this.CreateMonitor(RwsDatamartAvailabilityDiscovery.MonitorName + str, RwsDatamartAvailabilityDiscovery.ProbeName + str);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				ResponderDefinition definition2 = this.CreateResponder(RwsDatamartAvailabilityDiscovery.ResponderName + str, RwsDatamartAvailabilityDiscovery.AssemblyPath, RwsDatamartAvailabilityDiscovery.EscalateResponderTypeName, RwsDatamartAvailabilityDiscovery.AlertTypeId, monitorDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition2, base.TraceContext);
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000A40E8 File Offset: 0x000A22E8
		private ProbeDefinition CreateProbe(string probeName, string endPoint, string probeTypeName, int recurrenceIntervalSeconds, int timeoutSeconds, int maxRetryAttempts)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery.CreateProbe: Creating probe {0}", probeName, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 200);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = RwsDatamartAvailabilityDiscovery.AssemblyPath;
			probeDefinition.TypeName = RwsDatamartAvailabilityDiscovery.RwsDatamartAvailabilityProbeTypeName;
			probeDefinition.Name = probeName;
			probeDefinition.Endpoint = endPoint;
			probeDefinition.ServiceName = ExchangeComponent.Rws.Name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = timeoutSeconds;
			probeDefinition.MaxRetryAttempts = maxRetryAttempts;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery.CreateProbe: Created probe {0}", probeName, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 216);
			return probeDefinition;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000A4194 File Offset: 0x000A2394
		private MonitorDefinition CreateMonitor(string monitorName, string sampleMask)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, sampleMask, ExchangeComponent.Rws.Name, ExchangeComponent.Rws, RwsDatamartAvailabilityDiscovery.FailureCountThreshold, true, RwsDatamartAvailabilityDiscovery.MonitorIntervalSeconds);
			monitorDefinition.TargetResource = Environment.MachineName;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatamartAvailabilityDiscovery.CreateMonitor: Creating monitor {0} of type {1}", monitorName, RwsDatamartAvailabilityDiscovery.OverallConsecutiveProbeFailuresMonitorTypeName, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 246);
			return monitorDefinition;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000A41FC File Offset: 0x000A23FC
		private ResponderDefinition CreateResponder(string responderName, string assemblyPath, string responderTypeName, string alertTypeId, string alertMask)
		{
			ResponderDefinition result = OBDEscalateResponder.CreateDefinition(responderName, ExchangeComponent.Rws.Service, alertTypeId, alertMask, Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.Rws.EscalationTeam, Strings.RwsDatamartAvailabilityEscalationSubject(Environment.MachineName, responderName.Substring(40)), Strings.RwsDatamartAvailabilityEscalationBody(Environment.MachineName, responderName.Substring(40)), true, NotificationServiceClass.UrgentInTraining, RwsDatamartAvailabilityDiscovery.MinimumSecondsBetweenEscalates, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("RwsDatamartAvailabilityDiscovery.CreateResponder: Created responder {0} of type {1}", responderName, responderTypeName), null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 286);
			return result;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000A4298 File Offset: 0x000A2498
		private bool ShouldDoDeployment()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("RwsDatamartAvailabilityProbe will be only on deployed on HKXMG01MS103 AND BY2MG01MS103.", new object[0]), null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityDiscovery.cs", 301);
			return string.Equals(Environment.MachineName, "HKXMG01MS103", StringComparison.InvariantCultureIgnoreCase) || string.Equals(Environment.MachineName, "BY2MG01MS103", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x04001362 RID: 4962
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04001363 RID: 4963
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001364 RID: 4964
		internal static readonly string ProbeName = "RwsDatamartAvailabilityProbe";

		// Token: 0x04001365 RID: 4965
		internal static readonly string MonitorName = "RwsDatamartAvailabilityMonitor";

		// Token: 0x04001366 RID: 4966
		internal static readonly string ResponderName = "RwsDatamartAvailabilityEscalateResponder";

		// Token: 0x04001367 RID: 4967
		internal static readonly string AlertTypeId = "Exchange/Rws/RwsDatamartAvailabilityProbe/Responder";

		// Token: 0x04001368 RID: 4968
		internal static readonly int FailureCountThreshold = 4;

		// Token: 0x04001369 RID: 4969
		internal static readonly int ProbeRecurrenceIntervalSeconds = 300;

		// Token: 0x0400136A RID: 4970
		internal static readonly int MonitorIntervalSeconds = 1400;

		// Token: 0x0400136B RID: 4971
		internal static readonly int ProbeTimeoutSeconds = 180;

		// Token: 0x0400136C RID: 4972
		internal static readonly int ProbeRetryTimes = 0;

		// Token: 0x0400136D RID: 4973
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400136E RID: 4974
		private static readonly string RwsDatamartAvailabilityProbeTypeName = typeof(RwsDatamartAvailabilityProbe).FullName;

		// Token: 0x0400136F RID: 4975
		private static readonly string OverallConsecutiveProbeFailuresMonitorTypeName = typeof(OverallConsecutiveProbeFailuresMonitor).FullName;

		// Token: 0x04001370 RID: 4976
		private static readonly string EscalateResponderTypeName = typeof(OBDEscalateResponder).FullName;

		// Token: 0x04001371 RID: 4977
		private static readonly int MinimumSecondsBetweenEscalates = 43200;
	}
}
