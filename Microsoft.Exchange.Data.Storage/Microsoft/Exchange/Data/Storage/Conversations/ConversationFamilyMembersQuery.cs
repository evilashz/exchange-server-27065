using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D4 RID: 2260
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationFamilyMembersQuery : ConversationMembersQueryBase
	{
		// Token: 0x06005426 RID: 21542 RVA: 0x0015DBC5 File Offset: 0x0015BDC5
		public ConversationFamilyMembersQuery(IXSOFactory xsoFactory, IMailboxSession session) : base(xsoFactory, session)
		{
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0015DBCF File Offset: 0x0015BDCF
		protected override ComparisonFilter CreateConversationFilter(ConversationId conversationFamilyId)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.MapiConversationFamilyId, conversationFamilyId.GetBytes());
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0015DBE2 File Offset: 0x0015BDE2
		protected override IFolder GetSearchFolder(ICollection<PropertyDefinition> headerPropertyDefinition)
		{
			return base.XsoFactory.BindToFolder(base.Session, DefaultFolderType.AllItems, headerPropertyDefinition.ToArray<PropertyDefinition>());
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0015DBFD File Offset: 0x0015BDFD
		protected override IQueryResult GetQueryResult(IFolder rootFolder, ComparisonFilter conversationFilter, ICollection<PropertyDefinition> headerPropertyDefinition)
		{
			return rootFolder.IItemQuery(ItemQueryType.None, conversationFilter, ConversationFamilyMembersQuery.SortColumns, headerPropertyDefinition);
		}

		// Token: 0x04002D80 RID: 11648
		private static readonly SortBy[] SortColumns = new SortBy[]
		{
			new SortBy(InternalSchema.MapiConversationFamilyId, SortOrder.Ascending),
			new SortBy(ItemSchema.ConversationIndex, SortOrder.Ascending)
		};
	}
}
