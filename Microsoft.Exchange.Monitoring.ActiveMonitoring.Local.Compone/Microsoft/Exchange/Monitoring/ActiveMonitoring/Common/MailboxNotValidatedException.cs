using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000547 RID: 1351
	public class MailboxNotValidatedException : Exception
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000CB6FB File Offset: 0x000C98FB
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x000CB703 File Offset: 0x000C9903
		public string Password { get; private set; }

		// Token: 0x0600214B RID: 8523 RVA: 0x000CB70C File Offset: 0x000C990C
		public MailboxNotValidatedException(string password) : base("You tried to use an unverified monitoring mailbox without writing your code to handle this situation correctly. Please change your code to use the list of mailboxes with verified credentials provided by the MailboxDatabaseEndpoint.")
		{
			this.Password = password;
		}
	}
}
