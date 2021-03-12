using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000150 RID: 336
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ItemHasRegularExpressionMatchRule : ActivationRule
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public ItemHasRegularExpressionMatchRule(string regExName, string regExValue, RegExPropertyName propertyName, bool ignoreCase) : base("ItemHasRegularExpressionMatch")
		{
			this.RegExName = regExName;
			this.RegExValue = regExValue;
			this.PropertyName = propertyName;
			this.IgnoreCase = ignoreCase;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00022ADA File Offset: 0x00020CDA
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00022AE2 File Offset: 0x00020CE2
		[DataMember]
		public string RegExName { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00022AEB File Offset: 0x00020CEB
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00022AF3 File Offset: 0x00020CF3
		[DataMember]
		public string RegExValue { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00022AFC File Offset: 0x00020CFC
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00022B04 File Offset: 0x00020D04
		[DataMember]
		public RegExPropertyName PropertyName { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00022B0D File Offset: 0x00020D0D
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00022B15 File Offset: 0x00020D15
		[DataMember]
		public bool IgnoreCase { get; set; }
	}
}
