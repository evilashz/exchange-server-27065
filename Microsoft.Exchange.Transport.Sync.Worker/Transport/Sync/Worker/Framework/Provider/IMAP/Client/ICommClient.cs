using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001CC RID: 460
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ICommClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000D6D RID: 3437
		long TotalBytesSent { get; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000D6E RID: 3438
		long TotalBytesReceived { get; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000D6F RID: 3439
		bool IsConnected { get; }

		// Token: 0x06000D70 RID: 3440
		IAsyncResult BeginConnect(IMAPClientState clientState, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000D71 RID: 3441
		AsyncOperationResult<IMAPResultData> EndConnect(IAsyncResult asyncResult);

		// Token: 0x06000D72 RID: 3442
		IAsyncResult BeginNegotiateTlsAsClient(IMAPClientState clientState, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000D73 RID: 3443
		AsyncOperationResult<IMAPResultData> EndNegotiateTlsAsClient(IAsyncResult asyncResult);

		// Token: 0x06000D74 RID: 3444
		IAsyncResult BeginCommand(IMAPCommand command, IMAPClientState clientState, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000D75 RID: 3445
		IAsyncResult BeginCommand(IMAPCommand command, bool processResponse, IMAPClientState clientState, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000D76 RID: 3446
		AsyncOperationResult<IMAPResultData> EndCommand(IAsyncResult asyncResult);

		// Token: 0x06000D77 RID: 3447
		void Cancel();
	}
}
