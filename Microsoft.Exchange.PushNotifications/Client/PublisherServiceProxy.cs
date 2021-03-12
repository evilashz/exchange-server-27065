using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x0200000C RID: 12
	internal class PublisherServiceProxy : IPublisherServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public PublisherServiceProxy(PushNotificationsProxyPool<IPublisherServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreatePublisherProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002BFB File Offset: 0x00000DFB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002C03 File Offset: 0x00000E03
		private PushNotificationsProxyPool<IPublisherServiceContract> ProxyPool { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x00002C30 File Offset: 0x00000E30
		public IAsyncResult BeginPublishNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<IPublisherServiceContract>.Start(this.ProxyPool, delegate(IPublisherServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginPublishNotifications(notifications, poolCallback, poolState);
			}, delegate(IPublisherServiceContract proxy, IAsyncResult result)
			{
				proxy.EndPublishNotifications(result);
			}, asyncCallback, asyncState, "PublishNotifications", 3);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C88 File Offset: 0x00000E88
		public void EndPublishNotifications(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CAD File Offset: 0x00000EAD
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublisherServiceProxy>(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CCA File Offset: 0x00000ECA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CDC File Offset: 0x00000EDC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<IPublisherServiceContract> proxyPool = this.ProxyPool;
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

		// Token: 0x0400001A RID: 26
		private DisposeTracker disposeTracker;

		// Token: 0x0400001B RID: 27
		private bool isDisposed;
	}
}
