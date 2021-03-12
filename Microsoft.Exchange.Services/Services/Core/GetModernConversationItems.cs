using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Conversations;
using Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders;
using Microsoft.Exchange.Services.Core.Conversations.Repositories;
using Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000318 RID: 792
	internal class GetModernConversationItems : GetConversationItemsBase<IConversation, GetConversationItemsRequest, ConversationResponseType>
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x000756E6 File Offset: 0x000738E6
		public GetModernConversationItems(CallContext callContext, GetConversationItemsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000756F0 File Offset: 0x000738F0
		protected override int MaxItemsToReturn
		{
			get
			{
				if (base.Request.MaxItemsToReturn == 0)
				{
					return 100;
				}
				return Math.Min(base.Request.MaxItemsToReturn, 100);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00075714 File Offset: 0x00073914
		protected override IExchangeWebMethodResponse GetResponseInternal()
		{
			GetConversationItemsResponse getConversationItemsResponse = new GetConversationItemsResponse();
			getConversationItemsResponse.BuildForResults<ConversationResponseType>(this.InternalResults);
			return getConversationItemsResponse;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00075734 File Offset: 0x00073934
		protected override ICoreConversationFactory<IConversation> CreateConversationFactory(IMailboxSession mailboxSession)
		{
			ICoreConversationFactory<IConversation> result;
			if (base.CurrentConversationRequest.ConversationFamilyId == null)
			{
				result = new ConversationFactory(mailboxSession as MailboxSession);
			}
			else
			{
				ConversationId familyIdFromRequest = this.GetFamilyIdFromRequest();
				result = new ConversationFamilyFactory(mailboxSession, familyIdFromRequest);
			}
			return result;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0007576C File Offset: 0x0007396C
		protected override ConversationNodeLoadingListBuilderBase CreateConversationNodeLoadingListBuilder(IConversation conversation, ConversationRequestArguments requestArguments, List<IConversationTreeNode> nonSyncedNodes)
		{
			return new ConversationNodeLoadingListBuilder(nonSyncedNodes, conversation.FirstNode, requestArguments);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0007577B File Offset: 0x0007397B
		protected override ConversationResponseBuilderBase<ConversationResponseType> CreateBuilder(IMailboxSession mailboxSession, IConversation conversation, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments, ModernConversationNodeFactory conversationNodeFactory)
		{
			return new ConversationResponseBuilder(mailboxSession, conversation, conversationNodeFactory, base.ParticipantResolver, loadingList, requestArguments);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00075790 File Offset: 0x00073990
		protected virtual ConversationId GetFamilyIdFromRequest()
		{
			IdAndSession sessionFromConversationId = XsoConversationRepositoryExtensions.GetSessionFromConversationId(base.IdConverter, base.CurrentConversationRequest.ConversationFamilyId, MailboxSearchLocation.PrimaryOnly);
			return sessionFromConversationId.Id as ConversationId;
		}

		// Token: 0x04000F0F RID: 3855
		private const int DefaultMaxItemsToReturn = 100;
	}
}
