using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA3 RID: 2979
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EFA RID: 12026
		[DataMember(IsRequired = true, Order = 0)]
		public GetItemRequest Body;
	}
}
