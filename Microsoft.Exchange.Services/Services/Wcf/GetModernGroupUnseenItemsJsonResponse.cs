using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C6C RID: 3180
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupUnseenItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FBF RID: 12223
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernGroupUnseenItemsResponse Body;
	}
}
