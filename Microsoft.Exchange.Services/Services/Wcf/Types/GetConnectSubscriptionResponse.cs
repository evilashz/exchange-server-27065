using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF8 RID: 2808
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConnectSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x00109074 File Offset: 0x00107274
		public GetConnectSubscriptionResponse()
		{
			this.ConnectSubscriptionCollection = new ConnectSubscriptionCollection();
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x00109087 File Offset: 0x00107287
		// (set) Token: 0x06004FE0 RID: 20448 RVA: 0x0010908F File Offset: 0x0010728F
		[DataMember]
		public ConnectSubscriptionCollection ConnectSubscriptionCollection { get; set; }

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00109098 File Offset: 0x00107298
		public override string ToString()
		{
			return string.Format("GetConnectSubscriptionResponse: {0}", this.ConnectSubscriptionCollection);
		}
	}
}
