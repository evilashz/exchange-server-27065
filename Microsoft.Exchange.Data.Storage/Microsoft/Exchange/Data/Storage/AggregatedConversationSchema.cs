using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0B RID: 3083
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedConversationSchema : Schema
	{
		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06006E07 RID: 28167 RVA: 0x001D88ED File Offset: 0x001D6AED
		public new static AggregatedConversationSchema Instance
		{
			get
			{
				return AggregatedConversationSchema.instance;
			}
		}

		// Token: 0x04003ECA RID: 16074
		public const string IdPropertyName = "Id";

		// Token: 0x04003ECB RID: 16075
		public const string LastDeliveryTimePropertyName = "LastDeliveryTime";

		// Token: 0x04003ECC RID: 16076
		public const string TopicPropertyName = "ConversationTopic";

		// Token: 0x04003ECD RID: 16077
		public const string PreviewPropertyName = "Preview";

		// Token: 0x04003ECE RID: 16078
		public const string HasAttachmentsPropertyName = "HasAttachments";

		// Token: 0x04003ECF RID: 16079
		public const string HasIrmPropertyName = "HasIrm";

		// Token: 0x04003ED0 RID: 16080
		public const string ItemCountPropertyName = "ItemCount";

		// Token: 0x04003ED1 RID: 16081
		public const string SizePropertyName = "Size";

		// Token: 0x04003ED2 RID: 16082
		public const string ImportancePropertyName = "Importance";

		// Token: 0x04003ED3 RID: 16083
		public const string TotalItemLikesPropertyName = "TotalItemLikes";

		// Token: 0x04003ED4 RID: 16084
		public const string DirectParticipantsPropertyName = "DirectParticipants";

		// Token: 0x04003ED5 RID: 16085
		public const string ItemIdsPropertyName = "ItemIds";

		// Token: 0x04003ED6 RID: 16086
		public const string DraftItemIdsPropertyName = "DraftItemIds";

		// Token: 0x04003ED7 RID: 16087
		public const string ItemClassesPropertyName = "ItemClasses";

		// Token: 0x04003ED8 RID: 16088
		public const string InstanceKeyPropertyName = "InstanceKey";

		// Token: 0x04003ED9 RID: 16089
		public const string ConversationLikesPropertyName = "ConversationLikes";

		// Token: 0x04003EDA RID: 16090
		public const string IconIndexPropertyName = "IconIndex";

		// Token: 0x04003EDB RID: 16091
		public const string FlagStatusPropertyName = "FlagStatus";

		// Token: 0x04003EDC RID: 16092
		public const string UnreadCountPropertyName = "UnreadCount";

		// Token: 0x04003EDD RID: 16093
		public const string RichContentPropertyName = "RichContent";

		// Token: 0x04003EDE RID: 16094
		public static readonly ApplicationAggregatedProperty Id = new ApplicationAggregatedProperty("Id", typeof(ConversationId), PropertyFlags.None, ConversationPropertyAggregationStrategy.ConversationIdProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EDF RID: 16095
		public static readonly ApplicationAggregatedProperty LastDeliveryTime = new ApplicationAggregatedProperty("LastDeliveryTime", typeof(ExDateTime), PropertyFlags.None, ConversationPropertyAggregationStrategy.LastDeliveryTimeProperty, SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalConversationGlobalLastDeliveryTime));

		// Token: 0x04003EE0 RID: 16096
		public static readonly ApplicationAggregatedProperty Topic = new ApplicationAggregatedProperty("ConversationTopic", typeof(string), PropertyFlags.None, ConversationPropertyAggregationStrategy.ConversationTopicProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE1 RID: 16097
		public static readonly ApplicationAggregatedProperty Preview = new ApplicationAggregatedProperty("Preview", typeof(string), PropertyFlags.None, ConversationPropertyAggregationStrategy.PreviewProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE2 RID: 16098
		public static readonly ApplicationAggregatedProperty HasAttachments = new ApplicationAggregatedProperty("HasAttachments", typeof(bool), PropertyFlags.None, ConversationPropertyAggregationStrategy.HasAttachmentsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE3 RID: 16099
		public static readonly ApplicationAggregatedProperty HasIrm = new ApplicationAggregatedProperty("HasIrm", typeof(bool), PropertyFlags.None, ConversationPropertyAggregationStrategy.HasIrmProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE4 RID: 16100
		public static readonly ApplicationAggregatedProperty ItemCount = new ApplicationAggregatedProperty("ItemCount", typeof(int), PropertyFlags.None, ConversationPropertyAggregationStrategy.ItemCountProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE5 RID: 16101
		public static readonly ApplicationAggregatedProperty Size = new ApplicationAggregatedProperty("Size", typeof(int), PropertyFlags.None, ConversationPropertyAggregationStrategy.SizeProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE6 RID: 16102
		public static readonly ApplicationAggregatedProperty Importance = new ApplicationAggregatedProperty("Importance", typeof(Importance), PropertyFlags.None, ConversationPropertyAggregationStrategy.ImportanceProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE7 RID: 16103
		public static readonly ApplicationAggregatedProperty TotalItemLikes = new ApplicationAggregatedProperty("TotalItemLikes", typeof(int), PropertyFlags.None, ConversationPropertyAggregationStrategy.TotalItemLikesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE8 RID: 16104
		public static readonly ApplicationAggregatedProperty ConversationLikes = new ApplicationAggregatedProperty("ConversationLikes", typeof(int), PropertyFlags.None, ConversationPropertyAggregationStrategy.ConversationLikesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EE9 RID: 16105
		public static readonly ApplicationAggregatedProperty DirectParticipants = new ApplicationAggregatedProperty("DirectParticipants", typeof(Participant[]), PropertyFlags.Multivalued, ConversationPropertyAggregationStrategy.DirectParticipantsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EEA RID: 16106
		public static readonly ApplicationAggregatedProperty ItemIds = new ApplicationAggregatedProperty("ItemIds", typeof(StoreObjectId[]), PropertyFlags.Multivalued, PropertyAggregationStrategy.EntryIdsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EEB RID: 16107
		public static readonly ApplicationAggregatedProperty DraftItemIds = new ApplicationAggregatedProperty("DraftItemIds", typeof(StoreObjectId[]), PropertyFlags.Multivalued, ConversationPropertyAggregationStrategy.DraftItemIdsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EEC RID: 16108
		public static readonly ApplicationAggregatedProperty ItemClasses = new ApplicationAggregatedProperty("ItemClasses", typeof(string[]), PropertyFlags.Multivalued, PropertyAggregationStrategy.ItemClassesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EED RID: 16109
		public static readonly ApplicationAggregatedProperty IconIndex = new ApplicationAggregatedProperty("IconIndex", typeof(IconIndex), PropertyFlags.None, ConversationPropertyAggregationStrategy.IconIndexProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EEE RID: 16110
		public static readonly ApplicationAggregatedProperty FlagStatus = new ApplicationAggregatedProperty("FlagStatus", typeof(FlagStatus), PropertyFlags.None, ConversationPropertyAggregationStrategy.FlagStatusProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EEF RID: 16111
		public static readonly ApplicationAggregatedProperty UnreadCount = new ApplicationAggregatedProperty("UnreadCount", typeof(int), PropertyFlags.None, ConversationPropertyAggregationStrategy.UnreadCountProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EF0 RID: 16112
		public static readonly ApplicationAggregatedProperty RichContent = new ApplicationAggregatedProperty("RichContent", typeof(short[]), PropertyFlags.Multivalued, ConversationPropertyAggregationStrategy.RichContentProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EF1 RID: 16113
		public static readonly ApplicationAggregatedProperty InstanceKey = new ApplicationAggregatedProperty("InstanceKey", typeof(byte[]), PropertyFlags.Multivalued, ConversationPropertyAggregationStrategy.InstanceKeyProperty, SortByAndFilterStrategy.None);

		// Token: 0x04003EF2 RID: 16114
		private static readonly AggregatedConversationSchema instance = new AggregatedConversationSchema();
	}
}
