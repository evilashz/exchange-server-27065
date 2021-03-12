using System;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x02000248 RID: 584
	public class OAuthExchangeProbe : OAuthPartnerProbe
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x0006DA08 File Offset: 0x0006BC08
		internal static ProbeDefinition CreateDefinition(string monitoringUser, string probeName, string targetResourceName, Uri targetEndpoint)
		{
			return OAuthPartnerProbe.CreateDefinition(monitoringUser, probeName, targetResourceName, targetEndpoint, OAuthExchangeProbe.ProbeTypeName);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0006DA18 File Offset: 0x0006BC18
		protected override OAuthPartnerProbe.ProbeState RunOAuthPartnerProbe()
		{
			string empty = string.Empty;
			ResultType resultType = TestOAuthConnectivityHelper.SendExchangeOAuthRequest(base.MonitoringUser, null, new Uri(base.Definition.Endpoint), out empty, false, false, false);
			base.DiagnosticMessage = empty;
			if (resultType != ResultType.Error)
			{
				return OAuthPartnerProbe.ProbeState.Passed;
			}
			return OAuthPartnerProbe.ProbeState.FailedRequest;
		}

		// Token: 0x04000C55 RID: 3157
		private static readonly string ProbeTypeName = typeof(OAuthExchangeProbe).FullName;
	}
}
