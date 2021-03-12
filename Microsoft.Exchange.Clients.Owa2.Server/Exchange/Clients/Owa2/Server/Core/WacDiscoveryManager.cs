using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000052 RID: 82
	public class WacDiscoveryManager
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000092A4 File Offset: 0x000074A4
		private WacDiscoveryManager(Uri wacDiscoveryEndPoint)
		{
			if (wacDiscoveryEndPoint == null)
			{
				this.wacDiscoveryResult = new WacDiscoveryResultFailure(new WacDiscoveryFailureException("The WAC Discovery URL is null."));
				return;
			}
			this.wacDiscoveryResult = new WacDiscoveryResultFailure(new WacDiscoveryFailureException("WAC discovery has not yet succeeded with endpoint " + wacDiscoveryEndPoint));
			this.wacDiscoveryClient = new WacDiscoveryClient(wacDiscoveryEndPoint);
			this.currentFailureRetryTimeSpan = WacConfiguration.Instance.DiscoveryDataRetrievalErrorBaseRefreshInterval;
			this.wacDiscoveryDataRefreshTimer = new Timer(new TimerCallback(this.RefreshWacDiscoveryData));
			this.RefreshWacDiscoveryData(null);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009338 File Offset: 0x00007538
		public static WacDiscoveryManager Instance
		{
			get
			{
				if (WacDiscoveryManager.wacDiscoveryManager == null)
				{
					lock (WacDiscoveryManager.wacDiscoveryManagerConstructorLock)
					{
						if (WacDiscoveryManager.wacDiscoveryManager == null)
						{
							Uri wacDiscoveryEndPoint = WacConfiguration.Instance.WacDiscoveryEndPoint;
							WacDiscoveryManager.wacDiscoveryManager = new WacDiscoveryManager(wacDiscoveryEndPoint);
						}
					}
				}
				return WacDiscoveryManager.wacDiscoveryManager;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000939C File Offset: 0x0000759C
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000093E0 File Offset: 0x000075E0
		public WacDiscoveryResultBase WacDiscoveryResult
		{
			get
			{
				WacDiscoveryResultBase result;
				lock (this.wacDiscoveryDataLock)
				{
					result = this.wacDiscoveryResult;
				}
				return result;
			}
			private set
			{
				lock (this.wacDiscoveryDataLock)
				{
					this.wacDiscoveryResult = value;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009424 File Offset: 0x00007624
		public Uri WacDiscoveryEndPoint
		{
			get
			{
				if (this.wacDiscoveryClient != null)
				{
					return this.wacDiscoveryClient.WacDiscoveryEndPoint;
				}
				return null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000943C File Offset: 0x0000763C
		private void RefreshWacDiscoveryData(object data)
		{
			WacDiscoveryResultBase wacDiscoveryResultBase = this.wacDiscoveryClient.FetchDiscoveryResults();
			if (wacDiscoveryResultBase != null)
			{
				this.WacDiscoveryResult = wacDiscoveryResultBase;
				this.currentFailureRetryTimeSpan = WacConfiguration.Instance.DiscoveryDataRefreshInterval;
			}
			if (this.WacDiscoveryResult != null)
			{
				this.wacDiscoveryDataRefreshTimer.Change((long)WacConfiguration.Instance.DiscoveryDataRefreshInterval.TotalMilliseconds, -1L);
				return;
			}
			this.currentFailureRetryTimeSpan = this.currentFailureRetryTimeSpan.Add(this.currentFailureRetryTimeSpan);
			if (this.currentFailureRetryTimeSpan > WacConfiguration.Instance.DiscoveryDataRefreshInterval)
			{
				this.currentFailureRetryTimeSpan = WacConfiguration.Instance.DiscoveryDataRefreshInterval;
			}
			this.wacDiscoveryDataRefreshTimer.Change((long)this.currentFailureRetryTimeSpan.TotalMilliseconds, -1L);
		}

		// Token: 0x04000127 RID: 295
		private static readonly object wacDiscoveryManagerConstructorLock = new object();

		// Token: 0x04000128 RID: 296
		private readonly object wacDiscoveryDataLock = new object();

		// Token: 0x04000129 RID: 297
		private readonly WacDiscoveryClient wacDiscoveryClient;

		// Token: 0x0400012A RID: 298
		private readonly Timer wacDiscoveryDataRefreshTimer;

		// Token: 0x0400012B RID: 299
		private static WacDiscoveryManager wacDiscoveryManager;

		// Token: 0x0400012C RID: 300
		private WacDiscoveryResultBase wacDiscoveryResult;

		// Token: 0x0400012D RID: 301
		private TimeSpan currentFailureRetryTimeSpan;
	}
}
