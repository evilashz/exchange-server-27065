using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C77 RID: 3191
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateResponseFromModernGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FCA RID: 12234
		[DataMember(IsRequired = true, Order = 0)]
		public CreateResponseFromModernGroupResponse Body;
	}
}
