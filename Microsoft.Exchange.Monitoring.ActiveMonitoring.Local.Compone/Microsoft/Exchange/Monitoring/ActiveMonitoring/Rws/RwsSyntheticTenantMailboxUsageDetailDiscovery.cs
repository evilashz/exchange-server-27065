using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws
{
	// Token: 0x02000454 RID: 1108
	internal sealed class RwsSyntheticTenantMailboxUsageDetailDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001C17 RID: 7191 RVA: 0x000A2CB0 File Offset: 0x000A0EB0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsSyntheticTenantMailboxUsageDetailDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 93);
			this.ProbeName = string.Format("RwsSyntheticTenantMailboxUsageDetail{0}Probe", base.Definition.Attributes["TargetGroup"]);
			this.MonitorName = string.Format("RwsSyntheticTenantMailboxUsageDetail{0}Monitor", base.Definition.Attributes["TargetGroup"]);
			this.ResponderName = string.Format("RwsSyntheticTenantMailboxUsageDetail{0}EscalateResponder", base.Definition.Attributes["TargetGroup"]);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			this.CreateProbeDefinitions();
			this.CreateMonitorDefinitions();
			this.CreateResponderDefinitions();
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000A2D6C File Offset: 0x000A0F6C
		private bool ShouldDoDeployment()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on non datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 121);
				return false;
			}
			if (Datacenter.IsGallatinDatacenter())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on Gallatin datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 131);
				return false;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Datamining\\Monitoring"))
			{
				if (registryKey != null && (int)registryKey.GetValue("ActiveMonitoringEnabled", 0) != 0)
				{
					if (ExEnvironment.IsTest)
					{
						return true;
					}
					if (ExEnvironment.IsSdfDomain && Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}SM01MS001$", RegexOptions.IgnoreCase))
					{
						return true;
					}
					if (Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}MG01MS101$", RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on the server which doesn't have AM enabled", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 163);
				}
			}
			return false;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000A2E8C File Offset: 0x000A108C
		private void CreateProbeDefinitions()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsSyntheticTenantMailboxUsageDetailDiscovery.CreateProbe: Creating probe {0}", this.ProbeName, null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 178);
			int recurrenceIntervalSeconds = 600;
			int timeoutSeconds = 510;
			int maxRetryAttempts = 3;
			string endpoint = string.Empty;
			if (ExEnvironment.IsTest)
			{
				endpoint = base.Definition.Attributes["TdsConnString"];
			}
			else if (ExEnvironment.IsSdfDomain)
			{
				endpoint = base.Definition.Attributes["SdfConnString"];
			}
			else
			{
				endpoint = base.Definition.Attributes["PrdConnString"];
			}
			ProbeDefinition probeDefinition = new ProbeDefinition
			{
				AssemblyPath = RwsSyntheticTenantMailboxUsageDetailDiscovery.assemblyPath,
				TypeName = RwsSyntheticTenantMailboxUsageDetailDiscovery.probeTypeName,
				Name = this.ProbeName,
				ServiceName = base.Definition.ServiceName,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = timeoutSeconds,
				MaxRetryAttempts = maxRetryAttempts,
				Enabled = true,
				WorkItemVersion = RwsSyntheticTenantMailboxUsageDetailDiscovery.assemblyVersion,
				ExecutionLocation = string.Empty,
				Endpoint = endpoint,
				Account = base.Definition.Attributes["AzureAccount"],
				AccountPassword = base.Definition.Attributes["AzureAccountSecurePassword"]
			};
			probeDefinition.TargetGroup = base.Definition.Attributes["TargetGroup"];
			probeDefinition.TargetResource = base.Definition.Attributes["TargetResource"];
			int num = 1;
			if (base.Definition.Attributes.ContainsKey("DuplicateCopyNumber") && !int.TryParse(base.Definition.Attributes["DuplicateCopyNumber"], out num))
			{
				num = 1;
			}
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsSyntheticTenantMailboxUsageDetailDiscovery.CreateProbe: Created probe {0}", this.ProbeName, null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 237);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000A309C File Offset: 0x000A129C
		private void CreateMonitorDefinitions()
		{
			string monitorName = this.MonitorName;
			string probeName = this.ProbeName;
			int failureCount = 1;
			int monitoringInterval = 600;
			int recurrenceIntervalSeconds = 0;
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, probeName, ExchangeComponent.Rws.Service, ExchangeComponent.Rws, failureCount, true, monitoringInterval);
			monitorDefinition.TargetResource = Environment.MachineName;
			monitorDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsSyntheticTenantMailboxUsageDetailDiscovery.CreateMonitor: Creating monitor {0} of type {1}", monitorName, RwsSyntheticTenantMailboxUsageDetailDiscovery.OverallConsecutiveProbeFailuresMonitorTypeName, null, "CreateMonitorDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 272);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000A3138 File Offset: 0x000A1338
		private void CreateResponderDefinitions()
		{
			string responderName = this.ResponderName;
			string alertTypeId = string.Format("Exchange/Rws/{0}/Responder", this.ProbeName);
			string escalationSubjectUnhealthy = string.Format("Synthetic Tenant data is not valid in CFR MailboxUsageDetail table. Detect Machine = {0}", Environment.MachineName);
			string escalationMessageUnhealthy = "Synthetic tenant data availability/completeness issue for MailboxUsageDetail table detected";
			ResponderDefinition definition = EscalateResponder.CreateDefinition(responderName, ExchangeComponent.Rws.Service, alertTypeId, this.MonitorName, Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.Rws.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("RwsSyntheticTenantMailboxUsageDetailDiscovery.CreateResponder: Created responder {0} of type {1}", responderName, RwsSyntheticTenantMailboxUsageDetailDiscovery.responderTypeName), null, "CreateResponderDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsSyntheticTenantMailboxUsageDetailDiscovery.cs", 310);
		}

		// Token: 0x0400133A RID: 4922
		private const int DefaultRecurrenceIntervalSeconds = 600;

		// Token: 0x0400133B RID: 4923
		private const int DefaultTimeoutSeconds = 300;

		// Token: 0x0400133C RID: 4924
		private const int DefaultMaxRetryAttempts = 3;

		// Token: 0x0400133D RID: 4925
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x0400133E RID: 4926
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400133F RID: 4927
		private static string probeTypeName = typeof(RwsSyntheticTenantMailboxUsageDetailProbe).FullName;

		// Token: 0x04001340 RID: 4928
		private static string responderTypeName = typeof(EscalateResponder).FullName;

		// Token: 0x04001341 RID: 4929
		private static readonly string OverallConsecutiveProbeFailuresMonitorTypeName = typeof(OverallConsecutiveProbeFailuresMonitor).FullName;

		// Token: 0x04001342 RID: 4930
		private string ProbeName;

		// Token: 0x04001343 RID: 4931
		private string MonitorName;

		// Token: 0x04001344 RID: 4932
		private string ResponderName;
	}
}
