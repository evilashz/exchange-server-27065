using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000B RID: 11
	internal class PushNotificationChannelContext<TNotif> where TNotif : PushNotification
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000028A0 File Offset: 0x00000AA0
		public PushNotificationChannelContext(TNotif notification, CancellationToken cancellationToken, ITracer tracer)
		{
			this.Notification = notification;
			this.CancellationToken = cancellationToken;
			this.Tracer = tracer;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028C0 File Offset: 0x00000AC0
		public string AppId
		{
			get
			{
				TNotif notification = this.Notification;
				return notification.AppId;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028E1 File Offset: 0x00000AE1
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000028E9 File Offset: 0x00000AE9
		public TNotif Notification { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028F2 File Offset: 0x00000AF2
		public bool IsActive
		{
			get
			{
				return this.Notification != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002908 File Offset: 0x00000B08
		public bool IsCancelled
		{
			get
			{
				return this.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002923 File Offset: 0x00000B23
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000292B File Offset: 0x00000B2B
		public CancellationToken CancellationToken { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002934 File Offset: 0x00000B34
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000293C File Offset: 0x00000B3C
		protected ITracer Tracer { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002948 File Offset: 0x00000B48
		public void Done()
		{
			this.Tracer.TraceDebug<TNotif>((long)this.GetHashCode(), "[Done] Done with notification '{0}'", this.Notification);
			this.Notification = default(TNotif);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002984 File Offset: 0x00000B84
		public void Drop(string traces = null)
		{
			this.Tracer.TraceWarning<TNotif>((long)this.GetHashCode(), "[Drop] Dropping notification '{0}'", this.Notification);
			PushNotificationTracker.ReportDropped(this.Notification, traces ?? string.Empty);
			this.Notification = default(TNotif);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029D8 File Offset: 0x00000BD8
		public override string ToString()
		{
			TNotif notification = this.Notification;
			return notification.ToString();
		}
	}
}
