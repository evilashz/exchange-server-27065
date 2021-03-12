using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200004C RID: 76
	internal class MapiNotificationManager : DisposeTrackableBase
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00013D28 File Offset: 0x00011F28
		public MapiNotificationManager(Imap4ResponseFactory factory)
		{
			this.factory = factory;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00013D58 File Offset: 0x00011F58
		public Imap4ResponseFactory Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00013D60 File Offset: 0x00011F60
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00013D68 File Offset: 0x00011F68
		public bool InRealTimeMode { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00013D71 File Offset: 0x00011F71
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00013D79 File Offset: 0x00011F79
		public bool CapReached { get; set; }

		// Token: 0x060002BD RID: 701 RVA: 0x00013D84 File Offset: 0x00011F84
		public void RegisterHandler(IMapiNotificationHandler handler)
		{
			if (this.notificationHandlers.Contains(handler))
			{
				throw new ArgumentException("handler is already registered");
			}
			lock (this.thisLock)
			{
				this.notificationHandlers.Add(handler);
			}
			handler.Manager = this;
			try
			{
				handler.SubscribeNotification();
			}
			catch
			{
				this.UnregisterHandler(handler);
				throw;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00013E08 File Offset: 0x00012008
		public void UnregisterHandler(IMapiNotificationHandler handler)
		{
			lock (this.thisLock)
			{
				this.notificationHandlers.Remove(handler);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00013E50 File Offset: 0x00012050
		public void UnregisterAllHandlers()
		{
			IMapiNotificationHandler[] array = null;
			lock (this.thisLock)
			{
				if (this.notificationHandlers.Count == 0)
				{
					return;
				}
				array = new IMapiNotificationHandler[this.notificationHandlers.Count];
				this.notificationHandlers.CopyTo(array, 0);
				this.notificationHandlers.Clear();
				this.notificationQueue.Clear();
			}
			if (array != null)
			{
				foreach (IMapiNotificationHandler mapiNotificationHandler in array)
				{
					mapiNotificationHandler.UnsubscribeNotification();
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00013EF4 File Offset: 0x000120F4
		public bool ProcessNotifications()
		{
			MapiNotificationManager.NotificationRegistry[] array = null;
			bool result = false;
			lock (this.thisLock)
			{
				if (this.notificationQueue.Count == 0)
				{
					return result;
				}
				array = new MapiNotificationManager.NotificationRegistry[this.notificationQueue.Count];
				this.notificationQueue.CopyTo(array, 0);
				this.notificationQueue.Clear();
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (this.Factory.NothingToDo())
					{
						return result;
					}
					MapiNotificationManager.NotificationRegistry notificationRegistry = array[i];
					if (this.notificationHandlers.Contains(notificationRegistry.NotificationHandler))
					{
						notificationRegistry.NotificationHandler.ProcessNotification(notificationRegistry.Notification);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00013FD0 File Offset: 0x000121D0
		public void EnqueueNotification(Notification notification, IMapiNotificationHandler handler)
		{
			lock (this.thisLock)
			{
				if (base.IsDisposed || this.CapReached)
				{
					return;
				}
				if (!this.notificationHandlers.Contains(handler))
				{
					return;
				}
				if (this.notificationQueue.Count >= 1000)
				{
					this.CapReached = true;
					this.notificationQueue.Clear();
				}
				else
				{
					this.notificationQueue.Enqueue(new MapiNotificationManager.NotificationRegistry(notification, handler));
				}
			}
			if (this.InRealTimeMode || this.CapReached)
			{
				this.Factory.Session.SendToClient(new DataProcessResponseItem(this, new Action<ProtocolSession, DataProcessResponseItem>(MapiNotificationManager.ProcessNotificationsCallback)));
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001409C File Offset: 0x0001229C
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				this.UnregisterAllHandlers();
			}
			catch (LocalizedException ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Factory.Session.SessionId, "Exception caught while disposing MAPI notification manager. {0}", ex.ToString());
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000140EC File Offset: 0x000122EC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiNotificationManager>(this);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000140F4 File Offset: 0x000122F4
		private static void ProcessNotificationsCallback(ProtocolSession session, DataProcessResponseItem responseItem)
		{
			MapiNotificationManager mapiNotificationManager = responseItem.StateData as MapiNotificationManager;
			session.EnterCommandProcessing();
			try
			{
				if (mapiNotificationManager.CapReached)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(session.SessionId, "Start reload all views and notification handlers.");
					((Imap4ResponseFactory)session.ResponseFactory).ReloadAllNotificationHandlers();
					mapiNotificationManager.CapReached = false;
				}
				else if (mapiNotificationManager.ProcessNotifications())
				{
					string notifications = mapiNotificationManager.Factory.SelectedMailbox.GetNotifications();
					if (!string.IsNullOrEmpty(notifications))
					{
						responseItem.EnqueueResponseItem(new StringResponseItem(notifications));
					}
				}
			}
			finally
			{
				session.LeaveCommandProcessing();
			}
		}

		// Token: 0x04000213 RID: 531
		private const int MaxQueuedNotifications = 1000;

		// Token: 0x04000214 RID: 532
		private object thisLock = new object();

		// Token: 0x04000215 RID: 533
		private Queue<MapiNotificationManager.NotificationRegistry> notificationQueue = new Queue<MapiNotificationManager.NotificationRegistry>();

		// Token: 0x04000216 RID: 534
		private List<IMapiNotificationHandler> notificationHandlers = new List<IMapiNotificationHandler>();

		// Token: 0x04000217 RID: 535
		private Imap4ResponseFactory factory;

		// Token: 0x0200004D RID: 77
		private struct NotificationRegistry
		{
			// Token: 0x060002C5 RID: 709 RVA: 0x00014194 File Offset: 0x00012394
			public NotificationRegistry(Notification notification, IMapiNotificationHandler handler)
			{
				this.NotificationHandler = handler;
				this.Notification = notification;
			}

			// Token: 0x0400021A RID: 538
			public IMapiNotificationHandler NotificationHandler;

			// Token: 0x0400021B RID: 539
			public Notification Notification;
		}
	}
}
