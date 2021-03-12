using System;
using System.Web;
using Microsoft.Exchange.Autodiscover;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x0200000A RID: 10
	internal static class ResourceUrlBuilder
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002B0A File Offset: 0x00000D0A
		public static string GetResourceUrl(string protocol, string hostName)
		{
			return string.Format("https://{0}{1}", hostName, ResourceUrlBuilder.GetResourceUrlSuffixForProtocol(protocol, hostName));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B20 File Offset: 0x00000D20
		public static AutoDiscoverV2Response GetRedirectResponse(RequestDetailsLogger logger, string hostName, string redirectEmailAddress, string protocol, uint currentRedirectCount, string hostNameHint = null)
		{
			string text;
			if (hostNameHint != null)
			{
				text = string.Format("https://{0}/{1}?Email={2}&Protocol={3}&RedirectCount={4}&HostNameHint={5}", new object[]
				{
					hostName,
					"autodiscover/autodiscover.json",
					HttpUtility.UrlEncode(redirectEmailAddress),
					HttpUtility.UrlEncode(protocol),
					currentRedirectCount + 1U,
					hostNameHint
				});
			}
			else
			{
				text = string.Format("https://{0}/{1}?Email={2}&Protocol={3}&RedirectCount={4}", new object[]
				{
					hostName,
					"autodiscover/autodiscover.json",
					HttpUtility.UrlEncode(redirectEmailAddress),
					HttpUtility.UrlEncode(protocol),
					currentRedirectCount + 1U
				});
			}
			logger.AppendGenericInfo("GetRedirectUrlRedirectAbsoluteUrl", text);
			return new AutoDiscoverV2Response
			{
				RedirectUrl = text
			};
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BCC File Offset: 0x00000DCC
		private static string GetResourceUrlSuffixForProtocol(string protocol, string hostName)
		{
			SupportedProtocol supportedProtocol;
			Enum.TryParse<SupportedProtocol>(protocol, true, out supportedProtocol);
			switch (supportedProtocol)
			{
			case SupportedProtocol.Unknown:
				throw AutoDiscoverResponseException.BadRequest("InvalidProtocol", string.Format("The given protocol value '{0}' is invalid. Supported values are '{1}'", protocol, "ActiveSync, Ews"), null);
			case SupportedProtocol.Rest:
				throw AutoDiscoverResponseException.NotFound("MailboxNotEnabledForRESTAPI", "REST API is not yet supported for this mailbox.", null);
			case SupportedProtocol.ActiveSync:
				return "/Microsoft-Server-ActiveSync";
			case SupportedProtocol.Ews:
				return "/EWS/Exchange.asmx";
			default:
				return null;
			}
		}

		// Token: 0x0400000F RID: 15
		internal const string ActiveSyncUrlSuffix = "/Microsoft-Server-ActiveSync";

		// Token: 0x04000010 RID: 16
		internal const string WebServiceUrlSuffix = "/EWS/Exchange.asmx";

		// Token: 0x04000011 RID: 17
		internal const string EmailRedirectUrlSuffix = "autodiscover/autodiscover.json";
	}
}
