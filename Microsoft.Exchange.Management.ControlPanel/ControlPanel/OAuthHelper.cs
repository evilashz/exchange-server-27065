using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000397 RID: 919
	public static class OAuthHelper
	{
		// Token: 0x060030DB RID: 12507 RVA: 0x00094D14 File Offset: 0x00092F14
		static OAuthHelper()
		{
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.CRM, "OAuthACS.CRM");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.Exchange, "OAuthACS.Exchange");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.Intune, "OAuthACS.Intune");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.Lync, "OAuthACS.Lync");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.Office365Portal, "OAuthACS.Office365Portal");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.OfficeServiceManager, "OAuthACS.OfficeServiceManager");
			OAuthHelper.parterIdToNameMap.Add(WellknownPartnerApplicationIdentifiers.SharePoint, "OAuthACS.SharePoint");
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x00094DBC File Offset: 0x00092FBC
		public static KeyValuePair<string, string>? GetOAuthUserConstraint(IIdentity logonUser)
		{
			SidOAuthIdentity sidOAuthIdentity = (logonUser as SidOAuthIdentity) ?? (HttpContext.Current.Items["LogonUserIdentity"] as SidOAuthIdentity);
			OAuthIdentity oauthIdentity = (sidOAuthIdentity != null) ? sidOAuthIdentity.OAuthIdentity : (logonUser as OAuthIdentity);
			if (oauthIdentity != null)
			{
				string value = null;
				if (oauthIdentity.OAuthApplication == null || oauthIdentity.OAuthApplication.PartnerApplication == null || !OAuthHelper.parterIdToNameMap.TryGetValue(oauthIdentity.OAuthApplication.PartnerApplication.ApplicationIdentifier, out value))
				{
					value = "OAuthACS.UnknownPartner";
				}
				return new KeyValuePair<string, string>?(new KeyValuePair<string, string>("AuthMethod", value));
			}
			return null;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x00094E58 File Offset: 0x00093058
		private static string GetUniqueStringFromService(string servicePath, string schema, string workflow)
		{
			StringBuilder stringBuilder = new StringBuilder(servicePath);
			bool flag = !string.IsNullOrEmpty(schema);
			if (flag)
			{
				stringBuilder.Append('?');
				stringBuilder.Append("schema");
				stringBuilder.Append('=');
				stringBuilder.Append(schema);
			}
			bool flag2 = !string.IsNullOrEmpty(workflow);
			if (flag2)
			{
				stringBuilder.Append(flag ? '&' : '?');
				stringBuilder.Append("workflow");
				stringBuilder.Append('=');
				stringBuilder.Append(workflow);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x00094EE0 File Offset: 0x000930E0
		public static bool IsWebRequestAllowed(HttpContext context)
		{
			if (!context.IsAcsOAuthRequest())
			{
				return true;
			}
			VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
			return OAuthHelper.IsWebRequestAllowed(snapshotForCurrentUser, snapshotForCurrentUser.Eac.GetObjectsOfType<IEacWebRequest>(), context.Request);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x00094F18 File Offset: 0x00093118
		internal static bool IsWebRequestAllowed(VariantConfigurationSnapshot snapshot, IDictionary<string, IEacWebRequest> configs, HttpRequest request)
		{
			string relativePathToAppRoot = EcpUrl.GetRelativePathToAppRoot(request.FilePath);
			string text = request.QueryString["workflow"];
			string schema = request.QueryString["schema"];
			if (text == null && request.PathInfo != null && request.PathInfo.Length > 1 && request.PathInfo[0] == '/')
			{
				text = request.PathInfo.Substring(1);
			}
			string uniqueStringFromService = OAuthHelper.GetUniqueStringFromService(relativePathToAppRoot, schema, text);
			foreach (KeyValuePair<string, IEacWebRequest> keyValuePair in configs)
			{
				if (string.Compare(uniqueStringFromService, keyValuePair.Value.Request, true) == 0)
				{
					return keyValuePair.Value.Enabled;
				}
			}
			return snapshot.Eac.UnlistedServices.Enabled;
		}

		// Token: 0x0400239D RID: 9117
		private const string AuthMethodConstraintKey = "AuthMethod";

		// Token: 0x0400239E RID: 9118
		private const string OAuthWithACS = "OAuthACS.";

		// Token: 0x0400239F RID: 9119
		private const string UnknownPartner = "OAuthACS.UnknownPartner";

		// Token: 0x040023A0 RID: 9120
		private static readonly IDictionary<string, string> parterIdToNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
