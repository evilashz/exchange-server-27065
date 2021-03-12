using System;
using System.IO;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000040 RID: 64
	internal interface IStreamManager
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000139 RID: 313
		// (set) Token: 0x0600013A RID: 314
		int ListenPort { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013B RID: 315
		// (set) Token: 0x0600013C RID: 316
		TimeSpan CacheTimeout { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		TimeSpan ConnectionTimeout { get; set; }

		// Token: 0x0600013F RID: 319
		void StartListening();

		// Token: 0x06000140 RID: 320
		void StopListening();

		// Token: 0x06000141 RID: 321
		Stream WaitForConnection(Guid contextId);

		// Token: 0x06000142 RID: 322
		ICancelableAsyncResult BeginWaitForConnection(Guid contextId, AsyncCallback callback, object state);

		// Token: 0x06000143 RID: 323
		Stream EndWaitForConnection(IAsyncResult asyncResult);

		// Token: 0x06000144 RID: 324
		Stream Connect(int port, Guid contextId);

		// Token: 0x06000145 RID: 325
		ICancelableAsyncResult BeginConnect(int port, Guid contextId, AsyncCallback callback, object state);

		// Token: 0x06000146 RID: 326
		Stream EndConnect(IAsyncResult asyncResult);

		// Token: 0x06000147 RID: 327
		void CancelPendingOperation(Guid contextId);

		// Token: 0x06000148 RID: 328
		void CheckIn(Stream channel);

		// Token: 0x06000149 RID: 329
		Stream CheckOut(Guid contextId);
	}
}
