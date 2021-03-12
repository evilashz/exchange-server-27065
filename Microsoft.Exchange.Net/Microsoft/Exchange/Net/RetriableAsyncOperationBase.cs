using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B0E RID: 2830
	internal abstract class RetriableAsyncOperationBase<TClient, TAsyncResult> where TAsyncResult : BasicAsyncResult
	{
		// Token: 0x06003CEF RID: 15599 RVA: 0x0009EA90 File Offset: 0x0009CC90
		protected RetriableAsyncOperationBase(IServiceProxyPool<TClient> proxyPool, TAsyncResult callerAsyncResult, Action<TClient, AsyncCallback, object> beginDelegate, string debugMessage, int numberOfRetries)
		{
			ArgumentValidator.ThrowIfNull("proxyPool", proxyPool);
			ArgumentValidator.ThrowIfNull("callerAsyncResult", callerAsyncResult);
			ArgumentValidator.ThrowIfNull("beginDelegate", beginDelegate);
			ArgumentValidator.ThrowIfZeroOrNegative("numberOfRetries", numberOfRetries);
			this.ProxyPool = proxyPool;
			this.CallerAsyncResult = callerAsyncResult;
			this.BeginDelegate = beginDelegate;
			this.DebugMessage = (debugMessage ?? string.Empty);
			this.NumberOfRetries = numberOfRetries;
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x0009EB03 File Offset: 0x0009CD03
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x0009EB0B File Offset: 0x0009CD0B
		protected TAsyncResult CallerAsyncResult { get; set; }

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x0009EB14 File Offset: 0x0009CD14
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x0009EB1C File Offset: 0x0009CD1C
		private Action<TClient, AsyncCallback, object> BeginDelegate { get; set; }

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x0009EB25 File Offset: 0x0009CD25
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x0009EB2D File Offset: 0x0009CD2D
		private string DebugMessage { get; set; }

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x0009EB36 File Offset: 0x0009CD36
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x0009EB3E File Offset: 0x0009CD3E
		private int NumberOfRetries { get; set; }

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x0009EB47 File Offset: 0x0009CD47
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x0009EB4F File Offset: 0x0009CD4F
		private IServiceProxyPool<TClient> ProxyPool { get; set; }

		// Token: 0x06003CFA RID: 15610 RVA: 0x0009EB58 File Offset: 0x0009CD58
		protected void Begin()
		{
			Exception exception;
			if (!this.ProxyPool.TryCallServiceWithRetryAsyncBegin(new Action<IPooledServiceProxy<TClient>>(this.InvokeBeginDelegate), this.DebugMessage, this.NumberOfRetries, out exception))
			{
				TAsyncResult callerAsyncResult = this.CallerAsyncResult;
				callerAsyncResult.Complete(exception, false);
			}
		}

		// Token: 0x06003CFB RID: 15611
		protected abstract void InvokeEndDelegate(TClient proxy, IAsyncResult retryAsyncResult);

		// Token: 0x06003CFC RID: 15612 RVA: 0x0009EBA4 File Offset: 0x0009CDA4
		private void InvokeBeginDelegate(IPooledServiceProxy<TClient> pooledProxy)
		{
			this.NumberOfRetries--;
			this.BeginDelegate(pooledProxy.Client, new AsyncCallback(this.RetryAsyncCallback), pooledProxy);
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
		private void RetryAsyncCallback(IAsyncResult retryAsyncResult)
		{
			IPooledServiceProxy<TClient> cachedProxy = retryAsyncResult.AsyncState as IPooledServiceProxy<TClient>;
			Exception exception;
			bool flag = this.ProxyPool.TryCallServiceWithRetryAsyncEnd(cachedProxy, delegate(IPooledServiceProxy<TClient> x)
			{
				this.InvokeEndDelegate(x.Client, retryAsyncResult);
			}, this.DebugMessage, out exception);
			if (flag)
			{
				TAsyncResult callerAsyncResult = this.CallerAsyncResult;
				callerAsyncResult.Complete(retryAsyncResult.CompletedSynchronously);
				return;
			}
			if (this.NumberOfRetries > 0)
			{
				this.Begin();
				return;
			}
			TAsyncResult callerAsyncResult2 = this.CallerAsyncResult;
			callerAsyncResult2.Complete(exception, retryAsyncResult.CompletedSynchronously);
		}
	}
}
