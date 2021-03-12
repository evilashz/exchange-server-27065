using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB0 RID: 2736
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPopSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x00106B92 File Offset: 0x00104D92
		// (set) Token: 0x06004D13 RID: 19731 RVA: 0x00106B9A File Offset: 0x00104D9A
		[DataMember(IsRequired = true)]
		public PopSubscription PopSubscription { get; set; }

		// Token: 0x06004D14 RID: 19732 RVA: 0x00106BA3 File Offset: 0x00104DA3
		public override string ToString()
		{
			return string.Format("GetPopSubscriptionResponse: {0}", this.PopSubscription);
		}
	}
}
