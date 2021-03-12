using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA5 RID: 2725
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConnectedAccountsResponse : OptionsResponseBase
	{
		// Token: 0x06004CE4 RID: 19684 RVA: 0x00106976 File Offset: 0x00104B76
		public GetConnectedAccountsResponse()
		{
			this.ConnectedAccountsInformation = new ConnectedAccountsInformation();
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x00106989 File Offset: 0x00104B89
		// (set) Token: 0x06004CE6 RID: 19686 RVA: 0x00106991 File Offset: 0x00104B91
		[DataMember(IsRequired = true)]
		public ConnectedAccountsInformation ConnectedAccountsInformation { get; set; }

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0010699A File Offset: 0x00104B9A
		public override string ToString()
		{
			return string.Format("GetConnectedAccountsResponse: {0}", this.ConnectedAccountsInformation);
		}
	}
}
