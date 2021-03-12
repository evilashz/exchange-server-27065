using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	internal sealed class MultiMailboxSearchMailboxInfo : MultiMailboxSearchBase
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00009C58 File Offset: 0x00009058
		internal MultiMailboxSearchMailboxInfo(int version, Guid mailboxGuid, byte[] folderRestriction) : base(version)
		{
			this.mailboxGuid = mailboxGuid;
			this.folderRestriction = folderRestriction;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00009C30 File Offset: 0x00009030
		internal MultiMailboxSearchMailboxInfo(Guid mailboxGuid, byte[] folderRestriction) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.mailboxGuid = mailboxGuid;
			this.folderRestriction = folderRestriction;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00009C0C File Offset: 0x0000900C
		internal MultiMailboxSearchMailboxInfo(int version, Guid mailboxGuid) : base(version)
		{
			this.mailboxGuid = mailboxGuid;
			this.folderRestriction = null;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00009BE4 File Offset: 0x00008FE4
		internal MultiMailboxSearchMailboxInfo(Guid mailboxGuid) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.mailboxGuid = mailboxGuid;
			this.folderRestriction = null;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00009C7C File Offset: 0x0000907C
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00009C94 File Offset: 0x00009094
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.mailboxGuid = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00009CA8 File Offset: 0x000090A8
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00009CBC File Offset: 0x000090BC
		internal byte[] FolderRestriction
		{
			get
			{
				return this.folderRestriction;
			}
			set
			{
				this.folderRestriction = value;
			}
		}

		// Token: 0x04000B04 RID: 2820
		private Guid mailboxGuid;

		// Token: 0x04000B05 RID: 2821
		private byte[] folderRestriction;
	}
}
