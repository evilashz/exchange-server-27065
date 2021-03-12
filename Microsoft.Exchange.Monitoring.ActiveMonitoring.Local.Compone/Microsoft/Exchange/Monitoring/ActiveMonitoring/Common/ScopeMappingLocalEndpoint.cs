using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000562 RID: 1378
	internal class ScopeMappingLocalEndpoint : ScopeMappingEndpoint, IEndpoint
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000D14A1 File Offset: 0x000CF6A1
		public bool RestartOnChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000D14A4 File Offset: 0x000CF6A4
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x000D14AC File Offset: 0x000CF6AC
		public Exception Exception { get; set; }

		// Token: 0x0600229C RID: 8860 RVA: 0x000D14B5 File Offset: 0x000CF6B5
		public void Initialize()
		{
			if (LocalEndpointManager.IsDataCenter)
			{
				this.InitializeScopeAndSystemMonitoringMappings();
				this.InitializeDefaultScopes();
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000D14CC File Offset: 0x000CF6CC
		public bool DetectChange()
		{
			bool result;
			try
			{
				this.Initialize();
				result = true;
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceError<Exception>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.DetectChange] Detection failed: {0}", arg, null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 85);
				throw;
			}
			return result;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000D151C File Offset: 0x000CF71C
		internal override void InitializeScopeAndSystemMonitoringMappings()
		{
			try
			{
				ConcurrentDictionary<string, ScopeMapping> scopeMappings = new ConcurrentDictionary<string, ScopeMapping>(StringComparer.InvariantCultureIgnoreCase);
				ConcurrentDictionary<string, SystemMonitoringMapping> concurrentDictionary = new ConcurrentDictionary<string, SystemMonitoringMapping>(StringComparer.InvariantCultureIgnoreCase);
				string text = null;
				try
				{
					text = ExchangeSetupContext.BinPath;
				}
				catch (Exception arg)
				{
					WTFDiagnostics.TraceInformation<Exception>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] Failed to get Exchange Bin folder using ExchangeSetupContext.BinPath: {0}", arg, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 114);
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] Using current executing folder as the root.", null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 124);
					text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] fileFolder: {0}", text, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 132);
				string text2 = Path.Combine(text, "Monitoring\\Config\\ScopeMappings.xml");
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] fileName: {0}", text2, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 135);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text2);
				string localServiceEnvironmentName = this.GetLocalServiceEnvironmentName();
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] serviceName: {0}", localServiceEnvironmentName, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 142);
				XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("//Scope[@Name='{0}' and @Type='Service']", localServiceEnvironmentName));
				if (xmlNode == null)
				{
					WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] Unabled to find startingScope for Service '{0}'.", localServiceEnvironmentName, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 147);
				}
				else
				{
					base.InitializeScopeAndSystemMonitoringMappingsFromXml(xmlNode, scopeMappings, concurrentDictionary, null);
					lock (this.updateLocker)
					{
						base.ScopeMappings = scopeMappings;
						base.SystemMonitoringMappings = concurrentDictionary;
					}
				}
			}
			catch (Exception arg2)
			{
				WTFDiagnostics.TraceError<Exception>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeScopeAndSystemMonitoringMappings] Initialization failed: {0}", arg2, null, "InitializeScopeAndSystemMonitoringMappings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 166);
				throw;
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000D1738 File Offset: 0x000CF938
		internal override void InitializeDefaultScopes()
		{
			try
			{
				if (base.ScopeMappings == null || base.ScopeMappings.Count == 0)
				{
					WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeDefaultScopes] No ScopeMappings found for machine: {0}", Environment.MachineName, null, "InitializeDefaultScopes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 186);
				}
				else
				{
					ConcurrentDictionary<string, ScopeMapping> concurrentDictionary = new ConcurrentDictionary<string, ScopeMapping>(StringComparer.InvariantCultureIgnoreCase);
					ScopeMapping value = null;
					string localForestFqdn = TopologyProvider.LocalForestFqdn;
					if (!string.IsNullOrWhiteSpace(localForestFqdn) && base.ScopeMappings.TryGetValue(localForestFqdn, out value))
					{
						concurrentDictionary[localForestFqdn] = value;
					}
					try
					{
						IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
						if (localDAG != null && base.ScopeMappings.TryGetValue(localDAG.Name, out value))
						{
							concurrentDictionary[localDAG.Name] = value;
						}
					}
					catch (Exception arg)
					{
						WTFDiagnostics.TraceInformation<string, Exception>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeDefaultScopes] DAG discovery is n/a on '{0}': {1}", Environment.MachineName, arg, null, "InitializeDefaultScopes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 223);
					}
					lock (this.updateLocker)
					{
						base.DefaultScopes = concurrentDictionary;
					}
				}
			}
			catch (Exception arg2)
			{
				WTFDiagnostics.TraceError<Exception>(ExTraceGlobals.ScopeMappingLocalEndpointTracer, this.traceContext, "[ScopeMappingLocalEndpoint.InitializeDefaultScopes] Initialization failed: {0}", arg2, null, "InitializeDefaultScopes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ScopeMappingLocalEndpoint.cs", 238);
				throw;
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000D18C0 File Offset: 0x000CFAC0
		protected virtual string GetLocalServiceEnvironmentName()
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
				if (!string.IsNullOrWhiteSpace(ipproperties.DnsSuffix))
				{
					string text = ipproperties.DnsSuffix.ToLowerInvariant();
					string result;
					if (text.EndsWith("prod.outlook.com") || text.EndsWith("prod.exchangelabs.com"))
					{
						result = ScopeMappingEndpoint.ServiceEnvironment.Prod.ToString();
					}
					else if (text.EndsWith("sdf.exchangelabs.com") && !text.EndsWith("nampdt01.sdf.exchangelabs.com"))
					{
						result = ScopeMappingEndpoint.ServiceEnvironment.Sdf.ToString();
					}
					else if (text.EndsWith("nampdt01.sdf.exchangelabs.com"))
					{
						result = ScopeMappingEndpoint.ServiceEnvironment.Pdt.ToString();
					}
					else
					{
						if (!text.EndsWith("partner.outlook.cn"))
						{
							goto IL_C4;
						}
						result = ScopeMappingEndpoint.ServiceEnvironment.Gallatin.ToString();
					}
					return result;
				}
				IL_C4:;
			}
			return ScopeMappingEndpoint.ServiceEnvironment.Test.ToString();
		}

		// Token: 0x040018E5 RID: 6373
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x040018E6 RID: 6374
		private object updateLocker = new object();
	}
}
