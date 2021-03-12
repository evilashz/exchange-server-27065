using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000405 RID: 1029
	[DataContract]
	public class OneDriveProItemsPage
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x0007E682 File Offset: 0x0007C882
		public OneDriveProItemsPage()
		{
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0007E68C File Offset: 0x0007C88C
		internal OneDriveProItemsPage(int pageIndex, IListItem item)
		{
			this.PageIndex = pageIndex;
			this.ID = item["ID"].ToString();
			this.Name = (string)item["FileLeafRef"];
			this.ObjectType = (string)item["FSObjType"];
			this.SortBehavior = ((FieldLookupValue)item["SortBehavior"]).LookupValue;
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0007E703 File Offset: 0x0007C903
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x0007E70B File Offset: 0x0007C90B
		[DataMember]
		public int PageIndex { get; set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x0007E714 File Offset: 0x0007C914
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x0007E71C File Offset: 0x0007C91C
		[DataMember]
		public string ID { get; set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x0007E725 File Offset: 0x0007C925
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x0007E72D File Offset: 0x0007C92D
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x0007E736 File Offset: 0x0007C936
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x0007E73E File Offset: 0x0007C93E
		[DataMember]
		public string ObjectType { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0007E747 File Offset: 0x0007C947
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x0007E74F File Offset: 0x0007C94F
		[DataMember]
		public string SortBehavior { get; set; }
	}
}
