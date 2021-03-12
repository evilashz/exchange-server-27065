using System;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C7 RID: 967
	internal class InternalCloseConnectionEventSource : CloseConnectionEventSource
	{
		// Token: 0x06002C45 RID: 11333 RVA: 0x000B0BDC File Offset: 0x000AEDDC
		public InternalCloseConnectionEventSource(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, ulong sessionId, string remoteHost, NextHopConnection nextHopConnection, DeliveryAgentConnection.Stats stats)
		{
			this.source = new InternalDeliveryAgentEventSource(mexSession, null, sessionId, nextHopConnection, remoteHost, stats);
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000B0BF7 File Offset: 0x000AEDF7
		public bool ConnectionUnregistered
		{
			get
			{
				return this.source.ConnectionUnregistered;
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000B0C04 File Offset: 0x000AEE04
		public override void UnregisterConnection(SmtpResponse smtpResponse)
		{
			this.source.UnregisterConnection(smtpResponse);
		}

		// Token: 0x04001633 RID: 5683
		private InternalDeliveryAgentEventSource source;
	}
}
