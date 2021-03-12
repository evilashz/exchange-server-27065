using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000116 RID: 278
	internal class InboxFolderMapping : PropTagFolderMapping
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00013D8E File Offset: 0x00011F8E
		public InboxFolderMapping(WellKnownFolderType wkft, PropTag ptag)
		{
			base.Ptag = ptag;
			base.WKFType = wkft;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00013DA4 File Offset: 0x00011FA4
		public InboxFolderMapping(WellKnownFolderType wkft, ExtraPropTag ptag)
		{
			base.Ptag = (PropTag)ptag;
			base.WKFType = wkft;
		}
	}
}
