using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B0F RID: 2831
	internal class RetriableAsyncOperation<TClient> : RetriableAsyncOperationBase<TClient, BasicAsyncResult>
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x0009EC9C File Offset: 0x0009CE9C
		private RetriableAsyncOperation(IServiceProxyPool<TClient> proxyPool, Action<TClient, AsyncCallback, object> beginDelegate, Action<TClient, IAsyncResult> endDelegate, AsyncCallback callerAsyncCallback, object callerState, string debugMessage, int numberOfRetries) : base(proxyPool, new BasicAsyncResult(callerAsyncCallback, callerState), beginDelegate, debugMessage, numberOfRetries)
		{
			ArgumentValidator.ThrowIfNull("endDelegate", endDelegate);
			this.EndDelegate = endDelegate;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x0009ECC5 File Offset: 0x0009CEC5
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x0009ECCD File Offset: 0x0009CECD
		public Action<TClient, IAsyncResult> EndDelegate { get; set; }

		// Token: 0x06003D01 RID: 15617 RVA: 0x0009ECD8 File Offset: 0x0009CED8
		internal static IAsyncResult Start(IServiceProxyPool<TClient> proxyPool, Action<TClient, AsyncCallback, object> beginDelegate, Action<TClient, IAsyncResult> endDelegate, AsyncCallback asyncCallback, object asyncState, string debugMessage, int numberOfRetries)
		{
			RetriableAsyncOperation<TClient> retriableAsyncOperation = new RetriableAsyncOperation<TClient>(proxyPool, beginDelegate, endDelegate, asyncCallback, asyncState, debugMessage, numberOfRetries);
			retriableAsyncOperation.Begin();
			return retriableAsyncOperation.CallerAsyncResult;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x0009ED01 File Offset: 0x0009CF01
		protected override void InvokeEndDelegate(TClient proxy, IAsyncResult retryAsyncResult)
		{
			this.EndDelegate(proxy, retryAsyncResult);
		}
	}
}
