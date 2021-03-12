using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001E RID: 30
	internal class ApnsChannelContext : PushNotificationChannelContext<ApnsNotification>
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000056C4 File Offset: 0x000038C4
		public ApnsChannelContext(ApnsNotification notification, CancellationToken cancellationToken, ITracer tracer, ApnsChannelSettings settings) : base(notification, cancellationToken, tracer)
		{
			this.Settings = settings;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000056D7 File Offset: 0x000038D7
		public bool IsRetryable
		{
			get
			{
				return this.ConnectRetries < this.Settings.ConnectRetryMax && this.AuthenticateCount < this.Settings.AuthenticateRetryMax;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005701 File Offset: 0x00003901
		public int CurrentRetryDelay
		{
			get
			{
				return this.Settings.ConnectRetryDelay * this.ConnectRetries;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005715 File Offset: 0x00003915
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000571D File Offset: 0x0000391D
		private ApnsChannelSettings Settings { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005726 File Offset: 0x00003926
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000572E File Offset: 0x0000392E
		private int ConnectRetries { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005737 File Offset: 0x00003937
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000573F File Offset: 0x0000393F
		private int AuthenticateCount { get; set; }

		// Token: 0x06000124 RID: 292 RVA: 0x00005748 File Offset: 0x00003948
		public void IncrementConnectRetries()
		{
			this.ConnectRetries++;
			base.Tracer.TraceDebug<int, ApnsNotification>((long)this.GetHashCode(), "[IncrementConnectRetries] Connect attempts are now {0} for '{1}'", this.ConnectRetries, base.Notification);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000577B File Offset: 0x0000397B
		public void ResetConnectRetries()
		{
			this.ConnectRetries = 0;
			base.Tracer.TraceDebug<ApnsNotification>((long)this.GetHashCode(), "[ResetConnectRetries] Connect attempts reset for '{0}'", base.Notification);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000057A1 File Offset: 0x000039A1
		public void IncrementAuthenticateRetries()
		{
			this.AuthenticateCount++;
			base.Tracer.TraceDebug<int, ApnsNotification>((long)this.GetHashCode(), "[IncrementAuthenticateRetries] Authenticate attempts are now {0} for '{1}'", this.AuthenticateCount, base.Notification);
		}
	}
}
