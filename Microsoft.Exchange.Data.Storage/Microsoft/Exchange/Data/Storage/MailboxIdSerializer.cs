using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D89 RID: 3465
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MailboxIdSerializer
	{
		// Token: 0x06007750 RID: 30544 RVA: 0x0020E72D File Offset: 0x0020C92D
		public static string EmailAddressFromBytes(byte[] moniker)
		{
			if (moniker.Length > 1000)
			{
				throw new InvalidIdMalformedException();
			}
			return Encoding.UTF8.GetString(moniker, 0, moniker.Length);
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x0020E74E File Offset: 0x0020C94E
		public static int EmailAddressToByteCount(string smtpAddress)
		{
			return Encoding.UTF8.GetByteCount(smtpAddress);
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x0020E75B File Offset: 0x0020C95B
		public static int EmailAddressToBytes(string smtpAddress, byte[] bytes, int index)
		{
			return Encoding.UTF8.GetBytes(smtpAddress, 0, smtpAddress.Length, bytes, index);
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x0020E771 File Offset: 0x0020C971
		public static Guid MailboxGuidFromBytes(byte[] moniker)
		{
			if (moniker.Length > 50)
			{
				throw new InvalidIdMalformedException();
			}
			return new Guid(Encoding.UTF8.GetString(moniker, 0, moniker.Length));
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x0020E794 File Offset: 0x0020C994
		public static int MailboxGuidToByteCount(string mailboxGuid)
		{
			return Encoding.UTF8.GetByteCount(mailboxGuid);
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x0020E7A1 File Offset: 0x0020C9A1
		public static int MailboxGuidToBytes(string mailboxGuid, byte[] bytes, int index)
		{
			return Encoding.UTF8.GetBytes(mailboxGuid, 0, mailboxGuid.Length, bytes, index);
		}

		// Token: 0x040052A0 RID: 21152
		private const int MaxEmailAddressLength = 1000;

		// Token: 0x040052A1 RID: 21153
		private const int MaxMailboxGuidLength = 50;
	}
}
