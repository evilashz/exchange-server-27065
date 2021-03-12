using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A85 RID: 2693
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTextMessagingAccountRequest : BaseJsonRequest
	{
		// Token: 0x06004C40 RID: 19520 RVA: 0x00106285 File Offset: 0x00104485
		public override string ToString()
		{
			return "GetTextMessagingAccountRequest";
		}
	}
}
