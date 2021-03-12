using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200014F RID: 335
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ItemHasKnownEntityRule : ActivationRule
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x00022A42 File Offset: 0x00020C42
		public ItemHasKnownEntityRule(KnownEntityType entityType, string filterName, string regExFilter, bool ignoreCase) : base("ItemHasKnownEntity")
		{
			this.EntityType = entityType;
			this.FilterName = filterName;
			this.RegExFilter = regExFilter;
			this.IgnoreCase = ignoreCase;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00022A6C File Offset: 0x00020C6C
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00022A74 File Offset: 0x00020C74
		[DataMember]
		public KnownEntityType EntityType { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00022A7D File Offset: 0x00020C7D
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00022A85 File Offset: 0x00020C85
		[DataMember]
		public string FilterName { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00022A8E File Offset: 0x00020C8E
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00022A96 File Offset: 0x00020C96
		[DataMember]
		public string RegExFilter { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00022A9F File Offset: 0x00020C9F
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00022AA7 File Offset: 0x00020CA7
		[DataMember]
		public bool IgnoreCase { get; set; }
	}
}
