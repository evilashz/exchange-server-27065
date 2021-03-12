using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E4 RID: 484
	[Serializable]
	public sealed class MailboxImportRequest : RequestBase
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x0002E425 File Offset: 0x0002C625
		public MailboxImportRequest()
		{
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0002E42D File Offset: 0x0002C62D
		internal MailboxImportRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0002E436 File Offset: 0x0002C636
		public new string FilePath
		{
			get
			{
				return base.FilePath;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0002E43E File Offset: 0x0002C63E
		public ADObjectId Mailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0002E446 File Offset: 0x0002C646
		public override string ToString()
		{
			if (base.Name != null && base.TargetMailbox != null)
			{
				return string.Format("{0}\\{1}", base.TargetMailbox.ToString(), base.Name);
			}
			return base.ToString();
		}
	}
}
