using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x0200024B RID: 587
	public sealed class OAuthPassiveDiscovery : DiscoveryWorkItem
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0006DC5A File Offset: 0x0006BE5A
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.CafeTracer;
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0006DC64 File Offset: 0x0006BE64
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
					if (this.probesEnabled[PassiveMonitorType.OAuthRequestFailure])
					{
						this.CreateOAuthRequestFailureDefinitions();
					}
					if (this.probesEnabled[PassiveMonitorType.OAuthAcsTimeout])
					{
						this.CreateOAuthAcsTimeoutDefinitions();
					}
					if (this.probesEnabled[PassiveMonitorType.OAuthExpiredToken])
					{
						this.CreateOAuthExpiredTokenDefinitions();
					}
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0006DD20 File Offset: 0x0006BF20
		private void CreateOAuthRequestFailureDefinitions()
		{
			this.breadcrumbs.Drop("Creating OAuth Requests Failure work definitions");
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "OAuthRequestFailureTrigger_Error", null);
			MonitorDefinition monitorDefinition = this.CreateMonitorForEDS("EWSOAuthRequestFailureMonitor", sampleMask);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate OAuth health is not impacted by authentication request failures";
			this.CreateResponderChainForEDS(monitorDefinition, Strings.OAuthRequestFailureEscalationSubject, Strings.OAuthRequestFailureEscalationBody, PerfCounterHelper.UnitPercent, NotificationServiceClass.Urgent);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0006DDA8 File Offset: 0x0006BFA8
		private void CreateOAuthAcsTimeoutDefinitions()
		{
			this.breadcrumbs.Drop("Creating OAuth Acs timeout work definitions");
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "OAuthAcsTimeoutTrigger_Error", null);
			MonitorDefinition monitorDefinition = this.CreateMonitorForEDS("EWSOAuthAcsTimeoutMonitor", sampleMask);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate OAuth health is not impacted by authentication request failures";
			this.CreateResponderChainForEDS(monitorDefinition, Strings.OAuthRequestFailureEscalationSubject, Strings.OAuthRequestFailureEscalationBody, PerfCounterHelper.UnitPercent, NotificationServiceClass.Urgent);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0006DE30 File Offset: 0x0006C030
		private void CreateOAuthExpiredTokenDefinitions()
		{
			this.breadcrumbs.Drop("Creating OAuth Requests Failure work definitions");
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "OAuthPassiveMonitoringExceptionAboveThreshold", null);
			MonitorDefinition monitorDefinition = this.CreateMonitorForEDS("EWSOAuthExpiredTokenMonitor", sampleMask);
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate OAuth health is not impacted by authentication request failures";
			this.CreateResponderChainForEDS(monitorDefinition, "OAuth Expired Token passive monitor exceeded the threshold", "{Probe.ExtensionXml}", PerfCounterHelper.UnitPercent, NotificationServiceClass.Urgent);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0006DEAC File Offset: 0x0006C0AC
		private MonitorDefinition CreateMonitorForEDS(string monitorName, string sampleMask)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, sampleMask, ExchangeComponent.Ews.Name, ExchangeComponent.Ews, 1, true, this.EdsMonitoringIntervalSeconds);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate OAuth health is not impacted by authentication request failures";
			return monitorDefinition;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
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

		// Token: 0x06001092 RID: 4242 RVA: 0x0006DFC0 File Offset: 0x0006C1C0
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

		// Token: 0x06001093 RID: 4243 RVA: 0x0006E06C File Offset: 0x0006C26C
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CafeTracer, this.traceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPassiveDiscovery.cs", 312);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0006E0B4 File Offset: 0x0006C2B4
		private void Configure()
		{
			this.EdsMonitoringIntervalSeconds = (int)this.ReadAttribute("EdsMonitoringSpan", OAuthPassiveDiscovery.DefaultValues.EdsMonitoringInterval).TotalSeconds;
			foreach (object obj in Enum.GetValues(typeof(PassiveMonitorType)))
			{
				PassiveMonitorType passiveMonitorType = (PassiveMonitorType)obj;
				this.probesEnabled[passiveMonitorType] = this.ReadAttribute(passiveMonitorType + "MonitorEnabled", true);
			}
			this.CreateRespondersForTest = this.ReadAttribute("CreateRespondersForTest", true);
			this.AlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", !ExEnvironment.IsTest);
		}

		// Token: 0x04000C58 RID: 3160
		private const string OAuthRequestFailureName = "EWSOAuthRequestFailureMonitor";

		// Token: 0x04000C59 RID: 3161
		private const string OAuthRequestFailureTriggerErrorMask = "OAuthRequestFailureTrigger_Error";

		// Token: 0x04000C5A RID: 3162
		private const string OAuthAcsTimeoutName = "EWSOAuthAcsTimeoutMonitor";

		// Token: 0x04000C5B RID: 3163
		private const string OAuthAcsTimeoutTriggerErrorMask = "OAuthAcsTimeoutTrigger_Error";

		// Token: 0x04000C5C RID: 3164
		private const string OAuthExpiredTokenName = "EWSOAuthExpiredTokenMonitor";

		// Token: 0x04000C5D RID: 3165
		private const string OAuthExpiredTokenTriggerErrorMask = "OAuthPassiveMonitoringExceptionAboveThreshold";

		// Token: 0x04000C5E RID: 3166
		private Breadcrumbs breadcrumbs;

		// Token: 0x04000C5F RID: 3167
		private Dictionary<PassiveMonitorType, bool> probesEnabled = new Dictionary<PassiveMonitorType, bool>();

		// Token: 0x04000C60 RID: 3168
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x04000C61 RID: 3169
		private int EdsMonitoringIntervalSeconds;

		// Token: 0x04000C62 RID: 3170
		private bool CreateRespondersForTest;

		// Token: 0x04000C63 RID: 3171
		private bool AlertResponderEnabled;

		// Token: 0x0200024C RID: 588
		private class DefaultValues
		{
			// Token: 0x04000C64 RID: 3172
			public static readonly TimeSpan EdsMonitoringInterval = TimeSpan.FromMinutes(15.0);
		}
	}
}
