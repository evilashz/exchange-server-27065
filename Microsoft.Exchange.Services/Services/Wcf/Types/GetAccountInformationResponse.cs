using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A81 RID: 2689
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAccountInformationResponse : OptionsResponseBase
	{
		// Token: 0x06004C29 RID: 19497 RVA: 0x00106197 File Offset: 0x00104397
		public GetAccountInformationResponse()
		{
			this.AccountInfo = new GetAccountInformation();
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x001061AA File Offset: 0x001043AA
		// (set) Token: 0x06004C2B RID: 19499 RVA: 0x001061B2 File Offset: 0x001043B2
		[DataMember(IsRequired = true)]
		public GetAccountInformation AccountInfo { get; set; }

		// Token: 0x06004C2C RID: 19500 RVA: 0x001061BB File Offset: 0x001043BB
		public override string ToString()
		{
			return string.Format("GetAccountInformationResponse: {0}", this.AccountInfo);
		}
	}
}
