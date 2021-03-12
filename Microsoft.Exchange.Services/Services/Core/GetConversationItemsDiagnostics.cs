using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Conversations;
using Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders;
using Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000304 RID: 772
	internal class GetConversationItemsDiagnostics : GetConversationItemsBase<IConversation, GetConversationItemsDiagnosticsRequest, GetConversationItemsDiagnosticsResponseType>
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x00071B8E File Offset: 0x0006FD8E
		public GetConversationItemsDiagnostics(CallContext callContext, GetConversationItemsDiagnosticsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00071B98 File Offset: 0x0006FD98
		protected override PropertyDefinition[] AdditionalRequestedProperties
		{
			get
			{
				return new PropertyDefinition[]
				{
					ItemSchema.InternetReferences,
					ItemSchema.ConversationIndexTrackingEx,
					ItemSchema.InReplyTo
				};
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00071BC5 File Offset: 0x0006FDC5
		protected override int MaxItemsToReturn
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00071BCC File Offset: 0x0006FDCC
		protected override IExchangeWebMethodResponse GetResponseInternal()
		{
			GetConversationItemsDiagnosticsResponse getConversationItemsDiagnosticsResponse = new GetConversationItemsDiagnosticsResponse();
			getConversationItemsDiagnosticsResponse.BuildForResults<GetConversationItemsDiagnosticsResponseType>(this.InternalResults);
			return getConversationItemsDiagnosticsResponse;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00071BEC File Offset: 0x0006FDEC
		protected override ICoreConversationFactory<IConversation> CreateConversationFactory(IMailboxSession mailboxSession)
		{
			return new ConversationFactory((MailboxSession)mailboxSession);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00071BF9 File Offset: 0x0006FDF9
		protected override ConversationNodeLoadingListBuilderBase CreateConversationNodeLoadingListBuilder(IConversation conversation, ConversationRequestArguments requestArguments, List<IConversationTreeNode> nonSyncedNodes)
		{
			return new ConversationNodeDiagnosticsLoadingListBuilder(nonSyncedNodes);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00071C01 File Offset: 0x0006FE01
		protected override ConversationResponseBuilderBase<GetConversationItemsDiagnosticsResponseType> CreateBuilder(IMailboxSession mailboxSession, IConversation conversation, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments, ModernConversationNodeFactory conversationNodeFactory)
		{
			return new ConversationDiagnosticsResponseBuilder(mailboxSession, conversation, base.ParticipantResolver);
		}

		// Token: 0x04000EC5 RID: 3781
		private const int DefaultMaxItemsToReturn = 100;
	}
}
