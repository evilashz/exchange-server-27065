using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACE RID: 2766
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x00108171 File Offset: 0x00106371
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x00108179 File Offset: 0x00106379
		[DataMember(IsRequired = true)]
		public Subscription Subscription { get; set; }

		// Token: 0x06004EBD RID: 20157 RVA: 0x00108182 File Offset: 0x00106382
		public override string ToString()
		{
			return string.Format("NewSubscriptionResponse: {0}", this.Subscription);
		}
	}
}
