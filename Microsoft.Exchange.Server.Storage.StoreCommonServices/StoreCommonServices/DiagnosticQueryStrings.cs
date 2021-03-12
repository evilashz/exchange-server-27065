using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200005A RID: 90
	internal static class DiagnosticQueryStrings
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0001808C File Offset: 0x0001628C
		public static string UnableToLockMailbox(int mailboxNumber)
		{
			return string.Format("Unable to lock mailbox with MailboxNumber = {0}.", mailboxNumber);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001809E File Offset: 0x0001629E
		public static string MailboxStateNotFound(int mailboxNumber)
		{
			return string.Format("MailboxState is not found for mailbox with MailboxNumber = {0}.", mailboxNumber);
		}
	}
}
