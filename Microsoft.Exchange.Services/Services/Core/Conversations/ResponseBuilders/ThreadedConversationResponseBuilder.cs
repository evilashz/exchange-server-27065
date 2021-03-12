using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders
{
	// Token: 0x020003AD RID: 941
	internal class ThreadedConversationResponseBuilder : ConversationDataResponseBuilderBase<IThreadedConversation, IConversationThread, ThreadedConversationResponseType, ConversationThreadType>
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x00097E22 File Offset: 0x00096022
		public ThreadedConversationResponseBuilder(IMailboxSession mailboxSession, IThreadedConversation conversation, IModernConversationNodeFactory conversationNodeFactory, IParticipantResolver resolver, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments) : base(mailboxSession, conversation, requestArguments, loadingList, conversationNodeFactory, resolver)
		{
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00097F6C File Offset: 0x0009616C
		protected override IEnumerable<Tuple<IConversationThread, ConversationThreadType>> XsoAndEwsConversationNodes
		{
			get
			{
				List<IConversationThread> threads = base.Conversation.Threads.ToList<IConversationThread>();
				for (int i = 0; i < threads.Count; i++)
				{
					yield return new Tuple<IConversationThread, ConversationThreadType>(threads[i], base.Response.ConversationThreads[i]);
				}
				yield break;
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00097F8C File Offset: 0x0009618C
		protected override ThreadedConversationResponseType BuildSkeleton()
		{
			ThreadedConversationResponseType threadedConversationResponseType = new ThreadedConversationResponseType();
			int num = base.Conversation.Threads.Count<IConversationThread>();
			ConversationThreadType[] array = new ConversationThreadType[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new ConversationThreadType();
			}
			threadedConversationResponseType.ConversationThreads = array;
			return threadedConversationResponseType;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00097FD3 File Offset: 0x000961D3
		private BaseItemId[] GetItemIds(StoreObjectId[] itemIds)
		{
			return ConversationDataConverter.GetItemIds(base.MailboxSession, itemIds);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00098104 File Offset: 0x00096304
		protected override void BuildNodes()
		{
			base.BuildNodes();
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.ThreadId = this.GetConversationId(c.ThreadId);
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.Preview = c.Preview;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.UniqueSenders = base.ParticipantResolver.ResolveToEmailAddressWrapper(c.UniqueSenders);
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.LastDeliveryTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(c.LastDeliveryTime.GetValueOrDefault());
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalHasAttachments = c.HasAttachments;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalHasIrm = c.HasIrm;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalImportance = (ImportanceType)c.Importance;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalIconIndex = (IconIndexType)c.IconIndex;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalFlagStatus = (FlagStatusType)c.FlagStatus;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalMessageCount = c.ItemCount;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.UnreadCount = c.UnreadCount;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.InitialMessage = base.ConversationNodeFactory.CreateInstance(c.FirstNode);
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalItemIds = this.GetItemIds(c.ItemIds);
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalRichContent = c.RichContent;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.GlobalItemClasses = c.ItemClasses;
			});
			base.PopulateResponseWith(delegate(IConversationThread c, ConversationThreadType r)
			{
				r.DraftItemIds = this.GetItemIds(c.DraftItemIds);
			});
			base.Response.TotalThreadCount = base.Response.ConversationThreads.Length;
		}
	}
}
