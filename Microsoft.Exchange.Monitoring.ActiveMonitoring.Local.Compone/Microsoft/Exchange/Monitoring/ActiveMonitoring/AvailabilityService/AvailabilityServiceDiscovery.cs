using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService
{
	// Token: 0x02000012 RID: 18
	public sealed class AvailabilityServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000551F File Offset: 0x0000371F
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00005527 File Offset: 0x00003727
		public int DegradedTransitionSpanSeconds { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00005530 File Offset: 0x00003730
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00005538 File Offset: 0x00003738
		public int FailedProbeThreshold { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00005541 File Offset: 0x00003741
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00005549 File Offset: 0x00003749
		public bool IsAlertResponderEnabled { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00005552 File Offset: 0x00003752
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000555A File Offset: 0x0000375A
		public bool IsOnPremisesEnabled { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00005563 File Offset: 0x00003763
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000556B File Offset: 0x0000376B
		public int MinimumSecondsBetweenEscalates { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00005574 File Offset: 0x00003774
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000557C File Offset: 0x0000377C
		public int MonitoringIntervalSeconds { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005585 File Offset: 0x00003785
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000558D File Offset: 0x0000378D
		public int MonitoringRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005596 File Offset: 0x00003796
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000559E File Offset: 0x0000379E
		public int ProbeRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000055A7 File Offset: 0x000037A7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000055AF File Offset: 0x000037AF
		public int ProbeTimeoutSeconds { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000055B8 File Offset: 0x000037B8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000055C0 File Offset: 0x000037C0
		public string ProbeType { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000055C9 File Offset: 0x000037C9
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000055D1 File Offset: 0x000037D1
		public int UnhealthyTransitionSpanSeconds { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000055DA File Offset: 0x000037DA
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000055E2 File Offset: 0x000037E2
		public bool IsAlerting24Hours { get; private set; }

		// Token: 0x0600006C RID: 108 RVA: 0x000055EC File Offset: 0x000037EC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.breadcrumbs = new Breadcrumbs(1024, base.TraceContext);
			try
			{
				this.Configure();
				ICollection<MailboxDatabaseInfo> mbxDatabaseInfo = this.GetMbxDatabaseInfo();
				if (mbxDatabaseInfo != null)
				{
					this.CreateWorkItemDefinition(mbxDatabaseInfo);
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005640 File Offset: 0x00003840
		private void Configure()
		{
			this.DegradedTransitionSpanSeconds = (int)this.ReadAttribute("DegradedTransitionSpan", AvailabilityServiceDiscovery.DefaultDegradedTransitionSpan).TotalSeconds;
			this.FailedProbeThreshold = this.ReadAttribute("NumberOfConsecutiveFailures", 3);
			this.IsAlertResponderEnabled = this.ReadAttribute("IsAlertingEnabled", false);
			this.IsOnPremisesEnabled = this.ReadAttribute("EnableOnPrem", false);
			this.MinimumSecondsBetweenEscalates = (int)this.ReadAttribute("TimeToWaitUntilNextAlert", AvailabilityServiceDiscovery.DefaultEscalateInterval).TotalSeconds;
			this.MonitoringIntervalSeconds = (int)this.ReadAttribute("MonitorLookbackWindow", AvailabilityServiceDiscovery.DefaultMonitoringInterval).TotalSeconds;
			this.MonitoringRecurrenceIntervalSeconds = (int)this.ReadAttribute("MonitorExecutionInterval", AvailabilityServiceDiscovery.DefaultMonitoringRecurrenceInterval).TotalSeconds;
			this.ProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ProbeExecutionInterval", AvailabilityServiceDiscovery.DefaultProbeRecurrenceInterval).TotalSeconds;
			this.ProbeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutSpan", AvailabilityServiceDiscovery.DefaultProbeTimeout).TotalSeconds;
			this.ProbeType = this.ReadAttribute("ProbeType", "AvailabilityServiceCrossServerTest");
			this.UnhealthyTransitionSpanSeconds = (int)this.ReadAttribute("TimeToEscalateAfterUnhealthyState", AvailabilityServiceDiscovery.DefaultUnhealthyTransitionSpan).TotalSeconds;
			this.IsAlerting24Hours = this.ReadAttribute("IsAlerting24Hours", false);
			base.Definition.Attributes["ApiRetryCount"] = this.ReadAttribute("NumberOfRetriesInProbe", 0).ToString();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000057B4 File Offset: 0x000039B4
		private ICollection<MailboxDatabaseInfo> GetMbxDatabaseInfo()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (!LocalEndpointManager.IsDataCenter && !this.IsOnPremisesEnabled)
			{
				this.breadcrumbs.Drop("In case of on-premises, IsOnPremisesEnabled should be true in order to create probe/monitor/responder");
				return null;
			}
			foreach (AvailabilityServiceDiscovery.ProbeInfo probeInfo in AvailabilityServiceDiscovery.ProbeTable)
			{
				if (this.ProbeType.Equals(probeInfo.TypeName, StringComparison.OrdinalIgnoreCase))
				{
					this.probeInfo = probeInfo;
				}
			}
			if (this.probeInfo == null)
			{
				this.breadcrumbs.Drop("ProbeType {0} is not supported at this time.", new object[]
				{
					this.ProbeType
				});
				return null;
			}
			if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.breadcrumbs.Drop("Mailbox role is required and is not present on this server.");
				return null;
			}
			if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				this.breadcrumbs.Drop("Mailbox database collection is empty on this server.");
				return null;
			}
			return instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000058A8 File Offset: 0x00003AA8
		private void CreateWorkItemDefinition(ICollection<MailboxDatabaseInfo> mbxDBInfo)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mbxDBInfo)
			{
				if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
				{
					this.breadcrumbs.Drop("Ignore mailbox database {0} because it does not have monitoring mailbox", new object[]
					{
						mailboxDatabaseInfo.MailboxDatabaseName
					});
				}
				else
				{
					string databaseActiveHost = DirectoryAccessor.Instance.GetDatabaseActiveHost(mailboxDatabaseInfo.MailboxDatabaseGuid);
					if (!hashSet.Contains(databaseActiveHost))
					{
						if (!ExEnvironment.IsTest && databaseActiveHost.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
						{
							this.breadcrumbs.Drop("[{0}]: Skipping a mailbox because Offbox request requires a passive copy of database {1}", new object[]
							{
								this.ProbeType,
								mailboxDatabaseInfo.MailboxDatabaseName
							});
						}
						else
						{
							this.CreateProbe(mailboxDatabaseInfo);
							this.CreateMonitorAndResponder(mailboxDatabaseInfo);
							hashSet.Add(databaseActiveHost);
							this.breadcrumbs.Drop("[{0}]: From the machine {1}, created a probe [ID={3}] targeting on {2}", new object[]
							{
								this.ProbeType,
								Environment.MachineName,
								databaseActiveHost,
								hashSet.Count
							});
						}
					}
					else
					{
						this.breadcrumbs.Drop("[{0}]: Skipping a mailbox as we already have a probe for server {1}", new object[]
						{
							this.ProbeType,
							databaseActiveHost
						});
					}
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005A20 File Offset: 0x00003C20
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.AvailabilityServiceTracer, base.TraceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\AvailabilityServiceDiscovery.cs", 305);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005A68 File Offset: 0x00003C68
		private void CreateProbe(MailboxDatabaseInfo dbInfo)
		{
			ProbeDefinition baseDefinition = this.GetBaseDefinition(dbInfo);
			baseDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceIntervalSeconds;
			baseDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			this.CopyAttributes(this.probeInfo.Attributes, baseDefinition);
			this.probeResultName = baseDefinition.ConstructWorkItemResultName();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.AvailabilityServiceTracer, base.TraceContext, "configuring probe " + this.probeInfo.ProbeName, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\AvailabilityServiceDiscovery.cs", 328);
			base.Broker.AddWorkDefinition<ProbeDefinition>(baseDefinition, base.TraceContext);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005AFC File Offset: 0x00003CFC
		private ProbeDefinition GetBaseDefinition(MailboxDatabaseInfo dbInfo)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = AvailabilityServiceDiscovery.AssemblyPath;
			probeDefinition.TypeName = typeof(AvailabilityServiceLocalProbe).FullName;
			probeDefinition.Name = this.probeInfo.ProbeName;
			probeDefinition.Account = dbInfo.MonitoringAccount + "@" + dbInfo.MonitoringAccountDomain;
			probeDefinition.AccountPassword = dbInfo.MonitoringAccountPassword;
			probeDefinition.AccountDisplayName = dbInfo.MonitoringAccount;
			probeDefinition.SecondaryAccount = dbInfo.MonitoringAccount + "@" + dbInfo.MonitoringAccountDomain;
			probeDefinition.SecondaryAccountPassword = dbInfo.MonitoringAccountPassword;
			probeDefinition.SecondaryAccountDisplayName = dbInfo.MonitoringAccount;
			probeDefinition.ServiceName = ExchangeComponent.FreeBusy.Name;
			probeDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.Endpoint = AvailabilityServiceDiscovery.ProbeEndPoint.TrimEnd(new char[]
			{
				'/'
			}) + "/ews/exchange.asmx";
			probeDefinition.Attributes["DatabaseGuid"] = dbInfo.MailboxDatabaseGuid.ToString();
			return probeDefinition;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005C18 File Offset: 0x00003E18
		private void CreateMonitorAndResponder(MailboxDatabaseInfo dbInfo)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = AvailabilityServiceDiscovery.AssemblyPath;
			monitorDefinition.TypeName = typeof(AvailabilityServiceOffBoxRequestMonitor).FullName;
			monitorDefinition.Component = ExchangeComponent.FreeBusy;
			monitorDefinition.Name = this.probeInfo.MonitorName;
			monitorDefinition.InsufficientSamplesIntervalSeconds = 0;
			monitorDefinition.SampleMask = this.probeResultName;
			monitorDefinition.ServiceName = ExchangeComponent.FreeBusy.Name;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.MonitoringThreshold = (double)this.FailedProbeThreshold;
			monitorDefinition.MonitoringIntervalSeconds = this.MonitoringIntervalSeconds;
			monitorDefinition.RecurrenceIntervalSeconds = this.MonitoringRecurrenceIntervalSeconds;
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSpanSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, this.UnhealthyTransitionSpanSeconds)
			};
			WTFDiagnostics.TraceInformation(ExTraceGlobals.AvailabilityServiceTracer, base.TraceContext, "configuring monitor " + monitorDefinition.Name, null, "CreateMonitorAndResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\AvailabilityServiceDiscovery.cs", 402);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			if (this.IsAlertResponderEnabled)
			{
				string escalationMessageUnhealthy = this.GetEscalationBody(new Func<string, LocalizedString>(Strings.AvailabilityServiceEscalationHtmlBody), new Func<string, LocalizedString>(Strings.AvailabilityServiceEscalationBody));
				NotificationServiceClass notificationServiceClass = this.IsAlerting24Hours ? NotificationServiceClass.Urgent : NotificationServiceClass.Scheduled;
				ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(this.probeInfo.AlertResponderName, ExchangeComponent.FreeBusy.Name, monitorDefinition.Name, string.Format("{0}/{1}", monitorDefinition.Name, dbInfo.MailboxDatabaseName), dbInfo.MailboxDatabaseName, ServiceHealthStatus.Unhealthy, ExchangeComponent.FreeBusy.EscalationTeam, Strings.AvailabilityServiceEscalationSubjectUnhealthy(this.probeInfo.TypeName), escalationMessageUnhealthy, this.IsAlertResponderEnabled, notificationServiceClass, this.MinimumSecondsBetweenEscalates, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.AvailabilityServiceTracer, base.TraceContext, "configuring escalate responder " + responderDefinition.Name, null, "CreateMonitorAndResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\AvailabilityServiceDiscovery.cs", 432);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005E31 File Offset: 0x00004031
		private LocalizedString GetEscalationBody(Func<string, LocalizedString> htmlBodyDelegate, Func<string, LocalizedString> regularBodyDelegate)
		{
			if (LocalEndpointManager.IsDataCenter)
			{
				return htmlBodyDelegate(this.probeInfo.MonitorName);
			}
			return regularBodyDelegate(this.probeInfo.MonitorName);
		}

		// Token: 0x04000054 RID: 84
		public const string DatabaseGuidAttributeName = "DatabaseGuid";

		// Token: 0x04000055 RID: 85
		private const int DefaultFailedProbeThreshold = 3;

		// Token: 0x04000056 RID: 86
		private const int DefaultMaxRetryAttempts = 3;

		// Token: 0x04000057 RID: 87
		private const int DefaultApiRetryCount = 0;

		// Token: 0x04000058 RID: 88
		private static readonly string[] StandardAttributes = new string[]
		{
			"ApiRetryCount",
			"IsOutsideInMonitoring",
			"KnownErrorCodes",
			"OffBoxRequest",
			"PrimaryAuthN",
			"TargetPort",
			"TrustAnySslCertificate",
			"UserAgentPart",
			"Verbose"
		};

		// Token: 0x04000059 RID: 89
		private static readonly AvailabilityServiceDiscovery.ProbeInfo[] ProbeTable = new AvailabilityServiceDiscovery.ProbeInfo[]
		{
			new AvailabilityServiceDiscovery.ProbeInfo
			{
				TypeName = "AvailabilityServiceCrossServerTest",
				Attributes = AvailabilityServiceDiscovery.StandardAttributes,
				ProbeName = "AvailabilityServiceCrossServerTestProbe",
				MonitorName = "AvailabilityServiceCrossServerTestMonitor",
				AlertResponderName = "AvailabilityServiceCrossServerTestEscalate"
			}
		};

		// Token: 0x0400005A RID: 90
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400005B RID: 91
		private static readonly TimeSpan DefaultEscalateInterval = TimeSpan.FromHours(8.0);

		// Token: 0x0400005C RID: 92
		private static readonly TimeSpan DefaultMonitoringInterval = TimeSpan.FromMinutes(20.0);

		// Token: 0x0400005D RID: 93
		private static readonly TimeSpan DefaultMonitoringRecurrenceInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400005E RID: 94
		private static readonly TimeSpan DefaultProbeRecurrenceInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400005F RID: 95
		private static readonly TimeSpan DefaultProbeTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000060 RID: 96
		private static readonly TimeSpan DefaultDegradedTransitionSpan = TimeSpan.FromMinutes(0.0);

		// Token: 0x04000061 RID: 97
		private static readonly TimeSpan DefaultUnhealthyTransitionSpan = TimeSpan.FromMinutes(20.0);

		// Token: 0x04000062 RID: 98
		private static readonly string ProbeEndPoint = Uri.UriSchemeHttps + "://localhost/";

		// Token: 0x04000063 RID: 99
		private Breadcrumbs breadcrumbs;

		// Token: 0x04000064 RID: 100
		private string probeResultName;

		// Token: 0x04000065 RID: 101
		private AvailabilityServiceDiscovery.ProbeInfo probeInfo;

		// Token: 0x04000066 RID: 102
		private static Lazy<ActiveManager> activeManager = new Lazy<ActiveManager>(() => ActiveManager.GetCachingActiveManagerInstance());

		// Token: 0x02000013 RID: 19
		internal class ProbeInfo
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000078 RID: 120 RVA: 0x00005FEC File Offset: 0x000041EC
			// (set) Token: 0x06000079 RID: 121 RVA: 0x00005FF4 File Offset: 0x000041F4
			public string AlertResponderName { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600007A RID: 122 RVA: 0x00005FFD File Offset: 0x000041FD
			// (set) Token: 0x0600007B RID: 123 RVA: 0x00006005 File Offset: 0x00004205
			public string[] Attributes { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600007C RID: 124 RVA: 0x0000600E File Offset: 0x0000420E
			// (set) Token: 0x0600007D RID: 125 RVA: 0x00006016 File Offset: 0x00004216
			public string MonitorName { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600007E RID: 126 RVA: 0x0000601F File Offset: 0x0000421F
			// (set) Token: 0x0600007F RID: 127 RVA: 0x00006027 File Offset: 0x00004227
			public string ProbeName { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000080 RID: 128 RVA: 0x00006030 File Offset: 0x00004230
			// (set) Token: 0x06000081 RID: 129 RVA: 0x00006038 File Offset: 0x00004238
			public string TypeName { get; set; }
		}
	}
}
