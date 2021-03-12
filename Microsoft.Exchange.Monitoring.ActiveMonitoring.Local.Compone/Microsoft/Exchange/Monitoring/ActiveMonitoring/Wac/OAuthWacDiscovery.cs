using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Wac
{
	// Token: 0x0200051A RID: 1306
	public sealed class OAuthWacDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x000C4E6C File Offset: 0x000C306C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					this.serverToRunWacProbe = true;
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.DoWork: Mailbox role is present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 132);
				}
				if (!this.serverToRunWacProbe)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.DoWork: Mailbox role is not present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 140);
				}
				else
				{
					List<string> oauthMonitoringUsers = this.GetOAuthMonitoringUsers(instance);
					if (oauthMonitoringUsers.Count == 0)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.DoWork: Unable to locate valid monitoring user", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 152);
					}
					else
					{
						this.CreateProbeChain(oauthMonitoringUsers);
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, string.Format("OAuthWacDiscovery:: DoWork(): ExchangeServerRoleEndpoint object threw exception.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 168);
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000C4F6C File Offset: 0x000C316C
		private List<string> GetOAuthMonitoringUsers(LocalEndpointManager endpointManager)
		{
			List<string> list = new List<string>();
			if (endpointManager.MailboxDatabaseEndpoint == null || endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.GetOAuthMonitoringUsers: mailbox database collection is empty on this server", null, "GetOAuthMonitoringUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 186);
				return list;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.GetOAuthMonitoringUsers: Ignore mailbox database {0} because it does not have monitoring mailbox", mailboxDatabaseInfo.MailboxDatabaseName, null, "GetOAuthMonitoringUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 197);
				}
				else
				{
					list.Add(mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain);
				}
			}
			return list;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000C5058 File Offset: 0x000C3258
		private void CreateProbeChain(List<string> monitoringUsers)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.CreateProbeChains: Creating OAuth Partner probe chains", null, "CreateProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 218);
			if (LocalEndpointManager.IsDataCenter && this.serverToRunWacProbe)
			{
				string text = string.Empty;
				string secondaryEndpoint = string.Empty;
				string environment = OAuthWacDiscovery.GetEnvironment();
				if (environment == "SDF")
				{
					text = "https://word-view.officeapps-df.live.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";
					secondaryEndpoint = "https://sdfpilot.outlook.com:443/owa/{0}/wopi/files/@/owaatt";
				}
				else if (environment == "PROD")
				{
					text = "https://word-view.officeapps.live.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";
					secondaryEndpoint = "https://outlook.office365.com:443/owa/{0}/wopi/files/@/owaatt";
				}
				else if (environment == "PARTNER")
				{
					string gallatinDataCenterPrefix = OAuthWacDiscovery.GetGallatinDataCenterPrefix();
					if (string.Equals(gallatinDataCenterPrefix, "BJ"))
					{
						text = "https://bjb-partner.officewebapps.cn/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";
						secondaryEndpoint = "https://partner.outlook.cn:443/owa/{0}/wopi/files/@/owaatt";
					}
					else if (string.Equals(gallatinDataCenterPrefix, "SH"))
					{
						text = "https://sha-partner.officewebapps.cn/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";
						secondaryEndpoint = "https://partner.outlook.cn:443/owa/{0}/wopi/files/@/owaatt";
					}
				}
				else
				{
					text = "https://blue-wac.corp.microsoft.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";
					secondaryEndpoint = "https://exchangelabs.live-int.com/owa/{0}/wopi/files/@/owaatt";
				}
				if (!string.IsNullOrEmpty(text))
				{
					string monitoringUser = monitoringUsers[0];
					this.CreateOneProbeChain(OAuthWacDiscovery.probeStuff, monitoringUser, text, secondaryEndpoint, new Func<string, string, string, string, ProbeDefinition>(OAuthWacProbe.CreateDefinition));
				}
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.CreateProbeChain: Created OAuth WAC probe chain", null, "CreateProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 268);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000C5190 File Offset: 0x000C3390
		private static string GetEnvironment()
		{
			string hostName = Dns.GetHostName();
			string[] array = Dns.GetHostEntry(hostName).HostName.Split(new char[]
			{
				'.'
			});
			return array[array.Length - 3].ToUpper();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000C51D0 File Offset: 0x000C33D0
		private static string GetGallatinDataCenterPrefix()
		{
			string hostName = Dns.GetHostName();
			return hostName.Substring(0, 2).ToUpper();
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000C51F0 File Offset: 0x000C33F0
		private void CreateOneProbeChain(OAuthWacDiscovery.ProbeStuff probeStuff, string monitoringUser, string partnerServerEndpoint, string secondaryEndpoint, Func<string, string, string, string, ProbeDefinition> probeCreateDefinition)
		{
			string name = probeStuff.Name;
			WTFDiagnostics.TraceInformation<string, string, string, string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthWacDiscovery.CreateOneProbeChain: Configuring probe {0} at endpoint {1} and secondary endpoint {2} with user {3}", name, partnerServerEndpoint, secondaryEndpoint, monitoringUser, null, "CreateOneProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 316);
			ProbeDefinition probeDefinition = probeCreateDefinition(monitoringUser, name, partnerServerEndpoint, secondaryEndpoint);
			probeDefinition.AssemblyPath = OAuthWacDiscovery.AssemblyPath;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.RecurrenceIntervalSeconds = 900;
			probeDefinition.TimeoutSeconds = 300;
			probeDefinition.StartTime = DateTime.UtcNow;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			this.CreateMonitors(probeStuff, probeDefinition);
			WTFDiagnostics.TraceInformation<string, string, string, string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.CreateOneProbeChain: Created probes/monitors/responders for probe {0} at endpoint {1} and secondary endpoint {2} with user {3}", name, partnerServerEndpoint, secondaryEndpoint, monitoringUser, null, "CreateOneProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 351);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000C52B4 File Offset: 0x000C34B4
		private void CreateMonitors(OAuthWacDiscovery.ProbeStuff probeStuff, ProbeDefinition p)
		{
			string monitorName = probeStuff.MonitorName;
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, p.ConstructWorkItemResultName(), ExchangeComponent.OwaDependency.Name, ExchangeComponent.OwaDependency, 8, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitoringIntervalSeconds = 8100;
			monitorDefinition.TimeoutSeconds = 300;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.TargetResource = p.TargetResource;
			this.CreateResponders(probeStuff, monitorDefinition);
			monitorDefinition.StartTime = DateTime.UtcNow;
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate WAC health is not impacted by any issues";
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "configuring monitor " + monitorDefinition.Name, null, "CreateMonitors", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 399);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000C5384 File Offset: 0x000C3584
		private void CreateResponders(OAuthWacDiscovery.ProbeStuff probeStuff, MonitorDefinition m)
		{
			string alertResponderName = probeStuff.AlertResponderName;
			string customMessage = string.Concat(new object[]
			{
				probeStuff.Name,
				" Failed For last ",
				8,
				" attempts"
			});
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(alertResponderName, ExchangeComponent.OwaDependency.Name, m.Name, m.ConstructWorkItemResultName(), m.TargetResource, ServiceHealthStatus.None, ExchangeComponent.OwaDependency.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.EscalationMessageUnhealthy(customMessage), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "configuring escalate responder " + responderDefinition.Name, null, "CreateResponders", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacDiscovery.cs", 437);
			responderDefinition.StartTime = DateTime.UtcNow;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x0400179D RID: 6045
		public const int DefaultProbeTimeoutSeconds = 300;

		// Token: 0x0400179E RID: 6046
		public const int DefaultProbeRecurrenceIntervalSeconds = 900;

		// Token: 0x0400179F RID: 6047
		public const int DefaultFailedProbeThreshold = 8;

		// Token: 0x040017A0 RID: 6048
		private const string WacServerTdsEndpoint = "https://blue-wac.corp.microsoft.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";

		// Token: 0x040017A1 RID: 6049
		private const string WacServerSdfEndpoint = "https://word-view.officeapps-df.live.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";

		// Token: 0x040017A2 RID: 6050
		private const string WacServerProdEndpoint = "https://word-view.officeapps.live.com/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";

		// Token: 0x040017A3 RID: 6051
		private const string WacServerGallatinBeijingEndpoint = "https://bjb-partner.officewebapps.cn/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";

		// Token: 0x040017A4 RID: 6052
		private const string WacServerGallatinShanghaiEndpoint = "https://sha-partner.officewebapps.cn/wv/wordviewerframe.aspx?UI_LLCCDC_LLCC";

		// Token: 0x040017A5 RID: 6053
		private const string WopiExchangeServerTdsEndpoint = "https://exchangelabs.live-int.com/owa/{0}/wopi/files/@/owaatt";

		// Token: 0x040017A6 RID: 6054
		private const string WopiExchangeServerSdfEndpoint = "https://sdfpilot.outlook.com:443/owa/{0}/wopi/files/@/owaatt";

		// Token: 0x040017A7 RID: 6055
		private const string WopiExchangeServerProdEndpoint = "https://outlook.office365.com:443/owa/{0}/wopi/files/@/owaatt";

		// Token: 0x040017A8 RID: 6056
		private const string WopiExchangeServerGallatinEndpoint = "https://partner.outlook.cn:443/owa/{0}/wopi/files/@/owaatt";

		// Token: 0x040017A9 RID: 6057
		private const string Shanghai = "SH";

		// Token: 0x040017AA RID: 6058
		private const string Beijing = "BJ";

		// Token: 0x040017AB RID: 6059
		internal static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040017AC RID: 6060
		private static readonly OAuthWacDiscovery.ProbeStuff probeStuff = new OAuthWacDiscovery.ProbeStuff
		{
			ParterProbeType = "Wac",
			Name = "OAuthWacProbe",
			MonitorName = "OAuthWacMonitor",
			AlertResponderName = "OAuthWacAlert"
		};

		// Token: 0x040017AD RID: 6061
		private bool serverToRunWacProbe;

		// Token: 0x0200051B RID: 1307
		private class ProbeStuff
		{
			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x06002037 RID: 8247 RVA: 0x000C54C4 File Offset: 0x000C36C4
			// (set) Token: 0x06002038 RID: 8248 RVA: 0x000C54CC File Offset: 0x000C36CC
			public string ParterProbeType { get; set; }

			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x06002039 RID: 8249 RVA: 0x000C54D5 File Offset: 0x000C36D5
			// (set) Token: 0x0600203A RID: 8250 RVA: 0x000C54DD File Offset: 0x000C36DD
			public string Name { get; set; }

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x0600203B RID: 8251 RVA: 0x000C54E6 File Offset: 0x000C36E6
			// (set) Token: 0x0600203C RID: 8252 RVA: 0x000C54EE File Offset: 0x000C36EE
			public string MonitorName { get; set; }

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x0600203D RID: 8253 RVA: 0x000C54F7 File Offset: 0x000C36F7
			// (set) Token: 0x0600203E RID: 8254 RVA: 0x000C54FF File Offset: 0x000C36FF
			public string AlertResponderName { get; set; }
		}
	}
}
