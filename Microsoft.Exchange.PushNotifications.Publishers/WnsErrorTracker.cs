using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000E8 RID: 232
	internal class WnsErrorTracker
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00017524 File Offset: 0x00015724
		public WnsErrorTracker(int maxFailedAuthRequests, int baseDelayInMilliseconds, int backOffTimeInSeconds)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("maxFailedAuthRequests", maxFailedAuthRequests);
			ArgumentValidator.ThrowIfZeroOrNegative("baseDelayInMilliseconds", baseDelayInMilliseconds);
			ArgumentValidator.ThrowIfZeroOrNegative("backOffTimeInSeconds", backOffTimeInSeconds);
			this.MaxFailedAuthRequests = maxFailedAuthRequests;
			this.BaseDelay = baseDelayInMilliseconds;
			this.BackOffTime = backOffTimeInSeconds;
			this.ResetEndTimes();
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00017573 File Offset: 0x00015773
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001757B File Offset: 0x0001577B
		public ExDateTime DelayEndTime { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00017584 File Offset: 0x00015784
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001758C File Offset: 0x0001578C
		public ExDateTime BackOffEndTime { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00017595 File Offset: 0x00015795
		public virtual bool ShouldDelay
		{
			get
			{
				return this.DelayEndTime > ExDateTime.UtcNow;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000175A7 File Offset: 0x000157A7
		public virtual bool ShouldBackOff
		{
			get
			{
				return this.BackOffEndTime > ExDateTime.UtcNow;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x000175B9 File Offset: 0x000157B9
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x000175C1 File Offset: 0x000157C1
		public int FailedAuthRequests { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x000175CA File Offset: 0x000157CA
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x000175D2 File Offset: 0x000157D2
		public int NotificationErrorWeight { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000175DB File Offset: 0x000157DB
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x000175E3 File Offset: 0x000157E3
		private int MaxFailedAuthRequests { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000175EC File Offset: 0x000157EC
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x000175F4 File Offset: 0x000157F4
		private int BaseDelay { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000175FD File Offset: 0x000157FD
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00017605 File Offset: 0x00015805
		private int BackOffTime { get; set; }

		// Token: 0x0600076F RID: 1903 RVA: 0x0001760E File Offset: 0x0001580E
		public virtual void ReportAuthenticationSuccess()
		{
			this.FailedAuthRequests = 0;
			this.ResetEndTimes();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001761D File Offset: 0x0001581D
		public virtual void ReportAuthenticationFailure()
		{
			if (this.FailedAuthRequests < this.MaxFailedAuthRequests)
			{
				this.FailedAuthRequests++;
			}
			this.SetDelayEndTime();
			if (this.FailedAuthRequests == this.MaxFailedAuthRequests)
			{
				this.SetBackOffEndTime();
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00017655 File Offset: 0x00015855
		public virtual void ReportWnsRequestSuccess()
		{
			this.NotificationErrorWeight = 0;
			this.ResetEndTimes();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00017664 File Offset: 0x00015864
		public virtual void ReportWnsRequestFailure(WnsResultErrorType errorType)
		{
			if (this.NotificationErrorWeight < 100)
			{
				switch (errorType)
				{
				case WnsResultErrorType.Unknown:
					this.NotificationErrorWeight += 10;
					break;
				case WnsResultErrorType.Timeout:
					this.NotificationErrorWeight += 40;
					break;
				case WnsResultErrorType.Throttle:
					this.NotificationErrorWeight += 100;
					break;
				case WnsResultErrorType.AuthTokenExpired:
					this.NotificationErrorWeight += 40;
					break;
				case WnsResultErrorType.ServerUnavailable:
					this.NotificationErrorWeight += 2;
					break;
				}
			}
			if (this.NotificationErrorWeight >= 100)
			{
				this.SetBackOffEndTime();
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000176FC File Offset: 0x000158FC
		public virtual void ConsumeDelay(int amountInMilliseconds)
		{
			ArgumentValidator.ThrowIfNegative("amountInMilliseconds", amountInMilliseconds);
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (utcNow >= this.DelayEndTime)
			{
				return;
			}
			int val = (int)this.DelayEndTime.Subtract(utcNow).TotalMilliseconds;
			int num = Math.Min(val, amountInMilliseconds);
			if (num > 0)
			{
				Thread.Sleep(num);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00017755 File Offset: 0x00015955
		public virtual void Reset()
		{
			this.FailedAuthRequests = 0;
			this.NotificationErrorWeight = 0;
			this.ResetEndTimes();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001776B File Offset: 0x0001596B
		private void ResetEndTimes()
		{
			this.DelayEndTime = ExDateTime.MinValue;
			this.BackOffEndTime = ExDateTime.MinValue;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00017784 File Offset: 0x00015984
		private void SetDelayEndTime()
		{
			this.DelayEndTime = ExDateTime.UtcNow.AddMilliseconds((double)(this.FailedAuthRequests * this.BaseDelay));
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000177B4 File Offset: 0x000159B4
		private void SetBackOffEndTime()
		{
			this.BackOffEndTime = ExDateTime.UtcNow.AddSeconds((double)this.BackOffTime);
		}

		// Token: 0x04000416 RID: 1046
		private const int UnknownErrorWeight = 10;

		// Token: 0x04000417 RID: 1047
		private const int TimeoutErrorWeight = 40;

		// Token: 0x04000418 RID: 1048
		private const int ThrottlingErrorWeight = 100;

		// Token: 0x04000419 RID: 1049
		private const int AuthTokenExpiredWeight = 40;

		// Token: 0x0400041A RID: 1050
		private const int ServerUnavailableWeight = 2;

		// Token: 0x0400041B RID: 1051
		private const int MaxNotificationErrorWeight = 100;
	}
}
