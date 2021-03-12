using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A78 RID: 2680
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CompareTextMessagingVerificationCodeRequest : BaseJsonRequest
	{
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00105F15 File Offset: 0x00104115
		// (set) Token: 0x06004BF6 RID: 19446 RVA: 0x00105F1D File Offset: 0x0010411D
		[DataMember(IsRequired = true)]
		public string VerificationCode { get; set; }

		// Token: 0x06004BF7 RID: 19447 RVA: 0x00105F26 File Offset: 0x00104126
		public override string ToString()
		{
			return string.Format("CompareTextMessagingVerificationCodeRequest: VerificationCode = {0}", this.VerificationCode);
		}
	}
}
