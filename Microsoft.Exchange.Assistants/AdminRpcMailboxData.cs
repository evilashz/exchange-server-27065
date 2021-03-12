using System;
using System.Globalization;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000013 RID: 19
	internal class AdminRpcMailboxData : MailboxData
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003F7D File Offset: 0x0000217D
		public AdminRpcMailboxData(Guid mailboxGuid, int mailboxNumber, Guid databaseGuid) : base(mailboxGuid, databaseGuid, mailboxNumber.ToString(CultureInfo.InvariantCulture))
		{
			this.mailboxNumber = mailboxNumber;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003F9A File Offset: 0x0000219A
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003FA4 File Offset: 0x000021A4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			AdminRpcMailboxData adminRpcMailboxData = other as AdminRpcMailboxData;
			return adminRpcMailboxData != null && this.Equals(adminRpcMailboxData);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003FC9 File Offset: 0x000021C9
		public bool Equals(AdminRpcMailboxData other)
		{
			return other != null && this.mailboxNumber == other.MailboxNumber && base.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003FE8 File Offset: 0x000021E8
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.mailboxNumber.GetHashCode();
		}

		// Token: 0x040000A2 RID: 162
		private readonly int mailboxNumber;
	}
}
