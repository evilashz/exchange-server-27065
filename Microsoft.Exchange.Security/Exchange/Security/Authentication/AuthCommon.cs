using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000023 RID: 35
	internal static class AuthCommon
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000091CC File Offset: 0x000073CC
		public static bool IsFrontEnd
		{
			get
			{
				return !string.IsNullOrEmpty(AuthCommon.HttpProxyProtocolType.Value);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000091E0 File Offset: 0x000073E0
		public static ADRawEntry GetHttpContextADRawEntry(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			return httpContext.Items[AuthCommon.AuthenticatedUserObjectKey] as ADRawEntry;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009205 File Offset: 0x00007405
		public static void SetHttpContextADRawEntry(HttpContext httpContext, ADRawEntry adRawEntry)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			httpContext.Items[AuthCommon.AuthenticatedUserObjectKey] = adRawEntry;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00009226 File Offset: 0x00007426
		public static bool IsMultiTenancyEnabled
		{
			get
			{
				return AuthCommon.isMultiTenancyEnabled;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000922D File Offset: 0x0000742D
		public static bool IsWindowsLiveIDEnabled
		{
			get
			{
				return AuthCommon.isWindowsLiveIDEnabled;
			}
		}

		// Token: 0x0400016D RID: 365
		internal const string DefaultAnchorMailboxCookieName = "DefaultAnchorMailbox";

		// Token: 0x0400016E RID: 366
		public static readonly string AuthenticatedUserObjectKey = "AuthenticatedUserObjectKey";

		// Token: 0x0400016F RID: 367
		internal static readonly SecurityIdentifier MemberNameNullSid = new SecurityIdentifier(WellKnownSidType.NullSid, null);

		// Token: 0x04000170 RID: 368
		private static readonly bool isMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x04000171 RID: 369
		private static readonly bool isWindowsLiveIDEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.WindowsLiveID.Enabled;

		// Token: 0x04000172 RID: 370
		public static readonly StringAppSettingsEntry HttpProxyProtocolType = new StringAppSettingsEntry("HttpProxy.ProtocolType", string.Empty, ExTraceGlobals.AuthenticationTracer);
	}
}
