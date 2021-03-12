using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x0200003E RID: 62
	public sealed class CafeDiscovery : DiscoveryWorkItem
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000D330 File Offset: 0x0000B530
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000D338 File Offset: 0x0000B538
		public int ProbeRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000D341 File Offset: 0x0000B541
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000D349 File Offset: 0x0000B549
		public int MonitoringIntervalSeconds { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000D352 File Offset: 0x0000B552
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000D35A File Offset: 0x0000B55A
		public int MonitoringRecurrenceIntervalSeconds { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000D363 File Offset: 0x0000B563
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000D36B File Offset: 0x0000B56B
		public int ProbeTimeoutSeconds { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000D374 File Offset: 0x0000B574
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000D37C File Offset: 0x0000B57C
		public int Degraded1TransitionSeconds { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000D385 File Offset: 0x0000B585
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000D38D File Offset: 0x0000B58D
		public int DegradedTransitionSeconds { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000D396 File Offset: 0x0000B596
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000D39E File Offset: 0x0000B59E
		public int UnhealthyTransitionSeconds { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000D3A7 File Offset: 0x0000B5A7
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000D3AF File Offset: 0x0000B5AF
		public int Unhealthy1TransitionSeconds { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		public int Unhealthy2TransitionSeconds { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000D3C9 File Offset: 0x0000B5C9
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000D3D1 File Offset: 0x0000B5D1
		public int UnrecoverableTransitionSeconds { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		public int IISRecycleRetryCount { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000D3F3 File Offset: 0x0000B5F3
		public int IISRecycleRetryIntervalSeconds { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000D404 File Offset: 0x0000B604
		public int ClearLsassCacheIntervalSeconds { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000D40D File Offset: 0x0000B60D
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000D415 File Offset: 0x0000B615
		public int FailedProbeThreshold { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000D41E File Offset: 0x0000B61E
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000D426 File Offset: 0x0000B626
		public bool IsIISRecycleResponderEnabled { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000D42F File Offset: 0x0000B62F
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000D437 File Offset: 0x0000B637
		public bool IsClearLsassCacheResponderEnabled { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000D440 File Offset: 0x0000B640
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000D448 File Offset: 0x0000B648
		public bool IsAlertResponderEnabled { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000D451 File Offset: 0x0000B651
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000D459 File Offset: 0x0000B659
		public bool IsOfflineResponderEnabled { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000D462 File Offset: 0x0000B662
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000D46A File Offset: 0x0000B66A
		public bool IsOfflineFailedAlertResponderEnabled { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D473 File Offset: 0x0000B673
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000D47B File Offset: 0x0000B67B
		public bool AlertResponderCorrelationEnabled { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000D484 File Offset: 0x0000B684
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000D48C File Offset: 0x0000B68C
		public bool IsRebootResponderEnabled { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D495 File Offset: 0x0000B695
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000D49D File Offset: 0x0000B69D
		public bool Verbose { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public bool TrustAnySslCertificate { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public bool CreateRespondersForTest { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		public bool EnablePagedAlerts { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000D4D9 File Offset: 0x0000B6D9
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000D4E1 File Offset: 0x0000B6E1
		private Dictionary<HttpProtocol, bool> EnabledProbes { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000D4EA File Offset: 0x0000B6EA
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000D4F2 File Offset: 0x0000B6F2
		public double OfflineResponderFractionOverride { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D4FB File Offset: 0x0000B6FB
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.CafeTracer;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000D504 File Offset: 0x0000B704
		private static string CafeArrayName
		{
			get
			{
				if (CafeDiscovery.cafeArrayName == null)
				{
					if (CafeDiscovery.AdSession == null)
					{
						throw new ApplicationException("Couldn't create ADSession.");
					}
					Server server = CafeDiscovery.AdSession.FindLocalServer();
					ADObjectId adobjectId = (ADObjectId)server[ServerSchema.ClientAccessArray];
					if (adobjectId != null)
					{
						QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
						{
							new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, adobjectId),
							QueryFilter.NotFilter(ClientAccessArray.PriorTo15ExchangeObjectVersionFilter)
						});
						ClientAccessArray clientAccessArray = CafeDiscovery.AdSession.FindUnique<ClientAccessArray>(null, QueryScope.SubTree, filter);
						if (clientAccessArray != null)
						{
							CafeDiscovery.cafeArrayName = clientAccessArray.Name;
						}
					}
					if (CafeDiscovery.cafeArrayName == null)
					{
						CafeDiscovery.cafeArrayName = Strings.CafeArrayNameCouldNotBeRetrieved;
					}
				}
				return CafeDiscovery.cafeArrayName;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000D5B5 File Offset: 0x0000B7B5
		private static ITopologyConfigurationSession AdSession
		{
			get
			{
				if (CafeDiscovery.adSession == null)
				{
					CafeDiscovery.adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 252, "AdSession", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Cafe\\CafeDiscovery.cs");
				}
				return CafeDiscovery.adSession;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			this.cancellationToken = cancellationToken;
			TracingContext @default = TracingContext.Default;
			this.breadcrumbs = new Breadcrumbs(1024, @default);
			try
			{
				this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: start.");
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (!instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: Cafe role is not present on this server.");
				}
				else if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count == 0)
				{
					this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: mailbox database collection is empty on this server");
				}
				else
				{
					MailboxDatabaseInfo mailboxDatabaseInfo = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.FirstOrDefault((MailboxDatabaseInfo db) => !string.IsNullOrWhiteSpace(db.MonitoringAccountPassword));
					if (mailboxDatabaseInfo != null)
					{
						this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: using mailbox database {0}, user {1}@{2}", new object[]
						{
							mailboxDatabaseInfo.MailboxDatabaseName,
							mailboxDatabaseInfo.MonitoringAccount,
							mailboxDatabaseInfo.MonitoringAccountDomain
						});
						this.Configure(@default);
						this.CreateProbesAndMonitors(@default, mailboxDatabaseInfo);
						this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: end.");
					}
					else
					{
						this.breadcrumbs.Drop("CafeDiscovery.CreateWorkTasks: No probes created! No suitable monitoring mailbox was found.");
					}
				}
			}
			finally
			{
				this.ReportResult(@default);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D754 File Offset: 0x0000B954
		private void Configure(TracingContext traceContext)
		{
			this.Verbose = this.ReadAttribute("Verbose", false);
			this.ProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ProbeRecurrenceSpan", TimeSpan.FromSeconds(15.0)).TotalSeconds;
			this.ProbeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutSpan", TimeSpan.FromSeconds(15.0)).TotalSeconds;
			this.DegradedTransitionSeconds = (int)this.ReadAttribute("DegradedTransitionSpan", TimeSpan.Zero).TotalSeconds;
			this.Degraded1TransitionSeconds = (int)this.ReadAttribute("Degraded1TransitionSpan", TimeSpan.Zero).TotalSeconds;
			this.UnhealthyTransitionSeconds = (int)this.ReadAttribute("UnhealthyTransitionSpan", TimeSpan.FromMinutes(7.0)).TotalSeconds;
			this.Unhealthy1TransitionSeconds = (int)this.ReadAttribute("Unhealthy1TransitionSpan", TimeSpan.FromMinutes(8.0)).TotalSeconds;
			this.Unhealthy2TransitionSeconds = (int)this.ReadAttribute("Unhealthy2TransitionSpan", TimeSpan.FromMinutes(15.0)).TotalSeconds;
			this.UnrecoverableTransitionSeconds = (int)this.ReadAttribute("UnrecoverableTransitionSpan", TimeSpan.FromMinutes(60.0)).TotalSeconds;
			this.IISRecycleRetryCount = this.ReadAttribute("IISRecycleRetryCount", 1);
			this.IISRecycleRetryIntervalSeconds = (int)this.ReadAttribute("IISRecycleRetrySpan", TimeSpan.FromSeconds(30.0)).TotalSeconds;
			this.ClearLsassCacheIntervalSeconds = (int)this.ReadAttribute("ClearLsassCacheIntervalSpan", TimeSpan.FromSeconds(30.0)).TotalSeconds;
			this.FailedProbeThreshold = this.ReadAttribute("FailedProbeThreshold", 3);
			this.TrustAnySslCertificate = this.ReadAttribute("TrustAnySslCertificate", false);
			this.EnablePagedAlerts = this.ReadAttribute("EnablePagedAlerts", true);
			this.MonitoringIntervalSeconds = (int)this.ReadAttribute("MonitoringIntervalSpan", TimeSpan.FromSeconds(60.0)).TotalSeconds;
			this.MonitoringRecurrenceIntervalSeconds = (int)this.ReadAttribute("MonitoringRecurrenceIntervalSpan", TimeSpan.FromSeconds(0.0)).TotalSeconds;
			this.CreateRespondersForTest = this.ReadAttribute("CreateRespondersForTest", false);
			this.OfflineResponderFractionOverride = this.ReadAttribute("OfflineResponderFractionOverride", -1.0);
			this.AlertResponderCorrelationEnabled = (LocalEndpointManager.IsDataCenter && this.ReadAttribute("AlertResponderCorrelationEnabled", true));
			this.IsIISRecycleResponderEnabled = this.ReadAttribute("IISRecycleResponderEnabled", !ExEnvironment.IsTest);
			this.IsAlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", !ExEnvironment.IsTest);
			this.IsOfflineResponderEnabled = this.ReadAttribute("OfflineResponderEnabled", !ExEnvironment.IsTest);
			this.IsOfflineFailedAlertResponderEnabled = this.ReadAttribute("OfflineFailedAlertResponderEnabled", !ExEnvironment.IsTest);
			this.IsRebootResponderEnabled = this.ReadAttribute("RebootResponderEnabled", !ExEnvironment.IsTest);
			this.IsClearLsassCacheResponderEnabled = this.ReadAttribute("ClearLsassCacheResponderEnabled", !ExEnvironment.IsTest);
			if (LocalEndpointManager.IsDataCenter)
			{
				this.IsOfflineResponderEnabled = this.ReadAttribute("OfflineResponderEnabledInDC", false);
			}
			this.EnabledProbes = new Dictionary<HttpProtocol, bool>(10);
			for (int i = 0; i < 13; i++)
			{
				HttpProtocol httpProtocol = (HttpProtocol)i;
				string key = httpProtocol.ToString() + "ProbeEnabled";
				bool value = CafeDiscovery.IsProtocolAvailableInEnvironment(httpProtocol);
				bool flag;
				if (base.Definition.Attributes.ContainsKey(key) && bool.TryParse(base.Definition.Attributes[key], out flag))
				{
					value = flag;
				}
				this.EnabledProbes.Add(httpProtocol, value);
			}
			if (!LocalEndpointManager.IsDataCenter)
			{
				CafeProtocols.VirtualDirectories = from item in CafeDiscovery.AdSession.FindPaged<ADVirtualDirectory>(CafeDiscovery.AdSession.FindLocalServer().Id, QueryScope.SubTree, null, null, 0).ToList<ADVirtualDirectory>()
				where item.Identity.ToString().Contains("Default Web Site")
				select item;
			}
			this.breadcrumbs.Drop("CafeDiscovery.Configure: XML parameters read.");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DB64 File Offset: 0x0000BD64
		private void ReportResult(TracingContext traceContext)
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CafeTracer, traceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Cafe\\CafeDiscovery.cs", 413);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		private void CreateProbesAndMonitors(TracingContext traceContext, MailboxDatabaseInfo dbInfo)
		{
			foreach (ProtocolDescriptor protocolDescriptor in CafeProtocols.Protocols)
			{
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.breadcrumbs.Drop("CafeDiscovery.CreateProbesAndMonitors: cancelled by manager.");
					throw new OperationCanceledException(this.cancellationToken);
				}
				bool flag = this.EnabledProbes[protocolDescriptor.HttpProtocol];
				if (flag)
				{
					this.breadcrumbs.Drop("CafeDiscovery.AddProbesAndMonitors: adding {0}", new object[]
					{
						protocolDescriptor.HttpProtocol
					});
					this.AddWorkItemsForProtocol(protocolDescriptor, dbInfo, traceContext);
				}
				else
				{
					this.breadcrumbs.Drop("CafeDiscovery.AddProbesAndMonitors: skipped {0}", new object[]
					{
						protocolDescriptor.HttpProtocol
					});
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private void AddWorkItemsForProtocol(ProtocolDescriptor protocol, MailboxDatabaseInfo dbInfo, TracingContext traceContext)
		{
			ProbeIdentity probeIdentity = ProbeIdentity.Create(protocol.HealthSet, ProbeType.ProxyTest, null, protocol.AppPool);
			probeIdentity = this.AddProbe(probeIdentity, dbInfo, traceContext);
			this.AddMonitor(protocol, probeIdentity, traceContext);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		private ProbeIdentity AddProbe(ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo, TracingContext traceContext)
		{
			ProbeDefinition probeDefinition = CafeLocalProbe.CreateDefinition(dbInfo, probeIdentity, CafeDiscovery.ProbeEndPoint);
			probeDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			this.CopyAttributes(CafeDiscovery.ProbeAttributes, probeDefinition);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, traceContext);
			return probeDefinition;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		private void AddMonitor(ProtocolDescriptor protocol, ProbeIdentity probeIdentity, TracingContext traceContext)
		{
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.Component.Name, monitorIdentity.Component, this.FailedProbeThreshold, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = this.MonitoringRecurrenceIntervalSeconds;
			monitorDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.TargetResource = monitorIdentity.TargetResource;
			monitorDefinition.MonitoringIntervalSeconds = this.MonitoringIntervalSeconds;
			monitorDefinition.IsHaImpacting = this.ShouldCreateOfflineResponder(monitorIdentity.Component);
			monitorDefinition.MonitorStateTransitions = this.GetMonitorStateTransitions();
			monitorDefinition.ServicePriority = protocol.ProtocolPriority;
			monitorDefinition.ScenarioDescription = string.Format("Validate {0} protocal health on FE is not impacted by any issues", protocol.VirtualDirectory);
			this.CreateResponderChain(protocol, monitorDefinition, traceContext);
			if (this.AlertResponderCorrelationEnabled && protocol.HttpProtocol == HttpProtocol.EWS)
			{
				monitorDefinition.AllowCorrelationToMonitor = true;
			}
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		private MonitorStateTransition[] GetMonitorStateTransitions()
		{
			return new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Degraded1, this.Degraded1TransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, this.UnhealthyTransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy1, this.Unhealthy1TransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy2, this.Unhealthy2TransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSeconds)
			};
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000DE58 File Offset: 0x0000C058
		private void CreateResponderChain(ProtocolDescriptor protocol, MonitorIdentity monitorIdentity, TracingContext traceContext)
		{
			if (Utils.EnableResponderForCurrentEnvironment(this.IsClearLsassCacheResponderEnabled, this.CreateRespondersForTest) && VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.ClearLsassCacheResponder.Enabled)
			{
				ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("ClearLsassCacheResponder", null);
				ResponderDefinition responderDefinition = ClearLsassCacheResponder.CreateDefinition(responderIdentity.Name, monitorIdentity.GetAlertMask(), monitorIdentity.Component.ServerComponent, ServiceHealthStatus.Degraded);
				responderDefinition.AlertTypeId = monitorIdentity.Name;
				responderDefinition.ServiceName = monitorIdentity.Component.Name;
				responderDefinition.RecurrenceIntervalSeconds = this.ClearLsassCacheIntervalSeconds;
				responderDefinition.WaitIntervalSeconds = this.ClearLsassCacheIntervalSeconds;
				responderDefinition.TimeoutSeconds = this.ClearLsassCacheIntervalSeconds;
				responderDefinition.MaxRetryAttempts = 1;
				responderDefinition.Enabled = true;
				responderDefinition.TargetResource = responderIdentity.TargetResource;
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
			if (Utils.EnableResponderForCurrentEnvironment(this.IsIISRecycleResponderEnabled, this.CreateRespondersForTest))
			{
				ResponderIdentity responderIdentity2 = monitorIdentity.CreateResponderIdentity("RecycleAppPool", null);
				ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(responderIdentity2.Name, monitorIdentity.Name, monitorIdentity.TargetResource, ServiceHealthStatus.Degraded1, DumpMode.None, null, 15.0, 0, responderIdentity2.Component.Name, true, "Cafe");
				responderDefinition.AlertMask = monitorIdentity.GetAlertMask();
				responderDefinition.AlertTypeId = monitorIdentity.Name;
				responderDefinition.TargetResource = responderIdentity2.TargetResource;
				responderDefinition.RecurrenceIntervalSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.WaitIntervalSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.TimeoutSeconds = this.IISRecycleRetryIntervalSeconds;
				responderDefinition.MaxRetryAttempts = this.IISRecycleRetryCount;
				responderDefinition.Enabled = true;
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
			bool flag = this.ShouldCreateOfflineResponder(monitorIdentity.Component);
			if (flag)
			{
				ResponderIdentity responderIdentity3 = monitorIdentity.CreateResponderIdentity("Offline", null);
				ResponderDefinition responderDefinition = CafeOfflineResponder.CreateDefinition(responderIdentity3.Name, monitorIdentity.Name, monitorIdentity.Component.ServerComponent, ServiceHealthStatus.Unhealthy, responderIdentity3.Component.Name, this.OfflineResponderFractionOverride, "", "Datacenter", "F5AvailabilityData", "MachineOut");
				responderDefinition.TargetResource = responderIdentity3.TargetResource;
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
			bool flag2 = Utils.EnableResponderForCurrentEnvironment(this.IsOfflineFailedAlertResponderEnabled, this.CreateRespondersForTest) && (!LocalEndpointManager.IsDataCenter || CafeDiscovery.IsDatacenterP0Protocol(monitorIdentity.Component));
			if (flag2)
			{
				string alertMessageBody = CafeDiscovery.GetAlertMessageBody(Strings.CafeOfflineFailedEscalationRecoveryDetails(monitorIdentity.TargetResource), CafeDiscovery.CafeArrayName);
				ResponderIdentity responderIdentity4 = monitorIdentity.CreateResponderIdentity("OfflineFailedEscalate", null);
				ResponderDefinition responderDefinition = EscalateComponentStateResponder.CreateDefinition(responderIdentity4.Name, responderIdentity4.Component.Name, monitorIdentity.Name, monitorIdentity.GetAlertMask(), responderIdentity4.TargetResource, ServiceHealthStatus.Unhealthy1, monitorIdentity.Component.Service, monitorIdentity.Component.EscalationTeam, Strings.CafeEscalationSubjectUnhealthy, alertMessageBody, monitorIdentity.Component.ServerComponent, CafeUtils.TriggerConfig.ExecuteIfOnline, this.ProbeRecurrenceIntervalSeconds, "httpproxy\\" + protocol.LogFolderName.ToLower(), responderIdentity4.TargetResource, typeof(CafeExtraDetailsParser), true, this.EnablePagedAlerts ? NotificationServiceClass.Urgent : NotificationServiceClass.UrgentInTraining, 14400);
				if (this.AlertResponderCorrelationEnabled && protocol.DeferAlertOnCafeWideFailure)
				{
					CafeUtils.ConfigureResponderForCafeFailureCorrelation(responderDefinition);
				}
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
			bool flag3 = Utils.EnableResponderForCurrentEnvironment(this.IsRebootResponderEnabled, this.CreateRespondersForTest) && CafeDiscovery.IsDatacenterP0Protocol(monitorIdentity.Component);
			if (flag3)
			{
				ResponderIdentity responderIdentity5 = monitorIdentity.CreateResponderIdentity("RebootOfflineServer", null);
				ResponderDefinition responderDefinition = RebootServerComponentStateResponder.CreateDefinition(responderIdentity5.Name, responderIdentity5.Component.Name, monitorIdentity.Name, ServiceHealthStatus.Unhealthy2, monitorIdentity.Component.ServerComponent, CafeUtils.TriggerConfig.ExecuteIfOffline);
				responderDefinition.TargetResource = responderIdentity5.TargetResource;
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
			if (Utils.EnableResponderForCurrentEnvironment(this.IsAlertResponderEnabled, this.CreateRespondersForTest))
			{
				string alertMessageBody2 = CafeDiscovery.GetAlertMessageBody(Strings.CafeEscalationRecoveryDetails(monitorIdentity.TargetResource), CafeDiscovery.CafeArrayName);
				NotificationServiceClass notificationServiceClass = NotificationServiceClass.UrgentInTraining;
				if (this.EnablePagedAlerts)
				{
					notificationServiceClass = (flag2 ? NotificationServiceClass.Scheduled : NotificationServiceClass.Urgent);
				}
				CafeUtils.TriggerConfig triggerConfig = flag ? CafeUtils.TriggerConfig.ExecuteIfOffline : CafeUtils.TriggerConfig.ExecuteIfOnline;
				ResponderIdentity responderIdentity6 = monitorIdentity.CreateResponderIdentity("Escalate", null);
				ResponderDefinition responderDefinition = EscalateComponentStateResponder.CreateDefinition(responderIdentity6.Name, responderIdentity6.Component.Name, monitorIdentity.Name, monitorIdentity.GetAlertMask(), responderIdentity6.TargetResource, ServiceHealthStatus.Unrecoverable, monitorIdentity.Component.Service, monitorIdentity.Component.EscalationTeam, Strings.CafeEscalationSubjectUnhealthy, alertMessageBody2, monitorIdentity.Component.ServerComponent, triggerConfig, this.ProbeRecurrenceIntervalSeconds, "httpproxy\\" + protocol.LogFolderName.ToLower(), responderIdentity6.TargetResource, typeof(CafeExtraDetailsParser), true, notificationServiceClass, 14400);
				if (this.AlertResponderCorrelationEnabled && protocol.DeferAlertOnCafeWideFailure)
				{
					CafeUtils.ConfigureResponderForCafeFailureCorrelation(responderDefinition);
				}
				this.breadcrumbs.Drop("+" + responderDefinition.Name);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, traceContext);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E3BA File Offset: 0x0000C5BA
		private bool ShouldCreateOfflineResponder(Component component)
		{
			return Utils.EnableResponderForCurrentEnvironment(this.IsOfflineResponderEnabled, this.CreateRespondersForTest) && (!LocalEndpointManager.IsDataCenter || CafeDiscovery.IsDatacenterP0Protocol(component));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		internal static bool IsProtocolAvailableInEnvironment(HttpProtocol protocol)
		{
			switch (protocol)
			{
			case HttpProtocol.PowerShellLiveID:
			case HttpProtocol.Reporting:
			case HttpProtocol.XRop:
				return LocalEndpointManager.IsDataCenter;
			}
			return true;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E411 File Offset: 0x0000C611
		internal static bool IsDatacenterP0Protocol(Component component)
		{
			return component.ServerComponent == ServerComponentEnum.HttpProxyAvailabilityGroup;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E41D File Offset: 0x0000C61D
		private static string GetAlertMessageBody(string recoveryDetails, string cafeArrayName)
		{
			if (LocalEndpointManager.IsDataCenter)
			{
				return Strings.CafeEscalationMessageUnhealthyForDC(cafeArrayName);
			}
			return Strings.CafeEscalationMessageUnhealthy(recoveryDetails, cafeArrayName);
		}

		// Token: 0x04000128 RID: 296
		public const int DefaultProbeTimeoutSeconds = 15;

		// Token: 0x04000129 RID: 297
		public const int DefaultProbeRecurrenceIntervalSeconds = 15;

		// Token: 0x0400012A RID: 298
		public const int DefaultMonitoringIntervalSeconds = 60;

		// Token: 0x0400012B RID: 299
		public const int DefaultMonitoringRecurrenceIntervalSeconds = 0;

		// Token: 0x0400012C RID: 300
		public const int DefaultFailedProbeThreshold = 3;

		// Token: 0x0400012D RID: 301
		private const string ProbeName = "CafeLocalProbe";

		// Token: 0x0400012E RID: 302
		private const string TargetResource = "ClientAccess";

		// Token: 0x0400012F RID: 303
		public static readonly string ProbeEndPoint = Uri.UriSchemeHttps + "://localhost/";

		// Token: 0x04000130 RID: 304
		internal static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000131 RID: 305
		internal static readonly string ProbeTypeName = typeof(CafeLocalProbe).FullName;

		// Token: 0x04000132 RID: 306
		internal static readonly string RecycleResponderTypeName = typeof(ResetIISAppPoolResponder).FullName;

		// Token: 0x04000133 RID: 307
		private static readonly string[] ProbeAttributes = new string[]
		{
			"TrustAnySslCertificate",
			"Verbose",
			"HttpRequestTimeoutSpan"
		};

		// Token: 0x04000134 RID: 308
		private Breadcrumbs breadcrumbs;

		// Token: 0x04000135 RID: 309
		private CancellationToken cancellationToken;

		// Token: 0x04000136 RID: 310
		private static string cafeArrayName;

		// Token: 0x04000137 RID: 311
		private static ITopologyConfigurationSession adSession;
	}
}
