using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000878 RID: 2168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationFactory : ICoreConversationFactory<Conversation>
	{
		// Token: 0x060051A7 RID: 20903 RVA: 0x00154CF1 File Offset: 0x00152EF1
		public ConversationFactory(IMailboxSession session) : this(session, new ConversationTreeFactory(session), new ConversationMembersQuery(XSOFactory.Default, session), new ConversationDataExtractorFactory(XSOFactory.Default, session))
		{
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00154D16 File Offset: 0x00152F16
		protected ConversationFactory(IMailboxSession session, IConversationTreeFactory treeFactory, IConversationMembersQuery membersQuery, IConversationDataExtractorFactory dataExtractorFactory)
		{
			this.session = session;
			this.membersQuery = membersQuery;
			this.treeFactory = treeFactory;
			this.dataExtractorFactory = dataExtractorFactory;
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x00154D3B File Offset: 0x00152F3B
		protected IConversationTreeFactory TreeFactory
		{
			get
			{
				return this.treeFactory;
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x00154D43 File Offset: 0x00152F43
		protected IMailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x00154D4B File Offset: 0x00152F4B
		protected IConversationMembersQuery MembersQuery
		{
			get
			{
				return this.membersQuery;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x00154D53 File Offset: 0x00152F53
		protected IConversationDataExtractorFactory DataExtractorFactory
		{
			get
			{
				return this.dataExtractorFactory;
			}
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x00154D5B File Offset: 0x00152F5B
		public Conversation CreateConversation(ConversationId conversationId, params PropertyDefinition[] requestedProperties)
		{
			return this.CreateConversation(conversationId, null, false, false, requestedProperties);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00154D68 File Offset: 0x00152F68
		public virtual Conversation CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] requestedProperties)
		{
			return this.CreateConversation(conversationId, folderIds, useFolderIdsAsExclusionList, isIrmEnabled, false, null, requestedProperties);
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00154D7C File Offset: 0x00152F7C
		public virtual Conversation CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, bool isSmimeSupported, string domainName, params PropertyDefinition[] requestedProperties)
		{
			HashSet<PropertyDefinition> hashSet = this.SanitizePropertiesRequested(requestedProperties);
			List<IStorePropertyBag> queryResult = this.MembersQuery.Query(conversationId, hashSet, folderIds, useFolderIdsAsExclusionList);
			IConversationTree conversationTree = this.TreeFactory.Create(queryResult, hashSet);
			ConversationStateFactory stateFactory = new ConversationStateFactory(this.Session, conversationTree);
			ConversationDataExtractor conversationDataExtractor = this.DataExtractorFactory.Create(isIrmEnabled, hashSet, conversationId, conversationTree, isSmimeSupported, domainName);
			return this.InternalCreateConversation(conversationId, conversationDataExtractor, stateFactory, conversationTree);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00154DDE File Offset: 0x00152FDE
		protected virtual Conversation InternalCreateConversation(ConversationId conversationId, ConversationDataExtractor conversationDataExtractor, ConversationStateFactory stateFactory, IConversationTree conversationTree)
		{
			return new Conversation(conversationId, conversationTree, this.Session as MailboxSession, conversationDataExtractor, new ConversationTreeFactory(this.Session), stateFactory);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00154E00 File Offset: 0x00153000
		private HashSet<PropertyDefinition> SanitizePropertiesRequested(PropertyDefinition[] requestedProperties)
		{
			return this.TreeFactory.CalculatePropertyDefinitionsToBeLoaded(requestedProperties);
		}

		// Token: 0x04002C69 RID: 11369
		private readonly IMailboxSession session;

		// Token: 0x04002C6A RID: 11370
		private readonly IConversationMembersQuery membersQuery;

		// Token: 0x04002C6B RID: 11371
		private readonly IConversationTreeFactory treeFactory;

		// Token: 0x04002C6C RID: 11372
		private readonly IConversationDataExtractorFactory dataExtractorFactory;
	}
}
