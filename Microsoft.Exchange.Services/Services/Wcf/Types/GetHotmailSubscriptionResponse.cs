using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA6 RID: 2726
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetHotmailSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x001069AC File Offset: 0x00104BAC
		// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x001069B4 File Offset: 0x00104BB4
		[DataMember(IsRequired = true)]
		public HotmailSubscription HotmailSubscription { get; set; }

		// Token: 0x06004CEA RID: 19690 RVA: 0x001069BD File Offset: 0x00104BBD
		public override string ToString()
		{
			return string.Format("GetHotmailSubscriptionResponse: {0}", this.HotmailSubscription);
		}
	}
}
