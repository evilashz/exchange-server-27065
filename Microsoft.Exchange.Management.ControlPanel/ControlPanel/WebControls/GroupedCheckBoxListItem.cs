using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020004FB RID: 1275
	[DataContract]
	public class GroupedCheckBoxListItem : BaseRow
	{
		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000B8B11 File Offset: 0x000B6D11
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000B8B19 File Offset: 0x000B6D19
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000B8B22 File Offset: 0x000B6D22
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x000B8B2A File Offset: 0x000B6D2A
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000B8B33 File Offset: 0x000B6D33
		// (set) Token: 0x06003D8F RID: 15759 RVA: 0x000B8B3B File Offset: 0x000B6D3B
		[DataMember]
		public string Group { get; set; }

		// Token: 0x06003D90 RID: 15760 RVA: 0x000B8B44 File Offset: 0x000B6D44
		public GroupedCheckBoxListItem() : base(null, null)
		{
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000B8B4E File Offset: 0x000B6D4E
		public GroupedCheckBoxListItem(Identity identity, ADObject configurationObject) : base(identity, configurationObject)
		{
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000B8B58 File Offset: 0x000B6D58
		public GroupedCheckBoxListItem(ADObject configurationObject) : base(configurationObject)
		{
		}
	}
}
