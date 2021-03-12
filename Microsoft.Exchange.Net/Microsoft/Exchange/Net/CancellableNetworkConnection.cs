using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BDD RID: 3037
	internal class CancellableNetworkConnection : NetworkConnection
	{
		// Token: 0x06004242 RID: 16962 RVA: 0x000B08D3 File Offset: 0x000AEAD3
		public CancellableNetworkConnection(Socket socket, CancellationToken token, int bufferSize = 4096) : base(socket, 4096)
		{
			ArgumentValidator.ThrowIfNull("socket", socket);
			this.token = token;
			this.ctr = token.Register(new Action(base.Shutdown));
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000B090C File Offset: 0x000AEB0C
		protected override void ReadComplete(IAsyncResult iar)
		{
			this.ReadOrReadLineCompleteInternal(iar, new AsyncCallback(base.ReadComplete));
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000B0921 File Offset: 0x000AEB21
		protected override void ReadLineComplete(IAsyncResult iar)
		{
			this.ReadOrReadLineCompleteInternal(iar, new AsyncCallback(base.ReadLineComplete));
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000B0938 File Offset: 0x000AEB38
		protected override void WriteComplete(IAsyncResult iar)
		{
			if (this.token.IsCancellationRequested)
			{
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.AsyncOperationCancelled);
				TaskCompletionSource<object> taskCompletionSource = (TaskCompletionSource<object>)iar.AsyncState;
				taskCompletionSource.SetCanceled();
				return;
			}
			base.WriteComplete(iar);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000B0979 File Offset: 0x000AEB79
		protected override void InternalDispose(bool disposeTrueFinalizeFalse)
		{
			if (disposeTrueFinalizeFalse)
			{
				this.ctr.Dispose();
			}
			base.InternalDispose(disposeTrueFinalizeFalse);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000B0990 File Offset: 0x000AEB90
		private void ReadOrReadLineCompleteInternal(IAsyncResult iar, AsyncCallback completionMethod)
		{
			if (this.token.IsCancellationRequested)
			{
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.AsyncOperationCancelled);
				TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout> taskCompletionSource = (TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout>)iar.AsyncState;
				taskCompletionSource.SetCanceled();
				return;
			}
			completionMethod(iar);
		}

		// Token: 0x040038C4 RID: 14532
		private CancellationToken token;

		// Token: 0x040038C5 RID: 14533
		private CancellationTokenRegistration ctr;
	}
}
