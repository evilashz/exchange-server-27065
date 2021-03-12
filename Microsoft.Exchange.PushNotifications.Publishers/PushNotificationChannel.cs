using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000A RID: 10
	internal abstract class PushNotificationChannel<TNotif> : PushNotificationDisposable where TNotif : PushNotification
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000275A File Offset: 0x0000095A
		protected PushNotificationChannel(string appId, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("appId", appId);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.AppId = appId;
			this.Tracer = tracer;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000033 RID: 51 RVA: 0x00002788 File Offset: 0x00000988
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x000027C0 File Offset: 0x000009C0
		public event EventHandler<InvalidNotificationEventArgs> InvalidNotificationDetected;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027F5 File Offset: 0x000009F5
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027FD File Offset: 0x000009FD
		public string AppId { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002806 File Offset: 0x00000A06
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000280E File Offset: 0x00000A0E
		public ITracer Tracer { get; private set; }

		// Token: 0x06000039 RID: 57
		public abstract void Send(TNotif notification, CancellationToken cancelToken);

		// Token: 0x0600003A RID: 58 RVA: 0x00002818 File Offset: 0x00000A18
		protected virtual void OnInvalidNotificationFound(InvalidNotificationEventArgs e)
		{
			EventHandler<InvalidNotificationEventArgs> invalidNotificationDetected = this.InvalidNotificationDetected;
			if (invalidNotificationDetected != null)
			{
				invalidNotificationDetected(this, e);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002838 File Offset: 0x00000A38
		protected bool WaitUntilDoneOrCancelled(ICancelableAsyncResult asyncResult, PushNotificationChannelContext<TNotif> context, int stepTimeout)
		{
			int num = 0;
			while (!context.IsCancelled)
			{
				if (asyncResult.AsyncWaitHandle.WaitOne(stepTimeout))
				{
					return true;
				}
				num++;
				if (num % 3 == 0)
				{
					this.Tracer.TraceDebug<int>((long)this.GetHashCode(), "[WaitUntilDoneOrCancelled] Still waiting for the operation to finish: '{0}'", num);
				}
			}
			this.Tracer.TraceDebug((long)this.GetHashCode(), "[WaitUntilDoneOrCancelled] Cancellation token was signaled");
			asyncResult.Cancel();
			return false;
		}
	}
}
