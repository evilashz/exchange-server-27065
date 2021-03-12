using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020004FC RID: 1276
	[DataContract]
	public class GroupedCheckBoxTreeItem : GroupedCheckBoxListItem
	{
		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000B8B61 File Offset: 0x000B6D61
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x000B8B69 File Offset: 0x000B6D69
		[DataMember]
		public string Parent { get; set; }

		// Token: 0x06003D95 RID: 15765 RVA: 0x000B8B72 File Offset: 0x000B6D72
		public GroupedCheckBoxTreeItem() : base(null, null)
		{
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000B8B7C File Offset: 0x000B6D7C
		public GroupedCheckBoxTreeItem(Identity identity, ADObject configurationObject) : base(identity, configurationObject)
		{
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000B8B86 File Offset: 0x000B6D86
		public GroupedCheckBoxTreeItem(ADObject configurationObject) : base(configurationObject)
		{
		}
	}
}
