using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C75 RID: 3189
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAndPostModernGroupItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FC8 RID: 12232
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateAndPostModernGroupItemResponse Body;
	}
}
