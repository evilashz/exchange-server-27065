using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x0200022A RID: 554
	public class SharePointSignalStoreException : Exception
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x00078325 File Offset: 0x00076525
		public SharePointSignalStoreException()
		{
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0007832D File Offset: 0x0007652D
		public SharePointSignalStoreException(string message) : base(message)
		{
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00078336 File Offset: 0x00076536
		public SharePointSignalStoreException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
