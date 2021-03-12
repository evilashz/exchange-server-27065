using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x0200010C RID: 268
	public class DagForceRebootServerResponder : ForceRebootServerResponder
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00030630 File Offset: 0x0002E830
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServiceHealthStatus responderTargetState)
		{
			return DagForceRebootServerResponder.CreateDefinition(responderName, "Exchange", monitorName, responderTargetState);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003064C File Offset: 0x0002E84C
		internal static ResponderDefinition CreateDefinition(string responderName, string serviceName, string monitorName, ServiceHealthStatus responderTargetState)
		{
			ResponderDefinition responderDefinition = ForceRebootServerResponder.CreateDefinition(responderName, monitorName, responderTargetState, null, -1, "", "", "Datacenter, Stamp", "RecoveryData", "ArbitrationOnly", serviceName, true, "Dag", false);
			responderDefinition.AssemblyPath = DagForceRebootServerResponder.AssemblyPath;
			responderDefinition.TypeName = DagForceRebootServerResponder.TypeName;
			return responderDefinition;
		}

		// Token: 0x04000582 RID: 1410
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000583 RID: 1411
		private static readonly string TypeName = typeof(DagForceRebootServerResponder).FullName;
	}
}
