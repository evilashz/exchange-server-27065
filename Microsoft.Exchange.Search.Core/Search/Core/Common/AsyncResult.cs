using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000053 RID: 83
	internal class AsyncResult : LazyAsyncResult
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00003278 File Offset: 0x00001478
		public AsyncResult(AsyncCallback asyncCallback, object state) : base(null, state, asyncCallback)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003284 File Offset: 0x00001484
		public static AsyncResult EndAsyncOperation(IAsyncResult asyncResult)
		{
			AsyncResult asyncResult2 = LazyAsyncResult.EndAsyncOperation<AsyncResult>(asyncResult);
			if (asyncResult2.Result is Exception)
			{
				throw (Exception)asyncResult2.Result;
			}
			return asyncResult2;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000032B2 File Offset: 0x000014B2
		public void SetAsCompleted()
		{
			base.InvokeCallback();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000032BB File Offset: 0x000014BB
		public void SetAsCompleted(ComponentException exception)
		{
			base.InvokeCallback(exception);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000032C5 File Offset: 0x000014C5
		public void End()
		{
			AsyncResult.EndAsyncOperation(this);
		}
	}
}
