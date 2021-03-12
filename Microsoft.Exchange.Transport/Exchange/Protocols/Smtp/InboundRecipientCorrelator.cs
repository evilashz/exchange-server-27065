using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000408 RID: 1032
	internal class InboundRecipientCorrelator : RecipientCorrelator
	{
		// Token: 0x06002F85 RID: 12165 RVA: 0x000BE6AD File Offset: 0x000BC8AD
		public override void Add(MailRecipient recipient)
		{
			base.Add(recipient);
			this.hashSet.Add((string)recipient.Email);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000BE6CC File Offset: 0x000BC8CC
		public bool Contains(string recipientAddress)
		{
			return this.hashSet.Contains(recipientAddress);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000BE6DA File Offset: 0x000BC8DA
		public void AddEmpty()
		{
			this.recipientList.Add(null);
		}

		// Token: 0x0400175D RID: 5981
		private HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}
