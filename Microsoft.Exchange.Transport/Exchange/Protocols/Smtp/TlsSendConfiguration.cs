using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000519 RID: 1305
	internal class TlsSendConfiguration
	{
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000FF371 File Offset: 0x000FD571
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000FF379 File Offset: 0x000FD579
		public bool RequireTls { get; set; }

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000FF382 File Offset: 0x000FD582
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000FF38A File Offset: 0x000FD58A
		public SmtpX509Identifier TlsCertificateName { get; set; }

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000FF393 File Offset: 0x000FD593
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000FF39B File Offset: 0x000FD59B
		public string TlsCertificateFqdn { get; set; }

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000FF3A4 File Offset: 0x000FD5A4
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000FF3AC File Offset: 0x000FD5AC
		public RequiredTlsAuthLevel? TlsAuthLevel { get; set; }

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000FF3B5 File Offset: 0x000FD5B5
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000FF3BD File Offset: 0x000FD5BD
		public IList<SmtpDomainWithSubdomains> TlsDomains { get; set; }

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000FF3C6 File Offset: 0x000FD5C6
		// (set) Token: 0x06003D1B RID: 15643 RVA: 0x000FF3CE File Offset: 0x000FD5CE
		public bool ShouldSkipTls { get; set; }

		// Token: 0x06003D1C RID: 15644 RVA: 0x000FF3D8 File Offset: 0x000FD5D8
		public TlsSendConfiguration(SmtpSendConnectorConfig connector, RequiredTlsAuthLevel? tlsOverride, string nextHopDomain, string nextHopTlsDomain)
		{
			if (tlsOverride != null)
			{
				this.TlsAuthLevel = tlsOverride;
				this.ShouldSkipTls = false;
				this.RequireTls = true;
				this.authLevelOverrideDescription = string.Format("Overriding connector TLS configuration: TlsAuthLevel -> {0} : {1}, IgnoreSTARTTLS -> {2} : {3}, RequireTLS -> {4} : {5}", new object[]
				{
					connector.TlsAuthLevel,
					this.TlsAuthLevel,
					connector.IgnoreSTARTTLS,
					this.ShouldSkipTls,
					connector.RequireTLS,
					this.RequireTls
				});
			}
			else
			{
				this.TlsAuthLevel = EnumConverter.InternalToPublic(connector.TlsAuthLevel);
				this.ShouldSkipTls = connector.IgnoreSTARTTLS;
				this.RequireTls = connector.RequireTLS;
			}
			this.TlsCertificateName = connector.TlsCertificateName;
			this.TlsCertificateFqdn = (string.IsNullOrEmpty(connector.CertificateSubject) ? connector.Fqdn : connector.CertificateSubject);
			this.ResolveTlsDomains(connector, nextHopDomain, nextHopTlsDomain);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000FF4E0 File Offset: 0x000FD6E0
		public TlsSendConfiguration()
		{
			this.TlsAuthLevel = null;
			this.ShouldSkipTls = false;
			this.RequireTls = false;
			this.TlsDomains = null;
			this.TlsCertificateName = null;
			this.TlsCertificateFqdn = null;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000FF528 File Offset: 0x000FD728
		public void LogTlsOverride(IProtocolLogSession logSession)
		{
			if (!string.IsNullOrEmpty(this.authLevelOverrideDescription))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), this.authLevelOverrideDescription);
				logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, this.authLevelOverrideDescription);
			}
			if (!string.IsNullOrEmpty(this.domainOverrideDescription))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), this.domainOverrideDescription);
				logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, this.domainOverrideDescription);
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000FF59C File Offset: 0x000FD79C
		private void ResolveTlsDomains(SmtpSendConnectorConfig connector, string nextHopDomain, string nextHopTlsDomain)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("ResolveTlsDomains can only be invoked if connector has been initialized");
			}
			List<SmtpDomainWithSubdomains> list = new List<SmtpDomainWithSubdomains>();
			SmtpDomainWithSubdomains item;
			if (SmtpDomainWithSubdomains.TryParse(nextHopTlsDomain, out item))
			{
				list.Add(item);
				this.domainOverrideDescription = string.Format("Overriding connector TLS domain: {0}", nextHopTlsDomain);
			}
			else
			{
				SmtpDomainWithSubdomains tlsDomain = connector.TlsDomain;
				SmtpDomain domain;
				if (tlsDomain != null)
				{
					list.Add(tlsDomain);
				}
				else if (this.TlsAuthLevel != null && this.TlsAuthLevel.Value.Equals(RequiredTlsAuthLevel.DomainValidation) && SmtpDomain.TryParse(nextHopDomain, out domain))
				{
					list.Add(new SmtpDomainWithSubdomains(domain, true));
					list.Add(new SmtpDomainWithSubdomains(domain, false));
				}
			}
			this.TlsDomains = list;
		}

		// Token: 0x04001EFE RID: 7934
		private readonly string authLevelOverrideDescription;

		// Token: 0x04001EFF RID: 7935
		private string domainOverrideDescription;
	}
}
