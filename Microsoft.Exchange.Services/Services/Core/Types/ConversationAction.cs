using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F8 RID: 1016
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ConversationActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConversationAction
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0009E4F7 File Offset: 0x0009C6F7
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x0009E4FF File Offset: 0x0009C6FF
		[XmlElement("Action", typeof(ConversationActionType))]
		[IgnoreDataMember]
		public ConversationActionType Action { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0009E508 File Offset: 0x0009C708
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0009E51A File Offset: 0x0009C71A
		[XmlIgnore]
		[DataMember(Name = "Action", IsRequired = true)]
		public string ActionString
		{
			get
			{
				return this.Action.ToString();
			}
			set
			{
				this.Action = (ConversationActionType)Enum.Parse(typeof(ConversationActionType), value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0009E537 File Offset: 0x0009C737
		// (set) Token: 0x06001C8E RID: 7310 RVA: 0x0009E53F File Offset: 0x0009C73F
		[DataMember]
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Categories { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0009E548 File Offset: 0x0009C748
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x0009E550 File Offset: 0x0009C750
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool ClearCategories { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0009E559 File Offset: 0x0009C759
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x0009E561 File Offset: 0x0009C761
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlIgnore]
		public string[] CategoriesToRemove { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0009E56A File Offset: 0x0009C76A
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0009E572 File Offset: 0x0009C772
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("ConversationLastSyncTime")]
		[DateTimeString]
		public string ConversationLastSyncTime { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0009E57B File Offset: 0x0009C77B
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0009E583 File Offset: 0x0009C783
		[DataMember(IsRequired = true)]
		[XmlElement("ConversationId")]
		public ItemId ConversationId { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0009E58C File Offset: 0x0009C78C
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0009E594 File Offset: 0x0009C794
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement("ContextFolderId")]
		public TargetFolderId ContextFolderId { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0009E59D File Offset: 0x0009C79D
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x0009E5A5 File Offset: 0x0009C7A5
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement("DestinationFolderId")]
		public TargetFolderId DestinationFolderId { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0009E5AE File Offset: 0x0009C7AE
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0009E5B6 File Offset: 0x0009C7B6
		[DataMember(IsRequired = false)]
		[XmlElement("EnableAlwaysDelete")]
		public bool EnableAlwaysDelete { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0009E5BF File Offset: 0x0009C7BF
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x0009E5C7 File Offset: 0x0009C7C7
		[XmlElement("ProcessRightAway")]
		[DataMember(IsRequired = false)]
		public bool ProcessRightAway { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0009E5D0 File Offset: 0x0009C7D0
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0009E5D8 File Offset: 0x0009C7D8
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement("IsRead")]
		public bool? IsRead { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0009E5E1 File Offset: 0x0009C7E1
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0009E5E9 File Offset: 0x0009C7E9
		[IgnoreDataMember]
		[XmlElement("DeleteType")]
		public DisposalType? DeleteType { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0009E5F4 File Offset: 0x0009C7F4
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x0009E62B File Offset: 0x0009C82B
		[DataMember(Name = "DeleteType", IsRequired = false, EmitDefaultValue = false)]
		[XmlIgnore]
		public string DeleteTypeString
		{
			get
			{
				if (this.DeleteType != null)
				{
					return this.DeleteType.Value.ToString();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.DeleteType = (DisposalType?)Enum.Parse(typeof(DisposalType), value);
				}
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0009E64B File Offset: 0x0009C84B
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x0009E653 File Offset: 0x0009C853
		[DataMember(IsRequired = false)]
		[XmlElement("RetentionPolicyType")]
		public RetentionType? RetentionPolicyType { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0009E65C File Offset: 0x0009C85C
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x0009E664 File Offset: 0x0009C864
		[XmlElement("RetentionPolicyTagId")]
		[DataMember(IsRequired = false)]
		public string RetentionPolicyTagId { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0009E66D File Offset: 0x0009C86D
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x0009E675 File Offset: 0x0009C875
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement("Flag")]
		public FlagType Flag { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0009E67E File Offset: 0x0009C87E
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x0009E686 File Offset: 0x0009C886
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlElement("SuppressReadReceipts")]
		public bool? SuppressReadReceipts { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0009E68F File Offset: 0x0009C88F
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x0009E697 File Offset: 0x0009C897
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlIgnore]
		public bool? IsClutter { get; set; }
	}
}
