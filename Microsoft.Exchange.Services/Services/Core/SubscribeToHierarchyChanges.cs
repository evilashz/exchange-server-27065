using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000382 RID: 898
	internal sealed class SubscribeToHierarchyChanges : SingleStepServiceCommand<SubscribeToHierarchyChangesRequest, SubscribeToHierarchyChangesResponseMessage>
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x0008AA74 File Offset: 0x00088C74
		public SubscribeToHierarchyChanges(CallContext callContext, SubscribeToHierarchyChangesRequest request, Action<HierarchyNotification> callback) : base(callContext, request)
		{
			this.callback = callback;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0008AAE9 File Offset: 0x00088CE9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.responseMessage.Initialize(base.Result.Code, base.Result.Error);
			return this.responseMessage;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0008ABE8 File Offset: 0x00088DE8
		internal override ServiceResult<SubscribeToHierarchyChangesResponseMessage> Execute()
		{
			ExchangeServiceNotificationHandler handler = ExchangeServiceNotificationHandler.GetHandler(base.CallContext);
			if (base.Request.MailboxGuid == Guid.Empty)
			{
				handler.RemoveSubscription(base.Request.SubscriptionId);
			}
			else
			{
				ExchangePrincipal exchangePrincipal = (base.Request.MailboxGuid == base.CallContext.MailboxIdentityPrincipal.MailboxInfo.MailboxGuid) ? base.CallContext.MailboxIdentityPrincipal : base.CallContext.MailboxIdentityPrincipal.GetAggregatedExchangePrincipal(base.Request.MailboxGuid);
				handler.AddSubscription(exchangePrincipal, base.CallContext, base.Request.SubscriptionId, delegate
				{
					ExchangeServiceHierarchySubscription exchangeServiceHierarchySubscription = new ExchangeServiceHierarchySubscription(this.Request.SubscriptionId);
					using (Folder folder = Folder.Bind(handler.Session, DefaultFolderType.Configuration))
					{
						exchangeServiceHierarchySubscription.QueryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, this.querySubscriptionProperties);
						exchangeServiceHierarchySubscription.QueryResult.GetRows(exchangeServiceHierarchySubscription.QueryResult.EstimatedRowCount);
						exchangeServiceHierarchySubscription.MailboxGuid = handler.Session.MailboxGuid;
						exchangeServiceHierarchySubscription.Callback = this.callback;
						exchangeServiceHierarchySubscription.Subscription = Subscription.Create(exchangeServiceHierarchySubscription.QueryResult, new NotificationHandler(exchangeServiceHierarchySubscription.HandleNotification));
					}
					return exchangeServiceHierarchySubscription;
				});
			}
			return new ServiceResult<SubscribeToHierarchyChangesResponseMessage>(this.responseMessage);
		}

		// Token: 0x040010B8 RID: 4280
		private SubscribeToHierarchyChangesResponseMessage responseMessage = new SubscribeToHierarchyChangesResponseMessage();

		// Token: 0x040010B9 RID: 4281
		private Action<HierarchyNotification> callback;

		// Token: 0x040010BA RID: 4282
		private PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.UnreadCount,
			StoreObjectSchema.ContainerClass,
			StoreObjectSchema.ParentItemId,
			FolderSchema.IPMFolder,
			FolderSchema.IsHidden
		};
	}
}
