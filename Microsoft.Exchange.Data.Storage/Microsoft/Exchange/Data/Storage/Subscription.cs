using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Subscription : IDisposeTrackable, IDisposable
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0003239C File Offset: 0x0003059C
		private Subscription(StoreSession storeSession, StoreObjectId storeObjectId, NotificationHandler handler, bool passthruCallback)
		{
			Util.ThrowOnNullArgument(storeSession, "storeSession");
			this.storeSession = storeSession;
			this.itemId = storeObjectId;
			this.handler = handler;
			this.passthruCallback = passthruCallback;
			if (this.passthruCallback)
			{
				this.sink = new SubscriptionSink(this);
			}
			else
			{
				this.sink = new SubscriptionSink(storeSession.SubscriptionsManager, handler != null);
				storeSession.SubscriptionsManager.RegisterSubscription(this);
			}
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00032430 File Offset: 0x00030630
		private Subscription(StoreSession storeSession, StoreObjectId storeObjectId, AdviseFlags flags, NotificationHandler handler, NotificationCallbackMode callbackMode, bool passthruCallback) : this(storeSession, storeObjectId, handler, passthruCallback)
		{
			if (passthruCallback)
			{
				callbackMode = NotificationCallbackMode.Sync;
			}
			try
			{
				MapiNotificationHandler mapiNotificationHandler = new MapiNotificationHandler(this.sink.OnNotify);
				if (callbackMode == NotificationCallbackMode.Async)
				{
					mapiNotificationHandler = new MapiNotificationHandler(this.OnNotify);
					this.waitCallback = new WaitCallback(this.WaitCallbackProc);
				}
				this.adviseId = this.storeSession.Mailbox.Advise((storeObjectId == null) ? null : storeObjectId.ProviderLevelItemId, flags, mapiNotificationHandler, callbackMode);
				this.notificationSource = this.storeSession.Mailbox;
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000324D8 File Offset: 0x000306D8
		private Subscription(IQueryResult queryResult, NotificationHandler handler, bool passthruCallback) : this(queryResult.StoreSession, null, handler, passthruCallback)
		{
			this.queryResult = queryResult;
			this.notificationSource = (INotificationSource)queryResult;
			try
			{
				this.adviseId = queryResult.Advise(this.sink, false);
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00032538 File Offset: 0x00030738
		public NotificationHandler Handler
		{
			get
			{
				this.CheckDisposed("Handler::get");
				return this.handler;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0003254B File Offset: 0x0003074B
		public StoreSession StoreSession
		{
			get
			{
				this.CheckDisposed("StoreSession::get");
				return this.storeSession;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0003255E File Offset: 0x0003075E
		public StoreObjectId ItemId
		{
			get
			{
				this.CheckDisposed("ItemId::get");
				return this.itemId;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00032571 File Offset: 0x00030771
		public bool HasDroppedNotification
		{
			get
			{
				this.CheckDisposed("HasDroppedNotification::get");
				return this.sink.HasDroppedNotification;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00032589 File Offset: 0x00030789
		public bool HasPendingNotifications
		{
			get
			{
				this.CheckDisposed("HasPendingNotifications::get");
				return this.sink.Count > 0;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000325A4 File Offset: 0x000307A4
		internal SubscriptionSink Sink
		{
			get
			{
				return this.sink;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000325AC File Offset: 0x000307AC
		public static Subscription Create(StoreSession session, NotificationHandler handler, NotificationType notificationType, StoreId id)
		{
			return Subscription.Create(session, handler, notificationType, id, true, false);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000325B9 File Offset: 0x000307B9
		public static Subscription Create(StoreSession session, NotificationHandler handler, NotificationType notificationType, StoreId id, bool isSyncCallback)
		{
			return Subscription.Create(session, handler, notificationType, id, isSyncCallback, false);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000325C8 File Offset: 0x000307C8
		public static Subscription Create(StoreSession session, NotificationHandler handler, NotificationType notificationType, StoreId id, bool isSyncCallback, bool passthruCallback)
		{
			EnumValidator.ThrowIfInvalid<NotificationType>(notificationType, "notificationType");
			if (session == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "session");
				throw new ArgumentNullException("session");
			}
			if (handler == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "handler");
				throw new ArgumentNullException("handler");
			}
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "id");
				throw new ArgumentNullException("id");
			}
			if ((notificationType & NotificationType.ConnectionDropped) == NotificationType.ConnectionDropped)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "Subscription::Create. ConnectionDropped not valid on object notifications.");
				throw new InvalidOperationException("ConnectionDropped not valid on object notifications.");
			}
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			NotificationCallbackMode callbackMode = isSyncCallback ? NotificationCallbackMode.Sync : NotificationCallbackMode.Async;
			return Subscription.InternalCreate(session, handler, notificationType, storeObjectId, callbackMode, passthruCallback);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00032692 File Offset: 0x00030892
		public static Subscription CreateMailboxSubscription(StoreSession session, NotificationHandler handler, NotificationType notificationType)
		{
			return Subscription.CreateMailboxSubscription(session, handler, notificationType, false);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000326A0 File Offset: 0x000308A0
		public static Subscription CreateMailboxSubscription(StoreSession session, NotificationHandler handler, NotificationType notificationType, bool passthruCallback)
		{
			EnumValidator.ThrowIfInvalid<NotificationType>(notificationType, "notificationType");
			if (session == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "session");
				throw new ArgumentNullException("session");
			}
			if (handler == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "handler");
				throw new ArgumentNullException("handler");
			}
			if ((notificationType & NotificationType.ConnectionDropped) == NotificationType.ConnectionDropped && notificationType != NotificationType.ConnectionDropped)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "Subscription::Create. ConnectionDropped cannot be combined with other types of notification.");
				throw new InvalidOperationException("ConnectionDropped cannot be combined with other types of notification.");
			}
			return Subscription.InternalCreate(session, handler, notificationType, null, NotificationCallbackMode.Sync, passthruCallback);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0003273D File Offset: 0x0003093D
		public static Subscription Create(IQueryResult queryResult, NotificationHandler handler)
		{
			return Subscription.Create(queryResult, handler, false);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00032748 File Offset: 0x00030948
		public static Subscription Create(IQueryResult queryResult, NotificationHandler handler, bool passthruCallback)
		{
			if (queryResult == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "queryResult");
				throw new ArgumentNullException("queryResult");
			}
			if (handler == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Subscription::Create. {0} should not be null.", "handler");
				throw new ArgumentNullException("handler");
			}
			return Subscription.InternalCreate(queryResult, handler, passthruCallback);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000327A5 File Offset: 0x000309A5
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Subscription>(this);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000327AD File Offset: 0x000309AD
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000327C2 File Offset: 0x000309C2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000327D4 File Offset: 0x000309D4
		internal void OnNotify(MapiNotification notification)
		{
			Notification state;
			if (this.TryCreateXsoNotification(notification, out state))
			{
				ThreadPool.QueueUserWorkItem(this.waitCallback, state);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000327F9 File Offset: 0x000309F9
		internal bool TryCreateXsoNotification(MapiNotification notification, out Notification xsoNotification)
		{
			xsoNotification = null;
			if (notification.NotificationType == AdviseFlags.TableModified)
			{
				xsoNotification = Subscription.CreateQueryNotification(notification, this.queryResult.Columns);
			}
			else
			{
				xsoNotification = Subscription.CreateNotification(notification);
			}
			return xsoNotification != null;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00032830 File Offset: 0x00030A30
		internal void InvokeHandler(Notification notification)
		{
			this.handler(notification);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00032840 File Offset: 0x00030A40
		private static Subscription InternalCreate(StoreSession session, NotificationHandler handler, NotificationType notificationType, StoreObjectId storeObjectId, NotificationCallbackMode callbackMode, bool passthruCallback)
		{
			Subscription.CheckSubscriptionLimit(session);
			AdviseFlags adviseFlags = (AdviseFlags)0;
			if ((notificationType & NotificationType.NewMail) == NotificationType.NewMail)
			{
				adviseFlags |= AdviseFlags.NewMail;
			}
			if ((notificationType & NotificationType.Created) == NotificationType.Created)
			{
				adviseFlags |= AdviseFlags.ObjectCreated;
			}
			if ((notificationType & NotificationType.Deleted) == NotificationType.Deleted)
			{
				adviseFlags |= AdviseFlags.ObjectDeleted;
			}
			if ((notificationType & NotificationType.Modified) == NotificationType.Modified)
			{
				adviseFlags |= AdviseFlags.ObjectModified;
			}
			if ((notificationType & NotificationType.Moved) == NotificationType.Moved)
			{
				adviseFlags |= AdviseFlags.ObjectMoved;
			}
			if ((notificationType & NotificationType.Copied) == NotificationType.Copied)
			{
				adviseFlags |= AdviseFlags.ObjectCopied;
			}
			if ((notificationType & NotificationType.SearchComplete) == NotificationType.SearchComplete)
			{
				adviseFlags |= AdviseFlags.SearchComplete;
			}
			if ((notificationType & NotificationType.ConnectionDropped) == NotificationType.ConnectionDropped)
			{
				adviseFlags |= AdviseFlags.ConnectionDropped;
			}
			return new Subscription(session, storeObjectId, adviseFlags, handler, callbackMode, passthruCallback);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000328CB File Offset: 0x00030ACB
		private static Subscription InternalCreate(IQueryResult queryResult, NotificationHandler handler, bool passthruCallback)
		{
			Subscription.CheckSubscriptionLimit(queryResult.StoreSession);
			return new Subscription(queryResult, handler, passthruCallback);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000328E0 File Offset: 0x00030AE0
		private static void CheckSubscriptionLimit(StoreSession session)
		{
			if (session.SubscriptionsManager.SubscriptionCount >= StorageLimits.Instance.NotificationsMaxSubscriptions)
			{
				MailboxSession mailboxSession = session as MailboxSession;
				LocalizedString message;
				if (mailboxSession != null)
				{
					message = ServerStrings.ExTooManySubscriptions(mailboxSession.MailboxOwner.LegacyDn, mailboxSession.MailboxOwner.MailboxInfo.Location.ServerLegacyDn);
				}
				else
				{
					message = ServerStrings.ExTooManySubscriptionsOnPublicStore(session.ServerFullyQualifiedDomainName);
				}
				throw new TooManySubscriptionsException(message);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0003294C File Offset: 0x00030B4C
		private static Notification CreateQueryNotification(MapiNotification notification, ColumnPropertyDefinitions columns)
		{
			MapiTableNotification mapiTableNotification = notification as MapiTableNotification;
			QueryNotificationType eventType;
			switch (mapiTableNotification.TableEvent)
			{
			case TableEvent.TableChanged:
				eventType = QueryNotificationType.QueryResultChanged;
				break;
			case TableEvent.TableError:
				eventType = QueryNotificationType.Error;
				break;
			case TableEvent.TableRowAdded:
				eventType = QueryNotificationType.RowAdded;
				break;
			case TableEvent.TableRowDeleted:
			case TableEvent.TableRowDeletedExtended:
				eventType = QueryNotificationType.RowDeleted;
				break;
			case TableEvent.TableRowModified:
				eventType = QueryNotificationType.RowModified;
				break;
			case TableEvent.TableSortDone:
				eventType = QueryNotificationType.SortDone;
				break;
			case TableEvent.TableRestrictDone:
				eventType = QueryNotificationType.RestrictDone;
				break;
			case TableEvent.TableSetColDone:
				eventType = QueryNotificationType.SetColumnDone;
				break;
			case TableEvent.TableReload:
				eventType = QueryNotificationType.Reload;
				break;
			default:
				return null;
			}
			int hresult = mapiTableNotification.HResult;
			byte[] index = mapiTableNotification.Index.GetBytes() ?? Array<byte>.Empty;
			byte[] prior = mapiTableNotification.Prior.GetBytes() ?? Array<byte>.Empty;
			ICollection<PropertyDefinition> propertyDefinitions;
			object[] row;
			if (mapiTableNotification.Row != null && mapiTableNotification.Row.Length > 0)
			{
				if (mapiTableNotification.TableEvent == TableEvent.TableRowDeletedExtended)
				{
					PropTag[] array = new PropTag[mapiTableNotification.Row.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = mapiTableNotification.Row[i].PropTag;
					}
					propertyDefinitions = Subscription.PropertiesForRowDeletedExtended;
					ICollection<PropertyDefinition> columns2 = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, null, null, array);
					QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(null, columns2);
					queryResultPropertyBag.SetQueryResultRow(mapiTableNotification.Row);
					row = queryResultPropertyBag.GetProperties(propertyDefinitions);
				}
				else
				{
					if (!QueryResult.DoPropertyValuesMatchColumns(columns, mapiTableNotification.Row))
					{
						ExTraceGlobals.StorageTracer.TraceDebug(0L, "Subcription::CreateQueryNotification. The notification data does not match the columns the client knows about. Dropping notification.");
						return null;
					}
					QueryResultPropertyBag queryResultPropertyBag2 = new QueryResultPropertyBag(null, columns.SimplePropertyDefinitions);
					queryResultPropertyBag2.SetQueryResultRow(mapiTableNotification.Row);
					propertyDefinitions = columns.PropertyDefinitions;
					row = queryResultPropertyBag2.GetProperties(columns.PropertyDefinitions);
				}
			}
			else
			{
				propertyDefinitions = Array<UnresolvedPropertyDefinition>.Empty;
				row = Array<object>.Empty;
			}
			return new QueryNotification(eventType, hresult, index, prior, propertyDefinitions, row);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00032B08 File Offset: 0x00030D08
		private static Notification CreateNotification(MapiNotification notification)
		{
			Notification result;
			if (notification.NotificationType == AdviseFlags.NewMail)
			{
				MapiNewMailNotification mapiNewMailNotification = notification as MapiNewMailNotification;
				result = new NewMailNotification(StoreObjectId.FromProviderSpecificId(mapiNewMailNotification.EntryId, ObjectClass.GetObjectType(mapiNewMailNotification.MessageClass)), StoreObjectId.FromProviderSpecificId(mapiNewMailNotification.ParentId, StoreObjectType.Folder), mapiNewMailNotification.MessageClass, (MessageFlags)mapiNewMailNotification.MessageFlags);
			}
			else if (notification.NotificationType == AdviseFlags.SearchComplete)
			{
				result = new ObjectNotification(null, null, null, null, (NotificationObjectType)0, null, NotificationType.SearchComplete);
			}
			else if (notification.NotificationType == AdviseFlags.ConnectionDropped)
			{
				MapiConnectionDroppedNotification mapiConnectionDroppedNotification = notification as MapiConnectionDroppedNotification;
				result = new ConnectionDroppedNotification(mapiConnectionDroppedNotification.ServerDN, mapiConnectionDroppedNotification.UserDN, mapiConnectionDroppedNotification.TickDeath);
			}
			else
			{
				MapiObjectNotification mapiObjectNotification = notification as MapiObjectNotification;
				if (mapiObjectNotification == null)
				{
					throw new InvalidOperationException(ServerStrings.ExNotSupportedNotificationType((uint)notification.NotificationType));
				}
				AdviseFlags notificationType = notification.NotificationType;
				NotificationType type;
				if (notificationType <= AdviseFlags.ObjectDeleted)
				{
					if (notificationType == AdviseFlags.ObjectCreated)
					{
						type = NotificationType.Created;
						goto IL_10A;
					}
					if (notificationType == AdviseFlags.ObjectDeleted)
					{
						type = NotificationType.Deleted;
						goto IL_10A;
					}
				}
				else
				{
					if (notificationType == AdviseFlags.ObjectModified)
					{
						type = NotificationType.Modified;
						goto IL_10A;
					}
					if (notificationType == AdviseFlags.ObjectMoved)
					{
						type = NotificationType.Moved;
						goto IL_10A;
					}
					if (notificationType == AdviseFlags.ObjectCopied)
					{
						type = NotificationType.Copied;
						goto IL_10A;
					}
				}
				throw new InvalidOperationException(ServerStrings.ExNotSupportedNotificationType((uint)notification.NotificationType));
				IL_10A:
				UnresolvedPropertyDefinition[] propertyDefinitions;
				if (mapiObjectNotification.Tags != null)
				{
					propertyDefinitions = PropertyTagCache.UnresolvedPropertyDefinitionsFromPropTags(mapiObjectNotification.Tags);
				}
				else
				{
					propertyDefinitions = Array<UnresolvedPropertyDefinition>.Empty;
				}
				result = new ObjectNotification((mapiObjectNotification.EntryId == null) ? null : StoreObjectId.FromProviderSpecificId(mapiObjectNotification.EntryId, StoreObjectType.Unknown), (mapiObjectNotification.ParentId == null) ? null : StoreObjectId.FromProviderSpecificId(mapiObjectNotification.ParentId, StoreObjectType.Folder), (mapiObjectNotification.OldId == null) ? null : StoreObjectId.FromProviderSpecificId(mapiObjectNotification.OldId, StoreObjectType.Unknown), (mapiObjectNotification.OldParentId == null) ? null : StoreObjectId.FromProviderSpecificId(mapiObjectNotification.OldParentId, StoreObjectType.Folder), (NotificationObjectType)mapiObjectNotification.ObjectType, propertyDefinitions, type);
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00032CC2 File Offset: 0x00030EC2
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00032CE4 File Offset: 0x00030EE4
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			lock (this.disposeLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.isDisposed = true;
			}
			this.InternalDispose(disposing);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00032D44 File Offset: 0x00030F44
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.storeSession != null && !this.storeSession.IsDisposed)
					{
						try
						{
							if (!this.passthruCallback)
							{
								this.storeSession.SubscriptionsManager.UnRegisterSubscription(this);
							}
							if (this.adviseId != null && this.notificationSource != null && !this.notificationSource.IsDisposedOrDead)
							{
								this.notificationSource.Unadvise(this.adviseId);
							}
						}
						catch (StoragePermanentException arg)
						{
							ExTraceGlobals.SessionTracer.Information<StoragePermanentException>((long)this.GetHashCode(), "Subscription::InternalDispose. Exception ignored during subscription Dispose, {0}.", arg);
						}
						catch (StorageTransientException arg2)
						{
							ExTraceGlobals.SessionTracer.Information<StorageTransientException>((long)this.GetHashCode(), "Subscription::InternalDispose. Exception ignored during subscription Dispose, {0}.", arg2);
						}
					}
				}
				finally
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00032E2C File Offset: 0x0003102C
		private void WaitCallbackProc(object obj)
		{
			this.handler((Notification)obj);
		}

		// Token: 0x040001BF RID: 447
		private readonly StoreObjectId itemId;

		// Token: 0x040001C0 RID: 448
		private readonly StoreSession storeSession;

		// Token: 0x040001C1 RID: 449
		private readonly NotificationHandler handler;

		// Token: 0x040001C2 RID: 450
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040001C3 RID: 451
		private readonly object disposeLock = new object();

		// Token: 0x040001C4 RID: 452
		private readonly SubscriptionSink sink;

		// Token: 0x040001C5 RID: 453
		private readonly bool passthruCallback;

		// Token: 0x040001C6 RID: 454
		private readonly INotificationSource notificationSource;

		// Token: 0x040001C7 RID: 455
		private readonly IQueryResult queryResult;

		// Token: 0x040001C8 RID: 456
		private readonly WaitCallback waitCallback;

		// Token: 0x040001C9 RID: 457
		private readonly object adviseId;

		// Token: 0x040001CA RID: 458
		private bool isDisposed;

		// Token: 0x040001CB RID: 459
		private static readonly PropertyDefinition[] PropertiesForRowDeletedExtended = new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationId
		};
	}
}
