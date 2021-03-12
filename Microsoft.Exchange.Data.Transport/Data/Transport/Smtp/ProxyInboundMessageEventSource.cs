using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000048 RID: 72
	internal abstract class ProxyInboundMessageEventSource : ReceiveEventSource
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000063A0 File Offset: 0x000045A0
		protected ProxyInboundMessageEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x060001AE RID: 430
		public abstract void SetProxyRoutingOverride(IEnumerable<INextHopServer> destinationServers, bool internalDestination);
	}
}
