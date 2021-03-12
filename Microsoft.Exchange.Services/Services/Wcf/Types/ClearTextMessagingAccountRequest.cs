using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A76 RID: 2678
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ClearTextMessagingAccountRequest : BaseJsonRequest
	{
		// Token: 0x06004BF2 RID: 19442 RVA: 0x00105EFE File Offset: 0x001040FE
		public override string ToString()
		{
			return "ClearTextMessagingAccountRequest";
		}
	}
}
