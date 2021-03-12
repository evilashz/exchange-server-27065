using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000630 RID: 1584
	[XmlType(TypeName = "RequestedUnifiedGroupsSet", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RequestedUnifiedGroupsSet
	{
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000B7076 File Offset: 0x000B5276
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000B707E File Offset: 0x000B527E
		[XmlElement("FilterType", typeof(UnifiedGroupsFilterType))]
		[DataMember(Name = "FilterType", EmitDefaultValue = false, IsRequired = true)]
		public UnifiedGroupsFilterType FilterType { get; set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000B7087 File Offset: 0x000B5287
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x000B708F File Offset: 0x000B528F
		[XmlElement("SortType", typeof(UnifiedGroupsSortType))]
		[DataMember(Name = "SortType", EmitDefaultValue = false, IsRequired = false)]
		public UnifiedGroupsSortType SortType { get; set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000B7098 File Offset: 0x000B5298
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x000B70A0 File Offset: 0x000B52A0
		[DataMember(Name = "SortDirection", EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("SortDirection", typeof(SortDirection))]
		public SortDirection SortDirection { get; set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000B70A9 File Offset: 0x000B52A9
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x000B70B1 File Offset: 0x000B52B1
		[DataMember(Name = "GroupsLimit", EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("GroupsLimit", typeof(int))]
		public int GroupsLimit { get; set; }
	}
}
