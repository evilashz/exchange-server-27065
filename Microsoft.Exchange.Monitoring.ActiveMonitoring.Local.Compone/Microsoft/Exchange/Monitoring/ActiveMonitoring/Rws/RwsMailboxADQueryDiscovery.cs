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
	// Token: 0x02000442 RID: 1090
	internal sealed class RwsMailboxADQueryDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x0009DEA1 File Offset: 0x0009C0A1
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsMailboxADQueryDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 55);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			this.CreateProbeDefinitions();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0009DED4 File Offset: 0x0009C0D4
		private bool ShouldDoDeployment()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on non datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 78);
				return false;
			}
			if (Datacenter.IsGallatinDatacenter())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on Gallatin datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 88);
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
					if (ExEnvironment.IsSdfDomain)
					{
						return false;
					}
					if (Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}MG01MS[0-9]", RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on the server which doesn't have AM enabled", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 120);
				}
			}
			return false;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0009DFD4 File Offset: 0x0009C1D4
		private void CreateProbeDefinitions()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsMailboxADQueryDiscovery.CreateProbe: Creating probe {0}", "RwsMailboxADQueryProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 135);
			int recurrenceIntervalSeconds = 600;
			int timeoutSeconds = 570;
			int maxRetryAttempts = 1;
			string endpoint = string.Empty;
			if (ExEnvironment.IsTest)
			{
				endpoint = base.Definition.Attributes["TdsConnString"];
			}
			else
			{
				endpoint = base.Definition.Attributes["PrdConnString"];
			}
			ProbeDefinition definition = new ProbeDefinition
			{
				AssemblyPath = RwsMailboxADQueryDiscovery.assemblyPath,
				TypeName = RwsMailboxADQueryDiscovery.probeTypeName,
				Name = "RwsMailboxADQueryProbe",
				ServiceName = base.Definition.ServiceName,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = timeoutSeconds,
				MaxRetryAttempts = maxRetryAttempts,
				Enabled = true,
				WorkItemVersion = RwsMailboxADQueryDiscovery.assemblyVersion,
				ExecutionLocation = string.Empty,
				Endpoint = endpoint,
				Account = base.Definition.Attributes["AzureAccount"],
				AccountPassword = base.Definition.Attributes["AzureAccountSecurePassword"]
			};
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsMailboxADQueryDiscovery.CreateProbe: Created probe {0}", "RwsMailboxADQueryProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsMailboxADQueryDiscovery.cs", 178);
		}

		// Token: 0x040012DE RID: 4830
		private const string ProbeName = "RwsMailboxADQueryProbe";

		// Token: 0x040012DF RID: 4831
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x040012E0 RID: 4832
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040012E1 RID: 4833
		private static string probeTypeName = typeof(RwsMailboxADQueryProbe).FullName;
	}
}
