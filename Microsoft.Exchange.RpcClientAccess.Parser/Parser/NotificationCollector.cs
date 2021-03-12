using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000103 RID: 259
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NotificationCollector : BaseObject
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0000FB68 File Offset: 0x0000DD68
		internal NotificationCollector(int maxSize)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.maxSize = maxSize;
				this.notifyResults = new List<NotifyResult>();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		internal int MaxSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		internal List<NotifyResult> NotifyResults
		{
			get
			{
				return this.notifyResults;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public bool TryAddNotification(ServerObjectHandle notificationHandle, byte logonId, Encoding string8Encoding, Notification notification)
		{
			base.CheckDisposed();
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			NotifyResult notifyResult = new NotifyResult(notificationHandle, logonId, string8Encoding, notification);
			notifyResult.Serialize(this.writer);
			if (this.writer.Position > (long)this.maxSize)
			{
				if (ExTraceGlobals.NotificationDeliveryTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.NotificationDeliveryTracer.Information<string>(50457L, "Fail pack notif: {0}", notification.ToString());
				}
				return false;
			}
			if (ExTraceGlobals.NotificationDeliveryTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.NotificationDeliveryTracer.Information<string>(32676L, "Pack notif: {0}", notification.ToString());
			}
			this.notifyResults.Add(notifyResult);
			return true;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000FC86 File Offset: 0x0000DE86
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationCollector>(this);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000FC8E File Offset: 0x0000DE8E
		protected override void InternalDispose()
		{
			this.writer.Dispose();
			base.InternalDispose();
		}

		// Token: 0x04000307 RID: 775
		private readonly int maxSize;

		// Token: 0x04000308 RID: 776
		private readonly CountWriter writer = new CountWriter();

		// Token: 0x04000309 RID: 777
		private List<NotifyResult> notifyResults;
	}
}
