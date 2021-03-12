using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x02000174 RID: 372
	public sealed class EwsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000437C0 File Offset: 0x000419C0
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x000437C8 File Offset: 0x000419C8
		public int ProbeRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000437D1 File Offset: 0x000419D1
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x000437D9 File Offset: 0x000419D9
		public int MonitoringIntervalSeconds { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000437E2 File Offset: 0x000419E2
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x000437EA File Offset: 0x000419EA
		public int MonitoringRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000437F3 File Offset: 0x000419F3
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x000437FB File Offset: 0x000419FB
		public int ProbeTimeoutSeconds { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00043804 File Offset: 0x00041A04
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0004380C File Offset: 0x00041A0C
		public int DegradedTransitionSeconds { get; private set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00043815 File Offset: 0x00041A15
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0004381D File Offset: 0x00041A1D
		public int UnhealthyTransitionSeconds { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00043826 File Offset: 0x00041A26
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0004382E File Offset: 0x00041A2E
		public int UnrecoverableTransitionSeconds { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00043837 File Offset: 0x00041A37
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0004383F File Offset: 0x00041A3F
		public int IISRecycleRetryCount { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00043848 File Offset: 0x00041A48
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00043850 File Offset: 0x00041A50
		public int IISRecycleRetryIntervalSeconds { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00043859 File Offset: 0x00041A59
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00043861 File Offset: 0x00041A61
		public int FailedProbeThreshold { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0004386A File Offset: 0x00041A6A
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x00043872 File Offset: 0x00041A72
		public bool IsIISRecycleResponderEnabled { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0004387B File Offset: 0x00041A7B
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00043883 File Offset: 0x00041A83
		public bool IsFailoverResponderEnabled { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0004388C File Offset: 0x00041A8C
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00043894 File Offset: 0x00041A94
		public bool IsAlertResponderEnabled { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0004389D File Offset: 0x00041A9D
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x000438A5 File Offset: 0x00041AA5
		public string ServerRole { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000438AE File Offset: 0x00041AAE
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x000438B6 File Offset: 0x00041AB6
		public string ProbeName { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000438BF File Offset: 0x00041ABF
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x000438C7 File Offset: 0x00041AC7
		public bool Verbose { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000438D0 File Offset: 0x00041AD0
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x000438D8 File Offset: 0x00041AD8
		public bool EnablePagedAlerts { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000438E1 File Offset: 0x00041AE1
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x000438E9 File Offset: 0x00041AE9
		public bool CreateRespondersForTest { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000438F2 File Offset: 0x00041AF2
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x000438FA File Offset: 0x00041AFA
		public ProbeDefinition ProbeDefinition { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00043903 File Offset: 0x00041B03
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0004390B File Offset: 0x00041B0B
		public ProbeIdentity ProbeIdentity { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00043914 File Offset: 0x00041B14
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0004391C File Offset: 0x00041B1C
		public MonitorIdentity MonitorIdentity { get; private set; }

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00043928 File Offset: 0x00041B28
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.breadcrumbs = new Breadcrumbs(1024, base.TraceContext);
			try
			{
				this.Configure(base.TraceContext);
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				try
				{
					if (instance.ExchangeServerRoleEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "EwsDiscovery:: DoWork(): Could not find ExchangeServerRoleEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 218);
						return;
					}
				}
				catch (Exception ex)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, string.Format("EwsDiscovery:: DoWork(): ExchangeServerRoleEndpoint object threw exception.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 224);
					return;
				}
				try
				{
					if (instance.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "EwsDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 234);
						return;
					}
				}
				catch (Exception ex2)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, string.Format("EwsDiscovery:: DoWork(): MailboxDatabaseEndpoint object threw exception.  Exception:{0}", ex2.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 240);
					return;
				}
				foreach (EwsDiscovery.ProbeStuff probeStuff in EwsDiscovery.ProbeTable)
				{
					if (this.ProbeName.Equals(probeStuff.Name, StringComparison.OrdinalIgnoreCase))
					{
						this.probeStuff = probeStuff;
					}
				}
				if (this.probeStuff == null)
				{
					this.breadcrumbs.Drop("EwsDiscovery.DoWork: ProbeType {0} is not supported at this time.", new object[]
					{
						this.ProbeName
					});
					throw new NotSupportedException(string.Format("EwsDiscovery.DoWork: ProbeType {0} is not supported at this time.", this.ProbeName));
				}
				string serverRole;
				if ((serverRole = this.ServerRole) != null)
				{
					ICollection<MailboxDatabaseInfo> collection;
					if (!(serverRole == "Mailbox"))
					{
						if (!(serverRole == "ClientAccess"))
						{
							goto IL_2C6;
						}
						if (LocalEndpointManager.IsDataCenter)
						{
							this.breadcrumbs.Drop("EwsDiscovery.DoWork: Skipping ProbeType {0} as it is not needed for ClientAccess in Datacenter.", new object[]
							{
								this.ProbeName
							});
							return;
						}
						if (!instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
						{
							this.breadcrumbs.Drop("EwsDiscovery.DoWork: {0} role is required and is not present on this server.", new object[]
							{
								this.ServerRole
							});
							return;
						}
						if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count == 0)
						{
							this.breadcrumbs.Drop("EwsDiscovery.DoWork: mailbox database collection is empty on this server.");
							return;
						}
						collection = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
					}
					else
					{
						if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
						{
							this.breadcrumbs.Drop("EwsDiscovery.DoWork: {0} role is required and is not present on this server.", new object[]
							{
								this.ServerRole
							});
							return;
						}
						if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
						{
							this.breadcrumbs.Drop("EwsDiscovery.DoWork: mailbox database collection is empty on this server.");
							return;
						}
						collection = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
					}
					HashSet<string> hashSet = new HashSet<string>();
					this.breadcrumbs.Drop("CreateInstancePerServer={0},AllMailboxes={1}", new object[]
					{
						this.probeStuff.CreateInstancePerServer ? "True" : "False",
						collection.Count
					});
					foreach (MailboxDatabaseInfo mailboxDatabaseInfo in collection)
					{
						if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
						{
							this.breadcrumbs.Drop("Ignore mbxdb {0} (password empty)", new object[]
							{
								mailboxDatabaseInfo.MailboxDatabaseName
							});
						}
						else
						{
							string s = string.Format("Creating probe", new object[0]);
							DatabaseLocationInfo databaseLocationInfo = null;
							if (this.probeStuff.CreateInstancePerServer)
							{
								databaseLocationInfo = EwsDiscovery.activeManager.Value.GetServerForDatabase(mailboxDatabaseInfo.MailboxDatabaseGuid);
								string item = databaseLocationInfo.ServerFqdn.ToUpper();
								if (hashSet.Contains(item))
								{
									continue;
								}
								s = string.Format("Creating probe for {0}", databaseLocationInfo.ServerFqdn);
								hashSet.Add(item);
							}
							this.breadcrumbs.Drop(s);
							this.CreateProbe(base.TraceContext, mailboxDatabaseInfo, databaseLocationInfo);
							this.CreateMonitors(base.TraceContext);
							if (!this.probeStuff.CreateInstancePerServer)
							{
								break;
							}
						}
					}
					this.breadcrumbs.Drop("All done!");
					return;
				}
				IL_2C6:
				throw new NotSupportedException(string.Format("EwsDiscovery.DoWork: server role {0} is not supported at this time", this.ServerRole));
			}
			finally
			{
				this.ReportResult(base.TraceContext);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00043DF0 File Offset: 0x00041FF0
		private static ProbeDefinition CreateAutodiscoverProbeDefinition(string monitoringAccount, string monitoringAccountDomain, string monitoringAccountPassword, string probeName, string serviceName, string targetResource = null)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				TypeName = typeof(AutodiscoverE15Probe).FullName,
				Name = probeName,
				TargetResource = targetResource,
				ServiceName = serviceName,
				RecurrenceIntervalSeconds = 300,
				TimeoutSeconds = 20,
				MaxRetryAttempts = 0,
				Account = monitoringAccount + "@" + monitoringAccountDomain,
				AccountPassword = monitoringAccountPassword,
				AccountDisplayName = monitoringAccount,
				Endpoint = EwsConstants.AutodiscoverSvcEndpoint,
				SecondaryEndpoint = EwsConstants.AutodiscoverXmlEndpoint
			};
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00043E90 File Offset: 0x00042090
		private static ProbeDefinition CreateEwsGenericProbeDefinition(string monitoringAccount, string monitoringAccountDomain, string monitoringAccountPassword, string probeName, string serviceName, string targetResource = null)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				TypeName = typeof(EwsGenericProbe).FullName,
				Name = probeName,
				TargetResource = targetResource,
				ServiceName = serviceName,
				Account = monitoringAccount + "@" + monitoringAccountDomain,
				AccountPassword = monitoringAccountPassword,
				AccountDisplayName = monitoringAccount,
				RecurrenceIntervalSeconds = 300,
				TimeoutSeconds = 20,
				MaxRetryAttempts = 0,
				Endpoint = EwsConstants.EwsEndpoint
			};
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00043F28 File Offset: 0x00042128
		private void Configure(TracingContext traceContext)
		{
			this.Verbose = this.ReadAttribute("Verbose", true);
			this.ServerRole = this.ReadAttribute("ServerRole", "Mailbox");
			this.ProbeName = this.ReadAttribute("ProbeType", "EwsProtocolSelfTest");
			this.ProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ProbeRecurrenceSpan", TimeSpan.FromMinutes(300.0)).TotalSeconds;
			this.ProbeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutSpan", TimeSpan.FromSeconds(20.0)).TotalSeconds;
			this.MonitoringIntervalSeconds = (int)this.ReadAttribute("MonitoringIntervalSpan", TimeSpan.FromSeconds(1800.0)).TotalSeconds;
			this.MonitoringRecurrenceIntervalSeconds = (int)this.ReadAttribute("MonitoringRecurrenceIntervalSpan", TimeSpan.FromSeconds(0.0)).TotalSeconds;
			this.DegradedTransitionSeconds = (int)this.ReadAttribute("DegradedTransitionSpan", TimeSpan.FromMinutes(0.0)).TotalSeconds;
			this.UnhealthyTransitionSeconds = (int)this.ReadAttribute("UnhealthyTransitionSpan", TimeSpan.FromMinutes(20.0)).TotalSeconds;
			this.UnrecoverableTransitionSeconds = (int)this.ReadAttribute("UnrecoverableTransitionSpan", TimeSpan.FromMinutes(20.0)).TotalSeconds;
			this.IISRecycleRetryCount = this.ReadAttribute("IISRecycleRetryCount", 1);
			this.IISRecycleRetryIntervalSeconds = (int)this.ReadAttribute("IISRecycleRetrySpan", TimeSpan.FromSeconds(30.0)).TotalSeconds;
			this.FailedProbeThreshold = this.ReadAttribute("FailedProbeThreshold", 4);
			this.IsIISRecycleResponderEnabled = this.ReadAttribute("IISRecycleResponderEnabled", false);
			this.IsFailoverResponderEnabled = this.ReadAttribute("FailoverResponderEnabled", false);
			this.IsAlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", false);
			this.EnablePagedAlerts = this.ReadAttribute("EnablePagedAlerts", true);
			this.CreateRespondersForTest = this.ReadAttribute("CreateRespondersForTest", false);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00044138 File Offset: 0x00042338
		private void ReportResult(TracingContext traceContext)
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 498);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00044180 File Offset: 0x00042380
		private void CreateProbe(TracingContext traceContext, MailboxDatabaseInfo dbInfo, DatabaseLocationInfo dbLocationInfo = null)
		{
			this.ProbeDefinition = this.GetBaseDefinition(dbInfo, dbLocationInfo);
			this.ProbeDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceIntervalSeconds;
			this.ProbeDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			this.CopyAttributes(this.probeStuff.Attributes, this.ProbeDefinition);
			this.probeResultName = this.ProbeDefinition.ConstructWorkItemResultName();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring probe " + this.probeStuff.Name, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 523);
			base.Broker.AddWorkDefinition<ProbeDefinition>(this.ProbeDefinition, base.TraceContext);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00044234 File Offset: 0x00042434
		private ProbeDefinition GetBaseDefinition(MailboxDatabaseInfo dbInfo, DatabaseLocationInfo dbLocationInfo = null)
		{
			bool flag = true;
			string targetResource = (dbLocationInfo != null) ? dbLocationInfo.ServerFqdn : string.Empty;
			string name;
			if ((name = this.probeStuff.Name) != null)
			{
				if (!(name == "EWSDeepTest"))
				{
					if (!(name == "EWSSelfTest"))
					{
						if (!(name == "EWSCtpTest"))
						{
							if (!(name == "AutodiscoverSelfTest"))
							{
								if (!(name == "AutodiscoverCtpTest"))
								{
									goto IL_E2;
								}
								this.ProbeIdentity = ProbeIdentity.Create(ExchangeComponent.Autodiscover, ProbeType.Ctp, null, targetResource);
								flag = false;
							}
							else
							{
								this.ProbeIdentity = ProbeIdentity.Create(ExchangeComponent.AutodiscoverProtocol, ProbeType.SelfTest, null, "MSExchangeAutoDiscoverAppPool");
								flag = false;
							}
						}
						else
						{
							this.ProbeIdentity = ProbeIdentity.Create(ExchangeComponent.Ews, ProbeType.Ctp, null, targetResource);
						}
					}
					else
					{
						this.ProbeIdentity = ProbeIdentity.Create(ExchangeComponent.EwsProtocol, ProbeType.SelfTest, null, "MSExchangeServicesAppPool");
					}
				}
				else
				{
					this.ProbeIdentity = ProbeIdentity.Create(ExchangeComponent.EwsProtocol, ProbeType.DeepTest, null, dbInfo.MailboxDatabaseName);
				}
				if (!flag)
				{
					return EwsDiscovery.CreateAutodiscoverProbeDefinition(dbInfo.MonitoringAccount, dbInfo.MonitoringAccountDomain, dbInfo.MonitoringAccountPassword, this.ProbeIdentity.Name, this.ProbeIdentity.ServiceName, this.ProbeIdentity.TargetResource);
				}
				return EwsDiscovery.CreateEwsGenericProbeDefinition(dbInfo.MonitoringAccount, dbInfo.MonitoringAccountDomain, dbInfo.MonitoringAccountPassword, this.ProbeIdentity.Name, this.ProbeIdentity.ServiceName, this.ProbeIdentity.TargetResource);
			}
			IL_E2:
			throw new NotSupportedException(string.Format("probe type '{0}' is not supported at this time", this.probeStuff.Name));
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000443B4 File Offset: 0x000425B4
		private void CreateMonitors(TracingContext traceContext)
		{
			this.MonitorIdentity = this.ProbeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(this.MonitorIdentity.Name, this.probeResultName, this.MonitorIdentity.ServiceName, this.MonitorIdentity.Component, this.FailedProbeThreshold, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = this.MonitoringRecurrenceIntervalSeconds;
			monitorDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.MonitoringIntervalSeconds = this.MonitoringIntervalSeconds;
			monitorDefinition.TargetResource = this.ProbeIdentity.TargetResource;
			monitorDefinition.IsHaImpacting = this.IsFailoverResponderEnabled;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring monitor " + monitorDefinition.Name, null, "CreateMonitors", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 618);
			monitorDefinition.MonitorStateTransitions = this.CreateResponderChain(base.TraceContext, this.probeStuff.AppPool, monitorDefinition);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate EWS health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000444CC File Offset: 0x000426CC
		private MonitorStateTransition[] CreateResponderChain(TracingContext traceContext, string appPool, MonitorIdentity monitorIdentity)
		{
			MonitorStateTransition[] result = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, this.UnhealthyTransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSeconds)
			};
			if (Utils.EnableResponderForCurrentEnvironment(this.IsIISRecycleResponderEnabled, this.CreateRespondersForTest))
			{
				ResponderIdentity responderIdentity = this.MonitorIdentity.CreateResponderIdentity("Restart", appPool);
				ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(responderIdentity.Name, monitorIdentity.Name, appPool, ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, responderIdentity.ServiceName, true, this.probeStuff.ResponderThrottleGroup);
				responderDefinition.AlertMask = monitorIdentity.GetAlertMask();
				responderDefinition.AlertTypeId = monitorIdentity.Name;
				responderDefinition.TargetResource = responderIdentity.TargetResource;
				responderDefinition.RecurrenceIntervalSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.WaitIntervalSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.TimeoutSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.MaxRetryAttempts = this.IISRecycleRetryCount;
				responderDefinition.Enabled = this.IsIISRecycleResponderEnabled;
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring IISRecycle responder " + responderDefinition.Name, null, "CreateResponderChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 681);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
			if (Utils.EnableResponderForCurrentEnvironment(this.IsFailoverResponderEnabled, this.CreateRespondersForTest))
			{
				ResponderIdentity responderIdentity2 = this.MonitorIdentity.CreateResponderIdentity("Failover", appPool);
				ResponderDefinition responderDefinition = SystemFailoverResponder.CreateDefinition(responderIdentity2.Name, monitorIdentity.Name, ServiceHealthStatus.Unhealthy, this.ProbeIdentity.Component.Name, responderIdentity2.ServiceName, true);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring Failover responder " + responderDefinition.Name, null, "CreateResponderChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 701);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
			if (Utils.EnableResponderForCurrentEnvironment(this.IsAlertResponderEnabled, this.CreateRespondersForTest))
			{
				ResponderIdentity responderIdentity3 = this.MonitorIdentity.CreateResponderIdentity("Escalate", appPool);
				string escalationMessageUnhealthy = Strings.EwsAutodEscalationMessageUnhealthy((this.probeStuff.RecoveryStringDelegate != null) ? this.probeStuff.RecoveryStringDelegate(appPool) : string.Empty);
				ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(responderIdentity3.Name, responderIdentity3.ServiceName, monitorIdentity.Name, monitorIdentity.GetAlertMask(), this.MonitorIdentity.TargetResource, ServiceHealthStatus.Unrecoverable, this.ProbeIdentity.Component.EscalationTeam, Strings.EwsAutodEscalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				responderDefinition.Enabled = this.IsAlertResponderEnabled;
				responderDefinition.NotificationServiceClass = (this.EnablePagedAlerts ? NotificationServiceClass.Urgent : NotificationServiceClass.UrgentInTraining);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EWSTracer, base.TraceContext, "configuring escalate responder " + responderDefinition.Name, null, "CreateResponderChain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EwsDiscovery.cs", 733);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
			return result;
		}

		// Token: 0x04000809 RID: 2057
		internal static readonly string RecycleResponderTypeName = typeof(ResetIISAppPoolResponder).FullName;

		// Token: 0x0400080A RID: 2058
		public static string[] StandardAttributes = new string[]
		{
			"ApiRetryCount",
			"ApiRetrySleepInMilliseconds",
			"UseXropEndPoint",
			"TargetPort",
			"Domain",
			"IsOutsideInMonitoring",
			"ExchangeSku",
			"PrimaryAuthN",
			"TrustAnySslCertificate",
			"UserAgentPart",
			"Verbose",
			"OperationName",
			"IncludeExchangeRpcUrl"
		};

		// Token: 0x0400080B RID: 2059
		private static readonly EwsDiscovery.ProbeStuff[] ProbeTable = new EwsDiscovery.ProbeStuff[]
		{
			new EwsDiscovery.ProbeStuff
			{
				Name = "EWSDeepTest",
				Attributes = EwsDiscovery.StandardAttributes,
				AppPool = "MSExchangeServicesAppPool",
				CreateInstancePerServer = false,
				ResponderThrottleGroup = "Dag"
			},
			new EwsDiscovery.ProbeStuff
			{
				Name = "EWSSelfTest",
				Attributes = EwsDiscovery.StandardAttributes,
				AppPool = "MSExchangeServicesAppPool",
				CreateInstancePerServer = false,
				RecoveryStringDelegate = new Func<string, LocalizedString>(Strings.EwsAutodSelfTestEscalationRecoveryDetails),
				ResponderThrottleGroup = "Dag"
			},
			new EwsDiscovery.ProbeStuff
			{
				Name = "EWSCtpTest",
				Attributes = EwsDiscovery.StandardAttributes,
				AppPool = "MSExchangeServicesAppPool",
				CreateInstancePerServer = true,
				ResponderThrottleGroup = "Cafe"
			},
			new EwsDiscovery.ProbeStuff
			{
				Name = "AutodiscoverSelfTest",
				Attributes = EwsDiscovery.StandardAttributes,
				AppPool = "MSExchangeAutoDiscoverAppPool",
				CreateInstancePerServer = false,
				RecoveryStringDelegate = new Func<string, LocalizedString>(Strings.EwsAutodSelfTestEscalationRecoveryDetails),
				ResponderThrottleGroup = "Dag"
			},
			new EwsDiscovery.ProbeStuff
			{
				Name = "AutodiscoverCtpTest",
				Attributes = EwsDiscovery.StandardAttributes,
				AppPool = "MSExchangeAutoDiscoverAppPool",
				CreateInstancePerServer = true,
				ResponderThrottleGroup = "Cafe"
			}
		};

		// Token: 0x0400080C RID: 2060
		private string probeResultName;

		// Token: 0x0400080D RID: 2061
		private EwsDiscovery.ProbeStuff probeStuff;

		// Token: 0x0400080E RID: 2062
		private Breadcrumbs breadcrumbs;

		// Token: 0x0400080F RID: 2063
		private static Lazy<ActiveManager> activeManager = new Lazy<ActiveManager>(() => ActiveManager.GetNoncachingActiveManagerInstance());

		// Token: 0x02000175 RID: 373
		internal class ProbeStuff
		{
			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00044A09 File Offset: 0x00042C09
			// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00044A11 File Offset: 0x00042C11
			public string Name { get; set; }

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00044A1A File Offset: 0x00042C1A
			// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00044A22 File Offset: 0x00042C22
			public string[] Attributes { get; set; }

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00044A2B File Offset: 0x00042C2B
			// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00044A33 File Offset: 0x00042C33
			public string AppPool { get; set; }

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00044A3C File Offset: 0x00042C3C
			// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00044A44 File Offset: 0x00042C44
			public bool CreateInstancePerServer { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00044A4D File Offset: 0x00042C4D
			// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00044A55 File Offset: 0x00042C55
			public Func<string, LocalizedString> RecoveryStringDelegate { get; set; }

			// Token: 0x04000826 RID: 2086
			public string ResponderThrottleGroup;
		}
	}
}
