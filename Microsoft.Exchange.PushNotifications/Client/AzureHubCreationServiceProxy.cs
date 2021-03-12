using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x0200001C RID: 28
	internal class AzureHubCreationServiceProxy : IAzureHubCreationServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00003AFC File Offset: 0x00001CFC
		public AzureHubCreationServiceProxy(PushNotificationsProxyPool<IAzureHubCreationServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreateAzureHubCreationProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003B27 File Offset: 0x00001D27
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003B2F File Offset: 0x00001D2F
		private PushNotificationsProxyPool<IAzureHubCreationServiceContract> ProxyPool { get; set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B5C File Offset: 0x00001D5C
		public IAsyncResult BeginCreateHub(AzureHubDefinition hubDefinition, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<IAzureHubCreationServiceContract>.Start(this.ProxyPool, delegate(IAzureHubCreationServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginCreateHub(hubDefinition, poolCallback, poolState);
			}, delegate(IAzureHubCreationServiceContract proxy, IAsyncResult result)
			{
				proxy.EndCreateHub(result);
			}, asyncCallback, asyncState, "CreateHub", 3);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public void EndCreateHub(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BD9 File Offset: 0x00001DD9
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AzureHubCreationServiceProxy>(this);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003BE1 File Offset: 0x00001DE1
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C08 File Offset: 0x00001E08
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<IAzureHubCreationServiceContract> proxyPool = this.ProxyPool;
					if (proxyPool != null)
					{
						proxyPool.Dispose();
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.ProxyPool = null;
				this.isDisposed = true;
			}
		}

		// Token: 0x04000044 RID: 68
		private DisposeTracker disposeTracker;

		// Token: 0x04000045 RID: 69
		private bool isDisposed;
	}
}
