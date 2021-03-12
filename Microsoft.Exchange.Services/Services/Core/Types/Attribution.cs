using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C5 RID: 1733
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AttributionType")]
	[XmlType(TypeName = "Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class Attribution
	{
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000BFD8D File Offset: 0x000BDF8D
		// (set) Token: 0x0600356A RID: 13674 RVA: 0x000BFD95 File Offset: 0x000BDF95
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public string Id { get; set; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000BFD9E File Offset: 0x000BDF9E
		// (set) Token: 0x0600356C RID: 13676 RVA: 0x000BFDA6 File Offset: 0x000BDFA6
		[DataMember(IsRequired = true, Order = 2)]
		[XmlElement]
		public ItemId SourceId { get; set; }

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000BFDAF File Offset: 0x000BDFAF
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x000BFDB7 File Offset: 0x000BDFB7
		[XmlElement]
		[DataMember(IsRequired = true, Order = 3)]
		public string DisplayName { get; set; }

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000BFDC0 File Offset: 0x000BDFC0
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x000BFDC8 File Offset: 0x000BDFC8
		[DataMember(IsRequired = false, Order = 4)]
		[XmlElement]
		public bool IsWritable { get; set; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000BFDD1 File Offset: 0x000BDFD1
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000BFDD9 File Offset: 0x000BDFD9
		[DataMember(IsRequired = false, Order = 5)]
		[XmlElement]
		public bool IsQuickContact { get; set; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000BFDE2 File Offset: 0x000BDFE2
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000BFDEA File Offset: 0x000BDFEA
		[XmlElement]
		[DataMember(IsRequired = false, Order = 6)]
		public bool IsHidden { get; set; }

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000BFDF3 File Offset: 0x000BDFF3
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000BFDFB File Offset: 0x000BDFFB
		[DataMember(IsRequired = false, Order = 7)]
		[XmlElement]
		public FolderId FolderId { get; set; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000BFE04 File Offset: 0x000BE004
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000BFE0C File Offset: 0x000BE00C
		[XmlIgnore]
		[DataMember(IsRequired = false, Order = 8)]
		public string FolderName { get; set; }

		// Token: 0x06003579 RID: 13689 RVA: 0x000BFE15 File Offset: 0x000BE015
		public Attribution()
		{
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000BFE1D File Offset: 0x000BE01D
		public Attribution(string id, ItemId sourceId, string displayName)
		{
			this.Id = id;
			this.SourceId = sourceId;
			this.DisplayName = displayName;
		}
	}
}
