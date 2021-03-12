using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
	// Token: 0x0200044D RID: 1101
	internal sealed class RwsDatabaseCapacityDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x000A1470 File Offset: 0x0009F670
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatabaseCapacityDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 70);
			if (!this.ShouldDoDeployment())
			{
				return;
			}
			this.CreateProbeDefinitions();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000A14A4 File Offset: 0x0009F6A4
		private bool ShouldDoDeployment()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on non datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 92);
				return false;
			}
			if (Datacenter.IsGallatinDatacenter())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on Gallatin datacenter environment", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 102);
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
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "Skip RWS monitoring on the server which doesn't have AM enabled", null, "ShouldDoDeployment", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 134);
				}
			}
			return false;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000A15C0 File Offset: 0x0009F7C0
		private void CreateProbeDefinitions()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatabaseCapacityDiscovery.CreateProbe: Creating probe {0}", "RwsDatabaseCapacityProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 149);
			int recurrenceIntervalSeconds;
			if (!int.TryParse(base.Definition.Attributes["ProbeRecurrenceIntervalSeconds"], out recurrenceIntervalSeconds))
			{
				recurrenceIntervalSeconds = 86400;
			}
			int timeoutSeconds;
			if (!int.TryParse(base.Definition.Attributes["ProbeTimeoutSeconds"], out timeoutSeconds))
			{
				timeoutSeconds = 300;
			}
			int maxRetryAttempts;
			if (!int.TryParse(base.Definition.Attributes["ProbeMaxRetryAttempts"], out maxRetryAttempts))
			{
				maxRetryAttempts = 3;
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder = this.PrepareTargetConnectionString();
			List<SqlConnectionStringBuilder> list = this.PrepareSourceConnectionStrings();
			foreach (SqlConnectionStringBuilder sqlConnectionStringBuilder2 in list)
			{
				ProbeDefinition definition = new ProbeDefinition
				{
					AssemblyPath = RwsDatabaseCapacityDiscovery.assemblyPath,
					TypeName = RwsDatabaseCapacityDiscovery.probeTypeName,
					Name = string.Format("{0}/{1}/{2}", "RwsDatabaseCapacityProbe", sqlConnectionStringBuilder2.DataSource, sqlConnectionStringBuilder2.InitialCatalog),
					ServiceName = base.Definition.ServiceName,
					RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
					TimeoutSeconds = timeoutSeconds,
					MaxRetryAttempts = maxRetryAttempts,
					Enabled = true,
					WorkItemVersion = RwsDatabaseCapacityDiscovery.assemblyVersion,
					ExecutionLocation = string.Empty,
					Endpoint = sqlConnectionStringBuilder2.ToString(),
					SecondaryEndpoint = sqlConnectionStringBuilder.ToString()
				};
				base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext).Wait();
			}
			base.Result.StateAttribute5 = string.Format("{0} ProbeDefinition created.", list.Count);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDatabaseCapacityDiscovery.CreateProbe: Created probe {0}", "RwsDatabaseCapacityProbe", null, "CreateProbeDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityDiscovery.cs", 203);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000A17C0 File Offset: 0x0009F9C0
		private SqlConnectionStringBuilder PrepareTargetConnectionString()
		{
			string key = string.Empty;
			if (ExEnvironment.IsTest)
			{
				key = "TestEnvTargetConnectionString";
			}
			else if (ExEnvironment.IsSdfDomain)
			{
				key = "SdfEnvTargetConnectionString";
			}
			else
			{
				key = "ProdEnvTargetConnectionString";
			}
			return new SqlConnectionStringBuilder(base.Definition.Attributes[key]);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000A1810 File Offset: 0x0009FA10
		private List<SqlConnectionStringBuilder> PrepareSourceConnectionStrings()
		{
			string[] array;
			if (ExEnvironment.IsTest)
			{
				array = base.Definition.Attributes["TestEnvSourceDatabases"].Split(new char[]
				{
					';'
				});
			}
			else if (ExEnvironment.IsSdfDomain)
			{
				array = base.Definition.Attributes["SdfEnvSourceDatabases"].Split(new char[]
				{
					';'
				});
			}
			else
			{
				array = base.Definition.Attributes["ProdEnvSourceDatabases"].Split(new char[]
				{
					';'
				});
			}
			List<SqlConnectionStringBuilder> list = new List<SqlConnectionStringBuilder>();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array3 = text.Split(new char[]
					{
						','
					});
					if (array3.Length != 2)
					{
						throw new ArgumentException(string.Format("Server/Database pair [{0}] is invalid.", text), "SourceList");
					}
					list.Add(new SqlConnectionStringBuilder
					{
						DataSource = (array3[0].Contains("{DC}") ? array3[0].Replace("{DC}", Environment.MachineName.Trim().Substring(0, 3)) : array3[0]),
						InitialCatalog = array3[1],
						IntegratedSecurity = true
					});
				}
			}
			return list;
		}

		// Token: 0x0400131C RID: 4892
		private const string ProbeName = "RwsDatabaseCapacityProbe";

		// Token: 0x0400131D RID: 4893
		private const int DefaultRecurrenceIntervalSeconds = 86400;

		// Token: 0x0400131E RID: 4894
		private const int DefaultTimeoutSeconds = 300;

		// Token: 0x0400131F RID: 4895
		private const int DefaultMaxRetryAttempts = 3;

		// Token: 0x04001320 RID: 4896
		private static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04001321 RID: 4897
		private static string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001322 RID: 4898
		private static string probeTypeName = typeof(RwsDatabaseCapacityProbe).FullName;
	}
}
