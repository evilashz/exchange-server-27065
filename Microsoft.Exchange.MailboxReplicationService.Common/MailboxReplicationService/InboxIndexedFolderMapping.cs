using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000118 RID: 280
	internal class InboxIndexedFolderMapping : InboxFolderMapping
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00013DDC File Offset: 0x00011FDC
		public InboxIndexedFolderMapping(WellKnownFolderType wkft, ExtraPropTag ptag, int entryIndex) : base(wkft, ptag)
		{
			this.EntryIndex = entryIndex;
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00013DED File Offset: 0x00011FED
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00013DF5 File Offset: 0x00011FF5
		public int EntryIndex { get; protected set; }
	}
}
