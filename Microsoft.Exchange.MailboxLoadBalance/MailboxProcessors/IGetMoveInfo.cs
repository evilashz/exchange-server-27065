using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000B6 RID: 182
	internal interface IGetMoveInfo
	{
		// Token: 0x06000606 RID: 1542
		MoveInfo GetInfo(DirectoryMailbox mailbox, IAnchorRunspaceProxy runspace);
	}
}
