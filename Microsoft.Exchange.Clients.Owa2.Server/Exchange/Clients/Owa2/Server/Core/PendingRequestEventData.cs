using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200019B RID: 411
	internal class PendingRequestEventData
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003914E File Offset: 0x0003734E
		internal PendingRequestEventData(OwaAsyncResult asyncResult, ChunkedHttpResponse response)
		{
			this.asyncResult = asyncResult;
			this.response = response;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00039164 File Offset: 0x00037364
		internal OwaAsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003916C File Offset: 0x0003736C
		internal ChunkedHttpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040008FF RID: 2303
		private OwaAsyncResult asyncResult;

		// Token: 0x04000900 RID: 2304
		private ChunkedHttpResponse response;
	}
}
