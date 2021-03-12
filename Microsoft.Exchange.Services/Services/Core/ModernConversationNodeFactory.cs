using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003BE RID: 958
	internal class ModernConversationNodeFactory : ConversationNodeFactoryBase<ConversationNode>, IModernConversationNodeFactory
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x0009ADA4 File Offset: 0x00098FA4
		public ModernConversationNodeFactory(MailboxSession mailboxSession, ICoreConversation conversation, IParticipantResolver participantResolver, ItemResponseShape itemResponse, ICollection<PropertyDefinition> mandatoryPropertiesToLoad, ICollection<PropertyDefinition> conversationPropertiesToLoad, HashSet<PropertyDefinition> propertiesLoaded, Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem, IEnumerable<IConversationTreeNode> itemsToBeFullyLoaded, bool isOwaCall) : base(mailboxSession, conversation, participantResolver, itemResponse, mandatoryPropertiesToLoad, conversationPropertiesToLoad, propertiesLoaded, propertiesLoadedPerItem, isOwaCall)
		{
			this.itemsToBeFullyLoaded = itemsToBeFullyLoaded;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0009ADD0 File Offset: 0x00098FD0
		public ConversationNode CreateInstance(IConversationTreeNode treeNode)
		{
			return base.CreateInstance(treeNode, new Func<StoreObjectId, bool>(this.ShouldReturnOnlyMandatoryProperties));
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0009ADF2 File Offset: 0x00098FF2
		public bool TryLoadRelatedItemInfo(IConversationTreeNode treeNode, out IRelatedItemInfo relatedItem)
		{
			relatedItem = null;
			return treeNode.HasData && this.TryLoadRelatedItemInfo(treeNode.MainPropertyBag, out relatedItem);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0009AE10 File Offset: 0x00099010
		public bool TryLoadRelatedItemInfo(IStorePropertyBag storePropertyBag, out IRelatedItemInfo relatedItem)
		{
			VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
			StoreObjectId objectId = versionedId.ObjectId;
			ItemType itemType = null;
			relatedItem = null;
			if (base.TryLoadServiceItemType(objectId, storePropertyBag, false, out itemType))
			{
				relatedItem = (itemType as IRelatedItemInfo);
				return relatedItem != null;
			}
			return false;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0009AE59 File Offset: 0x00099059
		protected override ConversationNode CreateEmptyInstance()
		{
			return new ConversationNode();
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0009AE78 File Offset: 0x00099078
		private bool ShouldReturnOnlyMandatoryProperties(StoreObjectId id)
		{
			return !this.itemsToBeFullyLoaded.Any((IConversationTreeNode node) => node.IsPartOf(id));
		}

		// Token: 0x040011B4 RID: 4532
		private readonly IEnumerable<IConversationTreeNode> itemsToBeFullyLoaded;
	}
}
