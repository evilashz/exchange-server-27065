using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C72 RID: 3186
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PostModernGroupItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FC5 RID: 12229
		[DataMember(IsRequired = true, Order = 0)]
		public PostModernGroupItemRequest Body;
	}
}
