using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	public sealed class MailboxExportRequest : RequestBase
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x0002E4AD File Offset: 0x0002C6AD
		public MailboxExportRequest()
		{
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		internal MailboxExportRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0002E4BE File Offset: 0x0002C6BE
		public new string FilePath
		{
			get
			{
				return base.FilePath;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0002E4C6 File Offset: 0x0002C6C6
		public ADObjectId Mailbox
		{
			get
			{
				return base.SourceMailbox;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0002E4CE File Offset: 0x0002C6CE
		public override string ToString()
		{
			if (base.Name != null && base.SourceMailbox != null)
			{
				return string.Format("{0}\\{1}", base.SourceMailbox.ToString(), base.Name);
			}
			return base.ToString();
		}
	}
}
