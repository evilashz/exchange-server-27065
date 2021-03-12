using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000160 RID: 352
	internal abstract class RowNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x000306AC File Offset: 0x0002E8AC
		public RowNotificationHandler(string subscriptionId, SubscriptionParameters parameters, StoreObjectId folderId, IMailboxContext userContext, Guid mailboxGuid, ExTimeZone timeZone, bool remoteSubscription) : this(subscriptionId, parameters, folderId, userContext, mailboxGuid, new RowNotifier(subscriptionId, userContext, mailboxGuid), remoteSubscription)
		{
			this.timeZone = timeZone;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000306DC File Offset: 0x0002E8DC
		protected RowNotificationHandler(string subscriptionId, SubscriptionParameters parameters, StoreObjectId folderId, IMailboxContext userContext, Guid mailboxGuid, RowNotifier notifier, bool remoteSubscription) : base(subscriptionId, userContext, remoteSubscription)
		{
			this.mailboxGuid = mailboxGuid;
			this.notifier = notifier;
			this.notifier.RegisterWithPendingRequestNotifier();
			this.SetSubscriptionParameter(parameters);
			this.folderId = folderId;
			this.originalFolderId = folderId;
			this.originalFolderIdAsString = parameters.FolderId;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0003073B File Offset: 0x0002E93B
		internal ExDateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00030743 File Offset: 0x0002E943
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0003074B File Offset: 0x0002E94B
		internal RowNotifier Notifier
		{
			get
			{
				return this.notifier;
			}
			set
			{
				this.notifier = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00030754 File Offset: 0x0002E954
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0003075C File Offset: 0x0002E95C
		internal SubscriptionParameters SubscriptionParameters { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00030765 File Offset: 0x0002E965
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0003076D File Offset: 0x0002E96D
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00030775 File Offset: 0x0002E975
		internal StoreId SearchFolderId
		{
			get
			{
				return this.searchFolderId;
			}
			set
			{
				this.searchFolderId = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0003077E File Offset: 0x0002E97E
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00030786 File Offset: 0x0002E986
		internal ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0003078F File Offset: 0x0002E98F
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00030797 File Offset: 0x0002E997
		internal int RefCount
		{
			get
			{
				return this.refcount;
			}
			set
			{
				this.refcount = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D0A RID: 3338
		protected abstract PropertyDefinition[] SubscriptionProperties { get; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000307A0 File Offset: 0x0002E9A0
		protected Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000307A8 File Offset: 0x0002E9A8
		protected SortBy[] SortBy
		{
			get
			{
				return this.sortBy;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x000307B0 File Offset: 0x0002E9B0
		protected ViewFilter ViewFilter
		{
			get
			{
				return this.viewFilter;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000307B8 File Offset: 0x0002E9B8
		protected ClutterFilter ClutterFilter
		{
			get
			{
				return this.clutterFilter;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000307C0 File Offset: 0x0002E9C0
		protected string FromFilter
		{
			get
			{
				return this.fromFilter;
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000307C8 File Offset: 0x0002E9C8
		private void SetSubscriptionParameter(SubscriptionParameters parameters)
		{
			this.SubscriptionParameters = parameters;
			if (parameters.SortBy != null)
			{
				this.sortBy = SortResults.ToXsoSortBy(parameters.SortBy);
			}
			if (parameters.Filter != ViewFilter.All)
			{
				this.viewFilter = parameters.Filter;
			}
			if (parameters.ClutterFilter != ClutterFilter.All)
			{
				this.clutterFilter = parameters.ClutterFilter;
			}
			if (!string.IsNullOrEmpty(parameters.FromFilter))
			{
				this.fromFilter = parameters.FromFilter;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00030838 File Offset: 0x0002EA38
		internal override void HandleNotificationInternal(Notification notification, MapiNotificationsLogEvent logEvent, object context)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification row notification received. User: {0}. SubscriptionId: {1}", base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
			lock (base.SyncRoot)
			{
				if (base.IsDisposed_Reentrant)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandlerBase.HandleNotificationInternal for {0}: Ignoring notification because we're disposed (reentrant).", base.GetType().Name);
				}
				else if (notification == null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: Received a null Row Notification object for user {0} SubscriptionId: {1}", base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
					logEvent.NullNotification = true;
				}
				else
				{
					QueryNotification queryNotification = (QueryNotification)notification;
					if (this.ProcessErrorNotification(queryNotification))
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: Received a invalid Row Notification object for user {0} SubscriptionId: {1}", base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
						logEvent.InvalidNotification = true;
					}
					else
					{
						QueryNotificationType eventType = queryNotification.EventType;
						switch (eventType)
						{
						case QueryNotificationType.QueryResultChanged:
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<QueryNotificationType, SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: EventType {0}. Calling ProcessQueryResultChangedNotification. user {1} SubscriptionId: {2}", eventType, base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
							this.ProcessQueryResultChangedNotification();
							goto IL_1EE;
						case QueryNotificationType.RowAdded:
						case QueryNotificationType.RowDeleted:
						case QueryNotificationType.RowModified:
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<QueryNotificationType, SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: EventType {0} for user {1} SubscriptionId: {2}", eventType, base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
							this.notifier.AddFolderContentChangePayload(this.folderId, this.GetPayloadFromNotification(this.folderId, queryNotification));
							goto IL_1EE;
						case QueryNotificationType.Reload:
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<QueryNotificationType, SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: EventType {0}. Calling ProcessReloadNotification. user {1} SubscriptionId: {2}", eventType, base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
							this.ProcessReloadNotification();
							goto IL_1EE;
						}
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<QueryNotificationType, SmtpAddress, string>((long)this.GetHashCode(), "RowNotificationHandler.HandleNotification: EventType {0}. Ignoring. user {1} SubscriptionId: {2}", eventType, base.UserContext.PrimarySmtpAddress, base.SubscriptionId);
						IL_1EE:
						this.notifier.PickupData();
					}
				}
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00030A68 File Offset: 0x0002EC68
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.HandlePendingGetTimerCallback Start. SubscriptionId: {0}", base.SubscriptionId);
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.MissedNotifications)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.HandlePendingGetTimerCallback this.MissedNotifications == true. SubscriptionId: {0}", base.SubscriptionId);
					base.NeedRefreshPayload = true;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.HandlePendingGetTimerCallback setting this.MissedNotifications = false. SubscriptionId: {0}", base.SubscriptionId);
				base.MissedNotifications = false;
			}
			if (base.NeedRefreshPayload)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.HandlePendingGetTimerCallback NeedRefreshPayload.  SubscriptionId: {0}", base.SubscriptionId);
				this.AddFolderRefreshPayload();
				logEvent.RefreshPayloadSent = true;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.HandlePendingGetTimerCallback setting this.NeedRefreshPayload = false, since we have called AddFolderRefresshPayload.  SubscriptionId: {0}", base.SubscriptionId);
				base.NeedRefreshPayload = false;
			}
			this.notifier.PickupData();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00030B74 File Offset: 0x0002ED74
		protected static T GetItemProperty<T>(QueryNotification notification, int index)
		{
			return RowNotificationHandler.GetItemProperty<T>(notification, index, default(T));
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00030B94 File Offset: 0x0002ED94
		protected static T GetItemProperty<T>(QueryNotification notification, int index, T defaultValue)
		{
			if (!RowNotificationHandler.IsPropertyDefined(notification, index))
			{
				return defaultValue;
			}
			object obj = notification.Row[index];
			if (!(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00030BC5 File Offset: 0x0002EDC5
		protected static bool IsPropertyDefined(QueryNotification notification, int index)
		{
			return index >= 0 && index < notification.Row.Length && notification.Row[index] != null && !(notification.Row[index] is PropertyError);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		protected static PropertyDefinition[] GetPropertyDefinitionsForResponseShape(IEnumerable<Shape> shapes, ResponseShape responseShape, params PropertyDefinition[] specialProperties)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			foreach (PropertyPath propertyPath in responseShape.AdditionalProperties)
			{
				ExtendedPropertyUri extendedPropertyUri = propertyPath as ExtendedPropertyUri;
				if (extendedPropertyUri != null)
				{
					list.Add(extendedPropertyUri.ToPropertyDefinition());
				}
				else
				{
					foreach (Shape shape in shapes)
					{
						PropertyInformation propertyInformation;
						if (shape.TryGetPropertyInformation(propertyPath, out propertyInformation))
						{
							list.AddRange(propertyInformation.GetPropertyDefinitions(null));
							break;
						}
					}
				}
			}
			if (specialProperties != null)
			{
				list.AddRange(specialProperties);
			}
			return list.ToArray();
		}

		// Token: 0x06000D17 RID: 3351
		protected abstract NotificationPayloadBase GetPayloadFromNotification(StoreObjectId folderId, QueryNotification notification);

		// Token: 0x06000D18 RID: 3352 RVA: 0x00030CAC File Offset: 0x0002EEAC
		protected void GetPartialPayloadFromNotification(RowNotificationPayload payload, QueryNotification notification)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.GetPartialPayloadFromNotification Start. SubscriptionId: {0}", base.SubscriptionId);
			payload.SubscriptionId = base.SubscriptionId;
			payload.EventType = notification.EventType;
			payload.Prior = Convert.ToBase64String(notification.Prior);
			payload.FolderId = StoreId.StoreIdToEwsId(this.mailboxGuid, this.folderId);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00030D18 File Offset: 0x0002EF18
		protected override void InitSubscriptionInternal()
		{
			if (!base.UserContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method RowNotificationHandler.InitSubscriptionInternal");
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.InitSubscriptionInternal Start. SubscriptionId: {0}", base.SubscriptionId);
			if (!SearchUtil.IsViewFilterNonQuery(this.viewFilter) || this.clutterFilter != ClutterFilter.All)
			{
				this.CreateSubscriptionForFilteredView();
			}
			else
			{
				if (this.folderId == null)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "RowNotificationHandler: OriginalStoreFolderId {0} OriginalStoreFolderIdAsString {1} FolderId {2}", new object[]
					{
						this.originalFolderId,
						this.originalFolderIdAsString,
						this.folderId
					});
					throw new OwaInvalidOperationException(message);
				}
				using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, this.folderId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.InitSubscriptionInternal create folder subscription {0}", base.SubscriptionId);
					this.CreateSubscription(folder);
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.InitSubscriptionInternal End. subscription {0}", base.SubscriptionId);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00030E30 File Offset: 0x0002F030
		protected virtual QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.GetQueryResult. subscription {0}", base.SubscriptionId);
			QueryFilter queryFilter = null;
			SortBy[] itemQuerySortBy = this.SortBy;
			if (!string.IsNullOrEmpty(this.FromFilter))
			{
				queryFilter = PeopleIKnowQuery.GetItemQueryFilter(this.FromFilter);
				itemQuerySortBy = PeopleIKnowQuery.GetItemQuerySortBy(this.SortBy);
			}
			return folder.ItemQuery(ItemQueryType.None, queryFilter, itemQuerySortBy, this.SubscriptionProperties);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00030E96 File Offset: 0x0002F096
		protected virtual void ProcessReloadNotification()
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00030E98 File Offset: 0x0002F098
		protected virtual void ProcessQueryResultChangedNotification()
		{
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00030E9A File Offset: 0x0002F09A
		protected void AddFolderRefreshPayload()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "RowNotificationHandler::AddFolderRefreshPayload - called for subscription: {0}", base.SubscriptionId);
			this.notifier.AddFolderRefreshPayload(this.folderId, base.SubscriptionId);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00030ED0 File Offset: 0x0002F0D0
		protected bool ProcessErrorNotification(QueryNotification notification)
		{
			bool flag = false;
			if (notification.ErrorCode != 0)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<int, string>((long)this.GetHashCode(), "[RowNotificationHandler::ProcessErrorNotification]. Error in Notification: {0}. Subscription: {1}", notification.ErrorCode, base.SubscriptionId);
				flag = true;
			}
			else if (notification.EventType == QueryNotificationType.Error)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "[RowNotificationHandler::ProcessErrorNotification] Error in Notification, Type is QueryNotificationType.Error. Subscription: {0}", base.SubscriptionId);
				flag = true;
			}
			else if ((notification.EventType == QueryNotificationType.RowAdded || notification.EventType == QueryNotificationType.RowModified) && notification.Row.Length < this.SubscriptionProperties.Length)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<int, int, string>((long)this.GetHashCode(), "[RowNotificationHandler::ProcessErrorNotification] XSO gave an incomplete notification, expected row length {0}, actual row length {1}. Subscription: {2}", notification.Row.Length, this.SubscriptionProperties.Length, base.SubscriptionId);
				flag = true;
			}
			if (flag)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "[RowNotificationHandler::ProcessErrorNotification] Notification has error, calling AddFolderRefreshPayload and returning true. Subscription: {0}", base.SubscriptionId);
				this.AddFolderRefreshPayload();
				this.notifier.PickupData();
				return true;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandler::ProcessErrorNotification]. Returning false, no error to process. Subscription: {0}", base.SubscriptionId);
			return false;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00030FD8 File Offset: 0x0002F1D8
		internal static SingleRecipientType CreateRecipientFromParticipant(Participant participant)
		{
			return new SingleRecipientType
			{
				Mailbox = new EmailAddressWrapper(),
				Mailbox = 
				{
					Name = participant.DisplayName,
					EmailAddress = participant.EmailAddress,
					RoutingType = participant.RoutingType,
					MailboxType = MailboxHelper.GetMailboxType(participant.Origin, participant.RoutingType).ToString()
				}
			};
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00031050 File Offset: 0x0002F250
		protected string GetDateTimeProperty(QueryNotification notification, int index)
		{
			ExDateTime itemProperty = RowNotificationHandler.GetItemProperty<ExDateTime>(notification, index, ExDateTime.MinValue);
			if (ExDateTime.MinValue.Equals(itemProperty))
			{
				return null;
			}
			return ExDateTimeConverter.ToOffsetXsdDateTime(itemProperty, this.timeZone);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00031088 File Offset: 0x0002F288
		protected string GetEwsId(StoreId storeId)
		{
			if (storeId == null)
			{
				return null;
			}
			return StoreId.StoreIdToEwsId(this.mailboxGuid, storeId);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003109C File Offset: 0x0002F29C
		private void CreateSubscriptionForFilteredView()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.CreateSubscriptionForFilteredView Start. SubscriptionId: {0}", base.SubscriptionId);
			OwaSearchContext owaSearchContext = new OwaSearchContext();
			owaSearchContext.ViewFilter = (OwaViewFilter)SearchUtil.GetViewFilterForSearchFolder(this.viewFilter, this.clutterFilter);
			owaSearchContext.SearchFolderId = this.searchFolderId;
			owaSearchContext.FolderIdToSearch = this.folderId;
			owaSearchContext.RequestTimeZone = this.timeZone;
			owaSearchContext.FromFilter = this.fromFilter;
			StoreObjectId defaultFolderId = base.UserContext.MailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders);
			using (SearchFolder owaViewFilterSearchFolder = SearchUtil.GetOwaViewFilterSearchFolder(owaSearchContext, base.UserContext.MailboxSession, defaultFolderId, this.sortBy, CallContext.Current))
			{
				if (owaViewFilterSearchFolder == null)
				{
					throw new ArgumentNullException(string.Format("RowNotificationHandler::CreateSubscriptionForFilteredView - null searchFolder returned for subscriptionId: {0}. ViewFilter: {1}; Source folder id: {2}", base.SubscriptionId, this.viewFilter, this.SubscriptionParameters.FolderId.ToString()));
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, ViewFilter, string>((long)this.GetHashCode(), "RowNotificationHandler.CreateSubscriptionForFilteredView create filtered view subscriptionId: {0} . ViewFilter: {1}; Source folder id: {2}", base.SubscriptionId, this.viewFilter, this.SubscriptionParameters.FolderId);
				this.searchFolderId = owaSearchContext.SearchFolderId;
				this.CreateSubscription(owaViewFilterSearchFolder);
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000311D4 File Offset: 0x0002F3D4
		private void CreateSubscription(Folder subscriptionfolder)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.CreateSubscription Start for subscription {0}", base.SubscriptionId);
			base.QueryResult = this.GetQueryResult(subscriptionfolder);
			base.QueryResult.GetRows(base.QueryResult.EstimatedRowCount);
			base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RowNotificationHandler.CreateSubscription End for subscription {0}", base.SubscriptionId);
		}

		// Token: 0x040007EE RID: 2030
		private readonly Guid mailboxGuid;

		// Token: 0x040007EF RID: 2031
		private readonly StoreObjectId folderId;

		// Token: 0x040007F0 RID: 2032
		private StoreId searchFolderId;

		// Token: 0x040007F1 RID: 2033
		private ViewFilter viewFilter;

		// Token: 0x040007F2 RID: 2034
		private ClutterFilter clutterFilter;

		// Token: 0x040007F3 RID: 2035
		private string fromFilter;

		// Token: 0x040007F4 RID: 2036
		private SortBy[] sortBy;

		// Token: 0x040007F5 RID: 2037
		private RowNotifier notifier;

		// Token: 0x040007F6 RID: 2038
		private ExDateTime creationTime = ExDateTime.Now;

		// Token: 0x040007F7 RID: 2039
		private ExTimeZone timeZone;

		// Token: 0x040007F8 RID: 2040
		private int refcount;

		// Token: 0x040007F9 RID: 2041
		private readonly StoreObjectId originalFolderId;

		// Token: 0x040007FA RID: 2042
		private readonly string originalFolderIdAsString;
	}
}
