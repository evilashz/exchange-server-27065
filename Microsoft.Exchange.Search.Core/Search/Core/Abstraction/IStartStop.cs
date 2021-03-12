using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000033 RID: 51
	internal interface IStartStop : IDisposable
	{
		// Token: 0x06000119 RID: 281
		IAsyncResult BeginPrepareToStart(AsyncCallback callback, object context);

		// Token: 0x0600011A RID: 282
		void EndPrepareToStart(IAsyncResult asyncResult);

		// Token: 0x0600011B RID: 283
		IAsyncResult BeginStart(AsyncCallback callback, object context);

		// Token: 0x0600011C RID: 284
		void EndStart(IAsyncResult asyncResult);

		// Token: 0x0600011D RID: 285
		IAsyncResult BeginStop(AsyncCallback callback, object context);

		// Token: 0x0600011E RID: 286
		void EndStop(IAsyncResult asyncResult);
	}
}
