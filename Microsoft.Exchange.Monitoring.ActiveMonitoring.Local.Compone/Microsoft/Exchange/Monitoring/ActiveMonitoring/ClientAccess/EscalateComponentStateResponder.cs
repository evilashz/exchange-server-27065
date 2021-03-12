using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x0200004F RID: 79
	internal class EscalateComponentStateResponder : ExtraDetailsEscalateResponder
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00011C04 File Offset: 0x0000FE04
		internal static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationService, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, ServerComponentEnum serverComponentToVerifyState, CafeUtils.TriggerConfig triggerConfig, int probeIntervalSeconds, string logFolderRelativePath, string appPoolName, Type probeMonitorResultParserType, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400)
		{
			ResponderDefinition responderDefinition = ExtraDetailsEscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationService, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, probeIntervalSeconds, logFolderRelativePath, appPoolName, probeMonitorResultParserType, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59");
			responderDefinition.AssemblyPath = EscalateComponentStateResponder.AssemblyPath;
			responderDefinition.TypeName = EscalateComponentStateResponder.TypeName;
			responderDefinition.Attributes["ComponentStateServerComponentName"] = serverComponentToVerifyState.ToString();
			responderDefinition.Attributes["ComponentStateTriggerConfig"] = triggerConfig.ToString();
			return responderDefinition;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00011C92 File Offset: 0x0000FE92
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			CafeUtils.InvokeResponderGivenComponentState(this, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod1(cancelToken);
			}, base.TraceContext, cancellationToken);
		}

		// Token: 0x040001D4 RID: 468
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040001D5 RID: 469
		private static readonly string TypeName = typeof(EscalateComponentStateResponder).FullName;
	}
}
