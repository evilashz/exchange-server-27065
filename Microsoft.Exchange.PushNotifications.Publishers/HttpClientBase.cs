using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000002 RID: 2
	internal abstract class HttpClientBase : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public HttpClientBase(HttpClient httpClient)
		{
			ArgumentValidator.ThrowIfNull("httpClient", httpClient);
			this.HttpClient = httpClient;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020F2 File Offset: 0x000002F2
		private protected HttpClient HttpClient { protected get; private set; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020FB File Offset: 0x000002FB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210A File Offset: 0x0000030A
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && this.HttpClient != null)
				{
					this.HttpClient.Dispose();
					this.HttpClient = null;
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool isDisposed;
	}
}
