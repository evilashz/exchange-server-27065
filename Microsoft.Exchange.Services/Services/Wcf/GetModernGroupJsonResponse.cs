using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C6A RID: 3178
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FBE RID: 12222
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernGroupResponse Body;
	}
}
