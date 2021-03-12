using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E9 RID: 489
	internal interface IAutodiscoveryClient
	{
		// Token: 0x06000CD5 RID: 3285
		IAsyncResult BeginAutodiscover(AsyncCallback callback, object state);

		// Token: 0x06000CD6 RID: 3286
		Dictionary<GroupId, List<MailboxInfo>> EndAutodiscover(IAsyncResult result);

		// Token: 0x06000CD7 RID: 3287
		void CancelAutodiscover();
	}
}
