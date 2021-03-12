using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000017 RID: 23
	internal class AzureChallengeRequestServiceProxy : IAzureChallengeRequestServiceContract, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000036CF File Offset: 0x000018CF
		public AzureChallengeRequestServiceProxy(PushNotificationsProxyPool<IAzureChallengeRequestServiceContract> proxyPool = null)
		{
			this.ProxyPool = (proxyPool ?? PushNotificationsProxyPoolFactory.CreateAzureChallengeRequestProxyPool());
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000036FA File Offset: 0x000018FA
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003702 File Offset: 0x00001902
		private PushNotificationsProxyPool<IAzureChallengeRequestServiceContract> ProxyPool { get; set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003730 File Offset: 0x00001930
		public IAsyncResult BeginChallengeRequest(AzureChallengeRequestInfo issueSecret, AsyncCallback asyncCallback, object asyncState)
		{
			return RetriableAsyncOperation<IAzureChallengeRequestServiceContract>.Start(this.ProxyPool, delegate(IAzureChallengeRequestServiceContract proxy, AsyncCallback poolCallback, object poolState)
			{
				proxy.BeginChallengeRequest(issueSecret, poolCallback, poolState);
			}, delegate(IAzureChallengeRequestServiceContract proxy, IAsyncResult result)
			{
				proxy.EndChallengeRequest(result);
			}, asyncCallback, asyncState, "IssueRegistrationSecret", 3);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003788 File Offset: 0x00001988
		public void EndChallengeRequest(IAsyncResult result)
		{
			BasicAsyncResult basicAsyncResult = result as BasicAsyncResult;
			ArgumentValidator.ThrowIfNull("result should be a BasicAsyncResult instance", basicAsyncResult);
			basicAsyncResult.End();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000037AD File Offset: 0x000019AD
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AzureChallengeRequestServiceProxy>(this);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000037B5 File Offset: 0x000019B5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000037CA File Offset: 0x000019CA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000037DC File Offset: 0x000019DC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					PushNotificationsProxyPool<IAzureChallengeRequestServiceContract> proxyPool = this.ProxyPool;
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

		// Token: 0x04000038 RID: 56
		private DisposeTracker disposeTracker;

		// Token: 0x04000039 RID: 57
		private bool isDisposed;
	}
}
