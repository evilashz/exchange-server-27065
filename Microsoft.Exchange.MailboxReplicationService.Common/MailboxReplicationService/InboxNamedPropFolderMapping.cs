using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000117 RID: 279
	internal class InboxNamedPropFolderMapping : InboxFolderMapping
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00013DBA File Offset: 0x00011FBA
		public InboxNamedPropFolderMapping(WellKnownFolderType wkft, NamedPropData namedPropData) : base(wkft, PropTag.Null)
		{
			this.NamedPropData = namedPropData;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00013DCB File Offset: 0x00011FCB
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00013DD3 File Offset: 0x00011FD3
		public NamedPropData NamedPropData { get; protected set; }
	}
}
