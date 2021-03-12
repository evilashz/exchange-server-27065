using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB4 RID: 2740
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x06004D1F RID: 19743 RVA: 0x00106C31 File Offset: 0x00104E31
		public GetSubscriptionResponse()
		{
			this.SubscriptionCollection = new SubscriptionCollection();
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x00106C44 File Offset: 0x00104E44
		// (set) Token: 0x06004D21 RID: 19745 RVA: 0x00106C4C File Offset: 0x00104E4C
		[DataMember(IsRequired = true)]
		public SubscriptionCollection SubscriptionCollection { get; set; }

		// Token: 0x06004D22 RID: 19746 RVA: 0x00106C55 File Offset: 0x00104E55
		public override string ToString()
		{
			return string.Format("GetSubscriptionResponse: {0}", this.SubscriptionCollection);
		}
	}
}
