using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000341 RID: 833
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationType
	{
		// Token: 0x040013B6 RID: 5046
		public ItemIdType ConversationId;

		// Token: 0x040013B7 RID: 5047
		public string ConversationTopic;

		// Token: 0x040013B8 RID: 5048
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueRecipients;

		// Token: 0x040013B9 RID: 5049
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueRecipients;

		// Token: 0x040013BA RID: 5050
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueUnreadSenders;

		// Token: 0x040013BB RID: 5051
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueUnreadSenders;

		// Token: 0x040013BC RID: 5052
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueSenders;

		// Token: 0x040013BD RID: 5053
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueSenders;

		// Token: 0x040013BE RID: 5054
		public DateTime LastDeliveryTime;

		// Token: 0x040013BF RID: 5055
		[XmlIgnore]
		public bool LastDeliveryTimeSpecified;

		// Token: 0x040013C0 RID: 5056
		public DateTime GlobalLastDeliveryTime;

		// Token: 0x040013C1 RID: 5057
		[XmlIgnore]
		public bool GlobalLastDeliveryTimeSpecified;

		// Token: 0x040013C2 RID: 5058
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories;

		// Token: 0x040013C3 RID: 5059
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalCategories;

		// Token: 0x040013C4 RID: 5060
		public FlagStatusType FlagStatus;

		// Token: 0x040013C5 RID: 5061
		[XmlIgnore]
		public bool FlagStatusSpecified;

		// Token: 0x040013C6 RID: 5062
		public FlagStatusType GlobalFlagStatus;

		// Token: 0x040013C7 RID: 5063
		[XmlIgnore]
		public bool GlobalFlagStatusSpecified;

		// Token: 0x040013C8 RID: 5064
		public bool HasAttachments;

		// Token: 0x040013C9 RID: 5065
		[XmlIgnore]
		public bool HasAttachmentsSpecified;

		// Token: 0x040013CA RID: 5066
		public bool GlobalHasAttachments;

		// Token: 0x040013CB RID: 5067
		[XmlIgnore]
		public bool GlobalHasAttachmentsSpecified;

		// Token: 0x040013CC RID: 5068
		public int MessageCount;

		// Token: 0x040013CD RID: 5069
		[XmlIgnore]
		public bool MessageCountSpecified;

		// Token: 0x040013CE RID: 5070
		public int GlobalMessageCount;

		// Token: 0x040013CF RID: 5071
		[XmlIgnore]
		public bool GlobalMessageCountSpecified;

		// Token: 0x040013D0 RID: 5072
		public int UnreadCount;

		// Token: 0x040013D1 RID: 5073
		[XmlIgnore]
		public bool UnreadCountSpecified;

		// Token: 0x040013D2 RID: 5074
		public int GlobalUnreadCount;

		// Token: 0x040013D3 RID: 5075
		[XmlIgnore]
		public bool GlobalUnreadCountSpecified;

		// Token: 0x040013D4 RID: 5076
		public int Size;

		// Token: 0x040013D5 RID: 5077
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x040013D6 RID: 5078
		public int GlobalSize;

		// Token: 0x040013D7 RID: 5079
		[XmlIgnore]
		public bool GlobalSizeSpecified;

		// Token: 0x040013D8 RID: 5080
		[XmlArrayItem("ItemClass", IsNullable = false)]
		public string[] ItemClasses;

		// Token: 0x040013D9 RID: 5081
		[XmlArrayItem("ItemClass", IsNullable = false)]
		public string[] GlobalItemClasses;

		// Token: 0x040013DA RID: 5082
		public ImportanceChoicesType Importance;

		// Token: 0x040013DB RID: 5083
		[XmlIgnore]
		public bool ImportanceSpecified;

		// Token: 0x040013DC RID: 5084
		public ImportanceChoicesType GlobalImportance;

		// Token: 0x040013DD RID: 5085
		[XmlIgnore]
		public bool GlobalImportanceSpecified;

		// Token: 0x040013DE RID: 5086
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		public BaseItemIdType[] ItemIds;

		// Token: 0x040013DF RID: 5087
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		public BaseItemIdType[] GlobalItemIds;

		// Token: 0x040013E0 RID: 5088
		public DateTime LastModifiedTime;

		// Token: 0x040013E1 RID: 5089
		[XmlIgnore]
		public bool LastModifiedTimeSpecified;

		// Token: 0x040013E2 RID: 5090
		[XmlElement(DataType = "base64Binary")]
		public byte[] InstanceKey;

		// Token: 0x040013E3 RID: 5091
		public string Preview;

		// Token: 0x040013E4 RID: 5092
		public MailboxSearchLocationType MailboxScope;

		// Token: 0x040013E5 RID: 5093
		[XmlIgnore]
		public bool MailboxScopeSpecified;

		// Token: 0x040013E6 RID: 5094
		public IconIndexType IconIndex;

		// Token: 0x040013E7 RID: 5095
		[XmlIgnore]
		public bool IconIndexSpecified;

		// Token: 0x040013E8 RID: 5096
		public IconIndexType GlobalIconIndex;

		// Token: 0x040013E9 RID: 5097
		[XmlIgnore]
		public bool GlobalIconIndexSpecified;

		// Token: 0x040013EA RID: 5098
		[XmlArrayItem("ItemId", typeof(ItemIdType), IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), IsNullable = false)]
		public BaseItemIdType[] DraftItemIds;

		// Token: 0x040013EB RID: 5099
		public bool HasIrm;

		// Token: 0x040013EC RID: 5100
		[XmlIgnore]
		public bool HasIrmSpecified;

		// Token: 0x040013ED RID: 5101
		public bool GlobalHasIrm;

		// Token: 0x040013EE RID: 5102
		[XmlIgnore]
		public bool GlobalHasIrmSpecified;
	}
}
