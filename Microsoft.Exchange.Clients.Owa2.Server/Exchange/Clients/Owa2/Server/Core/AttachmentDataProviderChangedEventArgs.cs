using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200002D RID: 45
	public class AttachmentDataProviderChangedEventArgs : EventArgs
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00003A05 File Offset: 0x00001C05
		internal AttachmentDataProviderChangedEventArgs(MailboxSession mailboxSession)
		{
			this.MailboxSession = mailboxSession;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003A14 File Offset: 0x00001C14
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal MailboxSession MailboxSession { get; private set; }
	}
}
