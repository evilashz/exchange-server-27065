using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.WSSecurity;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000104 RID: 260
	internal sealed class OrganizationalClientContext : ExternalClientContext
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001EB04 File Offset: 0x0001CD04
		internal OrganizationalClientContext(SmtpAddress emailAddress, string requestorDomain, WSSecurityHeader wsSecurityHeader, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId) : base(emailAddress, wsSecurityHeader, budget, timeZone, clientCulture, messageId)
		{
			this.RequestorDomain = requestorDomain;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001EB1D File Offset: 0x0001CD1D
		public override ProxyAuthenticator CreateInternalProxyAuthenticator()
		{
			return ProxyAuthenticator.Create(base.WSSecurityHeader, null, base.MessageId);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001EB31 File Offset: 0x0001CD31
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001EB39 File Offset: 0x0001CD39
		public string RequestorDomain { get; private set; }

		// Token: 0x06000700 RID: 1792 RVA: 0x0001EB42 File Offset: 0x0001CD42
		public override string ToString()
		{
			return "<organizational>" + this.RequestorDomain;
		}
	}
}
