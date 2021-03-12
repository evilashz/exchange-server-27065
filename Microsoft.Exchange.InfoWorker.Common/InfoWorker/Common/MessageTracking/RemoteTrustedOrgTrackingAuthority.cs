using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002C0 RID: 704
	internal class RemoteTrustedOrgTrackingAuthority : WebServiceTrackingAuthority
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0005AFAE File Offset: 0x000591AE
		public static RemoteTrustedOrgTrackingAuthority Create(string domain, SmtpAddress proxyRecipient)
		{
			return new RemoteTrustedOrgTrackingAuthority(domain, TrackingAuthorityKind.RemoteTrustedOrg, proxyRecipient);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0005AFB8 File Offset: 0x000591B8
		protected override void SetAuthenticationMechanism(ExchangeServiceBinding ewsBinding)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0005AFBF File Offset: 0x000591BF
		public override bool IsAllowedScope(SearchScope scope)
		{
			return scope == SearchScope.World;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0005AFC5 File Offset: 0x000591C5
		public override string ToString()
		{
			return string.Format("Type=RemoteTrustedOrgTrackingAuthority,Domain={0}", this.domain);
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0005AFD7 File Offset: 0x000591D7
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Organization;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0005AFDA File Offset: 0x000591DA
		public override string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0005AFE2 File Offset: 0x000591E2
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0005AFEA File Offset: 0x000591EA
		public override SmtpAddress ProxyRecipient { get; protected set; }

		// Token: 0x060013A8 RID: 5032 RVA: 0x0005AFF4 File Offset: 0x000591F4
		private RemoteTrustedOrgTrackingAuthority(string domain, TrackingAuthorityKind responsibleTracker, SmtpAddress proxyRecipient) : base(responsibleTracker, null)
		{
			if (string.IsNullOrEmpty(domain) && SmtpAddress.Empty.Equals(proxyRecipient))
			{
				throw new ArgumentException("Either domain or proxyRecipient must be supplied, otherwise we cannot autodiscover the remote trusted organization");
			}
			this.domain = domain;
			this.ProxyRecipient = proxyRecipient;
		}

		// Token: 0x04000D0B RID: 3339
		private string domain;
	}
}
