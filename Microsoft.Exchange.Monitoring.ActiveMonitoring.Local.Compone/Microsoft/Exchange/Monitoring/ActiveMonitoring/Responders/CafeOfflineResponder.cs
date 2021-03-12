using System;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000F0 RID: 240
	public class CafeOfflineResponder : OfflineResponder
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServerComponentEnum componentToTakeOffline, ServiceHealthStatus responderTargetState, string serviceName, double minimumFractionRequiredOnline = -1.0, string failureReason = "", string arbitrationScope = "Datacenter", string arbitrationSource = "F5AvailabilityData", string requestedAction = "MachineOut")
		{
			ResponderDefinition responderDefinition = OfflineResponder.CreateDefinition(responderName, monitorName, componentToTakeOffline, responderTargetState, serviceName, null, -1, failureReason, arbitrationScope, arbitrationSource, requestedAction);
			responderDefinition.AssemblyPath = CafeOfflineResponder.AssemblyPath;
			responderDefinition.TypeName = CafeOfflineResponder.TypeName;
			responderDefinition.Attributes["CafeMinimumServerFractionOnline"] = minimumFractionRequiredOnline.ToString();
			return responderDefinition;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0002CC33 File Offset: 0x0002AE33
		protected override void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			CafeUtils.ConfigureResponderForCafeMinimumValues(this, delegate(AttributeHelper attribHelper)
			{
				this.<>n__FabricatedMethod3(attribHelper);
			}, delegate(int minRequired)
			{
				base.MinimumRequiredServers = minRequired;
			}, base.TraceContext);
		}

		// Token: 0x040004FF RID: 1279
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000500 RID: 1280
		private static readonly string TypeName = typeof(CafeOfflineResponder).FullName;
	}
}
