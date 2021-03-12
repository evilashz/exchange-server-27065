using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F64 RID: 3940
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationDataExtractorFactory : IConversationDataExtractorFactory
	{
		// Token: 0x060086D1 RID: 34513 RVA: 0x0024F441 File Offset: 0x0024D641
		public ConversationDataExtractorFactory(IXSOFactory xsoFactory, IMailboxSession session)
		{
			this.session = session;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060086D2 RID: 34514 RVA: 0x0024F457 File Offset: 0x0024D657
		public ConversationDataExtractor Create(bool isIrmEnabled, HashSet<PropertyDefinition> propertiesLoadedForExtractedItems, ConversationId conversationId, IConversationTree tree)
		{
			return this.Create(isIrmEnabled, propertiesLoadedForExtractedItems, conversationId, tree, false, null);
		}

		// Token: 0x060086D3 RID: 34515 RVA: 0x0024F468 File Offset: 0x0024D668
		public ConversationDataExtractor Create(bool isIrmEnabled, HashSet<PropertyDefinition> propertiesLoadedForExtractedItems, ConversationId conversationId, IConversationTree tree, bool isSmimeSupported, string domainName)
		{
			PropertyDefinition[] queriedPropertyDefinitions = (propertiesLoadedForExtractedItems != null) ? propertiesLoadedForExtractedItems.ToArray<PropertyDefinition>() : Array<PropertyDefinition>.Empty;
			int totalNodeCount = (tree == null) ? 0 : tree.Count;
			ItemPartLoader itemPartLoader = new ItemPartLoader(this.xsoFactory, this.session, isIrmEnabled, queriedPropertyDefinitions);
			OptimizationInfo optimizationInfo = new OptimizationInfo(conversationId, totalNodeCount);
			return new ConversationDataExtractor(itemPartLoader, isIrmEnabled, optimizationInfo, isSmimeSupported, domainName);
		}

		// Token: 0x04005A23 RID: 23075
		private readonly IMailboxSession session;

		// Token: 0x04005A24 RID: 23076
		private readonly IXSOFactory xsoFactory;
	}
}
