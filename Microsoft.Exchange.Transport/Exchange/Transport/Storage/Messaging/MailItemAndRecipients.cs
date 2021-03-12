using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000F5 RID: 245
	internal struct MailItemAndRecipients
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x000235D9 File Offset: 0x000217D9
		public MailItemAndRecipients(IMailItemStorage mailItem, IEnumerable<IMailRecipientStorage> recipients)
		{
			this.mailItem = mailItem;
			this.recipients = recipients;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000235E9 File Offset: 0x000217E9
		public IMailItemStorage MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000235F1 File Offset: 0x000217F1
		public IEnumerable<IMailRecipientStorage> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x0400043B RID: 1083
		private readonly IMailItemStorage mailItem;

		// Token: 0x0400043C RID: 1084
		private readonly IEnumerable<IMailRecipientStorage> recipients;
	}
}
