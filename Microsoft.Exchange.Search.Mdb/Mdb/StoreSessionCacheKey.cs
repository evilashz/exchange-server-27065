using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200002F RID: 47
	internal sealed class StoreSessionCacheKey : IEquatable<StoreSessionCacheKey>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000BF1C File Offset: 0x0000A11C
		internal StoreSessionCacheKey(Guid mdbGuid, Guid mailboxGuid, bool isMoveDestination)
		{
			this.mdbGuid = mdbGuid;
			this.mailboxGuid = mailboxGuid;
			this.isMoveDestination = isMoveDestination;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000BF39 File Offset: 0x0000A139
		internal Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000BF41 File Offset: 0x0000A141
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000BF49 File Offset: 0x0000A149
		internal bool IsMoveDestination
		{
			get
			{
				return this.isMoveDestination;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BF51 File Offset: 0x0000A151
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StoreSessionCacheKey);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BF60 File Offset: 0x0000A160
		public override int GetHashCode()
		{
			return this.MdbGuid.GetHashCode() ^ this.MailboxGuid.GetHashCode();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BF98 File Offset: 0x0000A198
		public bool Equals(StoreSessionCacheKey other)
		{
			return other != null && this.MdbGuid.Equals(other.MdbGuid) && this.MailboxGuid.Equals(other.MailboxGuid) && this.IsMoveDestination == other.IsMoveDestination;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
		public override string ToString()
		{
			return string.Format("[Mdb:{0},Mailbox:{1},IsMoveDestination{2}]", this.MdbGuid, this.MailboxGuid, this.IsMoveDestination);
		}

		// Token: 0x04000106 RID: 262
		private readonly Guid mdbGuid;

		// Token: 0x04000107 RID: 263
		private readonly Guid mailboxGuid;

		// Token: 0x04000108 RID: 264
		private readonly bool isMoveDestination;
	}
}
