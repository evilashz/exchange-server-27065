using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C6E RID: 3182
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FC1 RID: 12225
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernGroupsResponse Body;
	}
}
