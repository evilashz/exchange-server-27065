using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200001E RID: 30
	internal sealed class MailboxSessionCacheKey : IEquatable<MailboxSessionCacheKey>
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000060E3 File Offset: 0x000042E3
		internal MailboxSessionCacheKey(IExchangePrincipal exchangePrincipal)
		{
			this.mdbGuid = exchangePrincipal.MailboxInfo.GetDatabaseGuid();
			this.mailboxGuid = exchangePrincipal.MailboxInfo.MailboxGuid;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000610D File Offset: 0x0000430D
		internal MailboxSessionCacheKey(Guid mdbGuid, Guid mailboxGuid)
		{
			this.mdbGuid = mdbGuid;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006123 File Offset: 0x00004323
		internal Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000612B File Offset: 0x0000432B
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006133 File Offset: 0x00004333
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MailboxSessionCacheKey);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006144 File Offset: 0x00004344
		public override int GetHashCode()
		{
			return this.MdbGuid.GetHashCode() ^ this.MailboxGuid.GetHashCode();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000617C File Offset: 0x0000437C
		public bool Equals(MailboxSessionCacheKey other)
		{
			return other != null && this.MdbGuid.Equals(other.MdbGuid) && this.MailboxGuid.Equals(other.MailboxGuid);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000061BB File Offset: 0x000043BB
		public override string ToString()
		{
			return string.Format("[Mdb:{0},Mailbox:{1}]", this.MdbGuid, this.MailboxGuid);
		}

		// Token: 0x04000086 RID: 134
		private readonly Guid mdbGuid;

		// Token: 0x04000087 RID: 135
		private readonly Guid mailboxGuid;
	}
}
