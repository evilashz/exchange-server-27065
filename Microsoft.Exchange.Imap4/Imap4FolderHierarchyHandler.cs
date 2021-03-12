using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200000A RID: 10
	internal class Imap4FolderHierarchyHandler : DisposeTrackableBase, IMapiNotificationHandler
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002F3C File Offset: 0x0000113C
		public Imap4FolderHierarchyHandler(Imap4ResponseFactory responseFactory)
		{
			this.factory = responseFactory;
			using (Folder folder = Folder.Bind(this.factory.Store, DefaultFolderType.Configuration))
			{
				this.view = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, Imap4FolderHierarchyHandler.viewProperties);
				this.view.GetRows(1);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002FB0 File Offset: 0x000011B0
		public MapiNotificationManager Manager { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002FB9 File Offset: 0x000011B9
		public void SubscribeNotification()
		{
			if (this.viewSubscription != null)
			{
				throw new InvalidOperationException("Should call UnsubscribeNotification first.");
			}
			this.viewSubscription = Subscription.Create(this.view, new NotificationHandler(this.HandleQueryNotificationCallback));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002FEB File Offset: 0x000011EB
		public void SubscribeObjectNotification(StoreObjectId folderId)
		{
			if (this.objectSubscription != null)
			{
				throw new InvalidOperationException("Should call UnsubscribeObjectNotification first.");
			}
			this.objectSubscription = Subscription.Create(this.factory.Store, new NotificationHandler(this.HandleObjectNotificationCallback), NotificationType.Deleted, folderId);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003024 File Offset: 0x00001224
		public void UnsubscribeObjectNotification()
		{
			if (this.objectSubscription != null)
			{
				lock (this.factory.Store)
				{
					bool flag2 = false;
					try
					{
						if (!this.factory.Store.IsConnected)
						{
							this.factory.ConnectToTheStore();
							flag2 = true;
						}
						this.objectSubscription.Dispose();
					}
					finally
					{
						if (flag2)
						{
							this.factory.Store.Disconnect();
						}
					}
				}
				this.objectSubscription = null;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000030C4 File Offset: 0x000012C4
		public void UnsubscribeNotification()
		{
			if (this.viewSubscription != null)
			{
				this.viewSubscription.Dispose();
				this.viewSubscription = null;
			}
			this.UnsubscribeObjectNotification();
			if (this.Manager != null)
			{
				this.Manager.UnregisterHandler(this);
				this.Manager = null;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003104 File Offset: 0x00001304
		public void ProcessNotification(Notification notification)
		{
			if (notification is QueryNotification)
			{
				QueryNotification queryNotification = notification as QueryNotification;
				if (queryNotification.EventType == QueryNotificationType.Reload || queryNotification.EventType == QueryNotificationType.Error)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(this.Manager.Factory.Session.SessionId, "Start reload all views and notification handlers.");
					this.Manager.Factory.ReloadAllNotificationHandlers();
					this.reloadNeeded = false;
					return;
				}
				((Imap4Session)this.Manager.Factory.Session).NeedToRebuildFolderTable = true;
				return;
			}
			else
			{
				if (notification is ObjectNotification)
				{
					((Imap4Session)this.Manager.Factory.Session).NeedToRebuildFolderTable = true;
					this.Manager.Factory.SelectedMailbox.MailboxDoesNotExist = true;
					return;
				}
				throw new InvalidOperationException("Invalid notification type: " + notification);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000031D8 File Offset: 0x000013D8
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				this.UnsubscribeNotification();
			}
			catch (LocalizedException ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.factory.Session.SessionId, "Exception caught while unsubscribing folder view notification. {0}", ex.ToString());
			}
			finally
			{
				if (this.view != null)
				{
					try
					{
						this.view.Dispose();
					}
					catch (LocalizedException ex2)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.factory.Session.SessionId, "Exception caught while disposing folder view. {0}", ex2.ToString());
					}
					this.view = null;
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003284 File Offset: 0x00001484
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Imap4FolderHierarchyHandler>(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000328C File Offset: 0x0000148C
		private void HandleQueryNotificationCallback(Notification notification)
		{
			MapiNotificationManager manager = this.Manager;
			try
			{
				Trace sessionTracer = ProtocolBaseServices.SessionTracer;
				if (sessionTracer != null)
				{
					sessionTracer.TraceDebug<string>((manager == null) ? 0L : manager.Factory.Session.SessionId, "User {0} entering Imap4FolderHierarchyHandler.HandleQueryNotificationCallback.", (manager == null) ? "(Unknown user)" : manager.Factory.Session.GetUserNameForLogging());
				}
				QueryNotification queryNotification = notification as QueryNotification;
				if (queryNotification != null)
				{
					if (!this.reloadNeeded)
					{
						if (manager != null)
						{
							if (queryNotification.EventType == QueryNotificationType.RowAdded || queryNotification.EventType == QueryNotificationType.RowDeleted || queryNotification.EventType == QueryNotificationType.QueryResultChanged)
							{
								StoreObjectId storeObjectId = (queryNotification.Row != null && queryNotification.Row.Length > 0) ? ((VersionedId)queryNotification.Row[0]).ObjectId : null;
								if (storeObjectId == null || ((Imap4Session)manager.Factory.Session).Imap4Folders.Contains(storeObjectId) || queryNotification.EventType == QueryNotificationType.RowAdded)
								{
									manager.EnqueueNotification(queryNotification, this);
								}
							}
							else if (queryNotification.EventType == QueryNotificationType.Reload || queryNotification.EventType == QueryNotificationType.Error)
							{
								manager.EnqueueNotification(notification, this);
								this.reloadNeeded = true;
							}
						}
					}
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted((manager == null) ? 0L : manager.Factory.Session.SessionId);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000033E0 File Offset: 0x000015E0
		private void HandleObjectNotificationCallback(Notification notification)
		{
			MapiNotificationManager manager = this.Manager;
			try
			{
				Trace sessionTracer = ProtocolBaseServices.SessionTracer;
				if (sessionTracer != null)
				{
					sessionTracer.TraceDebug<string>((manager == null) ? 0L : manager.Factory.Session.SessionId, "User {0} entering Imap4FolderHierarchyHandler.HandleObjectNotificationCallback.", (manager == null) ? "(Unknown user)" : manager.Factory.Session.GetUserNameForLogging());
				}
				ObjectNotification objectNotification = notification as ObjectNotification;
				if (objectNotification != null && objectNotification.ObjectType == NotificationObjectType.Folder)
				{
					if (!this.reloadNeeded)
					{
						if (manager != null)
						{
							if (manager.Factory.SelectedMailbox != null && manager.Factory.SelectedMailbox.Uid.Equals(objectNotification.NotifyingItemId))
							{
								manager.EnqueueNotification(objectNotification, this);
							}
						}
					}
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted((manager == null) ? 0L : manager.Factory.Session.SessionId);
			}
		}

		// Token: 0x04000014 RID: 20
		private static PropertyDefinition[] viewProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName
		};

		// Token: 0x04000015 RID: 21
		private Imap4ResponseFactory factory;

		// Token: 0x04000016 RID: 22
		private QueryResult view;

		// Token: 0x04000017 RID: 23
		private Subscription viewSubscription;

		// Token: 0x04000018 RID: 24
		private Subscription objectSubscription;

		// Token: 0x04000019 RID: 25
		private bool reloadNeeded;
	}
}
