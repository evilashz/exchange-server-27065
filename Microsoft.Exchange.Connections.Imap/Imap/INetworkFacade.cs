using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface INetworkFacade : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011B RID: 283
		long TotalBytesSent { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011C RID: 284
		long TotalBytesReceived { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011D RID: 285
		bool IsConnected { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600011E RID: 286
		string Server { get; }

		// Token: 0x0600011F RID: 287
		IAsyncResult BeginConnect(ImapConnectionContext connectionContext, AsyncCallback callback, object callbackState);

		// Token: 0x06000120 RID: 288
		AsyncOperationResult<ImapResultData> EndConnect(IAsyncResult asyncResult);

		// Token: 0x06000121 RID: 289
		IAsyncResult BeginNegotiateTlsAsClient(ImapConnectionContext connectionContext, AsyncCallback callback, object callbackState);

		// Token: 0x06000122 RID: 290
		AsyncOperationResult<ImapResultData> EndNegotiateTlsAsClient(IAsyncResult asyncResult);

		// Token: 0x06000123 RID: 291
		IAsyncResult BeginCommand(ImapCommand command, ImapConnectionContext connectionContext, AsyncCallback callback, object callbackState);

		// Token: 0x06000124 RID: 292
		IAsyncResult BeginCommand(ImapCommand command, bool processResponse, ImapConnectionContext connectionContext, AsyncCallback callback, object callbackState);

		// Token: 0x06000125 RID: 293
		AsyncOperationResult<ImapResultData> EndCommand(IAsyncResult asyncResult);

		// Token: 0x06000126 RID: 294
		void Cancel();
	}
}
