using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationRegistration : ServerObject
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00026E74 File Offset: 0x00025074
		internal NotificationRegistration(NotificationHandler notificationHandler, Logon logon, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, ServerObjectHandle returnNotificationHandleValue) : base(logon)
		{
			this.notificationSink = logon.NotificationQueue.Register(notificationHandler, flags, eventFlags, wantGlobalScope, folderId, messageId, returnNotificationHandleValue, this.String8Encoding);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00026EAB File Offset: 0x000250AB
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationRegistration>(this);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00026EB3 File Offset: 0x000250B3
		protected override void InternalDispose()
		{
			this.notificationSink.Dispose();
			base.InternalDispose();
		}

		// Token: 0x04000246 RID: 582
		private readonly NotificationSink notificationSink;
	}
}
