using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C74 RID: 3188
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAndPostModernGroupItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FC7 RID: 12231
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateAndPostModernGroupItemRequest Body;
	}
}
