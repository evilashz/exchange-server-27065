using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020003CD RID: 973
	internal class ReadOnlyMailRecipientCollectionWrapper : ReadOnlyEnvelopeRecipientCollection
	{
		// Token: 0x06002C88 RID: 11400 RVA: 0x000B15CE File Offset: 0x000AF7CE
		public ReadOnlyMailRecipientCollectionWrapper(IList<MailRecipient> recipients, IReadOnlyMailItem mailItem)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.recipients = recipients;
			this.mailItem = mailItem;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000B1600 File Offset: 0x000AF800
		public override int Count
		{
			get
			{
				return this.recipients.Count;
			}
		}

		// Token: 0x17000D7A RID: 3450
		public override EnvelopeRecipient this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException(Strings.IndexOutOfBounds(index, this.Count));
				}
				MailRecipient recipientObject = this.recipients[index];
				return this.CreateRecipientWrapper(recipientObject);
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000B1658 File Offset: 0x000AF858
		public override bool Contains(RoutingAddress address)
		{
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				if (mailRecipient.Email.Equals(address))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000B16B8 File Offset: 0x000AF8B8
		public override EnvelopeRecipientCollection.Enumerator GetEnumerator()
		{
			return new EnvelopeRecipientCollection.Enumerator(this.recipients, new Converter<object, EnvelopeRecipient>(this.CreateRecipientWrapper));
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000B16D1 File Offset: 0x000AF8D1
		private MailRecipientWrapper CreateRecipientWrapper(object recipientObject)
		{
			return new MailRecipientWrapper((MailRecipient)recipientObject, this.mailItem);
		}

		// Token: 0x04001644 RID: 5700
		private IList<MailRecipient> recipients;

		// Token: 0x04001645 RID: 5701
		private IReadOnlyMailItem mailItem;
	}
}
