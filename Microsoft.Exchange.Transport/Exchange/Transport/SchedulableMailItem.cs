using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200019F RID: 415
	internal class SchedulableMailItem : ISchedulableMessage
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x00049350 File Offset: 0x00047550
		public SchedulableMailItem(TransportMessageId transportMessageId, MessageEnvelope messageEnvelope, IEnumerable<IMessageScope> scopes, DateTime submitTime)
		{
			ArgumentValidator.ThrowIfNull("transportMessageId", transportMessageId);
			ArgumentValidator.ThrowIfNull("messageEnvelope", messageEnvelope);
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			this.transportMessageId = transportMessageId;
			this.messageEnvelope = messageEnvelope;
			this.scopes = scopes;
			this.submitTime = submitTime;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000493A1 File Offset: 0x000475A1
		public TransportMessageId Id
		{
			get
			{
				return this.transportMessageId;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x000493A9 File Offset: 0x000475A9
		public DateTime SubmitTime
		{
			get
			{
				return this.submitTime;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000493B1 File Offset: 0x000475B1
		public IEnumerable<IMessageScope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000493B9 File Offset: 0x000475B9
		public MessageEnvelope MessageEnvelope
		{
			get
			{
				return this.messageEnvelope;
			}
		}

		// Token: 0x0400097E RID: 2430
		private readonly TransportMessageId transportMessageId;

		// Token: 0x0400097F RID: 2431
		private readonly MessageEnvelope messageEnvelope;

		// Token: 0x04000980 RID: 2432
		private readonly IEnumerable<IMessageScope> scopes;

		// Token: 0x04000981 RID: 2433
		private readonly DateTime submitTime;
	}
}
