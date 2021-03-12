using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B05 RID: 2821
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetConnectSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x0010933C File Offset: 0x0010753C
		// (set) Token: 0x0600501B RID: 20507 RVA: 0x00109344 File Offset: 0x00107544
		[DataMember(IsRequired = true)]
		public SetConnectSubscriptionData ConnectSubscription { get; set; }

		// Token: 0x0600501C RID: 20508 RVA: 0x0010934D File Offset: 0x0010754D
		public override string ToString()
		{
			return string.Format("SetConnectSubscriptionRequest: {0}", this.ConnectSubscription);
		}
	}
}
