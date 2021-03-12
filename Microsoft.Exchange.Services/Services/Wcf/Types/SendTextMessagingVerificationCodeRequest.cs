using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A8B RID: 2699
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendTextMessagingVerificationCodeRequest : BaseJsonRequest
	{
		// Token: 0x06004C5F RID: 19551 RVA: 0x001063A6 File Offset: 0x001045A6
		public override string ToString()
		{
			return "SendTextMessagingVerificationCodeRequest";
		}
	}
}
