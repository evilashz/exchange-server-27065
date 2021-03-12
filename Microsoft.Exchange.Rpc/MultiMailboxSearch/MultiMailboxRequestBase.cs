using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	internal abstract class MultiMailboxRequestBase : MultiMailboxSearchBase
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00009E4C File Offset: 0x0000924C
		internal MultiMailboxRequestBase(int version, MultiMailboxSearchMailboxInfo[] mailboxInfos) : base(version)
		{
			this.mailboxInfos = mailboxInfos;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00009E2C File Offset: 0x0000922C
		internal MultiMailboxRequestBase(MultiMailboxSearchMailboxInfo[] mailboxInfos) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.mailboxInfos = mailboxInfos;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00009E18 File Offset: 0x00009218
		internal MultiMailboxRequestBase(int version) : base(version)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00009E00 File Offset: 0x00009200
		internal MultiMailboxRequestBase() : base(MultiMailboxSearchBase.CurrentVersion)
		{
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00009E68 File Offset: 0x00009268
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00009E7C File Offset: 0x0000927C
		internal MultiMailboxSearchMailboxInfo[] MailboxInfos
		{
			get
			{
				return this.mailboxInfos;
			}
			set
			{
				this.mailboxInfos = value;
			}
		}

		// Token: 0x04000B0C RID: 2828
		private MultiMailboxSearchMailboxInfo[] mailboxInfos;
	}
}
