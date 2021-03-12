using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001BC RID: 444
	internal class UnseenItemNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003CDA1 File Offset: 0x0003AFA1
		public UnseenItemNotificationHandler(IMailboxContext userContext, IRecipientSession adSession) : base(userContext, true)
		{
			this.adSession = adSession;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003CDC8 File Offset: 0x0003AFC8
		internal string AddMemberSubscription(string subscriptionId, UserMailboxLocator mailboxLocator)
		{
			if (this.groupNotificationLocator == null)
			{
				throw new InvalidOperationException("Cannot add member subscription before subscribing to store notifications");
			}
			string subscriptionId2 = ModernGroupNotificationLocator.GetSubscriptionId(mailboxLocator);
			lock (this.syncNotifierCache)
			{
				if (!this.notifierCache.ContainsKey(subscriptionId2))
				{
					UnseenItemNotifier unseenItemNotifier = new UnseenItemNotifier(subscriptionId, base.UserContext, subscriptionId2, mailboxLocator);
					unseenItemNotifier.RegisterWithPendingRequestNotifier();
					this.notifierCache.Add(subscriptionId2, unseenItemNotifier);
				}
			}
			return subscriptionId2;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003CE50 File Offset: 0x0003B050
		internal void RemoveSubscription(string subscriptionId)
		{
			lock (this.syncNotifierCache)
			{
				if (this.notifierCache.ContainsKey(subscriptionId))
				{
					UnseenItemNotifier unseenItemNotifier = this.notifierCache[subscriptionId];
					unseenItemNotifier.UnregisterWithPendingRequestNotifier();
					this.notifierCache.Remove(subscriptionId);
				}
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003CEB8 File Offset: 0x0003B0B8
		internal bool HasNotifiers()
		{
			bool result;
			lock (this.syncNotifierCache)
			{
				result = (this.notifierCache.Count > 0);
			}
			return result;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003CF04 File Offset: 0x0003B104
		internal override void HandleNotificationInternal(Notification notification, MapiNotificationsLogEvent logEvent, object context)
		{
			if (!(notification is QueryNotification))
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "UnseenItemNotificationHandler.HandleNotificationInternal: Received a null QueryNotification object for group {0}", base.UserContext.PrimarySmtpAddress);
				logEvent.NullNotification = true;
				return;
			}
			lock (base.SyncRoot)
			{
				if (!base.IsDisposed)
				{
					this.GenerateAndAddGroupNotificationPayload();
				}
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003CF84 File Offset: 0x0003B184
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.MissedNotifications)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UnseenItemNotificationHandler.HandlePendingGetTimerCallback this.MissedNotifications == true. SubscriptionId: {0}", base.SubscriptionId);
					base.NeedRefreshPayload = true;
				}
				if (base.NeedRefreshPayload)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UnseenItemNotificationHandler.HandlePendingGetTimerCallback NeedRefreshPayload. SubscriptionId: {0}", base.SubscriptionId);
					this.GenerateAndAddGroupNotificationPayload();
					base.NeedRefreshPayload = false;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UnseenItemNotificationHandler.HandlePendingGetTimerCallback setting this.MissedNotifications = false. SubscriptionId: {0}", base.SubscriptionId);
				base.MissedNotifications = false;
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003D04C File Offset: 0x0003B24C
		protected virtual UnseenItemNotificationHandler.NotifierData[] GetUnSeenData()
		{
			UnseenItemNotifier[] notifierList = this.GetNotifierList();
			IMemberSubscriptionItem[] array = null;
			try
			{
				base.UserContext.LockAndReconnectMailboxSession(3000);
				this.unseenItemsReader.LoadLastNItemReceiveDates(base.UserContext.MailboxSession);
				array = this.groupNotificationLocator.GetMemberSubscriptions(base.UserContext.MailboxSession, from n in notifierList
				select n.UserMailboxLocator);
			}
			finally
			{
				if (base.UserContext.MailboxSessionLockedByCurrentThread())
				{
					base.UserContext.UnlockAndDisconnectMailboxSession();
				}
			}
			UnseenItemNotificationHandler.NotifierData[] array2 = new UnseenItemNotificationHandler.NotifierData[notifierList.Length];
			for (int i = 0; i < notifierList.Length; i++)
			{
				array2[i] = new UnseenItemNotificationHandler.NotifierData(notifierList[i], array[i].LastUpdateTimeUTC);
			}
			return array2;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003D11C File Offset: 0x0003B31C
		protected override void InitSubscriptionInternal()
		{
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling method UnseenItemNotificationHandler.InitSubscriptionInternal");
			}
			if (this.unseenItemsReader != null)
			{
				this.unseenItemsReader.Dispose();
			}
			this.groupNotificationLocator = new ModernGroupNotificationLocator(this.adSession);
			this.unseenItemsReader = UnseenItemsReader.Create(base.UserContext.MailboxSession);
			StoreObjectId defaultFolderId = base.UserContext.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, defaultFolderId))
			{
				base.QueryResult = this.GetQueryResult(folder);
				base.QueryResult.GetRows(base.QueryResult.EstimatedRowCount);
				base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "UnseenItemNotificationHandler.InitSubscriptionInternal succeeded for group {0}", base.UserContext.PrimarySmtpAddress);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003D220 File Offset: 0x0003B420
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool, SmtpAddress, Type>((long)this.GetHashCode(), "UnseenItemNotificationHandler.Dispose. IsDisposing: {0}, User: {1}, Type: {2}", isDisposing, base.UserContext.PrimarySmtpAddress, base.GetType());
			lock (base.SyncRoot)
			{
				if (isDisposing && this.unseenItemsReader != null)
				{
					MapiNotificationHandlerBase.DisposeXSOObjects(this.unseenItemsReader, base.UserContext);
					this.unseenItemsReader = null;
				}
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003D2AC File Offset: 0x0003B4AC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UnseenItemNotificationHandler>(this);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003D2B4 File Offset: 0x0003B4B4
		private void GenerateAndAddGroupNotificationPayload()
		{
			foreach (UnseenItemNotificationHandler.NotifierData notifierData in this.GetUnSeenData())
			{
				notifierData.Notifier.AddGroupNotificationPayload(this.GetPayload(notifierData));
				notifierData.Notifier.PickupData();
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003D2F7 File Offset: 0x0003B4F7
		private QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "UnseenItemNotificationHandler.GetQueryResult for group {0}", base.UserContext.PrimarySmtpAddress);
			return folder.ItemQuery(ItemQueryType.None, null, UnseenItemNotificationHandler.UnseenItemSortBy, UnseenItemNotificationHandler.UnseenItemQueryProperties);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003D32C File Offset: 0x0003B52C
		protected UnseenItemNotifier[] GetNotifierList()
		{
			UnseenItemNotifier[] result;
			lock (this.syncNotifierCache)
			{
				UnseenItemNotifier[] array = new UnseenItemNotifier[this.notifierCache.Values.Count];
				this.notifierCache.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003D394 File Offset: 0x0003B594
		private UnseenItemNotificationPayload GetPayload(UnseenItemNotificationHandler.NotifierData data)
		{
			return new UnseenItemNotificationPayload
			{
				SubscriptionId = data.Notifier.PayloadSubscriptionId,
				UnseenData = new UnseenDataType(this.unseenItemsReader.GetUnseenItemCount(data.LastVisitedDateUTC), ExDateTimeConverter.ToUtcXsdDateTime(data.LastVisitedDateUTC)),
				Source = MailboxLocation.FromMailboxContext(base.UserContext)
			};
		}

		// Token: 0x0400096E RID: 2414
		protected readonly Dictionary<string, UnseenItemNotifier> notifierCache = new Dictionary<string, UnseenItemNotifier>();

		// Token: 0x0400096F RID: 2415
		protected IUnseenItemsReader unseenItemsReader;

		// Token: 0x04000970 RID: 2416
		private readonly object syncNotifierCache = new object();

		// Token: 0x04000971 RID: 2417
		private static readonly PropertyDefinition[] UnseenItemQueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000972 RID: 2418
		private static readonly SortBy[] UnseenItemSortBy = new SortBy[]
		{
			new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
		};

		// Token: 0x04000973 RID: 2419
		private readonly IRecipientSession adSession;

		// Token: 0x04000974 RID: 2420
		private ModernGroupNotificationLocator groupNotificationLocator;

		// Token: 0x020001BD RID: 445
		internal class NotifierData
		{
			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003D431 File Offset: 0x0003B631
			// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0003D439 File Offset: 0x0003B639
			public UnseenItemNotifier Notifier { get; private set; }

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003D442 File Offset: 0x0003B642
			// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0003D44A File Offset: 0x0003B64A
			public ExDateTime LastVisitedDateUTC { get; private set; }

			// Token: 0x06000FDA RID: 4058 RVA: 0x0003D453 File Offset: 0x0003B653
			public NotifierData(UnseenItemNotifier notifier, ExDateTime lastVisitedDateUTC)
			{
				this.Notifier = notifier;
				this.LastVisitedDateUTC = lastVisitedDateUTC;
			}
		}
	}
}
