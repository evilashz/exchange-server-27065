using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200014B RID: 331
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ItemHasAttachmentRule))]
	[KnownType(typeof(ItemHasKnownEntityRule))]
	[KnownType(typeof(ItemIsRule))]
	[KnownType(typeof(ItemHasRegularExpressionMatchRule))]
	[KnownType(typeof(CollectionRule))]
	public abstract class ActivationRule
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x000229D0 File Offset: 0x00020BD0
		public ActivationRule(string type)
		{
			this.Type = type;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000229DF File Offset: 0x00020BDF
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x000229E7 File Offset: 0x00020BE7
		[DataMember]
		public string Type { get; set; }

		// Token: 0x040006D1 RID: 1745
		internal const string ExchangeJsonNameSpace = "http://schemas.datacontract.org/2004/07/Exchange";
	}
}
