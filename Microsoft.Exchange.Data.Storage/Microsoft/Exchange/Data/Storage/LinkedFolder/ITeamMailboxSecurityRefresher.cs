using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000973 RID: 2419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ITeamMailboxSecurityRefresher
	{
		// Token: 0x060059A1 RID: 22945
		void Refresh(ADUser mailbox, IRecipientSession session);
	}
}
