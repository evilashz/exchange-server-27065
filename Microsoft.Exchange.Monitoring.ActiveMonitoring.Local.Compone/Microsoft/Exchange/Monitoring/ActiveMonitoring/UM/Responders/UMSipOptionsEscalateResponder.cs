using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM.Responders
{
	// Token: 0x020004B5 RID: 1205
	internal class UMSipOptionsEscalateResponder : EscalateResponder
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x000B5C14 File Offset: 0x000B3E14
		public static ResponderDefinition CreateUMSipOptionEscalateResponder(string name, string typeName, string alertTypeId, string alertMask, string targetResource, string healthSet, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubject, string escalationMessage, TracingContext traceContext)
		{
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.UnifiedMessagingTracer, traceContext, "UMDiscovery:: DoWork(): Creating {0} for {1}", name, targetResource, null, "CreateUMSipOptionEscalateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\Responders\\UMSipOptionsEscalateResponder.cs", 51);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, healthSet, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubject, escalationMessage, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.AssemblyPath = UMMonitoringConstants.AssemblyPath;
			responderDefinition.TypeName = typeName;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.UnifiedMessagingTracer, traceContext, "UMDiscovery:: DoWork(): Created {0} for {1}", name, targetResource, null, "CreateUMSipOptionEscalateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UM\\Responders\\UMSipOptionsEscalateResponder.cs", 72);
			return responderDefinition;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B5CA4 File Offset: 0x000B3EA4
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			UMSipOptionsResponderUtils.InvokeBaseResponderMethodIfRequired(this, new List<string>
			{
				"15500",
				"15503",
				"15604"
			}, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod1(cancelToken);
			}, base.TraceContext, cancellationToken);
		}
	}
}
