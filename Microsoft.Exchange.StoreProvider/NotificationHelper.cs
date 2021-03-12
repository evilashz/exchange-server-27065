using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001FE RID: 510
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationHelper : DisposeTrackableBase
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0002B327 File Offset: 0x00029527
		internal NotificationHelper(MapiNotificationHandler handler) : this(handler, false)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002B334 File Offset: 0x00029534
		internal NotificationHelper(MapiNotificationHandler handler, bool callbackImmediately)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(31))
			{
				ComponentTrace<MapiNetTags>.Trace<string, string>(11490, 31, (long)this.GetHashCode(), "NotificationHelper.NotificationHelper: this={0}, handler={1}", TraceUtils.MakeHash(this), TraceUtils.MakeHash(handler));
			}
			if (!callbackImmediately)
			{
				this.waitCallback = new WaitCallback(this.WaitCallbackProc);
			}
			this.handlerReference = GCHandle.Alloc(handler, GCHandleType.Weak);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002B3A4 File Offset: 0x000295A4
		~NotificationHelper()
		{
			base.Dispose(false);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002B3D4 File Offset: 0x000295D4
		protected override void InternalDispose(bool disposing)
		{
			lock (this.handlerReferenceFreeLock)
			{
				this.handlerReference.Free();
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002B41C File Offset: 0x0002961C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationHelper>(this);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002B424 File Offset: 0x00029624
		public void OnNotify(MapiNotification notification)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(31))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(15586, 31, (long)this.GetHashCode(), "NotificationHelper.OnNotify params: ulEventType={0}", notification.NotificationType.ToString());
			}
			if (this.waitCallback == null)
			{
				this.CallHandler(notification);
				return;
			}
			if (!this.NotifyOnThreadPool(notification))
			{
				ComponentTrace<MapiNetTags>.Trace<AdviseFlags>(58411, 31, (long)this.GetHashCode(), "NotificationHelper.OnNotify params: ulEventType={0}", notification.NotificationType);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002B499 File Offset: 0x00029699
		private void WaitCallbackProc(object obj)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(31))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(9058, 31, (long)this.GetHashCode(), "NotificationHelper.WaitCallbackProc params: object={0}", TraceUtils.MakeHash(obj));
			}
			this.CallHandler((MapiNotification)obj);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002B4D0 File Offset: 0x000296D0
		private void CallHandler(MapiNotification notification)
		{
			MapiNotificationHandler mapiNotificationHandler;
			lock (this.handlerReferenceFreeLock)
			{
				mapiNotificationHandler = (this.handlerReference.IsAllocated ? ((MapiNotificationHandler)this.handlerReference.Target) : null);
			}
			if (mapiNotificationHandler != null)
			{
				mapiNotificationHandler(notification);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002B538 File Offset: 0x00029738
		public bool NotifyOnThreadPool(MapiNotification notification)
		{
			return ThreadPool.QueueUserWorkItem(this.waitCallback, notification);
		}

		// Token: 0x040009C8 RID: 2504
		private GCHandle handlerReference;

		// Token: 0x040009C9 RID: 2505
		private readonly object handlerReferenceFreeLock = new object();

		// Token: 0x040009CA RID: 2506
		private readonly WaitCallback waitCallback;
	}
}
