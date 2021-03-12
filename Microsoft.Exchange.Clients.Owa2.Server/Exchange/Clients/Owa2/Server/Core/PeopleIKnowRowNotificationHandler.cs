using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200019F RID: 415
	internal class PeopleIKnowRowNotificationHandler : RowNotificationHandler
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0003A5C8 File Offset: 0x000387C8
		public PeopleIKnowRowNotificationHandler(string subscriptionId, SubscriptionParameters parameters, StoreObjectId folderId, IMailboxContext userContext, Guid mailboxGuid, ExTimeZone timeZone, CultureInfo cultureInfo) : base(subscriptionId, parameters, folderId, userContext, mailboxGuid, timeZone, false)
		{
			this.cultureInfo = cultureInfo;
			OwsLogRegistry.Register("PeopleIKnowRowNotification", typeof(FindPeopleMetadata), new Type[0]);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003A5FC File Offset: 0x000387FC
		protected PeopleIKnowRowNotificationHandler(string subscriptionId, SubscriptionParameters parameters, StoreObjectId folderId, UserContext userContext, Guid mailboxGuid, RowNotifier notifier) : base(subscriptionId, parameters, folderId, userContext, mailboxGuid, notifier, false)
		{
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003A60E File Offset: 0x0003880E
		protected override PropertyDefinition[] SubscriptionProperties
		{
			get
			{
				return BrowsePeopleInMailFolder.SenderAndUnreadProperties;
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003A618 File Offset: 0x00038818
		protected override NotificationPayloadBase GetPayloadFromNotification(StoreObjectId folderId, QueryNotification notification)
		{
			ExTraceGlobals.PeopleIKnowNotificationsTracer.TraceDebug<string, QueryNotificationType>((long)this.GetHashCode(), "[PeopleIKnowRowNotificationHandler.GetPayloadFromNotification] SubscriptionId: {0}, NotificationEventType: {1}", base.SubscriptionId, notification.EventType);
			switch (notification.EventType)
			{
			case QueryNotificationType.RowAdded:
			case QueryNotificationType.RowModified:
				return this.GetRowNotificationPayload(notification);
			case QueryNotificationType.RowDeleted:
				return this.GetQueryResultChangedPayload(QueryNotificationType.QueryResultChanged);
			default:
				ExTraceGlobals.PeopleIKnowNotificationsTracer.TraceError<QueryNotificationType>((long)this.GetHashCode(), "[PeopleIKnowRowNotificationHandler.GetPayloadFromNotification] Unknown event type: {0}.", notification.EventType);
				return this.GetEmptyPayload();
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003A698 File Offset: 0x00038898
		private PeopleIKnowRowNotificationPayload GetRowNotificationPayload(QueryNotification notification)
		{
			PeopleIKnowRowNotificationPayload emptyPayload = this.GetEmptyPayload();
			emptyPayload.EventType = notification.EventType;
			emptyPayload.PersonaEmailAdress = RowNotificationHandler.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(this.SubscriptionProperties, MessageItemSchema.SenderSmtpAddress));
			emptyPayload.PersonaUnreadCount = RowNotificationHandler.GetItemProperty<int>(notification, Array.IndexOf<PropertyDefinition>(this.SubscriptionProperties, FolderSchema.UnreadCount));
			emptyPayload.Source = new MailboxLocation(base.MailboxGuid);
			return emptyPayload;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003A704 File Offset: 0x00038904
		protected override QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.PeopleIKnowNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "[PeopleIKnowRowNotificationHandler.GetQueryResult] SubscriptionId: {0}", base.SubscriptionId);
			SendersInMailFolder sendersInMailFolder = new SendersInMailFolder(folder);
			return sendersInMailFolder.GetQueryResultGroupedBySender(BrowsePeopleInMailFolder.SenderAndUnreadProperties);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003A740 File Offset: 0x00038940
		protected override void ProcessQueryResultChangedNotification()
		{
			ExTraceGlobals.PeopleIKnowNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "[PeopleIKnowRowNotificationHandler.ProcessQueryResultChangedNotification] SubscriptionId: {0}", base.SubscriptionId);
			PeopleIKnowRowNotificationPayload queryResultChangedPayload = this.GetQueryResultChangedPayload(QueryNotificationType.QueryResultChanged);
			base.Notifier.AddFolderContentChangePayload(base.FolderId, queryResultChangedPayload);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003A784 File Offset: 0x00038984
		protected override void ProcessReloadNotification()
		{
			ExTraceGlobals.PeopleIKnowNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "[PeopleIKnowRowNotificationHandler.ProcessReloadNotification] SubscriptionId: {0}", base.SubscriptionId);
			PeopleIKnowRowNotificationPayload emptyPayload = this.GetEmptyPayload();
			emptyPayload.EventType = QueryNotificationType.Reload;
			base.Notifier.AddFolderContentChangePayload(base.FolderId, emptyPayload);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003A7D0 File Offset: 0x000389D0
		private PeopleIKnowRowNotificationPayload GetQueryResultChangedPayload(QueryNotificationType queryNotificationType)
		{
			PeopleIKnowRowNotificationPayload emptyPayload = this.GetEmptyPayload();
			emptyPayload.EventType = queryNotificationType;
			this.SetPayloadOnQueryResultChangedNotification(emptyPayload);
			return emptyPayload;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003A7F4 File Offset: 0x000389F4
		private PeopleIKnowRowNotificationPayload GetEmptyPayload()
		{
			return new PeopleIKnowRowNotificationPayload
			{
				SubscriptionId = base.SubscriptionId,
				Source = new MailboxLocation(base.MailboxGuid)
			};
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003A878 File Offset: 0x00038A78
		protected virtual void SetPayloadOnQueryResultChangedNotification(PeopleIKnowRowNotificationPayload payload)
		{
			SimulatedWebRequestContext.Execute(base.UserContext, "PeopleIKnowRowNotification", delegate(MailboxSession mailboxSession, IRecipientSession adSession, RequestDetailsLogger logger)
			{
				FindPeopleParameters parameters = this.CreateFindPeopleParameters(logger);
				BrowsePeopleInMailFolder browsePeopleInMailFolder = new BrowsePeopleInMailFolder(parameters, mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.FromFavoriteSenders), NullTracer.Instance);
				FindPeopleResult findPeopleResult = browsePeopleInMailFolder.Execute();
				payload.Personas = findPeopleResult.PersonaList;
			});
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003A8B8 File Offset: 0x00038AB8
		private FindPeopleParameters CreateFindPeopleParameters(RequestDetailsLogger logger)
		{
			return new FindPeopleParameters
			{
				Paging = PeopleIKnowRowNotificationHandler.CreateIndexedPageView(0, 20),
				PersonaShape = Persona.DefaultPersonaShape,
				CultureInfo = this.cultureInfo,
				Logger = logger
			};
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003A8FC File Offset: 0x00038AFC
		private static IndexedPageView CreateIndexedPageView(int offset, int maxEntries)
		{
			return new IndexedPageView
			{
				Origin = BasePagingType.PagingOrigin.Beginning,
				Offset = offset,
				MaxRows = maxEntries
			};
		}

		// Token: 0x04000918 RID: 2328
		private const string LoggerEventId = "PeopleIKnowRowNotification";

		// Token: 0x04000919 RID: 2329
		private readonly CultureInfo cultureInfo;
	}
}
