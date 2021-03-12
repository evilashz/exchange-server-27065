using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationSink : BaseObject
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00017541 File Offset: 0x00015741
		private NotificationSink(NotificationQueue notificationQueue, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandleValue, StoreId? folderId, Encoding string8Encoding)
		{
			this.parentQueue = notificationQueue;
			this.notificationHandler = notificationHandler;
			this.returnNotificationHandleValue = returnNotificationHandleValue;
			this.rootFolderId = folderId;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00017570 File Offset: 0x00015770
		private NotificationSink(NotificationQueue notificationQueue, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandleValue, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, Encoding string8Encoding) : this(notificationQueue, notificationHandler, returnNotificationHandleValue, new StoreId?(folderId), string8Encoding)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				NotificationType notificationType = this.ToXsoNotificationType(flags, eventFlags, folderId, messageId, out this.folderId, out this.objectId);
				if (notificationType == NotificationType.NewMail && folderId == default(StoreId) && !wantGlobalScope)
				{
					wantGlobalScope = true;
				}
				if (wantGlobalScope && folderId != default(StoreId))
				{
					throw new RopExecutionException("When specifying wantGlobalScope to true, the FID must be zero.", (ErrorCode)2147746050U);
				}
				if (this.NotificationServerObjectId != null)
				{
					this.subscription = Subscription.Create(this.Logon.Session, new NotificationHandler(this.Handle), notificationType, this.NotificationServerObjectId, true, true);
				}
				else
				{
					this.subscription = Subscription.CreateMailboxSubscription(this.Logon.Session, new NotificationHandler(this.Handle), notificationType, true);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00017680 File Offset: 0x00015880
		private NotificationSink(NotificationQueue notificationQueue, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandleValue, View view, TableFlags tableFlags, StoreId? folderId, Encoding string8Encoding) : this(notificationQueue, notificationHandler, returnNotificationHandleValue, folderId, string8Encoding)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.view = view;
				if ((byte)(tableFlags & NotificationSink.NonNotificationTableFlags) != 0)
				{
					throw new ArgumentException(string.Format("tableFlags = {0}", tableFlags));
				}
				if ((byte)(tableFlags & TableFlags.NoNotifications) != 0)
				{
					this.subscription = null;
				}
				else
				{
					tableFlags &= ~NotificationSink.NonNotificationTableFlags;
					if ((tableFlags | NotificationSink.NotificationTableFlags) != NotificationSink.NotificationTableFlags)
					{
						throw new RopExecutionException(string.Format("We have flags which has not been processed yet. TableFlags = {0}.", tableFlags), (ErrorCode)2147746050U);
					}
					if (view != null && view.DataSource != null)
					{
						this.subscription = Subscription.Create(view.DataSource.QueryResult, new NotificationHandler(this.Handle), true);
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00017770 File Offset: 0x00015970
		private StoreObjectId NotificationServerObjectId
		{
			get
			{
				if (this.objectId != null)
				{
					return this.objectId;
				}
				return this.folderId;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00017787 File Offset: 0x00015987
		private Logon Logon
		{
			get
			{
				return this.parentQueue.Logon;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00017794 File Offset: 0x00015994
		public static NotificationSink CreateObjectNotificationSink(NotificationQueue notificationQueue, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandleValue, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, Encoding string8Encoding)
		{
			return new NotificationSink(notificationQueue, notificationHandler, returnNotificationHandleValue, flags, eventFlags, wantGlobalScope, folderId, messageId, string8Encoding);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000177B4 File Offset: 0x000159B4
		public static NotificationSink CreateQueryNotificationSink(NotificationHandler notificationHandler, NotificationQueue notificationQueue, ServerObjectHandle returnNotificationHandleValue, View view, TableFlags tableFlags, StoreId? folderId, Encoding string8Encoding)
		{
			return new NotificationSink(notificationQueue, notificationHandler, returnNotificationHandleValue, view, tableFlags, folderId, string8Encoding);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000177C5 File Offset: 0x000159C5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationSink>(this);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000177CD File Offset: 0x000159CD
		protected override void InternalDispose()
		{
			this.parentQueue.UnRegister(this);
			Util.DisposeIfPresent(this.subscription);
			base.InternalDispose();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000177F0 File Offset: 0x000159F0
		private NotificationType ToXsoNotificationType(NotificationFlags flags, NotificationEventFlags eventFlags, StoreId folderId, StoreId messageId, out StoreObjectId xsoFolderId, out StoreObjectId xsoMessageId)
		{
			xsoFolderId = null;
			xsoMessageId = null;
			if (folderId != default(StoreId))
			{
				xsoFolderId = this.Logon.Session.IdConverter.CreateFolderId(folderId);
			}
			if (messageId != default(StoreId))
			{
				xsoMessageId = this.Logon.Session.IdConverter.CreateMessageId(folderId, messageId);
			}
			if (eventFlags != NotificationEventFlags.None)
			{
				Feature.Stubbed(87114, string.Format("Other NotificationEventFlags {0}", flags));
				eventFlags = NotificationEventFlags.None;
			}
			NotificationType notificationType = (NotificationType)0;
			int num = 0;
			foreach (NotificationFlags notificationFlags in NotificationSink.notificationFlags)
			{
				if ((ushort)(flags & notificationFlags) != 0)
				{
					notificationType |= NotificationSink.notificationTypes[num];
					flags &= ~notificationFlags;
				}
				num++;
			}
			if (flags != (NotificationFlags)0)
			{
				throw Feature.NotImplemented(68088, string.Format("Other NotificationFlags {0}", flags));
			}
			return notificationType;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0001794E File Offset: 0x00015B4E
		private void Handle(Notification xsoNotification)
		{
			if (base.IsDisposed)
			{
				return;
			}
			this.Logon.Connection.ExecuteInContext<Notification>(xsoNotification, delegate(Notification innerNotification)
			{
				this.parentQueue.Enqueue(this.returnNotificationHandleValue, this.Logon, this.string8Encoding, innerNotification, this.rootFolderId, this.view);
				if (!this.Logon.HasActiveAsyncOperation && !Activity.IsForeground)
				{
					this.notificationHandler.InvokeCallback();
				}
			});
		}

		// Token: 0x040000F4 RID: 244
		public static readonly TableFlags NonNotificationTableFlags = ~TableFlags.NoNotifications;

		// Token: 0x040000F5 RID: 245
		public static readonly TableFlags NotificationTableFlags = TableFlags.NoNotifications;

		// Token: 0x040000F6 RID: 246
		private static readonly NotificationType[] notificationTypes = new NotificationType[]
		{
			NotificationType.Copied,
			NotificationType.Created,
			NotificationType.Deleted,
			NotificationType.Modified,
			NotificationType.Moved,
			NotificationType.SearchComplete,
			NotificationType.NewMail,
			NotificationType.Query
		};

		// Token: 0x040000F7 RID: 247
		private static readonly NotificationFlags[] notificationFlags = new NotificationFlags[]
		{
			NotificationFlags.ObjectCopied,
			NotificationFlags.ObjectCreated,
			NotificationFlags.ObjectDeleted,
			NotificationFlags.ObjectModified,
			NotificationFlags.ObjectMoved,
			NotificationFlags.SearchComplete,
			NotificationFlags.NewMail,
			NotificationFlags.TableModified
		};

		// Token: 0x040000F8 RID: 248
		private readonly ServerObjectHandle returnNotificationHandleValue;

		// Token: 0x040000F9 RID: 249
		private readonly NotificationQueue parentQueue;

		// Token: 0x040000FA RID: 250
		private readonly NotificationHandler notificationHandler;

		// Token: 0x040000FB RID: 251
		private readonly View view;

		// Token: 0x040000FC RID: 252
		private readonly StoreObjectId folderId;

		// Token: 0x040000FD RID: 253
		private readonly StoreObjectId objectId;

		// Token: 0x040000FE RID: 254
		private readonly Subscription subscription;

		// Token: 0x040000FF RID: 255
		private readonly Encoding string8Encoding;

		// Token: 0x04000100 RID: 256
		private readonly StoreId? rootFolderId;
	}
}
