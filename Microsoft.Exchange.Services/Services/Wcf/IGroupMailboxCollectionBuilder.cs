using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.GroupMailbox;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008A RID: 138
	internal interface IGroupMailboxCollectionBuilder
	{
		// Token: 0x0600036E RID: 878
		List<GroupMailbox> BuildGroupMailboxes(string[] externalIds);
	}
}
