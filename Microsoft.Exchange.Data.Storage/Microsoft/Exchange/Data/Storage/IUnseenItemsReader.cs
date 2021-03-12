using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D9 RID: 2009
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUnseenItemsReader : IDisposable
	{
		// Token: 0x06004B42 RID: 19266
		void LoadLastNItemReceiveDates(IMailboxSession mailboxSession);

		// Token: 0x06004B43 RID: 19267
		int GetUnseenItemCount(ExDateTime lastVisitedDate);
	}
}
