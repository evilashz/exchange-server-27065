using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B00 RID: 2816
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewConnectSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x001092A6 File Offset: 0x001074A6
		// (set) Token: 0x0600500D RID: 20493 RVA: 0x001092AE File Offset: 0x001074AE
		[DataMember(IsRequired = true)]
		public ConnectSubscription ConnectSubscription { get; set; }

		// Token: 0x0600500E RID: 20494 RVA: 0x001092B7 File Offset: 0x001074B7
		public override string ToString()
		{
			return string.Format("NewConnectSubscriptionResponse: {0}", this.ConnectSubscription);
		}
	}
}
