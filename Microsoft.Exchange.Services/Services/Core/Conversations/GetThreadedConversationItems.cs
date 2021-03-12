using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders;
using Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations
{
	// Token: 0x0200039E RID: 926
	internal class GetThreadedConversationItems : GetConversationItemsBase<IThreadedConversation, GetThreadedConversationItemsRequest, ThreadedConversationResponseType>
	{
		// Token: 0x06001A0A RID: 6666 RVA: 0x000963AC File Offset: 0x000945AC
		public GetThreadedConversationItems(CallContext callContext, GetThreadedConversationItemsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x000963B6 File Offset: 0x000945B6
		protected override int MaxItemsToReturn
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000963BC File Offset: 0x000945BC
		protected override IExchangeWebMethodResponse GetResponseInternal()
		{
			GetThreadedConversationItemsResponse getThreadedConversationItemsResponse = new GetThreadedConversationItemsResponse();
			getThreadedConversationItemsResponse.BuildForResults<ThreadedConversationResponseType>(this.InternalResults);
			return getThreadedConversationItemsResponse;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000963DC File Offset: 0x000945DC
		protected override ICoreConversationFactory<IThreadedConversation> CreateConversationFactory(IMailboxSession mailboxSession)
		{
			return new ThreadedConversationFactory(mailboxSession);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000963E4 File Offset: 0x000945E4
		protected override ConversationNodeLoadingListBuilderBase CreateConversationNodeLoadingListBuilder(IThreadedConversation conversation, ConversationRequestArguments requestArguments, List<IConversationTreeNode> nonSyncedNodes)
		{
			return new ThreadedConversationNodeLoadingListBuilder(nonSyncedNodes, requestArguments);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000963ED File Offset: 0x000945ED
		protected override ConversationResponseBuilderBase<ThreadedConversationResponseType> CreateBuilder(IMailboxSession mailboxSession, IThreadedConversation conversation, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments, ModernConversationNodeFactory conversationNodeFactory)
		{
			return new ThreadedConversationResponseBuilder(mailboxSession, conversation, conversationNodeFactory, base.ParticipantResolver, loadingList, requestArguments);
		}

		// Token: 0x0400114C RID: 4428
		private const int MaxItemsToReturnAcrossThreads = 100;
	}
}
