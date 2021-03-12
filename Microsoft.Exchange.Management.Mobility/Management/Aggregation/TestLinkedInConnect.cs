using System;
using System.Collections.Specialized;
using System.Management.Automation;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000041 RID: 65
	[Cmdlet("Test", "LinkedInConnect")]
	internal sealed class TestLinkedInConnect : Task
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		public TestLinkedInConnect()
		{
			this.tracer = new CmdletWriteVerboseTracer(this);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000BCDC File Offset: 0x00009EDC
		[Parameter(Mandatory = true)]
		public string AuthorizationCallbackUrl { get; set; }

		// Token: 0x06000284 RID: 644 RVA: 0x0000BCE5 File Offset: 0x00009EE5
		protected override void InternalProcessRecord()
		{
			base.WriteObject(this.AuthorizeApplication());
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BCF3 File Offset: 0x00009EF3
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || typeof(ExchangeConfigurationException).IsInstanceOfType(e) || typeof(LinkedInAuthenticationException).IsInstanceOfType(e);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BD22 File Offset: 0x00009F22
		private LinkedInAppAuthorizationResponse AuthorizeApplication()
		{
			return this.CreateAuthenticator().AuthorizeApplication(new NameValueCollection(), new HttpCookieCollection(), new HttpCookieCollection(), new Uri(this.AuthorizationCallbackUrl));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BD49 File Offset: 0x00009F49
		private LinkedInAuthenticator CreateAuthenticator()
		{
			return new LinkedInAuthenticator(this.ReadConfiguration(), this.CreateWebClient(), this.tracer);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BD64 File Offset: 0x00009F64
		private LinkedInConfig ReadConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return LinkedInConfig.CreateForAppAuth(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.RequestTokenEndpoint, peopleConnectApplicationConfig.AccessTokenEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BDAC File Offset: 0x00009FAC
		private LinkedInAppConfig ReadAppConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000BDF3 File Offset: 0x00009FF3
		private LinkedInWebClient CreateWebClient()
		{
			return new LinkedInWebClient(this.ReadAppConfiguration(), this.tracer);
		}

		// Token: 0x040000B6 RID: 182
		private readonly ITracer tracer;
	}
}
