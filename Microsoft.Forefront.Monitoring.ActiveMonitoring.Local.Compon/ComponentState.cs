using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000030 RID: 48
	public class ComponentState
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static bool VerifyExpectedState(string serverComponent, string expectedState, out string actualState)
		{
			if (!ServerComponentStateManager.IsValidComponent(serverComponent))
			{
				throw new ArgumentException(string.Format("{0} is not a valid component", serverComponent));
			}
			ServerComponentEnum serverComponent2 = (ServerComponentEnum)Enum.Parse(typeof(ServerComponentEnum), serverComponent);
			ServiceState effectiveState = ServerComponentStateManager.GetEffectiveState(serverComponent2);
			actualState = effectiveState.ToString();
			if (string.IsNullOrEmpty(expectedState))
			{
				throw new ArgumentException("ExpectedState is null or empty");
			}
			ServiceState serviceState = (ServiceState)Enum.Parse(typeof(ServiceState), expectedState);
			return object.Equals(effectiveState, serviceState);
		}
	}
}
