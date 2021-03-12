using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BC RID: 700
	internal abstract class WebServiceTrackingAuthority : TrackingAuthority
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0005AA04 File Offset: 0x00058C04
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x06001382 RID: 4994
		protected abstract void SetAuthenticationMechanism(ExchangeServiceBinding ewsBinding);

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001383 RID: 4995
		public abstract string Domain { get; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0005AA0C File Offset: 0x00058C0C
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0005AA13 File Offset: 0x00058C13
		public virtual SmtpAddress ProxyRecipient
		{
			get
			{
				return SmtpAddress.Empty;
			}
			protected set
			{
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0005AA15 File Offset: 0x00058C15
		protected WebServiceTrackingAuthority(TrackingAuthorityKind responsibleTracker, Uri uri) : base(responsibleTracker)
		{
			this.uri = uri;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005AA28 File Offset: 0x00058C28
		public IWebServiceBinding GetEwsBinding(DirectoryContext directoryContext)
		{
			IClientProxy clientProxy;
			switch (base.TrackingAuthorityKind)
			{
			case TrackingAuthorityKind.RemoteSiteInCurrentOrg:
			{
				ExchangeServiceBinding exchangeServiceBinding = new ExchangeServiceBinding("MessageTracking", WebServiceTrackingAuthority.noValidationCallback);
				this.SetAuthenticationMechanism(exchangeServiceBinding);
				exchangeServiceBinding.Proxy = new WebProxy();
				RemoteSiteInCurrentOrgTrackingAuthority remoteSiteInCurrentOrgTrackingAuthority = (RemoteSiteInCurrentOrgTrackingAuthority)this;
				exchangeServiceBinding.Url = this.Uri.ToString();
				exchangeServiceBinding.UserAgent = WebServiceTrackingAuthority.EwsUserAgentString;
				exchangeServiceBinding.RequestServerVersionValue = new RequestServerVersion();
				exchangeServiceBinding.RequestServerVersionValue.Version = VersionConverter.GetExchangeVersionType(remoteSiteInCurrentOrgTrackingAuthority.ServerVersion);
				exchangeServiceBinding.CookieContainer = new CookieContainer();
				clientProxy = new ClientProxyEWS(exchangeServiceBinding, this.Uri, remoteSiteInCurrentOrgTrackingAuthority.ServerVersion);
				break;
			}
			case TrackingAuthorityKind.RemoteForest:
				clientProxy = new ClientProxyRD(directoryContext, this.ProxyRecipient, this.Domain, ExchangeVersion.Exchange2010);
				break;
			case TrackingAuthorityKind.RemoteTrustedOrg:
				clientProxy = new ClientProxyRD(directoryContext, this.ProxyRecipient, this.Domain, ExchangeVersion.Exchange2010_SP1);
				break;
			default:
				throw new NotImplementedException();
			}
			return new WebServiceBinding(clientProxy, directoryContext, this);
		}

		// Token: 0x04000D00 RID: 3328
		private static RemoteCertificateValidationCallback noValidationCallback = (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslErr) => true;

		// Token: 0x04000D01 RID: 3329
		private Uri uri;

		// Token: 0x04000D02 RID: 3330
		private static readonly string EwsUserAgentString = WellKnownUserAgent.GetEwsNegoAuthUserAgent("Microsoft.Exchange.InfoWorker.Common.MessageTracking");
	}
}
