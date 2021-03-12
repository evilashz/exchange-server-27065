using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001EE RID: 494
	[Serializable]
	public sealed class SyncRequest : RequestBase
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x0002EC80 File Offset: 0x0002CE80
		public SyncRequest()
		{
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0002EC88 File Offset: 0x0002CE88
		internal SyncRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0002EC91 File Offset: 0x0002CE91
		public ADObjectId Mailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0002EC99 File Offset: 0x0002CE99
		public string RemoteServerName
		{
			get
			{
				return base.RemoteHostName;
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0002ECA1 File Offset: 0x0002CEA1
		public override string ToString()
		{
			return string.Format("{0}\\{1}", base.TargetMailbox, base.Name);
		}
	}
}
