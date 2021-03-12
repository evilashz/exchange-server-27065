using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000114 RID: 276
	internal abstract class PropTagFolderMapping : WellKnownFolderMapping
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00013D49 File Offset: 0x00011F49
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00013D51 File Offset: 0x00011F51
		public PropTag Ptag { get; protected set; }
	}
}
