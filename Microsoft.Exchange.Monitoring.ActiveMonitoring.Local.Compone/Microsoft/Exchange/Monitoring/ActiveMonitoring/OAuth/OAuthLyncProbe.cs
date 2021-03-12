using System;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x02000249 RID: 585
	public class OAuthLyncProbe : OAuthPartnerProbe
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x0006DA79 File Offset: 0x0006BC79
		internal static ProbeDefinition CreateDefinition(string monitoringUser, string probeName, string targetResourceName, Uri targetEndpoint)
		{
			return OAuthPartnerProbe.CreateDefinition(monitoringUser, probeName, targetResourceName, targetEndpoint, OAuthLyncProbe.ProbeTypeName);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0006DA8C File Offset: 0x0006BC8C
		protected override OAuthPartnerProbe.ProbeState RunOAuthPartnerProbe()
		{
			string empty = string.Empty;
			Uri targetUri = string.IsNullOrEmpty(base.Definition.Endpoint) ? null : new Uri(base.Definition.Endpoint);
			ResultType resultType = TestOAuthConnectivityHelper.SendLyncOAuthRequest(base.MonitoringUser, targetUri, out empty, false, false, false);
			base.DiagnosticMessage = empty;
			if (resultType != ResultType.Error)
			{
				return OAuthPartnerProbe.ProbeState.Passed;
			}
			return OAuthPartnerProbe.ProbeState.FailedRequest;
		}

		// Token: 0x04000C56 RID: 3158
		private static readonly string ProbeTypeName = typeof(OAuthLyncProbe).FullName;
	}
}
