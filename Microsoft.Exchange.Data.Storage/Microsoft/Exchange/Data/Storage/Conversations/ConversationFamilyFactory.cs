using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D1 RID: 2257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationFamilyFactory : ICoreConversationFactory<ConversationFamily>
	{
		// Token: 0x06005409 RID: 21513 RVA: 0x0015D0BC File Offset: 0x0015B2BC
		public ConversationFamilyFactory(IMailboxSession mailboxSession, ConversationId conversationFamilyId)
		{
			Util.ThrowOnNullArgument(mailboxSession, "session");
			this.mailboxSession = mailboxSession;
			XSOFactory @default = XSOFactory.Default;
			this.conversationFamilyId = conversationFamilyId;
			this.membersQuery = new ConversationFamilyMembersQuery(@default, this.MailboxSession);
			this.conversationFamilyTreeFactory = new ConversationTreeFactory(this.MailboxSession, ConversationTreeNodeFactory.ConversationFamilyTreeNodeIndexPropertyDefinition);
			this.selectedConversationTreeFactory = new ConversationTreeFactory(this.MailboxSession);
			this.dataExtractorFactory = new ConversationDataExtractorFactory(@default, this.MailboxSession);
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x0015D139 File Offset: 0x0015B339
		protected IConversationTreeFactory SelectedConversationTreeFactory
		{
			get
			{
				return this.selectedConversationTreeFactory;
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x0600540B RID: 21515 RVA: 0x0015D141 File Offset: 0x0015B341
		protected ConversationId ConversationFamilyId
		{
			get
			{
				return this.conversationFamilyId;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x0015D149 File Offset: 0x0015B349
		protected IMailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0015D151 File Offset: 0x0015B351
		public ConversationFamily CreateConversation(ConversationId conversationId, params PropertyDefinition[] requestedProperties)
		{
			return this.CreateConversation(conversationId, null, false, false, requestedProperties);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0015D15E File Offset: 0x0015B35E
		public ConversationFamily CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] requestedProperties)
		{
			return this.CreateConversation(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, false, null, requestedProperties);
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0015D170 File Offset: 0x0015B370
		public ConversationFamily CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, bool isSmimeSupported, string domainName, params PropertyDefinition[] requestedProperties)
		{
			Util.ThrowOnNullArgument(conversationId, "conversationId");
			HashSet<PropertyDefinition> hashSet = this.conversationFamilyTreeFactory.CalculatePropertyDefinitionsToBeLoaded(requestedProperties);
			List<IStorePropertyBag> list = this.membersQuery.Query(this.ConversationFamilyId, hashSet, folderIds, useFolderIdsAsExclusionList);
			Dictionary<object, List<IStorePropertyBag>> dictionary = this.membersQuery.AggregateMembersPerField(ItemSchema.ConversationId, this.ConversationFamilyId, list);
			List<IStorePropertyBag> list2;
			if (!dictionary.TryGetValue(conversationId, out list2))
			{
				list2 = new List<IStorePropertyBag>();
				dictionary.Add(conversationId, list2);
			}
			IConversationTree conversationTree = this.SelectedConversationTreeFactory.Create(list2, hashSet);
			ConversationDataExtractor dataExtractor = this.dataExtractorFactory.Create(isIrmEnabled, hashSet, conversationId, conversationTree, isSmimeSupported, domainName);
			if (dictionary.Count > 1)
			{
				IConversationTree conversationFamilyTree = this.conversationFamilyTreeFactory.Create(list, hashSet);
				return this.InternalCreateConversationFamilyWithSeveralConversations(dataExtractor, conversationFamilyTree, conversationId, conversationTree);
			}
			return this.InternalCreateConversationFamilyWithSingleConversation(dataExtractor, conversationTree);
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0015D232 File Offset: 0x0015B432
		protected virtual ConversationFamily InternalCreateConversationFamilyWithSeveralConversations(ConversationDataExtractor dataExtractor, IConversationTree conversationFamilyTree, ConversationId selectedConversationId, IConversationTree selectedConversationTree)
		{
			return new ConversationFamily(this.MailboxSession, dataExtractor, this.ConversationFamilyId, conversationFamilyTree, selectedConversationId, selectedConversationTree, this.SelectedConversationTreeFactory);
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0015D250 File Offset: 0x0015B450
		protected virtual ConversationFamily InternalCreateConversationFamilyWithSingleConversation(ConversationDataExtractor dataExtractor, IConversationTree conversationTree)
		{
			return new ConversationFamily(this.MailboxSession, dataExtractor, this.ConversationFamilyId, conversationTree, this.SelectedConversationTreeFactory);
		}

		// Token: 0x04002D76 RID: 11638
		private readonly ConversationFamilyMembersQuery membersQuery;

		// Token: 0x04002D77 RID: 11639
		private readonly ConversationTreeFactory conversationFamilyTreeFactory;

		// Token: 0x04002D78 RID: 11640
		private readonly IConversationTreeFactory selectedConversationTreeFactory;

		// Token: 0x04002D79 RID: 11641
		private readonly ConversationId conversationFamilyId;

		// Token: 0x04002D7A RID: 11642
		private readonly IMailboxSession mailboxSession;

		// Token: 0x04002D7B RID: 11643
		private readonly IConversationDataExtractorFactory dataExtractorFactory;
	}
}
