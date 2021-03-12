using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000012 RID: 18
	internal abstract class MailboxData
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003EF7 File Offset: 0x000020F7
		protected MailboxData(Guid mailboxGuid, Guid databaseGuid, string displayName)
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("databaseGuid");
			}
			this.mailboxGuid = mailboxGuid;
			this.displayName = displayName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003F2C File Offset: 0x0000212C
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003F34 File Offset: 0x00002134
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003F3C File Offset: 0x0000213C
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003F44 File Offset: 0x00002144
		public bool Equals(MailboxData other)
		{
			return other != null && this.DatabaseGuid == other.DatabaseGuid;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003F5C File Offset: 0x0000215C
		public override int GetHashCode()
		{
			return this.DatabaseGuid.GetHashCode();
		}

		// Token: 0x0400009F RID: 159
		private readonly Guid mailboxGuid;

		// Token: 0x040000A0 RID: 160
		private readonly string displayName;

		// Token: 0x040000A1 RID: 161
		private readonly Guid databaseGuid;
	}
}
