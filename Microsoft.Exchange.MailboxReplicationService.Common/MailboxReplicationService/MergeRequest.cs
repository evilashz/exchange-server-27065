using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public sealed class MergeRequest : RequestBase
	{
		// Token: 0x0600144C RID: 5196 RVA: 0x0002E557 File Offset: 0x0002C757
		public MergeRequest()
		{
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0002E55F File Offset: 0x0002C75F
		internal MergeRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0002E568 File Offset: 0x0002C768
		public new ADObjectId SourceMailbox
		{
			get
			{
				return base.SourceMailbox;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0002E570 File Offset: 0x0002C770
		public new ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0002E578 File Offset: 0x0002C778
		public override string ToString()
		{
			if (base.Name == null || ((this.TargetMailbox == null || (base.Flags & RequestFlags.Pull) != RequestFlags.Pull) && (this.SourceMailbox == null || (base.Flags & RequestFlags.Push) != RequestFlags.Push)))
			{
				return base.ToString();
			}
			if (this.TargetMailbox != null && (base.Flags & RequestFlags.Pull) == RequestFlags.Pull)
			{
				return string.Format("{0}\\{1}", this.TargetMailbox.ToString(), base.Name);
			}
			return string.Format("{0}\\{1}", this.SourceMailbox.ToString(), base.Name);
		}
	}
}
