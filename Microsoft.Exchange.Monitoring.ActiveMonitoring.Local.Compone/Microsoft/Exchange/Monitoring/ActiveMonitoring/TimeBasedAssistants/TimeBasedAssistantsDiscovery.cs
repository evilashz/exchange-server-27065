using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x0200052A RID: 1322
	public sealed class TimeBasedAssistantsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600208E RID: 8334 RVA: 0x000C68EC File Offset: 0x000C4AEC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint == null || !LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TimeBasedAssistantsTracer, base.TraceContext, "TimeBasedAssistantsDiscovery.DoWork: Mailbox role is not installed.No need to create TimeBasedAssistants related probes.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TimeBasedAssistants\\TimeBasedAssistantsDiscovery.cs", 50);
				return;
			}
			if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(TimeBasedAssistantsDiscovery.Server.Name))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TimeBasedAssistantsTracer, base.TraceContext, "TimeBasedAssistantsDiscovery.DoWork: Server is in maintenance mode.No need to create TimeBasedAssistants related probes.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TimeBasedAssistants\\TimeBasedAssistantsDiscovery.cs", 62);
				return;
			}
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			if (!snapshot.MailboxAssistants.TimeBasedAssistantsMonitoring.Enabled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TimeBasedAssistantsTracer, base.TraceContext, "TimeBasedAssistantsDiscovery.DoWork: Alerts are in training and should be running on SDF only (OM:1050471).No need to create TimeBasedAssistants related probes.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TimeBasedAssistants\\TimeBasedAssistantsDiscovery.cs", 76);
				return;
			}
			this.CreateAssistantsOutOfSlaContext(TbaOutOfSlaAlertType.Urgent);
			this.CreateAssistantsOutOfSlaContext(TbaOutOfSlaAlertType.Scheduled);
			this.CreateAssistantsInfrastructureValidationContext();
			this.CreateAssistantsNotRunningToCompletionContext();
			this.CreateAssistantsActiveDatabaseContext();
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000C69D7 File Offset: 0x000C4BD7
		private void CreateAssistantsOutOfSlaContext(TbaOutOfSlaAlertType alertType)
		{
			if (ExEnvironment.IsTest)
			{
				TimeBasedAssistantsOutOfSlaMonitor.ConfigureForTest();
			}
			this.CreateOutOfSlaContext(alertType);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000C69EC File Offset: 0x000C4BEC
		private void CreateAssistantsInfrastructureValidationContext()
		{
			if (ExEnvironment.IsTest)
			{
				InfrastructureValidationDiscovery.ConfigureForTest();
			}
			this.CreateInfrastructureValidationContext();
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000C6A00 File Offset: 0x000C4C00
		private void CreateAssistantsNotRunningToCompletionContext()
		{
			if (ExEnvironment.IsTest)
			{
				TimeBasedAssistantsNotRunningToCompletionDiscovery.ConfigureForTest();
			}
			this.CreateNotRunningToCompletionContext();
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000C6A14 File Offset: 0x000C4C14
		private void CreateAssistantsActiveDatabaseContext()
		{
			if (ExEnvironment.IsTest)
			{
				TimeBasedAssistantsActiveDatabaseDiscovery.ConfigureForTest();
			}
			this.CreateActiveDatabaseContext();
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000C6A28 File Offset: 0x000C4C28
		private void CreateOutOfSlaContext(TbaOutOfSlaAlertType alertType)
		{
			ProbeDefinition probeDefinition = TimeBasedAssistantsOutOfSlaMonitor.CreateOutOfSlaProbeDefinition(base.TraceContext, alertType);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = TimeBasedAssistantsOutOfSlaMonitor.CreateOutOfSlaMonitorDefinition(probeDefinition.Name, base.TraceContext, alertType);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = TimeBasedAssistantsOutOfSlaMonitor.CreateOutOfSlaEscalateResponderDefinition(monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), alertType);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000C6AA4 File Offset: 0x000C4CA4
		private void CreateInfrastructureValidationContext()
		{
			ProbeDefinition probeDefinition = InfrastructureValidationDiscovery.CreateInfrastructureValidationProbeDefinition(base.TraceContext);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = InfrastructureValidationDiscovery.CreateInfrastructureValidationMonitorDefinition(probeDefinition.Name, base.TraceContext);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = InfrastructureValidationDiscovery.CreateInfrastructureValidationEscalateResponderDefinition(monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName());
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000C6B1C File Offset: 0x000C4D1C
		private void CreateNotRunningToCompletionContext()
		{
			ProbeDefinition probeDefinition = TimeBasedAssistantsNotRunningToCompletionDiscovery.CreateNotRunningToCompletionProbeDefinition(base.TraceContext);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = TimeBasedAssistantsNotRunningToCompletionDiscovery.CreateNotRunningToCompletionMonitorDefinition(probeDefinition.Name, base.TraceContext);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = TimeBasedAssistantsNotRunningToCompletionDiscovery.CreateNotRunningToCompletionEscalateResponderDefinition(monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName());
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000C6B94 File Offset: 0x000C4D94
		private void CreateActiveDatabaseContext()
		{
			ProbeDefinition probeDefinition = TimeBasedAssistantsActiveDatabaseDiscovery.CreateActiveDatabaseProbeDefinition(base.TraceContext);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = TimeBasedAssistantsActiveDatabaseDiscovery.CreateActiveDatabaseMonitorDefinition(probeDefinition.Name, base.TraceContext);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = TimeBasedAssistantsActiveDatabaseDiscovery.CreateActiveDatabaseEscalateResponderDefinition(monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName());
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x040017EA RID: 6122
		private const string NoNeedToCreateProbesString = "No need to create TimeBasedAssistants related probes.";

		// Token: 0x040017EB RID: 6123
		private static readonly Server Server = LocalServer.GetServer();
	}
}
