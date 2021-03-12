using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BF RID: 447
	internal interface IEwsClient
	{
		// Token: 0x06000C18 RID: 3096
		IAsyncResult BeginEwsCall(AsyncCallback callback, object state);

		// Token: 0x06000C19 RID: 3097
		ServiceResponse EndEwsCall(IAsyncResult result);
	}
}
