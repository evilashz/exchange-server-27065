using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000027 RID: 39
	public sealed class MailboxEntry
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00006C7B File Offset: 0x00004E7B
		public MailboxEntry(int mailboxNumber, int mailboxPartitionNumber, Guid mailboxGuid, string mailboxOwnerName)
		{
			this.mailboxNumber = mailboxNumber;
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.mailboxGuid = mailboxGuid;
			this.mailboxOwnerName = mailboxOwnerName;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006CA0 File Offset: 0x00004EA0
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public string MailboxOwnerName
		{
			get
			{
				return this.mailboxOwnerName;
			}
		}

		// Token: 0x0400009A RID: 154
		private readonly int mailboxNumber;

		// Token: 0x0400009B RID: 155
		private readonly int mailboxPartitionNumber;

		// Token: 0x0400009C RID: 156
		private readonly Guid mailboxGuid;

		// Token: 0x0400009D RID: 157
		private readonly string mailboxOwnerName;
	}
}
