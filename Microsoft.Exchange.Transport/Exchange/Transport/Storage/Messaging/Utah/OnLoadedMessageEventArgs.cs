using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Storage;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000309 RID: 777
	internal class OnLoadedMessageEventArgs : StorageEventArgs
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x00080185 File Offset: 0x0007E385
		public OnLoadedMessageEventArgs(MailItem mailItem)
		{
			this.mailItem = mailItem;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x00080194 File Offset: 0x0007E394
		public override MailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x040011C1 RID: 4545
		private MailItem mailItem;
	}
}
