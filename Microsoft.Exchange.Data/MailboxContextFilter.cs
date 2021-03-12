using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal class MailboxContextFilter : QueryFilter
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00008409 File Offset: 0x00006609
		public MailboxContextFilter(Guid mailboxGuid)
		{
			this.mailboxGuid = mailboxGuid;
			this.mailboxFlags = 0UL;
			this.noADLookup = false;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008427 File Offset: 0x00006627
		public MailboxContextFilter(Guid mailboxGuid, ulong mailboxFlags)
		{
			this.mailboxGuid = mailboxGuid;
			this.mailboxFlags = mailboxFlags;
			this.noADLookup = false;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008444 File Offset: 0x00006644
		public MailboxContextFilter(Guid mailboxGuid, ulong mailboxFlags, bool noADLookup)
		{
			this.mailboxGuid = mailboxGuid;
			this.mailboxFlags = mailboxFlags;
			this.noADLookup = noADLookup;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008464 File Offset: 0x00006664
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.mailboxGuid.ToString());
			sb.Append(" ");
			sb.Append(this.mailboxFlags.ToString());
			sb.Append(" ");
			sb.Append(this.noADLookup.ToString());
			sb.Append(")");
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000084E6 File Offset: 0x000066E6
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000084EE File Offset: 0x000066EE
		public ulong MailboxFlags
		{
			get
			{
				return this.mailboxFlags;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000084F6 File Offset: 0x000066F6
		public bool NoADLookup
		{
			get
			{
				return this.noADLookup;
			}
		}

		// Token: 0x040000AB RID: 171
		private readonly Guid mailboxGuid;

		// Token: 0x040000AC RID: 172
		private readonly ulong mailboxFlags;

		// Token: 0x040000AD RID: 173
		private readonly bool noADLookup;
	}
}
