using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000108 RID: 264
	internal class DownlevelBadItemsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00012A46 File Offset: 0x00010C46
		public DownlevelBadItemsPermanentException(List<BadMessageRec> badItems) : base(new LocalizedString("DownlevelBadItemsPermanentException"))
		{
			this.BadItems = badItems;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00012A5F File Offset: 0x00010C5F
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00012A67 File Offset: 0x00010C67
		public List<BadMessageRec> BadItems { get; private set; }
	}
}
