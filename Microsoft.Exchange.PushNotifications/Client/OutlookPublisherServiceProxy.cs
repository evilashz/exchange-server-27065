using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x0200002F RID: 47
	internal class OutlookPublisherServiceProxy : IOutlookPublisherServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00004B17 File Offset: 0x00002D17
		public OutlookPublisherServiceProxy(PushNotificationsProxyPool<IOutlookPublisherServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreateOutlookPublisherProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004B42 File Offset: 0x00002D42
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004B4A File Offset: 0x00002D4A
		private PushNotificationsProxyPool<IOutlookPublisherServiceContract> ProxyPool { get; set; }

		// Token: 0x0600013C RID: 316 RVA: 0x00004B78 File Offset: 0x00002D78
		public IAsyncResult BeginPublishOutlookNotifications(OutlookNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<IOutlookPublisherServiceContract>.Start(this.ProxyPool, delegate(IOutlookPublisherServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginPublishOutlookNotifications(notifications, poolCallback, poolState);
			}, delegate(IOutlookPublisherServiceContract proxy, IAsyncResult result)
			{
				proxy.EndPublishOutlookNotifications(result);
			}, asyncCallback, asyncState, "PublishNotifications", 3);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public void EndPublishOutlookNotifications(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004BF5 File Offset: 0x00002DF5
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<OutlookPublisherServiceProxy>(this);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004BFD File Offset: 0x00002DFD
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004C12 File Offset: 0x00002E12
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004C24 File Offset: 0x00002E24
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<IOutlookPublisherServiceContract> proxyPool = this.ProxyPool;
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

		// Token: 0x04000068 RID: 104
		private DisposeTracker disposeTracker;

		// Token: 0x04000069 RID: 105
		private bool isDisposed;
	}
}
