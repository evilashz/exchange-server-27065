using System;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000058 RID: 88
	public class AutodiscoverDiscoveryHttpHandler : IHttpHandler
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00011C97 File Offset: 0x0000FE97
		public void ProcessRequest(HttpContext context)
		{
			this.GenerateResponse(context);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		internal virtual void GenerateResponse(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			this.AddEndpointEnabledHeaders(context.Response);
			if (request.IsAuthenticated)
			{
				response.Redirect("./Services.wsdl");
				return;
			}
			response.StatusCode = 401;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		protected void AddEndpointEnabledHeaders(HttpResponse response)
		{
			if (AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.SoapEndpointEnabled)
			{
				response.AddHeader("X-SOAP-Enabled", bool.TrueString);
			}
			response.AddHeader("X-WSSecurity-Enabled", AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.WsSecurityEndpointEnabled ? bool.TrueString : bool.FalseString);
			response.AddHeader("X-WSSecurity-For", (VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.WsSecurityEndpoint.Enabled && AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.WsSecurityEndpointEnabled) ? "Logon" : "None");
			FederationTrust federationTrust = FederationTrustCache.GetFederationTrust("MicrosoftOnline");
			if (federationTrust != null)
			{
				response.AddHeader("X-FederationTrustTokenIssuerUri", federationTrust.TokenIssuerUri.ToString());
			}
			if (AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.WsSecuritySymmetricKeyEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-SymmetricKey-Enabled", bool.TrueString);
			}
			if (AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.WsSecurityX509CertEndpointEnabled)
			{
				response.AddHeader("X-WSSecurity-X509Cert-Enabled", bool.TrueString);
			}
			response.AddHeader("X-OAuth-Enabled", AutodiscoverDiscoveryHttpHandler.webConfiguration.Member.OAuthEndpointEnabled ? bool.TrueString : bool.FalseString);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00011E0D File Offset: 0x0001000D
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040002A1 RID: 673
		private const string SoapEnabledHeaderName = "X-SOAP-Enabled";

		// Token: 0x040002A2 RID: 674
		private const string OAuthEnabledHeaderName = "X-OAuth-Enabled";

		// Token: 0x040002A3 RID: 675
		private const string WsSecurityEnabledHeaderName = "X-WSSecurity-Enabled";

		// Token: 0x040002A4 RID: 676
		private const string WsSecuritySymmetricKeyEnabledHeaderName = "X-WSSecurity-SymmetricKey-Enabled";

		// Token: 0x040002A5 RID: 677
		private const string WsSecurityX509CertEnabledHeaderName = "X-WSSecurity-X509Cert-Enabled";

		// Token: 0x040002A6 RID: 678
		private const string WsSecurityForHeaderName = "X-WSSecurity-For";

		// Token: 0x040002A7 RID: 679
		private const string FederationTrustTokenIssuerUriHeaderName = "X-FederationTrustTokenIssuerUri";

		// Token: 0x040002A8 RID: 680
		private const string NoneHeaderValue = "None";

		// Token: 0x040002A9 RID: 681
		private const string LogonHeaderValue = "Logon";

		// Token: 0x040002AA RID: 682
		private static LazyMember<AutodiscoverWebConfiguration> webConfiguration = new LazyMember<AutodiscoverWebConfiguration>(() => new AutodiscoverWebConfiguration());
	}
}
