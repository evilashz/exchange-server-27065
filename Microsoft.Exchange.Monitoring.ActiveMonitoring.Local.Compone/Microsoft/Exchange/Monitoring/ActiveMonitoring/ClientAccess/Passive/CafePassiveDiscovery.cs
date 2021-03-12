using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess.Passive
{
	// Token: 0x02000042 RID: 66
	public sealed class CafePassiveDiscovery : DiscoveryWorkItem
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000FD05 File Offset: 0x0000DF05
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.CafeTracer;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			this.breadcrumbs = new Breadcrumbs(1024, this.traceContext);
			try
			{
				if (!LocalEndpointManager.IsDataCenter)
				{
					this.breadcrumbs.Drop("CreateWorkTasks: Datacenter only.");
				}
				else if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					this.breadcrumbs.Drop("CreateWorkTasks: Cafe role is not present on this server.");
				}
				else
				{
					this.Configure();
					if (this.probesEnabled[PassiveMonitorType.CASRoutingLatency])
					{
						this.CreateCASRoutingLatencyDefinitions();
					}
					if (this.probesEnabled[PassiveMonitorType.CASRoutingFailure])
					{
						this.CreateCASRoutingFailureDefinitions();
					}
					if (this.probesEnabled[PassiveMonitorType.ThreadCount])
					{
						this.CreateThreadPoolDefinitions();
					}
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		private void CreateCASRoutingLatencyDefinitions()
		{
			this.breadcrumbs.Drop("Creating CAS Routing Latency work definitions");
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "CASRoutingLatencyTrigger_Error", null);
			MonitorDefinition monitorDefinition = this.CreateMonitorForEDS("ClientAccessRoutingLatencyMonitor", sampleMask);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate CAFE health is not impacted by CAS routing latency issues";
			this.CreateResponderChainForEDS(monitorDefinition, Strings.CASRoutingLatencyEscalationSubject, Strings.CASRoutingLatencyEscalationBody, PerfCounterHelper.UnitMs, NotificationServiceClass.UrgentInTraining);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000FE50 File Offset: 0x0000E050
		private void CreateCASRoutingFailureDefinitions()
		{
			this.breadcrumbs.Drop("Creating CAS Routing Failure work definitions");
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "CASRoutingFailureTrigger_Error", null);
			MonitorDefinition monitorDefinition = this.CreateMonitorForEDS("ClientAccessRoutingFailureMonitor", sampleMask);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate CAFE health is not impacted by CAS routing failure issues";
			this.CreateResponderChainForEDS(monitorDefinition, Strings.CASRoutingFailureEscalationSubject, Strings.CASRoutingFailureEscalationBody, PerfCounterHelper.UnitPercent, NotificationServiceClass.Urgent);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		private void CreateThreadPoolDefinitions()
		{
			this.breadcrumbs.Drop("Creating ThreadPool work definitions");
			int maxWorkerThreads = CafePassiveDiscovery.GetMaxWorkerThreads();
			int num = (int)((double)(maxWorkerThreads * Environment.ProcessorCount) * this.ThreadCountThresholdFraction);
			this.breadcrumbs.Drop("MaxWorkerThreads = {0}; Threshold = {1}", new object[]
			{
				maxWorkerThreads,
				num
			});
			foreach (ProtocolDescriptor protocolDescriptor in CafeProtocols.Protocols)
			{
				if (CafeDiscovery.IsProtocolAvailableInEnvironment(protocolDescriptor.HttpProtocol))
				{
					ProbeIdentity probeIdentity = ProbeIdentity.Create(protocolDescriptor.HealthSet, ProbeType.ProxyTest, "ThreadCount", protocolDescriptor.AppPool);
					probeIdentity = this.CreateProbeDefinition(probeIdentity, this.ThreadCountProbeRecurrenceIntervalSeconds, num);
					MonitorDefinition monitorDefinition = this.CreateMonitorForConsecutiveProbeFailures(probeIdentity, (this.ThreadCountProbeFailureCount + 1) * this.ThreadCountProbeRecurrenceIntervalSeconds, this.ThreadCountProbeFailureCount);
					monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
					{
						new MonitorStateTransition(ServiceHealthStatus.Unhealthy, this.ThreadCountUnheahlthyTransitionSeconds),
						new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.ThreadCountUnrecoverableTransitionSeconds)
					};
					monitorDefinition.ServicePriority = 0;
					monitorDefinition.ScenarioDescription = "Validate Cafe health is not impacted by threadpool issues";
					this.CreateResponderChain(monitorDefinition, Strings.CafeThreadCountSubjectUnhealthy(protocolDescriptor.AppPool), Strings.CafeThreadCountMessageUnhealthy(protocolDescriptor.AppPool, Math.Round(this.ThreadCountThresholdFraction * 100.0, 2).ToString(), maxWorkerThreads.ToString()), NotificationServiceClass.UrgentInTraining, ServiceHealthStatus.Unrecoverable);
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00010064 File Offset: 0x0000E264
		private ProbeIdentity CreateProbeDefinition(ProbeIdentity probeIdentity, int recurrenceIntervalSeconds, int threshold)
		{
			ProbeDefinition probeDefinition = ThreadCountLocalProbe.CreateDefinition(probeIdentity, threshold);
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = 30;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, this.traceContext);
			return probeDefinition;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000100A4 File Offset: 0x0000E2A4
		private MonitorDefinition CreateMonitorForConsecutiveProbeFailures(ProbeIdentity probeIdentity, int monitoringInterval, int numSamples)
		{
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.Component.Name, monitorIdentity.Component, numSamples, true, monitoringInterval);
			monitorDefinition.TargetResource = probeIdentity.TargetResource;
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			return monitorDefinition;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000100F4 File Offset: 0x0000E2F4
		private MonitorDefinition CreateMonitorForEDS(string monitorName, string sampleMask)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, sampleMask, ExchangeComponent.Cafe.Name, ExchangeComponent.Cafe, 1, true, this.EdsMonitoringIntervalSeconds);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			return monitorDefinition;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00010128 File Offset: 0x0000E328
		private void CreateResponderChainForEDS(MonitorDefinition monitorDefinition, string escalationSubject, string escalationBody, string counterUnits, NotificationServiceClass alertClass)
		{
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			if (Utils.EnableResponderForCurrentEnvironment(this.AlertResponderEnabled, this.CreateRespondersForTest))
			{
				MonitorIdentity monitorIdentity = monitorDefinition;
				ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Escalate", null);
				this.breadcrumbs.Drop("Responder: {0}", new object[]
				{
					responderIdentity.Name
				});
				ResponderDefinition definition = PerfCounterEscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.Component.Name, monitorIdentity.Name, monitorIdentity.GetAlertMask(), responderIdentity.TargetResource, ServiceHealthStatus.Unhealthy, monitorIdentity.Component.EscalationTeam, escalationSubject, escalationBody, counterUnits, true, alertClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59");
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, this.traceContext);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000101F4 File Offset: 0x0000E3F4
		private void CreateResponderChain(MonitorIdentity monitorIdentity, string escalationSubject, string escalationBody, NotificationServiceClass alertClass, ServiceHealthStatus healthStateForEscalation)
		{
			if (Utils.EnableResponderForCurrentEnvironment(this.AlertResponderEnabled, this.CreateRespondersForTest))
			{
				ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Escalate", null);
				this.breadcrumbs.Drop("Responder: {0}", new object[]
				{
					responderIdentity.Name
				});
				ResponderDefinition definition = EscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.Component.Name, monitorIdentity.Name, monitorIdentity.GetAlertMask(), responderIdentity.TargetResource, healthStateForEscalation, monitorIdentity.Component.EscalationTeam, escalationSubject, escalationBody, true, alertClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, this.traceContext);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000102A0 File Offset: 0x0000E4A0
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CafeTracer, this.traceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Cafe\\CafePassiveDiscovery.cs", 383);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000102E8 File Offset: 0x0000E4E8
		private void Configure()
		{
			this.EdsMonitoringIntervalSeconds = (int)this.ReadAttribute("EdsMonitoringSpan", CafePassiveDiscovery.DefaultValues.EdsMonitoringInterval).TotalSeconds;
			this.ThreadCountProbeFailureCount = this.ReadAttribute("ThreadCountProbeFailureCount", 5);
			this.ThreadCountThresholdFraction = this.ReadAttribute("ThreadCountThresholdFractionOfMax", 0.9);
			this.ThreadCountProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ThreadCountProbeRecurrenceInterval", CafePassiveDiscovery.DefaultValues.ThreadCountProbeRecurrenceIntervalSeconds).TotalSeconds;
			this.ThreadCountUnheahlthyTransitionSeconds = (int)this.ReadAttribute("ThreadCountUnheahlthyTransition", CafePassiveDiscovery.DefaultValues.ThreadCountUnheahlthyTransition).TotalSeconds;
			this.ThreadCountUnrecoverableTransitionSeconds = (int)this.ReadAttribute("ThreadCountUnrecoverableTransition", CafePassiveDiscovery.DefaultValues.ThreadCountUnrecoverableTransition).TotalSeconds;
			foreach (object obj in Enum.GetValues(typeof(PassiveMonitorType)))
			{
				PassiveMonitorType passiveMonitorType = (PassiveMonitorType)obj;
				this.probesEnabled[passiveMonitorType] = this.ReadAttribute(passiveMonitorType + "MonitorEnabled", true);
			}
			this.CreateRespondersForTest = this.ReadAttribute("CreateRespondersForTest", false);
			this.AlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", !ExEnvironment.IsTest);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00010444 File Offset: 0x0000E644
		private static int GetMaxWorkerThreads()
		{
			Configuration configuration = WebConfigurationManager.OpenMachineConfiguration();
			ProcessModelSection processModelSection = (ProcessModelSection)configuration.GetSection("system.web/processModel");
			return processModelSection.MaxWorkerThreads;
		}

		// Token: 0x04000178 RID: 376
		private const string CASRoutingLatencyName = "ClientAccessRoutingLatencyMonitor";

		// Token: 0x04000179 RID: 377
		private const string CASRoutingLatencyTriggerErrorMask = "CASRoutingLatencyTrigger_Error";

		// Token: 0x0400017A RID: 378
		private const string CASRoutingFailureName = "ClientAccessRoutingFailureMonitor";

		// Token: 0x0400017B RID: 379
		private const string CASRoutingFailureTriggerErrorMask = "CASRoutingFailureTrigger_Error";

		// Token: 0x0400017C RID: 380
		private Breadcrumbs breadcrumbs;

		// Token: 0x0400017D RID: 381
		private Dictionary<PassiveMonitorType, bool> probesEnabled = new Dictionary<PassiveMonitorType, bool>();

		// Token: 0x0400017E RID: 382
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400017F RID: 383
		private int ThreadCountProbeRecurrenceIntervalSeconds;

		// Token: 0x04000180 RID: 384
		private int ThreadCountUnheahlthyTransitionSeconds;

		// Token: 0x04000181 RID: 385
		private int ThreadCountUnrecoverableTransitionSeconds;

		// Token: 0x04000182 RID: 386
		private int ThreadCountProbeFailureCount;

		// Token: 0x04000183 RID: 387
		private double ThreadCountThresholdFraction;

		// Token: 0x04000184 RID: 388
		private int EdsMonitoringIntervalSeconds;

		// Token: 0x04000185 RID: 389
		private bool CreateRespondersForTest;

		// Token: 0x04000186 RID: 390
		private bool AlertResponderEnabled;

		// Token: 0x02000043 RID: 67
		private class DefaultValues
		{
			// Token: 0x04000187 RID: 391
			public const int ThreadCountProbeFailureCount = 5;

			// Token: 0x04000188 RID: 392
			public const double ThreadCountThresholdFraction = 0.9;

			// Token: 0x04000189 RID: 393
			public static readonly TimeSpan EdsMonitoringInterval = TimeSpan.FromMinutes(15.0);

			// Token: 0x0400018A RID: 394
			public static readonly TimeSpan ThreadCountProbeRecurrenceIntervalSeconds = TimeSpan.FromSeconds(60.0);

			// Token: 0x0400018B RID: 395
			public static readonly TimeSpan ThreadCountUnheahlthyTransition = TimeSpan.FromSeconds(0.0);

			// Token: 0x0400018C RID: 396
			public static readonly TimeSpan ThreadCountUnrecoverableTransition = TimeSpan.FromMinutes(15.0);
		}
	}
}
