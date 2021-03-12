using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000260 RID: 608
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationType
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00026E34 File Offset: 0x00025034
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00026E3C File Offset: 0x0002503C
		public ItemIdType ConversationId
		{
			get
			{
				return this.conversationIdField;
			}
			set
			{
				this.conversationIdField = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00026E45 File Offset: 0x00025045
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00026E4D File Offset: 0x0002504D
		public string ConversationTopic
		{
			get
			{
				return this.conversationTopicField;
			}
			set
			{
				this.conversationTopicField = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00026E56 File Offset: 0x00025056
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x00026E5E File Offset: 0x0002505E
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueRecipients
		{
			get
			{
				return this.uniqueRecipientsField;
			}
			set
			{
				this.uniqueRecipientsField = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00026E67 File Offset: 0x00025067
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x00026E6F File Offset: 0x0002506F
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueRecipients
		{
			get
			{
				return this.globalUniqueRecipientsField;
			}
			set
			{
				this.globalUniqueRecipientsField = value;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00026E78 File Offset: 0x00025078
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00026E80 File Offset: 0x00025080
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueUnreadSenders
		{
			get
			{
				return this.uniqueUnreadSendersField;
			}
			set
			{
				this.uniqueUnreadSendersField = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00026E89 File Offset: 0x00025089
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00026E91 File Offset: 0x00025091
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueUnreadSenders
		{
			get
			{
				return this.globalUniqueUnreadSendersField;
			}
			set
			{
				this.globalUniqueUnreadSendersField = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00026E9A File Offset: 0x0002509A
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00026EA2 File Offset: 0x000250A2
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueSenders
		{
			get
			{
				return this.uniqueSendersField;
			}
			set
			{
				this.uniqueSendersField = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00026EAB File Offset: 0x000250AB
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00026EB3 File Offset: 0x000250B3
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueSenders
		{
			get
			{
				return this.globalUniqueSendersField;
			}
			set
			{
				this.globalUniqueSendersField = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00026EBC File Offset: 0x000250BC
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x00026EC4 File Offset: 0x000250C4
		public DateTime LastDeliveryTime
		{
			get
			{
				return this.lastDeliveryTimeField;
			}
			set
			{
				this.lastDeliveryTimeField = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00026ECD File Offset: 0x000250CD
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x00026ED5 File Offset: 0x000250D5
		[XmlIgnore]
		public bool LastDeliveryTimeSpecified
		{
			get
			{
				return this.lastDeliveryTimeFieldSpecified;
			}
			set
			{
				this.lastDeliveryTimeFieldSpecified = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00026EDE File Offset: 0x000250DE
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00026EE6 File Offset: 0x000250E6
		public DateTime GlobalLastDeliveryTime
		{
			get
			{
				return this.globalLastDeliveryTimeField;
			}
			set
			{
				this.globalLastDeliveryTimeField = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00026EEF File Offset: 0x000250EF
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x00026EF7 File Offset: 0x000250F7
		[XmlIgnore]
		public bool GlobalLastDeliveryTimeSpecified
		{
			get
			{
				return this.globalLastDeliveryTimeFieldSpecified;
			}
			set
			{
				this.globalLastDeliveryTimeFieldSpecified = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00026F00 File Offset: 0x00025100
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x00026F08 File Offset: 0x00025108
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories
		{
			get
			{
				return this.categoriesField;
			}
			set
			{
				this.categoriesField = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00026F11 File Offset: 0x00025111
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00026F19 File Offset: 0x00025119
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalCategories
		{
			get
			{
				return this.globalCategoriesField;
			}
			set
			{
				this.globalCategoriesField = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00026F22 File Offset: 0x00025122
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x00026F2A File Offset: 0x0002512A
		public FlagStatusType FlagStatus
		{
			get
			{
				return this.flagStatusField;
			}
			set
			{
				this.flagStatusField = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00026F33 File Offset: 0x00025133
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x00026F3B File Offset: 0x0002513B
		[XmlIgnore]
		public bool FlagStatusSpecified
		{
			get
			{
				return this.flagStatusFieldSpecified;
			}
			set
			{
				this.flagStatusFieldSpecified = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00026F44 File Offset: 0x00025144
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x00026F4C File Offset: 0x0002514C
		public FlagStatusType GlobalFlagStatus
		{
			get
			{
				return this.globalFlagStatusField;
			}
			set
			{
				this.globalFlagStatusField = value;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00026F55 File Offset: 0x00025155
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x00026F5D File Offset: 0x0002515D
		[XmlIgnore]
		public bool GlobalFlagStatusSpecified
		{
			get
			{
				return this.globalFlagStatusFieldSpecified;
			}
			set
			{
				this.globalFlagStatusFieldSpecified = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00026F66 File Offset: 0x00025166
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x00026F6E File Offset: 0x0002516E
		public bool HasAttachments
		{
			get
			{
				return this.hasAttachmentsField;
			}
			set
			{
				this.hasAttachmentsField = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00026F77 File Offset: 0x00025177
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x00026F7F File Offset: 0x0002517F
		[XmlIgnore]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return this.hasAttachmentsFieldSpecified;
			}
			set
			{
				this.hasAttachmentsFieldSpecified = value;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00026F88 File Offset: 0x00025188
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00026F90 File Offset: 0x00025190
		public bool GlobalHasAttachments
		{
			get
			{
				return this.globalHasAttachmentsField;
			}
			set
			{
				this.globalHasAttachmentsField = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00026F99 File Offset: 0x00025199
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x00026FA1 File Offset: 0x000251A1
		[XmlIgnore]
		public bool GlobalHasAttachmentsSpecified
		{
			get
			{
				return this.globalHasAttachmentsFieldSpecified;
			}
			set
			{
				this.globalHasAttachmentsFieldSpecified = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00026FAA File Offset: 0x000251AA
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x00026FB2 File Offset: 0x000251B2
		public int MessageCount
		{
			get
			{
				return this.messageCountField;
			}
			set
			{
				this.messageCountField = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00026FBB File Offset: 0x000251BB
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x00026FC3 File Offset: 0x000251C3
		[XmlIgnore]
		public bool MessageCountSpecified
		{
			get
			{
				return this.messageCountFieldSpecified;
			}
			set
			{
				this.messageCountFieldSpecified = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00026FCC File Offset: 0x000251CC
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00026FD4 File Offset: 0x000251D4
		public int GlobalMessageCount
		{
			get
			{
				return this.globalMessageCountField;
			}
			set
			{
				this.globalMessageCountField = value;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00026FDD File Offset: 0x000251DD
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x00026FE5 File Offset: 0x000251E5
		[XmlIgnore]
		public bool GlobalMessageCountSpecified
		{
			get
			{
				return this.globalMessageCountFieldSpecified;
			}
			set
			{
				this.globalMessageCountFieldSpecified = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00026FEE File Offset: 0x000251EE
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x00026FF6 File Offset: 0x000251F6
		public int UnreadCount
		{
			get
			{
				return this.unreadCountField;
			}
			set
			{
				this.unreadCountField = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00026FFF File Offset: 0x000251FF
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x00027007 File Offset: 0x00025207
		[XmlIgnore]
		public bool UnreadCountSpecified
		{
			get
			{
				return this.unreadCountFieldSpecified;
			}
			set
			{
				this.unreadCountFieldSpecified = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00027010 File Offset: 0x00025210
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x00027018 File Offset: 0x00025218
		public int GlobalUnreadCount
		{
			get
			{
				return this.globalUnreadCountField;
			}
			set
			{
				this.globalUnreadCountField = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00027021 File Offset: 0x00025221
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x00027029 File Offset: 0x00025229
		[XmlIgnore]
		public bool GlobalUnreadCountSpecified
		{
			get
			{
				return this.globalUnreadCountFieldSpecified;
			}
			set
			{
				this.globalUnreadCountFieldSpecified = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00027032 File Offset: 0x00025232
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0002703A File Offset: 0x0002523A
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00027043 File Offset: 0x00025243
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x0002704B File Offset: 0x0002524B
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00027054 File Offset: 0x00025254
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x0002705C File Offset: 0x0002525C
		public int GlobalSize
		{
			get
			{
				return this.globalSizeField;
			}
			set
			{
				this.globalSizeField = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00027065 File Offset: 0x00025265
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x0002706D File Offset: 0x0002526D
		[XmlIgnore]
		public bool GlobalSizeSpecified
		{
			get
			{
				return this.globalSizeFieldSpecified;
			}
			set
			{
				this.globalSizeFieldSpecified = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00027076 File Offset: 0x00025276
		// (set) Token: 0x060016D5 RID: 5845 RVA: 0x0002707E File Offset: 0x0002527E
		[XmlArrayItem("ItemClass", IsNullable = false)]
		public string[] ItemClasses
		{
			get
			{
				return this.itemClassesField;
			}
			set
			{
				this.itemClassesField = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00027087 File Offset: 0x00025287
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0002708F File Offset: 0x0002528F
		[XmlArrayItem("ItemClass", IsNullable = false)]
		public string[] GlobalItemClasses
		{
			get
			{
				return this.globalItemClassesField;
			}
			set
			{
				this.globalItemClassesField = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00027098 File Offset: 0x00025298
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x000270A0 File Offset: 0x000252A0
		public ImportanceChoicesType Importance
		{
			get
			{
				return this.importanceField;
			}
			set
			{
				this.importanceField = value;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000270A9 File Offset: 0x000252A9
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x000270B1 File Offset: 0x000252B1
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.importanceFieldSpecified;
			}
			set
			{
				this.importanceFieldSpecified = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000270BA File Offset: 0x000252BA
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x000270C2 File Offset: 0x000252C2
		public ImportanceChoicesType GlobalImportance
		{
			get
			{
				return this.globalImportanceField;
			}
			set
			{
				this.globalImportanceField = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000270CB File Offset: 0x000252CB
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x000270D3 File Offset: 0x000252D3
		[XmlIgnore]
		public bool GlobalImportanceSpecified
		{
			get
			{
				return this.globalImportanceFieldSpecified;
			}
			set
			{
				this.globalImportanceFieldSpecified = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x000270DC File Offset: 0x000252DC
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x000270E4 File Offset: 0x000252E4
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		public BaseItemIdType[] ItemIds
		{
			get
			{
				return this.itemIdsField;
			}
			set
			{
				this.itemIdsField = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000270ED File Offset: 0x000252ED
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x000270F5 File Offset: 0x000252F5
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		public BaseItemIdType[] GlobalItemIds
		{
			get
			{
				return this.globalItemIdsField;
			}
			set
			{
				this.globalItemIdsField = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x000270FE File Offset: 0x000252FE
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x00027106 File Offset: 0x00025306
		public DateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTimeField;
			}
			set
			{
				this.lastModifiedTimeField = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0002710F File Offset: 0x0002530F
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00027117 File Offset: 0x00025317
		[XmlIgnore]
		public bool LastModifiedTimeSpecified
		{
			get
			{
				return this.lastModifiedTimeFieldSpecified;
			}
			set
			{
				this.lastModifiedTimeFieldSpecified = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00027120 File Offset: 0x00025320
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x00027128 File Offset: 0x00025328
		[XmlElement(DataType = "base64Binary")]
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKeyField;
			}
			set
			{
				this.instanceKeyField = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00027131 File Offset: 0x00025331
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x00027139 File Offset: 0x00025339
		public string Preview
		{
			get
			{
				return this.previewField;
			}
			set
			{
				this.previewField = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00027142 File Offset: 0x00025342
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0002714A File Offset: 0x0002534A
		public MailboxSearchLocationType MailboxScope
		{
			get
			{
				return this.mailboxScopeField;
			}
			set
			{
				this.mailboxScopeField = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00027153 File Offset: 0x00025353
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x0002715B File Offset: 0x0002535B
		[XmlIgnore]
		public bool MailboxScopeSpecified
		{
			get
			{
				return this.mailboxScopeFieldSpecified;
			}
			set
			{
				this.mailboxScopeFieldSpecified = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00027164 File Offset: 0x00025364
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x0002716C File Offset: 0x0002536C
		public IconIndexType IconIndex
		{
			get
			{
				return this.iconIndexField;
			}
			set
			{
				this.iconIndexField = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00027175 File Offset: 0x00025375
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x0002717D File Offset: 0x0002537D
		[XmlIgnore]
		public bool IconIndexSpecified
		{
			get
			{
				return this.iconIndexFieldSpecified;
			}
			set
			{
				this.iconIndexFieldSpecified = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00027186 File Offset: 0x00025386
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x0002718E File Offset: 0x0002538E
		public IconIndexType GlobalIconIndex
		{
			get
			{
				return this.globalIconIndexField;
			}
			set
			{
				this.globalIconIndexField = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00027197 File Offset: 0x00025397
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x0002719F File Offset: 0x0002539F
		[XmlIgnore]
		public bool GlobalIconIndexSpecified
		{
			get
			{
				return this.globalIconIndexFieldSpecified;
			}
			set
			{
				this.globalIconIndexFieldSpecified = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000271A8 File Offset: 0x000253A8
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x000271B0 File Offset: 0x000253B0
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		public BaseItemIdType[] DraftItemIds
		{
			get
			{
				return this.draftItemIdsField;
			}
			set
			{
				this.draftItemIdsField = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000271B9 File Offset: 0x000253B9
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x000271C1 File Offset: 0x000253C1
		public bool HasIrm
		{
			get
			{
				return this.hasIrmField;
			}
			set
			{
				this.hasIrmField = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000271CA File Offset: 0x000253CA
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x000271D2 File Offset: 0x000253D2
		[XmlIgnore]
		public bool HasIrmSpecified
		{
			get
			{
				return this.hasIrmFieldSpecified;
			}
			set
			{
				this.hasIrmFieldSpecified = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000271DB File Offset: 0x000253DB
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x000271E3 File Offset: 0x000253E3
		public bool GlobalHasIrm
		{
			get
			{
				return this.globalHasIrmField;
			}
			set
			{
				this.globalHasIrmField = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000271EC File Offset: 0x000253EC
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x000271F4 File Offset: 0x000253F4
		[XmlIgnore]
		public bool GlobalHasIrmSpecified
		{
			get
			{
				return this.globalHasIrmFieldSpecified;
			}
			set
			{
				this.globalHasIrmFieldSpecified = value;
			}
		}

		// Token: 0x04000F64 RID: 3940
		private ItemIdType conversationIdField;

		// Token: 0x04000F65 RID: 3941
		private string conversationTopicField;

		// Token: 0x04000F66 RID: 3942
		private string[] uniqueRecipientsField;

		// Token: 0x04000F67 RID: 3943
		private string[] globalUniqueRecipientsField;

		// Token: 0x04000F68 RID: 3944
		private string[] uniqueUnreadSendersField;

		// Token: 0x04000F69 RID: 3945
		private string[] globalUniqueUnreadSendersField;

		// Token: 0x04000F6A RID: 3946
		private string[] uniqueSendersField;

		// Token: 0x04000F6B RID: 3947
		private string[] globalUniqueSendersField;

		// Token: 0x04000F6C RID: 3948
		private DateTime lastDeliveryTimeField;

		// Token: 0x04000F6D RID: 3949
		private bool lastDeliveryTimeFieldSpecified;

		// Token: 0x04000F6E RID: 3950
		private DateTime globalLastDeliveryTimeField;

		// Token: 0x04000F6F RID: 3951
		private bool globalLastDeliveryTimeFieldSpecified;

		// Token: 0x04000F70 RID: 3952
		private string[] categoriesField;

		// Token: 0x04000F71 RID: 3953
		private string[] globalCategoriesField;

		// Token: 0x04000F72 RID: 3954
		private FlagStatusType flagStatusField;

		// Token: 0x04000F73 RID: 3955
		private bool flagStatusFieldSpecified;

		// Token: 0x04000F74 RID: 3956
		private FlagStatusType globalFlagStatusField;

		// Token: 0x04000F75 RID: 3957
		private bool globalFlagStatusFieldSpecified;

		// Token: 0x04000F76 RID: 3958
		private bool hasAttachmentsField;

		// Token: 0x04000F77 RID: 3959
		private bool hasAttachmentsFieldSpecified;

		// Token: 0x04000F78 RID: 3960
		private bool globalHasAttachmentsField;

		// Token: 0x04000F79 RID: 3961
		private bool globalHasAttachmentsFieldSpecified;

		// Token: 0x04000F7A RID: 3962
		private int messageCountField;

		// Token: 0x04000F7B RID: 3963
		private bool messageCountFieldSpecified;

		// Token: 0x04000F7C RID: 3964
		private int globalMessageCountField;

		// Token: 0x04000F7D RID: 3965
		private bool globalMessageCountFieldSpecified;

		// Token: 0x04000F7E RID: 3966
		private int unreadCountField;

		// Token: 0x04000F7F RID: 3967
		private bool unreadCountFieldSpecified;

		// Token: 0x04000F80 RID: 3968
		private int globalUnreadCountField;

		// Token: 0x04000F81 RID: 3969
		private bool globalUnreadCountFieldSpecified;

		// Token: 0x04000F82 RID: 3970
		private int sizeField;

		// Token: 0x04000F83 RID: 3971
		private bool sizeFieldSpecified;

		// Token: 0x04000F84 RID: 3972
		private int globalSizeField;

		// Token: 0x04000F85 RID: 3973
		private bool globalSizeFieldSpecified;

		// Token: 0x04000F86 RID: 3974
		private string[] itemClassesField;

		// Token: 0x04000F87 RID: 3975
		private string[] globalItemClassesField;

		// Token: 0x04000F88 RID: 3976
		private ImportanceChoicesType importanceField;

		// Token: 0x04000F89 RID: 3977
		private bool importanceFieldSpecified;

		// Token: 0x04000F8A RID: 3978
		private ImportanceChoicesType globalImportanceField;

		// Token: 0x04000F8B RID: 3979
		private bool globalImportanceFieldSpecified;

		// Token: 0x04000F8C RID: 3980
		private BaseItemIdType[] itemIdsField;

		// Token: 0x04000F8D RID: 3981
		private BaseItemIdType[] globalItemIdsField;

		// Token: 0x04000F8E RID: 3982
		private DateTime lastModifiedTimeField;

		// Token: 0x04000F8F RID: 3983
		private bool lastModifiedTimeFieldSpecified;

		// Token: 0x04000F90 RID: 3984
		private byte[] instanceKeyField;

		// Token: 0x04000F91 RID: 3985
		private string previewField;

		// Token: 0x04000F92 RID: 3986
		private MailboxSearchLocationType mailboxScopeField;

		// Token: 0x04000F93 RID: 3987
		private bool mailboxScopeFieldSpecified;

		// Token: 0x04000F94 RID: 3988
		private IconIndexType iconIndexField;

		// Token: 0x04000F95 RID: 3989
		private bool iconIndexFieldSpecified;

		// Token: 0x04000F96 RID: 3990
		private IconIndexType globalIconIndexField;

		// Token: 0x04000F97 RID: 3991
		private bool globalIconIndexFieldSpecified;

		// Token: 0x04000F98 RID: 3992
		private BaseItemIdType[] draftItemIdsField;

		// Token: 0x04000F99 RID: 3993
		private bool hasIrmField;

		// Token: 0x04000F9A RID: 3994
		private bool hasIrmFieldSpecified;

		// Token: 0x04000F9B RID: 3995
		private bool globalHasIrmField;

		// Token: 0x04000F9C RID: 3996
		private bool globalHasIrmFieldSpecified;
	}
}
