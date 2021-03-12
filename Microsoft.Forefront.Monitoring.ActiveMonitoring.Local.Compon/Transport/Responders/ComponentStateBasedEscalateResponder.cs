using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Responders
{
	// Token: 0x0200025D RID: 605
	public class ComponentStateBasedEscalateResponder : EscalateResponder
	{
		// Token: 0x06001446 RID: 5190 RVA: 0x0003BACC File Offset: 0x00039CCC
		protected override bool ShouldRaiseActiveMonitoringAlerts(EscalationEnvironment environment)
		{
			if (!base.ShouldRaiseActiveMonitoringAlerts(environment))
			{
				return false;
			}
			string text = base.Definition.Attributes["ServerComponentName"];
			string text2 = base.Definition.Attributes["ExpectedComponentState"];
			string arg;
			if (ComponentState.VerifyExpectedState(text, text2, out arg))
			{
				return true;
			}
			base.Result.StateAttribute1 = string.Format("Component:{0} actual state: {1}, expected state: {2}, suppressing Responder", text, arg, text2.ToString());
			return false;
		}

		// Token: 0x040009C2 RID: 2498
		internal const string ServerComponentName = "ServerComponentName";

		// Token: 0x040009C3 RID: 2499
		internal const string ExpectedComponentState = "ExpectedComponentState";
	}
}
