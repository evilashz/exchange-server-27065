using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment
{
	// Token: 0x02000030 RID: 48
	internal sealed class DraProtectorMonitoringContext : MonitoringContextBase
	{
		// Token: 0x06000180 RID: 384 RVA: 0x0000BE60 File Offset: 0x0000A060
		public DraProtectorMonitoringContext(IMaintenanceWorkBroker broker, TracingContext traceContext) : base(broker, traceContext)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public override void CreateContext()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			ProbeDefinition probeDefinition = DraProtectorProbe.CreateDefinition("DraProtectorProbe", 600);
			base.EnrollWorkItem<ProbeDefinition>(probeDefinition);
			string name = "DraProtectorMonitor";
			string sampleMask = probeDefinition.ConstructWorkItemResultName();
			string name2 = ExchangeComponent.BitlockerDeployment.Name;
			Component bitlockerDeployment = ExchangeComponent.BitlockerDeployment;
			int numberOfFailures = 1;
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(name, sampleMask, name2, bitlockerDeployment, 1200, 600, numberOfFailures, true);
			monitorDefinition.TargetResource = Environment.MachineName;
			MonitorStateResponderTuple[] array = new MonitorStateResponderTuple[1];
			MonitorStateResponderTuple[] array2 = array;
			int num = 0;
			MonitorStateResponderTuple monitorStateResponderTuple = default(MonitorStateResponderTuple);
			monitorStateResponderTuple.MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 18000);
			string name3 = "BitlockerDraProtectorEscalate";
			string name4 = ExchangeComponent.BitlockerDeployment.Name;
			string name5 = monitorDefinition.Name;
			string alertMask = monitorDefinition.ConstructWorkItemResultName();
			string targetResource = monitorDefinition.TargetResource;
			ServiceHealthStatus targetHealthState = ServiceHealthStatus.Unhealthy;
			string escalationTeam = "High Availability";
			string escalationSubjectUnhealthy = Strings.LocalMachineDriveNotProtectedWithDraEscalationSubject(Environment.MachineName);
			string escalationMessageUnhealthy = Strings.LocalMachineDriveNotProtectedWithDraEscalationMessage(BitlockerDeploymentUtility.GetVolumesNotProtectedWithDra(), Environment.MachineName);
			int minimumSecondsBetweenEscalates = 10800;
			monitorStateResponderTuple.Responder = EscalateResponder.CreateDefinition(name3, name4, name5, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, minimumSecondsBetweenEscalates, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			array2[num] = monitorStateResponderTuple;
			base.AddChainedResponders(ref monitorDefinition, array);
		}
	}
}
