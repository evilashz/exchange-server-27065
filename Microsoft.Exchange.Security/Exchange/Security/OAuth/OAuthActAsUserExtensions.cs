using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000CD RID: 205
	internal static class OAuthActAsUserExtensions
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0003244A File Offset: 0x0003064A
		internal static SecurityIdentifier GetMasterAccountSidIfAvailable(this OAuthActAsUser oauthActAsUser)
		{
			return oauthActAsUser.GetMasterAccountSidIfAvailable(OAuthActAsUserExtensions.useMasterAccountSid.Value);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0003245C File Offset: 0x0003065C
		internal static SecurityIdentifier GetMasterAccountSidIfAvailable(this OAuthActAsUser oauthActAsUser, bool useMasterAccountSid)
		{
			SecurityIdentifier result = null;
			if (oauthActAsUser != null)
			{
				result = oauthActAsUser.Sid;
				if (useMasterAccountSid && OAuthActAsUserExtensions.IsValidMasterAccountSid(oauthActAsUser.MasterAccountSid))
				{
					result = oauthActAsUser.MasterAccountSid;
				}
			}
			return result;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0003248D File Offset: 0x0003068D
		private static bool IsValidMasterAccountSid(SecurityIdentifier masterAccountSid)
		{
			return masterAccountSid != null && !masterAccountSid.Equals(OAuthActAsUserExtensions.selfSidSentinel);
		}

		// Token: 0x0400067B RID: 1659
		private static BoolAppSettingsEntry useMasterAccountSid = new BoolAppSettingsEntry("OAuthHttpModule.UseMasterAccountSid", false, ExTraceGlobals.OAuthTracer);

		// Token: 0x0400067C RID: 1660
		private static readonly SecurityIdentifier selfSidSentinel = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
	}
}
