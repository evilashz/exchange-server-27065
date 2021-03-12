using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws
{
	// Token: 0x02000447 RID: 1095
	internal sealed class RwsCNAMEMappingDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001BEB RID: 7147 RVA: 0x0009FC4E File Offset: 0x0009DE4E
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsCNAMEMappingDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 70);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			this.CreateProbeDefinitions();
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0009FC84 File Offset: 0x0009DE84
		private bool ShouldDoDeployment()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on non datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 91);
				return false;
			}
			if (Datacenter.IsGallatinDatacenter())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on Gallatin datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 101);
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
					if (Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}MG01MS104$", RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on the server which doesn't have AM enabled", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 141);
				}
			}
			return false;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0009FDBC File Offset: 0x0009DFBC
		private void CreateProbeDefinitions()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsCNAMEMappingDiscovery.CreateProbe: Creating probe {0}", "RwsCNAMEMappingProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 156);
			int recurrenceIntervalSeconds = 43200;
			int timeoutSeconds = 450;
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
				AssemblyPath = RwsCNAMEMappingDiscovery.assemblyPath,
				TypeName = RwsCNAMEMappingDiscovery.probeTypeName,
				Name = "RwsCNAMEMappingProbe",
				ServiceName = base.Definition.ServiceName,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = timeoutSeconds,
				MaxRetryAttempts = maxRetryAttempts,
				Enabled = true,
				WorkItemVersion = RwsCNAMEMappingDiscovery.assemblyVersion,
				ExecutionLocation = string.Empty,
				Endpoint = endpoint,
				Account = base.Definition.Attributes["AzureAccount"],
				AccountPassword = base.Definition.Attributes["AzureAccountSecurePassword"]
			};
			probeDefinition.Attributes["TargetDCList"] = base.Definition.Attributes["TargetDCList"];
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsCNAMEMappingDiscovery.CreateProbe: Created probe {0}", "RwsCNAMEMappingProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsCNAMEMappingDiscovery.cs", 204);
		}

		// Token: 0x040012FB RID: 4859
		private const string ProbeName = "RwsCNAMEMappingProbe";

		// Token: 0x040012FC RID: 4860
		private const int DefaultRecurrenceIntervalSeconds = 600;

		// Token: 0x040012FD RID: 4861
		private const int DefaultTimeoutSeconds = 300;

		// Token: 0x040012FE RID: 4862
		private const int DefaultMaxRetryAttempts = 3;

		// Token: 0x040012FF RID: 4863
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04001300 RID: 4864
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001301 RID: 4865
		private static string probeTypeName = typeof(RwsCNAMEMappingProbe).FullName;
	}
}
