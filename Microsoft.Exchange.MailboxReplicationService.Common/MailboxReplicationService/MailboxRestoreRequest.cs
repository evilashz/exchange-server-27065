using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	public sealed class MailboxRestoreRequest : RequestBase
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x0002E502 File Offset: 0x0002C702
		public MailboxRestoreRequest()
		{
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0002E50A File Offset: 0x0002C70A
		internal MailboxRestoreRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0002E513 File Offset: 0x0002C713
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0002E51B File Offset: 0x0002C71B
		public new ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0002E523 File Offset: 0x0002C723
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
