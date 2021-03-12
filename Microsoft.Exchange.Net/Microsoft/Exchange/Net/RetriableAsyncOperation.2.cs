using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B10 RID: 2832
	internal class RetriableAsyncOperation<TClient, TResult> : RetriableAsyncOperationBase<TClient, BasicAsyncResult<TResult>>
	{
		// Token: 0x06003D03 RID: 15619 RVA: 0x0009ED10 File Offset: 0x0009CF10
		private RetriableAsyncOperation(IServiceProxyPool<TClient> proxyPool, Action<TClient, AsyncCallback, object> beginDelegate, Func<TClient, IAsyncResult, TResult> endDelegate, AsyncCallback callerAsyncCallback, object callerState, string debugMessage, int numberOfRetries) : base(proxyPool, new BasicAsyncResult<TResult>(callerAsyncCallback, callerState), beginDelegate, debugMessage, numberOfRetries)
		{
			ArgumentValidator.ThrowIfNull("endDelegate", endDelegate);
			this.EndDelegate = endDelegate;
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x0009ED39 File Offset: 0x0009CF39
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x0009ED41 File Offset: 0x0009CF41
		public Func<TClient, IAsyncResult, TResult> EndDelegate { get; set; }

		// Token: 0x06003D06 RID: 15622 RVA: 0x0009ED4C File Offset: 0x0009CF4C
		internal static IAsyncResult Start(IServiceProxyPool<TClient> proxyPool, Action<TClient, AsyncCallback, object> beginDelegate, Func<TClient, IAsyncResult, TResult> endDelegate, AsyncCallback asyncCallback, object asyncState, string debugMessage, int numberOfRetries)
		{
			RetriableAsyncOperation<TClient, TResult> retriableAsyncOperation = new RetriableAsyncOperation<TClient, TResult>(proxyPool, beginDelegate, endDelegate, asyncCallback, asyncState, debugMessage, numberOfRetries);
			retriableAsyncOperation.Begin();
			return retriableAsyncOperation.CallerAsyncResult;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0009ED78 File Offset: 0x0009CF78
		protected override void InvokeEndDelegate(TClient proxy, IAsyncResult retryAsyncResult)
		{
			BasicAsyncResult<TResult> callerAsyncResult = base.CallerAsyncResult;
			callerAsyncResult.Result = this.EndDelegate(proxy, retryAsyncResult);
		}
	}
}
