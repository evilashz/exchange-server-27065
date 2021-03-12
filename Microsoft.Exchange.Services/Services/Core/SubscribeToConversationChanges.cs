using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000381 RID: 897
	internal sealed class SubscribeToConversationChanges : SingleStepServiceCommand<SubscribeToConversationChangesRequest, SubscribeToConversationChangesResponseMessage>
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x0008A6AD File Offset: 0x000888AD
		public SubscribeToConversationChanges(CallContext callContext, SubscribeToConversationChangesRequest request, Action<ConversationNotification> callback) : base(callContext, request)
		{
			this.callback = callback;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0008A6C9 File Offset: 0x000888C9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.responseMessage.Initialize(base.Result.Code, base.Result.Error);
			return this.responseMessage;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0008A8C0 File Offset: 0x00088AC0
		internal override ServiceResult<SubscribeToConversationChangesResponseMessage> Execute()
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
						ExchangeServiceConversationSubscription exchangeServiceConversationSubscription = new ExchangeServiceConversationSubscription(this.Request.SubscriptionId);
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
							ConversationResponseShape conversationResponseShape = this.Request.ConversationShape;
							if (conversationResponseShape == null)
							{
								conversationResponseShape = new ConversationResponseShape(ShapeEnum.Default, null);
							}
							if (unifiedView != null)
							{
								UnifiedView.UpdateConversationResponseShape(conversationResponseShape);
							}
							PropertyListForViewRowDeterminer propertyListForViewRowDeterminer = PropertyListForViewRowDeterminer.BuildForConversation(conversationResponseShape);
							exchangeServiceConversationSubscription.PropertyList = propertyListForViewRowDeterminer.GetPropertiesToFetch();
							exchangeServiceConversationSubscription.QueryResult = folder.ConversationItemQuery(null, sortColumns, exchangeServiceConversationSubscription.PropertyList);
							exchangeServiceConversationSubscription.QueryResult.GetRows(1);
							exchangeServiceConversationSubscription.MailboxGuid = handler.Session.MailboxGuid;
							exchangeServiceConversationSubscription.Callback = this.callback;
							exchangeServiceConversationSubscription.Subscription = Subscription.Create(exchangeServiceConversationSubscription.QueryResult, new NotificationHandler(exchangeServiceConversationSubscription.HandleNotification));
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>(0L, "SubscribeToConversationChanges.Execute: Adding subscription for {0}.", exchangeServiceConversationSubscription.SubscriptionId);
						}
						return exchangeServiceConversationSubscription;
					});
				}
			}
			return new ServiceResult<SubscribeToConversationChangesResponseMessage>(this.responseMessage);
		}

		// Token: 0x040010B6 RID: 4278
		private SubscribeToConversationChangesResponseMessage responseMessage = new SubscribeToConversationChangesResponseMessage();

		// Token: 0x040010B7 RID: 4279
		private Action<ConversationNotification> callback;
	}
}
