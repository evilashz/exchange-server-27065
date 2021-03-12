using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B02 RID: 2818
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveConnectSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x001092E5 File Offset: 0x001074E5
		// (set) Token: 0x06005013 RID: 20499 RVA: 0x001092ED File Offset: 0x001074ED
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x06005014 RID: 20500 RVA: 0x001092F6 File Offset: 0x001074F6
		public override string ToString()
		{
			return string.Format("RemoveConnectSubscriptionRequest: {0}", this.Identity);
		}
	}
}
