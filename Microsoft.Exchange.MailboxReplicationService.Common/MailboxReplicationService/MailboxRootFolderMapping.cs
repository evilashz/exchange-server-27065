using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000115 RID: 277
	internal class MailboxRootFolderMapping : PropTagFolderMapping
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x00013D62 File Offset: 0x00011F62
		public MailboxRootFolderMapping(WellKnownFolderType wkft, PropTag ptag)
		{
			base.Ptag = ptag;
			base.WKFType = wkft;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00013D78 File Offset: 0x00011F78
		public MailboxRootFolderMapping(WellKnownFolderType wkft, ExtraPropTag ptag)
		{
			base.Ptag = (PropTag)ptag;
			base.WKFType = wkft;
		}
	}
}
