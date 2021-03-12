using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types.Conversations
{
	// Token: 0x0200068E RID: 1678
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationThread")]
	[Serializable]
	public class ConversationThreadType : IConversationDataResponse
	{
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000B86A8 File Offset: 0x000B68A8
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x000B86B0 File Offset: 0x000B68B0
		[DataMember(Name = "ThreadId", IsRequired = true, Order = 1)]
		public ItemId ThreadId { get; set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000B86B9 File Offset: 0x000B68B9
		// (set) Token: 0x06003357 RID: 13143 RVA: 0x000B86DE File Offset: 0x000B68DE
		[DataMember(Name = "ConversationNodes", EmitDefaultValue = false, Order = 2)]
		public ConversationNode[] ConversationNodes
		{
			get
			{
				if (this.conversationNodes != null && this.conversationNodes.Count > 0)
				{
					return this.conversationNodes.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.conversationNodes = null;
					return;
				}
				this.conversationNodes = new List<ConversationNode>(value);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000B86FC File Offset: 0x000B68FC
		// (set) Token: 0x06003359 RID: 13145 RVA: 0x000B8704 File Offset: 0x000B6904
		[DataMember(Name = "TotalConversationNodesCount", IsRequired = false, Order = 3)]
		public int TotalConversationNodesCount { get; set; }

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000B870D File Offset: 0x000B690D
		// (set) Token: 0x0600335B RID: 13147 RVA: 0x000B8732 File Offset: 0x000B6932
		[DataMember(Name = "ToRecipients", EmitDefaultValue = false, Order = 4)]
		public EmailAddressWrapper[] ToRecipients
		{
			get
			{
				if (this.toRecipients != null && this.toRecipients.Count > 0)
				{
					return this.toRecipients.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.toRecipients = null;
					return;
				}
				this.toRecipients = new List<EmailAddressWrapper>(value);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000B8750 File Offset: 0x000B6950
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x000B8775 File Offset: 0x000B6975
		[DataMember(Name = "CcRecipients", EmitDefaultValue = false, Order = 5)]
		public EmailAddressWrapper[] CcRecipients
		{
			get
			{
				if (this.ccRecipients != null && this.ccRecipients.Count > 0)
				{
					return this.ccRecipients.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.ccRecipients = null;
					return;
				}
				this.ccRecipients = new List<EmailAddressWrapper>(value);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000B8793 File Offset: 0x000B6993
		// (set) Token: 0x0600335F RID: 13151 RVA: 0x000B879B File Offset: 0x000B699B
		[DataMember(Name = "LastDeliveryTime", EmitDefaultValue = false, Order = 6)]
		[DateTimeString]
		public string LastDeliveryTime { get; set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000B87A4 File Offset: 0x000B69A4
		// (set) Token: 0x06003361 RID: 13153 RVA: 0x000B87AC File Offset: 0x000B69AC
		[DataMember(Name = "UniqueSenders", EmitDefaultValue = false, Order = 7)]
		public EmailAddressWrapper[] UniqueSenders { get; set; }

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000B87B5 File Offset: 0x000B69B5
		// (set) Token: 0x06003363 RID: 13155 RVA: 0x000B87BD File Offset: 0x000B69BD
		[DataMember(Name = "Preview", EmitDefaultValue = false, Order = 8)]
		public string Preview { get; set; }

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000B87C6 File Offset: 0x000B69C6
		// (set) Token: 0x06003365 RID: 13157 RVA: 0x000B87CE File Offset: 0x000B69CE
		[DataMember(Name = "GlobalHasAttachments", EmitDefaultValue = false, Order = 9)]
		public bool GlobalHasAttachments { get; set; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000B87D7 File Offset: 0x000B69D7
		// (set) Token: 0x06003367 RID: 13159 RVA: 0x000B87DF File Offset: 0x000B69DF
		[DataMember(Name = "GlobalHasIrm", EmitDefaultValue = false, Order = 10)]
		public bool GlobalHasIrm { get; set; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000B87E8 File Offset: 0x000B69E8
		// (set) Token: 0x06003369 RID: 13161 RVA: 0x000B87F0 File Offset: 0x000B69F0
		[DataMember(Name = "GlobalImportance", EmitDefaultValue = false, Order = 11)]
		public ImportanceType GlobalImportance { get; set; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000B87F9 File Offset: 0x000B69F9
		// (set) Token: 0x0600336B RID: 13163 RVA: 0x000B8801 File Offset: 0x000B6A01
		[IgnoreDataMember]
		[XmlElement("GlobalIconIndex")]
		public IconIndexType GlobalIconIndex { get; set; }

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000B880A File Offset: 0x000B6A0A
		// (set) Token: 0x0600336D RID: 13165 RVA: 0x000B8812 File Offset: 0x000B6A12
		[IgnoreDataMember]
		[XmlElement("GlobalFlagStatus")]
		public FlagStatusType GlobalFlagStatus { get; set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000B881B File Offset: 0x000B6A1B
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000B8823 File Offset: 0x000B6A23
		[DataMember(Name = "GlobalMessageCount", EmitDefaultValue = false, Order = 12)]
		public int GlobalMessageCount { get; set; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000B882C File Offset: 0x000B6A2C
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x000B8834 File Offset: 0x000B6A34
		[DataMember(Name = "UnreadCount", EmitDefaultValue = false, Order = 13)]
		public int UnreadCount { get; set; }

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000B883D File Offset: 0x000B6A3D
		// (set) Token: 0x06003373 RID: 13171 RVA: 0x000B8845 File Offset: 0x000B6A45
		[DataMember(Name = "InitialMessage", EmitDefaultValue = false, Order = 14)]
		public ConversationNode InitialMessage { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000B884E File Offset: 0x000B6A4E
		// (set) Token: 0x06003375 RID: 13173 RVA: 0x000B8856 File Offset: 0x000B6A56
		[DataMember(EmitDefaultValue = false, Order = 15)]
		[XmlArrayItem("ItemId", typeof(ItemId), IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), IsNullable = false)]
		public BaseItemId[] GlobalItemIds { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000B885F File Offset: 0x000B6A5F
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x000B8867 File Offset: 0x000B6A67
		[DataMember(EmitDefaultValue = false, Order = 16)]
		[XmlArrayItem("Int16", IsNullable = false)]
		public short[] GlobalRichContent { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000B8870 File Offset: 0x000B6A70
		// (set) Token: 0x06003379 RID: 13177 RVA: 0x000B8878 File Offset: 0x000B6A78
		[XmlArrayItem("ItemClass", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 17)]
		public string[] GlobalItemClasses { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000B8881 File Offset: 0x000B6A81
		// (set) Token: 0x0600337B RID: 13179 RVA: 0x000B8889 File Offset: 0x000B6A89
		[DataMember(EmitDefaultValue = false, Order = 18)]
		[XmlArrayItem("ItemId", typeof(ItemId), IsNullable = false)]
		public BaseItemId[] DraftItemIds { get; set; }

		// Token: 0x04001D06 RID: 7430
		private List<ConversationNode> conversationNodes;

		// Token: 0x04001D07 RID: 7431
		private List<EmailAddressWrapper> toRecipients;

		// Token: 0x04001D08 RID: 7432
		private List<EmailAddressWrapper> ccRecipients;
	}
}
