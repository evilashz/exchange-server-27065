using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Transport.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004F1 RID: 1265
	internal class ControlServiceResponderHelper : ResponderDefinitionHelper
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x000BF3F0 File Offset: 0x000BD5F0
		internal override ResponderDefinition CreateDefinition()
		{
			string mandatoryValue = base.GetMandatoryValue<string>("WindowsServiceName");
			int mandatoryValue2 = base.GetMandatoryValue<int>("ControlCode");
			string optionalValue = base.GetOptionalValue<string>("ThrottleGroupName", "");
			string name = base.Name;
			string alertMask = base.AlertMask;
			ServiceHealthStatus targetHealthState = base.TargetHealthState;
			return ControlServiceResponder.CreateDefinition(name, alertMask, mandatoryValue, targetHealthState, mandatoryValue2, optionalValue);
		}
	}
}
