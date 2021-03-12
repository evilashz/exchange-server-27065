using System;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003CC RID: 972
	internal class InternalOpenConnectionEventSource : OpenConnectionEventSource
	{
		// Token: 0x06002C82 RID: 11394 RVA: 0x000B1571 File Offset: 0x000AF771
		public InternalOpenConnectionEventSource(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, RoutedMailItemWrapper deliverableMailItem, ulong sessionId, NextHopConnection nextHopConnection, DeliveryAgentConnection.Stats stats)
		{
			this.source = new InternalDeliveryAgentEventSource(mexSession, deliverableMailItem, sessionId, nextHopConnection, null, stats);
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000B158C File Offset: 0x000AF78C
		public InternalDeliveryAgentEventSource InternalEventSource
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000B1594 File Offset: 0x000AF794
		public override void FailQueue(SmtpResponse smtpResponse)
		{
			this.source.FailQueue(smtpResponse);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000B15A2 File Offset: 0x000AF7A2
		public override void DeferQueue(SmtpResponse smtpResponse)
		{
			this.source.DeferQueue(smtpResponse);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000B15B0 File Offset: 0x000AF7B0
		public override void DeferQueue(SmtpResponse smtpResponse, TimeSpan interval)
		{
			this.source.DeferQueue(smtpResponse, interval);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000B15BF File Offset: 0x000AF7BF
		public override void RegisterConnection(string remoteHost, SmtpResponse smtpResponse)
		{
			this.source.RegisterConnection(remoteHost, smtpResponse);
		}

		// Token: 0x04001643 RID: 5699
		private InternalDeliveryAgentEventSource source;
	}
}
