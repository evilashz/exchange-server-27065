using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	public sealed class FolderMoveRequest : RequestBase
	{
		// Token: 0x06001431 RID: 5169 RVA: 0x0002E265 File Offset: 0x0002C465
		public FolderMoveRequest()
		{
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0002E26D File Offset: 0x0002C46D
		internal FolderMoveRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0002E276 File Offset: 0x0002C476
		public new ADObjectId SourceMailbox
		{
			get
			{
				return base.SourceMailbox;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0002E27E File Offset: 0x0002C47E
		public new ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0002E286 File Offset: 0x0002C486
		public override string ToString()
		{
			if (base.Name != null && this.TargetMailbox != null)
			{
				return string.Format("{0}\\{1}", this.TargetMailbox.ToString(), base.Name);
			}
			return base.ToString();
		}
	}
}
