using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB7 RID: 2999
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetClientAccessTokenJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F0E RID: 12046
		[DataMember(IsRequired = true, Order = 0)]
		public GetClientAccessTokenRequest Body;
	}
}
