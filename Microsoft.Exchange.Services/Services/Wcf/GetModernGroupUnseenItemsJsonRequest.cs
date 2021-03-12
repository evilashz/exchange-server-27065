using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C6D RID: 3181
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupUnseenItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FC0 RID: 12224
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernGroupUnseenItemsRequest Body;
	}
}
