using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFF RID: 2815
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewConnectSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06005008 RID: 20488 RVA: 0x0010927B File Offset: 0x0010747B
		// (set) Token: 0x06005009 RID: 20489 RVA: 0x00109283 File Offset: 0x00107483
		[DataMember(IsRequired = true)]
		public NewConnectSubscriptionData ConnectSubscription { get; set; }

		// Token: 0x0600500A RID: 20490 RVA: 0x0010928C File Offset: 0x0010748C
		public override string ToString()
		{
			return string.Format("NewConnectSubscriptionRequest: {0}", this.ConnectSubscription);
		}
	}
}
