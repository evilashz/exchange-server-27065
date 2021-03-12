﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws
{
	// Token: 0x02000449 RID: 1097
	internal sealed class RwsPumperRunHistoryDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x000A04A8 File Offset: 0x0009E6A8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsPumperRunHistoryDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsPumperRunHistoryDiscovery.cs", 55);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			foreach (string targetDataMart in new List<string>
			{
				"CDM-TENANTDS-PUMPER.exmgmt.local",
				"CDM-TENANTDS-PUMPER01.exmgmt.local",
				"CDM-TENANTDS-PUMPER02.exmgmt.local",
				"CDM-TENANTDS-PUMPER03.exmgmt.local",
				"CDM-TENANTDS-PUMPER04.exmgmt.local",
				"CDM-TENANTDS-PUMPER05.exmgmt.local",
				"CDM-TENANTDS-PUMPER06.exmgmt.local",
				"CDM-TENANTDS-PUMPER07.exmgmt.local",
				"CDM-TENANTDS-PUMPER08.exmgmt.local",
				"CDM-TENANTDS-PUMPER09.exmgmt.local",
				"CDM-TENANTDS-PUMPER10.exmgmt.local",
				"CDM-TENANTDS-PUMPER11.exmgmt.local",
				"CDM-TENANTDS-PUMPER12.exmgmt.local"
			})
			{
				this.CreateProbeDefinitions(targetDataMart);
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000A05B8 File Offset: 0x0009E7B8
		private bool ShouldDoDeployment()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on non datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsPumperRunHistoryDiscovery.cs", 98);
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
					if (Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}MG(01|T03)MS\\d{3}$", RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on the server which doesn't have AM enabled", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsPumperRunHistoryDiscovery.cs", 131);
				}
			}
			return false;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000A06A8 File Offset: 0x0009E8A8
		private void CreateProbeDefinitions(string TargetDataMart)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsPumperRunHistoryDiscovery.CreateProbe: Creating probe {0}", "RwsPumperRunHistoryProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsPumperRunHistoryDiscovery.cs", 147);
			int recurrenceIntervalSeconds = 7200;
			int timeoutSeconds = 600;
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
			else if (Regex.IsMatch(Environment.MachineName.Trim(), "^[a-zA-Z0-9]{3}MGT03MS\\d{3}$", RegexOptions.IgnoreCase))
			{
				endpoint = base.Definition.Attributes["GalConnString"];
			}
			else
			{
				endpoint = base.Definition.Attributes["PrdConnString"];
			}
			ProbeDefinition definition = new ProbeDefinition
			{
				AssemblyPath = RwsPumperRunHistoryDiscovery.assemblyPath,
				TypeName = RwsPumperRunHistoryDiscovery.probeTypeName,
				Name = string.Format("{0}/{1}", "RwsPumperRunHistoryProbe", TargetDataMart),
				ServiceName = base.Definition.ServiceName,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = timeoutSeconds,
				MaxRetryAttempts = maxRetryAttempts,
				Enabled = true,
				WorkItemVersion = RwsPumperRunHistoryDiscovery.assemblyVersion,
				ExecutionLocation = string.Empty,
				Endpoint = endpoint,
				Account = base.Definition.Attributes["AzureAccount"],
				AccountPassword = base.Definition.Attributes["AzureAccountSecurePassword"],
				SecondaryEndpoint = TargetDataMart
			};
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsPumperRunHistoryDiscovery.CreateProbe: Created probe {0}", "RwsPumperRunHistoryProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsPumperRunHistoryDiscovery.cs", 199);
		}

		// Token: 0x04001302 RID: 4866
		private const string ProbeName = "RwsPumperRunHistoryProbe";

		// Token: 0x04001303 RID: 4867
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04001304 RID: 4868
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001305 RID: 4869
		private static string probeTypeName = typeof(RwsPumperRunHistoryProbe).FullName;
	}
}
