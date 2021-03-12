using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000009 RID: 9
	internal sealed class EnvelopeRecipientCollectionWrapper : EnvelopeRecipientCollection
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002E33 File Offset: 0x00001033
		internal EnvelopeRecipientCollectionWrapper(ReadOnlyEnvelopeRecipientCollection recipients)
		{
			this.recipients = recipients;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E42 File Offset: 0x00001042
		public override int Count
		{
			get
			{
				return this.recipients.Count;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E4F File Offset: 0x0000104F
		public override bool CanAdd
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000025 RID: 37
		public override EnvelopeRecipient this[int index]
		{
			get
			{
				return this.recipients[index];
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E64 File Offset: 0x00001064
		public override bool Contains(RoutingAddress address)
		{
			return this.recipients.Contains(address);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E72 File Offset: 0x00001072
		public override EnvelopeRecipientCollection.Enumerator GetEnumerator()
		{
			return this.recipients.GetEnumerator();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E7F File Offset: 0x0000107F
		public override void Add(RoutingAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E86 File Offset: 0x00001086
		public override void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E8D File Offset: 0x0000108D
		public override bool Remove(EnvelopeRecipient recipient)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E94 File Offset: 0x00001094
		public override int Remove(RoutingAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E9B File Offset: 0x0000109B
		public override bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EA2 File Offset: 0x000010A2
		public override bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse, string sourceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400001E RID: 30
		private ReadOnlyEnvelopeRecipientCollection recipients;
	}
}
