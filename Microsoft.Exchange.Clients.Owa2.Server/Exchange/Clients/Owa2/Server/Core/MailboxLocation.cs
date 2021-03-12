using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200017E RID: 382
	internal sealed class MailboxLocation : NotificationLocation
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00034A2A File Offset: 0x00032C2A
		public MailboxLocation(Guid mailboxGuid)
		{
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Mailbox guid cannot be empty.", "mailboxGuid");
			}
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00034A56 File Offset: 0x00032C56
		public static MailboxLocation FromMailboxContext(IMailboxContext mailboxContext)
		{
			if (mailboxContext != null && mailboxContext.ExchangePrincipal != null)
			{
				return new MailboxLocation(mailboxContext.ExchangePrincipal.MailboxInfo.MailboxGuid);
			}
			return null;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00034A7A File Offset: 0x00032C7A
		public override KeyValuePair<string, object> GetEventData()
		{
			return new KeyValuePair<string, object>("MailboxGuid", this.mailboxGuid);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00034A94 File Offset: 0x00032C94
		public override int GetHashCode()
		{
			return MailboxLocation.TypeHashCode ^ this.mailboxGuid.GetHashCode();
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00034ABC File Offset: 0x00032CBC
		public override bool Equals(object obj)
		{
			MailboxLocation mailboxLocation = obj as MailboxLocation;
			return mailboxLocation != null && this.mailboxGuid.Equals(mailboxLocation.mailboxGuid);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00034AEC File Offset: 0x00032CEC
		public override string ToString()
		{
			return this.mailboxGuid.ToString();
		}

		// Token: 0x04000868 RID: 2152
		private const string EventKey = "MailboxGuid";

		// Token: 0x04000869 RID: 2153
		private static readonly int TypeHashCode = typeof(MailboxLocation).GetHashCode();

		// Token: 0x0400086A RID: 2154
		private readonly Guid mailboxGuid;
	}
}
