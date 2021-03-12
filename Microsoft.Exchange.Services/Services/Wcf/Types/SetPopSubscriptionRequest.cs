using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE3 RID: 2787
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetPopSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00108894 File Offset: 0x00106A94
		// (set) Token: 0x06004F54 RID: 20308 RVA: 0x0010889C File Offset: 0x00106A9C
		[DataMember(IsRequired = true)]
		public SetPopSubscriptionData PopSubscription { get; set; }

		// Token: 0x06004F55 RID: 20309 RVA: 0x001088A5 File Offset: 0x00106AA5
		public override string ToString()
		{
			return string.Format("SetPopSubscriptionRequest: {0}", this.PopSubscription);
		}
	}
}
