using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF7 RID: 2807
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConnectSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x00109049 File Offset: 0x00107249
		// (set) Token: 0x06004FDB RID: 20443 RVA: 0x00109051 File Offset: 0x00107251
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x06004FDC RID: 20444 RVA: 0x0010905A File Offset: 0x0010725A
		public override string ToString()
		{
			return string.Format("GetConnectSubscriptionRequest: {0}", this.Identity);
		}
	}
}
