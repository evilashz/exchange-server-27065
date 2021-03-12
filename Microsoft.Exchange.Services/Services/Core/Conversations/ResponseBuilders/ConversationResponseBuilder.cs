using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders
{
	// Token: 0x020003AC RID: 940
	internal class ConversationResponseBuilder : ConversationDataResponseBuilderBase<IConversation, IConversation, ConversationResponseType, ConversationResponseType>
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x00097CE1 File Offset: 0x00095EE1
		public ConversationResponseBuilder(IMailboxSession mailboxSession, IConversation conversation, IModernConversationNodeFactory conversationNodeFactory, IParticipantResolver resolver, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments) : base(mailboxSession, conversation, requestArguments, loadingList, conversationNodeFactory, resolver)
		{
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00097DD0 File Offset: 0x00095FD0
		protected override IEnumerable<Tuple<IConversation, ConversationResponseType>> XsoAndEwsConversationNodes
		{
			get
			{
				yield return new Tuple<IConversation, ConversationResponseType>(base.Conversation, base.Response);
				yield break;
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00097DED File Offset: 0x00095FED
		protected override ConversationResponseType BuildSkeleton()
		{
			return new ConversationResponseType();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00097DF4 File Offset: 0x00095FF4
		protected override void BuildConversationProperties()
		{
			base.BuildConversationProperties();
			base.Response.CanDelete = EffectiveRightsProperty.GetFromEffectiveRights(base.Conversation.EffectiveRights, base.MailboxSession).Delete;
		}
	}
}
