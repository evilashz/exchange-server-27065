using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.WSSecurity;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000105 RID: 261
	internal sealed class PersonalClientContext : ExternalClientContext
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001EB54 File Offset: 0x0001CD54
		internal PersonalClientContext(SmtpAddress emailAddress, SmtpAddress externalId, WSSecurityHeader wsSecurityHeader, SharingSecurityHeader sharingSecurityHeader, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId) : base(emailAddress, wsSecurityHeader, budget, timeZone, clientCulture, messageId)
		{
			this.ExternalId = externalId;
			this.SharingSecurityHeader = sharingSecurityHeader;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001EB75 File Offset: 0x0001CD75
		public override ProxyAuthenticator CreateInternalProxyAuthenticator()
		{
			return ProxyAuthenticator.Create(base.WSSecurityHeader, this.SharingSecurityHeader, base.MessageId);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0001EB96 File Offset: 0x0001CD96
		public SmtpAddress ExternalId { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001EB9F File Offset: 0x0001CD9F
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001EBA7 File Offset: 0x0001CDA7
		public SharingSecurityHeader SharingSecurityHeader { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001EBB0 File Offset: 0x0001CDB0
		public override string IdentityForFilteredTracing
		{
			get
			{
				return this.ExternalId.ToString();
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001EBD1 File Offset: 0x0001CDD1
		public override string ToString()
		{
			return "<personal>" + this.ExternalId;
		}
	}
}
