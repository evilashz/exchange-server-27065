using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Transport
{
	// Token: 0x020004EB RID: 1259
	internal class TestMailflow : IWorkItem
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x000BEBE4 File Offset: 0x000BCDE4
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeProbe(discoveryDefinition, broker, traceContext);
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000BEC04 File Offset: 0x000BCE04
		private void InitializeProbe(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			ProbeDefinition definition = WorkDefinitionHelper.CreateProbeDefinition("XPremiseMailflowProbe", typeof(CrossPremiseMailFlowProbe), null, TestMailflow.Component.Name, TimeSpan.FromSeconds(600.0), false);
			broker.AddWorkDefinition<ProbeDefinition>(definition, traceContext);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000BEC4C File Offset: 0x000BCE4C
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			MonitorDefinition definition = OverallXFailuresMonitor.CreateDefinition("XPremiseMailflowMonitor", "XPremiseMailflowProbe", TestMailflow.Component.Name, TestMailflow.Component, 1230, 0, 1, true);
			broker.AddWorkDefinition<MonitorDefinition>(definition, traceContext);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000BEC8C File Offset: 0x000BCE8C
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			ResponderDefinition definition = EscalateResponder.CreateDefinition("XPremiseMailflowEscalateResponder", TestMailflow.Component.Name, "XPremiseMailflowMonitor", "XPremiseMailflowMonitor", null, ServiceHealthStatus.None, TestMailflow.Component.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.CrossPremiseMailflowEscalationMessage, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x040016CB RID: 5835
		private const int ProbeIntervalSeconds = 600;

		// Token: 0x040016CC RID: 5836
		private const int AlertingThreshold = 1;

		// Token: 0x040016CD RID: 5837
		private const string ProbeName = "XPremiseMailflowProbe";

		// Token: 0x040016CE RID: 5838
		private const string MonitorName = "XPremiseMailflowMonitor";

		// Token: 0x040016CF RID: 5839
		private const string ResponderName = "XPremiseMailflowEscalateResponder";

		// Token: 0x040016D0 RID: 5840
		private static Component Component = ExchangeComponent.Transport;
	}
}
