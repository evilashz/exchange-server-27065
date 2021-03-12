using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000686 RID: 1670
	[XmlType(TypeName = "UnifiedGroupsSet", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "UnifiedGroupsSet", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class UnifiedGroupsSet
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000B847C File Offset: 0x000B667C
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x000B8484 File Offset: 0x000B6684
		[XmlElement("FilterType", typeof(UnifiedGroupsFilterType))]
		[DataMember(Name = "FilterType", EmitDefaultValue = false)]
		public UnifiedGroupsFilterType FilterType { get; set; }

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000B848D File Offset: 0x000B668D
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x000B8495 File Offset: 0x000B6695
		[XmlElement("TotalGroups", typeof(int))]
		[DataMember(Name = "TotalGroups", EmitDefaultValue = false)]
		public int TotalGroups { get; set; }

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000B849E File Offset: 0x000B669E
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x000B84A6 File Offset: 0x000B66A6
		[XmlArray("Groups")]
		[XmlArrayItem("UnifiedGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(UnifiedGroup))]
		[DataMember(Name = "Groups", EmitDefaultValue = false)]
		public UnifiedGroup[] Groups { get; set; }
	}
}
