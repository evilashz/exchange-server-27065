using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000380 RID: 896
	internal sealed class SubscribeToCalendarChanges : SingleStepServiceCommand<SubscribeToCalendarChangesRequest, SubscribeToCalendarChangesResponseMessage>
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x0008A4E1 File Offset: 0x000886E1
		public SubscribeToCalendarChanges(CallContext callContext, SubscribeToCalendarChangesRequest request, Action<CalendarChangeNotificationType> callback) : base(callContext, request)
		{
			this.callback = callback;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0008A4FD File Offset: 0x000886FD
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.responseMessage.Initialize(base.Result.Code, base.Result.Error);
			return this.responseMessage;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0008A618 File Offset: 0x00088818
		internal override ServiceResult<SubscribeToCalendarChangesResponseMessage> Execute()
		{
			ExchangeServiceNotificationHandler handler = ExchangeServiceNotificationHandler.GetHandler(base.CallContext);
			if (base.Request.ParentFolderId == null)
			{
				handler.RemoveSubscription(base.Request.SubscriptionId);
			}
			else
			{
				handler.AddSubscription(base.CallContext.MailboxIdentityPrincipal, base.CallContext, base.Request.SubscriptionId, delegate
				{
					ExchangeServiceCalendarSubscription exchangeServiceCalendarSubscription = new ExchangeServiceCalendarSubscription(this.Request.SubscriptionId);
					StoreObjectId folderId = IdConverter.EwsIdToStoreObjectIdGivenStoreObjectType(this.Request.ParentFolderId.BaseFolderId.GetId(), StoreObjectType.Folder);
					using (Folder folder = Folder.Bind(handler.Session, folderId))
					{
						exchangeServiceCalendarSubscription.QueryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
						{
							ItemSchema.Id
						});
						exchangeServiceCalendarSubscription.QueryResult.GetRows(1);
						exchangeServiceCalendarSubscription.Callback = this.callback;
						exchangeServiceCalendarSubscription.Subscription = Subscription.Create(exchangeServiceCalendarSubscription.QueryResult, new NotificationHandler(exchangeServiceCalendarSubscription.HandleNotification));
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>(0L, "SubscribeToCalendarChanges.Execute: Adding subscription for {0}.", exchangeServiceCalendarSubscription.SubscriptionId);
					}
					return exchangeServiceCalendarSubscription;
				});
			}
			return new ServiceResult<SubscribeToCalendarChangesResponseMessage>(this.responseMessage);
		}

		// Token: 0x040010B4 RID: 4276
		private SubscribeToCalendarChangesResponseMessage responseMessage = new SubscribeToCalendarChangesResponseMessage();

		// Token: 0x040010B5 RID: 4277
		private readonly Action<CalendarChangeNotificationType> callback;
	}
}
