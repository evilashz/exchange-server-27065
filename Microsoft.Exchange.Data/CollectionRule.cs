using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200014D RID: 333
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CollectionRule : ActivationRule
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x000229F8 File Offset: 0x00020BF8
		public CollectionRule(string mode, ActivationRule[] rules) : base("Collection")
		{
			this.Mode = mode;
			this.Rules = rules;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00022A13 File Offset: 0x00020C13
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00022A1B File Offset: 0x00020C1B
		[DataMember]
		public string Mode { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00022A24 File Offset: 0x00020C24
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00022A2C File Offset: 0x00020C2C
		[DataMember]
		public ActivationRule[] Rules { get; set; }
	}
}
