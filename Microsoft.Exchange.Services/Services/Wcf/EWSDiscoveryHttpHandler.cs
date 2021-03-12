using System;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B75 RID: 2933
	public class EWSDiscoveryHttpHandler : IHttpHandler
	{
		// Token: 0x06005318 RID: 21272 RVA: 0x0010D6A5 File Offset: 0x0010B8A5
		public void ProcessRequest(HttpContext context)
		{
			this.GenerateResponse(context);
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0010D6B0 File Offset: 0x0010B8B0
		internal virtual void GenerateResponse(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			this.AddEndpointEnabledHeaders(context.Response);
			if (!request.IsAuthenticated)
			{
				response.StatusCode = 401;
				return;
			}
			if (request.HttpMethod == "GET")
			{
				response.Redirect("/ews/Services.wsdl");
				return;
			}
			if (request.HttpMethod == "HEAD")
			{
				response.StatusCode = 302;
				response.RedirectLocation = "/ews/Services.wsdl";
				return;
			}
			response.StatusCode = 400;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0010D740 File Offset: 0x0010B940
		protected void AddEndpointEnabledHeaders(HttpResponse response)
		{
			response.AddHeader("X-WSSecurity-Enabled", EWSSettings.IsWsSecurityEndpointEnabled ? bool.TrueString : bool.FalseString);
			response.AddHeader("X-WSSecurity-For", (VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.WsSecurityEndpoint.Enabled && EWSSettings.IsWsSecurityEndpointEnabled) ? "Logon" : "None");
			FederationTrust federationTrust = FederationTrustCache.GetFederationTrust("MicrosoftOnline");
			if (federationTrust != null)
			{
				response.AddHeader("X-FederationTrustTokenIssuerUri", federationTrust.TokenIssuerUri.ToString());
			}
			if (EWSSettings.IsWsSecuritySymmetricKeyEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-SymmetricKey-Enabled", bool.TrueString);
			}
			if (EWSSettings.IsWsSecurityX509CertEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-X509Cert-Enabled", bool.TrueString);
			}
			response.AddHeader("X-OAuth-Enabled", EWSSettings.IsOAuthEndpointEnabled ? bool.TrueString : bool.FalseString);
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600531B RID: 21275 RVA: 0x0010D812 File Offset: 0x0010BA12
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002E3C RID: 11836
		private const string OAuthEnabledHeaderName = "X-OAuth-Enabled";

		// Token: 0x04002E3D RID: 11837
		private const string WsSecurityEnabledHeaderName = "X-WSSecurity-Enabled";

		// Token: 0x04002E3E RID: 11838
		private const string WsSecuritySymmetricKeyEnabledHeaderName = "X-WSSecurity-SymmetricKey-Enabled";

		// Token: 0x04002E3F RID: 11839
		private const string WsSecurityX509CertEnabledHeaderName = "X-WSSecurity-X509Cert-Enabled";

		// Token: 0x04002E40 RID: 11840
		private const string WsSecurityForHeaderName = "X-WSSecurity-For";

		// Token: 0x04002E41 RID: 11841
		private const string FederationTrustTokenIssuerUriHeaderName = "X-FederationTrustTokenIssuerUri";

		// Token: 0x04002E42 RID: 11842
		private const string NoneHeaderValue = "None";

		// Token: 0x04002E43 RID: 11843
		private const string LogonHeaderValue = "Logon";

		// Token: 0x04002E44 RID: 11844
		private const string RedirectLocation = "/ews/Services.wsdl";
	}
}
