using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A91 RID: 2705
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetTextMessagingAccountRequest : BaseJsonRequest
	{
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x001064F1 File Offset: 0x001046F1
		// (set) Token: 0x06004C7C RID: 19580 RVA: 0x001064F9 File Offset: 0x001046F9
		[DataMember(IsRequired = true)]
		public SetTextMessagingAccountData Options { get; set; }

		// Token: 0x06004C7D RID: 19581 RVA: 0x00106502 File Offset: 0x00104702
		public override string ToString()
		{
			return string.Format("SetTextMessagingAccountRequest: {0}", this.Options);
		}
	}
}
