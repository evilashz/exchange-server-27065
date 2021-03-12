using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000007 RID: 7
	internal class Imap4DataAccessViewHandler : DataAccessView, IMapiNotificationHandler
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000027C4 File Offset: 0x000009C4
		public Imap4DataAccessViewHandler(ResponseFactory factory, Folder folder) : base(factory, folder)
		{
			object[][] rows = base.TableView.GetRows(DataAccessView.UidCacheSize / 2);
			for (int i = 0; i < rows.Length; i++)
			{
				base.AddStoreObjectIdToCache(((VersionedId)rows[i][0]).ObjectId, (int)rows[i][1]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002819 File Offset: 0x00000A19
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002821 File Offset: 0x00000A21
		public MapiNotificationManager Manager { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000282A File Offset: 0x00000A2A
		protected override PropertyDefinition[] AdditionalProperties
		{
			get
			{
				return Imap4DataAccessViewHandler.additionalProperties;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002831 File Offset: 0x00000A31
		public void SubscribeNotification()
		{
			if (this.subscription != null)
			{
				throw new InvalidOperationException("Should unsubscribe first");
			}
			this.subscription = Subscription.Create(base.TableView, new NotificationHandler(this.HandleNotificationCallback));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002863 File Offset: 0x00000A63
		public void UnsubscribeNotification()
		{
			if (this.subscription != null)
			{
				this.subscription.Dispose();
				this.subscription = null;
			}
			if (this.Manager != null)
			{
				this.Manager.UnregisterHandler(this);
				this.Manager = null;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000289C File Offset: 0x00000A9C
		public void ProcessNotification(Notification notification)
		{
			QueryNotification queryNotification = notification as QueryNotification;
			switch (queryNotification.EventType)
			{
			case QueryNotificationType.QueryResultChanged:
			case QueryNotificationType.Error:
			case QueryNotificationType.Reload:
				ProtocolBaseServices.SessionTracer.TraceDebug(this.Manager.Factory.Session.SessionId, "Start reload all views and notification handlers.");
				this.Manager.Factory.ReloadAllNotificationHandlers();
				this.reloadNeeded = false;
				return;
			case QueryNotificationType.RowAdded:
			{
				int docId;
				int imapId;
				StoreObjectId storeObjectId;
				Imap4Flags flags;
				int size;
				if (Imap4DataAccessViewHandler.GetDataFromNotification(queryNotification, out docId, out imapId, out storeObjectId, out flags, out size))
				{
					base.AddStoreObjectIdToCache(storeObjectId, imapId);
					this.Manager.Factory.SelectedMailbox.AddMessage(imapId, docId, flags, size);
				}
				return;
			}
			case QueryNotificationType.RowModified:
			{
				int docId2;
				int imapId2;
				StoreObjectId storeObjectId2;
				Imap4Flags flags2;
				int size2;
				if (Imap4DataAccessViewHandler.GetDataFromNotification(queryNotification, out docId2, out imapId2, out storeObjectId2, out flags2, out size2))
				{
					bool flag = this.Manager.Factory.SelectedMailbox.ModifyMessage(imapId2, docId2, flags2, size2);
					if (flag)
					{
						base.AddStoreObjectIdToCache(storeObjectId2, imapId2);
					}
				}
				return;
			}
			}
			throw new InvalidOperationException("Unexpected table notification type: " + queryNotification.EventType);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029B8 File Offset: 0x00000BB8
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				this.UnsubscribeNotification();
			}
			catch (LocalizedException ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Manager.Factory.Session.SessionId, "Exception caught while disposing unsubscribing DataAccessView notifications. {0}", ex.ToString());
			}
			finally
			{
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A20 File Offset: 0x00000C20
		private static bool GetDataFromNotification(QueryNotification queryNotification, out int docId, out int imapId, out StoreObjectId storeObjectId, out Imap4Flags flags, out int size)
		{
			docId = -1;
			imapId = -1;
			storeObjectId = null;
			flags = Imap4Flags.None;
			size = -1;
			object obj = queryNotification.Row[2];
			if (obj == null || obj is PropertyError)
			{
				return false;
			}
			docId = (int)obj;
			object obj2 = queryNotification.Row[1];
			if (obj2 == null || obj2 is PropertyError)
			{
				return false;
			}
			imapId = (int)obj2;
			object obj3 = queryNotification.Row[0];
			if (obj3 == null || obj3 is PropertyError)
			{
				return false;
			}
			storeObjectId = ((VersionedId)obj3).ObjectId;
			flags = Imap4FlagsHelper.Parse(queryNotification.Row[3], queryNotification.Row[4], queryNotification.Row[5], queryNotification.Row[6], queryNotification.Row[7], queryNotification.Row[8], queryNotification.Row[9]);
			object obj4 = queryNotification.Row[10];
			if (obj4 == null || obj4 is PropertyError)
			{
				return false;
			}
			size = (int)obj4;
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B00 File Offset: 0x00000D00
		private void HandleNotificationCallback(Notification notification)
		{
			MapiNotificationManager manager = this.Manager;
			try
			{
				Trace sessionTracer = ProtocolBaseServices.SessionTracer;
				if (sessionTracer != null)
				{
					sessionTracer.TraceDebug<string>((manager == null) ? 0L : manager.Factory.Session.SessionId, "User {0} entering Imap4DataAccessViewHandler.HandleNotificationCallback.", (manager == null) ? "(Unknown user)" : manager.Factory.Session.GetUserNameForLogging());
				}
				if (notification.Type != NotificationType.Query)
				{
					throw new NotSupportedException("Not supported exception: " + notification.Type);
				}
				if (!this.reloadNeeded)
				{
					if (manager != null)
					{
						QueryNotification queryNotification = notification as QueryNotification;
						switch (queryNotification.EventType)
						{
						case QueryNotificationType.QueryResultChanged:
						case QueryNotificationType.Error:
						case QueryNotificationType.Reload:
							manager.EnqueueNotification(notification, this);
							this.reloadNeeded = true;
							break;
						case QueryNotificationType.RowAdded:
						case QueryNotificationType.RowModified:
							manager.EnqueueNotification(notification, this);
							break;
						}
					}
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted((manager == null) ? 0L : manager.Factory.Session.SessionId);
			}
		}

		// Token: 0x04000004 RID: 4
		private static PropertyDefinition[] additionalProperties = new PropertyDefinition[]
		{
			ItemSchema.DocumentId,
			MessageItemSchema.MessageDelMarked,
			MessageItemSchema.MessageAnswered,
			MessageItemSchema.MessageTagged,
			MessageItemSchema.MessageDeliveryNotificationSent,
			MessageItemSchema.MimeConversionFailed,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead,
			ItemSchema.Size
		};

		// Token: 0x04000005 RID: 5
		private Subscription subscription;

		// Token: 0x04000006 RID: 6
		private bool reloadNeeded;

		// Token: 0x02000008 RID: 8
		internal struct AdditionalPropertyIndex
		{
			// Token: 0x04000008 RID: 8
			public const int DocumentId = 2;

			// Token: 0x04000009 RID: 9
			public const int MessageDelMarked = 3;

			// Token: 0x0400000A RID: 10
			public const int MessageAnswered = 4;

			// Token: 0x0400000B RID: 11
			public const int MessageTagged = 5;

			// Token: 0x0400000C RID: 12
			public const int MessageDeliveryNotificationSent = 6;

			// Token: 0x0400000D RID: 13
			public const int MimeConversionFailed = 7;

			// Token: 0x0400000E RID: 14
			public const int IsDraft = 8;

			// Token: 0x0400000F RID: 15
			public const int IsRead = 9;

			// Token: 0x04000010 RID: 16
			public const int Size = 10;
		}
	}
}
