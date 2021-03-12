using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200025D RID: 605
	internal interface IActionsSource
	{
		// Token: 0x06001EDD RID: 7901
		IEnumerable<ReplayAction> ReadActions(IActionWatermark watermark);
	}
}
