using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000407 RID: 1031
	internal class RecipientCorrelator : IEnumerable<MailRecipient>, IEnumerable
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000BE5EB File Offset: 0x000BC7EB
		public int Count
		{
			get
			{
				return this.recipientList.Count;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000BE5F8 File Offset: 0x000BC7F8
		public List<MailRecipient> Recipients
		{
			get
			{
				return this.recipientList;
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000BE600 File Offset: 0x000BC800
		public virtual void Add(MailRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("Can't add a null recipient to the recipient correlator");
			}
			this.recipientList.Add(recipient);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000BE61C File Offset: 0x000BC81C
		public MailRecipient Find(RoutingAddress recipientAddress)
		{
			foreach (MailRecipient mailRecipient in this.recipientList)
			{
				if (mailRecipient.Email == recipientAddress)
				{
					return mailRecipient;
				}
			}
			return null;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000BE680 File Offset: 0x000BC880
		public IEnumerator<MailRecipient> GetEnumerator()
		{
			return this.recipientList.GetEnumerator();
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000BE692 File Offset: 0x000BC892
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400175C RID: 5980
		protected List<MailRecipient> recipientList = new List<MailRecipient>();
	}
}
