using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C69 RID: 3177
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FBD RID: 12221
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernGroupRequest Body;
	}
}
