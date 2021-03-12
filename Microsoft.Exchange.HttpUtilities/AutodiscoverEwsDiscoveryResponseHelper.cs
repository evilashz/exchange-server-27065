using System;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpUtilities
{
	// Token: 0x02000003 RID: 3
	internal static class AutodiscoverEwsDiscoveryResponseHelper
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022A4 File Offset: 0x000004A4
		internal static void AddEndpointEnabledHeaders(HttpResponse response)
		{
			if (AutodiscoverEwsWebConfiguration.SoapEndpointEnabled)
			{
				response.AddHeader("X-SOAP-Enabled", bool.TrueString);
			}
			response.AddHeader("X-WSSecurity-Enabled", AutodiscoverEwsWebConfiguration.WsSecurityEndpointEnabled ? bool.TrueString : bool.FalseString);
			string value;
			if ((HttpProxyGlobals.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.WsSecurityEndpoint.Enabled) && AutodiscoverEwsWebConfiguration.WsSecurityEndpointEnabled)
			{
				value = "Logon";
			}
			else
			{
				value = "None";
			}
			response.AddHeader("X-WSSecurity-For", value);
			FederationTrust federationTrust = FederationTrustCache.GetFederationTrust("MicrosoftOnline");
			if (federationTrust != null)
			{
				response.AddHeader("X-FederationTrustTokenIssuerUri", federationTrust.TokenIssuerUri.ToString());
			}
			if (AutodiscoverEwsWebConfiguration.WsSecuritySymmetricKeyEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-SymmetricKey-Enabled", bool.TrueString);
			}
			if (AutodiscoverEwsWebConfiguration.WsSecurityX509CertEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-X509Cert-Enabled", bool.TrueString);
			}
			HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
			IHttpModule httpModule = applicationInstance.Modules["OAuthAuthModule"];
			response.AddHeader("X-OAuth-Enabled", (httpModule != null) ? bool.TrueString : bool.FalseString);
		}

		// Token: 0x04000003 RID: 3
		private const string SoapEnabledHeaderName = "X-SOAP-Enabled";

		// Token: 0x04000004 RID: 4
		private const string OAuthEnabledHeaderName = "X-OAuth-Enabled";

		// Token: 0x04000005 RID: 5
		private const string WsSecurityEnabledHeaderName = "X-WSSecurity-Enabled";

		// Token: 0x04000006 RID: 6
		private const string WsSecuritySymmetricKeyEnabledHeaderName = "X-WSSecurity-SymmetricKey-Enabled";

		// Token: 0x04000007 RID: 7
		private const string WsSecurityX509CertEnabledHeaderName = "X-WSSecurity-X509Cert-Enabled";

		// Token: 0x04000008 RID: 8
		private const string WsSecurityForHeaderName = "X-WSSecurity-For";

		// Token: 0x04000009 RID: 9
		private const string FederationTrustTokenIssuerUriHeaderName = "X-FederationTrustTokenIssuerUri";

		// Token: 0x0400000A RID: 10
		private const string NoneHeaderValue = "None";

		// Token: 0x0400000B RID: 11
		private const string LogonHeaderValue = "Logon";
	}
}
