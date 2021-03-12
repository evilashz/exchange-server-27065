using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x02000055 RID: 85
	internal class RebootServerComponentStateResponder : ForceRebootServerResponder
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000125B0 File Offset: 0x000107B0
		internal static ResponderDefinition CreateDefinition(string responderName, string serviceName, string monitorName, ServiceHealthStatus responderTargetState, ServerComponentEnum serverComponentToVerifyState, CafeUtils.TriggerConfig triggerConfig)
		{
			ResponderDefinition responderDefinition = ForceRebootServerResponder.CreateDefinition(responderName, monitorName, responderTargetState, null, -1, "", "", "Datacenter, Stamp", "RecoveryData", "ArbitrationOnly", serviceName, true, null, false);
			responderDefinition.AssemblyPath = RebootServerComponentStateResponder.AssemblyPath;
			responderDefinition.TypeName = RebootServerComponentStateResponder.TypeName;
			responderDefinition.Attributes["ComponentStateServerComponentName"] = serverComponentToVerifyState.ToString();
			responderDefinition.Attributes["ComponentStateTriggerConfig"] = triggerConfig.ToString();
			return responderDefinition;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00012646 File Offset: 0x00010846
		protected override void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			CafeUtils.ConfigureResponderForCafeMinimumValues(this, delegate(AttributeHelper attribHelper)
			{
				this.<>n__FabricatedMethod5(attribHelper);
			}, delegate(int minRequired)
			{
				base.MinimumRequiredServers = minRequired;
			}, base.TraceContext);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00012675 File Offset: 0x00010875
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			CafeUtils.InvokeResponderGivenComponentState(this, delegate(CancellationToken cancelToken)
			{
				this.<>n__FabricatedMethod7(cancelToken);
			}, base.TraceContext, cancellationToken);
		}

		// Token: 0x04000202 RID: 514
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000203 RID: 515
		private static readonly string TypeName = typeof(RebootServerComponentStateResponder).FullName;
	}
}
