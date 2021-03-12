using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B3 RID: 947
	internal class ConversationNodeProcessorFactory : IConversationNodeProcessorFactory
	{
		// Token: 0x06001AA0 RID: 6816 RVA: 0x00098515 File Offset: 0x00096715
		public ConversationNodeProcessorFactory(IConversationData conversationData, IModernConversationNodeFactory conversationNodeFactory, IParticipantResolver resolver, Dictionary<IConversationTreeNode, ConversationNode> serviceNodesMap)
		{
			this.conversationData = conversationData;
			this.conversationNodeFactory = conversationNodeFactory;
			this.serviceNodesMap = serviceNodesMap;
			this.participantResolver = resolver;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0009853A File Offset: 0x0009673A
		private IBreadcrumbsSource BreadcrumbsSource
		{
			get
			{
				return this.conversationData as IBreadcrumbsSource;
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00098548 File Offset: 0x00096748
		public IEnumerable<IConversationNodeProcessor> CreateNormalNodeProcessors()
		{
			List<IConversationNodeProcessor> list = new List<IConversationNodeProcessor>();
			list.Add(new PopulateNewParticipantsProperty(this.conversationData.LoadAddedParticipants(), this.participantResolver));
			if (this.BreadcrumbsSource != null)
			{
				list.Add(new PopulateForwardConversations(this.BreadcrumbsSource.ForwardConversationRootMessagePropertyBags, this.conversationNodeFactory));
			}
			list.Add(new PopulateInReplyTo(this.conversationNodeFactory, this.conversationData.BuildPreviousNodeGraph(), this.serviceNodesMap));
			return list;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000985C0 File Offset: 0x000967C0
		public IEnumerable<IConversationNodeProcessor> CreateRootNodeProcessors()
		{
			List<IConversationNodeProcessor> list = new List<IConversationNodeProcessor>();
			list.Add(new PopulateRootNode());
			if (this.BreadcrumbsSource != null)
			{
				list.Add(new PopulateBackwardConversation(this.BreadcrumbsSource.BackwardMessagePropertyBag, this.conversationNodeFactory));
			}
			return list;
		}

		// Token: 0x04001189 RID: 4489
		private readonly IConversationData conversationData;

		// Token: 0x0400118A RID: 4490
		private readonly IModernConversationNodeFactory conversationNodeFactory;

		// Token: 0x0400118B RID: 4491
		private readonly IParticipantResolver participantResolver;

		// Token: 0x0400118C RID: 4492
		private readonly Dictionary<IConversationTreeNode, ConversationNode> serviceNodesMap;
	}
}
