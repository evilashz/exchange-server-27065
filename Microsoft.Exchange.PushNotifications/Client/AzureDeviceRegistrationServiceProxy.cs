using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x0200001A RID: 26
	internal class AzureDeviceRegistrationServiceProxy : IAzureDeviceRegistrationServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000039A0 File Offset: 0x00001BA0
		public AzureDeviceRegistrationServiceProxy(PushNotificationsProxyPool<IAzureDeviceRegistrationServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreateAzureDeviceRegistrationProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000039CB File Offset: 0x00001BCB
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000039D3 File Offset: 0x00001BD3
		private PushNotificationsProxyPool<IAzureDeviceRegistrationServiceContract> ProxyPool { get; set; }

		// Token: 0x060000BC RID: 188 RVA: 0x00003A00 File Offset: 0x00001C00
		public IAsyncResult BeginDeviceRegistration(AzureDeviceRegistrationInfo hubDefinition, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<IAzureDeviceRegistrationServiceContract>.Start(this.ProxyPool, delegate(IAzureDeviceRegistrationServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginDeviceRegistration(hubDefinition, poolCallback, poolState);
			}, delegate(IAzureDeviceRegistrationServiceContract proxy, IAsyncResult result)
			{
				proxy.EndDeviceRegistration(result);
			}, asyncCallback, asyncState, "RegisterDevice", 3);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A58 File Offset: 0x00001C58
		public void EndDeviceRegistration(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003A7D File Offset: 0x00001C7D
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AzureDeviceRegistrationServiceProxy>(this);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003A85 File Offset: 0x00001C85
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A9A File Offset: 0x00001C9A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003AAC File Offset: 0x00001CAC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<IAzureDeviceRegistrationServiceContract> proxyPool = this.ProxyPool;
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

		// Token: 0x04000040 RID: 64
		private DisposeTracker disposeTracker;

		// Token: 0x04000041 RID: 65
		private bool isDisposed;
	}
}
