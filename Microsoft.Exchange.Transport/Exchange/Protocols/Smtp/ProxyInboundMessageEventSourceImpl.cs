using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AA RID: 1194
	internal class ProxyInboundMessageEventSourceImpl : ProxyInboundMessageEventSource
	{
		// Token: 0x060035E6 RID: 13798 RVA: 0x000DDA97 File Offset: 0x000DBC97
		private ProxyInboundMessageEventSourceImpl(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000DDAA0 File Offset: 0x000DBCA0
		// (set) Token: 0x060035E8 RID: 13800 RVA: 0x000DDAA8 File Offset: 0x000DBCA8
		public IEnumerable<INextHopServer> DestinationServers { get; private set; }

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000DDAB1 File Offset: 0x000DBCB1
		// (set) Token: 0x060035EA RID: 13802 RVA: 0x000DDAB9 File Offset: 0x000DBCB9
		public bool InternalDestination { get; private set; }

		// Token: 0x060035EB RID: 13803 RVA: 0x000DDAC2 File Offset: 0x000DBCC2
		public static ProxyInboundMessageEventSourceImpl Create(SmtpSession smtpSession)
		{
			return new ProxyInboundMessageEventSourceImpl(smtpSession);
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000DDACA File Offset: 0x000DBCCA
		public override void SetProxyRoutingOverride(IEnumerable<INextHopServer> destinationServers, bool internalDestination)
		{
			this.DestinationServers = destinationServers;
			this.InternalDestination = internalDestination;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000DDADA File Offset: 0x000DBCDA
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000DDAE7 File Offset: 0x000DBCE7
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000DDAF6 File Offset: 0x000DBCF6
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}
	}
}
