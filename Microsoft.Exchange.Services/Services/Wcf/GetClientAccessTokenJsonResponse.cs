using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB8 RID: 3000
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetClientAccessTokenJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F0F RID: 12047
		[DataMember(IsRequired = true, Order = 0)]
		public GetClientAccessTokenResponse Body;
	}
}
