using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D8 RID: 2264
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThreadedConversationFactory : ICoreConversationFactory<IThreadedConversation>
	{
		// Token: 0x0600547C RID: 21628 RVA: 0x0015E6F1 File Offset: 0x0015C8F1
		public ThreadedConversationFactory(IMailboxSession mailboxSession) : this(mailboxSession, new ConversationMembersQuery(XSOFactory.Default, mailboxSession), new ConversationTreeFactory(mailboxSession), new ConversationTreeFactory(mailboxSession, ConversationTreeNodeFactory.ConversationFamilyTreeNodeIndexPropertyDefinition), new ConversationDataExtractorFactory(XSOFactory.Default, mailboxSession))
		{
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0015E721 File Offset: 0x0015C921
		public ThreadedConversationFactory(IMailboxSession mailboxSession, IConversationMembersQuery membersQuery, IConversationTreeFactory conversationTreeFactory, IConversationTreeFactory conversationThreadTreeFactory, IConversationDataExtractorFactory dataExtractorFactory)
		{
			this.mailboxSession = mailboxSession;
			this.membersQuery = membersQuery;
			this.conversationTreeFactory = conversationTreeFactory;
			this.conversationThreadTreeFactory = conversationThreadTreeFactory;
			this.dataExtractorFactory = dataExtractorFactory;
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0015E74E File Offset: 0x0015C94E
		public IThreadedConversation CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] requestedItemProperties)
		{
			return this.CreateConversation(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, false, null, requestedItemProperties);
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0015E760 File Offset: 0x0015C960
		public IThreadedConversation CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, bool isSmimeSupported, string domainName, params PropertyDefinition[] requestedItemProperties)
		{
			ICollection<PropertyDefinition> defaultThreadProperties = ThreadedConversationFactory.DefaultThreadProperties;
			HashSet<PropertyDefinition> hashSet = this.SanitizePropertiesRequested(requestedItemProperties, defaultThreadProperties);
			Dictionary<object, List<IStorePropertyBag>> aggregatedMessages = this.QueryMessages(conversationId, folderIds, useFolderIdsAsExclusionList, hashSet);
			IConversationTree conversationTree = this.CreateTree(hashSet, aggregatedMessages);
			ConversationStateFactory stateFactory = this.CreateStateFactory(conversationTree);
			List<IConversationTree> threadTrees = this.CreateThreadTrees(hashSet, aggregatedMessages);
			ConversationDataExtractor conversationDataExtractor = this.dataExtractorFactory.Create(isIrmEnabled, hashSet, conversationId, conversationTree, isSmimeSupported, domainName);
			return this.InternalCreateConversation(conversationId, conversationDataExtractor, stateFactory, conversationTree, threadTrees, defaultThreadProperties);
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0015E7C9 File Offset: 0x0015C9C9
		public IThreadedConversation CreateConversation(ConversationId conversationId, params PropertyDefinition[] requestedProperties)
		{
			return this.CreateConversation(conversationId, null, false, false, requestedProperties);
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0015E7D6 File Offset: 0x0015C9D6
		private ConversationStateFactory CreateStateFactory(IConversationTree conversationTree)
		{
			return new ConversationStateFactory(this.mailboxSession, conversationTree);
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0015E7ED File Offset: 0x0015C9ED
		private IConversationTree CreateTree(ICollection<PropertyDefinition> propertyDefinitions, Dictionary<object, List<IStorePropertyBag>> aggregatedMessages)
		{
			return this.conversationTreeFactory.Create(aggregatedMessages.SelectMany((KeyValuePair<object, List<IStorePropertyBag>> d) => d.Value).ToList<IStorePropertyBag>(), propertyDefinitions);
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x0015E824 File Offset: 0x0015CA24
		private List<IConversationTree> CreateThreadTrees(ICollection<PropertyDefinition> propertyDefinitions, Dictionary<object, List<IStorePropertyBag>> aggregatedMessages)
		{
			List<IConversationTree> list = new List<IConversationTree>();
			foreach (KeyValuePair<object, List<IStorePropertyBag>> keyValuePair in aggregatedMessages)
			{
				IConversationTree item = this.conversationThreadTreeFactory.Create(keyValuePair.Value, propertyDefinitions);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0015E890 File Offset: 0x0015CA90
		private IThreadedConversation InternalCreateConversation(ConversationId conversationId, ConversationDataExtractor conversationDataExtractor, ConversationStateFactory stateFactory, IConversationTree tree, List<IConversationTree> threadTrees, ICollection<PropertyDefinition> requestedThreadProperties)
		{
			IList<IConversationThread> conversationThreads = this.CreateConversationThreads(tree, threadTrees, conversationDataExtractor, requestedThreadProperties);
			return new ThreadedConversation(stateFactory, conversationDataExtractor, conversationId, tree, conversationThreads);
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0015E8B8 File Offset: 0x0015CAB8
		private IList<IConversationThread> CreateConversationThreads(IConversationTree tree, List<IConversationTree> threadTrees, ConversationDataExtractor conversationDataExtractor, ICollection<PropertyDefinition> requestedThreadProperties)
		{
			bool isSingleThreadConversation = threadTrees.Count == 1;
			ConversationThreadDataExtractor threadDataExtractor = new ConversationThreadDataExtractor(requestedThreadProperties, tree, isSingleThreadConversation);
			IList<IConversationThread> list = new List<IConversationThread>();
			foreach (IConversationTree threadTree in threadTrees)
			{
				ConversationThread item = new ConversationThread(conversationDataExtractor, threadDataExtractor, threadTree, this.conversationTreeFactory);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0015E934 File Offset: 0x0015CB34
		private HashSet<PropertyDefinition> SanitizePropertiesRequested(PropertyDefinition[] requestedItemProperties, ICollection<PropertyDefinition> requestedThreadProperties)
		{
			ICollection<PropertyDefinition> requestedProperties = InternalSchema.Combine<PropertyDefinition>(requestedItemProperties, requestedThreadProperties);
			return this.conversationThreadTreeFactory.CalculatePropertyDefinitionsToBeLoaded(requestedProperties);
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0015E958 File Offset: 0x0015CB58
		private Dictionary<object, List<IStorePropertyBag>> QueryMessages(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, ICollection<PropertyDefinition> propertyDefinitions)
		{
			List<IStorePropertyBag> members = this.membersQuery.Query(conversationId, propertyDefinitions, folderIds, useFolderIdsAsExclusionList);
			return this.membersQuery.AggregateMembersPerField(ItemSchema.ConversationFamilyId, null, members);
		}

		// Token: 0x04002D95 RID: 11669
		private static ICollection<PropertyDefinition> DefaultThreadProperties = AggregatedConversationSchema.Instance.AllProperties;

		// Token: 0x04002D96 RID: 11670
		private readonly IMailboxSession mailboxSession;

		// Token: 0x04002D97 RID: 11671
		private readonly IConversationMembersQuery membersQuery;

		// Token: 0x04002D98 RID: 11672
		private readonly IConversationTreeFactory conversationTreeFactory;

		// Token: 0x04002D99 RID: 11673
		private readonly IConversationTreeFactory conversationThreadTreeFactory;

		// Token: 0x04002D9A RID: 11674
		private readonly IConversationDataExtractorFactory dataExtractorFactory;
	}
}
