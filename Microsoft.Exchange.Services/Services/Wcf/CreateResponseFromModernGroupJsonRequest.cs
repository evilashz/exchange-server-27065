using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C76 RID: 3190
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateResponseFromModernGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FC9 RID: 12233
		[DataMember(IsRequired = true, Order = 0)]
		public CreateResponseFromModernGroupRequest Body;
	}
}
