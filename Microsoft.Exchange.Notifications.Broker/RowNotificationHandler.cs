using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000010 RID: 16
	internal abstract class RowNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004EFE File Offset: 0x000030FE
		protected RowNotificationHandler(string name, MailboxSessionContext sessionContext, BaseSubscription parameters) : base(name, sessionContext)
		{
			ArgumentValidator.ThrowIfTypeInvalid<RowSubscription>("parameters", parameters);
			this.SetSubscriptionParameter((RowSubscription)parameters);
			this.MailboxGuid = base.SessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004F3A File Offset: 0x0000313A
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004F42 File Offset: 0x00003142
		private protected StoreObjectId FolderId { protected get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004F4B File Offset: 0x0000314B
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004F53 File Offset: 0x00003153
		private protected StoreId SearchFolderId { protected get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009B RID: 155
		protected abstract PropertyDefinition[] SubscriptionProperties { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004F5C File Offset: 0x0000315C
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004F64 File Offset: 0x00003164
		private protected Guid MailboxGuid { protected get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004F6D File Offset: 0x0000316D
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004F75 File Offset: 0x00003175
		private protected SortBy[] SortBy { protected get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004F7E File Offset: 0x0000317E
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004F86 File Offset: 0x00003186
		private protected ViewFilter ViewFilter { protected get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004F8F File Offset: 0x0000318F
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004F97 File Offset: 0x00003197
		private protected ClutterFilter ClutterFilter { protected get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004FA0 File Offset: 0x000031A0
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004FA8 File Offset: 0x000031A8
		private protected string FromFilter { protected get; private set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x00004FB4 File Offset: 0x000031B4
		internal static SingleRecipientType CreateRecipientFromParticipant(Participant participant)
		{
			return new SingleRecipientType
			{
				Mailbox = new EmailAddressWrapper
				{
					Name = participant.DisplayName,
					EmailAddress = participant.EmailAddress,
					RoutingType = participant.RoutingType,
					MailboxType = MailboxHelper.GetMailboxType(participant.Origin, participant.RoutingType).ToString()
				}
			};
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000501C File Offset: 0x0000321C
		internal override void HandleNotificationInternal(Notification notification, object context)
		{
			QueryNotification queryNotification = (QueryNotification)notification;
			if (notification == null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler - {0} : Received a null Row Notification object", base.Name);
				BrokerLogger.Set(LogField.RejectReason, "NullNotification");
				return;
			}
			RowNotification notificationPayload = this.GetNotificationPayload(queryNotification);
			if (notificationPayload != null)
			{
				base.SendPayloadsToQueue(notificationPayload);
				return;
			}
			BrokerLogger.Set(LogField.RejectReason, "NoPayload");
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000507C File Offset: 0x0000327C
		internal override void KeepAliveInternal()
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.NeedRefreshPayload)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler {0}. NeedRefreshPayload.", base.Name);
					this.GetRefreshPayload();
					base.NeedRefreshPayload = false;
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000050F0 File Offset: 0x000032F0
		protected static T GetItemProperty<T>(QueryNotification notification, int index)
		{
			return RowNotificationHandler.GetItemProperty<T>(notification, index, default(T));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005110 File Offset: 0x00003310
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

		// Token: 0x060000AB RID: 171 RVA: 0x00005141 File Offset: 0x00003341
		protected static bool IsPropertyDefined(QueryNotification notification, int index)
		{
			return index >= 0 && index < notification.Row.Length && notification.Row[index] != null && !(notification.Row[index] is PropertyError);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005174 File Offset: 0x00003374
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

		// Token: 0x060000AD RID: 173
		protected abstract RowNotification GetPayloadFromNotification(StoreObjectId folderId, QueryNotification notification);

		// Token: 0x060000AE RID: 174 RVA: 0x00005224 File Offset: 0x00003424
		protected void GetPartialPayloadFromNotification(RowNotification payload, QueryNotification notification)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. GetPartialPayloadFromNotification.", base.Name);
			payload.EventType = notification.EventType;
			payload.Prior = Convert.ToBase64String(notification.Prior);
			payload.FolderId = this.GetEwsId(this.FolderId);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000527C File Offset: 0x0000347C
		protected override void InitSubscriptionInternal(MailboxSession session)
		{
			if (!base.SessionContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("SessionContext lock should be acquired before calling this method RowNotificationHandler.InitSubscriptionInternal");
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Creating XSO Subscription");
			if (!SearchUtil.IsViewFilterNonQuery(this.ViewFilter) || this.ClutterFilter != ClutterFilter.All)
			{
				this.CreateSubscriptionForFilteredView(session);
				return;
			}
			using (Folder folder = Folder.Bind(session, this.FolderId))
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. InitSubscriptionInternal. Create folder", base.Name);
				this.CreateSubscription(folder);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005320 File Offset: 0x00003520
		protected virtual QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. GetQueryResult.", base.Name);
			QueryFilter queryFilter = null;
			SortBy[] sortColumns = this.SortBy;
			if (!string.IsNullOrEmpty(this.FromFilter))
			{
				queryFilter = PeopleIKnowQuery.GetItemQueryFilter(this.FromFilter);
				sortColumns = PeopleIKnowQuery.GetItemQuerySortBy(this.SortBy);
			}
			return folder.ItemQuery(ItemQueryType.None, queryFilter, sortColumns, this.SubscriptionProperties);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005386 File Offset: 0x00003586
		protected virtual RowNotification ProcessReloadNotification()
		{
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005389 File Offset: 0x00003589
		protected virtual RowNotification ProcessQueryResultChangedNotification()
		{
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000538C File Offset: 0x0000358C
		protected RowNotification GetRefreshPayload()
		{
			ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Handler : {0}. GetRefreshPayload.", base.Name);
			RowNotification rowNotification = this.CreateBlankPayload();
			rowNotification.FolderId = this.GetEwsId(this.FolderId);
			rowNotification.EventType = QueryNotificationType.Reload;
			return rowNotification;
		}

		// Token: 0x060000B4 RID: 180
		protected abstract RowNotification CreateBlankPayload();

		// Token: 0x060000B5 RID: 181 RVA: 0x000053D8 File Offset: 0x000035D8
		protected bool IsInvalidNotification(QueryNotification notification)
		{
			if (notification.ErrorCode != 0)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, int>((long)this.GetHashCode(), "Handler : {0}. Error in Notification: {1}.", base.Name, notification.ErrorCode);
				BrokerLogger.Set(LogField.RejectReason, string.Format("ErrorCode", notification.ErrorCode));
				return true;
			}
			if (notification.EventType == QueryNotificationType.Error)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Handler : {0}. Error in Notification, Type is QueryNotificationType.Error.", base.Name);
				BrokerLogger.Set(LogField.RejectReason, "QueryNotificationType.Error");
				return true;
			}
			if ((notification.EventType == QueryNotificationType.RowAdded || notification.EventType == QueryNotificationType.RowModified) && notification.Row.Length < this.SubscriptionProperties.Length)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, int, int>((long)this.GetHashCode(), "Handler : {0}. XSO gave an incomplete notification, expected row length {1}, actual row length {2}.", base.Name, notification.Row.Length, this.SubscriptionProperties.Length);
				BrokerLogger.Set(LogField.RejectReason, "IncompleteNotification");
				return true;
			}
			return false;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000054BC File Offset: 0x000036BC
		protected string GetDateTimeProperty(QueryNotification notification, int index)
		{
			ExDateTime itemProperty = RowNotificationHandler.GetItemProperty<ExDateTime>(notification, index, ExDateTime.MinValue);
			if (ExDateTime.MinValue.Equals(itemProperty))
			{
				return null;
			}
			return ExDateTimeConverter.ToUtcXsdDateTime(itemProperty);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000054EE File Offset: 0x000036EE
		protected string GetEwsId(StoreId storeId)
		{
			if (storeId == null)
			{
				return null;
			}
			return StoreId.StoreIdToEwsId(this.MailboxGuid, storeId);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005504 File Offset: 0x00003704
		private RowNotification GetNotificationPayload(QueryNotification queryNotification)
		{
			RowNotification result = null;
			if (this.IsInvalidNotification(queryNotification))
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Handler - {0} : Invalid notification. Generating Refresh Payload", base.Name);
				result = this.GetRefreshPayload();
			}
			else
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Got a valid Notification to process.");
				QueryNotificationType eventType = queryNotification.EventType;
				switch (eventType)
				{
				case QueryNotificationType.QueryResultChanged:
					ExTraceGlobals.ServiceTracer.TraceDebug<string, QueryNotificationType>((long)this.GetHashCode(), "Handler - {0} : EventType {1}. Calling ProcessQueryResultChangedNotification.", base.Name, eventType);
					return this.ProcessQueryResultChangedNotification();
				case QueryNotificationType.RowAdded:
				case QueryNotificationType.RowDeleted:
				case QueryNotificationType.RowModified:
					ExTraceGlobals.ServiceTracer.TraceDebug<string, QueryNotificationType>((long)this.GetHashCode(), "Handler - {0} : EventType {1}.", base.Name, eventType);
					return this.GetPayloadFromNotification(this.FolderId, queryNotification);
				case QueryNotificationType.Reload:
					ExTraceGlobals.ServiceTracer.TraceDebug<string, QueryNotificationType>((long)this.GetHashCode(), "Handler - {0} : EventType {1}. Calling ProcessReloadNotification.", base.Name, eventType);
					return this.ProcessReloadNotification();
				}
				ExTraceGlobals.ServiceTracer.TraceDebug<string, QueryNotificationType>((long)this.GetHashCode(), "Handler - {0} : EventType {0}. Ignoring.", base.Name, eventType);
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005628 File Offset: 0x00003828
		private void SetSubscriptionParameter(RowSubscription parameters)
		{
			this.FolderId = StoreId.EwsIdToStoreObjectId(parameters.FolderId);
			if (parameters.SortBy != null)
			{
				this.SortBy = SortResults.ToXsoSortBy(parameters.SortBy);
			}
			if (parameters.Filter != ViewFilter.All)
			{
				this.ViewFilter = parameters.Filter;
			}
			if (parameters.ClutterFilter != ClutterFilter.All)
			{
				this.ClutterFilter = parameters.ClutterFilter;
			}
			if (!string.IsNullOrEmpty(parameters.FromFilter))
			{
				this.FromFilter = parameters.FromFilter;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000056A0 File Offset: 0x000038A0
		private void CreateSubscriptionForFilteredView(MailboxSession session)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. CreateSubscriptionForFilteredView", base.Name);
			OwaSearchContext owaSearchContext = new OwaSearchContext();
			owaSearchContext.ViewFilter = (OwaViewFilter)SearchUtil.GetViewFilterForSearchFolder(this.ViewFilter, this.ClutterFilter);
			owaSearchContext.SearchFolderId = this.SearchFolderId;
			owaSearchContext.FolderIdToSearch = this.FolderId;
			owaSearchContext.RequestTimeZone = ExTimeZone.UtcTimeZone;
			owaSearchContext.FromFilter = this.FromFilter;
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.SearchFolders);
			using (SearchFolder owaViewFilterSearchFolder = SearchUtil.GetOwaViewFilterSearchFolder(owaSearchContext, session, defaultFolderId, this.SortBy, CallContext.Current))
			{
				if (owaViewFilterSearchFolder == null)
				{
					throw new ArgumentNullException(string.Format("Handler : {0}. CreateSubscriptionForFilteredView - null searchFolder. ViewFilter: {1}; Source folder id: {2}", base.Name, this.ViewFilter, this.GetEwsId(this.FolderId)));
				}
				ExTraceGlobals.ServiceTracer.TraceDebug<string, ViewFilter, string>((long)this.GetHashCode(), "Handler : {0}. CreateSubscriptionForFilteredView. Create filtered view. ViewFilter: {1}; Source folder id: {2}", base.Name, this.ViewFilter, this.GetEwsId(this.FolderId));
				this.SearchFolderId = owaSearchContext.SearchFolderId;
				this.CreateSubscription(owaViewFilterSearchFolder);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000057C0 File Offset: 0x000039C0
		private void CreateSubscription(Folder subscriptionfolder)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. CreateSubscription", base.Name);
			base.QueryResult = this.GetQueryResult(subscriptionfolder);
			base.QueryResult.GetRows(base.QueryResult.EstimatedRowCount);
			base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
		}
	}
}
