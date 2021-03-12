using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000039 RID: 57
	internal class LocalUserNotificationPublisherServiceProxy : ILocalUserNotificationPublisherServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00004EF2 File Offset: 0x000030F2
		public LocalUserNotificationPublisherServiceProxy(PushNotificationsProxyPool<ILocalUserNotificationPublisherServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreateLocalUserNotificationPublisherProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004F1D File Offset: 0x0000311D
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004F25 File Offset: 0x00003125
		private PushNotificationsProxyPool<ILocalUserNotificationPublisherServiceContract> ProxyPool { get; set; }

		// Token: 0x06000167 RID: 359 RVA: 0x00004F50 File Offset: 0x00003150
		public IAsyncResult BeginPublishUserNotifications(LocalUserNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<ILocalUserNotificationPublisherServiceContract>.Start(this.ProxyPool, delegate(ILocalUserNotificationPublisherServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginPublishUserNotifications(notifications, poolCallback, poolState);
			}, delegate(ILocalUserNotificationPublisherServiceContract proxy, IAsyncResult result)
			{
				proxy.EndPublishUserNotifications(result);
			}, asyncCallback, asyncState, "PublishNotifications", 3);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004FA8 File Offset: 0x000031A8
		public void EndPublishUserNotifications(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004FCD File Offset: 0x000031CD
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LocalUserNotificationPublisherServiceProxy>(this);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004FD5 File Offset: 0x000031D5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004FEA File Offset: 0x000031EA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004FFC File Offset: 0x000031FC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<ILocalUserNotificationPublisherServiceContract> proxyPool = this.ProxyPool;
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

		// Token: 0x04000072 RID: 114
		private DisposeTracker disposeTracker;

		// Token: 0x04000073 RID: 115
		private bool isDisposed;
	}
}
