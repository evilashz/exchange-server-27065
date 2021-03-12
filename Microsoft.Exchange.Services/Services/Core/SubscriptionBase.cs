using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000231 RID: 561
	internal abstract class SubscriptionBase : IDisposable
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x00046996 File Offset: 0x00044B96
		public SubscriptionBase(SubscriptionRequestBase subscriptionRequest, IdAndSession[] folderIds, Guid subscriptionOwnerObjectGuid) : this(subscriptionRequest, folderIds)
		{
			this.ownerObjectGuid = subscriptionOwnerObjectGuid;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000469A8 File Offset: 0x00044BA8
		protected SubscriptionBase(SubscriptionRequestBase subscriptionRequest, IdAndSession[] folderIds)
		{
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<int>((long)this.GetHashCode(), "SubscriptionBase constructor called. Hashcode: {0}.", this.GetHashCode());
			this.budgetKey = CallContext.Current.Budget.Owner;
			this.mailboxVersion = CallContext.Current.GetAccessingPrincipalServerVersion();
			StoreSession storeSessionForOperation = this.GetStoreSessionForOperation(folderIds);
			if (storeSessionForOperation is MailboxSession)
			{
				this.mailboxId = new MailboxId((MailboxSession)storeSessionForOperation);
			}
			Guid mailboxGuid = (this.MailboxId != null) ? new Guid(this.MailboxId.MailboxGuid) : Guid.Empty;
			this.subscriptionId = new SubscriptionId(Guid.NewGuid(), mailboxGuid).ToString();
			StoreId[] array = null;
			if (folderIds != null)
			{
				array = new StoreId[folderIds.Length];
				for (int i = 0; i < folderIds.Length; i++)
				{
					array[i] = folderIds[i].Id;
				}
			}
			this.eventQueue = this.CreateEventQueue(subscriptionRequest, storeSessionForOperation, array);
			this.lastWatermarkSent = this.eventQueue.CurrentWatermark.ToBase64String();
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, BudgetKey>((long)this.GetHashCode(), "SubscriptionBase constructor. Subscription: {0}. BudgetKey: {1}", this.TraceIdentifier, this.budgetKey);
			if (CallContext.Current.AccessingPrincipal == null)
			{
				this.CreatorSmtpAddress = string.Empty;
				return;
			}
			RemoteUserMailboxPrincipal remoteUserMailboxPrincipal = CallContext.Current.AccessingPrincipal as RemoteUserMailboxPrincipal;
			if (remoteUserMailboxPrincipal != null)
			{
				this.CreatorSmtpAddress = remoteUserMailboxPrincipal.PrimarySmtpAddress.ToString();
				return;
			}
			this.CreatorSmtpAddress = CallContext.Current.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00046B40 File Offset: 0x00044D40
		private static EventWatermark ConvertToEventWatermark(string watermark)
		{
			if (!string.IsNullOrEmpty(watermark))
			{
				try
				{
					return EventWatermark.Deserialize(watermark);
				}
				catch (CorruptDataException innerException)
				{
					throw new InvalidWatermarkException(innerException);
				}
			}
			return null;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00046B7C File Offset: 0x00044D7C
		private static void AddFoldersToMonitor(EventCondition eventCondition, StoreId[] folderIds)
		{
			if (folderIds != null)
			{
				for (int i = 0; i < folderIds.Length; i++)
				{
					StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(folderIds[i]);
					eventCondition.ContainerFolderIds.Add(asStoreObjectId);
					eventCondition.ObjectIds.Add(asStoreObjectId);
				}
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00046BBC File Offset: 0x00044DBC
		private EventQueue CreateEventQueue(SubscriptionRequestBase subscriptionRequest, StoreSession session, StoreId[] folderIds)
		{
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.CreateEventQueue called. Subscription: {0}.", this.TraceIdentifier);
			EventWatermark eventWatermark = SubscriptionBase.ConvertToEventWatermark(subscriptionRequest.Watermark);
			this.EventCondition = this.CreateEventCondition(subscriptionRequest, folderIds);
			this.EventCondition.EventSubtree = ((subscriptionRequest.SubscribeToAllFolders && this.mailboxVersion >= Server.E14SP1MinVersion) ? EventSubtreeFlag.IPMSubtree : EventSubtreeFlag.All);
			EventQueue result;
			if (eventWatermark == null)
			{
				result = EventQueue.Create(session, this.EventCondition, this.EventQueueSize);
			}
			else
			{
				result = EventQueue.Create(session, this.EventCondition, this.EventQueueSize, eventWatermark);
			}
			return result;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00046C50 File Offset: 0x00044E50
		protected StoreSession GetStoreSessionForOperation(IdAndSession[] folderIds)
		{
			StoreSession result;
			if (folderIds != null && folderIds.Length != 0)
			{
				result = folderIds[0].Session;
			}
			else
			{
				result = CallContext.Current.SessionCache.GetMailboxIdentityMailboxSession();
			}
			return result;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00046C84 File Offset: 0x00044E84
		private EventCondition CreateEventCondition(SubscriptionRequestBase subscriptionRequest, StoreId[] folderIds)
		{
			EventCondition eventCondition = new EventCondition();
			eventCondition.ObjectType = (EventObjectType.Item | EventObjectType.Folder);
			SubscriptionBase.AddFoldersToMonitor(eventCondition, folderIds);
			this.SetEventTypesToMonitor(eventCondition, subscriptionRequest.EventTypes);
			return eventCondition;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00046CB4 File Offset: 0x00044EB4
		private void SetEventTypesToMonitor(EventCondition eventCondition, EventType[] eventTypes)
		{
			eventCondition.EventType = EventType.None;
			for (int i = 0; i < eventTypes.Length; i++)
			{
				eventCondition.EventType |= (EventType)eventTypes[i];
			}
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, EventType>((long)this.GetHashCode(), "SubscriptionBase.CreateEvent. Subscription: {0}. EventType: {1}", this.TraceIdentifier, eventCondition.EventType);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00046D08 File Offset: 0x00044F08
		public string SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00046D10 File Offset: 0x00044F10
		public BudgetKey BudgetKey
		{
			get
			{
				return this.budgetKey;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00046D18 File Offset: 0x00044F18
		public bool CheckForEventsLater()
		{
			bool result;
			lock (this.lockObject)
			{
				if (this.isDisposed)
				{
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.CheckForEventsLater. InvalidSubscriptionException. Subscription: {0}.", this.TraceIdentifier);
					throw new InvalidSubscriptionException();
				}
				result = !this.eventQueue.IsQueueEmptyAndUpToDate();
			}
			return result;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00046D8C File Offset: 0x00044F8C
		internal string LastWatermarkSent
		{
			get
			{
				return this.lastWatermarkSent;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00046D94 File Offset: 0x00044F94
		internal MailboxId MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00046D9C File Offset: 0x00044F9C
		internal EventQueue EventQueue
		{
			get
			{
				return this.eventQueue;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00046DA4 File Offset: 0x00044FA4
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x00046DAC File Offset: 0x00044FAC
		internal EventCondition EventCondition { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00046DB5 File Offset: 0x00044FB5
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x00046DBD File Offset: 0x00044FBD
		internal string CreatorSmtpAddress { get; private set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00046DC6 File Offset: 0x00044FC6
		internal string TraceIdentifier
		{
			get
			{
				if (this.MailboxId != null)
				{
					return string.Format("{0}:{1}", this.MailboxId.SmtpAddress, this.SubscriptionId);
				}
				return this.SubscriptionId;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000E7B RID: 3707
		protected abstract int EventQueueSize { get; }

		// Token: 0x06000E7C RID: 3708 RVA: 0x00046DF4 File Offset: 0x00044FF4
		public virtual EwsNotificationType GetEvents(string theLastWatermarkSent)
		{
			int num;
			return this.GetEvents(theLastWatermarkSent, 50, out num);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00046E0C File Offset: 0x0004500C
		public virtual EwsNotificationType GetEvents(string theLastWatermarkSent, int maxEventCount, out int eventCount)
		{
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.GetEvents called. Before lock. Subscription: {0}.", this.TraceIdentifier);
			EwsNotificationType result;
			lock (this.lockObject)
			{
				if (this.isDisposed)
				{
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.GetEvents. InvalidSubscriptionException. Subscription: {0}.", this.TraceIdentifier);
					throw new InvalidSubscriptionException();
				}
				this.VerifyLastWatermarkSent(theLastWatermarkSent);
				List<Event> list;
				try
				{
					list = this.CreateEventList(maxEventCount);
					eventCount = list.Count;
				}
				catch (Exception arg)
				{
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "SubscriptionBase::GetEvents Subscription {0} failed with exception {1}", this.TraceIdentifier, arg);
					PerformanceMonitor.UpdateFailedSubscriptionCounter();
					throw;
				}
				string lastWatermark = this.GetLastWatermark(list);
				this.TraceEvents(list, lastWatermark);
				EwsNotificationType ewsNotificationType = this.CreateNotifications(list, lastWatermark);
				this.lastWatermarkSent = lastWatermark;
				ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<int, EwsNotificationType>((long)this.GetHashCode(), "SubscriptionBase.GetEvents. Hashcode: {0}. Notifications: {1}", this.GetHashCode(), ewsNotificationType);
				result = ewsNotificationType;
			}
			return result;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00046F1C File Offset: 0x0004511C
		private void VerifyLastWatermarkSent(string theLastWatermarkSent)
		{
			if (string.CompareOrdinal(theLastWatermarkSent, this.lastWatermarkSent) != 0)
			{
				ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.GetEvents. InvalidWatermarkException. Subscription: {0}.", this.TraceIdentifier);
				throw new InvalidWatermarkException();
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00046F50 File Offset: 0x00045150
		private List<Event> CreateEventList(int maximumEvents)
		{
			List<Event> list = new List<Event>();
			Event @event;
			do
			{
				@event = this.eventQueue.GetEvent();
				FaultInjection.GenerateFault((FaultInjection.LIDs)3534105917U);
				if (@event != null)
				{
					list.Add(@event);
				}
			}
			while (@event != null && list.Count < maximumEvents);
			if (list.Count > 1)
			{
				Dictionary<Tuple<StoreObjectId, StoreObjectId>, Event> dictionary = new Dictionary<Tuple<StoreObjectId, StoreObjectId>, Event>(list.Count / 2 + 1);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					Event event2 = list[i];
					if (event2.EventType == EventType.ObjectModified)
					{
						Tuple<StoreObjectId, StoreObjectId> key = Tuple.Create<StoreObjectId, StoreObjectId>(event2.ObjectId, event2.ParentObjectId);
						if (dictionary.ContainsKey(key))
						{
							list.RemoveAt(i);
						}
						else
						{
							dictionary.Add(key, event2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00047004 File Offset: 0x00045204
		private EwsNotificationType CreateNotifications(List<Event> events, string newLastWatermarkSent)
		{
			EwsNotificationType ewsNotificationType = new EwsNotificationType();
			SubscriptionBase.NotificationBuilder notificationBuilder = new SubscriptionBase.NotificationBuilder(this, ewsNotificationType);
			if (events.Count > 0)
			{
				for (int i = 0; i < events.Count; i++)
				{
					notificationBuilder.AddEvent(events[i]);
				}
			}
			else
			{
				notificationBuilder.AddStatusEvent(newLastWatermarkSent);
			}
			return ewsNotificationType;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00047050 File Offset: 0x00045250
		private string GetLastWatermark(List<Event> events)
		{
			EventWatermark eventWatermark;
			if (events.Count > 0)
			{
				eventWatermark = events[events.Count - 1].EventWatermark;
			}
			else
			{
				eventWatermark = this.eventQueue.CurrentWatermark;
			}
			return eventWatermark.ToBase64String();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00047090 File Offset: 0x00045290
		public virtual bool CheckCallerHasRights(CallContext callContext)
		{
			Guid objectGuid = callContext.EffectiveCaller.ObjectGuid;
			if (objectGuid == Guid.Empty)
			{
				ExTraceGlobals.SubscriptionBaseTracer.TraceDebug((long)this.GetHashCode(), "[SubscriptionBase::CheckCallerHasRights] Passed callContext.EffectiveCaller.ObjectGuid is Guid.Empty.  Cannot be owner.");
				return false;
			}
			return objectGuid == this.ownerObjectGuid;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000470DC File Offset: 0x000452DC
		protected void TraceEvents(List<Event> events, string lastWatermark)
		{
			if (!ExTraceGlobals.SubscriptionBaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			if (events.Count > 0)
			{
				ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, int>((long)this.GetHashCode(), "TraceEvents: Watermark {0}. {1} events retrieved from notification queue", lastWatermark, events.Count);
				for (int i = 0; i < events.Count; i++)
				{
					Event @event = events[i];
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug((long)this.GetHashCode(), "TraceEvents: Event[{0}] {1} {2} Id:{3} Parent:{4}", new object[]
					{
						i,
						@event.ObjectType,
						@event.EventType,
						@event.ObjectId,
						@event.ParentObjectId
					});
				}
				return;
			}
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, int>((long)this.GetHashCode(), "TraceEvents: Watermark {0}. No events in notification queue", lastWatermark, events.Count);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x000471AC File Offset: 0x000453AC
		public virtual bool IsExpired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000471AF File Offset: 0x000453AF
		public virtual bool UseWatermarks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000471B2 File Offset: 0x000453B2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000471BC File Offset: 0x000453BC
		protected virtual void Dispose(bool isDisposing)
		{
			ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, bool, bool>((long)this.GetHashCode(), "SubscriptionBase.Dispose() called. Before lock. Subscription: {0}. IsDisposed: {1} IsDisposing: {2}", this.TraceIdentifier, this.isDisposed, isDisposing);
			if (!this.isDisposed)
			{
				if (isDisposing)
				{
					lock (this.lockObject)
					{
						ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "SubscriptionBase.Dispose(). After lock. Subscription: {0}. IsDisposed: {1}", this.TraceIdentifier, this.isDisposed);
						if (this.eventQueue != null)
						{
							this.eventQueue.Dispose();
							this.eventQueue = null;
							ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionBase.Dispose(). After EventQueue dispose. Subscription: {0}.", this.TraceIdentifier);
						}
						this.isDisposed = true;
						return;
					}
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000B21 RID: 2849
		private const int MaximumNotificationEventCount = 50;

		// Token: 0x04000B22 RID: 2850
		private string subscriptionId;

		// Token: 0x04000B23 RID: 2851
		protected EventQueue eventQueue;

		// Token: 0x04000B24 RID: 2852
		private string lastWatermarkSent;

		// Token: 0x04000B25 RID: 2853
		private MailboxId mailboxId;

		// Token: 0x04000B26 RID: 2854
		private Guid ownerObjectGuid;

		// Token: 0x04000B27 RID: 2855
		protected bool isDisposed;

		// Token: 0x04000B28 RID: 2856
		private BudgetKey budgetKey;

		// Token: 0x04000B29 RID: 2857
		private int mailboxVersion;

		// Token: 0x04000B2A RID: 2858
		protected object lockObject = new object();

		// Token: 0x02000232 RID: 562
		private sealed class NotificationBuilder
		{
			// Token: 0x06000E88 RID: 3720 RVA: 0x00047290 File Offset: 0x00045490
			internal NotificationBuilder(SubscriptionBase subscription, EwsNotificationType notificationObject)
			{
				this.notificationObject = notificationObject;
				this.subscription = subscription;
				this.supportsExchange2010SchemaVersion = ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2010);
				this.notificationObject.SubscriptionId = this.subscription.SubscriptionId;
				this.mailboxId = subscription.MailboxId;
				this.emitWatermarks = subscription.UseWatermarks;
				if (this.emitWatermarks)
				{
					this.notificationObject.PreviousWatermark = subscription.LastWatermarkSent;
					this.notificationObject.MoreEvents = subscription.CheckForEventsLater();
				}
			}

			// Token: 0x06000E89 RID: 3721 RVA: 0x0004731C File Offset: 0x0004551C
			internal void AddStatusEvent(string watermark)
			{
				BaseNotificationEventType baseNotificationEventType = new BaseNotificationEventType(NotificationTypeEnum.StatusEvent);
				baseNotificationEventType.Watermark = watermark;
				this.notificationObject.AddNotificationEvent(baseNotificationEventType);
			}

			// Token: 0x06000E8A RID: 3722 RVA: 0x00047344 File Offset: 0x00045544
			internal void AddEvent(Event xsoEvent)
			{
				foreach (EventType eventType in SubscriptionBase.NotificationBuilder.allEventTypes)
				{
					if ((xsoEvent.EventType & eventType) == eventType && (this.subscription.EventCondition.EventType & eventType) == eventType)
					{
						BaseNotificationEventType baseNotificationEventType = null;
						EventType eventType2 = eventType;
						if (eventType2 <= EventType.ObjectModified)
						{
							switch (eventType2)
							{
							case EventType.NewMail:
								baseNotificationEventType = this.BuildObjectChangedEvent(NotificationTypeEnum.NewMailEvent, xsoEvent);
								break;
							case EventType.ObjectCreated:
								baseNotificationEventType = this.BuildObjectChangedEvent(NotificationTypeEnum.CreatedEvent, xsoEvent);
								break;
							case EventType.NewMail | EventType.ObjectCreated:
								break;
							case EventType.ObjectDeleted:
								baseNotificationEventType = this.BuildObjectChangedEvent(NotificationTypeEnum.DeletedEvent, xsoEvent);
								break;
							default:
								if (eventType2 == EventType.ObjectModified)
								{
									baseNotificationEventType = this.BuildModifiedEvent(xsoEvent);
								}
								break;
							}
						}
						else if (eventType2 != EventType.ObjectMoved)
						{
							if (eventType2 != EventType.ObjectCopied)
							{
								if (eventType2 == EventType.FreeBusyChanged)
								{
									baseNotificationEventType = this.BuildObjectChangedEvent(NotificationTypeEnum.FreeBusyChangedEvent, xsoEvent);
								}
							}
							else
							{
								baseNotificationEventType = this.BuildMovedCopiedEvent(NotificationTypeEnum.CopiedEvent, xsoEvent);
							}
						}
						else
						{
							baseNotificationEventType = this.BuildMovedCopiedEvent(NotificationTypeEnum.MovedEvent, xsoEvent);
						}
						if (baseNotificationEventType != null)
						{
							this.notificationObject.AddNotificationEvent(baseNotificationEventType);
						}
					}
				}
			}

			// Token: 0x06000E8B RID: 3723 RVA: 0x0004742C File Offset: 0x0004562C
			private BaseNotificationEventType BuildModifiedEvent(Event xsoEvent)
			{
				ModifiedEventType modifiedEventType = new ModifiedEventType();
				this.AddWatermarkAndTimeStamp(xsoEvent, modifiedEventType);
				if (xsoEvent.ObjectType == EventObjectType.Folder)
				{
					modifiedEventType.FolderId = this.CreateFolderId(xsoEvent.ObjectId);
					if (xsoEvent.UnreadItemCount >= 0L)
					{
						modifiedEventType.UnreadCount = (int)xsoEvent.UnreadItemCount;
					}
				}
				else
				{
					modifiedEventType.ItemId = this.CreateItemId(xsoEvent.ObjectId, xsoEvent.ParentObjectId);
				}
				modifiedEventType.ParentFolderId = this.CreateFolderId(xsoEvent.ParentObjectId);
				return modifiedEventType;
			}

			// Token: 0x06000E8C RID: 3724 RVA: 0x000474A8 File Offset: 0x000456A8
			private BaseNotificationEventType BuildMovedCopiedEvent(NotificationTypeEnum eventType, Event xsoEvent)
			{
				MovedCopiedEventType movedCopiedEventType = new MovedCopiedEventType(eventType);
				this.AddWatermarkAndTimeStamp(xsoEvent, movedCopiedEventType);
				if (xsoEvent.ObjectType == EventObjectType.Folder)
				{
					movedCopiedEventType.FolderId = this.CreateFolderId(xsoEvent.ObjectId);
					movedCopiedEventType.OldFolderId = this.CreateFolderId(xsoEvent.OldObjectId);
				}
				else
				{
					movedCopiedEventType.ItemId = this.CreateItemId(xsoEvent.ObjectId, xsoEvent.ParentObjectId);
					movedCopiedEventType.OldItemId = this.CreateItemId(xsoEvent.OldObjectId, xsoEvent.OldParentObjectId);
				}
				movedCopiedEventType.ParentFolderId = this.CreateFolderId(xsoEvent.ParentObjectId);
				movedCopiedEventType.OldParentFolderId = this.CreateFolderId(xsoEvent.OldParentObjectId);
				return movedCopiedEventType;
			}

			// Token: 0x06000E8D RID: 3725 RVA: 0x00047548 File Offset: 0x00045748
			private BaseNotificationEventType BuildObjectChangedEvent(NotificationTypeEnum eventType, Event xsoEvent)
			{
				BaseObjectChangedEventType baseObjectChangedEventType = new BaseObjectChangedEventType(eventType);
				this.AddWatermarkAndTimeStamp(xsoEvent, baseObjectChangedEventType);
				if (xsoEvent.ObjectType == EventObjectType.Folder)
				{
					baseObjectChangedEventType.FolderId = this.CreateFolderId(xsoEvent.ObjectId);
				}
				else
				{
					baseObjectChangedEventType.ItemId = this.CreateItemId(xsoEvent.ObjectId, xsoEvent.ParentObjectId);
				}
				baseObjectChangedEventType.ParentFolderId = this.CreateFolderId(xsoEvent.ParentObjectId);
				return baseObjectChangedEventType;
			}

			// Token: 0x06000E8E RID: 3726 RVA: 0x000475AC File Offset: 0x000457AC
			private void AddWatermarkAndTimeStamp(Event notificationEvent, BaseObjectChangedEventType newEvent)
			{
				if (this.emitWatermarks)
				{
					newEvent.Watermark = notificationEvent.EventWatermark.ToBase64String();
				}
				newEvent.TimeStamp = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(notificationEvent.TimeStamp, this.supportsExchange2010SchemaVersion);
			}

			// Token: 0x06000E8F RID: 3727 RVA: 0x000475E0 File Offset: 0x000457E0
			private FolderId CreateFolderId(StoreObjectId xsoStoreId)
			{
				ConcatenatedIdAndChangeKey concatenatedIdAndChangeKey;
				if (this.mailboxId != null)
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedId(xsoStoreId, this.mailboxId, null);
				}
				else
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedIdForPublicFolder(xsoStoreId);
				}
				return new FolderId(concatenatedIdAndChangeKey.Id, concatenatedIdAndChangeKey.ChangeKey);
			}

			// Token: 0x06000E90 RID: 3728 RVA: 0x00047620 File Offset: 0x00045820
			private ItemId CreateItemId(StoreObjectId xsoStoreId, StoreObjectId xsoParentFolderId)
			{
				ConcatenatedIdAndChangeKey concatenatedIdAndChangeKey;
				if (this.mailboxId != null)
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedId(xsoStoreId, this.mailboxId, null);
				}
				else
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedIdForPublicFolderItem(xsoStoreId, xsoParentFolderId, null);
				}
				return new ItemId(concatenatedIdAndChangeKey.Id, concatenatedIdAndChangeKey.ChangeKey);
			}

			// Token: 0x04000B2D RID: 2861
			private static readonly EventType[] allEventTypes = (EventType[])Enum.GetValues(typeof(EventType));

			// Token: 0x04000B2E RID: 2862
			private EwsNotificationType notificationObject;

			// Token: 0x04000B2F RID: 2863
			private SubscriptionBase subscription;

			// Token: 0x04000B30 RID: 2864
			private bool supportsExchange2010SchemaVersion;

			// Token: 0x04000B31 RID: 2865
			private bool emitWatermarks;

			// Token: 0x04000B32 RID: 2866
			private MailboxId mailboxId;
		}
	}
}
