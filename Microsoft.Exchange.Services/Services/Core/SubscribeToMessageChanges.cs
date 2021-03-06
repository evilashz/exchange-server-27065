using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000383 RID: 899
	internal sealed class SubscribeToMessageChanges : SingleStepServiceCommand<SubscribeToMessageChangesRequest, SubscribeToMessageChangesResponseMessage>
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x0008ACCD File Offset: 0x00088ECD
		public SubscribeToMessageChanges(CallContext callContext, SubscribeToMessageChangesRequest request, Action<MessageNotification> callback) : base(callContext, request)
		{
			this.callback = callback;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0008ACE9 File Offset: 0x00088EE9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.responseMessage.Initialize(base.Result.Code, base.Result.Error);
			return this.responseMessage;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0008AED4 File Offset: 0x000890D4
		internal override ServiceResult<SubscribeToMessageChangesResponseMessage> Execute()
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				UnifiedView unifiedView = UnifiedView.Create(ExTraceGlobals.NotificationsCallTracer, base.CallContext, base.Request.MailboxGuids, base.Request.ParentFolderId);
				if (unifiedView != null)
				{
					disposeGuard.Add<UnifiedView>(unifiedView);
				}
				ExchangeServiceNotificationHandler handler = ExchangeServiceNotificationHandler.GetHandler(base.CallContext, unifiedView != null && unifiedView.UnifiedSessionRequired);
				if (base.Request.ParentFolderId == null || (unifiedView != null && !unifiedView.UnifiedViewScopeSpecified))
				{
					handler.RemoveSubscription(base.Request.SubscriptionId);
				}
				else
				{
					ExchangePrincipal exchangePrincipal = base.CallContext.MailboxIdentityPrincipal;
					if (unifiedView == null)
					{
						IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(base.Request.ParentFolderId.BaseFolderId.GetId(), BasicTypes.Folder, null, false);
						Guid guid = Guid.Parse(idHeaderInformation.MailboxId.MailboxGuid);
						if (guid != base.CallContext.MailboxIdentityPrincipal.MailboxInfo.MailboxGuid)
						{
							exchangePrincipal = base.CallContext.MailboxIdentityPrincipal.GetAggregatedExchangePrincipal(guid);
						}
					}
					handler.AddSubscription(exchangePrincipal, base.CallContext, base.Request.SubscriptionId, delegate
					{
						ExchangeServiceMessageSubscription exchangeServiceMessageSubscription = new ExchangeServiceMessageSubscription(this.Request.SubscriptionId);
						IdAndSession idAndSession = (unifiedView == null) ? new IdAndSession(IdConverter.EwsIdToFolderId(this.Request.ParentFolderId.BaseFolderId.GetId()), handler.Session) : unifiedView.CreateIdAndSession(handler.Session);
						using (DisposeGuard disposeGuard2 = default(DisposeGuard))
						{
							Folder folder;
							if (unifiedView != null && unifiedView.SearchFolder != null)
							{
								folder = unifiedView.SearchFolder;
							}
							else
							{
								folder = Folder.Bind(idAndSession.Session, idAndSession.Id);
								disposeGuard2.Add<Folder>(folder);
							}
							SortBy[] sortColumns = SortResults.ToXsoSortBy(this.Request.SortOrder);
							ItemResponseShape itemResponseShape = this.Request.MessageShape;
							if (itemResponseShape == null)
							{
								itemResponseShape = new ItemResponseShape(ShapeEnum.Default, BodyResponseType.Best, false, null);
							}
							PropertyListForViewRowDeterminer propertyListForViewRowDeterminer = PropertyListForViewRowDeterminer.BuildForItems(itemResponseShape, folder);
							exchangeServiceMessageSubscription.PropertyList = propertyListForViewRowDeterminer.GetPropertiesToFetch();
							exchangeServiceMessageSubscription.QueryResult = folder.ItemQuery(ItemQueryType.None, null, sortColumns, exchangeServiceMessageSubscription.PropertyList);
							exchangeServiceMessageSubscription.QueryResult.GetRows(1);
							exchangeServiceMessageSubscription.MailboxGuid = handler.Session.MailboxGuid;
							exchangeServiceMessageSubscription.Callback = this.callback;
							exchangeServiceMessageSubscription.Subscription = Subscription.Create(exchangeServiceMessageSubscription.QueryResult, new NotificationHandler(exchangeServiceMessageSubscription.HandleNotification));
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>(0L, "SubscribeToMessageChanges.Execute: Adding subscription for {0}.", exchangeServiceMessageSubscription.SubscriptionId);
						}
						return exchangeServiceMessageSubscription;
					});
				}
			}
			return new ServiceResult<SubscribeToMessageChangesResponseMessage>(this.responseMessage);
		}

		// Token: 0x040010BB RID: 4283
		private SubscribeToMessageChangesResponseMessage responseMessage = new SubscribeToMessageChangesResponseMessage();

		// Token: 0x040010BC RID: 4284
		private Action<MessageNotification> callback;
	}
}
