using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008E3 RID: 2275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationMembersQuery : ConversationMembersQueryBase
	{
		// Token: 0x06005558 RID: 21848 RVA: 0x00162188 File Offset: 0x00160388
		public ConversationMembersQuery(IXSOFactory xsoFactory, IMailboxSession session) : base(xsoFactory, session)
		{
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00162192 File Offset: 0x00160392
		protected override ComparisonFilter CreateConversationFilter(ConversationId conversationId)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.MapiConversationId, conversationId.GetBytes());
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x001621A5 File Offset: 0x001603A5
		protected override IFolder GetSearchFolder(ICollection<PropertyDefinition> headerPropertyDefinition)
		{
			return base.XsoFactory.BindToFolder(base.Session, DefaultFolderType.Configuration, headerPropertyDefinition.ToArray<PropertyDefinition>());
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x001621C0 File Offset: 0x001603C0
		protected override IQueryResult GetQueryResult(IFolder rootFolder, ComparisonFilter conversationFilter, ICollection<PropertyDefinition> headerPropertyDefinition)
		{
			return rootFolder.IConversationMembersQuery(conversationFilter, null, headerPropertyDefinition);
		}
	}
}
