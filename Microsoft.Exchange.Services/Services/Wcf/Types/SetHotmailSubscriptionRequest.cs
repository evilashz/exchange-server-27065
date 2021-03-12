using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD9 RID: 2777
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetHotmailSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0010848C File Offset: 0x0010668C
		// (set) Token: 0x06004F03 RID: 20227 RVA: 0x00108494 File Offset: 0x00106694
		[DataMember(IsRequired = true)]
		public SetHotmailSubscriptionData HotmailSubscription { get; set; }

		// Token: 0x06004F04 RID: 20228 RVA: 0x0010849D File Offset: 0x0010669D
		public override string ToString()
		{
			return string.Format("SetHotmailSubscriptionRequest: {0}", this.HotmailSubscription);
		}
	}
}
