using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000095 RID: 149
	internal class SystemFailoverResponderHelper : ResponderDefinitionHelper
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x00020808 File Offset: 0x0001EA08
		internal override ResponderDefinition CreateDefinition()
		{
			string text = (!string.IsNullOrWhiteSpace(base.ServiceName)) ? base.ServiceName : "Exchange";
			string mandatoryValue = base.GetMandatoryValue<string>("ComponentName");
			string name = base.Name;
			string alertMask = base.AlertMask;
			ServiceHealthStatus targetHealthState = base.TargetHealthState;
			bool enabled = base.Enabled;
			string serviceName = text;
			return SystemFailoverResponder.CreateDefinition(name, alertMask, targetHealthState, mandatoryValue, serviceName, enabled);
		}
	}
}
