using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x02000244 RID: 580
	public sealed class OAuthDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0006C280 File Offset: 0x0006A480
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0006C288 File Offset: 0x0006A488
		public int ProbeRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0006C291 File Offset: 0x0006A491
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0006C299 File Offset: 0x0006A499
		public int ProbeTimeoutSeconds { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0006C2A2 File Offset: 0x0006A4A2
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0006C2AA File Offset: 0x0006A4AA
		public int FailedProbeThreshold { get; private set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0006C2B3 File Offset: 0x0006A4B3
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0006C2BB File Offset: 0x0006A4BB
		public bool IsAlertResponderEnabled { get; private set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0006C2CC File Offset: 0x0006A4CC
		public bool ExchangeProbeEnabled { get; private set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0006C2D5 File Offset: 0x0006A4D5
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0006C2DD File Offset: 0x0006A4DD
		public bool LyncProbeEnabled { get; private set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0006C2E6 File Offset: 0x0006A4E6
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0006C2EE File Offset: 0x0006A4EE
		public bool SharePointProbeEnabled { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0006C2F7 File Offset: 0x0006A4F7
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x0006C2FF File Offset: 0x0006A4FF
		public bool OnPremProbeEnabled { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0006C308 File Offset: 0x0006A508
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x0006C310 File Offset: 0x0006A510
		public List<Uri> ExchangeServerEndpoints { get; private set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0006C319 File Offset: 0x0006A519
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x0006C321 File Offset: 0x0006A521
		public List<Uri> LyncServerEndpoints { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0006C32A File Offset: 0x0006A52A
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x0006C332 File Offset: 0x0006A532
		public List<Uri> SharePointServerEndpoints { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0006C33B File Offset: 0x0006A53B
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0006C343 File Offset: 0x0006A543
		public string MonitoringUserIdentity { get; private set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0006C34C File Offset: 0x0006A54C
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0006C354 File Offset: 0x0006A554
		public bool Verbose { get; private set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0006C35D File Offset: 0x0006A55D
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0006C365 File Offset: 0x0006A565
		public bool StartRightAway { get; private set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0006C36E File Offset: 0x0006A56E
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0006C376 File Offset: 0x0006A576
		public bool TrustAnySslCertificate { get; private set; }

		// Token: 0x06001044 RID: 4164 RVA: 0x0006C380 File Offset: 0x0006A580
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				this.serverToRunExchangeProbe = true;
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.DoWork: Cafe role is present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 157);
			}
			if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				this.serverToRunLyncProbe = true;
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.DoWork: Cafe role is present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 166);
			}
			if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				this.serverToRunSharepointProbe = true;
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.DoWork: Cafe role is present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 175);
			}
			if (!this.serverToRunExchangeProbe && !this.serverToRunLyncProbe && !this.serverToRunSharepointProbe)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.DoWork: Server roles are not present on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 183);
				return;
			}
			this.Configure();
			List<string> oauthMonitoringUsers = this.GetOAuthMonitoringUsers(instance);
			if (oauthMonitoringUsers.Count == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.DoWork: Unable to locate valid monitoring user", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 198);
				return;
			}
			this.CreateProbeChains(oauthMonitoringUsers);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0006C4C0 File Offset: 0x0006A6C0
		private void Configure()
		{
			this.Verbose = this.ReadAttribute("Verbose", false);
			this.ProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ProbeRecurrenceSpan", TimeSpan.FromSeconds(900.0)).TotalSeconds;
			this.ProbeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutSpan", TimeSpan.FromSeconds(480.0)).TotalSeconds;
			this.FailedProbeThreshold = this.ReadAttribute("FailedProbeThreshold", 4);
			this.TrustAnySslCertificate = this.ReadAttribute("TrustAnySslCertificate", false);
			this.ExchangeProbeEnabled = this.ReadAttribute("ExchangeProbeEnabled", true);
			this.LyncProbeEnabled = this.ReadAttribute("LyncProbeEnabled", true);
			this.SharePointProbeEnabled = this.ReadAttribute("SharePointProbeEnabled", true);
			this.ExchangeServerEndpoints = this.ParseEndpoints(this.ReadAttribute("ExchangeServerEndpoints", string.Empty));
			this.LyncServerEndpoints = this.ParseEndpoints(this.ReadAttribute("LyncServerEndpoints", string.Empty));
			this.SharePointServerEndpoints = this.ParseEndpoints(this.ReadAttribute("SharePointServerEndpoints", string.Empty));
			this.MonitoringUserIdentity = this.ReadAttribute("MonitoringUserIdentity", string.Empty);
			this.IsAlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", !ExEnvironment.IsTest);
			this.StartRightAway = this.ReadAttribute("StartRightAway", false);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0006C620 File Offset: 0x0006A820
		private List<Uri> ParseEndpoints(string rawEndpoints)
		{
			List<Uri> list = new List<Uri>();
			if (!string.IsNullOrEmpty(rawEndpoints))
			{
				string[] array = rawEndpoints.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					try
					{
						list.Add(new Uri(text));
					}
					catch (Exception ex)
					{
						WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.ParseEndpoints: Invalid endpoint URI supplied {0}:{1}", text, ex.Message, null, "ParseEndpoints", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 271);
						throw;
					}
				}
			}
			return list;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0006C6C0 File Offset: 0x0006A8C0
		private List<string> GetOAuthMonitoringUsers(LocalEndpointManager endpointManager)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(this.MonitoringUserIdentity))
			{
				if (endpointManager.MailboxDatabaseEndpoint == null || endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count == 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetOAuthMonitoringUsers: mailbox database collection is empty on this server", null, "GetOAuthMonitoringUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 301);
					return list;
				}
				using (IEnumerator<MailboxDatabaseInfo> enumerator = endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MailboxDatabaseInfo mailboxDatabaseInfo = enumerator.Current;
						if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetOAuthMonitoringUsers: Ignore mailbox database {0} because it does not have monitoring mailbox", mailboxDatabaseInfo.MailboxDatabaseName, null, "GetOAuthMonitoringUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 313);
						}
						else
						{
							list.Add(mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain);
						}
					}
					return list;
				}
			}
			list.Add(this.MonitoringUserIdentity);
			return list;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
		private void CreateProbeChains(List<string> monitoringUsers)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.CreateProbeChains: Creating OAuth Partner probe chains", null, "CreateProbeChains", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 341);
			if (LocalEndpointManager.IsDataCenter || this.OnPremProbeEnabled)
			{
				if (this.ExchangeProbeEnabled && this.serverToRunExchangeProbe)
				{
					this.CreateByEndPointPartnerProbeChains(OAuthDiscovery.probeDefinitions["Exchange"], monitoringUsers, this.ExchangeServerEndpoints, new Func<string, string, string, Uri, ProbeDefinition>(OAuthExchangeProbe.CreateDefinition));
				}
				if (this.LyncProbeEnabled && this.serverToRunLyncProbe)
				{
					this.CreateByEndPointPartnerProbeChains(OAuthDiscovery.probeDefinitions["Lync"], monitoringUsers, this.LyncServerEndpoints, new Func<string, string, string, Uri, ProbeDefinition>(OAuthLyncProbe.CreateDefinition));
				}
				if (this.SharePointProbeEnabled && this.serverToRunSharepointProbe)
				{
					this.CreateByEndPointPartnerProbeChains(OAuthDiscovery.probeDefinitions["SharePoint"], monitoringUsers, this.SharePointServerEndpoints, new Func<string, string, string, Uri, ProbeDefinition>(OAuthSharePointProbe.CreateDefinition));
				}
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.CreateProbeChains: Created OAuth Partner probes chains", null, "CreateProbeChains", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 375);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0006C8DC File Offset: 0x0006AADC
		private void CreateByEndPointPartnerProbeChains(OAuthDiscovery.ProbeStuff probeStuff, List<string> monitoringUsers, List<Uri> partnerServerEndpoints, Func<string, string, string, Uri, ProbeDefinition> probeCreateDefinition)
		{
			string monitoringUser = monitoringUsers[0];
			foreach (Uri partnerServerEndpoint in partnerServerEndpoints)
			{
				this.CreateOneProbeChain(probeStuff, monitoringUser, partnerServerEndpoint, probeCreateDefinition);
			}
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0006C938 File Offset: 0x0006AB38
		private void CreateByMonitoringUserPartnerProbeChains(OAuthDiscovery.ProbeStuff probeStuff, List<string> monitoringUsers, List<Uri> partnerServerEndpoints, Func<string, string, string, Uri, ProbeDefinition> probeCreateDefinition)
		{
			Uri partnerServerEndpoint = null;
			if (partnerServerEndpoints.Count > 0)
			{
				partnerServerEndpoint = partnerServerEndpoints[0];
			}
			using (List<string>.Enumerator enumerator = monitoringUsers.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string monitoringUser = enumerator.Current;
					this.CreateOneProbeChain(probeStuff, monitoringUser, partnerServerEndpoint, probeCreateDefinition);
				}
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0006C9A0 File Offset: 0x0006ABA0
		private void CreateOneProbeChain(OAuthDiscovery.ProbeStuff probeStuff, string monitoringUser, Uri partnerServerEndpoint, Func<string, string, string, Uri, ProbeDefinition> probeCreateDefinition)
		{
			string name = probeStuff.Name;
			string arg = (partnerServerEndpoint != null) ? partnerServerEndpoint.Authority : monitoringUser;
			WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.CreateByEndPointPartnerProbeChains: Configuring probe {0} at endpoint {1} with user {2}", name, (partnerServerEndpoint != null) ? partnerServerEndpoint.AbsolutePath : "(none)", monitoringUser, null, "CreateOneProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 456);
			ProbeDefinition probeDefinition = probeCreateDefinition(monitoringUser, name, arg, partnerServerEndpoint);
			probeDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			this.CopyAttributes(probeStuff.Attributes, probeDefinition);
			if (this.StartRightAway)
			{
				probeDefinition.StartTime = DateTime.UtcNow;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring probe " + probeDefinition.Name, null, "CreateOneProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 484);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			this.CreateMonitors(probeStuff, probeDefinition);
			WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.CreateByEndPointPartnerProbeChains: Created probes/monitors/responders for probe {0} at endpoint {1} with user {2}", name, (partnerServerEndpoint != null) ? partnerServerEndpoint.AbsolutePath : "(none)", monitoringUser, null, "CreateOneProbeChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 494);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0006CAD0 File Offset: 0x0006ACD0
		private void CreateMonitors(OAuthDiscovery.ProbeStuff probeStuff, ProbeDefinition p)
		{
			string monitorName = probeStuff.MonitorName;
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, p.ConstructWorkItemResultName(), ExchangeComponent.Ews.Name, ExchangeComponent.Ews, this.FailedProbeThreshold, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitoringIntervalSeconds = (this.FailedProbeThreshold + 1) * this.ProbeRecurrenceIntervalSeconds;
			monitorDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.TargetResource = p.TargetResource;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate OAuth health is not impacted by any issues";
			this.CreateResponders(probeStuff, monitorDefinition);
			if (this.StartRightAway)
			{
				monitorDefinition.StartTime = DateTime.UtcNow;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring monitor " + monitorDefinition.Name, null, "CreateMonitors", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 544);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		private void CreateResponders(OAuthDiscovery.ProbeStuff probeStuff, MonitorDefinition m)
		{
			if (this.IsAlertResponderEnabled)
			{
				string alertResponderName = probeStuff.AlertResponderName;
				string escalationMessageUnhealthy = Strings.EwsAutodEscalationMessageUnhealthy(string.Empty);
				ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(alertResponderName, ExchangeComponent.Ews.Name, m.Name, m.ConstructWorkItemResultName(), m.TargetResource, ServiceHealthStatus.None, ExchangeComponent.Ews.EscalationTeam, Strings.EscalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CafeTracer, base.TraceContext, "configuring escalate responder " + responderDefinition.Name, null, "CreateResponders", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthDiscovery.cs", 583);
				if (this.StartRightAway)
				{
					responderDefinition.StartTime = DateTime.UtcNow;
				}
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
		}

		// Token: 0x04000C22 RID: 3106
		public const int DefaultProbeTimeoutSeconds = 480;

		// Token: 0x04000C23 RID: 3107
		public const int DefaultProbeRecurrenceIntervalSeconds = 900;

		// Token: 0x04000C24 RID: 3108
		public const int DefaultFailedProbeThreshold = 4;

		// Token: 0x04000C25 RID: 3109
		internal static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000C26 RID: 3110
		private static readonly string[] ProbeAttributes = new string[]
		{
			"TrustAnySslCertificate",
			"Verbose"
		};

		// Token: 0x04000C27 RID: 3111
		private static readonly Dictionary<string, OAuthDiscovery.ProbeStuff> probeDefinitions = new Dictionary<string, OAuthDiscovery.ProbeStuff>
		{
			{
				"Exchange",
				new OAuthDiscovery.ProbeStuff
				{
					ParterProbeType = "Exchange",
					Name = "OAuthExchangeProbe",
					Attributes = OAuthDiscovery.ProbeAttributes,
					MonitorName = "OAuthExchangeMonitor",
					AlertResponderName = "OAuthExchangeAlert"
				}
			},
			{
				"Lync",
				new OAuthDiscovery.ProbeStuff
				{
					ParterProbeType = "Lync",
					Name = "OAuthLyncProbe",
					Attributes = OAuthDiscovery.ProbeAttributes,
					MonitorName = "OAuthLyncMonitor",
					AlertResponderName = "OAuthLyncAlert"
				}
			},
			{
				"SharePoint",
				new OAuthDiscovery.ProbeStuff
				{
					ParterProbeType = "SharePoint",
					Name = "OAuthSharePointProbe",
					Attributes = OAuthDiscovery.ProbeAttributes,
					MonitorName = "OAuthSharePointMonitor",
					AlertResponderName = "OAuthSharePointAlert"
				}
			}
		};

		// Token: 0x04000C28 RID: 3112
		private bool serverToRunExchangeProbe;

		// Token: 0x04000C29 RID: 3113
		private bool serverToRunLyncProbe;

		// Token: 0x04000C2A RID: 3114
		private bool serverToRunSharepointProbe;

		// Token: 0x02000245 RID: 581
		private class ProbeStuff
		{
			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06001050 RID: 4176 RVA: 0x0006CDB0 File Offset: 0x0006AFB0
			// (set) Token: 0x06001051 RID: 4177 RVA: 0x0006CDB8 File Offset: 0x0006AFB8
			public string ParterProbeType { get; set; }

			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06001052 RID: 4178 RVA: 0x0006CDC1 File Offset: 0x0006AFC1
			// (set) Token: 0x06001053 RID: 4179 RVA: 0x0006CDC9 File Offset: 0x0006AFC9
			public string Name { get; set; }

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06001054 RID: 4180 RVA: 0x0006CDD2 File Offset: 0x0006AFD2
			// (set) Token: 0x06001055 RID: 4181 RVA: 0x0006CDDA File Offset: 0x0006AFDA
			public string[] Attributes { get; set; }

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06001056 RID: 4182 RVA: 0x0006CDE3 File Offset: 0x0006AFE3
			// (set) Token: 0x06001057 RID: 4183 RVA: 0x0006CDEB File Offset: 0x0006AFEB
			public string MonitorName { get; set; }

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06001058 RID: 4184 RVA: 0x0006CDF4 File Offset: 0x0006AFF4
			// (set) Token: 0x06001059 RID: 4185 RVA: 0x0006CDFC File Offset: 0x0006AFFC
			public string AlertResponderName { get; set; }
		}
	}
}
