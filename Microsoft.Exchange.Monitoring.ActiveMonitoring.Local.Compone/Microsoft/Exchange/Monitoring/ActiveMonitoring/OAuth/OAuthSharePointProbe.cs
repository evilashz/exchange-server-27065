using System;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x0200024A RID: 586
	public class OAuthSharePointProbe : OAuthPartnerProbe
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x0006DB03 File Offset: 0x0006BD03
		internal static ProbeDefinition CreateDefinition(string monitoringUser, string probeName, string targetResourceName, Uri targetEndpoint)
		{
			return OAuthPartnerProbe.CreateDefinition(monitoringUser, probeName, targetResourceName, targetEndpoint, OAuthSharePointProbe.ProbeTypeName);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0006DB14 File Offset: 0x0006BD14
		protected override OAuthPartnerProbe.ProbeState RunOAuthPartnerProbe()
		{
			string diagnosticMessage = string.Empty;
			string text = base.MonitoringUser.PrimarySmtpAddress.ToString();
			if (string.IsNullOrEmpty(text))
			{
				diagnosticMessage = "User email is empty.";
				return OAuthPartnerProbe.ProbeState.FailedRequest;
			}
			string[] separator = new string[]
			{
				"@"
			};
			string[] array = text.Split(separator, StringSplitOptions.None);
			if (array == null || array.Length < 2)
			{
				diagnosticMessage = string.Format("User email does not contain '@': {0} ", text);
				return OAuthPartnerProbe.ProbeState.FailedRequest;
			}
			string[] separator2 = new string[]
			{
				"."
			};
			string[] array2 = array[1].Split(separator2, StringSplitOptions.None);
			if (array2 == null || array2.Length < 2)
			{
				diagnosticMessage = string.Format("User email does not contain '.': {0} ", text);
				return OAuthPartnerProbe.ProbeState.FailedRequest;
			}
			string text2 = array2[0];
			if (string.IsNullOrEmpty(text2))
			{
				diagnosticMessage = string.Format("Unable to parse tenant name from user email address '.': {0} ", text);
				return OAuthPartnerProbe.ProbeState.FailedRequest;
			}
			string arg;
			if (text2.ToLower().Contains("sdf"))
			{
				arg = ".spoppe.com/_vti_bin/listdata.svc";
			}
			else
			{
				arg = ".sharepoint.com/_vti_bin/listdata.svc";
			}
			string uriString = string.Format("https://{0}{1}", text2, arg);
			Uri targetUri = new Uri(uriString);
			ResultType resultType = TestOAuthConnectivityHelper.SendSPOAuthRequest(base.MonitoringUser, targetUri, out diagnosticMessage, false, false, false);
			base.DiagnosticMessage = diagnosticMessage;
			if (resultType != ResultType.Error)
			{
				return OAuthPartnerProbe.ProbeState.Passed;
			}
			return OAuthPartnerProbe.ProbeState.FailedRequest;
		}

		// Token: 0x04000C57 RID: 3159
		private static readonly string ProbeTypeName = typeof(OAuthSharePointProbe).FullName;
	}
}
