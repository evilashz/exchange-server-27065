using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000151 RID: 337
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ItemIsRule : ActivationRule
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00022B1E File Offset: 0x00020D1E
		public ItemIsRule(ItemIsRuleItemType itemType, string itemClass, bool includeSubClasses = false, ItemIsRuleFormType formType = ItemIsRuleFormType.Read) : base("ItemIs")
		{
			this.ItemClass = itemClass;
			this.IncludeSubClasses = includeSubClasses;
			this.ItemType = itemType;
			this.FormType = formType;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00022B48 File Offset: 0x00020D48
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00022B50 File Offset: 0x00020D50
		[DataMember]
		public ItemIsRuleItemType ItemType { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00022B59 File Offset: 0x00020D59
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00022B61 File Offset: 0x00020D61
		[DataMember]
		public ItemIsRuleFormType FormType { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00022B6A File Offset: 0x00020D6A
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x00022B72 File Offset: 0x00020D72
		[DataMember]
		public string ItemClass { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00022B7B File Offset: 0x00020D7B
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00022B83 File Offset: 0x00020D83
		[DataMember(EmitDefaultValue = false)]
		public bool IncludeSubClasses { get; set; }
	}
}
