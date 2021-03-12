using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000617 RID: 1559
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "QueryStringType")]
	[Serializable]
	public class QueryStringType
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000B6C0B File Offset: 0x000B4E0B
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x000B6C13 File Offset: 0x000B4E13
		[XmlAttribute]
		[DataMember(IsRequired = false)]
		public bool ResetCache { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000B6C1C File Offset: 0x000B4E1C
		// (set) Token: 0x060030F0 RID: 12528 RVA: 0x000B6C24 File Offset: 0x000B4E24
		[DataMember(IsRequired = false)]
		[XmlAttribute]
		public bool ReturnHighlightTerms { get; set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000B6C2D File Offset: 0x000B4E2D
		// (set) Token: 0x060030F2 RID: 12530 RVA: 0x000B6C35 File Offset: 0x000B4E35
		[XmlAttribute]
		[DataMember(IsRequired = false)]
		public bool ReturnDeletedItems { get; set; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000B6C3E File Offset: 0x000B4E3E
		// (set) Token: 0x060030F4 RID: 12532 RVA: 0x000B6C46 File Offset: 0x000B4E46
		[XmlAttribute]
		[DataMember(IsRequired = false)]
		public int MaxResultsCount { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000B6C4F File Offset: 0x000B4E4F
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x000B6C57 File Offset: 0x000B4E57
		[DataMember(IsRequired = false)]
		[XmlAttribute]
		public bool WaitForSearchComplete { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000B6C60 File Offset: 0x000B4E60
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x000B6C68 File Offset: 0x000B4E68
		[DataMember(IsRequired = false)]
		[XmlAttribute]
		public bool OptimizedSearch { get; set; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000B6C71 File Offset: 0x000B4E71
		// (set) Token: 0x060030FA RID: 12538 RVA: 0x000B6C79 File Offset: 0x000B4E79
		[XmlText]
		[DataMember(IsRequired = true)]
		public string Value { get; set; }
	}
}
